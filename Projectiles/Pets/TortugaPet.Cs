using RemnantOfTheAncientsMod.Buffs;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Projectiles.Pets
{
	public class TortugaPet : ModProjectile
	{
		public override void SetStaticDefaults() 
		{
Main.projFrames[Projectile.type] = 5;
Main.projPet[Projectile.type] = true;
		}

		public override void SetDefaults() 
		{
Projectile.CloneDefaults(ProjectileID.ZephyrFish);
AIType = ProjectileID.ZephyrFish;
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

            if (!player.dead && player.HasBuff(ModContent.BuffType<TortugaPetBuff>()))
            {
                Projectile.timeLeft = 2;
            }
        }
	}
}