using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Localization;
using Terraria.GameContent.Creative;
using opswordsII.Items.Items;

namespace opswordsII.Items.Armor
{
	[AutoloadEquip(EquipType.Legs)]
	public class R_I_L: ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Reinforced Iron Greaves");
			Tooltip.SetDefault(""
			+ "\nDecrease the maximum speed by 3/4");

			DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Perneras de hierro reforzado");
			DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), ""
		   + "\nDisminulle la velocidad maxima en 3/4");

			DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Jambières en fer renforcées");
			Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), ""
		  + "\nDiminuez la vitesse maximale de 3/4");

			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
			Item.width = 18;
			Item.height = 18;
			Item.value = 10000;
			Item.rare = ItemRarityID.Blue;
			Item.defense = 18;
		}

		public override void UpdateEquip(Player player)
		{
			player.moveSpeed = 0.25f;
		}

		public override void AddRecipes()
		{
			CreateRecipe()
			.AddIngredient(ModContent.ItemType<Reinforced_ironBar>(),6)
			.AddTile(TileID.Anvils)
			.Register();
		}
	}
}

