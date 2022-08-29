using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using RemnantOfTheAncientsMod.Dusts;
using static Terraria.ModLoader.ModContent;
using Terraria.Audio;
using Terraria.GameContent;

namespace RemnantOfTheAncientsMod.Projectiles
{

    public class Curecedball : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Curecedball"); //projectile name
            
        }
            public override void SetDefaults()   
        {    
            Projectile.width = 36;       //projectile width
            Projectile.height = 36;  //projectile height
            Projectile.friendly = true;      //make that the projectile will not damage you
           Projectile.DamageType = DamageClass.Melee;          // 
            Projectile.tileCollide = true;   //make that the projectile will be destroed if it hits the terrain
            Projectile.penetrate = 1;      //how many NPC will penetrate
            Projectile.timeLeft = 200;   //how many time this projectile has before disepire
            Projectile.light = 0.75f;    // projectile light
            Projectile.extraUpdates = 1;
			Main.projFrames[Projectile.type] = 3;
            Projectile.ignoreWater = true; 
            Projectile.aiStyle = ProjectileID.BallofFire;
			Projectile.CloneDefaults(ProjectileID.BallofFire);

        }
		public override bool OnTileCollide(Vector2 oldVelocity)
		{
		Projectile.penetrate--;
			if (Projectile.penetrate <= 1)
			{
				Projectile.Kill();
			}
			else
			{
				Collision.HitTiles(Projectile.position, Projectile.velocity, Projectile.width, Projectile.height);
				SoundEngine.PlaySound(SoundID.Item10, Projectile.position);

				// If the projectile hits the left or right side of the tile, reverse the X velocity
				if (Math.Abs(Projectile.velocity.X - oldVelocity.X) > float.Epsilon)
				{
					Projectile.velocity.X = -oldVelocity.X;
				}

				// If the projectile hits the top or bottom side of the tile, reverse the Y velocity
				if (Math.Abs(Projectile.velocity.Y - oldVelocity.Y) > float.Epsilon)
				{
					Projectile.velocity.Y = -oldVelocity.Y;
				}
			}

			return false;
		}

		public override bool PreDraw(ref Color lightColor)
		{
			Main.instance.LoadProjectile(Projectile.type);
			Texture2D texture = TextureAssets.Projectile[Projectile.type].Value;

			// Redraw the projectile with the color not influenced by light
			Vector2 drawOrigin = new Vector2(texture.Width * 0.5f, Projectile.height * 0.5f);
			for (int k = 0; k < Projectile.oldPos.Length; k++)
			{
				Vector2 drawPos = (Projectile.oldPos[k] - Main.screenPosition) + drawOrigin + new Vector2(0f, Projectile.gfxOffY);
				Color color = Projectile.GetAlpha(lightColor) * ((Projectile.oldPos.Length - k) / (float)Projectile.oldPos.Length);
				Main.EntitySpriteDraw(texture, drawPos, null, color, Projectile.rotation, drawOrigin, Projectile.scale, SpriteEffects.None, 0);
			}

			return true;
		}
		public override void AI()
		{
			}
		
                   public override void OnHitNPC(NPC target, int damage, float knockback, bool crit) {
			if (Main.rand.NextBool()) {
				target.AddBuff(BuffType<Buffs.Burning_Sand>(), 300);
			}
        }
    }
  }
