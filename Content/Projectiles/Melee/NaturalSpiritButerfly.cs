using Microsoft.Xna.Framework;
using RemnantOfTheAncientsMod.Common.UtilsTweaks;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;


namespace RemnantOfTheAncientsMod.Content.Projectiles.Melee
{
    public class NaturalSpiritButerfly : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 3;
        }
        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.BabyBird);
            Projectile.width = 26;     
            Projectile.height = 26;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.tileCollide = false;
            Projectile.penetrate = -1;
            Projectile.timeLeft = Utils1.FormatTimeToTick(Second:5);
            Projectile.light = 1.75f;
            Projectile.ignoreWater = true;
            Projectile.scale = 1f;
            Projectile.hide = false;
            Projectile.alpha = 120;
            // AIType = ProjectileID.FlinxMinion;

            Projectile.minionSlots = 0;
        }

        int timeLeftToRerurn = Utils1.FormatTimeToTick(Second: 1);
        public override bool PreAI()
        {
            Player player = Main.player[Projectile.owner];
            if (Projectile.frame++ > Main.projFrames[Projectile.type]) Projectile.frame = 0;

            if (Projectile.timeLeft < timeLeftToRerurn)
            {
                Vector2 vel = player.Center - Projectile.Center;

                Projectile.velocity = vel / Math.Max(Projectile.timeLeft, 1);
                return false;
            }

         

            return base.PreAI();
        }
       

        public override void OnKill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.Item10, Projectile.position);

            const int NUM_DUSTS = 20;
            for (int i = 0; i < RemnantOfTheAncientsMod.ParticleMeter(NUM_DUSTS); i++)
            {
                int p1 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.GreenFairy, 0f, 0f, 100, default(Color), 1f);
                Main.dust[p1].velocity = Projectile.velocity;
                Main.dust[p1].noGravity = true;
            }
        } 
    }
}
