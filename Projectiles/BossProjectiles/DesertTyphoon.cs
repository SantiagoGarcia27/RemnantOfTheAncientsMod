using System;
using Microsoft.Xna.Framework;
using Terraria;
using RemnantOfTheAncientsMod.Dusts;
using static Terraria.ModLoader.ModContent;
using Terraria.Audio;
using Terraria.ID;

namespace RemnantOfTheAncientsMod.Projectiles.BossProjectile
{


    public class DesertTyphoon : DesertTyphoonModel
	{
		public override bool Friendly => false;
		public override bool Hostile => true;
		public override int PenetrateKill => 0;


		public override void AI()
		{
		/*	Projectile.rotation += Projectile.direction * 0.8f;
			if (Projectile.alpha > 70)
			{
				Projectile.alpha -= 15;
				if (Projectile.alpha < 70) Projectile.alpha = 70;
			}
			if (Projectile.localAI[0] == 0f)
			{
				AdjustMagnitude(ref Projectile.velocity);
				Projectile.localAI[0] = 10f;
			}
			Vector2 move = Vector2.Zero;
			float distance = 400f;
			bool target = false;
			for (int k = 0; k < 200; k++)
			{
				if (Main.npc[k].active && !Main.npc[k].dontTakeDamage && Main.npc[k].friendly && Main.npc[k].lifeMax > 5)
				{
					Vector2 newMove = Main.npc[k].Center - Projectile.Center;
					float distanceTo = (float)Math.Sqrt(newMove.X * newMove.X + newMove.Y * newMove.Y);
					if (distanceTo < distance)
					{
						move = newMove;
						distance = distanceTo;
						target = true;
					}
				}
				if (target)
				{
					AdjustMagnitude(ref move);
					Projectile.velocity = (10 * Projectile.velocity + move) / 11f;
					AdjustMagnitude(ref Projectile.velocity);
				}
				if (Projectile.alpha <= 100)
				{
					RemnantOfTheAncientsMod r = GetInstance<RemnantOfTheAncientsMod>();
					int NUM_DUSTS = r.ParticlleMetter(2);
					for (int i = 0; i < NUM_DUSTS; i++)
					{
						int dust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustType<QuemaduraA>());
						Main.dust[dust].velocity /= 0.5f;
					}
				}
			}*/
		}
		private void AdjustMagnitude(ref Vector2 vector)
		{
			float magnitude = (float)Math.Sqrt(vector.X * vector.X + vector.Y * vector.Y);
			if (magnitude > 106f) vector *= 26f / magnitude;
			
		}
		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			Projectile.penetrate--;
			if (Projectile.penetrate <= 0) Projectile.Kill();
			else
			{
				Collision.HitTiles(Projectile.position, Projectile.velocity, Projectile.width, Projectile.height);
				SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
				if (Math.Abs(Projectile.velocity.X - oldVelocity.X) > float.Epsilon) Projectile.velocity.X = -oldVelocity.X;
				if (Math.Abs(Projectile.velocity.Y - oldVelocity.Y) > float.Epsilon) Projectile.velocity.Y = -oldVelocity.Y;
			}
			return false;
		}
	}
}
