using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Content.Projectiles.BossProjectiles.Calamity
{

    public class SignusMimic : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 6;
        }
        public override void SetDefaults()
        {
            Projectile.width = 136;
            Projectile.height = 136;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 20000;
            Projectile.extraUpdates = 1;
            Projectile.ignoreWater = true;
            Projectile.friendly = false;
            Projectile.hostile = true;
            AIType = -1;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.tileCollide = false;
        }
        public override void AI()
        {
            if (++Projectile.frameCounter >= 6)
            {
                Projectile.frameCounter = 0;
                // Or more compactly Projectile.frame = ++Projectile.frame % Main.projFrames[Projectile.type];
                if (++Projectile.frame >= Main.projFrames[Projectile.type])
                    Projectile.frame = 0;
            }
            // Projectile.rotation = (float)Math.Atan2(Projectile.velocity.Y, Projectile.velocity.X) + 1.00f;
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(180f);
            Lighting.AddLight(Projectile.position, TorchID.Purple);
        }
        public override void OnKill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.Roar, Projectile.position);
            Vector2 usePos = Projectile.position;
            Vector2 rotVector = (Projectile.rotation - MathHelper.ToRadians(90f)).ToRotationVector2();
            usePos += rotVector * 16f;


            for (int i = 0; i < RemnantOfTheAncientsMod.ParticleMeter(10,5,2,0); i++)
            {
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Shadowflame, 0f, 0f, 100, default, 1.5f);
            }
        }
    }
}