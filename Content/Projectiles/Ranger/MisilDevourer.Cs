using Microsoft.Xna.Framework;
using RemnantOfTheAncientsMod.Common.UtilsTweaks;
using SangarUtilities.Common.UtilsTweaks;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Content.Projectiles.Ranger
{
    public class MisilDevourer : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
        }
        NPC targetActual = null;
        public override void SetDefaults()
        {
            Projectile.width = 10; 
            Projectile.height = 10;
            Projectile.aiStyle = 1; 
            Projectile.friendly = true; 
            Projectile.hostile = false; 
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.penetrate = 1; 
            Projectile.timeLeft = 600; 
            Projectile.alpha = 0;
            Projectile.light = 0.5f; 
            Projectile.ignoreWater = true;
            Projectile.tileCollide = true; 
            Projectile.extraUpdates = 1; 
            AIType = ProjectileID.RocketIII;
        }
        public override void OnKill(int timeLeft)
        {
            Vector2 launchVelocity = new Vector2(-4, 0);         
            for (int i = 0; i < Projectile.ai[1]; i++)
            {

                launchVelocity = launchVelocity.RotatedBy(MathHelper.PiOver4);
                Projectile.NewProjectile(Projectile.InheritSource(Projectile), Projectile.Center + new Vector2(Main.rand.Next(-5,1)*16, Main.rand.Next(-1, 5) * 16), launchVelocity, ModContent.ProjectileType<LittleDevourer>(), Projectile.damage / 2, Projectile.knockBack, Projectile.owner, targetActual?.whoAmI ?? -1, 1);
            }
            if (Main.rand.NextFloat() <= (float)5 / 100) Projectile.NewProjectile(Projectile.InheritSource(Projectile), Projectile.Center + new Vector2(Main.rand.Next(-5, 1) * 16, Main.rand.Next(-1, 5) * 16), launchVelocity, CallUtils.TryGetProjectileFromMod(RemnantOfTheAncientsMod.CalamityMod, "ApotheosisWorm"), Projectile.damage / 2, Projectile.knockBack, Projectile.owner);
            base.OnKill(timeLeft);
        }
        public override void AI() 
        {
            Projectile.rotation = (float)Math.Atan2(Projectile.velocity.Y, Projectile.velocity.X) + 1.00f;
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(0f);

            foreach(NPC target in Main.npc)
            { 
                if (target.active && !target.friendly && !target.dontTakeDamage && target.defense <= 998 && !target.immortal)
                {
                    targetActual = target;
                    float shootToX = target.position.X + target.width * 0.5f - Projectile.Center.X;
                    float shootToY = target.position.Y - Projectile.Center.Y;
                    float distance = (float)Math.Sqrt((double)(shootToX * shootToX + shootToY * shootToY));

                    if (distance < 480f && !target.friendly && target.active)
                    {
                        distance = 3f / distance;
                        shootToX *= distance * 5;
                        shootToY *= distance * 5;

                        Projectile.velocity.X = shootToX;
                        Projectile.velocity.Y = shootToY;
                    }
                }
            }
            generateDust();
        }
        private void generateDust()
        {
            Vector2 position = Projectile.position + new Vector2(Main.rand.NextFloat(-8f, 8f), Main.rand.NextFloat(-8f, 8f));
            Dust dust = Dust.NewDustDirect(position, Projectile.width, Projectile.height, DustID.Water);

            // Configura los valores de la partícula
            dust.noGravity = true;
            dust.velocity = Vector2.Zero;
            dust.scale = 1f;
        }
    }
}