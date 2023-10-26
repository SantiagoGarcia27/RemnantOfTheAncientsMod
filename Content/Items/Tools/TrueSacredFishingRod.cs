using RemnantOfTheAncientsMod.Content.Projectiles.Bobbers;
using Terraria.GameContent.Creative;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using Terraria.Localization;

namespace RemnantOfTheAncientsMod.Content.Items.Tools
{
    public class TrueSacredFishingRod : ModItem
	{
		public override void SetStaticDefaults()
		{
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}
        public int MaxBobers = 2;
        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(MaxBobers);
        public override void SetDefaults()
		{
			Item.width = 24;
			Item.height = 28;
			Item.useAnimation = 8;
			Item.useTime = 8;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.UseSound = SoundID.Item1;
			Item.rare = ItemRarityID.Yellow;
			Item.fishingPole = 90;
			Item.shootSpeed = 15f;
			Item.shoot = ModContent.ProjectileType<TrueSacredBobber>();
			Item.value = Item.buyPrice(0, 3, 0, 0);
		}
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            float spreadAmount = 75f; // how much the different bobbers are spread out.

            for (int index = 0; index < MaxBobers; ++index)
            {
                Vector2 bobberSpeed = velocity + new Vector2(Main.rand.NextFloat(-spreadAmount, spreadAmount) * 0.05f, Main.rand.NextFloat(-spreadAmount, spreadAmount) * 0.05f);

                // Generate new bobbers
                Projectile.NewProjectile(source, position, bobberSpeed, type, 0, 0f, player.whoAmI);
            }
            return false;
        }
        public override void AddRecipes()
		{
			CreateRecipe()
			.AddIngredient(ModContent.ItemType<SacredFishingRod>())
             .AddIngredient(ItemID.ChlorophyteBar, 10)
            .AddTile(TileID.MythrilAnvil)
			.Register();
		}
	}
}