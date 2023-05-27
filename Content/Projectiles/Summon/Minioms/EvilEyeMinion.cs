using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RemnantOfTheAncientsMod.Content.Buffs.Buffs.Minions;
using RemnantOfTheAncientsMod.Content.Buffs.Debuff;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Humanizer.In;
using static Terraria.ModLoader.ModContent;


namespace RemnantOfTheAncientsMod.Content.Projectiles.Summon.Minioms
{
    public class EvilEyeMinion : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Illuminated SunFlower Minion");
            Main.projFrames[Projectile.type] = 3;
            ProjectileID.Sets.MinionTargettingFeature[Projectile.type] = true;
            Main.projPet[Projectile.type] = true;
            ProjectileID.Sets.SentryShot[Projectile.type] = true;
            ProjectileID.Sets.MinionSacrificable[Projectile.type] = true;
        }
        public sealed override void SetDefaults()
        {
            Projectile.width = 30;
            Projectile.height = 60;
            Projectile.minion = true;
            Projectile.minionSlots = 1;
            Projectile.penetrate = -1;
            Projectile.alpha = 100;
            Projectile.manualDirectionChange = true;
            Projectile.netImportant = true;
            Projectile.timeLeft = 2000;
            Projectile.tileCollide = false;
        }
        public override bool? CanCutTiles()
        {
            return false;
        }
        public override bool MinionContactDamage()
        {
            return false;
        }
        public int RangeMax = 8;
        public int AttackTimmer = (int)Utils1.FormatTime(0f, 0f, 0f, 0.5f);
        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            CheckActive(player);
            GeneralBehavior(player, out Vector2 vectorToIdlePosition, out float distanceToIdlePosition);
            SearchForTargets(player, out bool foundTarget, out float distanceFromTarget, out Vector2 targetCenter);
            Movement(foundTarget, distanceFromTarget, targetCenter, distanceToIdlePosition, vectorToIdlePosition);



            if (AttackTimmer == 0)
            {
                AttackTimmer = (int)Utils1.FormatTime(0f, 0f, 0f, 0.5f);
            }
            else
            {
                AttackTimmer--;
            }
            if (new RemnantOfTheAncientsMod().ParticleMeter(3) != 0)
            {
                AnimateTexture();
            }
            AreaEffect(player);


        }
        public bool IsAttack = false;
        private void AreaEffect(Player player)
        {
            for (int i = 0; i < Main.maxNPCs;i++)
            {
                if (Projectile.Distance(Main.npc[i].Center) <= RangeMax * 16)
                {
                    Main.npc[i].AddBuff(BuffType<Hell_Fire>(), Utils1.FormatTime(0, 0, 0, 2));
                    if (AttackTimmer == 1)
                    {            
                        Main.npc[i].StrikeNPC(Projectile.damage, 0, 1, Main.rand.NextBool(6), false, true);
                        if (Main.npc[i].type != 679)
                        {
                            IsAttack = true;
                        }
                        if (IsAttack)
                        {
                            SpawnParticles();
                        }
                    }
                }
                //IsAttack = false;
            }
        }
        private void GeneralBehavior(Player owner, out Vector2 vectorToIdlePosition, out float distanceToIdlePosition)
        {
            Vector2 idlePosition = owner.Center;
            idlePosition.Y -= 48f; // Go up 48 coordinates (three tiles from the center of the player)

            // If your minion doesn't aimlessly move around when it's idle, you need to "put" it into the line of other summoned minions
            // The index is projectile.minionPos
            float minionPositionOffsetX = (10 + Projectile.minionPos * 40) * -owner.direction;
            idlePosition.X += minionPositionOffsetX; // Go behind the player

            // All of this code below this line is adapted from Spazmamini code (ID 388, aiStyle 66)

            // Teleport to player if distance is too big
            TeleportToPlayer(out vectorToIdlePosition,out distanceToIdlePosition, owner, idlePosition);

            // If your minion is flying, you want to do this independently of any conditions
            float overlapVelocity = 0.04f;

            // Fix overlap with other minions
            for (int i = 0; i < Main.maxProjectiles; i++)
            {
                Projectile other = Main.projectile[i];

                if (i != Projectile.whoAmI && other.active && other.owner == Projectile.owner && Math.Abs(Projectile.position.X - other.position.X) + Math.Abs(Projectile.position.Y - other.position.Y) < Projectile.width)
                {
                    if (Projectile.position.X < other.position.X)
                    {
                        Projectile.velocity.X -= overlapVelocity;
                    }
                    else
                    {
                        Projectile.velocity.X += overlapVelocity;
                    }

                    if (Projectile.position.Y < other.position.Y)
                    {
                        Projectile.velocity.Y -= overlapVelocity;
                    }
                    else
                    {
                        Projectile.velocity.Y += overlapVelocity;
                    }
                }
            }
        }
        public void TeleportToPlayer(out Vector2 vectorToIdlePosition, out float distanceToIdlePosition, Player player, Vector2 idlePosition)
        {
            vectorToIdlePosition = idlePosition - Projectile.Center;
            distanceToIdlePosition = vectorToIdlePosition.Length();

            if (Main.myPlayer == player.whoAmI && distanceToIdlePosition > 2000f)
            {
                Projectile.position = idlePosition;
                Projectile.velocity *= 0.1f;
                Projectile.netUpdate = true;
            }
        }

        public int AnimationCounter = 0; 

        private void SearchForTargets(Player owner, out bool foundTarget, out float distanceFromTarget, out Vector2 targetCenter)
        {
            // Starting search distance
            distanceFromTarget = 500f;
            targetCenter = Projectile.position;
            foundTarget = false;

            // This code is required if your minion weapon has the targeting feature
            if (owner.HasMinionAttackTargetNPC)
            {
                NPC npc = Main.npc[owner.MinionAttackTargetNPC];
                float between = Vector2.Distance(npc.Center, Projectile.Center);

                // Reasonable distance away so it doesn't target across multiple screens
                if (between < 2000f)
                {
                    distanceFromTarget = between;
                    targetCenter = npc.Center;
                    foundTarget = true;
                }
            }
            else
            {
                if (IsAttack == true && AnimationCounter >= Utils1.FormatTime(0, 0, 0, 1))
                {
                    IsAttack = false;
                    AnimationCounter = 0;
                }
                else
                {
                    AnimationCounter++;
                }
            }

            if (!foundTarget)
            {
                // This code is required either way, used for finding a target
                for (int i = 0; i < Main.maxNPCs; i++)
                {
                    NPC npc = Main.npc[i];

                    if (npc.CanBeChasedBy())
                    {
                        float between = Vector2.Distance(npc.Center, Projectile.Center);
                        bool closest = Vector2.Distance(Projectile.Center, targetCenter) > between;
                        bool inRange = between < distanceFromTarget;
                        bool lineOfSight = Collision.CanHitLine(Projectile.position, Projectile.width, Projectile.height, npc.position, npc.width, npc.height);
                        // Additional check for this specific minion behavior, otherwise it will stop attacking once it dashed through an enemy while flying though tiles afterwards
                        // The number depends on various parameters seen in the movement code below. Test different ones out until it works alright
                        bool closeThroughWall = between < 100f;

                        if (((closest && inRange) || !foundTarget) && (lineOfSight || closeThroughWall))
                        {
                            distanceFromTarget = between;
                            targetCenter = npc.Center;
                            foundTarget = true;
                        }
                    }
                }
            }
            

            // friendly needs to be set to true so the minion can deal contact damage
            // friendly needs to be set to false so it doesn't damage things like target dummies while idling
            // Both things depend on if it has a target or not, so it's just one assignment here
            // You don't need this assignment if your minion is shooting things instead of dealing contact damage
            Projectile.friendly = foundTarget;
        }

        private void Movement(bool foundTarget, float distanceFromTarget, Vector2 targetCenter, float distanceToIdlePosition, Vector2 vectorToIdlePosition)
        {
            // Default movement parameters (here for attacking)
            float speed = 15f;//8
            float inertia = 20f;

            if (foundTarget)
            {
                // Minion has a target: attack (here, fly towards the enemy)
                if (distanceFromTarget > 40f)
                {
                    // The immediate range around the target (so it doesn't latch onto it when close)
                    Vector2 direction = targetCenter - Projectile.Center;
                    direction.Normalize();
                    direction *= speed;

                    Projectile.velocity = (Projectile.velocity * (inertia - 1) + direction) / inertia;
                }
            }
            else
            {
                // Minion doesn't have a target: return to player and idle
                if (distanceToIdlePosition > 600f)
                {
                    // Speed up the minion if it's away from the player
                    speed = 12f;
                    inertia = 60f;
                }
                else
                {
                    // Slow down the minion if closer to the player
                    speed = 4f;
                    inertia = 80f;
                }

                if (distanceToIdlePosition > 20f)
                {
                    // The immediate range around the player (when it passively floats about)

                    // This is a simple movement formula using the two parameters and its desired direction to create a "homing" movement
                    vectorToIdlePosition.Normalize();
                    vectorToIdlePosition *= speed;
                    Projectile.velocity = (Projectile.velocity * (inertia - 1) + vectorToIdlePosition) / inertia;
                }
                else if (Projectile.velocity == Vector2.Zero)
                {
                    // If there is a case where it's not moving at all, give it a little "poke"
                    Projectile.velocity.X = -0.15f;
                    Projectile.velocity.Y = -0.05f;
                }
            }
        }



        public void AnimateTexture()
        {
            if (++Projectile.frameCounter >= 9)
            {
                Projectile.frameCounter = 0;
                if (++Projectile.frame >= Main.projFrames[Projectile.type])
                    Projectile.frame = 0;
            }
        }
        public void SpawnParticles()
        {
            Vector2 offset = new Vector2(Projectile.width / 2, Projectile.height / 2) + new Vector2(24, 24);//24
            for (int i = 0; i < new RemnantOfTheAncientsMod().ParticleMeter(90); i++)
            {
                Vector2 dustPos = Projectile.position - new Vector2(24, 24) + offset + new Vector2(RangeMax * 16, 0).RotatedBy(MathHelper.ToRadians(18 * i));//60
                var d = Dust.NewDustPerfect(dustPos, DustID.InfernoFork, Vector2.Zero);
                d.noLight = false;
                d.noGravity = true;
            }
        }
        public void CheckActive(Player player)
        {
            if (player.dead || !player.active)
            {
                player.ClearBuff(BuffType<EvilEyeMinionBuff>());
            }
            if (player.HasBuff(BuffType<EvilEyeMinionBuff>()))
            {
                Projectile.timeLeft = 2;
            }
        }
        public float fade = 2.6f;
        public override bool PreDraw(ref Color lightColor)
        {
            var texture = Request<Texture2D>("RemnantOfTheAncientsMod/Content/Projectiles/Summon/Minioms/EvilEyeArea");
            Vector2 origin = new Vector2(texture.Width() * 0.5f, texture.Height() * 0.5f);//0.5
            if (Main.myPlayer == Projectile.owner)
            {
                Color color = new Color(Color.Red.R, Color.Orange.G, Color.Red.B, 20) * fade;
                if (IsAttack)
                {
                    Main.spriteBatch.Draw((Texture2D)texture, Projectile.Center - Main.screenPosition, null, color, 0f, origin, 1.0f, SpriteEffects.None, 0f);
                }
            }
            return true;
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.immune[Projectile.owner] = 2;
        }
    }
}