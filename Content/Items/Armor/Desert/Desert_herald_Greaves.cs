using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Localization;
using Terraria.GameContent.Creative;
using RemnantOfTheAncientsMod.Content.Items.Items;
using RemnantOfTheAncientsMod.Common.UtilsTweaks;

namespace RemnantOfTheAncientsMod.Content.Items.Armor.Desert
{
	[AutoloadEquip(EquipType.Legs)]
	public class Desert_herald_Greaves : ModItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Desert Herald Greaves");
            Tooltip.SetDefault(tooltip());
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Grèves du héraut du désert");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Grevas del heraldo del desierto");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }
        public static string tooltip()
        {
            return Utils1.IncreasedMinionTooltip(1);
        }

        public override void SetDefaults()
		{
			Item.width = 18;
			Item.height = 18;
			Item.value = 10000;
			Item.rare = 3;
			Item.defense = 5;//18
		}

		public override void UpdateEquip(Player player)
		{
			player.maxMinions++;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ModContent.ItemType<Sand_escense>(), 2)
            .AddIngredient(ItemID.SandBlock, 5)
            .AddIngredient(ItemID.Sandstone, 2)
            .AddTile(TileID.Anvils)
            .Register();
        }
    }
}

