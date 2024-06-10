using Microsoft.Xna.Framework;
using RemnantOfTheAncientsMod.Common.UtilsTweaks;
using RemnantOfTheAncientsMod.Content.Buffs.Buffs.Minions;
using RemnantOfTheAncientsMod.Content.Projectiles.BossProjectiles.Frozen;
using RemnantOfTheAncientsMod.Content.Projectiles.Multiclass;
using RemnantOfTheAncientsMod.Content.Projectiles.Summon.Summon.Minioms;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Content.Projectiles.Ranger
{

    public class NanoDrone : HoverShooter
    {
        public override void SetStaticDefaults()
        {
            // //DisplayName.SetDefault("Baby Frozen Assaulter Minion");

            Main.projFrames[Projectile.type] = 1;
            Main.projPet[Projectile.type] = true;
            ProjectileID.Sets.MinionSacrificable[Projectile.type] = true;
            ProjectileID.Sets.MinionTargettingFeature[Projectile.type] = true; //This is necessary for right-click targeting
        }

        public override void SetDefaults()
        {
            Projectile.netImportant = true;
            Projectile.width = 24;
            Projectile.height = 32;
            Projectile.friendly = true;
            Projectile.minion = true;
            Projectile.minionSlots = 1;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 18000;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.usesLocalNPCImmunity = true;
            AIType = ProjectileID.Retanimini;
            inertia = 100f;//20
            shoot = ModContent.ProjectileType<FrostBeamF>();
            shootSpeed = 12f; //12
            shootCool = 40f;
            range = 30f;

        }

        public override void CheckActive()
        {
        }
        public int ShootCounter = 0;
        public override void Shoot(Vector2 targetPos)
        {
            base.Shoot(targetPos);
            ShootCounter++;
            if (ShootCounter >= 2)
            {
                Projectile.Kill();
            }
        }
        public override void OnKill(int timeLeft)
        {
            CreateParticleCircle(30, 4f);
            base.OnKill(timeLeft);
        }
        public void CreateParticleCircle(int particleCount, float radius)
        {
            for (int i = 0; i < particleCount; i++)
            {
                // Calcula el ángulo para esta partícula
                double angle = Math.PI * 2 * i / particleCount;

                // Calcula la posición de la partícula usando trigonometría
                float x = Projectile.Center.X + (float)(radius * Math.Cos(angle));
                float y = Projectile.Center.Y + (float)(radius * Math.Sin(angle));

                // Crea la partícula en la posición calculada
                Dust.NewDust(new Vector2(x, y), 0, 0, DustID.BlueFlare);
            }
        }
        public override void SelectFrame()
        {
            Projectile.frameCounter++;
            if (Projectile.frameCounter >= 6)
            {
                Projectile.frameCounter = 0;
                Projectile.frame = (Projectile.frame + 1) % 4; //3
            }
        }
    }
}