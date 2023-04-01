using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Projectiles
{
    public class CloneStarWrath : ModProjectile
    {
        public override string Texture => "Terraria/Images/Projectile_" + ProjectileID.StarWrath;
        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.StarWrath);
            AIType = ProjectileID.StarWrath;
            Projectile.usesLocalNPCImmunity = true;
        }
    }
    public class CloneStarFury : ModProjectile
    {
        public override string Texture => "Terraria/Images/Projectile_" + ProjectileID.Starfury;
        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.Starfury);
            AIType = ProjectileID.Starfury;
            Projectile.usesLocalNPCImmunity = true;
        }
    }
    public class CloneFallingStar : ModProjectile
    {
        public override string Texture => "Terraria/Images/Projectile_" + ProjectileID.FallingStar;
        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.SuperStar);
            AIType = ProjectileID.SuperStar;
            Projectile.usesLocalNPCImmunity = true;
        }
    }
}
