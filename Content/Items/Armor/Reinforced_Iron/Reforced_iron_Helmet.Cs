using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.GameContent.Creative;
using RemnantOfTheAncientsMod.Content.Items.Items;
using RemnantOfTheAncientsMod.Common.UtilsTweaks;

namespace RemnantOfTheAncientsMod.Content.Items.Armor.Reinforced_Iron
{
	[AutoloadEquip(EquipType.Head)]
	public class Reforced_iron_Helmet : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Reinforced Iron Helmet");		       
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Casque en fer renforc�");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Casco de hierro reforzado");
            Tooltip.SetDefault(tooltip());

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}
        public static string tooltip()
        {
            return LocalizationHelper.IncreasedDamageByTooltip(5, DamageClass.Melee)
				+"\n"+ LocalizationHelper.IncreasedMovmentSpeedTooltip(-25);
        }
        public override void SetDefaults()
		{
			Item.width = 18;
			Item.height = 18;
			Item.value = 10000;
			Item.rare = ItemRarityID.Blue;
			Item.defense = 8;//5
		}
		public override void UpdateEquip(Player player)
		{
			player.GetDamage(DamageClass.Melee) *= 1.05f;
            player.moveSpeed -= 0.25f;
        }

		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == ModContent.ItemType<Reforced_iron_chesplate>() && legs.type == ModContent.ItemType<Reforced_Iron_Greaves>();
		}

		public override void UpdateArmorSet(Player player)
		{
			player.setBonus = " + 2 defense"
				+ "\n" + LocalizationHelper.IncreasedDamageByTooltip(2, DamageClass.Generic)
				+ "\n" + LocalizationHelper.IncreasedMovmentSpeedTooltip(15);
			player.GetDamage(DamageClass.Generic) *= 1.02f;
			player.statDefense += 2;
			player.moveSpeed += 0.15f;
		}

		public override void AddRecipes()
		{
			CreateRecipe()
			.AddIngredient(ModContent.ItemType<Reinforced_ironBar>(), 4)
			.AddTile(TileID.Anvils)
			.Register();
		}
	}
}