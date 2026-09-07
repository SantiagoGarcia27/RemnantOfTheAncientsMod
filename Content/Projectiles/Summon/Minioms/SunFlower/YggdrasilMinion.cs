using FargowiltasSouls.Common.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RemnantOfTheAncientsMod.Common.UtilsTweaks;
using RemnantOfTheAncientsMod.Content.Buffs.Buffs.Minions;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;


namespace RemnantOfTheAncientsMod.Content.Projectiles.Summon.Minioms.SunFlower
{
    public class YggdrasilMinion : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 3;
            ProjectileID.Sets.MinionTargettingFeature[Projectile.type] = true;
            Main.projPet[Projectile.type] = true;
            ProjectileID.Sets.SentryShot[Projectile.type] = true;
            ProjectileID.Sets.MinionSacrificable[Projectile.type] = true;
        }
        public sealed override void SetDefaults()
        { 
            Projectile.width = 30;
            Projectile.height = 60;
            Projectile.sentry = true;
            Projectile.minionSlots = 1;
            Projectile.penetrate = -1;
            Projectile.alpha = 100;
            Projectile.manualDirectionChange = true;
            Projectile.netImportant = true;
            Projectile.timeLeft = 36000;
            Projectile.light = 3;
        }
        public override bool? CanCutTiles()
        {
            return false;
        }
        public override bool MinionContactDamage()
        {
            return true;
        }
        public int RangeMax = 35 * 16;
        public int HealTimmer = Utils1.FormatTimeToTick(0, 0, 0, 7);
        public int Heal = 30;
        public int DefenseBonus = 15;
        public float DamageBonus = 1.10f;
        public float DamageReductionBonus = 0.05f;


        float Interpolation = 0.5f;
        float InterpolationOpacity = 0.5f;
        bool IncreaseInterpolation = true;
        bool IncreaseInterpolationOpacity = true;
        float AuraRotation = 0f;
        public override void AI()
        {
            Projectile.Size = new Vector2(TextureAssets.Projectile[Projectile.type].Value.Width, TextureAssets.Projectile[Projectile.type].Value.Height /3.1f);
            Player player = Main.player[Projectile.owner];

            CheckActive(player);
            Projectile.velocity = new Vector2(0, 7f);
            if(HealTimmer == 0)
            {
                HealTimmer = Utils1.FormatTimeToTick(0, 0, 0, 7);
            }
            else
            {
                HealTimmer--;
            }
            if (RemnantOfTheAncientsMod.ParticleMeter(3) != 0)
            {
                AnimateTexture();
            }

            if (HealTimmer % 3 == 0)
            {
                if (Interpolation >= 1f) IncreaseInterpolation = false;
                if (Interpolation <= 0f) IncreaseInterpolation = true;
                Interpolation += 0.1f * (IncreaseInterpolation ? 1f : -1f);
            }
            if (HealTimmer % 4 == 0)
            {
                if (InterpolationOpacity >= 1f) IncreaseInterpolationOpacity = false;
                if (InterpolationOpacity <= 0f) IncreaseInterpolationOpacity = true;
                InterpolationOpacity += 0.01f * (IncreaseInterpolationOpacity ? 1f : -1f);
            }
            AuraRotation += 0.01f;

            for (int p = 0; p < Main.maxPlayers; p++)
            {
                bool playerAvalible = Main.player[p].active && !Main.player[p].dead;
                bool playerOnRange = Projectile.WithinRange(Main.player[p].Center, RangeMax);

                if (playerAvalible && playerOnRange)
                {
                    Main.player[p].AddBuff(BuffID.Sunflower, Utils1.FormatTimeToTick(0, 0, 0, 4));
                    if (HealTimmer == 1)
                    {
                        Main.player[p].HealEffect(Heal);
                        Main.player[p].statLife += Heal;
                        SpawnParticles();
                    }
                    Main.player[p].AddBuff(BuffType<YggdrasilBuff>(), Utils1.FormatTimeToTick(0, 0, 0, 4));
                }
            } 
        }
        public void AnimateTexture()
        {
            if (++Projectile.frameCounter >= 9)
            {
                Projectile.frameCounter = 0;
                if (++Projectile.frame >= Main.projFrames[Projectile.type])
                    Projectile.frame = 0;
            }
        }
        public void SpawnParticles()
        {
            Vector2 offset = new Vector2(Projectile.width / 2, Projectile.height / 2) + new Vector2(24, 24);//24
            for (int i = 0; i <RemnantOfTheAncientsMod.ParticleMeter(90); i++)
            {
                Vector2 dustPos = Projectile.position - new Vector2(24, 24) + offset + new Vector2(RangeMax * 16, 0).RotatedBy(MathHelper.ToRadians(18 * i));//60
                var d = Dust.NewDustPerfect(dustPos, DustID.GrassBlades, Vector2.Zero);
                d.noLight = false;
                d.noGravity = true;
            }
        }
        public void CheckActive(Player player)
        {
            if (player.HasBuff(BuffType<YggdrasilMinionBuff>()))
            {
                Projectile.timeLeft = 2;
            }
        }
        Color auraColor;

        float waveScale = 0f;
        public override bool PreDraw(ref Color lightColor)
        {
            var texture = Request<Texture2D>("RemnantOfTheAncientsMod/Content/Projectiles/Summon/Minioms/AreaEffect_Ygdrasil");
           // Vector2 origin = new Vector2(texture.Width() * 0.5f, texture.Height() * 0.5f);//0.5
            Vector2 origin = texture.Size() * 0.5f;

            auraColor = Color.Lerp(new Color(48, 107, 0, 0), new Color(42, 89, 4, 1), Interpolation);
            auraColor *= float.Lerp(0.3f,0.5f, Interpolation);
            Main.EntitySpriteDraw((Texture2D)texture, Projectile.Center - Main.screenPosition, null, auraColor, AuraRotation, origin, 4.5f, SpriteEffects.None, 1f);//4.5

            if (waveScale < 3.5) waveScale += 0.01f;
            else waveScale = 0f;

            var textureWave = Request<Texture2D>("RemnantOfTheAncientsMod/Content/Projectiles/Summon/Minioms/AreaEffect_Wave");
            Main.EntitySpriteDraw((Texture2D)textureWave, Projectile.Center - Main.screenPosition, null, auraColor, AuraRotation, origin, waveScale, SpriteEffects.None, 1f);

            return true;
        }

      
        public override void DrawBehind(int index, List<int> behindNPCsAndTiles, List<int> behindNPCs, List<int> behindProjectiles, List<int> overPlayers, List<int> overWiresUI)
        {
            overPlayers.Add(index);
            base.DrawBehind(index, behindNPCsAndTiles, behindNPCs, behindProjectiles, overPlayers, overWiresUI);
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.immune[Projectile.owner] = 2;
        }
        public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough, ref Vector2 hitboxCenterFrac)
        {
            fallThrough = false;
            return base.TileCollideStyle(ref width, ref height, ref fallThrough, ref hitboxCenterFrac);
        }
    }
}