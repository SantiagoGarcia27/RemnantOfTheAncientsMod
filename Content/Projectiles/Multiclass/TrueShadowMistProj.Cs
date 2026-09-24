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

        private float RespawnColdown
        {
            set
            {
                Projectile.ai[0] = value;
            }
            get
            {
                return Projectile.ai[0];
            }
        }

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
        public override bool? CanCutTiles() => false;

        public override bool MinionContactDamage() => true;
        
        public override void AI()
        {
            Player player = Main.player[Projectile.owner];

            checkActive(player);
            MoveToPlayer(player);
            SpawnMist();
            SpawnClouds(player);
        }

        private void SpawnClouds(Player player)
        {
            
            if (RespawnColdown-- > 0) return;

           
            int cloudType = ProjectileType<TrueShadowMistCloudProj>();
            int activeCloudCount = player.ownedProjectileCounts[cloudType];

            if (activeCloudCount < MaxClouds)
            {
                float initialPhase = Main.rand.NextFloat(0f, MathF.PI * 2f);
                float direction = Main.rand.NextBool() ? 1f : -1f;

                Projectile.NewProjectile(Projectile.GetSource_FromThis(), player.Center, Vector2.Zero, cloudType, Projectile.damage, Projectile.knockBack, Projectile.owner, initialPhase, direction);
            }
        }

        private void SpawnMist()
        {
            if (Main.netMode == NetmodeID.Server) return;

            if (Main.rand.NextBool(5))
            {
                Vector2 spawnPos = Projectile.Center + new Vector2(Main.rand.NextFloat(-Projectile.width, Projectile.width), Main.rand.NextFloat(-Projectile.height, Projectile.height));
                Vector2 velocity = (spawnPos - Projectile.Center).SafeNormalize(Vector2.Zero) * Main.rand.NextFloat(1f, 3f);

                int dustIndex = Dust.NewDust(spawnPos, 0, 0, DustID.Shadowflame, velocity.X, velocity.Y, 150, default, Main.rand.NextFloat(1.5f, 2.5f));
                Main.dust[dustIndex].noGravity = true;
            }
        }
        private void MoveToPlayer(Player player)
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

        public override bool PreDraw(ref Color lightColor) => true;

        public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough, ref Vector2 hitboxCenterFrac)
        {
            fallThrough = false;
            return base.TileCollideStyle(ref width, ref height, ref fallThrough, ref hitboxCenterFrac);
        }

    }


    public class TrueShadowMistCloudProj : ModProjectile
    {
        private float MaxHp
        {
            get => Projectile.ai[2];
            set => Projectile.ai[2] = value; 
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
            Projectile.timeLeft = Utils1.FormatTimeToTick(Second: 63);

            if(Main.netMode != NetmodeID.MultiplayerClient) Projectile.ai[2] = 2;
        }
        public override bool? CanCutTiles() => false;
        
        public override bool MinionContactDamage() => true;

        public override void AI()
        {
            if (Main.netMode != NetmodeID.MultiplayerClient) return;

            Player player = Main.player[Projectile.owner];
            checkActive(player);
            DriftAroundPlayer(player);
            SpawnMistDust();
            CheckHostileProjectileCollision();

        }

        private void CheckHostileProjectileCollision()
        {
            Rectangle myHitbox = Projectile.Hitbox;
           
            foreach(Projectile objetive in Main.ActiveProjectiles)
            {
                if (!objetive.hostile || !objetive.Hitbox.Intersects(myHitbox)) return;
                
                objetive.Kill();
                if (MaxHp-- > 0) return;
                    
                ApplyParentCooldown();
                Projectile.Kill();
                break;
            }
        }

        private void ApplyParentCooldown()
        {
            int parentType = ProjectileType<TrueShadowMistProj>();

            foreach(Projectile proj in Main.ActiveProjectiles)
            {
                if (proj.owner != Projectile.owner || proj.type != parentType) return;
                
                proj.ai[0] = TrueShadowMistProj.CloudCooldown;
                break; 
            }
        }

        private void DriftAroundPlayer(Player player)
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

        private void SpawnMistDust()
        {
            if (Main.netMode == NetmodeID.Server) return;

            if (Main.rand.NextBool(2))
            {
                Vector2 dustPos = Projectile.Center + new Vector2(Main.rand.NextFloat(-20f, 20f), Main.rand.NextFloat(-15f, 15f));
                int dustIndex = Dust.NewDust(dustPos, 0, 0, DustID.Corruption, Projectile.velocity.X * 0.1f, Main.rand.NextFloat(-0.5f, 0.5f), 200, default, Main.rand.NextFloat(1.2f, 2f));
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

    }
}