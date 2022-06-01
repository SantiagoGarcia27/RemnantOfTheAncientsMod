using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using opswordsII.Dusts;
using static Terraria.ModLoader.ModContent;


namespace opswordsII.Projectiles
{

    public class Flare1 : ModProjectile
    {
        public override string Texture => "Terraria/Projectile_" + ProjectileID.Flare;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Flare1"); //projectile name
        }
            public override void SetDefaults()   
        {    
            Projectile.CloneDefaults(ProjectileID.Flare);
            Projectile.timeLeft = 0; 
              AIType = ProjectileID.Flare; 
              
             
        }
       public override void Kill(int timeLeft) {

            }
    }            
}
