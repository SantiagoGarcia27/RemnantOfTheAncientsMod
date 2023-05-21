using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Localization;
using Terraria.GameContent.Creative;
using RemnantOfTheAncientsMod.Content.Items.Items;

namespace RemnantOfTheAncientsMod.Content.Items.Armor.Daylight
{
	[AutoloadEquip(EquipType.Legs)]
	public class Daylight_Legging : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Daylight Greaves");
			DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Grebas de tusonita");
			DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Grèves Tuxonite");
            Tooltip.SetDefault("5% increased ranged critical strike chance");
            Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "+5% de chances de coup critique à distance");
            Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Probabilidad de golpe crítico a distancia aumentada un 5%");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
			Item.width = 18;
			Item.height = 18;
			Item.value = 3000;
			Item.rare = ItemRarityID.White;
			Item.defense = 4;
		}
        public override void UpdateEquip(Player player)
        {
            player.GetCritChance(DamageClass.Ranged) += 5f;
        }
        public override void AddRecipes()
		{
			CreateRecipe()
			.AddIngredient(ModContent.ItemType<TuxoniteBar>(), 25)//30
			.AddTile(TileID.Anvils)
			.Register();
		}
	}
}

