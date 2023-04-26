using Terraria;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Projectiles
{

    public class EnchantedShield : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			////DisplayName.SetDefault("Curecedball"); //projectile name

		}
		public override void SetDefaults()
		{
            Projectile.width = 1;
            Projectile.height = 1;
            Projectile.friendly = true;
            Projectile.tileCollide = false;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 2000;
            Projectile.light = 1.0f;
            Projectile.extraUpdates = 1;
            Projectile.ignoreWater = true;
            AIType = -1;
            //Projectile.CloneDefaults(ProjectileID.BallofFire);

        }
		public override void AI()
		{
            Player player = Main.player[Main.myPlayer];
            Projectile.Center = player.Center;
        }
	}
}
