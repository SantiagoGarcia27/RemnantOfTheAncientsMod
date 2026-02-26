using RemnantOfTheAncientsMod.Content.Items.ReforgeCatalyst;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Common.Global.Items
{
    public class CatalystGlobalItem : GlobalItem
    {
        public override bool InstancePerEntity => true;
        public bool Catalyst { get; set; }
        public int ApplyPrice { get; set; }

        public int Reforges { get; set; } = -1;
    }

    public static class CatalystItemExtensions
    {
        public static bool IsCatalyst(this Item item)
        {
            return item.TryGetGlobalItem(out CatalystGlobalItem global) && global.Catalyst;
        }

        public static int GetApplyPrice(this Item item)
        {
            return item.TryGetGlobalItem(out CatalystGlobalItem global) ? global.ApplyPrice : 0;
        }

        public static void SetCatalyst(this Item item, bool value)
        {
            item.GetGlobalItem<CatalystGlobalItem>().Catalyst = value;
        }

        public static void SetApplyPrice(this Item item, string price)
        {
            if (int.TryParse(price, out int parsedPrice))
            {
                item.GetGlobalItem<CatalystGlobalItem>().ApplyPrice = parsedPrice;
            }
        }
        public static void SetApplyPrice(this Item item, int price)
        {
            item.GetGlobalItem<CatalystGlobalItem>().ApplyPrice = price;
        }

        public static void SetReforges(this Item item, int reforges)
        {
            item.GetGlobalItem<CatalystGlobalItem>().Reforges = reforges;
        }


        /// <summary>
        /// Obtiene el reforge del catalizador. Delega al método virtual de ModReforgeCatalyst.
        /// </summary>
        public static int GetCatalystReforge(this Item catalystItem, Item inputItem)
        {
            return catalystItem.ModItem is ModReforgeCatalyst catalyst ? catalyst.GetCatalystReforge(inputItem) : -1;
        }
    }
}

