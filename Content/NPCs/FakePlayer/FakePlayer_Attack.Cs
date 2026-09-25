using Microsoft.Xna.Framework;
using PlayerProxyLib.Common;
using RemnantOfTheAncientsMod.Common.Global;
using RemnantOfTheAncientsMod.Common.UtilsTweaks;
using RemnantOfTheAncientsMod.Content.Projectiles;
using System;
using System.IO;
using Terraria;
using Terraria.Audio;
using Terraria.Chat;
using Terraria.ID;
using Terraria.Localization;
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

    private int meleeWeaponIndex = -1;
    private int rangerWeaponIndex = -1;
    private int attackSequence;
    private bool receivedVisualState;

    public FakePlayer_Attack(Player proxy, NPC npc, FakePlayerEquipmentList inventory)
    {
        this.proxy = proxy;
        this.npc = npc;
        this.inventory = inventory;
    }


    public void SetTarget(Player target) => this.target = target;

    public void SetInventory(FakePlayerEquipmentList inventory) => this.inventory = inventory;

    private int lastSlot = 0;

    private int SelectedItem
    {
        get => proxy.selectedItem;
        set
        {
            if (proxy.selectedItem == value)
                return;

            proxy.selectedItem = value;
            if (Main.netMode != NetmodeID.MultiplayerClient) npc.netUpdate = true;
        }
    }
    public void AI()
    {
        if (Main.netMode == NetmodeID.MultiplayerClient || target == null || !target.active || target.dead)
            return;

        float distance = npc.Center.DistanceSQ(target.Center);
        float distanceMin = 10.ToCoordinatePosition() * 10.ToCoordinatePosition();
        float distanceMax = 80.ToCoordinatePosition() * 80.ToCoordinatePosition();

        int nextSlot = distance < distanceMin ? 0 : distance < distanceMax ? 1 : -1;
        if (nextSlot != lastSlot)
        {
            ClearWeaponAnimation(lastSlot);
            lastSlot = nextSlot;
        }

        if (nextSlot == 0)
        {
            if (meleeWeapon == null) return;
            SelectedItem = 0;
            switch (meleeWeapon.category)
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
        else if (nextSlot == 1)
        {
            SelectedItem = 1;
            ShootAi();
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
        if (Main.netMode == NetmodeID.MultiplayerClient)
            return;

        Player target = Main.player[npc.target];

        float distance = npc.Center.DistanceSQ(target.Center);
        float distanceMin = 10.ToCoordinatePosition() * 10.ToCoordinatePosition();
        if (proxy.ownedProjectileCounts[meleeWeapon.item.shoot] < 1 && distance < distanceMin)
        {
            dismissWeaponnTimmer = dismissWeaponnTimmerMax;

            MeleeWeaponIndex = Projectile.NewProjectile(npc.GetSource_FromAI(), proxy.Center, Vector2.Zero, meleeWeapon.item.shoot, 25, 0f, proxy.whoAmI);
            RecordAttack();
            if (TryGetProjectile(MeleeWeaponIndex, out Projectile projectile))
                ApplyProxyStats(projectile, meleeWeapon.item);
            else
                MeleeWeaponIndex = -1;
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
                    if (TryGetProjectile(MeleeWeaponIndex, out Projectile projectile) && projectile.active && projectile.owner == proxy.whoAmI)
                        projectile.Kill();
                    MeleeWeaponIndex = -1;
                }

            }
        }
    }
    public void SpearMeleeAi()
    {
        if (Main.netMode == NetmodeID.MultiplayerClient)
            return;

        // Primero controlar la lanza existente
        if (MeleeWeaponIndex >= 0)
        {
            if (!TryGetProjectile(MeleeWeaponIndex, out Projectile proj) || !proj.active || proj.owner != proxy.whoAmI || proj.type != meleeWeapon.item.shoot)
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
        RecordAttack();

        if (TryGetProjectile(MeleeWeaponIndex, out Projectile projectile))
            ApplyProxyStats(projectile, meleeWeapon.item);
        else
            MeleeWeaponIndex = -1;
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

            if (Main.netMode == NetmodeID.MultiplayerClient)
                return;

            int id = meleeWeapon.item.shoot;

            Vector2 velocity = target.Center - npc.Center;
            velocity.Normalize();

            MeleeWeaponIndex = Projectile.NewProjectile(npc.GetSource_FromAI(), proxy.Center, velocity, id, meleeWeapon.item.damage, 0f, proxy.whoAmI);
            RecordAttack();

            if (TryGetProjectile(MeleeWeaponIndex, out Projectile projectile))
                ApplyProxyStats(projectile, meleeWeapon.item);
            else
                MeleeWeaponIndex = -1;

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
            RecordAttack();
        }

        // Animación en curso
        if (meleeAnimationTimer > 0)
        {
            proxy.itemAnimation = meleeAnimationTimer;
            proxy.itemAnimationMax = meleeWeapon.item.useAnimation;

            proxy.itemTime = meleeItemTimeTimer;
            proxy.itemTimeMax = meleeWeapon.item.useTime;

            // Progreso de la animación: 0 -> 1
            float progress = 1f - (meleeAnimationTimer / (float)meleeWeapon.item.useAnimation);

            // Ángulo del brazo durante el swing
            float armRotation = MathHelper.Lerp(-2.2f, 0.8f, progress);

            // Invertir el movimiento si mira hacia la izquierda
            if (proxy.direction == -1)
                armRotation = MathHelper.Pi - armRotation;

            proxy.SetCompositeArmFront(true, Player.CompositeArmStretchAmount.Full, armRotation);
            if (Main.netMode != NetmodeID.MultiplayerClient && proxy.ownedProjectileCounts[ModContent.ProjectileType<DamageHitbox>()] <= 0)
            {
                int index = Projectile.NewProjectile(Projectile.GetSource_None(), proxy.position - proxy.Size, Vector2.Zero, ModContent.ProjectileType<DamageHitbox>(), meleeWeapon.item.damage, meleeWeapon.item.knockBack, Owner: proxy.whoAmI);
                if (TryGetProjectile(index, out Projectile proj))
                {
                    proj.hostile = true;
                    proj.friendly = false;
                    proj.timeLeft = Math.Max(1, 20);
                    proj.Size = meleeWeapon.item.Size * 3 * meleeWeapon.item.scale;
                    proj.GetGlobalProjectile<RemnantGlobalProjectile>().useVanillaDamageScale = false;
                    SyncProxyProjectile(proj);
                }
            }
            if (meleeItemTimeTimer > 0)
                meleeItemTimeTimer--;

            meleeAnimationTimer--;

            return;
        }
    }
    public void ShootAi()
    {
        if (rangeWeapon == null)
            return;

        if (!Collision.CanHitLine(
            npc.position,
            npc.width,
            npc.height,
            target.position,
            target.width,
            target.height))
        {
            proxy.controlUseItem = false;
            return;
        }
        proxy.inventory[54] = new Item(ItemID.WoodenArrow, 100);
        proxy.inventory[3] = new Item(ItemID.MusketBall, 100);
        proxy.inventory[1] = rangeWeapon.item;
        SelectedItem = 1;

        proxy.SetMouseWorld(target.Center);

        proxy.controlUseItem = true;
        proxy.ItemCheck();
    }


    /*public void ShootAi()
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
            meleeItemTimeTimer = rangeWeapon.item.useTime;

            attackTimer = rangeWeapon.item.useTime;
            RecordAttack();

            // Disparar
            int id = rangeWeapon.item.shoot;
            
            Vector2 velocity = target.Center - proxy.Center;

            if (velocity.LengthSquared() > 0f)
            {
                velocity.Normalize();
                velocity *= rangeWeapon.item.shootSpeed * 1.5f;
                if(target.Center.Distance(proxy.Center) > 60.ToCoordinatePosition())
                {
                    float diference = target.Center.Distance(proxy.Center) - 60.ToCoordinatePosition();
                    velocity.RotatedByRandom(diference.ToTilePosition());
                }

                proxy.inventory[3] = new Item(ItemID.MusketBall, 10);
                if (id == ProjectileID.PurificationPowder)
                {
                    Item armmo = proxy.ChooseAmmo(rangeWeapon.item);
                    id = armmo?.shoot ?? ProjectileID.WoodenArrowFriendly;
                }

                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    RangerWeaponIndex = Projectile.NewProjectile(npc.GetSource_FromAI(), proxy.Center, velocity, id, rangeWeapon.item.damage, 0f, proxy.whoAmI);
                    if (TryGetProjectile(RangerWeaponIndex, out Projectile projectile))
                        ApplyProxyStats(projectile, rangeWeapon.item);
                    else
                        RangerWeaponIndex = -1;

                    if (projectile.whoAmI >= 0 && projectile.whoAmI < Main.maxProjectiles)
                    {
                        Projectile proj = Main.projectile[projectile.whoAmI];

                        ChatHelper.BroadcastChatMessage(
                            NetworkText.FromLiteral(
                                $"PROJ={projectile.whoAmI} owner={proj.owner} " +
                                $"ownerActive={Main.player[proj.owner].active} " +
                                $"friendly={proj.friendly} hostile={proj.hostile} " +
                                $"damage={proj.damage} type={proj.type}"
                            ),
                            Color.Yellow
                        );
                    }
                }
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
    }*/

    private void ApplyProxyStats(Projectile projectile, Item weapon)
    {
        projectile.hostile = proxy.hostile;
        projectile.friendly = !proxy.hostile;

        projectile.GetGlobalProjectile<RemnantGlobalProjectile>().useVanillaDamageScale = false;
        projectile.DamageType = weapon.DamageType;
        projectile.ArmorPenetration = (int)proxy.GetTotalArmorPenetration(weapon.DamageType);
        SyncProxyProjectile(projectile);
    }

    private static void SyncProxyProjectile(Projectile projectile)
    {
        projectile.netImportant = true;
        projectile.netUpdate = true;
        // The proxy slot is needed by vanilla weapon AI, but is not a connected
        // client that can send owner-triggered netUpdate packets.
        if (Main.netMode == NetmodeID.Server)
        {
            NetMessage.SendData(MessageID.SyncProjectile, number: projectile.whoAmI);
            projectile.netUpdate = false;
        }
    }

    private void RecordAttack()
    {
        attackSequence++;
        npc.netUpdate = true;

        if (Main.netMode != NetmodeID.Server)
            SoundEngine.PlaySound(proxy.HeldItem.UseSound, npc.Center);
    }

    public readonly struct VisualState
    {
        public readonly int AnimationTimer;
        public readonly int ItemTimeTimer;
        public readonly int ItemAnimation;
        public readonly int ItemAnimationMax;
        public readonly int ItemTime;
        public readonly int ItemTimeMax;
        public readonly int AttackSequence;
        public readonly float ItemRotation;

        public VisualState(int animationTimer, int itemTimeTimer, int itemAnimation, int itemAnimationMax, int itemTime, int itemTimeMax, int attackSequence, float itemRotation)
        {
            AnimationTimer = animationTimer;
            ItemTimeTimer = itemTimeTimer;
            ItemAnimation = itemAnimation;
            ItemAnimationMax = itemAnimationMax;
            ItemTime = itemTime;
            ItemTimeMax = itemTimeMax;
            AttackSequence = attackSequence;
            ItemRotation = itemRotation;
        }
    }

    public VisualState CaptureVisualState() => new(
        meleeAnimationTimer, meleeItemTimeTimer, proxy.itemAnimation,
        proxy.itemAnimationMax, proxy.itemTime, proxy.itemTimeMax,
        attackSequence, proxy.itemRotation);

    public static void WriteVisualState(BinaryWriter writer, VisualState state)
    {
        writer.Write(state.AnimationTimer);
        writer.Write(state.ItemTimeTimer);
        writer.Write(state.ItemAnimation);
        writer.Write(state.ItemAnimationMax);
        writer.Write(state.ItemTime);
        writer.Write(state.ItemTimeMax);
        writer.Write(state.AttackSequence);
        writer.Write(state.ItemRotation);
    }

    public static VisualState ReadVisualState(BinaryReader reader) => new(
        reader.ReadInt32(), reader.ReadInt32(), reader.ReadInt32(), reader.ReadInt32(),
        reader.ReadInt32(), reader.ReadInt32(), reader.ReadInt32(), reader.ReadSingle());

    public void ApplyVisualState(VisualState state)
    {
        if (receivedVisualState && state.AttackSequence != attackSequence && Main.netMode == NetmodeID.MultiplayerClient)
            SoundEngine.PlaySound(proxy.HeldItem.UseSound, npc.Center);

        meleeAnimationTimer = Math.Max(0, state.AnimationTimer);
        meleeItemTimeTimer = Math.Max(0, state.ItemTimeTimer);
        proxy.itemAnimation = Math.Max(0, state.ItemAnimation);
        proxy.itemAnimationMax = Math.Max(0, state.ItemAnimationMax);
        proxy.itemTime = Math.Max(0, state.ItemTime);
        proxy.itemTimeMax = Math.Max(0, state.ItemTimeMax);
        proxy.itemRotation = state.ItemRotation;
        attackSequence = state.AttackSequence;
        receivedVisualState = true;
        if (proxy.selectedItem == 9)
            proxy.SetCompositeArmFront(false, Player.CompositeArmStretchAmount.None, 0f);
    }

    public void ClientVisualAI()
    {
        if (Main.netMode != NetmodeID.MultiplayerClient)
            return;

        if (meleeAnimationTimer > 0)
        {
            if (proxy.selectedItem == 0 && meleeWeapon?.item?.shoot <= ProjectileID.None)
            {
                float progress = 1f - meleeAnimationTimer / (float)Math.Max(1, meleeWeapon.item.useAnimation);
                float armRotation = MathHelper.Lerp(-2.2f, 0.8f, progress);
                if (proxy.direction == -1)
                    armRotation = MathHelper.Pi - armRotation;
                proxy.SetCompositeArmFront(true, Player.CompositeArmStretchAmount.Full, armRotation);
            }
            else if (proxy.selectedItem == 1 && npc.target >= 0 && npc.target < Main.maxPlayers)
            {
                Player currentTarget = Main.player[npc.target];
                if (currentTarget != null && currentTarget.active)
                {
                    Vector2 direction = currentTarget.Center - proxy.MountedCenter;
                    float rotation = direction.ToRotation() - MathHelper.PiOver2;
                    proxy.SetCompositeArmFront(true, Player.CompositeArmStretchAmount.Full, rotation);
                    proxy.itemRotation = rotation + 1.6f * proxy.direction;
                }
            }

            proxy.itemAnimation = Math.Max(0, meleeAnimationTimer--);
        }
        else if (proxy.itemAnimation > 0)
        {
            proxy.itemAnimation--;
        }

        if (meleeItemTimeTimer > 0)
            meleeItemTimeTimer--;
        if (proxy.itemTime > 0)
            proxy.itemTime--;

        if (meleeAnimationTimer == 0 && proxy.itemAnimation == 0)
            proxy.SetCompositeArmFront(false, Player.CompositeArmStretchAmount.None, 0f);
    }

    public void StopAnimation()
    {
        ClearWeaponAnimation(lastSlot);
        lastSlot = 9;
        SelectedItem = 9;
        npc.netUpdate = true;
    }

    private static bool TryGetProjectile(int index, out Projectile projectile)
    {
        if (index >= 0 && index < Main.maxProjectiles)
        {
            projectile = Main.projectile[index];
            return projectile != null;
        }

        projectile = null;
        return false;
    }

    public void Dispose()
    {
        foreach (Projectile projectile in Main.projectile)
            if (projectile != null && projectile.active && projectile.owner == proxy.whoAmI)
                projectile.Kill();

        MeleeWeaponIndex = -1;
        RangerWeaponIndex = -1;
    }

  

    private void ClearWeaponAnimation(int previousSlot)
    {
        meleeAnimationTimer = 0;
        meleeItemTimeTimer = 0;

        proxy.itemAnimation = 0;
        proxy.itemTime = 0;
        proxy.itemAnimationMax = 0;
        proxy.itemTimeMax = 0;

        proxy.SetCompositeArmFront(false, Player.CompositeArmStretchAmount.None, 0f);


        if (previousSlot == 0)
        {
            if (TryGetProjectile(MeleeWeaponIndex, out Projectile projectile) && projectile.active && projectile.owner == proxy.whoAmI)
                projectile.Kill();
            MeleeWeaponIndex = -1;
        }
        if (previousSlot == 1 || previousSlot == 0)
            proxy.channel = false;
    }
}
