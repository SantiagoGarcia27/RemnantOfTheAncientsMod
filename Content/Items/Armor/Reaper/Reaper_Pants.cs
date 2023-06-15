using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Localization;
using Terraria.GameContent.Creative;
using RemnantOfTheAncientsMod.Content.Items.Items;
using Microsoft.Xna.Framework;
using RemnantOfTheAncientsMod.Common.UtilsTweaks;
using RemnantOfTheAncientsMod.Common.Global;

namespace RemnantOfTheAncientsMod.Content.Items.Armor.Reaper
{
	[AutoloadEquip(EquipType.Legs)]
	public class Reaper_Pants : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("True Reaper Pants");
			Tooltip.SetDefault(tooltip());

			DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Pantalones de parca verdaderos");
			Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), tooltip());

			DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Vrai pantalon de faucheur");
			Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), tooltip());

			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}
        public static string tooltip()
        {
            return Utils1.IncreasedMovmentSpeedTooltip(10) +
                "\n" + Utils1.IncreasedManaMaxTooltip(40) +
                "\n" + Utils1.IncreasedCritByTooltip(5,DamageClass.Generic);
        }
        private static readonly Color rarityColorOne = Utils1.GetReaperColor(1);

        private static readonly Color rarityColorTwo = Utils1.GetReaperColor(2);

        internal static Color GetRarityColor()
        {
            return Utils1.ColorSwap(rarityColorOne, rarityColorTwo, 9f);
        }
		public override void SetDefaults()
		{
			Item.width = 18;
			Item.height = 18;
			Item.value = Item.sellPrice(1, 0, 0, 0);
            Item.GetGlobalItem<CustomTooltip>().customRarity = CustomRarity.Reaper;
			Item.GetGlobalItem<CustomTooltip>().ReaperItem = true;
            Item.rare = ItemRarityID.Purple;
            Item.defense = 27;
		}

		public override void UpdateEquip(Player player)
		{
			player.moveSpeed += 0.1f;
			player.statManaMax2 += 40;
			player.GetCritChance(DamageClass.Generic) += 5;
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

