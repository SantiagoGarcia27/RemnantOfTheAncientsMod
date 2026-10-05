using Microsoft.Xna.Framework;
using RemnantOfTheAncientsMod.Common.Global.Items;
using RemnantOfTheAncientsMod.Common.Global.Items.WeaponsModels;
using RemnantOfTheAncientsMod.Common.RemPlayer;
using RemnantOfTheAncientsMod.Common.UtilsTweaks;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Common.WeaponsRework
{

    public class BowsChargeRework : GlobalItem
    {
     
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
        private bool IsBow;

        private int _burstRemaining;
        private int _burstDelay;

        private Vector2 _burstPosition;
        private Vector2 _burstVelocity;
        private int _burstType;
        private int _burstDamage;
        private float _burstKnockback;

        public override bool InstancePerEntity => true;
        public override void SetDefaults(Item item)
        {
            IsRepeater = item.GetGlobalItem<BowRework>().IsRepeater;
            IsBow = item.GetGlobalItem<BowRework>().IsBow;

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
        public override bool AppliesToEntity(Item entity, bool lateInstantiation) => entity.useAmmo == AmmoID.Arrow && !entity.GetGlobalItem<BowRework>().IsRepeater;
        public override void ModifyShootStats(Item item, Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {

            if (IsBow && BowsReworkConfig)
            {
                if (_timmer == 0) _timmer = 1;
                velocity *= _timmer;
                if (velocity == Vector2.Zero) velocity.X = 5 * player.direction * _timmer;
                float damageMultiplier = 1 + (_timmer / (_timmerMax * 2) * 3);
                damage = (int)(damage * damageMultiplier);
                knockback *= damageMultiplier;
            }

            base.ModifyShootStats(item, player, ref position, ref velocity, ref type, ref damage, ref knockback);
        }
        int shootdelay = 0;
        public override bool Shoot(Item item, Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (!IsRepeater && !BannedBows.Contains(item.type))
            {
                if (IsBow && BowsReworkConfig)
                {
                    AutoCharge = player.GetModPlayer<RemnantPlayer>().AutoCharge;
                    if ((_timmer > 1 || Canshoot) && (Main.mouseLeftRelease || AutoCharge))
                    {
                        Canshoot = false;
                        int numProjectiles = item.useAnimation / item.useTime;

                        if (numProjectiles > 1 && _timmer >= _timmerMax)
                        {
                            _burstRemaining = numProjectiles;

                            _burstPosition = position;
                            _burstVelocity = velocity;
                            _burstType = type;
                            _burstDamage = damage;
                            _burstKnockback = knockback;
                        }
                        item.GetGlobalItem<RemnantGlobalItem>().Timmer = 0;
                        _timmer = 0;
                        return base.Shoot(item, player, source, position, velocity, type, damage, knockback);
                    }
                }
                if (Canshoot || !BowsReworkConfig)
                    return base.Shoot(item, player, source, position, velocity, type, damage, knockback);
                else return false;
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
            StatPlayer statPlayer = player.GetModPlayer<StatPlayer>();
            if (!statPlayer.ChargeBonus.ContainsKey(DamageClass.Ranged)) statPlayer.ChargeBonus.Add(DamageClass.Ranged, 1f);
            ChargeBonus = statPlayer.ChargeBonus[DamageClass.Ranged];

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

                if (_burstRemaining > 0 && player.whoAmI == Main.myPlayer)
                {
                    if (_burstDelay <= 0)
                    {
                        Projectile.NewProjectile(player.GetSource_ItemUse(item), _burstPosition, _burstVelocity, _burstType, _burstDamage, _burstKnockback, player.whoAmI);

                        _burstRemaining--;
                        _burstDelay = 1;
                    }
                    else
                    {
                        _burstDelay--;
                    }
                }
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
            BowsReworkConfig = item.GetGlobalItem<BowRework>().BowReworkConfig;
            return base.CanUseItem(item, player);
        }
    }
}
