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
			DisplayName.SetDefault("Daylight skirt");
			DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Pollera de hojas");
			DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Jupe lumière du jour");
            Tooltip.SetDefault(tooltip());
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
        public static string tooltip()
        {
            return Utils1.IncreasedDamageByTooltip(4, DamageClass.Summon) +
                "\n" + Utils1.IncreasedManaMaxTooltip(5);
        }
        public override void UpdateEquip(Player player)
        {
            player.GetDamage(DamageClass.Summon) *= 1.04f;
            player.statManaMax2 += 5;
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

