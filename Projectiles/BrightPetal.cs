using Microsoft.Xna.Framework;
using RemnantOfTheAncientsMod.Buffs.Debuff;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Projectiles
{
    internal class BrightPetal : BasePetal
    {
        public override string texture => "RemnantOfTheAncientsMod/Projectiles/BrightPetal";
        public override float light => 1f;
        public override int buffId => 0;
        public override Color color => Color.LightGoldenrodYellow;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Bright Petal");
        }
    }
    internal class BrightPetal_Fire : BasePetal
    {
        public override string texture => "RemnantOfTheAncientsMod/Projectiles/BrightPetalB";
        public override float light => 1.5f;
        public override int buffId => BuffID.OnFire;

        public override Color color => Color.Orange;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Bright Petal");
        }
        public override void SetDefaults()
        {
            Projectile.height = 8;
            Projectile.width = 12;
            Projectile.damage = 15;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.knockBack = 1.2f;
            Projectile.friendly = true;
            Projectile.tileCollide = true;
            Projectile.light = 1.5f;
            Projectile.timeLeft = 120;
        }
    }


    public abstract class BasePetal : ModProjectile
    {
        public abstract string texture { get; }
        public abstract float light { get; }
        public abstract int buffId { get; }
        public abstract Color color { get; }

        public override string Texture => texture;
        public override void SetDefaults()
        {
            Projectile.height = 8;
            Projectile.width = 12;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.knockBack = 1.2f;
            Projectile.friendly = true;
            Projectile.tileCollide = true;
            Projectile.light = light;
            Projectile.timeLeft = 120;
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if (buffId != 0)
            {
                if (Main.rand.NextBool()) target.AddBuff(buffId, 300);
            }
        }
        public override Color? GetAlpha(Color lightColor) => color;
    }
}
