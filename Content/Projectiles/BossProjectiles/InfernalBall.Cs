using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Content.Projectiles.BossProjectile
{

    public class InfernalBall : BaseInfernalBall
    {
        public override bool Friendly => false;
        public override bool Strong => false;
        public override float Scale => 1.5f;
        public override void SetStaticDefaults() => base.SetDefaults();
        public override void AI()
        {
            AnimateTexture();  
            Projectile.rotation = (float)Math.Atan2(Projectile.velocity.Y, Projectile.velocity.X) + 1.00f;
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(10f);
        }
        public void AnimateTexture()
        {
            if (++Projectile.frameCounter >= 4)
            {
                Projectile.frameCounter = 0;
                if (++Projectile.frame >= Main.projFrames[Projectile.type])
                    Projectile.frame = 0;
            }
        }
    }
    public class InfernalBallF : BaseInfernalBall
    {
        public override bool Friendly => false;
        public override bool Strong => true;
        public override float Scale => 1.0f;
        public override void SetStaticDefaults() => base.SetDefaults();
        public override void AI()
        {
            AnimateTexture();
            Projectile.rotation = (float)Math.Atan2(Projectile.velocity.Y, Projectile.velocity.X) + 1.00f;
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(10f);
        }

        public void AnimateTexture()
        {
            if (++Projectile.frameCounter >= 4)
            {
                Projectile.frameCounter = 0;
                if (++Projectile.frame >= Main.projFrames[Projectile.type])
                    Projectile.frame = 0;
            }
        }
    }
    public class InfernalBall_f : BaseInfernalBall
    {
        public override bool Friendly => true;
        public override bool Strong => false;
        public override float Scale => 1.5f;
        public override void SetStaticDefaults() => base.SetDefaults();
        public override void AI()
        {
            AnimateTexture();
            Projectile.rotation = (float)Math.Atan2(Projectile.velocity.Y, Projectile.velocity.X) + 1.00f;
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(10f);

            Lighting.AddLight(Projectile.position, 1, 0, 0);
            if(Projectile.scale < 1)
            {
                Projectile.scale += 0.05f;
            }
            if(Projectile.timeLeft < 570)
            {
                int dust5 = Dust.NewDust(Projectile.position, Projectile.width + 20, Projectile.height, DustID.Torch);
                Main.dust[dust5].velocity = Projectile.velocity;
            }
        }

        public void AnimateTexture()
        {
            if (++Projectile.frameCounter >= 4)
            {
                Projectile.frameCounter = 0;
                if (++Projectile.frame >= Main.projFrames[Projectile.type])
                    Projectile.frame = 0;
            }
        }
    }
    public class InfernalBallF_f : BaseInfernalBall
    {
        public override bool Friendly => true;
        public override bool Strong => true;
        public override float Scale => 1.0f;
        public override void SetStaticDefaults() => base.SetDefaults();
        public override void AI()
        {
            AnimateTexture();
           
            Projectile.rotation = (float)Math.Atan2(Projectile.velocity.Y, Projectile.velocity.X) + 1.00f;
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(10f);


            Lighting.AddLight(Projectile.position, 1,0,0);
            if (Projectile.scale < 1)
            {
                Projectile.scale += 0.05f;
            }
            if (Projectile.timeLeft < 570)
            {
                int dust5 = Dust.NewDust(Projectile.position, Projectile.width + 20, Projectile.height, DustID.BlueTorch);
                Main.dust[dust5].velocity = Projectile.velocity;
                Main.dust[dust5].alpha -= 60; 
            }
        }

        public void AnimateTexture()
        {
            if (++Projectile.frameCounter >= 4)
            {
                Projectile.frameCounter = 0;
                if (++Projectile.frame >= Main.projFrames[Projectile.type])
                    Projectile.frame = 0;
            }
        }
    }
    public abstract class BaseInfernalBall : ModProjectile
    {

        public abstract bool Friendly { get; }
        public abstract float Scale { get; }
        public abstract bool Strong { get; }

        public override string Texture => selecttexture();
        public string selecttexture()
        {
            if (Strong) return "RemnantOfTheAncientsMod/Content/Projectiles/Textures/InfernalBallF";
            else return "RemnantOfTheAncientsMod/Content/Projectiles/Textures/InfernalBall";
        }
        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 4;
        }
        public override void SetDefaults()
        {
            Projectile.width = 46;
            Projectile.height = 34;
            Projectile.friendly = Friendly;
            Projectile.hostile = !Friendly;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.tileCollide = false;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 200;
            Projectile.light = 1.20f;
            Projectile.scale = Scale;
            Projectile.extraUpdates = 1;
            Projectile.ignoreWater = true;
            AIType = ProjectileID.TerraBeam;
            Main.projFrames[Projectile.type] = 4;
        }
        public override void OnKill(int timeLeft)
        {
            short dust = Strong ? DustID.IceTorch : DustID.Torch;
            const int NUM_DUSTS = 20;
            for (int i = 0; i < ModContent.GetInstance<RemnantOfTheAncientsMod>().ParticleMeter(NUM_DUSTS); i++)
            {

                int p1 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, dust, 0f, 0f, 100, default(Color), 3f);
                Main.dust[p1].velocity = Projectile.velocity * 0f;
                Main.dust[p1].noGravity = true;
            }
        }
    }
}