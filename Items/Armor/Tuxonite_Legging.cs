using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Localization;
using Terraria.GameContent.Creative;
using RemnantOfTheAncientsMod.Items.Items;

namespace RemnantOfTheAncientsMod.Items.Armor
{
	[AutoloadEquip(EquipType.Legs)]
	public class Tuxonite_Legging : ModItem
	{
		public override void SetStaticDefaults()
		{
//DisplayName.SetDefault("Tuxonite Greaves");
//DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Grebas de tusonita");
//DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Grèves Tuxonite");
CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
Item.width = 18;
Item.height = 18;
Item.value = 3000;
Item.rare = ItemRarityID.White;
Item.defense = 5;
		}

		public override void AddRecipes()
		{
CreateRecipe()
.AddIngredient(ModContent.ItemType<TuxoniteBar>(), 30)
.AddTile(TileID.Anvils)
.Register();
		}
	}
}

