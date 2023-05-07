using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace RemnantOfTheAncientsMod.Content.Projectiles.Melee
{

    public class SolarangP : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("SolarangP"); //projectile name
          //  ProjectileID.Sets.CountsAsHoming[Projectile.type] = true;
        }
        public override void SetDefaults()
        {
            AIType = ProjectileID.Flamarang;
            Projectile.CloneDefaults(ProjectileID.Flamarang);
            Projectile.scale = 1.5f;
            Projectile.tileCollide = false;
            Projectile.penetrate = 2;
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit) 
        {
if (Main.rand.NextBool())
            {
	target.AddBuff(BuffID.Daybreak, 300);
}
            if (new RemnantOfTheAncientsMod().ParticleMeter(4) != 0)
            {
                Projectile.NewProjectile(Projectile.GetSource_None(), target.position, new Vector2(0f, 0f), ProjectileID.SolarWhipSwordExplosion, damage / 10, 0);
            }
        }

        public override void AI()
        {
            Projectile.rotation += Projectile.direction * 0.8f;

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
        private void AdjustMagnitude(ref Vector2 vector)
        {
            float magnitude = (float)Math.Sqrt(vector.X * vector.X + vector.Y * vector.Y);
            if (magnitude > 106f) vector *= 26f / magnitude;
        }
    }
}