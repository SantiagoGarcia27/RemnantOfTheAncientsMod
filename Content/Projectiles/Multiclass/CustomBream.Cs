using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Content.Projectiles.Multiclass
{
    internal class HealthBream : BaseBolt
    {
        public override Color color => Color.LightGreen;
        public override int ProjId => ProjectileID.PinkLaser;

        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            target.Heal((int)Utils1.GetValueFromPorcentage(target.statLifeMax2, 5));
        }
    }
    internal class InterceptionBream : BaseBolt
    {
        public override Color color => Color.Blue;
        public override int ProjId => ProjectileID.PinkLaser;

        public override void AI()
        {
            base.AI();
            for (int i = 0; i < Main.maxProjectiles; i++)
            {
                if (Main.projectile[i].hostile && !Main.projectile[i].friendly)
                { 
                    if (Vector2.Distance(Projectile.Center, Main.projectile[i].Center) < 10)
                    {
                        Main.projectile[i].Kill();
                        Projectile.Kill();
                    }
                }
            }
        }
    }

    public abstract class BaseBolt : ModProjectile
    {
        public override string Texture => "RemnantOfTheAncientsMod/Content/Projectiles/Multiclass/CustomBream";
        public abstract int ProjId { get; }
        public abstract Color color { get; }
        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjId);
            Projectile.height = 50;
            Projectile.width = 50;
            Projectile.DamageType = DamageClass.Generic;
            Projectile.friendly = true;
            Projectile.hostile = true;
            Projectile.tileCollide = false;
            Projectile.penetrate = 1;
            Projectile.knockBack = -5f;
            AIType = ProjId;
        }  
        public override Color? GetAlpha(Color lightColor) => color;
        public override void AI()
        {
           // Projectile.Size = new Vector2(TextureAssets.Projectile[Projectile.type].Value.Width, TextureAssets.Projectile[Projectile.type].Value.Height);
            Projectile.rotation = (float)Math.Atan2(Projectile.velocity.Y, Projectile.velocity.X) + 1.00f;
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(0f);
            base.AI();
        }
    }
}
