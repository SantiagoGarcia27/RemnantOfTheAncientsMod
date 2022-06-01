using opswordsII;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace opswordsII.Projectiles.Pets
{
	public class TortugaPet : ModProjectile
	{
		public override void SetStaticDefaults() {
			// DisplayName.SetDefault("Paper Airplane"); // Automatic from .lang files
			Main.projFrames[Projectile.type] = 5;
			Main.projPet[Projectile.type] = true;
		}

		public override void SetDefaults() {
			Projectile.CloneDefaults(ProjectileID.ZephyrFish);
			AIType = ProjectileID.ZephyrFish;
		}

		public override bool PreAI() {
			Player player = Main.player[Projectile.owner];
			player.zephyrfish = false; // Relic from AIType
			return true;
		}

		public override void AI() {
			Player player = Main.player[Projectile.owner];
			Player1 modPlayer = player.GetModPlayer<Player1>();
			if (player.dead) {
				modPlayer.TortugaPet = false;
			}
			if (modPlayer.TortugaPet) {
				Projectile.timeLeft = 2;
			}
		}
	}
}