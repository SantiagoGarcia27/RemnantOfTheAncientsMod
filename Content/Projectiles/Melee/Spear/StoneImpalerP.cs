using Microsoft.Xna.Framework;
using RemnantOfTheAncientsMod.Content.Projectiles.BossProjectiles.Gemstone;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Content.Projectiles.Melee.Spear
{
	public class StoneImpalerP : ModProjectile
	{
		// Define the range of the Spear Projectile. These are overridable properties, in case you'll want to make a class inheriting from this one.
		protected virtual float HoldoutRangeMin => 24f;
		protected virtual float HoldoutRangeMax => 96f;

		public override void SetDefaults() {
			Projectile.CloneDefaults(ProjectileID.Spear); // Clone the default values for a vanilla spear. Spear specific values set for width, height, aiStyle, friendly, penetrate, tileCollide, scale, hide, ownerHitCheck, and melee.
		}

		bool maxRangeReached = false;
        public override bool PreAI() {
			Player player = Main.player[Projectile.owner];
			int duration = player.itemAnimationMax;
			player.heldProj = Projectile.whoAmI;

			// Reset projectile time left if necessary
			if (Projectile.timeLeft > duration) {
				Projectile.timeLeft = duration;
			}

			Projectile.velocity = Vector2.Normalize(Projectile.velocity);

			float halfDuration = duration * 0.5f;
			float progress;

			if (Projectile.timeLeft < halfDuration) {
				progress = Projectile.timeLeft / halfDuration;
			}
			else {
				if(!maxRangeReached)
				{
					maxRangeReached = true;
					if (Main.rand.NextBool(3))
					{
						Vector2 center = player.MountedCenter + Projectile.velocity * HoldoutRangeMax;

                        int index = Projectile.NewProjectile(Projectile.GetSource_FromThis(), center, Vector2.Normalize(Projectile.velocity) * 10f, ModContent.ProjectileType<GemstoneCrusherProj_Diamond>(), Projectile.damage, Projectile.knockBack, Projectile.owner);
						Main.projectile[index].DamageType = DamageClass.Melee;
						Main.projectile[index].penetrate = 2;
                        Main.projectile[index].hostile = false;
						Main.projectile[index].friendly = true;
					}
                }
				progress = (duration - Projectile.timeLeft) / halfDuration;
			}

			// Move the projectile from the HoldoutRangeMin to the HoldoutRangeMax and back, using SmoothStep for easing the movement
			Projectile.Center = player.MountedCenter + Vector2.SmoothStep(Projectile.velocity * HoldoutRangeMin, Projectile.velocity * HoldoutRangeMax, progress);

			// Apply proper rotation to the sprite.
			if (Projectile.spriteDirection == -1) {
				// If sprite is facing left, rotate 45 degrees
				Projectile.rotation += MathHelper.ToRadians(45f);
			}
			else {
				// If sprite is facing right, rotate 135 degrees
				Projectile.rotation += MathHelper.ToRadians(135f);
			}

			// Avoid spawning dusts on dedicated servers
			if (!Main.dedServ) {
				// These dusts are added later, for the 'ExampleMod' effect
				/*if (Main.rand.NextBool(3)) {
					Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, ModContent.DustType<Sparkle>(), Projectile.velocity.X * 2f, Projectile.velocity.Y * 2f, Alpha: 128, Scale: 1.2f);
				}

				if (Main.rand.NextBool(4)) {
					Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, ModContent.DustType<Sparkle>(), Alpha: 128, Scale: 0.3f);
				}*/
			}

			return false; // Don't execute vanilla AI.
		}
	}
}
