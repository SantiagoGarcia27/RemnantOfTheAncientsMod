using RemnantOfTheAncientsMod.Common.RemPlayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Common.UtilsTweaks
{
    public static class ShopUtils
    {
        const float minDiscount = 0.1f;
        static public float maxStyleStat {get; set;}
        static public float GetShopDiscount(Player player)
        {
            StatPlayer modPlayer = player.GetModPlayer<StatPlayer>();
            if (modPlayer == null || modPlayer.StyleStat == 0) return 1f;

            float discount = getStyleDiscount(modPlayer.StyleStat);
            if (discount < minDiscount) return minDiscount;
            return discount;
        }

        static private float getStyleDiscount(float value)
        {
            return 1f - (value / 200f);
        }
        static public double getStyleDiscountPorcentage(float value)
        {
            return Math.Round(value / 2f,2); 
        }

    }
}
