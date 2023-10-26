using RemnantOfTheAncientsMod.Content.Projectiles.Bobbers;
using Terraria.GameContent.Creative;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Content.Items.Tools
{
    public class SacredFishingRod : ModItem
	{
		public override void SetStaticDefaults()
		{
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
			Item.width = 24;
			Item.height = 28;
			Item.useAnimation = 8;
			Item.useTime = 8;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.UseSound = SoundID.Item1;
			Item.rare = ItemRarityID.Pink;
			Item.fishingPole = 70;
			Item.shootSpeed = 15f;
			Item.shoot = ModContent.ProjectileType<SacredBobber>();
			Item.value = Item.buyPrice(0, 1, 0, 0);
		}

		public override void AddRecipes()
		{
			CreateRecipe()
			.AddIngredient(ItemID.HallowedBar, 7)
			.AddTile(TileID.MythrilAnvil)
			.Register();
		}
	}
}