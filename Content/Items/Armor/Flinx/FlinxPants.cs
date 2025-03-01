using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Localization;
using Terraria.GameContent.Creative;

namespace RemnantOfTheAncientsMod.Content.Items.Armor.Flinx
{
    [AutoloadEquip(EquipType.Legs)]
	public class FlinxPants : ModItem
	{
		public override void SetStaticDefaults()
		{	
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

        public int MovementSpeedBonus = 5;
        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(MovementSpeedBonus);
        public override void SetDefaults()
		{
			Item.width = 18;
			Item.height = 18;
			Item.value = Item.sellPrice(0, 1, 0, 0); 
			Item.rare = ItemRarityID.Green;
			Item.defense = 2;
		}
        public override void UpdateEquip(Player player)
        {
            player.moveSpeed *= 1 + MovementSpeedBonus/100f;
        }
        public override void AddRecipes()
		{
			CreateRecipe()
			.AddRecipeGroup("GoldBar",4)
			.AddIngredient(ItemID.FlinxFur,4)
            .AddIngredient(ItemID.Silk, 5)
            .AddTile(TileID.Loom)
			.Register();
		}
	}
}

