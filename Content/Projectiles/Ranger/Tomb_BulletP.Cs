using System;
using Microsoft.Xna.Framework;
using RemnantOfTheAncientsMod.Common.UtilsTweaks;
using RemnantOfTheAncientsMod.Content.Buffs.Debuff;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Content.Projectiles.Ranger
{

    public class Tomb_BulletP : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // //DisplayName.SetDefault("Tomb_BulletP");
        }
        public override void SetDefaults()
        {
            Projectile.width = 13;
            Projectile.height = 2;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.tileCollide = true;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 2000;
            Projectile.light = 1.0f;
            Projectile.extraUpdates = 1;
            Projectile.ignoreWater = true;
            AIType = ProjectileID.Bullet;
        }
        public override void AI()
        {
            Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 1.00f;
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(0f);
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(ModContent.BuffType<CurseMarkBuff>(), (int)Utils1.FormatTimeToTick(0, 0, 0, 10));
            base.OnHitNPC(target, hit, damageDone);
        }
        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            target.AddBuff(ModContent.BuffType<CurseMarkBuff>(), (int)Utils1.FormatTimeToTick(0, 0, 0, 10));
            base.OnHitPlayer(target, info);
        }
        public override void OnKill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.Item10, Projectile.position);

            Vector2 usePos = Projectile.position;
            const int NUM_DUSTS = 10;
            for (int i = 0; i < 10; i++)
            {
                Vector2 rotVector = (Projectile.rotation - MathHelper.ToRadians(i * -18)).ToRotationVector2();
                //usePos += rotVector * 16f;
               

                Dust dust = Dust.NewDustDirect(usePos, Projectile.width, Projectile.height, DustID.Shadowflame);
                dust.position = (dust.position + Projectile.Center) / 2f;
                //  Main.LocalPlayer.position = dust.position;
                dust.velocity = Projectile.velocity * 0.5f;
                //dust.velocity += rotVector * 2f;
                //dust.velocity *= 0.5f;
                dust.noGravity = true;
                usePos -= rotVector * 2;// * 8f;

            }
        }
    }
}