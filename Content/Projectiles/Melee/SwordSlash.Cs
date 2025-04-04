using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RemnantOfTheAncientsMod.Common.UtilsTweaks;
using RemnantOfTheAncientsMod.World;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Content.Projectiles.Melee
{
	public class SwordSlash : ModProjectile
	{
		public int FrameCount = 0;
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("God Slayer Slash"); //projectile name
			Main.projFrames[Projectile.type] = 1;
		}
		public override void SetDefaults()
		{
			Projectile.width = 46;       //projectile width
			Projectile.height = 50;  //projectile height
			Projectile.friendly = true;      //make that the projectile will not damage you
			Projectile.DamageType = DamageClass.Melee;          // 
			Projectile.tileCollide = false;   //make that the projectile will be destroed if it hits the terrain
			Projectile.penetrate = 20;      //how many NPC will penetrate
			Projectile.timeLeft = (int)Utils1.FormatTimeToTick(0,0,0,3); //how many time this projectile has before disepire
			Projectile.light =0.5f;    // projectile light
			Projectile.scale = 2f;
			Projectile.extraUpdates = 1;
			Projectile.ignoreWater = true;
			AIType = ProjectileID.TerraBeam;
			Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 10;
		}
		int blockCounter = 0;
		public override void AI()
		{
            Projectile.tileCollide = false;
            //Projectile.direction = Main.player[Main.myPlayer].direction;
            Projectile.spriteDirection = Projectile.direction;
			//SpriteEffects effects = Main.player[Main.myPlayer].direction == 1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
			Player player = Main.player[Projectile.owner];

			if (DistanceUtils.TouchWall(Projectile))
			{
				if (blockCounter % 2 == 0)
				{
					Projectile.damage -= 1;
				}
				blockCounter++;
            }
			if(!DistanceUtils.TouchFlour(Projectile))
			{
				Projectile.velocity.Y += 1;
			}
			else if(DistanceUtils.ExistTileSolid(Projectile.Center + new Vector2(0,1 *16)))
			{
                Projectile.velocity.Y -= 1;
            }
			else
			{
				Projectile.velocity.Y = 0;
            }

			if(Projectile.damage == 0)
			{
                Projectile.Kill();
			}
			AnimateTexture();

		}
		public int dir = 1;
		public override bool PreDraw(ref Color lightColor)
		{
			SpriteEffects effects = dir == 1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
			Texture2D texture2D = ModContent.Request<Texture2D>(Texture).Value;
			Vector2 posBonus = new Vector2(Projectile.width, Projectile.height) / (Reaper.ReaperMode ? 1 : 2);
			Main.spriteBatch.Draw(texture2D, Projectile.Center - Main.screenPosition + posBonus, null, lightColor, Projectile.rotation, new Vector2(Projectile.width / 2, Projectile.height / 2), Projectile.scale, effects, 0f);
			return false;
		}
        public override void OnSpawn(IEntitySource source)
        {
			dir = Main.player[Main.myPlayer].direction;
            Projectile.position -= new Vector2(0, 1)*16;
            base.OnSpawn(source);
        }
        public void AnimateTexture()
		{
			//switch (Projectile.frameCounter)
			//{
			//	case 0:
			//                 Projectile.frame = 0;
			//                 Projectile.frameCounter++;
			//		break;
			//	case 1:
			//                 Projectile.frame = 1;
			//		Projectile.frameCounter++;
			//                 break;
			//	case 2:
			//                 Projectile.frame = 2;
			//		Projectile.frameCounter++;
			//                 break;
			//	case 3:
			//                 Projectile.frame = 3;
			//		Projectile.frameCounter++;
			//                 break;
			//             case 4:
			//                 Projectile.frame = 4;
			//                 Projectile.frameCounter++;
			//                 break;
			//             case 5:
			//                 Projectile.frame = 5;
			//                 Projectile.frameCounter++;
			//                 break;
			//             case 6:
			//                 Projectile.frame = 6;
			//                 Projectile.frameCounter++;
			//                 break;
			//             case 7:
			//                 Projectile.frameCounter = 0;
			//		Projectile.Kill();
			//                 break; 
			//	default:
			//		Projectile.frameCounter++;
			//                 break;


			//         }

			//if (++Projectile.frameCounter >= 5)
			//{
			//	Projectile.frameCounter = 0;
			//	if (++Projectile.frame >= Main.projFrames[Projectile.type])
			//	{
			//		Projectile.frame = 0;
			//		Projectile.Kill();
			//	}
			//}
		}

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
		{
		}
	}
}
