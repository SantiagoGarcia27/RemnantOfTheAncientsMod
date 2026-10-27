using RemnantOfTheAncientsMod.Content.Buffs.Buffs.Pets;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Content.Projectiles.Pets
{
	public class FrozenPetProjectile : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			Main.projFrames[Projectile.type] = 1;
			Main.projPet[Projectile.type] = true;
		}

		public override void SetDefaults()
		{
			Projectile.CloneDefaults(ProjectileID.CompanionCube);
			Projectile.height = 28;
			Projectile.width = 28;
			AIType = ProjectileID.CompanionCube;
			Projectile.scale = 1f;
		}

		public override bool PreAI()
		{
			Player player = Main.player[Projectile.owner];
			player.companionCube = false;
			return true;
		}

		public override void AI()
		{
			Player player = Main.player[Projectile.owner];

			if (!player.dead && player.HasBuff(ModContent.BuffType<FrozenPetBuff>()))
			{
				Projectile.timeLeft = 2;
			}
		}
	}
}