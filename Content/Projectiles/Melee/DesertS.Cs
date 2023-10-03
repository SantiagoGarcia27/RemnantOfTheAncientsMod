using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria.Audio;
using RemnantOfTheAncientsMod.Content.Buffs.Debuff;
using RemnantOfTheAncientsMod.Content.Dusts;

namespace RemnantOfTheAncientsMod.Content.Projectiles.Melee
{
    public class DesertS : ModProjectile
    {
        public override void SetStaticDefaults()
        {
           // //DisplayName.SetDefault("DesertS");
        }
        public override void SetDefaults()   
        {    
            Projectile.width = 36;      
            Projectile.height = 10;
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
            Projectile.rotation = (float)Math.Atan2(Projectile.velocity.Y, Projectile.velocity.X) + 1.00f;
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(45f);

            if (Projectile.scale < 1)
            {
                Projectile.scale += 0.05f;
            }
            GenerateParticle();
        }
        public void GenerateParticle()
        {
            if (GetInstance<RemnantOfTheAncientsMod>().ParticleMeter(4) != 0)
            {
                int dust5 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustType<QuemaduraA>());
                Main.dust[dust5].velocity = Projectile.velocity;
            }
        }
        public override void OnKill(int timeLeft) 
        {
            SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
            for (int i = 0; i < GetInstance<RemnantOfTheAncientsMod>().ParticleMeter(20); i++)
            {
                int p1 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.GemTopaz, 0f, 0f, 100, default(Color), 3f);
                Main.dust[p1].velocity = Projectile.velocity * 0f;
                Main.dust[p1].noGravity = true;
            }
        }
            
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone) 
       {
		    if (Main.rand.NextBool()) target.AddBuff(BuffType<Burning_Sand>(), 300);
       }
    }
}