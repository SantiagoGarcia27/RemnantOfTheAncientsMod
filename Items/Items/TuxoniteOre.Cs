using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.GameContent.Creative;
using Terraria.ID;

namespace opswordsII.Items.Items
{
    public class TuxoniteOre : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Tuxonite Ore"); 
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Polish), "Ruda Tuxonite");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Minerai de tuxonite");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Mena de Tusonita");
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 100;
		}

		public override void SetDefaults()
		{
			Item.width = 26; 
			Item.height = 26;
			Item.maxStack = 99; 
			Item.value = 100;
			Item.rare = ItemRarityID.White;
		}
	}
}
