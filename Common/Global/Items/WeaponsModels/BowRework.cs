using Microsoft.Xna.Framework;
using RemnantOfTheAncientsMod.Common.RemPlayer;
using RemnantOfTheAncientsMod.Common.UtilsTweaks;
using RemnantOfTheAncientsMod.Common.WeaponsRework;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Common.Global.Items.WeaponsModels
{
    public class BowRework : GlobalItem
    {
        public bool IsRepeater;
        public bool IsBow;








        public bool WeaponConf = ModContent.GetInstance<ConfigServer>().VanillaWeaponsChangesConf;
        public bool BowReworkConfig = ModContent.GetInstance<ConfigServer>().BowReworkConf;


        public override void SetDefaults(Item item)
        {
            if(WeaponConf)
            {
                if (Utils1.NameHasWord(item.Name, "Repeater") || Utils1.NameHasWord(item.Name, "Repetidor") || item.type == ItemID.ChlorophyteShotbow)
                {
                    item.useAnimation += 5;
                    item.useTime += 5;
                    IsRepeater = true;
                    item.channel = true;
                }
                else if (item.type == ItemID.Blowpipe || item.type == ItemID.Blowgun)
                {
                    IsRepeater = true;
                    item.channel = true;
                }
                else if(item.useAmmo == AmmoID.Arrow)
                {
                    IsBow = true;
                }

            }
        }
        public override void ModifyShootStats(Item item, Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            var globalPlayer = item.GetGlobalItem<RemnantGlobalItem>();
            if (WeaponConf)
            {
                if (item.useAmmo == AmmoID.Arrow)
                {
                    float bonus = player.GetModPlayer<StatPlayer>().ArrowSpeedBonus;

                    velocity *= bonus;
                }
                if (IsRepeater)
                {
                    velocity = velocity.RotatedByRandom(MathHelper.ToRadians(1 + (globalPlayer.UseTimeReduction * 20)));
                }
            }
        }
        public override bool Shoot(Item item, Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            List<int> BannedBows = BowsChargeRework.BannedBows;

            if (BowReworkConfig && !BannedBows.Contains(item.type))
            {
                if (item.useAmmo == AmmoID.Arrow && !IsRepeater)
                {
                    bool Canshoot = item.GetGlobalItem<BowsChargeRework>().Canshoot;
                    return Canshoot && base.Shoot(item, player, source, position, velocity, type, damage, knockback);
                }
            }
            return base.Shoot(item, player, source, position, velocity, type, damage, knockback);
        }
        public override void HoldItem(Item item, Player player)
        {
            var globalPlayer = item.GetGlobalItem<RemnantGlobalItem>();
            if (IsRepeater)
            {
                float multiplier = 32f;
                if (item.type == ItemID.Blowpipe)
                    multiplier = 32f;
                else if (item.type == ItemID.Blowgun)
                    multiplier = 26f;

                if (player.channel)
                {
                    if (globalPlayer.Timmer <= item.useTime * 16f)
                    {
                        globalPlayer.Timmer++;
                        globalPlayer.UseTimeReduction = globalPlayer.Timmer / (item.useTime * multiplier);
                    }
                }
                else if (Main.mouseLeftRelease)
                {
                    globalPlayer.Timmer = globalPlayer.UseTimeReduction = 0;
                }
            }
            base.HoldItem(item, player);
        }
        public override float UseTimeMultiplier(Item item, Player player)
        {
            if (IsRepeater)
            {
                var globalPlayer = item.GetGlobalItem<RemnantGlobalItem>();
                //Main.NewText(1f - UseTimeReduction);
                return 1f - globalPlayer.UseTimeReduction;
            }
            return base.UseTimeMultiplier(item, player);
        }
        public override bool CanUseItem(Item item, Player player)
        {
            BowReworkConfig = ModContent.GetInstance<ConfigServer>().BowReworkConf;
            return base.CanUseItem(item, player);
        }

        public override bool InstancePerEntity => true;
    }
}
