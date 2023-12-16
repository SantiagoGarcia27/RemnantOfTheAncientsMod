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
	public class Reinforced_iron_Helmet : ModItem
	{
		public override void SetStaticDefaults()
		{	
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}
		
		public int MeleeDamageBonus = 5;
        public int MovmentSpeedBonus = -25;
        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(MeleeDamageBonus,MovmentSpeedBonus);
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
			return body.type == ModContent.ItemType<Reinforced_iron_chesplate>() && legs.type == ModContent.ItemType<Reinforced_Iron_Greaves>();
		}

		public override void UpdateArmorSet(Player player)
		{	
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