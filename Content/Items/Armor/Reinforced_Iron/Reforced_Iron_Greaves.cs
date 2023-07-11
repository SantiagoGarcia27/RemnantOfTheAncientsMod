using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Localization;
using Terraria.GameContent.Creative;
using RemnantOfTheAncientsMod.Content.Items.Items;
using RemnantOfTheAncientsMod.Common.UtilsTweaks;

namespace RemnantOfTheAncientsMod.Content.Items.Armor.Reinforced_Iron
{
	[AutoloadEquip(EquipType.Legs)]
	public class Reforced_Iron_Greaves : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Reinforced Iron Greaves");
			DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Perneras de hierro reforzado");
			DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Jambières en fer renforcées");
            Tooltip.SetDefault(tooltip());
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}
        public static string tooltip()
        {
            return LocalizationHelper.IncreasedMovmentSpeedTooltip(-25);
        }

        public override void SetDefaults()
		{
			Item.width = 18;
			Item.height = 18;
			Item.value = 10000;
			Item.rare = ItemRarityID.Blue;
			Item.defense = 7;//18
		}

		public override void UpdateEquip(Player player)
		{
			player.moveSpeed -= 0.25f;
        }

		public override void AddRecipes()
		{
			CreateRecipe()
			.AddIngredient(ModContent.ItemType<Reinforced_ironBar>(), 6)
			.AddTile(TileID.Anvils)
			.Register();
		}
	}
}

