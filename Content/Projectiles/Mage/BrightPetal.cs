using Microsoft.Xna.Framework;
using RemnantOfTheAncientsMod.Common.UtilsTweaks;
using System;
using System.IO;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Content.Projectiles.Mage
{
    internal class BrightPetal : BasePetal
    {
        public override string texture => "RemnantOfTheAncientsMod/Content/Projectiles/Mage/BrightPetal";
        public override float light => 1f;
        public override int buffId => 0;
        public override Color color => Color.LightGoldenrodYellow;
        public override void SetStaticDefaults()
        {
           // //DisplayName.SetDefault("Bright Petal");
        }
    }
    internal class BrightPetal_Fire : BasePetal
    {
        public override string texture => "RemnantOfTheAncientsMod/Content/Projectiles/Mage/BrightPetalB";
        public override float light => 1.5f;
        public override int buffId => BuffID.OnFire;

        public override Color color => Color.Orange;
        public override void SetStaticDefaults()
        {
           // //DisplayName.SetDefault("Bright Petal");
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
            Projectile.timeLeft = Utils1.FormatTimeToTick(Second:3);
        }



        int timer = 0;
        int timmerMax = Utils1.FormatTimeToTick(Second:1);


        bool Stopped
        {
            get => Projectile.ai[0] == 1; 
            set =>  Projectile.ai[0] = value.ToInt();       
        }

        public override void AI()           
        {              
            if(Main.netMode == NetmodeID.MultiplayerClient) return;


            Projectile.rotation = (float)Math.Atan2(Projectile.velocity.Y, Projectile.velocity.X) + 1.00f;
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(0f);

            if(Stopped)
            {
                if(timer < timmerMax) timer++;
                else Projectile.Kill();
                
                return;
            }
            Vector2 absVelocity = new(Math.Abs(Projectile.velocity.X), Math.Abs(Projectile.velocity.Y));

            if (absVelocity.X <= 0.2f && absVelocity.Y <= 0.2f) Projectile.velocity = Vector2.Zero;
            if (absVelocity.X <= 0 && absVelocity.Y <= 0) Stopped = true;
            else Projectile.velocity *= 0.95f;
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (buffId != 0)
            {
                if (Main.rand.NextBool()) target.AddBuff(buffId, 300);
            }
        }
        public override void OnKill(int timeLeft)
        {
            int damage = (int)(Projectile.damage * (buffId != 0 ? 1 : 0.5f));
            for (int i = 0; i < 5; i++)
            {
                int p = Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, Main.rand.NextVector2Circular(3f, 3f), ProjectileID.SeedlerThorn, damage, Projectile.knockBack, Projectile.owner);
                Main.projectile[p].DamageType = DamageClass.Magic;
            }
            base.OnKill(timeLeft);
        }
        public override Color? GetAlpha(Color lightColor) => color;
    }
}
