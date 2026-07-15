using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RemnantOfTheAncientsMod.Common.UtilsTweaks;
using SangarUtilities.Common.UtilsTweaks;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Content.Projectiles.BossProjectiles.Frozen
{
    public class Frozenp : BaseFrozenP
    {
        public override bool Friendly => false;
        public override bool hostile => true;
        public override int DefenseIgnore => 0;
    }
    public class frozen_p_f : BaseFrozenP
    {
        public override bool Friendly => true;
        public override bool hostile => false;
        public override int DefenseIgnore => 0;
    }
    public class Frozenp_M : BaseFrozenP
    {
        public override bool Friendly => true;
        public override bool hostile => false;
        public override int DefenseIgnore => 6;
    }
    public abstract class BaseFrozenP : ModProjectile
    {
        public override string Texture => "RemnantOfTheAncientsMod/Content/Projectiles/Textures/Frozenp";
        public abstract bool Friendly { get; }
        public abstract bool hostile { get; }
        public abstract int DefenseIgnore { get; }

        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 4;

            base.SetStaticDefaults();
        }
        public override void SetDefaults()
        {
            Projectile.width = 36;
            Projectile.height = 36;
            Projectile.penetrate = 2;
            Projectile.timeLeft = 20000;
            Projectile.light = 1.15f;
            Projectile.extraUpdates = 1;
            Projectile.ignoreWater = true;
            Projectile.friendly = Friendly;
            Projectile.hostile = hostile;
            Projectile.tileCollide = true;
            AIType = ProjectileID.IceSpike;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.ArmorPenetration += DefenseIgnore;
            if (RemnantOfTheAncientsMod.CalamityMod != null) Projectile.tileCollide = false;
        }
        public override void AI()
        {
            Projectile.rotation = (float)Math.Atan2(Projectile.velocity.Y, Projectile.velocity.X) + 1.00f;
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(90f);
            Lighting.AddLight(Projectile.velocity, TorchID.Ice);
        }
        public override void OnKill(int timeLeft)
        {
            if (Main.rand.NextBool(4))
            {
                SoundEngine.PlaySound(SoundID.Item27 with
                {
                    Volume = 0.2f,
                    PitchVariance = 1.2f
                }, Projectile.position);
            }
            Vector2 usePos = Projectile.position;
            Vector2 rotVector = (Projectile.rotation - MathHelper.ToRadians(90f)).ToRotationVector2();
            usePos += rotVector * 16f;


            for (int i = 0; i <RemnantOfTheAncientsMod.ParticleMeter(5); i++)
            {
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Ice, 0f, 0f, 100, default, 1.5f);
            }
        } 
        public override void OnSpawn(IEntitySource source)
        {
            int maxFrames = Main.projFrames[Projectile.type];
            int frame = Main.rand.Next(maxFrames);
            Projectile.frame = frame;
            base.OnSpawn(source);
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.immune[Projectile.owner] = 0;
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D texture = TextureAssets.Projectile[Projectile.type].Value;
            Vector2 drawPos = Projectile.Center - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY);
            Rectangle frame = texture.Frame(1,Main.projFrames[Projectile.type],0,Projectile.frame);
            Vector2 drawOrigin = frame.Size() / 2f;
            lightColor = Main.rand.Next(0, 2) switch
            {
                0 => Color.Cyan,
                1 => Color.White,
                2 => Color.DarkBlue,
                _ => Color.White
            };
            for (int k = 2; k < Projectile.oldPos.Length; k++)
            {
                Vector2 drawPos2 = Projectile.oldPos[k] + Projectile.Size / 2f - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY);
                Color color = Projectile.GetAlpha(lightColor) * ((Projectile.oldPos.Length - k) / (float)Projectile.oldPos.Length);
                Main.EntitySpriteDraw(texture, drawPos2, frame, color, Projectile.rotation, drawOrigin, Projectile.scale, SpriteEffects.None, 0);
            }
            return true;
        }
    }
}