using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RemnantOfTheAncientsMod.Common.UtilsTweaks;
using RemnantOfTheAncientsMod.Content.Buffs.Debuff;
using RemnantOfTheAncientsMod.Content.Dusts;
using System;
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
			if (new RemnantOfTheAncientsMod().ParticleMeter(3) != 0)
			{
				var dust = Dust.NewDust(Projectile.position, 95, 95, ModContent.DustType<HollyBurn_P>(), 0, 0, 0, default, 8 + Projectile.scale * 1.5f);
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
			DrawLaser(TextureAssets.Projectile[Projectile.type].Value, Projectile.Center, Projectile.velocity, 10, Projectile.damage, -1.57f, Projectile.scale, 1000f, Color.White, 1);
			return false;
		}

		public void DrawLaser(Texture2D texture, Vector2 start, Vector2 unit, float step, int damage, float rotation = 0f, float scale = 2f, float maxDist = 2000f, Color color = default(Color), int transDist = 50)
		{
			float r = unit.ToRotation() + rotation;
			for (float i = transDist; i <= Distance; i += step)
			{
				var origin = start + i * unit;

				if (DistanceUtils.ExistTileSolid(new Vector2(start.X,start.Y + i)))
				{
					i = Distance -1;
                    Main.EntitySpriteDraw(texture, start + (Distance + step) * unit - Main.screenPosition, new Rectangle(0, 52, 28, 26), Color.White, r, new Vector2(28 * .5f, 26 * .5f), scale, 0, 0);
                }
				else
				{
					Main.EntitySpriteDraw(texture, origin - Main.screenPosition, new Rectangle(0, 26, 28, 26), i < transDist ? Color.Transparent : Color.White, r, new Vector2(28 * .5f, 26 * .5f), scale, 0, 0);
				}
            }
		}


		public override void OnKill(int timeLeft)
		{
			SoundEngine.PlaySound(SoundID.Item27, Projectile.Center);
			Vector2 usePos = Projectile.position;
			Vector2 rotVector = (Projectile.rotation - MathHelper.ToRadians(45f)).ToRotationVector2();
			usePos += rotVector * 1f;
		}

		public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
		{
			float point = 0f;
			return Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), Projectile.Center, Projectile.Center + Projectile.velocity * Distance, 22, ref point);//player.Center
		}

		 public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
		{
			target.AddBuff(BuffType<HollyBurn>(), 400);
			target.immune[Projectile.owner] = 5;
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
			Utils.PlotTileLine(Projectile.Center, Projectile.Center + Projectile.velocity * (Distance - 1f), 26, DelegateMethods.CastLight);
		}

		public override bool ShouldUpdatePosition() => false;

		public override void CutTiles()
		{
			DelegateMethods.tilecut_0 = TileCuttingContext.AttackProjectile;
			Vector2 unit = Projectile.velocity;
			Utils.PlotTileLine(Projectile.Center, Projectile.Center + unit * Distance, (Projectile.width + 16) * Projectile.scale, DelegateMethods.CutTiles);
		}

	}
}
