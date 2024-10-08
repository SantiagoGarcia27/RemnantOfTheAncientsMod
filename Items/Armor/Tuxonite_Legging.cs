using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Localization;
using Terraria.GameContent.Creative;

namespace opswordsII.Items.Armor
{
	[AutoloadEquip(EquipType.Legs)]
	public class Tuxonite_Legging : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Tuxonite Greaves");
			DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Grebas de tusonita");
			DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Grèves Tuxonite");
		}

		public override void SetDefaults()
		{
			Item.width = 18;
			Item.height = 18;
			Item.value = 3000;
			Item.rare = 0;
			Item.defense = 5;
		}

		public override void AddRecipes()
		{
			CreateRecipe()
			.AddIngredient(null,"TuxoniteBar",30)
			.AddTile(TileID.Anvils)
			.Register();
		}
	}
}

