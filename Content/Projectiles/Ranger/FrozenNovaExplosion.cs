using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RemnantOfTheAncientsMod.Common.UtilsTweaks;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Content.Projectiles.Ranger
{
	public class FrozenNovaExplosion : ModProjectile
	{
		public override void SetStaticDefaults()
		{
           
        }

		public override void SetDefaults()
		{
			Projectile.width = 60;
			Projectile.height = 60;
			Projectile.aiStyle = 1;
			Projectile.friendly = true;
			Projectile.hostile = false;
			Projectile.DamageType = DamageClass.Ranged;
			Projectile.penetrate = 1000;
			Projectile.timeLeft = 800;
			Projectile.alpha = 255;
			Projectile.light = 1.5f;
			Projectile.ignoreWater = true;
			Projectile.tileCollide = false;
			Projectile.extraUpdates = 1;
			AIType = -1;
            Main.projFrames[Projectile.type] = 7;
        }

		int MaxFrameCounter;
		public override void AI()
		{
			MaxFrameCounter = (int)Utils1.FormatTimeToTick(0, 0, 0, 3) / Main.projFrames[Projectile.type];
			Projectile.velocity = Vector2.Zero;
			if (Projectile.frameCounter++ % MaxFrameCounter == 0) 
			{
				Projectile.frame++;
                if (Projectile.frame == 7) Projectile.Kill();
			}
		}
        public override void OnSpawn(IEntitySource source)
        {
			//Projectile.frame = 1;
            base.OnSpawn(source);
        }
		public override bool PreDraw(ref Color lightColor)
		{
			SpriteEffects effects = SpriteEffects.None;
			if (Projectile.spriteDirection == 1)
			{
				effects = SpriteEffects.FlipHorizontally;
			}
			Vector2 vectorFrame = new(TextureAssets.Projectile[Projectile.type].Value.Width / 2, TextureAssets.Projectile[Projectile.type].Value.Height / Main.projFrames[Projectile.type] / 2);
			Vector2 position = new Vector2(Projectile.Center.X, Projectile.Center.Y) - Main.screenPosition;
			var texture = ModContent.Request<Texture2D>("RemnantOfTheAncientsMod/Content/Projectiles/Ranger/FrozenNovaExplosion");
			position -= new Vector2(texture.Width(), texture.Height() / (Projectile.frameCounter + 1)) / 2f;
			position.X += (vectorFrame * 1f + new Vector2(0f, 4f + Projectile.gfxOffY)).X;
			Color color = Utils.MultiplyRGBA(new Color(Color.White.R, Color.White.G, Color.White.B, Projectile.alpha), Color.White);
			int frameHeight = texture.Height() / Main.projFrames[Projectile.type];
			int startY = frameHeight * Projectile.frame;
			Rectangle sourceRectangle = new(0, startY, texture.Width(), frameHeight);
			Main.spriteBatch.Draw((Texture2D)texture, position, sourceRectangle, color, Projectile.rotation, vectorFrame, Projectile.scale, effects, 0f);

			return false;
		}
		
		public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
		{
			target.AddBuff(Projectile.ai[2] == 0 ? BuffID.Frostburn2 : BuffID.Frostburn, 300);
			if (Main.rand.NextBool(2)) target.AddBuff(BuffID.Slow, 300);
		}
	}
}