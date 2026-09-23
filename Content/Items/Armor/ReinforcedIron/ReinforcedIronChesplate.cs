using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.GameContent.Creative;
using RemnantOfTheAncientsMod.Content.Items.Items;
using RemnantOfTheAncientsMod.Common.UtilsTweaks;

namespace RemnantOfTheAncientsMod.Content.Items.Armor.ReinforcedIron
{
    [AutoloadEquip(EquipType.Body)]
	public class ReinforcedIronChesplate : ModItem
	{
		public override void SetStaticDefaults()
		{		
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

        private readonly int DamageReduction = 8;
        private readonly int MovmentSpeedBonus = -25;

        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(DamageReduction, MovmentSpeedBonus);
        public override void SetDefaults()
		{
			Item.width = 34;
			Item.height = 24;
			Item.value = 10000;
			Item.rare = ItemRarityID.Blue;
			Item.defense = 13;
		}
        public override void UpdateEquip(Player player)
        {
			player.endurance += DamageReduction/100f;
            player.moveSpeed += MovmentSpeedBonus/100f;
        }

        public override void AddRecipes()
		{
			CreateRecipe()
            .AddIngredient(ModContent.ItemType<ReinforcedIronBar>(), RecipeUtils.SearchAmmountRecipe(ItemID.IronChainmail, ItemID.IronBar))
            .AddTile(TileID.Anvils)
			.Register();
		}
	}
}

