using RemnantOfTheAncientsMod.Content.Items.Items;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Content.Items.Armor.Desert
{
	[AutoloadEquip(EquipType.Legs)]
	public class Desert_herald_Greaves : ModItem
	{
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }
        public int MinionMaxBonus = 1;
        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(MinionMaxBonus);
        public override void SetDefaults()
		{
			Item.width = 18;
			Item.height = 18;
			Item.value = 10000;
			Item.rare = ItemRarityID.Orange;
			Item.defense = 5;
		}

		public override void UpdateEquip(Player player)
		{
			player.maxMinions += MinionMaxBonus;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ModContent.ItemType<Sand_escense>(), 10)
            .AddIngredient(ItemID.SandBlock, 5)
            .AddIngredient(ItemID.Sandstone, 2)
            .AddTile(TileID.Anvils)
            .Register();
        }
    }
}

