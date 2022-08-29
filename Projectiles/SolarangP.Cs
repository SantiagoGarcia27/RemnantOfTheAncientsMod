using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using RemnantOfTheAncientsMod.Dusts;
using static Terraria.ModLoader.ModContent;


namespace RemnantOfTheAncientsMod.Projectiles
{

    public class SolarangP : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("SolarangP"); //projectile name
          //  ProjectileID.Sets.CountsAsHoming[Projectile.type] = true;
        }
            public override void SetDefaults()   
        {    
            AIType = ProjectileID.Flamarang;
            Projectile.CloneDefaults(ProjectileID.	Flamarang);
            Projectile.scale = 1.5f;
       
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit) {
			if (Main.rand.NextBool()) {
				target.AddBuff(BuffID.Daybreak, 300);
			}
        }
    }
}