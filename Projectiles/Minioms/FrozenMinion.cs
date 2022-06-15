using Terraria;
using Terraria.ID;
using static Terraria.ModLoader.ModContent;
using opswordsII.Buffs;

namespace opswordsII.Projectiles.Minioms
{
    public class FrozenMinion : HoverShooter
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Baby Frozen Assaulter Minion");

			Main.projFrames[Projectile.type] = 6;
			Main.projPet[Projectile.type] = true;
			ProjectileID.Sets.MinionSacrificable[Projectile.type] = true;
			ProjectileID.Sets.MinionTargettingFeature[Projectile.type] = true; //This is necessary for right-click targeting
		}

		public override void SetDefaults()
		{
			Projectile.netImportant = true;
			Projectile.width = 24;
			Projectile.height = 32;
			Projectile.friendly = true;
			Projectile.minion = true;
			Projectile.minionSlots = 1;
			Projectile.penetrate = -1;
			Projectile.timeLeft = 18000;
			Projectile.tileCollide = false;
			Projectile.ignoreWater = true;
			inertia = 100f;//20
			shoot = ProjectileType<Frozenp_M>();
			shootSpeed = 12f; //12
			shootCool = 10f;
		}

		public override void CheckActive()
		{
			Player player = Main.player[Projectile.owner];
			Player1 modPlayer = player.GetModPlayer<Player1>();
			if (player.dead || !player.active)
			{
				player.ClearBuff(BuffType<FrozenMinionBuff>());
			}
			if (player.HasBuff(BuffType<FrozenMinionBuff>()))
			{
				Projectile.timeLeft = 2;
			}
			if (modPlayer.FrozenMinion)
			{ // Make sure you are resetting this bool in ModPlayer.ResetEffects. See ExamplePlayer.ResetEffects
				Projectile.timeLeft = 2;

			}
		}
		public override void SelectFrame()
		{
			Projectile.frameCounter++;
			if (Projectile.frameCounter >= 6)
			{
				Projectile.frameCounter = 0;
				Projectile.frame = (Projectile.frame + 1) % 4; //3
			}
		}
	}
}



