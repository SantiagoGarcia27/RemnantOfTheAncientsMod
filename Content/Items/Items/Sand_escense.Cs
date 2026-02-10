using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.GameContent.Creative;

namespace RemnantOfTheAncientsMod.Content.Items.Items
{
	public class Sand_escense : ModItem
	{
		public override void SetStaticDefaults()
		{
		//DisplayName.SetDefault("Sand Essence");
           // //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Polish), "Escense piasku");
           // //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Escense de sable");
           // //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Esencia de arena");
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 100;
		}
		public override void SetDefaults()
		{
			Item.width = 28; 
			Item.height = 32;
			Item.maxStack = 100; 
			Item.value = 100; 
			Item.rare = ItemRarityID.Green;
        }
	}
}