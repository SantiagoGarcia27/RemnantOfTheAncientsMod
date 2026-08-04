using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RemnantOfTheAncientsMod.Common.UtilsTweaks;
using RemnantOfTheAncientsMod.Content.Buffs.Debuff;
using RemnantOfTheAncientsMod.Content.Dusts;
using Terraria;
using Terraria.Audio;
using Terraria.Enums;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace RemnantOfTheAncientsMod.Content.Projectiles.Mage.Lazer
{
    public class Judment_Lasser_Shooter : ModProjectile
	{
        public override string Texture => RemnantOfTheAncientsMod.PlaceHolderPath;
        public override void SetDefaults()
        {
            Projectile.width = 22;
            Projectile.height = 2;
            Projectile.aiStyle = 1;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.penetrate = 500;
            Projectile.timeLeft = 30;
            Projectile.alpha = 255;
            Projectile.light = 1.5f;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.extraUpdates = 1;
            AIType = -1;
        }
        public override void AI()
        {
			if (RemnantOfTheAncientsMod.ParticleMeter(3) != 0)
			{
				var dust = Dust.NewDust(Projectile.position, 95, 95, DustType<HollyBurn_P>(), 0, 0, 0, default, 8 + Projectile.scale * 1.5f);
				Main.dust[dust].noGravity = true;
			}
			Vector2 velocity = new Vector2(0, 20);
			velocity.Normalize();
            Projectile p = Projectile.NewProjectileDirect(Projectile.GetSource_FromAI(), Projectile.position, velocity, ModContent.ProjectileType<Judment_Lasser>(), Projectile.damage, Projectile.knockBack, Projectile.owner, Projectile.ai[1]);
			p.scale = Projectile.scale;
			Projectile.velocity = Vector2.Zero;
        }
    }

    public class Judment_Lasser : ModProjectile
	{
		public float Distance
		{
			get => Projectile.ai[0];
			set => Projectile.ai[0] = value;
		}

		public override void SetDefaults()
		{
			Projectile.width = 20;
			Projectile.height = 20;
			Projectile.friendly = true;
			Projectile.DamageType = DamageClass.Magic;
			Projectile.tileCollide = false;
			Projectile.penetrate = -1;
			Projectile.timeLeft = 30; 
			Projectile.light = 1.75f;   
			Projectile.ignoreWater = true;
			Projectile.scale = 2f;
			//Projectile.hide = true;

		}

		public override bool PreDraw(ref Color lightColor)
		{
			SpriteBatch spriteBatch = Main.spriteBatch;
			Texture2D texture = TextureAssets.Projectile[Projectile.type].Value;

			// Glow pass - additive blending for the outer radiance
			spriteBatch.End();
			spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Additive, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, null, Main.GameViewMatrix.TransformationMatrix);

			DrawLaser(texture, Projectile.Center, Projectile.velocity, 10, -1.57f, Projectile.scale, 1, true);

			// Normal pass - body and core
			spriteBatch.End();
			spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, null, Main.GameViewMatrix.TransformationMatrix);

			DrawLaser(texture, Projectile.Center, Projectile.velocity, 10, -1.57f, Projectile.scale, 1, false);

			return false;
		}

		public float floor;
		public void DrawLaser(Texture2D texture, Vector2 start, Vector2 unit, float step, float rotation, float scale, int transDist, bool isGlow)
		{
			float r = unit.ToRotation() + rotation;
			float time = Main.GlobalTimeWrappedHourly;
			float pulse = 0.85f + 0.15f * (float)Math.Sin(time * 10f);
			float glowPulse = 0.7f + 0.3f * (float)Math.Sin(time * 14f);

			Rectangle bodyRect = new(0, 26, 28, 26);
			Rectangle endRect = new(0, 57, 26, 22);
			Vector2 texOrigin = new(28 * 0.5f, 26 * 0.5f);

			for (float i = transDist; i <= Distance; i += step)
			{
				Vector2 drawPos = start + i * unit - Main.screenPosition;
				bool hitSolid = DistanceUtils.ExistTileSolid(new Vector2(start.X, start.Y + i), true);

				if (isGlow)
				{
					if (hitSolid)
					{
						Color glowColor = new Color(255, 200, 50) * 0.4f * glowPulse;
						Main.EntitySpriteDraw(texture, drawPos, endRect, glowColor, r, texOrigin, scale * 3f * glowPulse, 0, 0);
						floor = i;
						break;
					}
					else
					{
						float mult = HaveBlockMultiplier(start.X, start.Y + i);
						Color glowColor = new Color(255, 200, 50) * 0.3f * glowPulse;
						Main.EntitySpriteDraw(texture, drawPos, bodyRect, glowColor, r, texOrigin, scale * mult * 2.2f * glowPulse, 0, 0);
					}
				}
				else
				{
					if (hitSolid)
					{
						if (RemnantOfTheAncientsMod.ParticleMeterChoice())
						{
							var dust = Dust.NewDust(new Vector2(start.X - 2 * 16, start.Y + i), Projectile.width * 2, 25, DustType<HollyBurn_P>(), 0, 0, 0, default, 1 + Projectile.scale * 2f);
							Main.dust[dust].noGravity = true;
						}
						Main.EntitySpriteDraw(texture, drawPos, endRect, Color.White, r, texOrigin, scale * 2, 0, 0);
						Main.EntitySpriteDraw(texture, drawPos, endRect, Color.White * 0.9f, r, texOrigin, scale * 0.8f * pulse, 0, 0);
						floor = i;
						break;
					}

					else
					{
						float mult = HaveBlockMultiplier(start.X, start.Y + i);
						Main.EntitySpriteDraw(texture, drawPos, bodyRect, Color.White, r, texOrigin, scale * mult, 0, 0);
						Main.EntitySpriteDraw(texture, drawPos, bodyRect, Color.White * 0.85f * pulse, r, texOrigin, scale * mult * 0.5f, 0, 0);

						int dustChance = RemnantOfTheAncientsMod.ParticleMeter(45, 90, 180, 0);
						if (dustChance > 0 && Main.rand.NextBool(dustChance))
						{
							var d = Dust.NewDustPerfect(new Vector2(start.X + Main.rand.NextFloat(-16, 16), start.Y + i), DustType<HollyBurn_P>(), new Vector2(Main.rand.NextFloat(-1f, 1f), Main.rand.NextFloat(-0.5f, 0.5f)), 0, default, 1f + Projectile.scale);
							d.noGravity = true;
						}
					}
				}
			}
		}
		public float HaveBlockMultiplier(float x, float y)
		{
			float a = 1;
			for (int i = 0; i < 10; i++)
			{
				if (DistanceUtils.ExistTileSolid(new Vector2(x, y + i * 10)))
				{
					return a += i / 100;
				}
			}

			return a;
		}

		public override void OnKill(int timeLeft)
		{
			//SoundEngine.PlaySound(SoundID.Item27, Projectile.Center);
			Vector2 usePos = Projectile.position;
			Vector2 rotVector = (Projectile.rotation - MathHelper.ToRadians(45f)).ToRotationVector2();
			usePos += rotVector * 1f;
		}

		public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
		{
			float point = 0f;
			return Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size()* Projectile.scale, Projectile.Center, Projectile.Center + Projectile.velocity * Distance, 22, ref point);//player.Center
		}

		public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
		{
			target.AddBuff(BuffType<HollyBurn>(), 400);
			target.immune[Projectile.owner] = 5;
			Projectile.damage = (int)(Projectile.damage * 0.9f);
		}

		public override void AI()
		{
			Player player = Main.player[Projectile.owner];

			UpdatePlayer(player);
			CastLights();
		}
		private void UpdatePlayer(Player player)
		{
			if (Projectile.owner == Main.myPlayer)
			{
				Projectile.netUpdate = true;
			}	
			player.heldProj = Projectile.whoAmI;
			//player.itemTime = 2;
			//player.itemAnimation = 2;		
		}

		private void CastLights()
		{
			DelegateMethods.v3_1 = new Vector3(0.8f, 0.8f, 1f);
			Utils.PlotTileLine(Projectile.Center, Projectile.Center + Projectile.velocity * (floor/*Distance*/ - 1f), 26, DelegateMethods.CastLight);
		}

		public override bool ShouldUpdatePosition() => false;

		public override void CutTiles()
		{
			DelegateMethods.tilecut_0 = TileCuttingContext.AttackProjectile;
			Vector2 unit = Projectile.velocity;
			Utils.PlotTileLine(Projectile.Center, Projectile.Center + unit * Distance, (Projectile.width + 16) * Projectile.scale, DelegateMethods.CutTiles);
		}
        public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough, ref Vector2 hitboxCenterFrac)
        {
			fallThrough = true;
            return base.TileCollideStyle(ref width, ref height, ref fallThrough, ref hitboxCenterFrac);
        }

    }
}
