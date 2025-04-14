using System;
using Microsoft.Xna.Framework;
using RemnantOfTheAncientsMod.Common.Global;
using RemnantOfTheAncientsMod.Common.UtilsTweaks;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Content.Projectiles.Ranger
{
    public class EnchantedIceBullet : ModProjectile
    {
        public override void SetStaticDefaults()
        {       
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5;    //The length of old position to be recorded
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0;        //The recording mode
        }

        public override void SetDefaults()
        {
            Projectile.width = 22;
            Projectile.height = 2;
            Projectile.aiStyle = 1;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 400;
            Projectile.alpha = 100;
            Projectile.light = 1.5f;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = true;
            Projectile.extraUpdates = 1;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 30;
            AIType = ProjectileID.Bullet;
        }
        public override void AI()
        {
            Projectile.rotation = (float)Math.Atan2(Projectile.velocity.Y, Projectile.velocity.X) + 1.00f;
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(0f);
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Projectile.Kill();
            return true;
        }
        public override void OnKill(int timeLeft)
        {
            for (int i = 0; i < 360; i += 360/5)
            {
                double angle = MathHelper.ToRadians(i);
                Vector2 direction = new((float)Math.Cos(angle), (float)Math.Sin(angle)); 
                Vector2 position = Projectile.Center + direction * 10;

                int dustIndex = Dust.NewDust(position, 0, 0, DustID.Ice, 0, 0, 0, default, 1f); 
                Main.dust[dustIndex].velocity = direction * 2f;
            }
            int numProjectile = 4;
            if (Main.rand.NextBool(2))
            {
                for (int i = 0; i < 360; i += 360 / numProjectile)
                {

                    Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, Projectile.velocity.RotatedBy(i) / 2, ModContent.ProjectileType<EnchantedIceFragment>(), Projectile.damage / numProjectile, 0, Main.myPlayer);
                }
            }
            base.OnKill(timeLeft);
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.Slow, (int)Utils1.FormatTimeToTick(0, 0, 0, 20));
            base.OnHitNPC(target, hit, damageDone);
        }
    }


    public class EnchantedIceFragment : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5;    //The length of old position to be recorded
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0;        //The recording mode
        }

        public override void SetDefaults()
        {
            Projectile.width = 22;
            Projectile.height = 2;
            Projectile.aiStyle = 1;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 400;
            Projectile.alpha = 100;
            Projectile.light = 1.5f;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.extraUpdates = 1;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 30;
            AIType = ProjectileID.Bullet;
        }
        public override void AI()
        {
            Projectile.rotation = (float)Math.Atan2(Projectile.velocity.Y, Projectile.velocity.X) + 1.00f;
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(0f);

            int timmer = (int)Utils1.FormatTimeToTick(Second: 0.3f);

            if (counter >= timmer)
            {
                NPC target = Projectile.FindTargetWithinRange(300, false);

                if (target != null)
                {
                    Projectile.velocity = (target.Center - Projectile.Center).SafeNormalize(Vector2.Zero) * 20f;
                }
            }
            else
            {
                counter++;
            }
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Projectile.Kill();
            return true;
        }
        int counter = 0;
        public override bool? CanHitNPC(NPC target)
        {
            int timmer = (int)Utils1.FormatTimeToTick(Second: 0.3f);
            if (counter >= timmer) return true;
            return false;
        }
        public override void OnKill(int timeLeft)
        {
           

            base.OnKill(timeLeft);
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.Slow, (int)Utils1.FormatTimeToTick(0, 0, 0, 20));
            base.OnHitNPC(target, hit, damageDone);
        }
    }
}
	
