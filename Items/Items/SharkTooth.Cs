using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.GameContent.Creative;

namespace RemnantOfTheAncientsMod.Items.Items
{
	public class SharkTooth : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Shark Tooth");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Polish), "Ząb rekina");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Dent de requin");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Diente de tiburon");
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 25;
		}

		public override void SetDefaults()
		{
			Item.width = 26;
			Item.height = 26;
			Item.maxStack = 100;
			Item.value = 500;
			Item.rare = ItemRarityID.Blue;
		}
	}
}
