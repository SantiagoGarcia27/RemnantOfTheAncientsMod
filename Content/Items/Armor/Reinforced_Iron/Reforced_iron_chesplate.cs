using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.GameContent.Creative;
using RemnantOfTheAncientsMod.Content.Items.Items;
using RemnantOfTheAncientsMod.Common.UtilsTweaks;

namespace RemnantOfTheAncientsMod.Content.Items.Armor.Reinforced_Iron
{
	[AutoloadEquip(EquipType.Body)]
	public class Reforced_iron_chesplate : ModItem
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Reinforced Iron Breastplate");
			//DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Cuirasse en fer renforcé");
			//DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Corasa de hierro reforzada");
			//Tooltip.SetDefault(//Tooltip());
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}
        //public static string //Tooltip()
        //{
        //    return LocalizationHelper.IncreasedDamageReduction//Tooltip(10)
        //        //+ "\n" + LocalizationHelper.IncreasedMovmentSpeed//Tooltip(-25);
        //}
        public int DamageReduction = 8;
        public int MovmentSpeedBonus = -25;
        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(DamageReduction, MovmentSpeedBonus);
        public override void SetDefaults()
		{
			Item.width = 18;
			Item.height = 18;
			Item.value = 10000;
			Item.rare = ItemRarityID.Blue;
			Item.defense = 13;//5
		}
        public override void UpdateEquip(Player player)
        {
			player.endurance += 0.08f;
            player.moveSpeed -= 0.25f;
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

