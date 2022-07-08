using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Localization;
using Terraria.GameContent.Creative;

namespace opswordsII.Items.Armor
{
	[AutoloadEquip(EquipType.Legs)]
	public class NightLeggings : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Night Greaves");
			Tooltip.SetDefault(""
			+ "\n5% increased movement speed");

			DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Perneras De La Noche");
			DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), ""
		   + "\nAumenta un 5% la velocidad de movimiento");

			DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Grèves de Nuit");
			Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), ""
		  + "\n5% D'augmentation de la Vitesse de Déplacement");

			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
			Item.width = 18;
			Item.height = 18;
			Item.value = 40000;
			Item.rare = ItemRarityID.Orange;
			Item.defense = 9;
		}

		public override void UpdateEquip(Player player)
		{
			player.moveSpeed += 1.05f;
		}

		public override void AddRecipes()
		{
			CreateRecipe()
			.AddIngredient(null, "NightBar", 13)
			.AddTile(TileID.DemonAltar)
			.Register();
		}
	}
}

