using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.GameContent.Creative;
using RemnantOfTheAncientsMod.Items.Items;

namespace RemnantOfTheAncientsMod.Items.Armor
{
    [AutoloadEquip(EquipType.Body)]
	public class Tuxonite_chesplate: ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Tuxonite Chainmail");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Cotte de mailles Tuxonite");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Cota de malla de tusonita");
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}
		public override void SetDefaults()
		{
			Item.width = 18;
			Item.height = 18;
			Item.value = 3000;
			Item.rare = ItemRarityID.White;
			Item.defense = 7;
		}
	     public override void AddRecipes()
		{
			CreateRecipe()
			.AddIngredient(ModContent.ItemType<TuxoniteBar>(), 35)
			.AddTile(TileID.Anvils)
			.Register();
		}
	}
}

