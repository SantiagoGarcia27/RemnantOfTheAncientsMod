using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;
using RemnantOfTheAncientsMod.Content.Items.Items;

namespace RemnantOfTheAncientsMod.Content.Items.Tools
{
	public class ReinforcedIronAxe : ModItem
	{
		public override void SetStaticDefaults()
		{
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

        int rangeBonus = -2;
        public override void SetDefaults()
		{
			Item.damage = 9;
			Item.DamageType = DamageClass.Melee;
			Item.width = 38;
			Item.height = 38;
			Item.useTime = 60;
			Item.useAnimation = 60;
			Item.axe = 60;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 5;
			Item.value = 1300;
			Item.rare = ItemRarityID.White;
			Item.UseSound = SoundID.Item1;
			Item.scale = 1.28f;
			Item.autoReuse = true;
			Item.useTurn = true;
		}
        public override void HoldItem(Player player)
        {
            Player.tileRangeX += rangeBonus;
            Player.tileRangeY += rangeBonus;
        }


        public override void AddRecipes()
		{
			CreateRecipe()
			.AddIngredient<ReinforcedIronBar>(9)
			.AddIngredient(ItemID.Wood, 3)
			.AddTile(TileID.Anvils)
			.Register();
		}

	}
}