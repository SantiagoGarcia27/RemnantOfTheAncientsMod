using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.GameContent.Creative;
using RemnantOfTheAncientsMod.Content.Items.Items;
using RemnantOfTheAncientsMod.Common.UtilsTweaks;

namespace RemnantOfTheAncientsMod.Content.Items.Armor.ReinforcedIron
{
    [AutoloadEquip(EquipType.Head)]
	public class ReinforcedIronHelmet : ModItem
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
			Item.width = 22;
			Item.height = 20;
			Item.value = 10000;
			Item.rare = ItemRarityID.Blue;
			Item.defense = 8;//5
		}
		public override void UpdateEquip(Player player)
		{
			player.GetDamage(DamageClass.Melee) *= 1f + (MeleeDamageBonus/100f);
            player.moveSpeed += MovmentSpeedBonus/100f;
        }

		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == ModContent.ItemType<ReinforcedIronChesplate>() && legs.type == ModContent.ItemType<ReinforcedIronGreaves>();
		}
		int DamageSetBonus = 2;
		int DefenseSetBonus = 2;
		int MovmentSpeedSetBonus = 15;

		public override void UpdateArmorSet(Player player)
		{
            player.setBonus = this.GetLocalization("SetBonus").WithFormatArgs(DamageSetBonus, MovmentSpeedSetBonus, DefenseSetBonus).Value;
            player.GetDamage(DamageClass.Generic) += DamageSetBonus / 100f;
			player.statDefense += DefenseSetBonus;
			player.moveSpeed += MovmentSpeedSetBonus / 100f;
		}

		public override void AddRecipes()
		{
			CreateRecipe()
			.AddIngredient(ModContent.ItemType<ReinforcedIronBar>(), RecipeUtils.SearchAmmountRecipe(ItemID.IronHelmet, ItemID.IronBar))
			.AddTile(TileID.Anvils)
			.Register();
		}
	}
}