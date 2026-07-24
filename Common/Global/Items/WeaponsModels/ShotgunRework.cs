/*using Microsoft.Xna.Framework;
using RemnantOfTheAncientsMod.Common.RemPlayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Common.Global.Items.WeaponsModels
{
    public class ShotgunRework : GlobalItem
    {
        public bool IsShotgun = false;
        public int BulletAmmount = 0;
        public int Unacurency = 0;
        public int CustomProjectileShoot = -1;
        public int ExtraProjectile = -1;

        public override bool InstancePerEntity => true;

        public bool WeaponConf = ModContent.GetInstance<ConfigServer>().VanillaWeaponsChangesConf;
        public override void SetDefaults(Item item)
        {
            if (WeaponConf)
            {
                if (item.type == ItemID.Boomstick)
                {
                    item.GetGlobalItem<CustomTooltip>().SecondHabilitie = true;
                    item.GetGlobalItem<RemnantGlobalItem>().CanCharge = true;
                    new Shotgun(item, true, 3, -1, 3);

                }
                if (item.type == ItemID.QuadBarrelShotgun)
                {
                    item.GetGlobalItem<CustomTooltip>().SecondHabilitie = true;
                    item.GetGlobalItem<RemnantGlobalItem>().CanCharge = true;
                    new Shotgun(item, true, 7, -1, 45);

                }
                if (item.type == ItemID.Shotgun)
                {
                    item.GetGlobalItem<CustomTooltip>().SecondHabilitie = true;
                    item.GetGlobalItem<RemnantGlobalItem>().CanCharge = true;
                    new Shotgun(item, true, 3, -1, 5);

                }
                if (item.type == ItemID.OnyxBlaster)
                {
                    item.GetGlobalItem<CustomTooltip>().SecondHabilitie = true;
                    item.GetGlobalItem<RemnantGlobalItem>().CanCharge = true;
                    new Shotgun(item, true, 4, -1, 5, ExtraProjectile: ProjectileID.BlackBolt);

                }
                if (item.type == ItemID.TacticalShotgun)
                {
                    item.GetGlobalItem<CustomTooltip>().SecondHabilitie = true;
                    item.GetGlobalItem<RemnantGlobalItem>().CanCharge = true;
                    new Shotgun(item, true, 6, -1, 10);
                }
            }
            base.SetDefaults(item);
        }



        public void SetShootgunStats(bool _IsShotgun, int _BulletAmmount, int _Unacurency, int _CustomProjectileShoot, int _ExtraProjectile)
        {
            IsShotgun = _IsShotgun;
            BulletAmmount = _BulletAmmount;
            Unacurency = _Unacurency;
            CustomProjectileShoot = _CustomProjectileShoot;
            ExtraProjectile = _ExtraProjectile;
        }

        public override bool AltFunctionUse(Item item, Player player)
        {
            if (IsShotgun) 
                return true;
            return base.AltFunctionUse(item,player);
        }

        public override bool Shoot(Item item, Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {

            if (IsShotgun)
            {
                bool canCharge = item.GetGlobalItem<RemnantGlobalItem>().CanCharge;
                if (BulletAmmount > 0 && canCharge)
                {
                    if (Main.mouseRight && Main.mouseLeft)
                    {
                        SoundEngine.PlaySound(SoundID.Item36);
                        if (Timmer > 0)
                        {
                            shoot(BulletAmmount + (int)Timmer, Unacurency, CustomProjectileShoot);
                            if (item.type == ItemID.OnyxBlaster)
                                shoot(1 + (int)Timmer / 5, 45, ProjectileID.BlackBolt);
                            PlayerKnockback(player, velocity);
                            Timmer = 0;
                        }
                        return false;
                    }
                    else if (Main.mouseRight && !Main.mouseLeft)
                    {
                        item.useStyle = ItemUseStyleID.EatFood;
                        return false;
                    }
                    else if (player.altFunctionUse == 0 && !Main.mouseRight)
                    {
                        shoot(BulletAmmount, Unacurency, CustomProjectileShoot);
                        if (ExtraProjectile != -1) Projectile.NewProjectile(source, position, velocity, ExtraProjectile, damage, knockback, player.whoAmI);
                        
                        return false;
                    }

                }
                else
                {
                    SoundEngine.PlaySound(SoundID.Item36);
                    shoot(BulletAmmount, Unacurency, CustomProjectileShoot);
                    return true;
                }
            }
            void shoot(int ShotgunBulletAmmount, int Unacurency, int CustomProjectileShoot = -1)
            {
                int finalType = CustomProjectileShoot != -1 && type == ProjectileID.Bullet ? CustomProjectileShoot : type;

                item.useStyle = ItemUseStyleID.Shoot;
                SoundEngine.PlaySound(SoundID.Item36);
                for (int i = 0; i < ShotgunBulletAmmount; i++)
                {
                    Vector2 newVelocity = velocity.RotatedByRandom(MathHelper.ToRadians(Unacurency));
                    newVelocity *= 1f - Main.rand.NextFloat(0.3f);
                    Projectile.NewProjectile(source, position, newVelocity, finalType, damage, knockback, player.whoAmI);
                }
            }

            void PlayerKnockback(Player player, Vector2 velocity)
            {
                player.velocity = -velocity * (int)Timmer * 0.05f;
            }

            return base.Shoot(item, player, source, position, velocity, type, damage, knockback);
        }
        public float Timmer = 0;
        public float TimmerMax = 0;
        public override void HoldItem(Item item, Player player)
        {
            if (IsShotgun)
            {
                TimmerMax = item.useTime / 4;

                if (player.channel && player.altFunctionUse == 2)
                {
                    if (Timmer < TimmerMax)
                    {
                        Timmer += 0.5f * player.GetModPlayer<StatPlayer>().ChargeBonus[DamageClass.Ranged];
                        Timmer = Math.Min(Timmer, TimmerMax);
                    }
                }
                else if (Main.mouseRightRelease)
                {
                    Timmer = 0;
                }

                item.GetGlobalItem<RemnantGlobalItem>().Timmer = Timmer;
                item.GetGlobalItem<RemnantGlobalItem>().TimmerMax = TimmerMax;
            }
        }
        public override bool CanConsumeAmmo(Item weapon, Item ammo, Player player)
        {
            if (IsShotgun)
            {
                if (Main.mouseRight && !Main.mouseLeft)
                {
                    return false;
                }
                return base.CanConsumeAmmo(weapon, ammo, player);
            }
            return base.CanConsumeAmmo(weapon, ammo, player);
        }
    }
    public class Shotgun : GlobalItem
    {
        public Shotgun(Item item, bool IsShotgun, int BulletAmmount, int CustomProjectileShoot, int Unacurency, bool CanCharge = true, int ExtraProjectile = -1)
        {
            item.channel = true;
            item.GetGlobalItem<ShotgunRework>().SetShootgunStats(IsShotgun, BulletAmmount, Unacurency, CustomProjectileShoot, ExtraProjectile);
            item.GetGlobalItem<RemnantGlobalItem>().CanCharge = CanCharge;
        }
    }
}
*/