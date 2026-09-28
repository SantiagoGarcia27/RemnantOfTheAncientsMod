using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RemnantOfTheAncientsMod.Content.Projectiles.BossProjectiles.Gemstone;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Content.Projectiles.Melee.Flail
{
	internal class RockAndRollProjectile : ModProjectile
	{
		public override void SetStaticDefaults() {
			ProjectileID.Sets.HeldProjDoesNotUsePlayerGfxOffY[Type] = true;
		}

		public override void SetDefaults() {
			Projectile.netImportant = true;
			Projectile.width = 22;
			Projectile.height = 22;
			Projectile.friendly = true;
			Projectile.penetrate = -1;
			Projectile.DamageType = DamageClass.Melee;
			Projectile.scale = 0.8f;
			Projectile.usesLocalNPCImmunity = true;
			Projectile.localNPCHitCooldown = 10;

			Projectile.aiStyle = ProjAIStyleID.Flail;
			AIType = ProjectileID.Mace;

			DrawOffsetX = -6;
			DrawOriginOffsetY = -6;
		}

		public override Color? GetAlpha(Color lightColor) {
			return Color.White;
		}

		public override bool PreDrawExtras() {
			Projectile.type = ProjectileID.Mace;
			return base.PreDrawExtras();
		}

		public override void FlailStats(ref int launchTimeLimit, ref float launchSpeed, ref float maxLaunchLength, ref float retractAcceleration, ref float maxRetractSpeed, ref float forcedRetractAcceleration, ref float maxForcedRetractSpeed, ref int ricochetTimeLimit, ref float spinVisualDistance) {
			spinVisualDistance += 10;
		}

		public override void FlailSpinCollisionRange(ref float range) {
			range += 10;
		}

		public override bool PreDraw(ref Color lightColor) {
			Projectile.type = ModContent.ProjectileType<RockAndRollProjectile>();

			if (Projectile.ai[0] == 1f) {
				Texture2D projectileTexture = TextureAssets.Projectile[Type].Value;
				Vector2 drawPosition = Projectile.position + new Vector2(Projectile.width, Projectile.height) / 2f + Vector2.UnitY * Projectile.gfxOffY - Main.screenPosition;
				Vector2 drawOrigin = new Vector2(projectileTexture.Width, projectileTexture.Height) / 2f;
				Color drawColor = Projectile.GetAlpha(lightColor);
				drawColor.A = 127;
				drawColor *= 0.5f;
				int launchTimer = (int)Projectile.ai[1];
				if (launchTimer > 5) {
					launchTimer = 5;
				}

				SpriteEffects spriteEffects = Projectile.spriteDirection == 1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;

				for (float transparency = 1f; transparency >= 0f; transparency -= 0.125f) {
					float opacity = 1f - transparency;
					Vector2 drawAdjustment = Projectile.velocity * -launchTimer * transparency;
					Main.EntitySpriteDraw(projectileTexture, drawPosition + drawAdjustment, null, drawColor * opacity, Projectile.rotation, drawOrigin, Projectile.scale * 1.15f * MathHelper.Lerp(0.5f, 1f, opacity), spriteEffects, 0);
				}
			}

			return base.PreDraw(ref lightColor);
		}

		public override void AI() {
			if (Main.myPlayer == Projectile.owner && Projectile.ai[0] == 2f && Projectile.ai[1] == 0f) {
				int index = Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, Projectile.velocity, ModContent.ProjectileType<GemstoneCrusherProj_Diamond>(), Projectile.damage / 2, Projectile.knockBack, Main.myPlayer);
				Projectile proj = Main.projectile[index];
				proj.friendly = true;
				proj.hostile = false;
				Projectile.ai[1]++;
			}
		}
	}
}
