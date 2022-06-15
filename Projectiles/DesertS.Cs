using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using opswordsII.Dusts;
using static Terraria.ModLoader.ModContent;
using Terraria.Audio;

namespace opswordsII.Projectiles
{
    public class DesertS : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("DesertS");
        }
        public override void SetDefaults()   
        {    
            Projectile.width = 36;      
            Projectile.height = 36;
            Projectile.friendly = true;     
            Projectile.DamageType = DamageClass.Melee;        
            Projectile.tileCollide = true;  
            Projectile.penetrate = 1;   
            Projectile.timeLeft = 2000; 
            Projectile.light = 1.75f;
            Projectile.extraUpdates = 1;
            Projectile.ignoreWater = true;
            AIType = ProjectileID.TerraBeam;  
        }
        public override void AI()           
        {                                                       
            Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 1.00f;  
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(45f);
        }
        public override void Kill(int timeLeft) 
        {
            SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
            Vector2 usePos = Projectile.position;
			Vector2 rotVector = (Projectile.rotation - MathHelper.ToRadians(45f)).ToRotationVector2(); 
			usePos += rotVector * 16f;
			const int NUM_DUSTS = 20;
			for (int i = 0; i < NUM_DUSTS; i++) {
				Dust dust = Dust.NewDustDirect(usePos, Projectile.width, Projectile.height, 81);
				dust.position = (dust.position + Projectile.Center) / 2f;
				dust.velocity += rotVector * 2f;
				dust.velocity *= 0.5f;
				dust.noGravity = true;
				usePos -= rotVector * 8f;
            }
       }
            
       public override void OnHitNPC(NPC target, int damage, float knockback, bool crit) 
       {
		    if (Main.rand.NextBool()) target.AddBuff(BuffType<Buffs.Burning_Sand>(), 300);
       }
    }
}