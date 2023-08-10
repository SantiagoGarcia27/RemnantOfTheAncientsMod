using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.GameContent.Creative;
using RemnantOfTheAncientsMod.Content.Items.Items;
using RemnantOfTheAncientsMod.Common.UtilsTweaks;

namespace RemnantOfTheAncientsMod.Content.Items.Armor.Desert
{
	[AutoloadEquip(EquipType.Body)]
	public class Desert_herald_chesplate : ModItem
	{
        public override void SetStaticDefaults()
        {
           // //DisplayName.SetDefault("Desert Herald Breastplate");
            //Tooltip.SetDefault(//Tooltip());
           // //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Cuirasse de héraut du désert");
           // //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Coraza del heraldo del desierto");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }
        //public static string //Tooltip()
        //{
        //    return LocalizationHelper.IncreasedMinion//Tooltip(1);
        //}
        public int MinionMaxBonus = 1;
        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(MinionMaxBonus);
        public override void SetDefaults()
		{
			Item.width = 18;
			Item.height = 18;
			Item.value = 10000;
			Item.rare = ItemRarityID.Blue;
			Item.defense = 6;//5
		}
        public override void UpdateEquip(Player player)
        {
			player.maxMinions ++;       
        }

        public override void AddRecipes()
		{
			CreateRecipe()
			.AddIngredient(ModContent.ItemType<Sand_escense>(), 10)
            .AddIngredient(ItemID.SandBlock,10)
            .AddIngredient(ItemID.Sandstone, 5)
            .AddTile(TileID.Anvils)
			.Register();
		}
	}
}

