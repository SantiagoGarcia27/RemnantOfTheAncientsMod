using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.GameContent.Creative;

namespace opswordsII.Items.Items
{
	public class Sand_escense : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Sand Essence");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Polish), "Escense piasku");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Escense de sable");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Esencia de arena");
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 100;
		}
			public override void SetDefaults()
		{
			Item.width = 26; 
			Item.height = 26;
			Item.maxStack = 99; 
			Item.value = 100; 
			Item.rare = ItemRarityID.Green;
        }
	}
}