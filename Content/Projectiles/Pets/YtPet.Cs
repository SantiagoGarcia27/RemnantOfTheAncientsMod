using RemnantOfTheAncientsMod.Content.Buffs.Buffs.Pets;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Content.Projectiles.Pets
{
	public class YtPet : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			Main.projFrames[Projectile.type] = 4;
			//Main.projPet[Projectile.type] = true;
			ProjectileID.Sets.TrailingMode[Projectile.type] = 2;
			ProjectileID.Sets.LightPet[Projectile.type] = true;
		}

		public override void SetDefaults()
		{
			Projectile.CloneDefaults(ProjectileID.SuspiciousTentacle);
			AIType = ProjectileID.SuspiciousTentacle;
			Projectile.scale = 1f;
			Projectile.light = 1.25f;
		}

		public override bool PreAI()
		{
			Player player = Main.player[Projectile.owner];
			player.suspiciouslookingTentacle = false;
			return true;
		}

		public override void AI()
		{
			Player player = Main.player[Projectile.owner];

			if (!player.dead && player.HasBuff(ModContent.BuffType<YtBuff>()))
			{
				Projectile.timeLeft = 2;
			}
			Projectile.alpha = 100;
		}
	}
}