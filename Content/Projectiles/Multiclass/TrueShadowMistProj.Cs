using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RemnantOfTheAncientsMod.Common.UtilsTweaks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;


namespace RemnantOfTheAncientsMod.Content.Projectiles.Multiclass
{
    public class TrueShadowMistProj : ModProjectile
    {
        public override string Texture => "RemnantOfTheAncientsMod/Assets/MagicPixel";

        private const int MaxClouds = 3;
        public static readonly float CloudCooldown = Utils1.FormatTimeToTick(Second: 6);

        // localAI[0] = cooldown timer, only set when a cloud is destroyed by a hostile projectile

        public override void SetStaticDefaults()
        {
        }
        public sealed override void SetDefaults()
        {
            Projectile.width = 30;
            Projectile.height = 30;
            Projectile.penetrate = -1;
            Projectile.alpha = 100;
            Projectile.manualDirectionChange = true;
            Projectile.netImportant = true;
            Projectile.timeLeft = -1;

        }
        public override bool? CanCutTiles()
        {
            return false;
        }
        public override bool MinionContactDamage()
        {
            return true;
        }

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            checkActive(player);
            moveToPlayer(player);
            spawnMist(player);
            spawnClouds(player);
        }

        private void spawnClouds(Player player)
        {
            // Cooldown only active after a cloud was destroyed by a hostile projectile
            if (Projectile.localAI[0] > 0)
            {
                Projectile.localAI[0]--;
                return;
            }

            int activeCloudCount = 0;
            int cloudType = ProjectileType<TrueShadowMistCloudProj>();
            for (int i = 0; i < Main.maxProjectiles; i++)
            {
                Projectile proj = Main.projectile[i];
                if (proj.active && proj.owner == Projectile.owner && proj.type == cloudType)
                    activeCloudCount++;
            }

            if (activeCloudCount < MaxClouds)
            {
                float initialPhase = Main.rand.NextFloat(0f, MathF.PI * 2f);
                float direction = Main.rand.NextBool() ? 1f : -1f;
                Projectile.NewProjectile(Projectile.GetSource_FromThis(), player.Center, Vector2.Zero, cloudType, Projectile.damage, Projectile.knockBack, Projectile.owner, initialPhase, direction);
            }
        }

        private void spawnMist(Player player)
        {
            if (Main.rand.NextBool(5))
            {
                Vector2 spawnPos = Projectile.Center + new Vector2(Main.rand.NextFloat(-Projectile.width, Projectile.width), Main.rand.NextFloat(-Projectile.height, Projectile.height));
                Vector2 velocity = (spawnPos - Projectile.Center).SafeNormalize(Vector2.Zero) * Main.rand.NextFloat(1f, 3f);
                int dustIndex = Dust.NewDust(spawnPos, 0, 0, DustID.Shadowflame, velocity.X, velocity.Y, 150, default(Color), Main.rand.NextFloat(1.5f, 2.5f));
                Main.dust[dustIndex].noGravity = true;
            }
        }
        private void moveToPlayer(Player player)
        {
            Projectile.Center = player.Center;
        }

        private void checkActive(Player player)
        {
            if (player.dead || !player.active || !player.GetModPlayer<RemnantPlayer>().TrueShadowMist)
            {
                Projectile.timeLeft = 2;
                Projectile.Kill();
            }
            else
            {
                Projectile.timeLeft = 1000;
            }
        }

        public override bool PreDraw(ref Color lightColor)
        {
            return true;
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {

        }
        public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough, ref Vector2 hitboxCenterFrac)
        {
            fallThrough = false;
            return base.TileCollideStyle(ref width, ref height, ref fallThrough, ref hitboxCenterFrac);
        }

    }


    public class TrueShadowMistCloudProj : ModProjectile
    {
        int maxHp = 2;
        public override void SetStaticDefaults()
        {
        }
        public sealed override void SetDefaults()
        {
            Projectile.width = 30;
            Projectile.height = 30;
            Projectile.penetrate = -1;
            Projectile.alpha = 100;
            Projectile.manualDirectionChange = true;
            Projectile.netImportant = true;
            Projectile.tileCollide = false;
            Projectile.timeLeft = (int)Utils1.FormatTimeToTick(Second: 63);

        }
        public override bool? CanCutTiles()
        {
            return false;
        }
        public override bool MinionContactDamage()
        {
            return true;
        }

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            checkActive(player);
            driftAroundPlayer(player);
            spawnMistDust();
            checkHostileProjectileCollision();
        }

        private void checkHostileProjectileCollision()
        {
            Rectangle myHitbox = Projectile.Hitbox;
            for (int i = 0; i < Main.maxProjectiles; i++)
            {
                Projectile other = Main.projectile[i];
                if (other.active && other.hostile && !other.friendly && other.Hitbox.Intersects(myHitbox))
                {
                    if (maxHp <= 1)
                    {
                        other.Kill();
                        applyParentCooldown();
                        Projectile.Kill();
                        break;
                    }
                    else
                    {
                        maxHp--;
                        other.Kill();
                    }
                }
            }
        }

        private void applyParentCooldown()
        {
            int parentType = ProjectileType<TrueShadowMistProj>();
            for (int i = 0; i < Main.maxProjectiles; i++)
            {
                Projectile proj = Main.projectile[i];
                if (proj.active && proj.owner == Projectile.owner && proj.type == parentType)
                {
                    proj.localAI[0] = TrueShadowMistProj.CloudCooldown;
                    break;
                }
            }
        }

        private void driftAroundPlayer(Player player)
        {
            // Keep up with player movement so clouds don't lag behind
            Projectile.Center += player.velocity;

            // ai[0] = phase angle, ai[1] = direction (1 or -1)
            Projectile.ai[0] += Projectile.ai[1] * 0.035f;

            float horizontalRange = 60f;
            float verticalBob = 20f;

            float offsetX = MathF.Sin(Projectile.ai[0]) * horizontalRange;
            float offsetY = MathF.Sin(Projectile.ai[0] * 1.7f) * verticalBob;

            Vector2 target = player.Center + new Vector2(offsetX, offsetY);
            Projectile.Center = Vector2.Lerp(Projectile.Center, target, 0.08f);
        }

        private void spawnMistDust()
        {
            if (Main.rand.NextBool(2))
            {
                Vector2 dustPos = Projectile.Center + new Vector2(Main.rand.NextFloat(-20f, 20f), Main.rand.NextFloat(-15f, 15f));
                int dustIndex = Dust.NewDust(dustPos, 0, 0, DustID.Corruption, Projectile.velocity.X * 0.1f, Main.rand.NextFloat(-0.5f, 0.5f), 200, default(Color), Main.rand.NextFloat(1.2f, 2f));
                Main.dust[dustIndex].noGravity = true;
                Main.dust[dustIndex].velocity *= 0.2f;
            }
        }

        private void checkActive(Player player)
        {
            if (player.dead || !player.active || !player.GetModPlayer<RemnantPlayer>().TrueShadowMist)
            {
                Projectile.timeLeft = 2;
                Projectile.Kill();
            } 
        }

        public override bool PreDraw(ref Color lightColor)
        {
            lightColor = new Color(44,42,92);
            return true;
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {

        }

    }
}