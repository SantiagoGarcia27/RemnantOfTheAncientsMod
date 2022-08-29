using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.GameContent.Creative;
using RemnantOfTheAncientsMod.Items.Items;

namespace RemnantOfTheAncientsMod.Items.Armor
{
    [AutoloadEquip(EquipType.Body)]
	public class Reforced_iron_chesplate: ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Reinforced Iron Breastplate");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Cuirasse en fer renforcé");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Corasa de hierro reforzada");
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}
		public override void SetDefaults()
		{
			Item.width = 18;
			Item.height = 18;
			Item.value = 10000;
			Item.rare = ItemRarityID.Blue;
			Item.defense = 5;
		}
		public override void AddRecipes()
		{
			CreateRecipe()
			.AddIngredient(ModContent.ItemType<Reinforced_ironBar>(), 5)
			.AddTile(TileID.Anvils)
			.Register();
		}
	}
}

