using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;

namespace RemnantOfTheAncientsMod.Content.Items.Weapons.Magic
{
	public class Bee_worm : ModItem
	{
		public override void SetDefaults()
		{
			Item.damage = 69;
			Item.noMelee = true;
			Item.DamageType = DamageClass.Magic;
			Item.channel = true;
			Item.mana = 9;
			Item.rare = ItemRarityID.Yellow;
			Item.width = 28;
			Item.height = 30;
			Item.useTime = 9;
			Item.UseSound = SoundID.Item16;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.shootSpeed = 14f;
			Item.useAnimation = 9;
			Item.shoot = ProjectileID.Bee;
			Item.value = 550000;
			Item.ResearchUnlockCount = 1;
            Item.autoReuse = true;
		}
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			if (Main.rand.NextFloat() <= (float)1 / 2) Projectile.NewProjectile(source, position, velocity, ProjectileID.Wasp, 100, 1, player.whoAmI);
			return true;
		}
		public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
		{
			velocity = new Vector2(velocity.X, velocity.Y).RotatedByRandom(MathHelper.ToRadians(10));
		}
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-10, 0);
        }
        public override void AddRecipes()
		{
			CreateRecipe()
			.AddIngredient(ItemID.BeeGun, 2)
			.AddIngredient(ItemID.WaspGun, 1)
			.AddIngredient(ItemID.SoulofMight, 3)
			.AddIngredient(ItemID.SoulofSight, 3)
			.AddIngredient(ItemID.SoulofFright, 3)
			.AddTile(TileID.HoneyDispenser)
			.Register();
		}
	}
}
