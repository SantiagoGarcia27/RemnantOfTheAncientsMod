using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Localization;
using Terraria.GameContent.Creative;

namespace RemnantOfTheAncientsMod.Content.Items.Armor.TrueHallowed
{
	[AutoloadEquip(EquipType.Legs)]
	public class TrueHallowedGreaves : ModItem
	{
        int MovmentSpeedBonus = 10;
		int DamageBonus = 10;
        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(DamageBonus,MovmentSpeedBonus);
        public override void SetStaticDefaults()
		{
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
			Item.width = 18;
			Item.height = 18;
			Item.value = Item.sellPrice(gold: 5);
			Item.rare = ItemRarityID.Yellow;
			Item.defense = 12;
		}

		public override void UpdateEquip(Player player)
		{
			player.moveSpeed *= 1.10f;
			player.GetDamage(DamageClass.Generic) *= 1.10f;
		}

        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ItemID.HallowedGreaves)
            .AddIngredient(ItemID.ChlorophyteBar, 12)
            .AddTile(TileID.MythrilAnvil)
            .Register();
        }
	}
}

