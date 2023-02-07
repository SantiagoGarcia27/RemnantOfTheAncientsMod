using Microsoft.Xna.Framework;
using RemnantOfTheAncientsMod.Buffs.Debuff;
using RemnantOfTheAncientsMod.Dusts;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Projectiles.BossProjectile
{


    public class DesertTyphoon : DesertTyphoonModel
    {
        public override bool Friendly => false;
        public override bool Hostile => true;
        public override int PenetrateKill => 10;
        public override void SetStaticDefaults() => Main.projFrames[Projectile.type] = 4;

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Projectile.penetrate--;
            if (Projectile.penetrate <= PenetrateKill) Projectile.Kill();
            else
            {
                Collision.HitTiles(Projectile.position, Projectile.velocity, Projectile.width, Projectile.height);
                SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
                if (Math.Abs(Projectile.velocity.X - oldVelocity.X) > float.Epsilon) Projectile.velocity.X = -oldVelocity.X;
                if (Math.Abs(Projectile.velocity.Y - oldVelocity.Y) > float.Epsilon) Projectile.velocity.Y = -oldVelocity.Y;
            }
            return false;
        }
        public override void AI()
        {
            AnimateTexture();
            GenerateParticle();
            if (Projectile.alpha > 70)
            {
                Projectile.alpha -= 15;
                if (Projectile.alpha < 70) Projectile.alpha = 70;
            }
            if (Projectile.localAI[0] == 0f)
            {
                AdjustMagnitude(ref Projectile.velocity);
                Projectile.localAI[0] = 10f;
            }
        }
        private void AdjustMagnitude(ref Vector2 vector)
        {
            float magnitude = (float)Math.Sqrt(vector.X * vector.X + vector.Y * vector.Y);
            if (magnitude > 106f) vector *= 26f / magnitude;
        }
        public void GenerateParticle()
        {
            for (int i = 0; i < ModContent.GetInstance<RemnantOfTheAncientsMod>().ParticlleMetter(5); i++)
            {
                if (Projectile.alpha <= 100)
                {
                    int dust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, ModContent.DustType<QuemaduraA>());
                    Main.dust[dust].velocity /= 0.5f;
                }
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

    public class DesertTyphoonFriendly : DesertTyphoonModel
    {
        public override bool Friendly => true;
        public override bool Hostile => false;
        public override int PenetrateKill => 30;
        public override void SetStaticDefaults() => Main.projFrames[Projectile.type] = 4;
        public override void AI()
        {
            AnimateTexture();
            GenerateParticle();
            if (Projectile.alpha > 70)
            {
                Projectile.alpha -= 15;
                if (Projectile.alpha < 70) Projectile.alpha = 70;
            }
            if (Projectile.localAI[0] == 0f)
            {
                AdjustMagnitude(ref Projectile.velocity);
                Projectile.localAI[0] = 10f;
            }
            Vector2 move = Vector2.Zero;
            float distance = 400f;
            bool target = false;
            for (int k = 0; k < 200; k++)
            {
                if (Main.npc[k].active && !Main.npc[k].dontTakeDamage && !Main.npc[k].friendly && Main.npc[k].lifeMax > 5)
                {
                    Vector2 newMove = Main.npc[k].Center - Projectile.Center;
                    float distanceTo = (float)Math.Sqrt(newMove.X * newMove.X + newMove.Y * newMove.Y);
                    if (distanceTo < distance)
                    {
                        move = newMove;
                        distance = distanceTo;
                        target = true;
                    }
                }
                if (target)
                {
                    AdjustMagnitude(ref move);
                    Projectile.velocity = (10 * Projectile.velocity + move) / 11f;
                    AdjustMagnitude(ref Projectile.velocity);
                }
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
        public void GenerateParticle()
        {
            for (int i = 0; i < ModContent.GetInstance<RemnantOfTheAncientsMod>().ParticlleMetter(5); i++)
            {
                if (Projectile.alpha <= 100)
                {
                    int dust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, ModContent.DustType<QuemaduraA>());
                    Main.dust[dust].velocity /= 0.5f;
                }
            }
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Projectile.penetrate--;
            if (Projectile.penetrate <= 0) Projectile.Kill();
            else
            {
                Collision.HitTiles(Projectile.position, Projectile.velocity, Projectile.width, Projectile.height);
                SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
                if (Math.Abs(Projectile.velocity.X - oldVelocity.X) > float.Epsilon) Projectile.velocity.X = -oldVelocity.X;
                if (Math.Abs(Projectile.velocity.Y - oldVelocity.Y) > float.Epsilon) Projectile.velocity.Y = -oldVelocity.Y;
            }
            return false;
        }
        private void AdjustMagnitude(ref Vector2 vector)
        {
            float magnitude = (float)Math.Sqrt(vector.X * vector.X + vector.Y * vector.Y);
            if (magnitude > 106f) vector *= 26f / magnitude;
        }
    }

    public abstract class DesertTyphoonModel : ModProjectile
    {
        public override string Texture => "RemnantOfTheAncientsMod/Projectiles/Textures/DesertTyphoon";
        public abstract bool Friendly { get; }

        public abstract bool Hostile { get; }
        public abstract int PenetrateKill { get; }

        public override void SetDefaults()
        {
            //Projectile.CloneDefaults(ProjectileID.DemonScythe);
            //Projectile.aiStyle = ProjectileID.Typhoon;
            Projectile.width = 36;
            Projectile.height = 36;
            Projectile.friendly = Friendly;
            Projectile.hostile = Hostile;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.tileCollide = true;
            Projectile.penetrate = PenetrateKill;
            Projectile.timeLeft = 200;
            Projectile.light = 0.75f;
            Projectile.extraUpdates = 1;
            Projectile.ignoreWater = true;

        }
        public override void AI()
        {
            Projectile.light = 1.0f;

        }

        public override bool PreDraw(ref Color lightColor)
        {
            /* Main.instance.LoadProjectile(Projectile.type);
             Texture2D texture = TextureAssets.Projectile[Projectile.type].Value;
             Vector2 drawOrigin = new Vector2(texture.Width * 0.5f, Projectile.height * 0.5f);
             for (int k = 0; k < Projectile.oldPos.Length; k++)
             {
                 Vector2 drawPos = (Projectile.oldPos[k] - Main.screenPosition) + drawOrigin + new Vector2(0f, Projectile.gfxOffY);
                 Color color = Projectile.GetAlpha(lightColor) * ((Projectile.oldPos.Length - k) / (float)Projectile.oldPos.Length);
                 Main.EntitySpriteDraw(texture, drawPos, null, color, Projectile.rotation, drawOrigin, Projectile.scale, SpriteEffects.None, 0);
             }*/

            return true;
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if (Main.rand.NextBool()) target.AddBuff(ModContent.BuffType<Burning_Sand>(), 300);

        }
    }
}
