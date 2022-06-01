using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using opswordsII;
using Terraria.Localization;

namespace opswordsII.Items.Armor
{
	[AutoloadEquip(EquipType.Body)]
	public class Tuxonite_chesplate: ModItem
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			DisplayName.SetDefault("Tuxonite Chainmail");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Cotte de mailles Tuxonite");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Cota de malla de tusonita");
			Tooltip.SetDefault("");
		}
		public override void SetDefaults()
		{
			Item.width = 18;
			Item.height = 18;
			Item.value = 3000;
			Item.rare = 0;
			Item.defense = 7;
		}
	     public override void AddRecipes()
		{
			CreateRecipe()
			.AddIngredient(null,"TuxoniteBar",35)
			.AddTile(TileID.Anvils)
			.Register();
		}
	}
}

