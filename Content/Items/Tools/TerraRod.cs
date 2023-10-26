using Microsoft.Xna.Framework;
using RemnantOfTheAncientsMod.Content.Projectiles.Bobbers;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.DataStructures;

namespace RemnantOfTheAncientsMod.Content.Items.Tools
{
	public class TerraRod : ModItem
	{
		public override void SetStaticDefaults()
		{
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
            ItemID.Sets.CanFishInLava[Type] = true;   
        }
        public int MaxBobers = 4;
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
            Item.fishingPole = 100;
			Item.shootSpeed = 20f;
			Item.shoot = ModContent.ProjectileType<TerraBobber>();
			Item.value = Item.buyPrice(0, 10, 0, 0);
		}
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            float spreadAmount = 75f; 

            for (int index = 0; index < MaxBobers; ++index)
            {
                Vector2 bobberSpeed = velocity + new Vector2(Main.rand.NextFloat(-spreadAmount, spreadAmount) * 0.05f, Main.rand.NextFloat(-spreadAmount, spreadAmount) * 0.05f);
                Projectile.NewProjectile(source, position, bobberSpeed, type, 0, 0f, player.whoAmI);
            }
            return false;
        }
        public override void HoldItem(Player player)
        {
            player.accFishingLine = true;
        }
        public override void AddRecipes()
		{
            CreateRecipe()
            .AddIngredient(ModContent.ItemType<TrueNigthFishingRod>())
            .AddIngredient(ModContent.ItemType<TrueSacredFishingRod>())
            .AddIngredient(ItemID.BrokenHeroSword, 1)
            .AddTile(TileID.MythrilAnvil)
            .Register();
        }
	}
}