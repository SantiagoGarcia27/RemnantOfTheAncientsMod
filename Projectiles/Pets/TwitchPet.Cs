using RemnantOfTheAncientsMod;
using RemnantOfTheAncientsMod.Buffs;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Projectiles.Pets
{
	public class TwitchPet : ModProjectile
	{
		public override void SetStaticDefaults() 
		{	
			Main.projFrames[Projectile.type] = 5;
            Main.projPet[Projectile.type] = true;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 2;
            ProjectileID.Sets.LightPet[Projectile.type] = true;
        }

		public override void SetDefaults() {
			Projectile.CloneDefaults(ProjectileID.ZephyrFish);
			AIType = ProjectileID.ZephyrFish;
			Projectile.light = 1.25f;
			Projectile.scale = 1.25f;
		}

		public override bool PreAI() {
			Player player = Main.player[Projectile.owner];
			player.zephyrfish = false; 
			return true;
		}

		public override void AI() {
            Player player = Main.player[Projectile.owner];

            if (!player.dead && player.HasBuff(ModContent.BuffType<YoutubeMoradoBuff>()))
            {
                Projectile.timeLeft = 2;
            }
        }
	}
}