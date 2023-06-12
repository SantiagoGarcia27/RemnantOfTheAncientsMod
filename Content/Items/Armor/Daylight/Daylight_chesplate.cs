using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.GameContent.Creative;
using RemnantOfTheAncientsMod.Content.Items.Items;
using RemnantOfTheAncientsMod.Common.UtilsTweaks;

namespace RemnantOfTheAncientsMod.Content.Items.Armor.Daylight
{
    [AutoloadEquip(EquipType.Body)]
	public class Daylight_chesplate : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Daylight Shirt");
			DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Cotte de mailles Tuxonite");
			DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Camisa de luz diurna");
            Tooltip.SetDefault(tooltip());
            Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), tooltip());
            Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), tooltip());
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}
		public override void SetDefaults()
		{
			Item.width = 18;
			Item.height = 18;
			Item.value = Item.sellPrice(0, 0, 10, 0);
            Item.rare = ItemRarityID.White;
			Item.defense = 3;
		}
		public static string tooltip()
		{
			return Utils1.IncreasedCritByTooltip(3, DamageClass.Magic) +
				"\n" + Utils1.IncreasedMinionTooltip(1) +
				"\n" + Utils1.IncreasedManaMaxTooltip(5);
        }
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

