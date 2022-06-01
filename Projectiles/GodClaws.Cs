/*using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using opswordsII.Dusts;
using static Terraria.ModLoader.ModContent;


namespace opswordsII.Projectiles
{

	public class GodClaws : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("GodClaws"); //projectile name
		}
		public override void SetDefaults()
		{
			Projectile.width = 36;       //projectile width
			Projectile.height = 36;  //projectile height
			Projectile.friendly = true;      //make that the projectile will not damage you
			Projectile.DamageType = DamageClass.Melee;          // 
			Projectile.tileCollide = true;   //make that the projectile will be destroed if it hits the terrain
			Projectile.penetrate = 1;      //how many NPC will penetrate
			Projectile.timeLeft = 2000;   //how many time this projectile has before disepire
			Projectile.light = 1.75f;    // projectile light
			Projectile.scale = 1.5f;
			Projectile.extraUpdates = 1;
			Projectile.ignoreWater = true;
			AIType = ProjectileID.TerraBeam;
		}
		public override void AI()
		{

			{
				int explosionRadius = 3;
				//if (Projectile.type == 29 || Projectile.type == 470 || Projectile.type == 637)
				{
					explosionRadius = 7;
				}
				int minTileX = (int)(Projectile.position.X / 16f - (float)explosionRadius);
				int maxTileX = (int)(Projectile.position.X / 16f + (float)explosionRadius);
				int minTileY = (int)(Projectile.position.Y / 16f - (float)explosionRadius);
				int maxTileY = (int)(Projectile.position.Y / 16f + (float)explosionRadius);
				if (minTileX < 0)
				{
					minTileX = 0;
				}
				if (maxTileX > Main.maxTilesX)
				{
					maxTileX = Main.maxTilesX;
				}
				if (minTileY < 0)
				{
					minTileY = 0;
				}
				if (maxTileY > Main.maxTilesY)
				{
					maxTileY = Main.maxTilesY;
				}
				bool canKillWalls = false;
				for (int x = minTileX; x <= maxTileX; x++)
				{
					for (int y = minTileY; y <= maxTileY; y++)
					{
						float diffX = Math.Abs((float)x - Projectile.position.X / 16f);
						float diffY = Math.Abs((float)y - Projectile.position.Y / 16f);
						double distance = Math.Sqrt((double)(diffX * diffX + diffY * diffY));
						if (distance < (double)explosionRadius && Main.tile[x, y] != null && Main.tile[x, y].wall == 0)
						{
							canKillWalls = true;
							break;
						}

					}
				}
			}

		}

		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			/*  Mod CalamityMod = ModLoader.GetMod("CalamityMod");
			 if (CalamityMod != null)

			 target.immune[Projectile.owner] = 0;

			  target.AddBuff(ModLoader.GetMod("CalamityMod").BuffType("GodSlayerInferno"), 400);
			  target.AddBuff(ModLoader.GetMod("CalamityMod").BuffType("Marked"), 400);
			  target.AddBuff(BuffID.OnFire, 1080);
			 target.AddBuff(BuffID.BrokenArmor, 1080);
			 target.AddBuff(BuffID.Slow, 1080);
			 target.AddBuff(BuffID.CursedInferno, 1080);
			 target.AddBuff(BuffID.Ichor, 1080);
		  }
		}
	}
}*/