using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Content.Projectiles
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

        public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough, ref Vector2 hitboxCenterFrac)
        {
            fallThrough = true;
            return base.TileCollideStyle(ref width, ref height, ref fallThrough, ref hitboxCenterFrac);
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

        public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough, ref Vector2 hitboxCenterFrac)
        {
            fallThrough = true;
            return base.TileCollideStyle(ref width, ref height, ref fallThrough, ref hitboxCenterFrac);
        }
    }
}
