using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using RemnantOfTheAncientsMod.Buffs;

namespace RemnantOfTheAncientsMod.Projectiles.Minioms
{
	public class DesertMinion : ModProjectile
	{
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("Baby Desert Aniquilator Minion");		
			Main.projFrames[Projectile.type] = 6;
			ProjectileID.Sets.MinionTargettingFeature[Projectile.type] = true;
			Main.projPet[Projectile.type] = true;
			ProjectileID.Sets.MinionSacrificable[Projectile.type] = true;
			//ProjectileID.Sets.CountsAsHoming[Projectile.type] = true;
			AIType = ProjectileID.BabySlime;
		}

		public sealed override void SetDefaults() {
			Projectile.width = 10;
			Projectile.height = 20;
			Projectile.tileCollide = true;
			Projectile.friendly = true;
			Projectile.minion = true;
			Projectile.minionSlots = 1f;
			Projectile.penetrate = -1;
			AIType = ProjectileID.BabySlime;
			Projectile.CloneDefaults(ProjectileID.BabySlime);
		}

		
		public override bool? CanCutTiles() {
			return false;
		}

		public override bool MinionContactDamage() {
			return true;
		}

		public override void AI() {
			Player player = Main.player[Projectile.owner];

			#region Active check
			// This is the "active check", makes sure the minion is alive while the player is alive, and despawns if not
			if (player.dead || !player.active) {
				player.ClearBuff(BuffType<DesertMinionBuff>());
			}
			if (player.HasBuff(BuffType<DesertMinionBuff>())) {
				Projectile.timeLeft = 2;
			}
			#endregion
		}
			   public override void OnHitNPC(NPC target, int damage, float knockback, bool crit) {
			if (Main.rand.NextBool()) {
				target.AddBuff(BuffType<Buffs.Burning_Sand>(), 300);
			
		  }
		}
	}
}