using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.GameContent.Creative;
using RemnantOfTheAncientsMod.Common.UtilsTweaks;

namespace RemnantOfTheAncientsMod.Content.Items.Armor.Daylight
{
	[AutoloadEquip(EquipType.Head)]
	public class DaylightHat : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Daylight Hat");
			DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Sombrero de luz diurna");
			DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Chapeau de la lumière du jour");

            Tooltip.SetDefault(tooltip());
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}
        public static string tooltip()
        {
            return Utils1.IncreasedDamageByTooltip(3,DamageClass.Magic) +
                "\n" + Utils1.IncreasedDamageByTooltip(3, DamageClass.Summon) +
                "\n" + Utils1.IncreasedManaMaxTooltip(20);
        }
        public override void SetDefaults()
		{
			Item.width = 18;
			Item.height = 18;
			Item.value = Item.sellPrice(0,0,5,0);
			Item.rare = ItemRarityID.White;
			Item.defense = 3;
		}

		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == ModContent.ItemType<Daylight_chesplate>() && legs.type == ModContent.ItemType<Daylight_Legging>();
		}
        public override void UpdateEquip(Player player)
        {
            player.GetDamage(DamageClass.MagicSummonHybrid) *= 1.03f;
			player.statManaMax2 += 20;	
        }
        public override void UpdateArmorSet(Player player)
		{

			player.setBonus = Language.GetTextValue("Mods.RemnantOfTheAncientsMod.SetBonus.Daylight");

			if (Main.dayTime)
			{
				player.lifeRegen = player.lifeRegen > 0 ? player.lifeRegen : 1;
				player.lifeRegen *= 2;
			}
			else
			{
				player.statManaMax2 += 20;
			}
			RemnantPlayer.DaylightArmorSetBonus = true;
			//if (RemnantPlayer.tuxoniteStealthCounter == 0)
			//{
			//	player.shroomiteStealth = true;
			//	if (RemnantPlayer.tuxoniteStealthDuration < Utils1.FormatTime(0, 0, 0, 10))
			//	{
			//		RemnantPlayer.tuxoniteStealthDuration++;
			//	}
			//	else
			//	{
			//		RemnantPlayer.tuxoniteStealthCounter = (int)Main.rand.NextFloat(Utils1.FormatTime(0, 0, 0, 30));
			//		RemnantPlayer.tuxoniteStealthDuration = 0;
			//	}
			//}
			//else
			//{
			//	RemnantPlayer.tuxoniteStealthCounter--;
			//}
		}

		public override void AddRecipes()
		{
            CreateRecipe()
             .AddIngredient(ItemID.Daybloom, 10)
             .AddIngredient(ItemID.FallenStar, 1)
             .AddTile(TileID.Anvils)
             .Register();
        }
	}
}