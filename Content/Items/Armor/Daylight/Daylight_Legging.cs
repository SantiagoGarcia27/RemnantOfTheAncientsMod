using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Localization;
using Terraria.GameContent.Creative;
using RemnantOfTheAncientsMod.Content.Items.Accesories;
using RemnantOfTheAncientsMod.Common.UtilsTweaks;

namespace RemnantOfTheAncientsMod.Content.Items.Armor.Daylight
{
    [AutoloadEquip(EquipType.Legs)]
	public class Daylight_Legging : ModItem
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Daylight skirt");
			//DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Pollera de hojas");
			//DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Jupe lumière du jour");
            //Tooltip.SetDefault(//Tooltip());
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
			Item.width = 18;
			Item.height = 18;
			Item.value = Item.sellPrice(0, 0, 5, 0);
            Item.rare = ItemRarityID.White;
			Item.defense = 4;
		}
        //public static string //Tooltip()
        //{
        //    return LocalizationHelper.IncreasedDamageBy//Tooltip(4, DamageClass.Summon) +
        //        "\n" + LocalizationHelper.IncreasedManaMax//Tooltip(5);
        //}
        public override void UpdateEquip(Player player)
        {
            player.GetDamage(DamageClass.Summon) *= 1.04f;
            player.statManaMax2 += 5;
        }
        public override void AddRecipes()
		{
			CreateRecipe()
            .AddIngredient(ItemID.Daybloom, 8)
            .AddIngredient(ItemID.Vine, 1)
            .AddIngredient<IronBand>()
            .AddTile(TileID.Anvils)
            .Register();
		}
	}
}

