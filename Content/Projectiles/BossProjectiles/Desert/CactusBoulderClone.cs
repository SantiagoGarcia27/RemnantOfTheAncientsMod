using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Content.Projectiles.BossProjectile.Desert
{
    public class CactusBoulderClone : ModProjectile
    {
        public override string Texture => "Terraria/Images/Projectile_" + ProjectileID.RollingCactus;
        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.Boulder);
            AIType = ProjectileID.Boulder;
        }

        public override void AI()
        {
            Player owner = Main.player[Projectile.owner];
            if (Projectile.friendly && Projectile.ai[2] == 1 && Main.myPlayer == owner.whoAmI)
            {
                
                Vector2 MousePosition = new Vector2(Player.tileTargetX, Player.tileTargetY -1).ToWorldCoordinates();
                Projectile.Center = MousePosition;
            }
            base.AI();
        }
    }
}