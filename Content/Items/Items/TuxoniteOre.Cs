using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.GameContent.Creative;
using Terraria.ID;

namespace RemnantOfTheAncientsMod.Content.Items.Items
{
    public class TuxoniteOre : ModItem
	{
		public override void SetStaticDefaults()
		{
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 100;
		}

		public override void SetDefaults()
		{
			Item.width = 26; 
			Item.height = 26;
			Item.maxStack = 100; 
			Item.value = 100;
			Item.rare = ItemRarityID.White;
		}
	}
}
