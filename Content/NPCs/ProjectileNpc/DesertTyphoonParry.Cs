using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.ItemDropRules;
using Terraria.ModLoader.Utilities;
using RemnantOfTheAncientsMod.Content.Items.Items;
using RemnantOfTheAncientsMod.Content.Dusts;
using System;
using Microsoft.Xna.Framework;

namespace RemnantOfTheAncientsMod.Content.NPCs
{
    public class DesertTyphoonParry : ModNPC
    {
        public override string Texture => "RemnantOfTheAncientsMod/Content/Projectiles/Textures/DesertTyphoon";
        public override void SetStaticDefaults()
        {
            //DisplayName.SetDefault("Armored Slime");
            //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Slime blindado");
            //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Slime blindé");
            Main.npcFrameCount[Type] = 4;
            NPC.spriteDirection = NPC.direction;
        }

        public override void SetDefaults()
        {
            NPC.lifeMax = 10;
            NPC.damage = 1;
            NPC.width = 36;
            NPC.height = 36;
            NPC.friendly = false;
          //  NPC.hostile = true;
           // NPC.DamageType = DamageClass.Magic;
            NPC.noTileCollide = false;
           // NPC.penetrate = PenetrateKill;
            NPC.timeLeft = 200;
            NPC.aiStyle = -1;
            NPC.noGravity = true;
            //NPC.light = 0.75f;
            //NPC.extraUpdates = 1;
            //NPC.ignoreWater = true;
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
            for (int i = 0; i < ModContent.GetInstance<RemnantOfTheAncientsMod>().ParticleMeter(5); i++)
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
        public void AnimateTexture()
        {
            frameCounter++;
            if (frameCounter >= Main.projFrames[NPC.type])
            {
                frameCounter = 0;
            }
            NPC.frame.Y =frameCounter;
        }
    }
}
