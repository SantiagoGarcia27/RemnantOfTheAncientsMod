using Microsoft.Xna.Framework;
using RemnantOfTheAncientsMod.Content.Dusts;
using RemnantOfTheAncientsMod.Content.Projectiles.BossProjectiles.Desert;
using System;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Content.NPCs.ProjectileNpc
{
    public class DesertTyphoonParry : ModNPC
    {
        public override string Texture => "RemnantOfTheAncientsMod/Content/Projectiles/Textures/DesertTyphoon";
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[Type] = 4;
        }

        public override void SetDefaults()
        {
            NPC.lifeMax = 10;
            NPC.damage = 1;
            NPC.width = 36;
            NPC.height = 36;
            NPC.friendly = false;
            NPC.noTileCollide = false;
            NPC.timeLeft = 200;
            NPC.aiStyle = -1;
            NPC.noGravity = true;
        }
        public override void AI()
        {
            AnimateTexture();
            GenerateParticle();
            if (NPC.alpha > 70)
            {
                NPC.alpha -= 15;
                if (NPC.alpha < 70) NPC.alpha = 70;
            }
            if (NPC.localAI[0] == 0f)
            {
                AdjustMagnitude(ref NPC.velocity);
                NPC.localAI[0] = 10f;
            }
            if(NPC.collideX || NPC.collideY)
            {
                NPC.life = 0;
            }
        }
        public override Color? GetAlpha(Color drawColor)
        {
            return Color.White;
        }
        private void AdjustMagnitude(ref Vector2 vector)
        {
            float magnitude = (float)Math.Sqrt(vector.X * vector.X + vector.Y * vector.Y);
            if (magnitude > 106f) vector *= 26f / magnitude;
        }
        public override bool? CanFallThroughPlatforms() => true;
        public void GenerateParticle()
        {
            for (int i = 0; i < RemnantOfTheAncientsMod.ParticleMeter(5); i++)
            {
                if (NPC.alpha <= 100)
                {
                    int dust = Dust.NewDust(NPC.position, NPC.width, NPC.height, ModContent.DustType<QuemaduraA>());
                    Main.dust[dust].velocity /= 0.5f;
                }
            }
        }
        public override void OnHitByItem(Player player, Item item, NPC.HitInfo hit, int damageDone)
        {
            NPC.velocity *= -3;
            damageDone = 1;
            hit.Damage = 1;
            base.OnHitByItem(player, item, hit, damageDone);
        }
        public override void OnHitByProjectile(Projectile projectile, NPC.HitInfo hit, int damageDone)
        {
            if(projectile.DamageType == DamageClass.SummonMeleeSpeed)
            {
                NPC.velocity *= -3;
                damageDone = 1;
                hit.Damage = 1;
                NPC.direction = hit.HitDirection;
                NPC.dontTakeDamage = true;
            }
            base.OnHitByProjectile(projectile, hit, damageDone);
        }
        int frameCounter = 0;
        /* public void AnimateTexture()
         {
             frameCounter++;
             if (frameCounter >= Main.projFrames[NPC.type])
             {
                 frameCounter = 0;
             }
             NPC.frame.Y = frameCounter;
         }*/

        public void AnimateTexture()
        {
            int type = ModContent.ProjectileType<DesertTyphoon>();
            int frames = Main.projFrames[type];

            int frameHeight = TextureAssets.Projectile[type]
                .Value.Height / frames;

            if (++NPC.frameCounter >= 5)
            {
                NPC.frameCounter = 0;
                NPC.frame.Y += frameHeight;

                if (NPC.frame.Y >= frameHeight * frames)
                    NPC.frame.Y = 0;
            }
        }
    }
}
