using Microsoft.Xna.Framework;
using RemnantOfTheAncientsMod.Buffs.Debuff;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Projectiles.Mage
{
    public class CustomBolt : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("CustomBolt");
        }
        public override void SetDefaults()
        {
            Projectile.height = 8;
            Projectile.width = 12;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.knockBack = 1.2f;
            Projectile.friendly = true;
            Projectile.tileCollide = true;
            Projectile.light = 1f;
            Projectile.timeLeft = 120;
            Projectile.penetrate = -1;
            AIType = ProjectileID.RubyBolt;
        }
        public override void AI()
        {
            float maxDetectRadius = 400f; // The maximum radius at which a projectile can detect a target
            float projSpeed = 5f; // The speed at which the projectile moves towards the target
            GenerateParticle();
            // Trying to find NPC closest to the projectile
            NPC closestNPC = FindClosestNPC(maxDetectRadius);
            if (closestNPC == null)
                return;

            // If found, change the velocity of the projectile and turn it in the direction of the target
            // Use the SafeNormalize extension method to avoid NaNs returned by Vector2.Normalize when the vector is zero
            Projectile.velocity = (closestNPC.Center - Projectile.Center).SafeNormalize(Vector2.Zero) * projSpeed;
            Projectile.rotation = Projectile.velocity.ToRotation();

            Lighting.AddLight(Projectile.position, 1, 0, 0);
            if (Projectile.scale < 1)
            {
                Projectile.scale += 0.05f;
            }
           
            
        }
        public void GenerateParticle()
        {
            if (ModContent.GetInstance<RemnantOfTheAncientsMod>().ParticlleMetter(4) != 0)
            {
                int dust5 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.GemRuby, 0f, 0f, 100, Color.Red, 3f);
                Main.dust[dust5].velocity = Projectile.velocity;

                const int NUM_DUSTS = 1;//50
                for (int i = 0; i < ModContent.GetInstance<RemnantOfTheAncientsMod>().ParticlleMetter(NUM_DUSTS); i++)
                {
                    int p1 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.GemRuby, 0f, 0f, 100, Color.Red, 3f);
                    Main.dust[p1].velocity = Projectile.velocity;
                    Main.dust[p1].noGravity = true;
                }
            }
        }

        // Finding the closest NPC to attack within maxDetectDistance range
        // If not found then returns null
        public NPC FindClosestNPC(float maxDetectDistance)
        {
            NPC closestNPC = null;
            float sqrMaxDetectDistance = maxDetectDistance * maxDetectDistance;
            for (int k = 0; k < Main.maxNPCs; k++)
            {
                NPC target = Main.npc[k];
                if (target.CanBeChasedBy())
                {
                    float sqrDistanceToTarget = Vector2.DistanceSquared(target.Center, Projectile.Center);
                    if (sqrDistanceToTarget < sqrMaxDetectDistance)
                    {
                        sqrMaxDetectDistance = sqrDistanceToTarget;
                        closestNPC = target;
                    }
                }
            }
            return closestNPC;
        }

        public override Color? GetAlpha(Color lightColor) => Color.Red;
    }
}
