using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RemnantOfTheAncientsMod.Buffs.Debuff;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.Enums;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace RemnantOfTheAncientsMod.Projectiles.Lazer
{

	/*public class HollyLaser : ModProjectile
	{
        private Vector2 laserStart;
        public override void SetDefaults()
        {
            Projectile.width = 4;
            Projectile.height = 4;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 600;
            Projectile.tileCollide = false;
            laserStart = Projectile.position;
        }
        public override void AI()
        {
            if (Projectile.position.Y + Projectile.height >= Main.worldSurface * 16)
            {
                Projectile.Kill();
            }

            if (Projectile.tileCollide)
            {
                // Crear láser con partículas
                Vector2 laserEnd = Projectile.position;
                int laserLength = (int)Vector2.Distance(laserStart, laserEnd);
                for (int i = 0; i < laserLength; i += 4)
                {
                    Dust dust = Dust.NewDustDirect(Vector2.Lerp(laserStart, laserEnd, i / (float)laserLength), 1, 1, DustType<Dusts.HollyBurn_P>(), 0, 0, 0, Color.White, 1f);
                    dust.noGravity = true;
                    dust.fadeIn = 1f;
                    dust.scale *= 2f;
                }
                Projectile.Kill();
            }
        }
        public override bool PreDraw(ref Color lightColor)
        { 
            return false;
        }

        public override void PostDraw(Color lightColor)     
        {
            var laserTexture = Request<Texture2D>("RemnantOfTheAncientsMod/Projectiles/HollyLaser");
            Rectangle laserRect = new Rectangle(0, 0, 2, (int)Projectile.Distance(laserStart));
            Vector2 laserOrigin = new Vector2(laserRect.Width / 2f, 0f);

            Vector2 laserDirection = Projectile.DirectionTo(laserStart);
            Main.spriteBatch.Draw((Texture2D)laserTexture, Projectile.Center - Main.screenPosition, laserRect, default(Color),laserDirection.ToRotation(), laserOrigin, 1f, SpriteEffects.None, 0f);
//Main.spriteBatch.Draw((Texture2D)a, position, NPC.frame, color, NPC.rotation, vector2, NPC.scale, effects, 0f);
		}
	}
}*/

	public class HollyLaser : ModProjectile
	{
		// The actual distance is stored in the ai0 field
		// By making a property to handle this it makes our life easier, and the accessibility more readable
		public float Distance
		{
			get => Projectile.ai[0];
			set => Projectile.ai[0] = value;
		}

		// The actual charge value is stored in the localAI0 field
		public float Charge
		{
			get => Projectile.localAI[0];
			set => Projectile.localAI[0] = value;
		}

		// Are we at max charge? With c#6 you can simply use => which indicates this is a get only property
		public bool IsAtMaxCharge => Charge == 1f;

		public override void SetDefaults()
		{
			Projectile.width = 20;
			Projectile.height = 20;
			Projectile.friendly = true;
			Projectile.DamageType = DamageClass.Magic;
			Projectile.tileCollide = false;
			Projectile.penetrate = -1;
			// Projectile.timeLeft = 20000;   //how many time this projectile has before disepire
			Projectile.light = 1.75f;    // projectile light
			Projectile.ignoreWater = true;
			Projectile.scale = 2f;
			Projectile.hide = true;
			//aiType = ProjectileID.InfluxWaver;

		}

		public override bool PreDraw(ref Color lightColor)
		{
			DrawLaser(TextureAssets.Projectile[Projectile.type].Value, Projectile.Center, Projectile.velocity, 10, Projectile.damage, -1.57f, 1f, 1000f, Color.White, 1);
			return false;
		}

		public void DrawLaser(Texture2D texture, Vector2 start, Vector2 unit, float step, int damage, float rotation = 0f, float scale = 2f, float maxDist = 2000f, Color color = default(Color), int transDist = 50)
		{
			float r = unit.ToRotation() + rotation;
			for (float i = transDist; i <= Distance; i += step)
			{
				var origin = start + i * unit;
				Main.EntitySpriteDraw(texture, origin - Main.screenPosition, new Rectangle(0, 26, 28, 26), i < transDist ? Color.Transparent : Color.White, r, new Vector2(28 * .5f, 26 * .5f), scale, 0, 0);
			}
			Main.EntitySpriteDraw(texture, start + unit * (transDist - step) - Main.screenPosition, new Rectangle(0, 0, 28, 26), Color.White, r, new Vector2(28 * .5f, 26 * .5f), scale, 0, 0);
			Main.EntitySpriteDraw(texture, start + (Distance + step) * unit - Main.screenPosition, new Rectangle(0, 52, 28, 26), Color.White, r, new Vector2(28 * .5f, 26 * .5f), scale, 0, 0);
		}


		public override void Kill(int timeLeft)
		{
			SoundEngine.PlaySound(SoundID.Item27, Projectile.Center);
			Vector2 usePos = Projectile.position;
			Vector2 rotVector = (Projectile.rotation - MathHelper.ToRadians(45f)).ToRotationVector2();
			usePos += rotVector * 1f;
		}

		public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
		{
			if (!IsAtMaxCharge) return false;
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
			//Projectile.position = player.Center + Projectile.velocity * MOVE_DISTANCE;
			Projectile.timeLeft = 2;

			UpdatePlayer(player);
			ChargeLaser(player);

			if (Charge < 1f) return;

			SetLaserPosition(player);
			//SpawnDusts(player);
			CastLights();
		}
		/*private void SpawnDusts(Player player)
		{
Vector2 unit = Projectile.velocity * -1;
Vector2 dustPos = player.Center + Projectile.velocity * Distance;

for (int i = 0; i < 2; ++i) 
{
	float num1 = Projectile.velocity.ToRotation() + (Main.rand.NextBool(2)? -1.0f : 1.0f) * 1.57f;
	float num2 = (float)(Main.rand.NextDouble() * 0.8f + 1.0f);
	Vector2 dustVel = new Vector2((float)Math.Cos(num1) * num2, (float)Math.Sin(num1) * num2);
	Dust dust = Main.dust[Dust.NewDust(dustPos, 0, 0, DustID.Electric, dustVel.X, dustVel.Y)];
	dust.noGravity = true;
	dust.scale = 1.2f;
	dust = Dust.NewDustDirect(Main.player[Projectile.owner].Center, 0, 0, DustID.Smoke, -unit.X * Distance, -unit.Y * Distance);
	dust.fadeIn = 0f;
	dust.noGravity = true;
	dust.scale = 0.88f;
	dust.color = Color.Cyan;
}

if (Main.rand.NextBool(5)) 
{
	Vector2 offset = Projectile.velocity.RotatedBy(1.57f) * ((float)Main.rand.NextDouble() - 0.5f) * Projectile.width;
	Dust dust = Main.dust[Dust.NewDust(dustPos + offset - Vector2.One * 4f, 8, 8, DustID.Smoke, 0.0f, 0.0f, 100, new Color(), 1.5f)];
	dust.velocity *= 0.5f;
	dust.velocity.Y = -Math.Abs(dust.velocity.Y);
	unit = dustPos - Main.player[Projectile.owner].Center;
	unit.Normalize();
	dust = Main.dust[Dust.NewDust(Main.player[Projectile.owner].Center + 55 * unit, 8, 8, DustID.Smoke, 0.0f, 0.0f, 100, new Color(), 1.5f)];
	dust.velocity = dust.velocity * 0.5f;
	dust.velocity.Y = -Math.Abs(dust.velocity.Y);
}
	}*/

		private void SetLaserPosition(Player player)
		{
			Vector2 target = new Vector2(Main.mouseX, Main.mouseY - (40 * 16)); //Main.screenPosition +

			for (Distance = 1f; Distance <= 1200f; Distance += 5f)//7200
			{
				var start = Projectile.Center + Projectile.velocity * Distance;//player
				if (!Collision.CanHit(target, 1, 1, start, 1, 1))
				{
					Distance -= 1f;
					break;
				}
			}
		}

		private void ChargeLaser(Player player)
		{
			if (!player.channel)
			{
				Projectile.Kill();
			}
			else
			{
				if (Main.time % 10 < 1 && !player.CheckMana(player.inventory[player.selectedItem].mana, true)) Projectile.Kill();

				Vector2 offset = Projectile.velocity;
				offset *= 1f - 20;
				Vector2 pos = Projectile.Center + offset - new Vector2(10, 10);//player.Center
				if (Charge < 1f) Charge++;

				int chargeFact = (int)(Charge / 20f);
				Vector2 dustVelocity = Vector2.UnitX * 18f;
				dustVelocity = dustVelocity.RotatedBy(Projectile.rotation - 1.57f);
				Vector2 spawnPos = Projectile.Center + dustVelocity;
				for (int k = 0; k < chargeFact + 1; k++)
				{
					Vector2 spawn = spawnPos + ((float)Main.rand.NextDouble() * 6.28f).ToRotationVector2() * (12f - chargeFact * 2);
					Dust dust = Main.dust[Dust.NewDust(pos, 20, 20, DustID.Electric, Projectile.velocity.X / 2f, Projectile.velocity.Y / 2f)];
					dust.velocity = Vector2.Normalize(spawnPos - spawn) * 1.5f * (10f - chargeFact * 2f) / 10f;
					dust.noGravity = true;
					dust.scale = Main.rand.Next(10, 20) * 0.05f;
				}
			}
		}

		private void UpdatePlayer(Player player)
		{
			if (Projectile.owner == Main.myPlayer)
			{
				Vector2 diff = Main.MouseWorld - player.Center;
				diff.Normalize();
				Projectile.velocity = diff;
				Projectile.direction = Main.MouseWorld.X > player.position.X ? 1 : -1; // 1: -1
				Projectile.netUpdate = true;
			}

			int dir = Projectile.direction;
			player.ChangeDir(dir); // Set player direction to where we are shooting
			player.heldProj = Projectile.whoAmI; // Update player's held projectile
			player.itemTime = 2; // Set item time to 2 frames while we are used
			player.itemAnimation = 2; // Set item animation time to 2 frames while we are used
			player.itemRotation = (float)Math.Atan2(Projectile.velocity.Y * dir, Projectile.velocity.X * dir); // Set the item rotation to where we are shooting
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
