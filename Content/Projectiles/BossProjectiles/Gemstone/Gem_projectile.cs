using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using RemnantOfTheAncientsMod.Common.UtilsTweaks;
using SangarUtilities.Common.UtilsTweaks;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Content.Projectiles.BossProjectiles.Gemstone
{
    public class GemstoneCrusherProj_Emerald : GemstoneCrusherProj
    {
        public override int GemId => ItemID.Emerald;
        public override void SetDefaults()
        {
            base.SetDefaults();
            Projectile.penetrate = -1;
            Projectile.tileCollide = true;
            Projectile.timeLeft = 900;
        }

        public override void AI()
        {
            Projectile.rotation += 0.1f * Projectile.direction;
            Projectile.velocity.Y += 0.2f;
            base.AI();
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            float bounce = (DificultyUtils.MasochistMode && Projectile.hostile) ? 1.1f : 0.9f;

            if (Projectile.velocity.X != oldVelocity.X)
                Projectile.velocity.X = -oldVelocity.X * bounce;

            if (Projectile.velocity.Y != oldVelocity.Y)
                Projectile.velocity.Y = -oldVelocity.Y * bounce;

            if (Math.Abs(Projectile.velocity.Y) > 12) Projectile.velocity.Y = 6 * Math.Sign(Projectile.velocity.Y);
            if (Math.Abs(Projectile.velocity.X) > 12) Projectile.velocity.X = 6 * Math.Sign(Projectile.velocity.X);
            return false;
        }
    }
    public class GemstoneCrusherProj_Sapphire : GemstoneCrusherProj
    {
        public override int GemId => ItemID.Sapphire;
        public override void SetDefaults()
        {
            base.SetDefaults();
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            Projectile.timeLeft = 900;
        }
        public override void AI()
        {
            
            Projectile.rotation -= 0.1f * Projectile.direction;
            int ownerIndex = (int)Projectile.ai[0];
            Entity owner = Projectile.friendly ? Main.player[Projectile.owner]:Main.npc[ownerIndex];
            if (!owner.active)
            {
                Projectile.Kill();
                return;
            }

            int count = 0;
            int index = 0;

            for (int i = 0; i < Main.maxProjectiles; i++)
            {
                Projectile p = Main.projectile[i];

                if (p.active && p.type == Projectile.type && (int)p.ai[0] == owner.whoAmI)
                {
                    if (i == Projectile.whoAmI) index = count;
                    count++;
                }
            }

            if (count <= 0) return;

            float rotationSpeed = 0.03f;
            float angle = Main.GameUpdateCount * rotationSpeed + MathHelper.TwoPi * index / count;

            float radius = Math.Max(owner.width, owner.height) * 2f;
            Projectile.Center = owner.Center + angle.ToRotationVector2() * radius;

            base.AI();
        }
    }
    public class GemstoneCrusherProj_Ruby : GemstoneCrusherProj
    {
        public override int GemId => ItemID.Ruby;
        public override void SetDefaults()
        {
            base.SetDefaults();
            Projectile.timeLeft = Utils1.FormatTimeToTick(Second:3);
        }

        public override void AI()
        {
            Projectile.rotation += 0.1f * Projectile.direction;
            Projectile.velocity.Y += 0.3f;
            base.AI();
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.OnFire, Utils1.FormatTimeToTick(Second: 2));
            base.OnHitNPC(target, hit, damageDone);
        }
        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            target.AddBuff(BuffID.OnFire, Utils1.FormatTimeToTick(Second: 2));
            base.OnHitPlayer(target, info);
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            float bounce = (Projectile.hostile && DificultyUtils.InfernumMode) ? 0.7f : 0.1f;
            if (Projectile.velocity.X != oldVelocity.X)
                Projectile.velocity.X = -oldVelocity.X * bounce;

            if (Projectile.velocity.Y != oldVelocity.Y)
                Projectile.velocity.Y = -oldVelocity.Y * bounce;

            return false;
        }
    }
    public class GemstoneCrusherProj_Diamond : GemstoneCrusherProj
    {
        public override int GemId => ItemID.Diamond;
        public override void SetDefaults()
        {
            Projectile.penetrate = -1;
            base.SetDefaults();
        }
        private Vector2 oldVelocity = Vector2.Zero;
        public override void AI()
        {
            Projectile.rotation += 0.1f * Projectile.direction;
            
            if(Projectile.ai[1] >= Utils1.FormatTimeToTick(Second:1))
            {
                Projectile.velocity = oldVelocity;
            }
            else
            {
                Projectile.ai[1]++;
                Projectile.velocity = Vector2.Zero;
            }
            base.AI();
        }
        public override void OnSpawn(IEntitySource source)
        {
            oldVelocity = Projectile.velocity;
            base.OnSpawn(source);
        }
    }

    public abstract class GemstoneCrusherProj : ModProjectile
    {
        public abstract int GemId { get; }
        public override string Texture => getTexture(GemId);
        public override void SetDefaults()
        {
            Projectile.width = 18;
            Projectile.height = 18;
            Projectile.friendly = false;
            Projectile.hostile = true;
            Projectile.tileCollide = true;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 200;
            Projectile.light = 0f;
            Projectile.extraUpdates = 1;
            Projectile.ignoreWater = true;
            Projectile.scale = 1f;
            AIType = ProjectileID.Bullet;
        }

        static string getTexture(int GemId)
        {
            return "Terraria/Images/Item_" + GemId;
        }
        public override void PostDraw(Color lightColor)
        {
            /* Type type = GetType();
             String ruta = type.FullName.Replace('.', '/');
             string Texture = $"{ruta}_Glow";


             if (Texture != null)
             {


                 Texture2D texture = ModContent.Request<Texture2D>(Texture, AssetRequestMode.ImmediateLoad).Value;
                 Vector2 pos = Projectile.Center - Main.screenPosition;
                 Rectangle source = new(0, 0, texture.Width, texture.Height);
                 Main.spriteBatch.Draw(texture,pos, source, Color.White, Projectile.rotation, texture.Size() * 0.5f, Projectile.scale,SpriteEffects.None,0f);
             }
             base.PostDraw(lightColor);*/

            Type type = GetType();
            string ruta = type.FullName.Replace('.', '/');
            string Texture = $"{ruta}_Glow";

            Texture2D texture = ModContent.Request<Texture2D>(
                Texture,
                AssetRequestMode.ImmediateLoad
            ).Value;

            Vector2 pos = Projectile.Center - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY);

            Vector2 origin = new(
                texture.Width * 0.5f,
                texture.Height * 0.5f
            );

            Main.spriteBatch.Draw(
                texture,
                pos,
                null,
                Color.White,
                Projectile.rotation,
                origin,
                Projectile.scale,
                SpriteEffects.None,
                0f
            );

            base.PostDraw(lightColor);
        }
    }
}