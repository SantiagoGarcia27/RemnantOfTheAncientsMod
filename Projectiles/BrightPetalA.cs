using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Projectiles
{
    internal class BrightPetalA : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Bright Petal");
        }

        // falta añadir la variante en grayscale y el postprocessing 
        
        public override void SetDefaults()
        {
            Projectile.height = 8;
            Projectile.width = 12;
            Projectile.damage = 15;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.knockBack = 1.2f;
            Projectile.friendly = true;
            Projectile.tileCollide = true;
            Projectile.light = 1.5f;
            Projectile.timeLeft = 120;

        }
    }
}
