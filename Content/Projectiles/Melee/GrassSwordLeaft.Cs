using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Content.Projectiles.Melee
{
    public class GrassSwordLeaft : ModProjectile
    {
        public override void SetStaticDefaults()
        {
           // //DisplayName.SetDefault("Sharp Leaf");
            Main.projFrames[Projectile.type] = 7;
        }
        public override void SetDefaults()   
        {    
            Projectile.width = 20;      
            Projectile.height = 20;
            Projectile.aiStyle = 152;
            Projectile.friendly = true;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.DamageType = DamageClass.Melee;        
            Projectile.penetrate = 2;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = -1;
            Projectile.timeLeft = 100; 
            Projectile.extraUpdates = 0;
        }
        float num = (float)Math.PI / 2f;
#pragma warning disable CS0414 // The field 'GrassSwordLeaft.flag' is assigned but its value is never used
        bool flag = false;
#pragma warning restore CS0414 // The field 'GrassSwordLeaft.flag' is assigned but its value is never used
        public override void AI()
        {

            if (Projectile.ai[0] != 0f)
            {
                Projectile.velocity = Projectile.velocity.RotatedBy(Projectile.ai[0]);
            }
            if (Collision.LavaCollision(Projectile.position, Projectile.width, Projectile.height))
            {
                Projectile.Kill();
                return;
            }
            if (Projectile.frameCounter++ >= 2)
            {
               
                if (Projectile.frame++ >= 7)
                {
                    Projectile.frame = 1;
                }
                Projectile.frameCounter = 1;
            }
            float fromValue = 60 - Projectile.timeLeft;
            float fromMax = Projectile.ai[1] + 20f;
            float num4 = Utils.Remap(fromValue, Projectile.ai[1], fromMax, 0f, 1f) * Utils.Remap(fromValue, Projectile.ai[1], Projectile.ai[1] + 60f, 1f, 0f);
            Projectile.velocity = Projectile.velocity.SafeNormalize(Vector2.UnitY) * (4f + 12f * (1f - num4));
            Projectile.Opacity = Utils.Remap(fromValue, 0f, 10f, 0f, 1f) * Utils.Remap(fromValue, 30f, 60f, 1f, 0f);
            num = 0f;
            if (Main.rand.NextBool(3))
            {
                Dust dust2 = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, 40, 0f, 0f, 0, default(Color), 1.2f);
                dust2.noGravity = true;
                dust2.velocity = Projectile.velocity * 0.5f;
            }
            if (Projectile.localAI[1] == 0f)
            {
                Projectile.localAI[1] = 1f;
                for (int i = 0; i < 10; i++)
                {
                    Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, 40, 0f, 0f, 0, default(Color), 0.7f).velocity = Projectile.velocity.SafeNormalize(Vector2.UnitY).RotatedBy(Main.rand.NextFloat() * ((float)Math.PI * 2f) * 0.25f) * (Main.rand.NextFloat() * 3f);
                }
            }

            Projectile.rotation = Projectile.velocity.ToRotation() + num;
            Projectile.tileCollide = false;

            float time = 3f; // Tiempo de duración del movimiento circular
            float angle = ((float)Math.PI * 2f) / time; // Ángulo de rotación por fotograma
            float y = Projectile.position.Y - (float)Math.Sin((time - Projectile.timeLeft) * angle) * 100f; // Nueva posición Y
            Projectile.position = new Vector2(Projectile.position.X, y);
        }

        //public void GenerateParticle()
        //{
        //    if (GetInstance<RemnantOfTheAncientsMod>().ParticleMeter(4) != 0)
        //    {
        //        int dust5 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustType<QuemaduraA>());
        //        Main.dust[dust5].velocity = Projectile.velocity;
        //    }
        //}
        //public override void Kill(int timeLeft) 
        //{
        //    SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
        //    for (int i = 0; i < GetInstance<RemnantOfTheAncientsMod>().ParticleMeter(20); i++)
        //    {
        //        int p1 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.GemTopaz, 0f, 0f, 100, default(Color), 3f);
        //        Main.dust[p1].velocity = Projectile.velocity * 0f;
        //        Main.dust[p1].noGravity = true;
        //    }
        //}

         public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
        }
    }
}