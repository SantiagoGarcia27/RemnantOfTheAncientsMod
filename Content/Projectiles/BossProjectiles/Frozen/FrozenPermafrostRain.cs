using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RemnantOfTheAncientsMod.Common.UtilsTweaks;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;


namespace RemnantOfTheAncientsMod.Content.Projectiles.BossProjectiles.Frozen
{
    public class FrozenPermafrostRain : ModProjectile
    {
        public override string Texture => "RemnantOfTheAncientsMod/Content/Items/Weapons/Melee/Permafrost";
        public override void SetStaticDefaults()
        {
            // //DisplayName.SetDefault("SkyCutterS"); //projectile name
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5; // The length of old position to be recorded
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0; // The recording mode
        }
        public override void SetDefaults()
        {
            Projectile.width = 36;       //projectile width
            Projectile.height = 36;  //projectile height
            Projectile.friendly = false;
            Projectile.hostile = !Projectile.friendly;//make that the projectile will not damage you
            Projectile.DamageType = DamageClass.Melee;          // 
            Projectile.tileCollide = true;   //make that the projectile will be destroed if it hits the terrain
            Projectile.penetrate = 1;      //how many NPC will penetrate
            Projectile.timeLeft = 2000;   //how many time this projectile has before disepire
            Projectile.light = 1.75f;    // projectile light
            Projectile.extraUpdates = 1;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.ignoreWater = true;
            Projectile.scale = 0.7f;
            AIType = ProjectileID.InfluxWaver;


        }
        public override void AI()
        {
            //if (Projectile.ai[2] <= Utils1.FormatTimeToTick(0, 0, 0, 2) && Projectile.ai[2] > 0)
            //{
            //    Projectile.velocity = Vector2.Zero;
            //    Projectile.ai[2]--;
            //}

            //if (Projectile.ai[2] == 0)
            //{
                float maxDetectRadius = 200f;
                float projSpeed = Projectile.stepSpeed;//4
                Player target = FindClosestNPC(maxDetectRadius);
                if (target == null)
                    return;

                Projectile.velocity = (target.Center - Projectile.Center).SafeNormalize(Vector2.Zero) * projSpeed;
                Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(45f);
            //}
        }
        public Player FindClosestNPC(float maxDetectDistance)
        {
            Player target = null;
            float sqrMaxDetectDistance = maxDetectDistance * maxDetectDistance;
            for (int k = 0; k < RemnantOfTheAncientsMod.MaxPlayers; k++)
            {
                Player target_ = Main.player[k];
                if (!target_.dead && target_.active)
                {
                    float sqrDistanceToTarget = Vector2.DistanceSquared(target_.Center, Projectile.Center);
                    if (sqrDistanceToTarget < sqrMaxDetectDistance)
                    {
                        sqrMaxDetectDistance = sqrDistanceToTarget;
                        target = target_;
                    }
                }
            }
            if (target == null)
            {
                target = Main.player[Main.myPlayer];
            }
            return target;
        }

        //public override void OnSpawn(IEntitySource source)
        //{
        //    Projectile.ai[2] = Utils1.FormatTimeToTick(0, 0, 0, 2);
        //    base.OnSpawn(source);
        //}
        public override void OnKill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
            for (int i = 0; i < new RemnantOfTheAncientsMod().ParticleMeter(10); i++)
            {
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Glass);
            }
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            Projectile.Kill();
        }
        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            Projectile.Kill();
            base.OnHitPlayer(target, info);
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Main.instance.LoadProjectile(Projectile.type);
            Texture2D texture = TextureAssets.Projectile[Projectile.type].Value;

            Vector2 drawOrigin = new Vector2(texture.Width * 0.5f, Projectile.height * 0.5f);
            for (int k = 0; k < Projectile.oldPos.Length; k++)
            {
                Vector2 drawPos = (Projectile.oldPos[k] - Main.screenPosition) + drawOrigin + new Vector2(0f, Projectile.gfxOffY);
                Color color = Projectile.GetAlpha(lightColor) * ((Projectile.oldPos.Length - k) / (float)Projectile.oldPos.Length);
                Main.EntitySpriteDraw(texture, drawPos, null, color, Projectile.rotation, drawOrigin, Projectile.scale, SpriteEffects.None, 0);
            }

            return true;
        }
    }
}

