using CalamityMod.Projectiles.Melee;
using Microsoft.Xna.Framework;
using RemnantOfTheAncientsMod.Common.Global;
using RemnantOfTheAncientsMod.Common.UtilsTweaks;
using RemnantOfTheAncientsMod.Content.NPCs.FakePlayer;
using System;
using Terraria;
using Terraria.ID;
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
    public FakePlayer_Attack(Player proxy, NPC npc, FakePlayerEquipmentList inventory, ref int meleeWeaponIndex)
    {
        this.proxy = proxy;
        this.npc = npc;
        this.inventory = inventory;
        this.meleeWeaponIndex = meleeWeaponIndex;
    }

    public void SetTarget(Player target) => this.target = target;
    

    public void AI()
    {

        float distance = npc.Center.DistanceSQ(target.Center);
        float distanceMin = 10.ToCoordinatePosition() * 10.ToCoordinatePosition();

        if (target != null && !target.dead)
        {
            if (distance < distanceMin)
            {
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
            else
            {
                ShootAi();
            }
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
    public void MaceAI()
    {
        Player target = Main.player[npc.target];

        float distance = npc.Center.DistanceSQ(target.Center);
        float distanceMin = 10.ToCoordinatePosition() * 10.ToCoordinatePosition();
        if (proxy.ownedProjectileCounts[ProjectileID.Mace] < 1 && distance < distanceMin)
        {
            dismissWeaponnTimmer = dismissWeaponnTimmerMax;

            MeleeWeaponIndex = Projectile.NewProjectile(npc.GetSource_FromAI(), proxy.Center, Vector2.Zero, ProjectileID.Mace, 25, 0f, proxy.whoAmI);
            Main.projectile[MeleeWeaponIndex].hostile = true;
            Main.projectile[MeleeWeaponIndex].friendly = false;
        }
        else
        {
            if (MeleeWeaponIndex == -1) return;

            if (dismissWeaponnTimmer > 0) dismissWeaponnTimmer--;
            else
            {
                if (proxy.ownedProjectileCounts[ProjectileID.Mace] >= 1)
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
                /*if (proxy.itemAnimation > 0)
                   proxy.itemAnimation--;

                return;*/
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
        velocity *= meleeWeapon.item.shootSpeed * 2;

        proxy.itemAnimationMax = meleeWeapon.item.useAnimation;
        proxy.itemAnimation = proxy.itemAnimationMax;


        proxy.itemAnimationMax = meleeWeapon.item.useAnimation;
        proxy.itemAnimation = proxy.itemAnimationMax;
        proxy.itemTime = proxy.itemAnimation;
        proxy.itemAnimationMax = Math.Max(proxy.itemAnimationMax, 1);

        MeleeWeaponIndex = Projectile.NewProjectile(npc.GetSource_FromAI(), proxy.Center, velocity, id, meleeWeapon.item.damage, 0f, proxy.whoAmI);

        Projectile spear = Main.projectile[MeleeWeaponIndex];

        spear.hostile = true;
        spear.friendly = false;

        spear.GetGlobalProjectile<RemnantGlobalProjectile>()
            .useVanillaDamageScale = false;
    }
    public void GenericMeleeAi()
    { 
        if (attackTimer > 0)
        {
            attackTimer--;
            return;
        }
        else attackTimer = meleeWeapon.item.useTime;


        int id = meleeWeapon.item.shoot;

        Vector2 velocity = target.Center - npc.Center;
        velocity.Normalize();

        MeleeWeaponIndex = Projectile.NewProjectile(npc.GetSource_FromAI(), proxy.Center, velocity, id, meleeWeapon.item.damage, 0f, proxy.whoAmI);
        Main.projectile[MeleeWeaponIndex].hostile = true;
        Main.projectile[MeleeWeaponIndex].friendly = false;
        Main.projectile[MeleeWeaponIndex].GetGlobalProjectile<RemnantGlobalProjectile>().useVanillaDamageScale = false;  
    }



    public void ShootAi()
    {
        if (rangeWeapon == null) return;
        if (rangeWeapon?.item.shoot == ProjectileID.None) return;

        if (proxy.ownedProjectileCounts[rangeWeapon.item.shoot] < 1)
        {
            if (attackTimer > 0)
            {
                attackTimer--;
                return;
            }
            else attackTimer = rangeWeapon.item.useTime;


            int id = rangeWeapon.item.shoot;
            Vector2 velocity = (target.Center - npc.Center);
            velocity.Normalize();
            velocity *= rangeWeapon.item.shootSpeed;
            int index = Projectile.NewProjectile(npc.GetSource_FromAI(), proxy.Center, velocity, id, rangeWeapon.item.damage, 0f, proxy.whoAmI);
            Main.projectile[index].hostile = true;
            Main.projectile[index].friendly = false;
            Main.projectile[index].GetGlobalProjectile<RemnantGlobalProjectile>().useVanillaDamageScale = false;
        }
    }
}