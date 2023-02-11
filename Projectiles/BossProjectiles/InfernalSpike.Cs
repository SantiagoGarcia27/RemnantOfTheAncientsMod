using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace RemnantOfTheAncientsMod.Projectiles
{
    public class InfernalSpike : BaseInfernalSpike
    {
        public override bool Friendly => false;
        public override bool Strong => false;
        public override float Scale => 2f;
        private int Frames = 6;
        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = Frames;
        }
        public override void AI()
        {
            AnimateTexture();
        }
        public override void SetDefaults() => base.SetDefaults();
        public void AnimateTexture()
        {
            if (++Projectile.frameCounter >= Frames)
            {
                Projectile.frameCounter = 0;
                if (++Projectile.frame >= Main.projFrames[Projectile.type])
                    Projectile.frame = 0;
            }
        }
    }

    public class InfernalSpikeF : BaseInfernalSpike
    {
        public override bool Friendly => false;
        public override bool Strong => true;
        public override float Scale => 2f;
        private int Frames = 8;

        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = Frames;
        }

        public override void SetDefaults() => base.SetDefaults();
        public override void AI()
        {
            AnimateTexture();
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
    }

    public class InfernalSpike_f : BaseInfernalSpike
    {
        private int Frames = 6;
        public override bool Friendly => true;
        public override bool Strong => false;
        public override float Scale => 1.5f;
        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = Frames;
        }
        public override void SetDefaults() => base.SetDefaults();

        public override void AI()
        {
            AnimateTexture();
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
    }

    public class InfernalSpikeF_f : BaseInfernalSpike
    {
        public override bool Friendly => true;
        public override bool Strong => true;
        public override float Scale => 1.5f;
        private int Frames = 8;
        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = Frames;
        }
        public override void SetDefaults() => base.SetDefaults();
        public override void AI()
        {
            AnimateTexture();
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
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.defense = target.defense / 2;
        }
    }

    public abstract class BaseInfernalSpike : ModProjectile
    {
        public abstract bool Friendly { get; }
        public abstract float Scale { get; }
        public abstract bool Strong { get; }
        public override string Texture => selecttexture();

        public string selecttexture()
        {
            if (Strong)
            {
                return "RemnantOfTheAncientsMod/Projectiles/Textures/InfernalSpikeF";
            }
            else
            {
                return "RemnantOfTheAncientsMod/Projectiles/Textures/InfernalSpike";
            }
        }
        public override void SetDefaults()
        {
            Projectile.width = 16;
            Projectile.height = 16;
            Projectile.friendly = Friendly;
            Projectile.hostile = !Friendly;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.tileCollide = false;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 2000;
            Projectile.light = 1.20f;
            Projectile.scale = Scale;
            Projectile.extraUpdates = 1;
            Projectile.ignoreWater = true;
            AIType = ProjectileID.BlueMoon;
        }

    }
}

