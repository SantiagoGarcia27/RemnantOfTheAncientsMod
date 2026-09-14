using Microsoft.Xna.Framework;
using RemnantOfTheAncientsMod.Common.Global;
using RemnantOfTheAncientsMod.Common.UtilsTweaks;
using RemnantOfTheAncientsMod.Content.Projectiles;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static FakePlayer_Setup;

public class FakePlayer_Attack
{


    private int attackTimer;
    Player proxy;
    NPC npc;

    private int dismissWeaponnTimmer;
    readonly int dismissWeaponnTimmerMax = Utils1.FormatTimeToTick(Second: 2);

    FakePlayerEquipmentList inventory;
    public FakePlayerEquipment meleeWeapon => inventory.meleeWeapon;
    public FakePlayerEquipment rangeWeapon => inventory.rangedWeapon;
    Player target;

    int meleeWeaponIndex { get; set; }
    int rangerWeaponIndex { get; set; }
    public FakePlayer_Attack(Player proxy, NPC npc, FakePlayerEquipmentList inventory, ref int meleeWeaponIndex, ref int rangerWeaponIndex)
    {
        this.proxy = proxy;
        this.npc = npc;
        this.inventory = inventory;
        this.meleeWeaponIndex = meleeWeaponIndex;
        this.rangerWeaponIndex = rangerWeaponIndex;
    }


    public void SetTarget(Player target) => this.target = target;

    private int lastSlot = 0;

    private int SelectedItem
    {
        get => proxy.selectedItem;
        set
        {
            proxy.selectedItem = value;
            if (Main.netMode != NetmodeID.MultiplayerClient) npc.netUpdate = true;
        }
    }
    public void AI()
    {

        float distance = npc.Center.DistanceSQ(target.Center);
        float distanceMin = 10.ToCoordinatePosition() * 10.ToCoordinatePosition();
        float distanceMax = 80.ToCoordinatePosition() * 80.ToCoordinatePosition();

        if (target != null && !target.dead)
        {

            
            if (distance < distanceMin)
            {
                if (meleeWeapon == null) return;
                SelectedItem = 0; 
                switch(meleeWeapon.category)
                {
                    case FakePlayerEquipment.itemType.spear:
                        SpearMeleeAi();
                        break;
                    case FakePlayerEquipment.itemType.mace:
                        MaceAI();
                        break;
                    default:
                        GenericMeleeAi();
                        break;
                }
               
            }
            else if(distance < distanceMax)
            {
                SelectedItem = 1;
                ShootAi();
            }
            if (lastSlot != SelectedItem)
            {
                ClearWeaponAnimation();
            }
            lastSlot = SelectedItem;
        }
    }

    int MeleeWeaponIndex
    {
        get => meleeWeaponIndex;
        set
        {
            meleeWeaponIndex = value;
            if (Main.netMode != NetmodeID.MultiplayerClient) npc.netUpdate = true;
        }
    }
    int RangerWeaponIndex
    {
        get => rangerWeaponIndex;
        set
        {
            rangerWeaponIndex = value;
            if (Main.netMode != NetmodeID.MultiplayerClient) npc.netUpdate = true;
        }
    }
    public void MaceAI()
    {
        Player target = Main.player[npc.target];

        float distance = npc.Center.DistanceSQ(target.Center);
        float distanceMin = 10.ToCoordinatePosition() * 10.ToCoordinatePosition();
        if (proxy.ownedProjectileCounts[meleeWeapon.item.shoot] < 1 && distance < distanceMin)
        {
            dismissWeaponnTimmer = dismissWeaponnTimmerMax;

            MeleeWeaponIndex = Projectile.NewProjectile(npc.GetSource_FromAI(), proxy.Center, Vector2.Zero, meleeWeapon.item.shoot, 25, 0f, proxy.whoAmI);
            Projectile projectile = Main.projectile[MeleeWeaponIndex];
            ApplyProxyStats(projectile, rangeWeapon.item);
        }
        else
        {
            if (MeleeWeaponIndex == -1) return;

            if (dismissWeaponnTimmer > 0) dismissWeaponnTimmer--;
            else
            {
                if (proxy.ownedProjectileCounts[meleeWeapon.item.shoot] >= 1)
                {
                    proxy.channel = false;
                    Main.projectile[MeleeWeaponIndex].Kill();
                    MeleeWeaponIndex = -1;
                }

            }
        }
    }
    public void SpearMeleeAi()
    {
        // Primero controlar la lanza existente
        if (MeleeWeaponIndex >= 0)
        {
         
            Projectile proj = Main.projectile[MeleeWeaponIndex];
            
            if (!proj.active || proj.type != meleeWeapon.item.shoot)
            {
                MeleeWeaponIndex = -1;
            }
            else
            {
                proxy.itemAnimationMax = meleeWeapon.item.useAnimation;

                proxy.itemAnimation = Math.Max(0, proxy.itemAnimationMax - (int)proj.ai[0]);

                proxy.itemTime = proxy.itemAnimation;

                return;
            }
        }

        // Esperar entre ataques
        if (attackTimer > 0)
        {
            attackTimer--;
            return;
        }

        attackTimer = meleeWeapon.item.useTime;

        int id = meleeWeapon.item.shoot;

        Vector2 velocity = target.Center - npc.Center;
        velocity.Normalize();
        velocity *= meleeWeapon.item.shootSpeed;

        proxy.itemAnimationMax = meleeWeapon.item.useAnimation;
        proxy.itemAnimation = proxy.itemAnimationMax;
        proxy.itemTime = proxy.itemAnimation;
        proxy.itemAnimationMax = Math.Max(proxy.itemAnimationMax, 1);

        MeleeWeaponIndex = Projectile.NewProjectile(npc.GetSource_FromAI(), proxy.Center, velocity, id, meleeWeapon.item.damage, 0f, proxy.whoAmI);

        Projectile projectile = Main.projectile[MeleeWeaponIndex];
        ApplyProxyStats(projectile, rangeWeapon.item);
    }

    private int meleeAnimationTimer;
    private int meleeItemTimeTimer;

    public void GenericMeleeAi()
    {
        // =========================
        // PROYECTILES
        // =========================
        if (meleeWeapon.item.shoot > ProjectileID.None)
        {
            if (attackTimer > 0)
            {
                attackTimer--;
                return;
            }

            attackTimer = meleeWeapon.item.useTime;

            int id = meleeWeapon.item.shoot;

            Vector2 velocity = target.Center - npc.Center;
            velocity.Normalize();

            MeleeWeaponIndex = Projectile.NewProjectile(npc.GetSource_FromAI(), proxy.Center, velocity, id, meleeWeapon.item.damage, 0f, proxy.whoAmI);

            Projectile projectile = Main.projectile[MeleeWeaponIndex];
            ApplyProxyStats(projectile, rangeWeapon.item);

            return;
        }

        BasicMeleeAi();

        // Cooldown entre ataques
        if (attackTimer > 0)
            attackTimer--;
    }

    private void BasicMeleeAi()
    {
        // =========================
        // MELEE BÁSICO
        // =========================


        if (meleeAnimationTimer <= 0 && attackTimer <= 0)
        {
            proxy.inventory[0] = meleeWeapon.item;
            proxy.selectedItem = 0;

            proxy.itemAnimationMax = meleeWeapon.item.useAnimation;
            meleeAnimationTimer = meleeWeapon.item.useAnimation;

            meleeItemTimeTimer = meleeWeapon.item.useTime;
            attackTimer = meleeWeapon.item.useTime;
        }

        // Animación en curso
        if (meleeAnimationTimer > 0)
        {
            proxy.itemAnimation = meleeAnimationTimer;
            proxy.itemAnimationMax = meleeWeapon.item.useAnimation;

            proxy.itemTime = meleeItemTimeTimer;
            proxy.itemTimeMax = meleeWeapon.item.useTime;

            // Dejar que Terraria procese el uso del arma
            proxy.ItemCheck();

            // Progreso de la animación: 0 -> 1
            float progress = 1f - (meleeAnimationTimer / (float)meleeWeapon.item.useAnimation);

            // Ángulo del brazo durante el swing
            float armRotation = MathHelper.Lerp(-2.2f, 0.8f, progress);

            // Invertir el movimiento si mira hacia la izquierda
            if (proxy.direction == -1)
                armRotation = MathHelper.Pi - armRotation;

            proxy.SetCompositeArmFront(true, Player.CompositeArmStretchAmount.Full, armRotation);
            if (proxy.ownedProjectileCounts[ModContent.ProjectileType<DamageHitbox>()] <= 0)
            {
                int index = Projectile.NewProjectile(Projectile.GetSource_None(), proxy.position - proxy.Size, Vector2.Zero, ModContent.ProjectileType<DamageHitbox>(), meleeWeapon.item.damage, meleeWeapon.item.knockBack, Owner: proxy.whoAmI);
                Projectile proj = Main.projectile[index];
                proj.hostile = true;
                proj.friendly = false;
                proj.timeLeft = Math.Max(1, 20);
                proj.Size = meleeWeapon.item.Size * 3 * meleeWeapon.item.scale;
                proj.GetGlobalProjectile<RemnantGlobalProjectile>().useVanillaDamageScale = false;
            }
            if (meleeItemTimeTimer > 0)
                meleeItemTimeTimer--;

            meleeAnimationTimer--;

            return;
        }
    }



    public void ShootAi()
    {
        if (rangeWeapon == null) return;
        if (rangeWeapon.item.shoot == ProjectileID.None) return;
        if (!Collision.CanHitLine(npc.position, npc.width, npc.height, target.position, target.width, target.height)) return;
        // =========================
        // INICIAR DISPARO
        // =========================
        if (attackTimer <= 0 && meleeAnimationTimer <= 0)
        {
            proxy.inventory[1] = rangeWeapon.item;
            SelectedItem = 1;

            proxy.itemAnimationMax = rangeWeapon.item.useAnimation;
            proxy.itemAnimation = rangeWeapon.item.useAnimation;

            proxy.itemTimeMax = rangeWeapon.item.useTime;
            proxy.itemTime = rangeWeapon.item.useTime;

            meleeAnimationTimer = rangeWeapon.item.useAnimation;

            attackTimer = rangeWeapon.item.useTime;

            // Disparar
            int id = rangeWeapon.item.shoot;
            
            Vector2 velocity = target.Center - proxy.Center;

            if (velocity.LengthSquared() > 0f)
            {
                velocity.Normalize();
                velocity *= rangeWeapon.item.shootSpeed * 1.5f;

                proxy.inventory[3] = new Item(ItemID.MusketBall, 10);
                if (id == ProjectileID.PurificationPowder)
                {
                    Item armmo = proxy.ChooseAmmo(rangeWeapon.item);
                    id = armmo?.shoot ?? ProjectileID.WoodenArrowFriendly;
                }

                if(rangeWeapon.item.UseSound == SoundID.Item38) rangeWeapon.item.UseSound = SoundID.Item1;
               
                RangerWeaponIndex = Projectile.NewProjectile(npc.GetSource_FromAI(), proxy.Center, velocity, id, rangeWeapon.item.damage, 0f, proxy.whoAmI);
                Projectile projectile = Main.projectile[RangerWeaponIndex];
                ApplyProxyStats(projectile, rangeWeapon.item);
            }
        }

        // =========================
        // ANIMACIÓN DEL ARMA
        // =========================
        if (meleeAnimationTimer > 0)
        {
            proxy.itemAnimation = meleeAnimationTimer;
            proxy.itemAnimationMax = rangeWeapon.item.useAnimation;

            proxy.itemTime = Math.Min(
                meleeItemTimeTimer,
                rangeWeapon.item.useTime
            );

            proxy.itemTimeMax = rangeWeapon.item.useTime;

            proxy.ItemCheck();

            // Apuntar hacia el objetivo
            Vector2 direction = target.Center - proxy.MountedCenter;

            if (direction.LengthSquared() > 0f)
            {
                float offset = 1.6f;
                float rotation = direction.ToRotation() - MathHelper.PiOver2;

                proxy.SetCompositeArmFront(true, Player.CompositeArmStretchAmount.Full, rotation);
                proxy.itemRotation = rotation + (offset * proxy.direction);
            }
           

            meleeAnimationTimer--;

            if (meleeItemTimeTimer > 0)
                meleeItemTimeTimer--;
        }

        // =========================
        // COOLDOWN
        // =========================
        if (attackTimer > 0)
            attackTimer--;
    }

    private void ApplyProxyStats(Projectile projectile, Item weapon)
    {
        projectile.hostile = proxy.hostile;
        projectile.friendly = !proxy.hostile;

        projectile.GetGlobalProjectile<RemnantGlobalProjectile>().useVanillaDamageScale = false;
        projectile.DamageType = weapon.DamageType;
        projectile.ArmorPenetration = (int)proxy.GetTotalArmorPenetration(weapon.DamageType);
    }

  

    private void ClearWeaponAnimation()
    {
        meleeAnimationTimer = 0;
        meleeItemTimeTimer = 0;

        proxy.itemAnimation = 0;
        proxy.itemTime = 0;
        proxy.itemAnimationMax = 0;
        proxy.itemTimeMax = 0;

        proxy.SetCompositeArmFront(false, Player.CompositeArmStretchAmount.None, 0f);


        if(SelectedItem != 0)
        {
            if (meleeWeapon != null && proxy.ownedProjectileCounts[meleeWeapon.item.shoot] >= 1)
            {
                proxy.channel = false;
                Main.projectile[MeleeWeaponIndex].Kill();
                MeleeWeaponIndex = -1;
            }
        }
        else if(SelectedItem != 1)
        {
            if (rangeWeapon != null && proxy.ownedProjectileCounts[rangeWeapon.item.shoot] >= 1)
            {
                proxy.channel = false;
            }
        }
    }
}