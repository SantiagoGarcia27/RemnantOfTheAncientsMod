using Microsoft.Xna.Framework;
using RemnantOfTheAncientsMod.Content.Buffs.Buffs.Pets;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Content.Projectiles.Pets
{
	public class DakoPetProjectile : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			Main.projFrames[Projectile.type] = 1;
			Main.projPet[Projectile.type] = true;
		}

		public override void SetDefaults()
		{
			Projectile.CloneDefaults(ProjectileID.ZephyrFish);
			AIType = ProjectileID.ZephyrFish;
			Projectile.scale = 1f;
		}

		public override bool PreAI()
		{
			Player player = Main.player[Projectile.owner];
			player.zephyrfish = false; // Relic from AIType
			return true;
		}

		public override void AI()
		{
			Player player = Main.player[Projectile.owner];

			if (!player.dead && player.HasBuff(ModContent.BuffType<DakoVanityBuff>()))
			{
				Projectile.timeLeft = 2;
			}
		}
		public override void PostDraw(Color lightColor)
		{
			lightColor = Color.Red;
			base.PostDraw(lightColor);
		}
		public override bool PreDraw(ref Color lightColor)
		{
			if (Projectile.ai[1] != -1)
				lightColor = Main.DiscoColor;
			else
			{
				Color c = Color.Red;
				lightColor = c;
			}
			return true;
		}
	}
}