using System;
using CalamityMod.Items.Potions.Alcohol;
using Microsoft.Xna.Framework;
using RemnantOfTheAncientsMod.Common.UtilsTweaks;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Content.Projectiles.Ranger
{
    [JITWhenModsEnabled("InfernumMode")]
    public class FrozenHommingIceBlock : ModProjectile
    {
        public override string Texture => "CalamityMod/Projectiles/Magic/IceBlock";
        public override void SetStaticDefaults()
        {
            // //DisplayName.SetDefault("Bala de slime furioso");     //The English name of the projectile
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5;    //The length of old position to be recorded
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0;        //The recording mode
        }
        public override bool IsLoadingEnabled(Mod mod)
        {
            return ModLoader.TryGetMod("CalamityMod", out mod);
        }
        public override void SetDefaults()
        {
            Projectile.width = 22;
            Projectile.height = 2;
            Projectile.aiStyle = 1;
            Projectile.friendly = false;
            Projectile.hostile = true;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 800;
            Projectile.alpha = 255;
            Projectile.light = 1.5f;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = true;
            Projectile.extraUpdates = 1;
            AIType = -1;
        }
        public override void AI()
        {
            if (Projectile.ai[2] <= Utils1.FormatTimeToTick(0, 0, 0, 2) && Projectile.ai[2] > 0)
            {
                Projectile.velocity = Vector2.Zero;
                Projectile.ai[2]--;
            }

            if (Projectile.ai[2] == 0)
            {
                float maxDetectRadius = 500f;
                float projSpeed = 4f;
                Player target = FindClosestNPC(maxDetectRadius);
                if (target == null)
                    return;

                Projectile.velocity = (target.Center - Projectile.Center).SafeNormalize(Vector2.Zero) * projSpeed;
                Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(90f);
            }
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

        public override void OnSpawn(IEntitySource source)
        {
            Projectile.ai[2] = Utils1.FormatTimeToTick(0, 0, 0, 2);
            base.OnSpawn(source);
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Projectile.penetrate--;
            Collision.HitTiles(Projectile.position, Projectile.velocity, Projectile.width, Projectile.height);
            SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
            if (Math.Abs(Projectile.velocity.X - oldVelocity.X) > float.Epsilon) Projectile.velocity.X = -oldVelocity.X;
            if (Math.Abs(Projectile.velocity.Y - oldVelocity.Y) > float.Epsilon) Projectile.velocity.Y = -oldVelocity.Y;
            Projectile.Kill();
            return false;
        }
        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            target.AddBuff(BuffID.Frostburn2, (int)Utils1.FormatTimeToTick(0, 0, 0, 5));
            target.AddBuff(BuffID.Frozen, (int)Utils1.FormatTimeToTick(0, 0, 0, 1));
            SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
            if (Math.Abs(Projectile.velocity.X - Projectile.oldVelocity.X) > float.Epsilon) Projectile.velocity.X = -Projectile.oldVelocity.X;
            if (Math.Abs(Projectile.velocity.Y - Projectile.oldVelocity.Y) > float.Epsilon) Projectile.velocity.Y = -Projectile.oldVelocity.Y;
            Projectile.Kill();
            base.OnHitPlayer(target, info);
        }

    }
}
	
