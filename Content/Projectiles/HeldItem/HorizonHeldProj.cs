using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Content.Projectiles.HeldItem
{

    public class HorizonHeldProj : ModProjectile
	{
        public override string Texture => RemnantOfTheAncientsMod.PlaceHolderPath;
        public override void SetStaticDefaults()
		{
		}
		public override void SetDefaults()
		{
			Projectile.width = 36;  
			Projectile.height = 36;
			Projectile.friendly = true;
			Projectile.DamageType = DamageClass.Default;
			Projectile.tileCollide = false;
			Projectile.penetrate = -1;
			Projectile.timeLeft = 1110;
			Projectile.light = 0f;
			Main.projFrames[Projectile.type] = 3;
			Projectile.ignoreWater = true;
			Projectile.aiStyle = -1;


		}
		public static int[] speed = [0, 0, 0, 0];
        public static int[] speedMax = [3, 4,4, 3];
        public float fade = 1.6f;
        public static float[] rotation = [0f, 0f, 0f, 0f];
        public override void AI()
		{
            Player player = Main.player[Projectile.owner];
			Projectile.Center = player.Center;
            //if (player.whoAmI == Main.myPlayer)
			{
				for (int i = 0; i < speedMax.Length; i++)
				{
                    UpdateRotation(i, speedMax[i]);
                }
            }
            base.AI();
        }
		public static void UpdateRotation(int index,int speedValue)
		{
            if (++speed[index] >= speedValue)
            {
				if (rotation[index] >= 360) rotation[index] = 0;
				else rotation[index] += 0.8f; 
                speed[index] = 0;
            }
        }
        public override bool PreDraw(ref Color lightColor)
		{
			Color BaseColor = GetColor();
            Player player = Main.player[Main.myPlayer];
			if (player.whoAmI == Main.myPlayer)
			{
				Asset<Texture2D>[] textures =
                [
                    ModContent.Request<Texture2D>("RemnantOfTheAncientsMod/Content/Effects/MagicCircle/MagicCircleCenter_3"),
					ModContent.Request<Texture2D>("RemnantOfTheAncientsMod/Content/Effects/MagicCircle/MagicCircleExterior_1"),
					ModContent.Request<Texture2D>("RemnantOfTheAncientsMod/Content/Effects/MagicCircle/MagicCircleExterior_2"),
                    ModContent.Request<Texture2D>("RemnantOfTheAncientsMod/Content/Effects/MagicCircle/MagicCircleExterior_1")
                ];

				Vector2[] origin =
                [
                    new Vector2(textures[0].Width(), textures[0].Height()) * 0.5f,
					new Vector2(textures[1].Width(), textures[1].Height()) * 0.5f,
					new Vector2(textures[2].Width(), textures[2].Height()) * 0.5f,
                    new Vector2(textures[3].Width(), textures[3].Height()) * 0.5f
                ];

				if (Projectile.ai[0] <= 170)
				{
					Color color = new Color(BaseColor.R, BaseColor.G, BaseColor.B, 20) * fade;
					Main.spriteBatch.Draw((Texture2D)textures[0], Projectile.Center - Main.screenPosition, null, color, rotation[0], origin[0], 1.2f, SpriteEffects.None, 0f);

					Main.spriteBatch.Draw((Texture2D)textures[1], Projectile.Center - Main.screenPosition, null, color, -rotation[1], origin[1], 1.2f, SpriteEffects.None, 0f);

                    Main.spriteBatch.Draw((Texture2D)textures[2], Projectile.Center - Main.screenPosition, null, color, rotation[2], origin[2], 2.1f, SpriteEffects.None, 0f);

                    Main.spriteBatch.Draw((Texture2D)textures[3], Projectile.Center - Main.screenPosition, null, color, -rotation[3], origin[3], 2.1f, SpriteEffects.None, 0f);
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
