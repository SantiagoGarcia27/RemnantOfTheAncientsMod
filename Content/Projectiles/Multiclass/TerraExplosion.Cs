using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Content.Projectiles.Multiclass
{

    public class TerraExplosion : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("TerraExplosion"); //projectile name
        }
        int counter = (int)(0.5*60);
        public override void SetDefaults()
        {
            Projectile.width = 10;       //projectile width
            Projectile.height = 10;  //projectile height
            Projectile.friendly = true;      //make that the projectile will not damage you
            Projectile.DamageType = DamageClass.MagicSummonHybrid;          // 
            Projectile.tileCollide = false;   //make that the projectile will be destroed if it hits the terrain
            Projectile.penetrate = 1;      //how many NPC will penetrate
            Projectile.timeLeft = 200;   //how many time this projectile has before disepire
            Projectile.light = 1.75f;    // projectile light
            Projectile.extraUpdates = 1;
            Projectile.ignoreWater = true;
           // Projectile.alpha= 0;
            AIType = ProjectileID.TerraBeam;
        }
        public override void AI()           //this make that the projectile will face the corect way
        {
            if (counter == 0) 
            {
                float maxDetectRadius = 400f;
                float projSpeed = 9f; //5

                NPC closestNPC = FindClosestNPC(maxDetectRadius);
                if (closestNPC == null) return;
                Projectile.velocity = (closestNPC.Center - Projectile.Center).SafeNormalize(Vector2.Zero) * projSpeed;
                Projectile.rotation = Projectile.velocity.ToRotation();
            }
            else
            {
                counter--;
            }


            int ParticleCount = new RemnantOfTheAncientsMod().ParticleMeter(6);
            if (ParticleCount > 0)
            {
                for (int i = 0; i <= ParticleCount; i++)
                {
                    Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.TerraBlade);
                }
            }
        }
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
        public override void Kill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.Item14, Projectile.position);
            for (int i = 0; i < 1; i++)
            {
              var p = Projectile.NewProjectile(Projectile.GetSource_None(), Projectile.position, Vector2.Zero,ModContent.ProjectileType<DamageHitbox>(), Projectile.damage/2, Projectile.knockBack, Projectile.owner);
                Main.projectile[p].width = 30;
                Main.projectile[p].height = 30;
                Main.projectile[p].timeLeft = 10;
                Main.projectile[p].usesLocalNPCImmunity = true;
                Main.projectile[p].localNPCHitCooldown = 4;
                Main.projectile[p].penetrate = -1;
            }
            Vector2 explosionPos = Projectile.Center;
            for (int i = 0; i < 50; i++)
            {
                Dust.NewDust(explosionPos, Projectile.width, Projectile.height, DustID.Smoke, 0f, 0f, 100, default(Color), 1.5f);
                Dust.NewDust(explosionPos, Projectile.width, Projectile.height, DustID.TerraBlade, 0f, 0f, 100, default(Color), 1f);
            }

        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            //Projectile.Kill();
            if (Main.rand.NextBool())
            {
               // target.AddBuff(BuffID.Frozen, 300);
            }
        }
    }
}