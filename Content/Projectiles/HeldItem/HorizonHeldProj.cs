using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Mono.Cecil;
using ReLogic.Content;
using RemnantOfTheAncientsMod.Content.Projectiles.Mage;
using System.Security.Cryptography.X509Certificates;
using Terraria;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Content.Projectiles.HeldItem
{

    public class HorizonHeldProj : ModProjectile
	{
        public override string Texture => "RemnantOfTheAncientsMod/Content/Projectiles/HeldItem/PlaceHolder";
        public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Curecedball"); //projectile name

		}
		public override void SetDefaults()
		{
			Projectile.width = 36;       //projectile width
			Projectile.height = 36;  //projectile height
			Projectile.friendly = true;      //make that the projectile will not damage you
			Projectile.DamageType = DamageClass.Default;          // 
			Projectile.tileCollide = false;   //make that the projectile will be destroed if it hits the terrain
			Projectile.penetrate = -1;      //how many NPC will penetrate
			Projectile.timeLeft = 110;   //how many time this projectile has before disepire
			Projectile.light = 0f;    // projectile light
			Projectile.extraUpdates = 1;
			Main.projFrames[Projectile.type] = 3;
			Projectile.ignoreWater = true;
			Projectile.aiStyle = -1;


		}
		public int speed = 0;
        public float fade = 1.6f;
        public float rotation = 0;
        public int Charge = 0;
        public override void AI()
		{
            
            Vector2 SubVelocity = new Vector2(Projectile.ai[0], Projectile.ai[1]);
            Player player = Main.player[Main.myPlayer];
            if (player.whoAmI == Main.myPlayer)
			{
				if (++speed >= 10)
				{
					if (rotation++ >= 360)
					{
						rotation = 0;
					}
					speed = 0;
				}
				Charge++;
				//Main.NewText(Charge);
				//if (Charge >= 180 / Projectile.ai[2])
				//{
				//    Projectile.Kill();
				//}

			}
                base.AI();
        }
        public override bool PreDraw(ref Color lightColor)
		{
			Color BaseColor = GetColor();
            Player player = Main.player[Main.myPlayer];
			if (player.whoAmI == Main.myPlayer)
			{
				Asset<Texture2D>[] textures = new[]
				{
					ModContent.Request<Texture2D>("RemnantOfTheAncientsMod/Content/Effects/MagicCircle/MagicCircleCenter_3"),
					ModContent.Request<Texture2D>("RemnantOfTheAncientsMod/Content/Effects/MagicCircle/MagicCircleExterior_1"),
					ModContent.Request<Texture2D>("RemnantOfTheAncientsMod/Content/Effects/MagicCircle/MagicCircleExterior_2")
				};

				Vector2[] origin = new[]
				{
					new Vector2(textures[0].Width(), textures[0].Height()) * 0.5f,
					new Vector2(textures[1].Width(), textures[1].Height()) * 0.5f,
					new Vector2(textures[2].Width(), textures[2].Height()) * 0.5f
				};

				//var texture = ModContent.Request<Texture2D>("RemnantOfTheAncientsMod/Content/Effects/MagicCircle/MagicCircleCenter_3");
				//var texture2 = ModContent.Request<Texture2D>("RemnantOfTheAncientsMod/Content/Effects/MagicCircle/MagicCircleExterior_1");
				//Vector2 origin = new Vector2(texture.Width() * 0.5f, texture.Height() * 0.5f);//0.5
				//Vector2 origin2 = new Vector2(texture2.Width() * 0.5f, texture2.Height() * 0.5f);//0.5

				if (Projectile.ai[0] <= 170)
				{
					Color color = new Color(BaseColor.R, BaseColor.G, BaseColor.B, 20) * fade;
					Main.spriteBatch.Draw((Texture2D)textures[0], player.Center - Main.screenPosition, null, color, rotation, origin[0], 1.2f, SpriteEffects.None, 0f);

					Main.spriteBatch.Draw((Texture2D)textures[1], player.Center - Main.screenPosition, null, color, -rotation, origin[1], 1.4f, SpriteEffects.None, 0f);

                    Main.spriteBatch.Draw((Texture2D)textures[2], player.Center - Main.screenPosition, null, color, rotation, origin[2], 2.4f, SpriteEffects.None, 0f);
                }
			}
			return true;
		}
		public Color GetColor()
		{

			return Main.DiscoColor;
			
		}
	}
}
