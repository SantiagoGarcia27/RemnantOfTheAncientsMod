using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria.Audio;
using Terraria.GameContent;
using RemnantOfTheAncientsMod.Content.Buffs.Debuff;
using RemnantOfTheAncientsMod.Common.Global;
using static System.Net.Mime.MediaTypeNames;
using RemnantOfTheAncientsMod.Common.UtilsTweaks;
using Terraria.DataStructures;
using System.IO;

namespace RemnantOfTheAncientsMod.Content.Projectiles.Ranger
{
	public class TrueHallowArrow : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Desert Arrow");     //The English name of the projectile
			ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5;    //The length of old position to be recorded
			ProjectileID.Sets.TrailingMode[Projectile.type] = 0;        //The recording mode
		}

		public override void SetDefaults()
		{
			Projectile.width = 8;         
			Projectile.height = 8;
			Projectile.aiStyle = 1;
			Projectile.friendly = true;
			Projectile.hostile = false;
			Projectile.DamageType = DamageClass.Ranged;
			Projectile.penetrate = 1;
			Projectile.timeLeft = 800;
			Projectile.alpha = 0;
			Projectile.light = 0.5f;
			Projectile.ignoreWater = true;
			Projectile.tileCollide = true;
			Projectile.extraUpdates = 1;
			AIType = ProjectileID.WoodenArrowFriendly;
		}

		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			Projectile.penetrate--;
			if (Projectile.penetrate <= 30)
			{
				Projectile.Kill();
			}
			else
			{
				Collision.HitTiles(Projectile.position, Projectile.velocity, Projectile.width, Projectile.height);
				SoundEngine.PlaySound(SoundID.Item10, Projectile.position);

				// If the projectile hits the left or right side of the tile, reverse the X velocity
				if (Math.Abs(Projectile.velocity.X - oldVelocity.X) > float.Epsilon)
				{
					Projectile.velocity.X = -oldVelocity.X;
				}

				// If the projectile hits the top or bottom side of the tile, reverse the Y velocity
				if (Math.Abs(Projectile.velocity.Y - oldVelocity.Y) > float.Epsilon)
				{
					Projectile.velocity.Y = -oldVelocity.Y;
				}
			}

			return false;
		}
        Color MainColor = Color.Pink;
        public override bool PreDraw(ref Color lightColor)
		{
			Main.instance.LoadProjectile(Projectile.type);
			Texture2D texture = TextureAssets.Projectile[Projectile.type].Value;

			// Redraw the projectile with the color not influenced by light
			Vector2 drawOrigin = new Vector2(texture.Width * 0.5f, Projectile.height * 0.5f);
            string path = "RemnantOfTheAncientsMod/Content/Projectiles/Ranger/TrueHallowArrow";

            if (MainColor == Color.SkyBlue)
                path += "Alt";
            for (int k = 0; k < Projectile.oldPos.Length; k++)
			{
				
				texture = ModContent.Request<Texture2D>(path).Value;
				Vector2 drawPos = (Projectile.oldPos[k] - Main.screenPosition) + drawOrigin + new Vector2(0f, Projectile.gfxOffY);
				Color color = Projectile.GetAlpha(lightColor) * ((Projectile.oldPos.Length - k) / (float)Projectile.oldPos.Length);
				Main.EntitySpriteDraw(texture, drawPos, null, color, Projectile.rotation, drawOrigin, Projectile.scale, SpriteEffects.None, 0);
			}
            DrawGlowMask(ref MainColor, texture);
            void DrawGlowMask(ref Color MainColor, Texture2D texture)
            {
                SpriteEffects effects = SpriteEffects.None;
                if (Projectile.spriteDirection == 1)
                {
                    effects = SpriteEffects.FlipHorizontally;
                }
                Vector2 vectorFrame = new(TextureAssets.Projectile[Projectile.type].Value.Width / 2, TextureAssets.Projectile[Projectile.type].Value.Height / 2);
                Vector2 position = new Vector2(Projectile.Center.X, Projectile.Center.Y) - Main.screenPosition;

                texture = ModContent.Request<Texture2D>(path + "_Glow").Value;
                position -= new Vector2(texture.Width, texture.Height) / 2f;
                position += vectorFrame * 1f + new Vector2(0f, 4f + Projectile.gfxOffY);
                Color color = Utils.MultiplyRGBA(MainColor, Color.LightYellow);
                Main.spriteBatch.Draw(texture, position, null, color, Projectile.rotation, vectorFrame, Projectile.scale, effects, 0f);
            }
            return false;
		}
		
		public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
		{
			for(int i = 0; i < 360; i += 5)
			{
				
				Vector2 velocity = Projectile.velocity.RotatedBy(MathHelper.ToRadians(i));
				int DustType = DustID.PinkFairy;

                if (MainColor == Color.SkyBlue)
                    DustType = DustID.BlueFairy;

				Dust.NewDust(target.Center,1,1, DustType, velocity.X /4,velocity.Y/ 4,0, MainColor, 2f);
			}
			var p = Projectile.NewProjectile(Projectile.GetSource_FromThis(), target.position, Vector2.Zero, ModContent.ProjectileType<DamageHitbox>(), damageDone/4, 0, Main.myPlayer);
			Main.projectile[p].Size = new Vector2(140, 140) * 2f;
            Main.projectile[p].penetrate = -1;
            Main.projectile[p].timeLeft = (int)Utils1.FormatTimeToTick(Second:1);
            Main.projectile[p].usesLocalNPCImmunity = true;
            Main.projectile[p].localNPCHitCooldown = -1;
        }
        public override void OnSpawn(IEntitySource source)
        {
			if (Main.rand.NextBool(2))
				MainColor = Color.SkyBlue;
			else
				MainColor = Color.Pink;
            base.OnSpawn(source);
        }
        public override void AI()
		{
			Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 1.00f;
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(-90f);
			Projectile.GetGlobalProjectile<RemnantGlobalProjectile>().HommingProjectile(Projectile,Range:400,speed:10f,rotationBonus:90f);
        }
	}
}
	
