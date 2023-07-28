using System;
using Microsoft.CodeAnalysis.MSBuild;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Content.Projectiles
{

    public class DamageHitbox : ModProjectile
    {
        public override void SetStaticDefaults()
        {
           // //DisplayName.SetDefault("");
        }
        public override void SetDefaults()
        {
            Projectile.width = 1;
            Projectile.height = 1;
            Projectile.friendly = true;
            Projectile.tileCollide = false;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 2000;
            Projectile.light = 1.0f;
            Projectile.extraUpdates = 1;
            Projectile.ignoreWater = true;
            AIType = -1;
        }
        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            Dust dust = new Dust();
            switch (Projectile.ai[0])
            {
                case 1:
                    dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Torch, 0f, 0f, 100, default, 2f);
                    dust.noGravity = true;
                    break;
                case 2:
                    dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.EnchantedNightcrawler, 0f, 0f, 100, default, 2f);
                    dust.noGravity = true;
                    player.statDefense += 3;

                    Vector2 offset = new Vector2(player.width / 2, player.height / 2) + new Vector2(24, 24);
                    for (int i = 0; i < new RemnantOfTheAncientsMod().ParticleMeter(20); i++)
                    {
                        Vector2 dustPos = player.position - new Vector2(24, 24) + offset + new Vector2(60, 0).RotatedBy(MathHelper.ToRadians(18 * i));
                        var d = Dust.NewDustPerfect(dustPos, 172, Vector2.Zero);
                        d.noGravity = true;
                    }
                    break;
                    case 3:
                    dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Demonite, 0f, 0f, 100, default, 2f);
                    dust.noGravity = true;
                    break;
            }
            if (Projectile.ai[1] == 1)
            {  
                Projectile.Center = player.Center;
            }
        }
         public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (Projectile.ai[0] == 1)
            {
                target.AddBuff(BuffID.OnFire, 100);
            }
            if (Projectile.ai[0] == 3)
            {
                target.AddBuff(BuffID.ShadowFlame, 100);
            }
        }
        public override void Kill(int timeLeft)
        {
           
        }
    }
}