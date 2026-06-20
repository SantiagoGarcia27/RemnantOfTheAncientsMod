using Microsoft.Xna.Framework;
using RemnantOfTheAncientsMod.Common.UtilsTweaks;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Content.Projectiles.Trower
{
    public class GemstoneCrusherProj_Emerald : GemstoneCrusherProj
    {
        public override int GemId => ItemID.Emerald;
        public override void SetDefaults()
        {
            base.SetDefaults();
            Projectile.penetrate = -1;
            Projectile.tileCollide = true;
            Projectile.timeLeft = 900;
        }

        public override void AI()
        {
            Projectile.rotation += 0.1f * Projectile.direction;
            Projectile.velocity.Y += 0.2f;
            base.AI();
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            if(Math.Abs(Projectile.velocity.Y) > 0.001f)
            Projectile.velocity.Y *= -2f;
            if (Math.Abs(Projectile.velocity.Y) > 9) Projectile.velocity.Y = 9 * Math.Sign(Projectile.velocity.Y);
            return false;
        }
    }
    public class GemstoneCrusherProj_Sapphire : GemstoneCrusherProj
    {
        public override int GemId => ItemID.Sapphire;
        public override void SetDefaults()
        {
            base.SetDefaults();
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            Projectile.timeLeft = 900;
        }
        public override void AI()
        {
            NPC npc = Main.npc[(int)Projectile.ai[0]];

            if (!npc.active)
            {
                Projectile.Kill();
                return;
            }

            int count = 0;
            int index = 0;

            for (int i = 0; i < Main.maxProjectiles; i++)
            {
                Projectile p = Main.projectile[i];

                if (p.active &&
                    p.type == Projectile.type &&
                    (int)p.ai[0] == npc.whoAmI)
                {
                    if (i == Projectile.whoAmI)
                        index = count;

                    count++;
                }
            }

            if (count <= 0)
                return;

            float rotationSpeed = 0.03f;

            float angle =
                Main.GameUpdateCount * rotationSpeed +
                MathHelper.TwoPi * index / count;

            float radius =
                Math.Max(npc.width, npc.height) * 2f;

            Projectile.Center =
                npc.Center +
                angle.ToRotationVector2() * radius;

            base.AI();
        }
    }
    public class GemstoneCrusherProj_Ruby : GemstoneCrusherProj
    {
        public override int GemId => ItemID.Ruby;
        public override void SetDefaults() => AIType = ProjectileID.Shuriken;

        public override void AI()
        {
            Projectile.velocity.Y += 0.3f;
            base.AI();
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.OnFire, (int)Utils1.FormatTimeToTick(Second: 2));
            base.OnHitNPC(target, hit, damageDone);
        }
        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            target.AddBuff(BuffID.OnFire, (int)Utils1.FormatTimeToTick(Second: 2));
            base.OnHitPlayer(target, info);
        }
    }
    public class GemstoneCrusherProj_Diamond : GemstoneCrusherProj
    {
        public override int GemId => ItemID.Diamond;
        public override void SetDefaults() => AIType = ProjectileID.Fireball;
    }

    public abstract class GemstoneCrusherProj : ModProjectile
    {
        public abstract int GemId { get; }
        public override string Texture => getTexture(GemId);
        public override void SetDefaults()
        {
            Projectile.width = 16;
            Projectile.height = 16;
            Projectile.friendly = false;
            Projectile.hostile = true;
            Projectile.tileCollide = true;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 200;
            Projectile.light = 1.05f;
            Projectile.extraUpdates = 1;
            Projectile.ignoreWater = true;
            Projectile.scale = 1f;
            AIType = ProjectileID.Bullet;
        }

        static string getTexture(int GemId)
        {
            return "Terraria/Images/Item_" + GemId;
        }
    }
}