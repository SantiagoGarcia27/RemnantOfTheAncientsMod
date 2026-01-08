using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Mono.Cecil;
using RemnantOfTheAncientsMod.Common.UtilsTweaks;
using RemnantOfTheAncientsMod.Content.Items.Weapons.Ranger.Bows;
using System;
using System.Linq;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using static System.Net.Mime.MediaTypeNames;

class PlayerClone : ModProjectile
{
    public override void SetStaticDefaults()
    {
        Main.projFrames[Projectile.type] = 1;
        Main.projPet[Projectile.type] = true;
        ProjectileID.Sets.MinionSacrificable[Projectile.type] = false;
        ProjectileID.Sets.MinionTargettingFeature[Projectile.type] = true;
    }
    public override string Texture => RemnantOfTheAncientsMod.RemnantOfTheAncientsMod.PlaceHolderPath;
    public override void SetDefaults()
    {
        Projectile.width = 22;
        Projectile.height = 42;
        Projectile.netImportant = true;
        Projectile.friendly = true;
        Projectile.minionSlots = 0f;
        Projectile.alpha = 200;
        Projectile.aiStyle = -1;
        Projectile.timeLeft = 3;
        Projectile.penetrate = -1;
        Projectile.minion = true;
        AIType = -1;
        Projectile.tileCollide = false;
    }

    bool isOnAir = false;
    int wingFrame = 0;
    int wingCounter = 0;
    Item currentWings = new(ItemID.AngelWings);
    float orbitPos = 0;
    NPC target = null;
    public override void AI()
    {
        Player owner = Main.player[Projectile.owner];
        if (orbitPos == 0)
            orbitPos = Projectile.localAI[1];
        CheckActive(owner);
        int targetIndex = Projectile.FindTargetWithLineOfSight();
        if (target != null) {
            if (target.life <= 0 || !target.active) target = null;
        }
        else {
            if (targetIndex != -1) target = Main.npc[targetIndex];
        }

        SelectWeapon(out Item heldItem, out int ProjectileShoot, out float speedBonus);

        CleanupUnusedProjectiles(heldItem);
        if (target != null) AttackAi(heldItem, ProjectileShoot, speedBonus);
        MovementAi(orbitDistanceToEnemy: heldItem.CountsAsClass(DamageClass.Melee) ? 16f : 148f);

        currentWings = owner.equippedWings ?? new Item(ItemID.AngelWings);
        Projectile.direction = owner.direction;
        isOnAir = DistanceUtils.TouchFlour(Projectile);
        if (!isOnAir)
        {
            if (wingCounter++ > 4)
            {
                if (wingFrame++ > 2) wingFrame = 0;
                wingCounter = 0;
            }
        }
        
        base.AI();
    }
    private void CheckActive(Player owner)
    {
        if (!owner.dead && owner.active && owner.HasBuff(BuffID.ShadowDodge))
        {
            Projectile.timeLeft = 2;
        }
    }
    private void CleanupUnusedProjectiles(Item heldItem)
    {
        if (playerDummy != null)
        {

            // Clean up active projectiles that shouldn't exist
            for (int i = 0; i < Main.projectile.Length; i++)
            {
                Projectile proj = Main.projectile[i];

                // Skip if not our projectile
                if (!proj.active || proj.owner == playerDummy.whoAmI)
                {

                    bool shouldKill = false;

                    // Handle specific projectile types
                    if (proj.type == ProjectileID.Retanimini || proj.type == ProjectileID.Spazmamini)
                    {
                        // Kill retina laser if we're not using OpticStaff
                        if (heldItem.type != ItemID.OpticStaff)
                        {
                            shouldKill = true;
                            
                        }
                    }

                    if (shouldKill)
                    {
                        proj.Kill();
                        projeAmmount = 0;
                    }
                }
            }
        }
    }
    void SelectWeapon(out Item heldItem, out int ProjectileShoot, out float speedBonus)
    {
        Player owner = Main.player[Projectile.owner];
        Item currentOwnerWeapon = owner.HeldItem;
        if (currentOwnerWeapon == null) currentOwnerWeapon = new Item(ItemID.Excalibur);
        DamageClass currentOwnerWeaponClass = currentOwnerWeapon.DamageType;

        speedBonus = 1f;

        if (currentOwnerWeaponClass == DamageClass.Summon)
        {
            heldItem = new Item(ItemID.OpticStaff); //Durendal
            ProjectileShoot = heldItem.shoot;
            if(playerDummy != null) playerDummy.AddBuff(BuffID.TwinEyesMinion, 1);
            speedBonus =0.1f;
        }
        else if (currentOwnerWeaponClass == DamageClass.Magic)
        {
            heldItem = new Item(ItemID.MagicalHarp);
            ProjectileShoot = heldItem.shoot;
            speedBonus = 0.1f;
        }
        else if (currentOwnerWeaponClass == DamageClass.Ranged)
        {
            heldItem = new Item(ModContent.ItemType<Failnaught>());
            ProjectileShoot = ProjectileID.HolyArrow;
        }
        else if (currentOwnerWeaponClass == DamageClass.Throwing)
        {
            heldItem = new Item(ItemID.LightDisc);
            speedBonus = 0.5f;
            ProjectileShoot = heldItem.shoot;
        }
        else if (currentOwnerWeaponClass == DamageClass.Melee)
        {
            heldItem = new Item(ItemID.Excalibur);
            ProjectileShoot = ProjectileShoot = heldItem.shoot; ;
            speedBonus = 1f;
        }
        else
        {
            heldItem = new Item(ItemID.Excalibur);
            ProjectileShoot = ProjectileShoot = heldItem.shoot; ;
            speedBonus = 1f;
        }
    }
    void MovementAi(float orbitDistanceToOwner = 2f, float orbitDistanceToEnemy = 16f)
    {
        Vector2 targetPosition;
        float maxSpeed = 8f;
        float acceleration = 0.2f;
        float deceleration = 0.95f;
        float catchUpMultiplier = 1.5f;
        float teleportDistance = 800f; // Teleport if too far away
        Player owner = Main.player[Projectile.owner];
        if (target == null)
        {    
            // Calculate desired position
            Vector2 desiredPosition = owner.Center - new Vector2(orbitPos * orbitDistanceToOwner * 16, 2 * 16);
            targetPosition = desiredPosition;

            currentWings = owner.equippedWings ?? new Item(ItemID.AngelWings);

            float distanceToTarget = Projectile.Center.Distance(targetPosition);

            // If too far away, teleport to catch up
            if (distanceToTarget > teleportDistance)
            {
                Projectile.Center = targetPosition;
                Projectile.velocity = Vector2.Zero;
                return;
            }

            // Smooth movement logic
            float currentSpeed = Math.Max(2f, (1 + owner.moveSpeed) * owner.maxRunSpeed);
            float dynamicMaxSpeed = currentSpeed * (distanceToTarget > 160f ? catchUpMultiplier : 1f);

            if (distanceToTarget > 32f) // Start moving when far enough
            {
                Vector2 directionToTarget = Projectile.Center.DirectionTo(targetPosition);

                // Accelerate towards target
                Vector2 desiredVelocity = directionToTarget * Math.Min(dynamicMaxSpeed, distanceToTarget * 0.1f);

                // Smooth acceleration
                Projectile.velocity = Vector2.Lerp(Projectile.velocity, desiredVelocity, acceleration);

                // Limit maximum speed
                if (Projectile.velocity.Length() > dynamicMaxSpeed)
                {
                    Projectile.velocity = Vector2.Normalize(Projectile.velocity) * dynamicMaxSpeed;
                }
            }
            else if (distanceToTarget < orbitDistanceToEnemy) // Close enough, slow down
            {
                Projectile.velocity *= deceleration;
                if (Projectile.velocity.Length() < 0.5f)
                {
                    Projectile.velocity = Vector2.Zero;
                }
            }

            // Add slight following behavior based on owner's velocity
            if (owner.velocity.Length() > 1f)
            {
                Vector2 predictedPosition = targetPosition + owner.velocity * 0.3f;
                Vector2 directionToPredicted = Projectile.Center.DirectionTo(predictedPosition);
                Projectile.velocity += directionToPredicted * 0.5f;
            }
        }
        else
        {
            // Combat behavior - more responsive movement
            Vector2 combatPosition = target.Center + new Vector2(orbitPos * 64f, -32f);
            float distanceToCombatPos = Projectile.Center.Distance(combatPosition);

            if (distanceToCombatPos > 16f)
            {
                Vector2 directionToCombat = Projectile.Center.DirectionTo(combatPosition);
                Projectile.velocity = Vector2.Lerp(Projectile.velocity, directionToCombat * maxSpeed * 1.2f, 0.15f);
            }
            else
            {
                Projectile.velocity *= 0.9f;
            }
        }

        isOnAir = !DistanceUtils.TouchFlour(Projectile);
        if (isOnAir)
        {
            if (wingCounter++ > 4)
            {
                if (wingFrame++ > 2) wingFrame = 0;
                wingCounter = 0;
            }
        }
        else
        {
            wingFrame = 0;
        }

        base.AI();
    }
    int attackTimmer = 0;
    int projeAmmount = 0;
    public void AttackAi(Item heldItem, int ProjectileShoot, float speedBonus)
    {
        if (attackTimmer++ > heldItem.useTime * 2)
        {
            if (ProjectileShoot == 0) ProjectileShoot = heldItem.shoot;

            if (heldItem.type == ItemID.OpticStaff)
            {
                if (projeAmmount == 0)
                {
                    var p = Projectile.NewProjectile(playerDummy.GetSource_ItemUse(heldItem), Projectile.Center, (target.Center - Projectile.Center) * speedBonus, ProjectileShoot, heldItem.damage / 2, heldItem.knockBack, playerDummy.whoAmI);
                    Main.projectile[p].minionSlots = 0;
                    Main.projectile[p].usesLocalNPCImmunity = true;
                    var a = Projectile.NewProjectile(playerDummy.GetSource_ItemUse(heldItem), Projectile.Center, (target.Center - Projectile.Center) * speedBonus, ProjectileID.Spazmamini, heldItem.damage / 2, heldItem.knockBack, playerDummy.whoAmI);
                    Main.projectile[a].minionSlots = 0;
                    Main.projectile[a].usesLocalNPCImmunity = true;
                    projeAmmount++;
                }
            }
            else if (heldItem.type == ItemID.MagicalHarp)
            {
                int selection = 0;
            
                switch (Main.rand.Next(3))
                {
                    case 0:
                        selection = 76;
                        break;
                    case 1:
                        selection = 77;
                        break;
                    case 2:
                        selection = 78;
                        break;
                    default:
                        selection = 76;
                        break;
                }
                var p = Projectile.NewProjectile(playerDummy.GetSource_ItemUse(heldItem), Projectile.Center, (target.Center - Projectile.Center) * speedBonus, selection, heldItem.damage, heldItem.knockBack, playerDummy.whoAmI);
                
                Main.projectile[p].usesLocalNPCImmunity = true;
            }
            else if (heldItem.type == ItemID.Excalibur)
            {
                float adjustedItemScale = playerDummy.GetAdjustedItemScale(heldItem);
                int p = Projectile.NewProjectile(playerDummy.GetSource_ItemUse(heldItem), Projectile.Center, new Vector2(Projectile.direction, 0f), ProjectileID.Excalibur, heldItem.damage, heldItem.knockBack, playerDummy.whoAmI, playerDummy.direction * playerDummy.gravDir, 17, adjustedItemScale);
                Main.projectile[p].localAI[2] = Projectile.whoAmI;
                Main.projectile[p].usesLocalNPCImmunity = true;
                NetMessage.SendData(MessageID.PlayerControls, -1, -1, null, playerDummy.whoAmI);
            }
            else 
            {
               int p = Projectile.NewProjectile(playerDummy.GetSource_ItemUse(heldItem), Projectile.Center, (target.Center - Projectile.Center) * speedBonus, ProjectileShoot, heldItem.damage, heldItem.knockBack, playerDummy.whoAmI);
               Main.projectile[p].usesLocalNPCImmunity = true;
            }
            attackTimmer = 0;
        }
    }
    Player playerDummy;
    public override void OnSpawn(IEntitySource source)
    {
        orbitPos = Projectile.localAI[1];
        base.OnSpawn(source);
    }
    public override bool PreDraw(ref Color lightColor)
    {
        Main.spriteBatch.End();
        Main.spriteBatch.Begin((SpriteSortMode)0, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, null, Main.Transform);
        try
        {
            int owner = Projectile.owner;
            Player other = Main.player[owner];
            if (Main.playerVisualClone[owner] == null)
            {
                Main.playerVisualClone[owner] = new Player();
            }
            Main.playerVisualClone[owner] = new Player();
            playerDummy = Main.playerVisualClone[owner];
            playerDummy.CopyVisuals(other);
            playerDummy.ResetEffects();
            playerDummy.ResetVisibleAccessories();
            playerDummy.DisplayDollUpdate();
            playerDummy.UpdateSocialShadow();
            playerDummy.Center = Projectile.Center - (Projectile.velocity.SafeNormalize(Vector2.Zero) *42f);
            playerDummy.direction = target == null ? other.direction : (Projectile.velocity.X > 0f) ? 1 : (-1);
        
            playerDummy.equippedWings = currentWings;
            playerDummy.ShouldDrawWingsThatAreAlwaysAnimated();
            playerDummy.velocity.Y = Projectile.velocity.Y;
            playerDummy.wings = 2;
            playerDummy.wingFrame = wingFrame;
            playerDummy.PlayerFrame();
            playerDummy.socialIgnoreLight = true;
            Main.PlayerRenderer.DrawPlayer(Main.Camera, playerDummy, Projectile.position, 0f, playerDummy.fullRotationOrigin);
        }
        catch (Exception e)
        {
            TimeLogger.DrawException(e);
            Projectile.active = false;
            Main.spriteBatch.End();
            Main.spriteBatch.Begin();
            return false;
        }
        return false;
    }
}