using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using Microsoft.Xna.Framework;
using static Terraria.ModLoader.ModContent;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;

namespace opswordsII.Items.Magic
{
	public class LaserMachineGun : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("L.M.G");
            Tooltip.SetDefault("Legendary Drop");
			
		}

		public override void SetDefaults()
		{
			Item.damage = 95;
			Item.noMelee = true;
			Item.DamageType = DamageClass.Magic;
			Item.channel = true; //Channel so that you can held the weapon [Important]
			Item.mana = 2;
			Item.rare = -12;
			Item.width = 28;
			Item.height = 30;
			Item.useTime = 5;
			Item.useStyle = 5;
			Item.shootSpeed = 14f;
			Item.useAnimation = 5;
			Item.shoot = ProjectileID.LaserMachinegunLaser;
			Item.UseSound = SoundID.Item39;  
			Item.value = 600000;
			Item.autoReuse = true;
			
		}
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			float numberProjectiles = 5 + Main.rand.Next(4); // 3, 4, or 5 shots
			float rotation = MathHelper.ToRadians(4);
			position += Vector2.Normalize(velocity) * 45f;
			for (int i = 0; i < numberProjectiles; i++)
			{
				Vector2 perturbedSpeed = velocity.RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))) * .2f; // Watch out for dividing by 0 if there is only 1 projectile.
				Projectile.NewProjectile(source, position, perturbedSpeed, 592, 40, knockback, player.whoAmI);
			}
			return false;
		}
	}
}