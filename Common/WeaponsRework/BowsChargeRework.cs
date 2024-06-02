using Microsoft.Xna.Framework;
using RemnantOfTheAncientsMod.Common.Global.Items;
using RemnantOfTheAncientsMod.Common.UtilsTweaks;
using RemnantOfTheAncientsMod.Content.Items.Weapons.Ranger.Bows;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Common.WeaponsRework
{

    public class BowsChargeRework : GlobalItem
    {
        public bool IsBow;
        public bool Canshoot = false;
        public float ChargeBonus; 
        public bool AutoCharge = false;

        private float _timmer;
        private float _timmerMax;

        public static List<int> BannedBows =
        [
           ItemID.DaedalusStormbow
        ];

        private bool WeaponsReworkConfig = ModContent.GetInstance<ConfigServer>().VanillaWeaponsChangesConf;
        private bool BowsReworkConfig = ModContent.GetInstance<ConfigServer>().BowReworkConf;
        private bool IsRepeater;
        public override bool InstancePerEntity => true;
        public override void SetDefaults(Item item)
        {
            IsRepeater = item.GetGlobalItem<RemnantGlobalItem>().IsRepeater;
            if (BannedBows.Contains(item.type))
            {
                item.GetGlobalItem<RemnantGlobalItem>().CanCharge = false;
                IsBow = false;
            }
            else if (item.useAmmo == AmmoID.Arrow && !IsRepeater)
            {
                IsBow = true;
                item.channel = true;
                item.autoReuse = true;
                item.GetGlobalItem<RemnantGlobalItem>().CanCharge = BowsReworkConfig;
            }
            base.SetDefaults(item);
        }
        public override bool AppliesToEntity(Item entity, bool lateInstantiation) => entity.useAmmo == AmmoID.Arrow && !entity.GetGlobalItem<RemnantGlobalItem>().IsRepeater;
        public override void ModifyShootStats(Item item, Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {

            if (IsBow && BowsReworkConfig)
            {
                if (_timmer == 0) _timmer = 1;
                velocity *= _timmer /*/ 2*/;
                if (velocity == Vector2.Zero) velocity.X = 5 * player.direction * _timmer;
                float damageMultiplier = 1 + ((_timmer / (_timmerMax * 2)) * 3);
                //Main.NewText(velocity + "|" + damageMultiplier);
                damage *= (int)damageMultiplier;
                knockback *= damageMultiplier;
                // item.GetGlobalItem<RemnantGlobalItem>().Timmer = 0;
            }

            base.ModifyShootStats(item, player, ref position, ref velocity, ref type, ref damage, ref knockback);
        }
        public override bool Shoot(Item item, Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            //Canshoot = true;
            if (!IsRepeater && !BannedBows.Contains(item.type))
            {
                if (IsBow && BowsReworkConfig)
                {
                    AutoCharge = player.GetModPlayer<RemnantPlayer>().AutoCharge;
                    //if (Main.mouseLeft)
                    //{
                    // Main.NewText(Canshoot + "|" + _timmer + "|" + _timmerMax + "|" + item.GetGlobalItem<RemnantGlobalItem>().Timmer);
                    //return false;	
                    // }
                   // Main.NewText(_timmer + "||" + Canshoot + "|" + Main.mouseLeftRelease + "||" + AutoCharge);
                    if ((_timmer > 1 || Canshoot) && (Main.mouseLeftRelease || AutoCharge))
                    {
                        Canshoot = false;
                        float numProjectiles = item.useAnimation / item.useTime;
                        int i = 0;
                        int shootdelay = 0;
                        if (numProjectiles > 1 && _timmer >= _timmerMax)
                        {
                            while(i < numProjectiles)
                            {
                                if (shootdelay <= 0)
                                {
                                    Projectile.NewProjectile(source, position, velocity, type, damage, knockback);
                                    shootdelay = (int)Utils1.FormatTimeToTick(0, 0, 0, 1);
                                    i++;
                                }
                                else
                                {
                                    shootdelay--;
                                }
                            }
                           
                        }
                        item.GetGlobalItem<RemnantGlobalItem>().Timmer = 0;
                        _timmer = 0;
                        return base.Shoot(item, player, source, position, velocity, type, damage, knockback);
                    }
                }
                if (Canshoot || !BowsReworkConfig)
                {
                    return base.Shoot(item, player, source, position, velocity, type, damage, knockback);
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return base.Shoot(item, player, source, position, velocity, type, damage, knockback);
            }
        }
        public override void HoldItem(Item item, Player player)
        {
            _timmer = item.GetGlobalItem<RemnantGlobalItem>().Timmer;
            _timmerMax = item.GetGlobalItem<RemnantGlobalItem>().TimmerMax;
            ChargeBonus = player.GetModPlayer<RemnantPlayer>().ChargeBonus;

            if (BowsReworkConfig && !BannedBows.Contains(item.type))
            {
                if (IsBow)
                {
                    item.GetGlobalItem<RemnantGlobalItem>().TimmerMax = item.useTime / 2;

                    if (player.channel && Main.mouseLeft)
                    {
                        if (_timmer <= _timmerMax)
                        {
                            if (_timmer > 2)
                            {
                                Canshoot = true;
                            }
                            item.GetGlobalItem<RemnantGlobalItem>().Timmer += 0.5f * ChargeBonus;
                            if (item.GetGlobalItem<RemnantGlobalItem>().Timmer > _timmerMax)
                            {
                                item.GetGlobalItem<RemnantGlobalItem>().Timmer = _timmerMax;
                            }
                        }
                        else
                        {
                            Canshoot = true;
                        }
                        
                    }
                    else if (Main.mouseLeftRelease)
                    {
                        item.GetGlobalItem<RemnantGlobalItem>().Timmer = 0;
                        _timmer = 0;
                    }

                }
            }
            else
            {
                _timmer = 0;
                _timmerMax = 0;
            }
            base.HoldItem(item, player);
        }
        public override bool CanShoot(Item item, Player player)
        {
            if (IsBow && BowsReworkConfig)
            {
                item.GetGlobalItem<RemnantGlobalItem>().CanCharge = BowsReworkConfig;
                return Main.mouseLeftRelease || AutoCharge;
            }
            return base.CanShoot(item, player);
        }
        public override bool CanUseItem(Item item, Player player)
        {
            AutoCharge = player.GetModPlayer<RemnantPlayer>().AutoCharge;
            BowsReworkConfig = item.GetGlobalItem<RemnantGlobalItem>().BowReworkConfig;
            return base.CanUseItem(item, player);
        }
    }
}
