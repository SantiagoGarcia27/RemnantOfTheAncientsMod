using System;
using System.IO;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using static Terraria.ModLoader.ModContent;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using opswordsII;
using opswordsII.Projectiles.Lazer;
using Terraria.DataStructures;

namespace opswordsII.Items.Magic
{
	public class Judgment : ModItem
	{
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("Judgment");
			
		}

		public override void SetDefaults() {
			Item.damage = 14;
			Item.noMelee = true;
			Item.DamageType = DamageClass.Magic;
			Item.channel = true; //Channel so that you can hold the weapon [Important]
			Item.autoReuse = true;
			Item.mana = 5;
			Item.rare = 3;
			Item.width = 28;
			Item.height = 30;
			Item.useTime = 20;
			Item.UseSound = SoundID.Item13;
			Item.useStyle = ItemUseStyleID.HoldUp;
			Item.shootSpeed = 14f;
			Item.useAnimation = 20;
			Item.shoot = ModContent.ProjectileType<HollyLaser>(); //Laser
			Item.value = Item.sellPrice(silver: 3);
		}
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			Vector2 target = Main.screenPosition + new Vector2(Main.mouseX, Main.mouseY);
			float ceilingLimit = target.Y;
		/*	if (ceilingLimit > player.Center.Y - 200f)
			{
				ceilingLimit = player.Center.Y - 200f;
			}*/
			for (int i = 0; i < 1; i++)
			{
				position = player.Center + new Vector2((-Main.rand.Next(0, 0) * player.direction), -600f);//401
				position.Y -= (100 * i);//100
				Vector2 heading = target - position;
				if (heading.Y < 0f)
				{
					heading.Y *= -1f;
				}
				if (heading.Y < 20f)
				{
					heading.Y = 20f;
				}
				heading.Normalize();
				heading *= new Vector2(velocity.X, velocity.Y).Length();
				velocity.X = heading.X;
				velocity.Y = heading.Y + Main.rand.Next(-40, 41) * 0.02f;
				Projectile.NewProjectile(source,position.X, position.Y, velocity.X, velocity.Y, ModContent.ProjectileType<HollyLaser>(), damage, knockback, player.whoAmI, 0f, ceilingLimit);
			}
			return false;
		}

	}
}
