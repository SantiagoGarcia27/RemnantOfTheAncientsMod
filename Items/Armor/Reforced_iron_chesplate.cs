using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using opswordsII;
using Terraria.Localization;

namespace opswordsII.Items.Armor
{
	[AutoloadEquip(EquipType.Body)]
	public class Reforced_iron_chesplate: ModItem
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			DisplayName.SetDefault("Reinforced Iron Breastplate");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Cuirasse en fer renforcé");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Corasa de hierro reforzada");
			Tooltip.SetDefault("");
		}
		public override void SetDefaults()
		{
			Item.width = 18;
			Item.height = 18;
			Item.value = 10000;
			Item.rare = 1;
			Item.defense = 5;
		}
	      public override void AddRecipes()
		{
			CreateRecipe()
			.AddIngredient(null,"Reinforced_ironBar", 5)
			.AddTile(TileID.Anvils)
			.Register();
		}
	}
}

