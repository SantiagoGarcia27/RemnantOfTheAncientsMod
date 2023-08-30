using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using Terraria.Localization;

namespace RemnantOfTheAncientsMod.Content.Items.Armor.Daylight
{
	[AutoloadEquip(EquipType.Body)]
	public class Daylight_chesplate : ModItem
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Daylight Shirt");
			//DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Cotte de mailles Tuxonite");
			//DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Camisa de luz diurna");
            //Tooltip.SetDefault(//Tooltip());
            //Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), //Tooltip());
            //Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), //Tooltip());
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}
        public int IncreasedMagicDamage = 3;
        public int IncreasedMinionBonus = 1;
        public int IncreasesMaxMana = 5;
        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(IncreasedMagicDamage, IncreasesMaxMana, IncreasedMinionBonus);
        public override void SetDefaults()
		{
			Item.width = 18;
			Item.height = 18;
			Item.value = Item.sellPrice(0, 0, 10, 0);
            Item.rare = ItemRarityID.White;
			Item.defense = 3;
		}
		//public static string //Tooltip()
		//{
		//	return LocalizationHelper.IncreasedCritBy//Tooltip(3, DamageClass.Magic) +
		//		"\n" + LocalizationHelper.IncreasedMinion//Tooltip(1) +
		//		"\n" + LocalizationHelper.IncreasedManaMax//Tooltip(5);
  //      }
        public override void UpdateEquip(Player player)
        {
			player.GetCritChance(DamageClass.Magic) += 3f;
			player.maxMinions++;
			player.statManaMax2 += 5;
        }
        public override void AddRecipes()
		{
			CreateRecipe()
			.AddIngredient(ItemID.Daybloom,8)
			.AddIngredient(ItemID.Vine, 1)
            .AddRecipeGroup(RecipeGroupID.IronBar,2)
            .AddTile(TileID.Anvils)
			.Register();
		}
	}
}

