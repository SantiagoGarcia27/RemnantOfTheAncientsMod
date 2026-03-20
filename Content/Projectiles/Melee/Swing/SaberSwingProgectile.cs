/*using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RemnantOfTheAncientsMod.Common.Global.Items;
using RemnantOfTheAncientsMod.Common.Global.Items.WeaponsModels;
using System;
using System.IO;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Content.Projectiles.Melee.Swing
{
    // ─────────────────────────────────────────────────────────────────────────────
    // HOW THE ANIMATION WORKS (overview)
    // ─────────────────────────────────────────────────────────────────────────────
    // The sword is a "held projectile": it has no real velocity and is manually
    // positioned on the player's hand every frame inside SetSwordPosition().
    //
    // Its visual angle is driven by two values stored in the ai/localAI arrays:
    //   • InitialAngle  – the fixed starting angle calculated once on spawn.
    //   • Progress      – how far (in radians) the sword has rotated FROM that
    //                     starting angle. This value grows every frame.
    //
    // Final rotation each frame:
    //   Projectile.rotation = InitialAngle + spriteDirection * Progress
    //
    // The animation goes through three sequential stages:
    //
    //   1. Prepare  – sword appears and pulls BACK (wind-up).
    //                 Progress decreases from WINDUP*SWINGRANGE → 0.
    //                 Size grows from 0 → 1 (fade-in effect).
    //
    //   2. Execute  – sword sweeps FORWARD through most of the arc.
    //                 Progress grows from 0 → SWINGRANGE*(1-UNWIND) via SmoothStep
    //                 (easing in/out for a natural look).
    //                 When the arc is done a DamageHitbox projectile is spawned.
    //
    //   3. Unwind   – sword finishes the last portion of the arc and disappears.
    //                 Progress grows from SWINGRANGE*(1-UNWIND) → SWINGRANGE.
    //                 Size shrinks from 1 → 0 (fade-out effect).
    //
    // Each stage has its own duration (prepTime / execTime / hideTime), all
    // derived from the item's useAnimation divided by 3, so the full swing takes
    // exactly one use-animation cycle and respects melee attack-speed modifiers.
    // ─────────────────────────────────────────────────────────────────────────────

    // This projectile is drawn and positioned manually; the sprite is centered on
    // the player's hand. Actual collision (Colliding / CutTiles) is calculated as
    // a line from the hand to the sword tip, not from the sprite hitbox.
    public class SaberSwingProgectile : ModProjectile
    {
        // ── Arc angle constants ───────────────────────────────────────────────────
        // SWINGRANGE: total angle swept by a normal vertical swing (≈ 300°).
        private const float SWINGRANGE = 1.67f * (float)Math.PI;
        // HORIZONTAL_SWINGRANGE: total angle swept by a horizontal swing (180°).
        // Narrower than a vertical swing for a quicker, more focused slash.
        private const float HORIZONTAL_SWINGRANGE = (float)Math.PI;
        // FIRSTHALFSWING: fraction of the arc that happens BEFORE the cursor
        // angle, so the swing naturally passes through where the player is aiming.
        private const float FIRSTHALFSWING = 0.45f;
        // SPINRANGE: total angle swept by a spin attack (≈ 630°, more than a full
        // circle). Currently unused because AttackType only has Swing.
        private const float SPINRANGE = 3.5f * (float)Math.PI;

        // ── Animation timing constants ────────────────────────────────────────────
        // WINDUP: how far BACK (as a fraction of SWINGRANGE) the sword goes during
        // the Prepare stage before sweeping forward — creates the "wind-up" look.
        private const float WINDUP = 0.15f;
        // UNWIND: what fraction of SWINGRANGE is left for the Unwind (fade-out)
        // stage. The Execute stage only covers (1 - UNWIND) of the arc so Unwind
        // can finish the remaining portion while the sword disappears.
        private const float UNWIND = 0.4f;
        // SPINTIME: duration multiplier for spin attacks relative to a normal swing.
        private float SPINTIME = 2.5f;

        // Which attack animation to play.
        // The combo cycles: HorizontalSwing, HorizontalSwing, Swing (vertical), repeat.
        private enum AttackType
        {
            // Vertical swing: a wide 300° arc aimed loosely toward the cursor.
            Swing,
            // Horizontal swing: a 180° arc centered on the horizontal axis.
            HorizontalSwing
        }

        // The three sequential stages every attack goes through (see methods below).
        private enum AttackStage
        {
            Prepare, // Wind-up: sword appears and pulls back before striking.
            Execute, // Strike:  sword sweeps forward through the main arc.
            Unwind   // Follow-through: sword finishes the arc and fades out.
        }

        // ── State stored in Terraria's ai / localAI arrays ────────────────────────
        // Terraria automatically syncs Projectile.ai[] over the network.
        // Projectile.localAI[] is local-only (no sync), which is fine for
        // purely visual values like Progress and Size.

        // ai[0] – which attack type is playing (cast to AttackType).
        private AttackType CurrentAttack
        {
            get => (AttackType)Projectile.ai[0];
            set => Projectile.ai[0] = (float)value;
        }

        // localAI[0] – current stage of the animation (cast to AttackStage).
        // Setting this also resets Timer to 0 so each stage starts fresh.
        private AttackStage CurrentStage
        {
            get => (AttackStage)Projectile.localAI[0];
            set
            {
                Projectile.localAI[0] = (float)value;
                Timer = 0; // reset the timer when the projectile switches states
            }
        }

        // Returns the swing arc angle for the current attack type.
        // Horizontal swings use a shorter arc (180°) vs vertical swings (300°).
        private float CurrentSwingRange => CurrentAttack == AttackType.HorizontalSwing ? HORIZONTAL_SWINGRANGE : SWINGRANGE;

        // Returns true for any swing-like attack (horizontal or vertical), as
        // opposed to the spin attack which uses different timing and arc logic.
        private bool IsSwingAttack => CurrentAttack == AttackType.Swing || CurrentAttack == AttackType.HorizontalSwing;

        // ai[1] – the angle at which the swing STARTS (radians, world space).
        //         Set once in OnSpawn based on the cursor position.
        private ref float InitialAngle => ref Projectile.ai[1];
        // ai[2] – frames elapsed within the current stage. Incremented at the
        //         end of AI() and reset to 0 whenever the stage changes.
        private ref float Timer => ref Projectile.ai[2];
        // localAI[1] – how far (radians) the sword has rotated from InitialAngle.
        //              Drives the visual sweep of the swing each frame.
        private ref float Progress => ref Projectile.localAI[1];
        // localAI[2] – current visual scale of the sword (0 = invisible, 1 = full).
        //              Used for the fade-in during Prepare and fade-out during Unwind.
        private ref float Size => ref Projectile.localAI[2];

        // ── Per-stage durations (in frames) ──────────────────────────────────────
        // Each stage lasts exactly one third of the item's use animation, scaled by
        // the player's attack speed. This means faster attack speed = faster swing.
        private float BasePhaseTime => Owner.HeldItem.useAnimation / 3f / Owner.GetTotalAttackSpeed(Projectile.DamageType);
        private float prepTime => BasePhaseTime; // Duration of the Prepare (wind-up) stage.
        private float execTime => BasePhaseTime; // Duration of the Execute (strike) stage.
        private float hideTime => BasePhaseTime; // Duration of the Unwind (fade-out) stage.

        // The texture is read from the item that owns this projectile (set via SetID),
        // so the swing sprite always matches the weapon being swung.
        public override string Texture => GetTexturePath();
        private Player Owner => Main.player[Projectile.owner];

        // Returns the texture path for the item currently held. Uses vanilla path
        // format for vanilla items and the mod's path for modded items.
        private string GetTexturePath()
        {
            if (itemBase == null) return "Terraria/Images/Item_0";
            Item item = ContentSamples.ItemsByType[itemBase.type];
            string Texture = item.type < ItemID.Count ? "Terraria/Images/Item_" + item.type : ItemLoader.GetItem(item.type).Texture;
            return Texture;
        }
        // The weapon item whose stats (damage, scale, texture) this projectile uses.
        // Must be set by the item BEFORE spawning the projectile via SetID().
        public static Item itemBase;
        // Tracks how many consecutive saber swings have been performed.
        // The combo pattern repeats every 3 attacks: Horizontal, Horizontal, Vertical.
        public static int comboCounter = 0;
        // Called by the owning item before UseItem() spawns the projectile.
        public static void SetID(Item id)
        {
            itemBase = id;
        }
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.HeldProjDoesNotUsePlayerGfxOffY[Type] = true;
        }

        public override void SetDefaults()
        {
            Projectile.width = 46; // Hitbox width of projectile
            Projectile.height = 48; // Hitbox height of projectile
            Projectile.friendly = true; // Projectile hits enemies
            Projectile.timeLeft = 10000; // Time it takes for projectile to expire
            Projectile.penetrate = -1; // Projectile pierces infinitely
            Projectile.tileCollide = false; // Projectile does not collide with tiles
            Projectile.usesLocalNPCImmunity = true; // Uses local immunity frames
            Projectile.localNPCHitCooldown = -1; // We set this to -1 to make sure the projectile doesn't hit twice
            Projectile.ownerHitCheck = true; // Make sure the owner of the projectile has line of sight to the target (aka can't hit things through tile).
            Projectile.DamageType = DamageClass.Melee; // Projectile is a melee projectile
            Projectile.scale = itemBase == null? 1.1f: itemBase.scale;
        }

        public override void OnSpawn(IEntitySource source)
        {
            // Determine which side of the player the cursor is on; this controls
            // spriteDirection (+1 = right, -1 = left) and flips the sprite accordingly.
            Projectile.spriteDirection = Main.MouseWorld.X > Owner.MountedCenter.X ? 1 : -1;

            if (CurrentAttack == AttackType.HorizontalSwing)
            {
                // Horizontal swing: force the arc to sweep around the horizontal axis
                // regardless of cursor vertical position. The target angle is fixed at
                // 0° (right) or π (left) so the slash always feels horizontal.
                float targetAngle = Projectile.spriteDirection == 1 ? 0f : (float)Math.PI;
                InitialAngle = targetAngle - FIRSTHALFSWING * HORIZONTAL_SWINGRANGE * Projectile.spriteDirection;
                InitialAngle += 100;
            }
            else
            {
                // Vertical swing: aim toward the cursor with angle constraints.
                float targetAngle = (Main.MouseWorld - Owner.MountedCenter).ToRotation();

                if (Projectile.spriteDirection == 1)
                {
                    // Clamp the aim angle so the sword doesn't point straight up or behind
                    // the player when swinging to the right (-60° to +30° from horizontal).
                    targetAngle = MathHelper.Clamp(targetAngle, (float)-Math.PI * 1 / 3, (float)Math.PI * 1 / 6);
                }
                else
                {
                    if (targetAngle < 0)
                    {
                        targetAngle += 2 * (float)Math.PI; // Shift negative angles to [0, 2π] so the clamp below works correctly.
                    }
                    // Clamp aim angle for left-side swings (150° to 240° from positive-X).
                    targetAngle = MathHelper.Clamp(targetAngle, (float)Math.PI * 5 / 6, (float)Math.PI * 4 / 3);
                }

                // InitialAngle is placed BEFORE the target angle by FIRSTHALFSWING of the arc,
                // so the swing naturally passes through the aimed direction part-way through Execute.
                InitialAngle = targetAngle - FIRSTHALFSWING * SWINGRANGE * Projectile.spriteDirection;
            }
        }

        public override void SendExtraAI(BinaryWriter writer)
        {
            // spriteDirection is derived from the cursor position in OnSpawn and is not
            // synced automatically by Terraria. All ai[] slots are already occupied, so
            // we manually write it as a signed byte for other clients in multiplayer.
            writer.Write((sbyte)Projectile.spriteDirection);
        }

        public override void ReceiveExtraAI(BinaryReader reader)
        {
            Projectile.spriteDirection = reader.ReadSByte();
        }

        public override void AI()
        {
            // Keep the player's use animation alive so the game doesn't cancel the swing.
            Owner.itemAnimation = 2;
            Owner.itemTime = 2;

            // Cancel the swing if the player is dead, crowd-controlled, or has items disabled.
            if (!Owner.active || Owner.dead || Owner.noItems || Owner.CCed)
            {
                Projectile.Kill();
                return;
            }

            // Delegate per-frame logic to the current animation stage.
            // Each stage method updates Progress and Size, then advances to the
            // next stage (or kills the projectile) when its timer expires.
            switch (CurrentStage)
            {
                case AttackStage.Prepare:
                    PrepareStrike();
                    break;
                case AttackStage.Execute:
                    ExecuteStrike();
                    break;
                default:
                    UnwindStrike();
                    break;
            }

            // After updating Progress/Size, reposition the sword on the player's hand.
            SetSwordPosition();
            // Advance the frame counter for the current stage.
            Timer++;
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Vector2 origin;
            float rotationOffset;
            SpriteEffects effects;

            if (Projectile.spriteDirection > 0)
            {
                // For a right-facing swing the pivot point is the bottom-left corner of
                // the sprite, and we rotate it 45° so the blade points diagonally.
                origin = new Vector2(0, Projectile.height);
                rotationOffset = MathHelper.ToRadians(45f);
                effects = SpriteEffects.None;
            }
            else
            {
                // For a left-facing swing the pivot moves to the bottom-right corner
                // and the sprite is flipped horizontally; offset becomes 135°.
                origin = new Vector2(Projectile.width, Projectile.height);
                rotationOffset = MathHelper.ToRadians(135f);
                effects = SpriteEffects.FlipHorizontally;
            }

            Texture2D texture = ModContent.Request<Texture2D>(Texture).Value;

            // Draw centered on the projectile's world position (which sits on the player's hand).
            // rotationOffset aligns the sprite so the blade tip points in the rotation direction.
            Main.spriteBatch.Draw(texture, Projectile.Center - Main.screenPosition, default, lightColor * Projectile.Opacity, Projectile.rotation + rotationOffset, origin, Projectile.scale, effects, 0);
            // Returning false suppresses Terraria's default projectile drawing.
            return false;
        }

        // Instead of using the rectangular sprite hitbox, collision is tested as a
        // line segment from the player's center to the sword tip. This makes hits
        // feel accurate regardless of the current rotation angle.
        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            Vector2 start = Owner.MountedCenter;
            Vector2 end = start + Projectile.rotation.ToRotationVector2() * ((Projectile.Size.Length()) * Projectile.scale);
            float collisionPoint = 0f;
            return Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), start, end, 15f * Projectile.scale, ref collisionPoint);
        }

        // Tile cutting uses the same line from hand to tip so grass/vines are cut
        // along the actual blade path rather than the rectangular hitbox.
        public override void CutTiles()
        {
            Vector2 start = Owner.MountedCenter;
            Vector2 end = start + Projectile.rotation.ToRotationVector2() * (Projectile.Size.Length() * Projectile.scale);
            Utils.PlotTileLine(start, end, 15 * Projectile.scale, DelegateMethods.CutTiles);
        }

        // Damage is intentionally allowed during Prepare as well (returns true) so the
        // wind-up can hit enemies the player pulls back through. During Execute and
        // Unwind the base implementation returns null (use default damage rules).
        public override bool? CanDamage()
        {
            if (CurrentStage == AttackStage.Prepare)
                return true;
            return base.CanDamage();
        }

        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            // Make knockback go away from player
            modifiers.HitDirectionOverride = target.position.X > Owner.MountedCenter.X ? 1 : -1;
        }

        // Repositions the projectile on the player's front hand every frame and
        // updates the arm's composite rotation to match the current sword angle.
        public void SetSwordPosition()
        {
            // Rotation = base angle + how far the sword has swept so far.
            // spriteDirection flips the sweep direction for left-facing swings.
            Projectile.rotation = InitialAngle + Projectile.spriteDirection * Progress;

            // Rotate the player's front arm to follow the sword (−90° because the
            // arm's zero rotation points downward, not to the right).
            Owner.SetCompositeArmFront(true, Player.CompositeArmStretchAmount.Full, Projectile.rotation - MathHelper.ToRadians(90f));
            // Get the world position of the hand after applying the arm rotation.
            Vector2 armPosition = Owner.GetFrontHandPosition(Player.CompositeArmStretchAmount.Full, Projectile.rotation - (float)Math.PI / 2);

            armPosition.Y += Owner.gfxOffY; // Account for vertical mount/grapple offsets.
            Projectile.Center = armPosition; // Snap the projectile to the hand position.
            // Scale includes: Size (0→1 fade), a fixed 1.2× boost, and the item's
            // melee-size modifier so accessories that increase sword size work correctly.
            Projectile.scale = Size * 1.2f * Owner.GetAdjustedItemScale(Owner.HeldItem);

            Owner.heldProj = Projectile.whoAmI; // Tell the game this is the held projectile.
        }

        // ── Stage 1: Prepare ──────────────────────────────────────────────────────
        // The sword appears (Size 0→1) and holds slightly BEHIND InitialAngle
        // (Progress counts DOWN from WINDUP*SWINGRANGE to 0). This gives the
        // visual impression of the player pulling the sword back before striking.
        private void PrepareStrike()
        {
            // Progress shrinks linearly: starts at WINDUP offset, reaches 0 at end of prep.
            Progress = WINDUP * CurrentSwingRange * (1f - Timer / prepTime);
            // SmoothStep eases the fade-in so the sword doesn't just pop into existence.
            Size = MathHelper.SmoothStep(0, 1, Timer / prepTime);

            if (Timer >= prepTime)
            {
                SoundEngine.PlaySound(SoundID.Item1); // Play swing sound now (too early on spawn).
                CurrentStage = AttackStage.Execute;   // Advance to the forward sweep.
            }
        }

        // ── Stage 2: Execute ──────────────────────────────────────────────────────
        // The sword sweeps forward. Progress grows from 0 to SWINGRANGE*(1-UNWIND)
        // via SmoothStep so the motion accelerates at the start and decelerates
        // slightly near the end (feels snappier than a linear sweep).
        // When execTime expires the DamageHitbox is spawned and we move to Unwind.
        private void ExecuteStrike()
        {
            if (IsSwingAttack)
            {
                // Advance Progress through (1-UNWIND) of the full arc.
                // The remaining UNWIND fraction is reserved for the Unwind stage.
                Progress = MathHelper.SmoothStep(0, CurrentSwingRange, (1f - UNWIND) * Timer / execTime);

                if (Timer >= execTime)
                {
                    Player player = Main.player[Projectile.owner];
                    Rectangle rec = new Rectangle((int)Projectile.position.X, (int)Projectile.position.Y, (int)Projectile.Size.X, (int)Projectile.Size.Y);
                    // Trigger any on-hit visual/sound effects defined on the item (e.g. particles).
                    itemBase.GetGlobalItem<SaberGlobalItem>().MeleeEffects(itemBase, player, rec);

                    Vector2 pos = player.position;
                    float widthMultiplier = 2f;
                    float heightMultiplier = ReaperGlobalItem.currentScale + 0.5f;
                    float reaperScale = (float)Math.Pow(ReaperGlobalItem.currentScale - 0.5f, 2);
                    // Only spawn the hitbox if one doesn't already exist for this player,
                    // preventing double-hits if the swing fires faster than 60 fps.
                    if (Main.player[Projectile.owner].ownedProjectileCounts[ModContent.ProjectileType<DamageHitbox>()] < 1)
                    {
                        Vector2 size = new(90 * widthMultiplier, 90 * heightMultiplier);
                        var a = Projectile.NewProjectile(Projectile.GetSource_FromAI(), pos, Vector2.Zero, ModContent.ProjectileType<DamageHitbox>(), itemBase.damage, itemBase.knockBack, Projectile.owner);
                        Main.projectile[a].Size = size * itemBase.scale * ReaperGlobalItem.currentScale;
                        Main.projectile[a].friendly = true;
                        Main.projectile[a].hostile = false;
                        Main.projectile[a].timeLeft = 60;
                        Main.projectile[a].penetrate = 5;
                        Main.projectile[a].Center = player.Center;
                        DamageHitbox.SetItem(itemBase);
                        CurrentStage = AttackStage.Unwind;
                    }
                }
            }
            else
            {
                // Spin attack (unused currently): sweeps through SPINRANGE over SPINTIME
                // times the normal exec duration. Half-way through the spin, local NPC
                // immunity is reset so the sword can hit each enemy a second time.
                Progress = MathHelper.SmoothStep(0, SPINRANGE, (1f - UNWIND / 2) * Timer / (execTime * SPINTIME));

                if (Timer == (int)(execTime * SPINTIME * 3 / 4))
                {
                    SoundEngine.PlaySound(SoundID.Item1); // Play sword sound again for the second half of the spin.
                    Projectile.ResetLocalNPCHitImmunity(); // Allow hitting enemies a second time in the back half.
                }

                if (Timer >= execTime * SPINTIME)
                {
                    CurrentStage = AttackStage.Unwind;
                }
            }
        }

        // ── Stage 3: Unwind ───────────────────────────────────────────────────────
        // The sword finishes the last UNWIND fraction of the arc while fading out
        // (Size 1→0). Progress picks up exactly where ExecuteStrike left off so
        // the motion is continuous. When hideTime expires the projectile is killed.
        private void UnwindStrike()
        {
            if (IsSwingAttack)
            {
                // Progress resumes from (1-UNWIND)*CurrentSwingRange and reaches CurrentSwingRange.
                Progress = MathHelper.SmoothStep(0, CurrentSwingRange, (1f - UNWIND) + UNWIND * Timer / hideTime);
                // Size fades from 1 to 0 — the sword shrinks and disappears smoothly.
                Size = 1f - MathHelper.SmoothStep(0, 1, Timer / hideTime);

                if (Timer >= hideTime)
                {
                    Projectile.Kill();
                }
            }
            else
            {
                // Spin variant: same idea but the fade-out covers a longer arc.
                Progress = MathHelper.SmoothStep(0, SPINRANGE, (1f - UNWIND / 2) + UNWIND / 2 * Timer / (hideTime * SPINTIME / 2));
                Size = 1f - MathHelper.SmoothStep(0, 1, Timer / (hideTime * SPINTIME / 2));

                if (Timer >= hideTime * SPINTIME / 2)
                {
                    Projectile.Kill();
                }
            }
        }
    }
}
*/