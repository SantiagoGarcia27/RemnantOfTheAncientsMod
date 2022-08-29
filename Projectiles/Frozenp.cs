using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Projectiles
{
    public class Frozenp : BaseFrozenP
    {
        public override bool Friendly => false;
        public override bool hostile => true;
        public override void SetStaticDefaults() => base.SetDefaults();
    }
    public class frozen_p_f : BaseFrozenP
    {
        public override bool Friendly => true;
        public override bool hostile => false;
        public override void SetStaticDefaults() => base.SetDefaults();


    }
    public class Frozenp_M : BaseFrozenP
    {
        public override bool Friendly => true;
        public override bool hostile => false;
        public override void SetStaticDefaults() => base.SetDefaults();

    }
    public abstract class BaseFrozenP : ModProjectile
    {
        public override string Texture => "RemnantOfTheAncientsMod/Projectiles/Frozenp";
        public abstract bool Friendly { get; }
        public abstract bool hostile { get; }
        public override void SetDefaults()
        {
            Projectile.width = 36;
            Projectile.height = 36;
            Projectile.penetrate = 2;
            Projectile.timeLeft = 20000;
            Projectile.light = 0.15f;
            Projectile.extraUpdates = 1;
            Projectile.ignoreWater = true;
            Projectile.friendly = Friendly;
            Projectile.hostile = hostile;
            Projectile.tileCollide = true;
            AIType = ProjectileID.IceSpike;
            Projectile.DamageType = DamageClass.Magic;
            if (RemnantOfTheAncientsMod.CalamityMod != null) Projectile.tileCollide = false;
        }
        public override void AI()
        {
            Projectile.rotation = (float)Math.Atan2(Projectile.velocity.Y, Projectile.velocity.X) + 1.00f;
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(90f);
        }
        public override void Kill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
            Vector2 usePos = Projectile.position;
            Vector2 rotVector = (Projectile.rotation - MathHelper.ToRadians(90f)).ToRotationVector2();
            usePos += rotVector * 16f;

            const int NUM_DUSTS = 10;
            for (int i = 0; i < NUM_DUSTS; i++)
            {
                Dust dust = Dust.NewDustDirect(usePos, Projectile.width, Projectile.height, DustID.Tin);
                dust.position = (dust.position + Projectile.Center) / 2f;
                dust.velocity += rotVector * 2f;
                dust.velocity *= 0.5f;
                dust.noGravity = true;
                usePos -= rotVector * 8f;
            }
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.immune[Projectile.owner] = 0;
        }
    }
}