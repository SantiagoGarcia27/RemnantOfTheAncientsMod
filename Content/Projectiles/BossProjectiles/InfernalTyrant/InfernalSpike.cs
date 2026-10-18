using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;


namespace RemnantOfTheAncientsMod.Content.Projectiles.BossProjectiles.InfernalTyrant
{
    public class InfernalSpike : BaseInfernalSpike
    {
        public override bool Friendly => false;
        public override bool Strong => false;
        public override float Scale => 1.5f;
    }

    public class InfernalSpikeF : BaseInfernalSpike
    {
        public override bool Friendly => false;
        public override bool Strong => true;
        public override float Scale => 1.5f;
    }

    public class InfernalSpike_f : BaseInfernalSpike
    {  
        public override bool Friendly => true;
        public override bool Strong => false;
        public override float Scale => 1.5f;
    }

    public class InfernalSpikeF_f : BaseInfernalSpike
    {
        public override bool Friendly => true;
        public override bool Strong => true;
        public override float Scale => 1.5f;
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.defense = target.defense / 2;
        }
    }

    public abstract class BaseInfernalSpike : ModProjectile
    {
        public abstract bool Friendly { get; }
        public abstract float Scale { get; }
        public abstract bool Strong { get; }

        private int Frames => Strong ? 8 : 6;
        public override string Texture => selecttexture();

        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = Frames;
        }
        public string selecttexture()
        {
            string path = GetType().FullName!.Replace('.', '/');
            path = path[..path.LastIndexOf('/')];
            path += $"/{ (!Strong ? "InfernalSpike" : "InfernalSpikeF") }";
            return path;
        }
        public override void SetDefaults()
        {
            Projectile.width = (int)(Scale * (!Strong ? 12 : 24));
            Projectile.height = (int)(Scale * (!Strong ? 12 : 24));
            Projectile.friendly = Friendly;
            Projectile.hostile = !Friendly;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.tileCollide = false;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 2000;
            Projectile.light = 1.20f;
            Projectile.scale = Scale;
            Projectile.ignoreWater = true;
        }
        public override void AI()
        {
            float speed = Strong ? 0.006f : 0.003f;
            Projectile.rotation += speed;
            DecelerateMovement();
            AnimateTexture();
            base.AI();
        }
        public void AnimateTexture()
        {
            if (++Projectile.frameCounter >= Frames)
            {
                Projectile.frameCounter = 0;
                if (++Projectile.frame >= Main.projFrames[Projectile.type])
                    Projectile.frame = 0;
            }
        }
        public void DecelerateMovement()
        {
            if (Projectile.velocity.LengthSquared() > 0.01f)
            {
                Projectile.velocity *= 0.975f;
                Projectile.rotation = Projectile.velocity.ToRotation();
            }
            else
            {
                Projectile.velocity = Vector2.Zero;
            }
        }

    }
}

