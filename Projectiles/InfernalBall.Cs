using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Projectiles
{

    public class InfernalBall : BaseInfernalBall
    {
        public override bool Friendly => false;
        public override bool Hostile => true;
        public override bool Strong => false;
        public override float Scale => 1.5f;
        public override void SetStaticDefaults() => base.SetDefaults();
        public override void AI() => base.AI();
    }
    public class InfernalBallF : BaseInfernalBall
    {
        public override bool Friendly => false;
        public override bool Hostile => true;
        public override bool Strong => true;
        public override float Scale => 1.0f;
        public override void SetStaticDefaults() => base.SetDefaults();
        public override void AI() => base.AI();
    }
    public class InfernalBall_f : BaseInfernalBall
    {
        public override bool Friendly => true;
        public override bool Hostile => false;
        public override bool Strong => false;
        public override float Scale => 1.5f;
        public override void SetStaticDefaults() => base.SetDefaults();
        public override void AI() => base.AI();
    }
    public class InfernalBallF_f : BaseInfernalBall
    {
        public override bool Friendly => true;
        public override bool Hostile => false;
        public override bool Strong => true;
        public override float Scale => 1.0f;
        public override void SetStaticDefaults() => base.SetDefaults();
        public override void AI() => base.AI();
    }
    public abstract class BaseInfernalBall : ModProjectile
    {

        public abstract bool Friendly { get; }
        public abstract bool Hostile { get; }
        public abstract float Scale { get; }
        public abstract bool Strong { get; }

        public override string Texture => selecttexture();
        public string selecttexture()
        {
            if (Strong) return "RemnantOfTheAncientsMod/Projectiles/Textures/InfernalBallF";
            else return "RemnantOfTheAncientsMod/Projectiles/Textures/InfernalBall";
        }
        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 4;
        }
        public override void SetDefaults()
        {
            Projectile.width = 46;
            Projectile.height = 46;
            Projectile.friendly = Friendly;
            Projectile.hostile = Hostile;
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
        public override void AI()
        {
            Projectile.rotation = (float)Math.Atan2(Projectile.velocity.Y, Projectile.velocity.X) + 1.00f;
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(10f);
        }
    }
}