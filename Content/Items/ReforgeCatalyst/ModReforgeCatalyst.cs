using RemnantOfTheAncientsMod.Common.Global.Items;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Content.Items.ReforgeCatalyst
{
    public abstract class ModReforgeCatalyst : ModItem
    {
        public override void SetStaticDefaults()
        {
        }
        public override void SetDefaults()
        {
            Item.width = 38;
            Item.height = 34;
            Item.maxStack = 999;
            Item.value = Item.sellPrice(gold: 10);
            Item.rare = ItemRarityID.Green;
            Item.SetCatalyst(true);
        }

        /// <summary>
        /// Devuelve el prefijo que aplica este catalizador. Override en subclases.
        /// </summary>
        public virtual int GetCatalystReforge(Item inputItem)
        {
            if (Item.TryGetGlobalItem(out CatalystGlobalItem global) && global.Reforges != -1)
                return global.Reforges;
            return -1;
        }
    }
}
