using System;
using Terraria;
using Terraria.ModLoader;

namespace opswordsII.NPCs
{
	//ported from my tAPI mod because I'm lazy
	public abstract class Hover : ModNPC
	{
		protected float speed = 2f;
		protected float acceleration = 0.1f;
		protected float speedY = 1.5f;
		protected float accelerationY = 0.04f;

		public override void AI() {
			if (!ShouldMove(NPC.ai[3])) {
				CustomBehavior(ref NPC.ai[3]);
				return;
			}
			bool flag33 = false;
			if (NPC.justHit) {
				NPC.ai[2] = 0f;
			}
			if (NPC.ai[2] >= 0f) {
				int num379 = 16;
				bool flag34 = false;
				bool flag35 = false;
				if (NPC.position.X > NPC.ai[0] - (float)num379 && NPC.position.X < NPC.ai[0] + (float)num379) {
					flag34 = true;
				}
				else if (NPC.velocity.X < 0f && NPC.direction > 0 || NPC.velocity.X > 0f && NPC.direction < 0) {
					flag34 = true;
				}
				num379 += 24;
				if (NPC.position.Y > NPC.ai[1] - (float)num379 && NPC.position.Y < NPC.ai[1] + (float)num379) {
					flag35 = true;
				}
				if (flag34 && flag35) {
					NPC.ai[2] += 1f;
					if (NPC.ai[2] >= 30f && num379 == 16) {
						flag33 = true;
					}
					if (NPC.ai[2] >= 60f) {
						NPC.ai[2] = -200f;
						NPC.direction *= -1;
						NPC.velocity.X *= -1f;
						NPC.collideX = false;
					}
				}
				else {
					NPC.ai[0] = NPC.position.X;
					NPC.ai[1] = NPC.position.Y;
					NPC.ai[2] = 0f;
				}
				NPC.TargetClosest(true);
			}
			else {
				NPC.ai[2] += 1f;
				if (Main.player[NPC.target].position.X + Main.player[NPC.target].width / 2 > NPC.position.X + (float)(NPC.width / 2)) {
					NPC.direction = -1;
				}
				else {
					NPC.direction = 1;
				}
			}
			int num380 = (int)((NPC.position.X + NPC.width / 2) / 16f) + NPC.direction * 2;
			int num381 = (int)((NPC.position.Y + NPC.height) / 16f);
			bool flag36 = true;
			bool flag37 = false;
			int num382 = 3;
			for (int num404 = num381; num404 < num381 + num382; num404++) {
				if (Main.tile[num380, num404] == null) {
				//	Main.tile[num380, num404] = new Tile();
				}
				/*if (Main.tile[num380, num404].nactive() && Main.tileSolid[(int)Main.tile[num380, num404].type] || Main.tile[num380, num404].LiquidAmount > 0) {
					if (num404 <= num381 + 1) {
						flag37 = true;
					}
					flag36 = false;
					break;
				}*/
			}
			if (flag33) {
				flag37 = false;
				flag36 = true;
			}
			if (flag36) {
				NPC.velocity.Y += Math.Max(0.2f, 2.5f * accelerationY);
				if (NPC.velocity.Y > Math.Max(2f, speedY)) {
					NPC.velocity.Y = Math.Max(2f, speedY);
				}
			}
			else {
				if (NPC.directionY < 0 && NPC.velocity.Y > 0f || flag37) {
					NPC.velocity.Y -= 0.2f;
				}
				if (NPC.velocity.Y < -4f) {
					NPC.velocity.Y = -4f;
				}
			}
			if (NPC.collideX) {
				NPC.velocity.X = NPC.oldVelocity.X * -0.4f;
				if (NPC.direction == -1 && NPC.velocity.X > 0f && NPC.velocity.X < 1f) {
					NPC.velocity.X = 1f;
				}
				if (NPC.direction == 1 && NPC.velocity.X < 0f && NPC.velocity.X > -1f) {
					NPC.velocity.X = -1f;
				}
			}
			if (NPC.collideY) {
				NPC.velocity.Y = NPC.oldVelocity.Y * -0.25f;
				if (NPC.velocity.Y > 0f && NPC.velocity.Y < 1f) {
					NPC.velocity.Y = 1f;
				}
				if (NPC.velocity.Y < 0f && NPC.velocity.Y > -1f) {
					NPC.velocity.Y = -1f;
				}
			}
			if (NPC.direction == -1 && NPC.velocity.X > -speed) {
				NPC.velocity.X -= acceleration;
				if (NPC.velocity.X > speed) {
					NPC.velocity.X -= acceleration;
				}
				else if (NPC.velocity.X > 0f) {
					NPC.velocity.X += acceleration / 2f;
				}
				if (NPC.velocity.X < -speed) {
					NPC.velocity.X = -speed;
				}
			}
			else if (NPC.direction == 1 && NPC.velocity.X < speed) {
				NPC.velocity.X += acceleration;
				if (NPC.velocity.X < -speed) {
					NPC.velocity.X += acceleration;
				}
				else if (NPC.velocity.X < 0f) {
					NPC.velocity.X -= acceleration / 2f;
				}
				if (NPC.velocity.X > speed) {
					NPC.velocity.X = speed;
				}
			}
			if (NPC.directionY == -1 && (double)NPC.velocity.Y > -speedY) {
				NPC.velocity.Y -= accelerationY;
				if ((double)NPC.velocity.Y > speedY) {
					NPC.velocity.Y -= accelerationY * 1.25f;
				}
				else if (NPC.velocity.Y > 0f) {
					NPC.velocity.Y += accelerationY * 0.75f;
				}
				if ((double)NPC.velocity.Y < -speedY) {
					NPC.velocity.Y = -speedY;
				}
			}
			else if (NPC.directionY == 1 && (double)NPC.velocity.Y < speedY) {
				NPC.velocity.Y += accelerationY;
				if ((double)NPC.velocity.Y < -speedY) {
					NPC.velocity.Y += accelerationY * 1.25f;
				}
				else if (NPC.velocity.Y < 0f) {
					NPC.velocity.Y -= accelerationY * 0.75f;
				}
				if ((double)NPC.velocity.Y > speedY) {
					NPC.velocity.Y = speedY;
				}
			}
			CustomBehavior(ref NPC.ai[3]);
		}

		public virtual void CustomBehavior(ref float ai) {
		}

		public virtual bool ShouldMove(float ai) {
			return true;
		}
	}
}