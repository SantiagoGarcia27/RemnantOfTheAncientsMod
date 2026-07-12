using System;
using Microsoft.Xna.Framework;
using RemnantOfTheAncientsMod.Common.UtilsTweaks;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;


namespace RemnantOfTheAncientsMod.Content.Projectiles.Mage
{
    public class HydraCluster_Proj : ModProjectile
    {
        public override void SetStaticDefaults()
        {
           // //DisplayName.SetDefault("SkyCutterS"); 
        }
        public override void SetDefaults()
        {
            Projectile.width = 46;     
            Projectile.height = 46;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.tileCollide = true;
            Projectile.penetrate = -1;
            Projectile.timeLeft = Utils1.FormatTimeToTick(Second: 10);
            Projectile.light = 1.75f;
            Projectile.extraUpdates = 1;
            Projectile.ignoreWater = true;
            Projectile.scale = 1f;
            AIType = ProjectileID.InfluxWaver;
        }
        public override void AI()          
        {
            Projectile.velocity *= 0.95f;
            Projectile.rotation += 0.05f;
            if (Projectile.localAI[0] % 10 == 0) {
                Vector2 speed = new Vector2(10, 10).RotatedBy(Main.rand.NextFloat(1f, 200f));
                Projectile.NewProjectile(Projectile.GetSource_FromAI(), Projectile.Center.X, Projectile.Center.Y, speed.X, speed.Y, ProjectileID.FrostBlastFriendly, Projectile.damage, 1f, Main.myPlayer);
            }
            Projectile.localAI[0] += 1f;
        }

        public override void OnKill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.Item10, Projectile.position);

            const int NUM_DUSTS = 20;
            for (int i = 0; i < RemnantOfTheAncientsMod.ParticleMeter(NUM_DUSTS); i++)
            {
                int p1 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Ice, 0f, 0f, 100, default(Color), 1f);
                Main.dust[p1].velocity = Projectile.velocity;
                Main.dust[p1].noGravity = true;
            }
        } 
    }
}
