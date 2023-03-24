using System;
using Microsoft.Xna.Framework;
using RemnantOfTheAncientsMod.Projectiles.Melee;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Projectiles
{
    public class TerraBulletP : ModProjectile
    {
        private int trailDelay = 0;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Terra Bullet");
        }
        public override void SetDefaults()
        {
            Projectile.width = 13;
            Projectile.height = 2;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.tileCollide = true;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 2000;
            Projectile.light = 0.1f;
            Projectile.extraUpdates = 1;
            Projectile.ignoreWater = true;
            AIType = ProjectileID.Bullet;
        }
        public override void AI()
        {
            Projectile.rotation = (float)Math.Atan2(Projectile.velocity.Y, Projectile.velocity.X) + 1.00f;
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(0f);

            trailDelay++;
            if (trailDelay >= 5)
            {
                trailDelay = 0;


               int i = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.TerraBlade, 0f, 0f, 100, Color.GreenYellow, 1f);
                Main.dust[i].noGravity = true;
            }
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            for (int i = 0; i < 5; i++)
            {
                Vector2 Position = Main.rand.NextVector2CircularEdge(target.width + 100, target.height + 100) + target.position;
                Vector2 Velocity = Position.DirectionTo(target.position) * 0.7f;
                Projectile.NewProjectileDirect(Projectile.GetSource_None(), Position, Velocity, ModContent.ProjectileType<TerraBlockP>(), damage / 4, Main.myPlayer, Projectile.owner);
            }
        }
    }

    public class TerraBlockP : ModProjectile
    {
        public override string Texture => "Terraria/Images/Item_" + ItemID.DirtBlock;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Terra Block");
        }
        public override void SetDefaults()
        {
            Projectile.width = 13;
            Projectile.height = 2;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.tileCollide = true;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 2000;
            Projectile.light = 0.1f;
            Projectile.extraUpdates = 1;
            Projectile.ignoreWater = true;
            AIType = ProjectileID.Bullet;
        }
        public override void AI()
        {
            Projectile.rotation = (float)Math.Atan2(Projectile.velocity.Y, Projectile.velocity.X) + 1.00f;
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(0f);
        }
    }
}