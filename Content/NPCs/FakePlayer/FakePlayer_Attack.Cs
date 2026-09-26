using Microsoft.Xna.Framework;
using PlayerProxyLib.Common;
using RemnantOfTheAncientsMod.Common.UtilsTweaks;
using System;
using System.IO;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using static FakePlayer_Setup;

public class FakePlayer_Attack
{
    Player proxy;
    NPC npc;
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
        if (Main.netMode == NetmodeID.MultiplayerClient)
            return;

        if (target == null || !target.active || target.dead)
        {
            ReleaseItem();
            return;
        }

        float distance = npc.Center.DistanceSQ(target.Center);

        float distanceMin = 10.ToCoordinatePosition() * 10.ToCoordinatePosition();

        float distanceMax = 80.ToCoordinatePosition() * 80.ToCoordinatePosition();

        int nextSlot =
            distance < distanceMin ? 0 :
            distance < distanceMax ? 1 :
            -1;

        if (nextSlot != lastSlot)
        {
            ClearWeaponAnimation(lastSlot);
            lastSlot = nextSlot;
        }

        if (proxy.inventory[54].IsAir)
            proxy.inventory[54] = new Item(ItemID.WoodenArrow, 100);

        if (proxy.inventory[55].IsAir)
            proxy.inventory[55] = new Item(ItemID.MusketBall, 100);


        bool canAttack = false;

        if (nextSlot == 0 && !proxy.inventory[0].IsAir)
        {
            SelectedItem = 0;
            canAttack = true;
        }
        else if (nextSlot == 1 && !proxy.inventory[1].IsAir)
        {
            SelectedItem = 1;
            canAttack = true;
        }
       

        if (!canAttack)
        {
            ReleaseItem();
            return;
        }

        proxy.SetMouseWorld(target.Center);

        Item item = proxy.HeldItem;

        proxy.controlUseItem = true;

        // Las armas normales necesitan nuevos "clicks"
        // para repetir el ataque si no tienen autoReuse.
        if (!item.channel && proxy.itemAnimation <= 0)
        {
            proxy.releaseUseItem = true;
        }

        // Si item.channel == true:
        // NO tocamos releaseUseItem mientras esté mantenido.
    }

    private void ReleaseItem()
    {
        proxy.controlUseItem = false;

        // El botón realmente fue soltado.
        proxy.releaseUseItem = true;

        // Termina cualquier channel activo.
        proxy.channel = false;
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
    
    private int meleeAnimationTimer;
    private int meleeItemTimeTimer;

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

    public void Dispose()
    {
        foreach (Projectile projectile in Main.projectile)
            if (projectile != null && projectile.active && projectile.owner == proxy.whoAmI)
                projectile.Kill();

    }



    private void ClearWeaponAnimation(int previousSlot)
    {
        meleeAnimationTimer = 0;
        meleeItemTimeTimer = 0;

        proxy.itemAnimation = 0;
        proxy.itemTime = 0;
        proxy.itemAnimationMax = 0;
        proxy.itemTimeMax = 0;

        proxy.SetCompositeArmFront(
            false,
            Player.CompositeArmStretchAmount.None,
            0f
        );

        if (previousSlot == 0 || previousSlot == 1)
            ReleaseItem();
    }
}
