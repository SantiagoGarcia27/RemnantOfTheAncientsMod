using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RemnantOfTheAncientsMod.Common.UtilsTweaks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Content.Projectiles.HeldItem
{

    public class BloodThornHeldProj : ModProjectile
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
			Projectile.timeLeft = 210;   //how many time this projectile has before disepire
			Projectile.light = 0f;    // projectile light
			Projectile.extraUpdates = 1;
			Main.projFrames[Projectile.type] = 3;
			Projectile.ignoreWater = true;
			Projectile.aiStyle = -1;


		}
		public int speed = 0;     
        public float rotation = 0;
        public int Charge = 0;
        public override void AI()
		{
            Player player = Main.player[Main.myPlayer];
            

            if (++speed >= 10)
			{
                if (rotation++ >= 360)
                {
                    rotation = 0;
                }
                speed = 0;
			}
            Charge++;
            if (Charge >= 180)
            {       
                Projectile.position += Vector2.Normalize(Projectile.velocity) * 1f;
                
                for (int i = 0; i < 20; i++)
                {
                    Vector2 pos1 = player.position + new Vector2((i + 2 * i) * 16, 3 * 16);
                    Vector2 pos2 = player.position - new Vector2((i + 2 * i) * 16, -3 * 16);
                    pos1 = DistanceUtils.setPositionOnSolidFloor(pos1);
                    pos2 = DistanceUtils.setPositionOnSolidFloor(pos2);
                   Projectile.NewProjectile(Projectile.GetSource_FromAI(),pos1, new Vector2(0, -10f), ProjectileID.SharpTears, player.HeldItem.damage, player.HeldItem.knockBack, player.whoAmI,1,1);
                    Projectile.NewProjectile(Projectile.GetSource_FromAI(), pos2, new Vector2(0, -10f), ProjectileID.SharpTears, player.HeldItem.damage, player.HeldItem.knockBack, player.whoAmI, 1, 1);
                }
             
                
                Projectile.Kill();
            }
                base.AI();
        }
        public override bool PreDraw(ref Color lightColor)
		{
			
			Player player = Main.player[Main.myPlayer];
            var texture = ModContent.Request<Texture2D>("RemnantOfTheAncientsMod/Content/Effects/MagicCircle/MagicCircleCenter_7");
            var texture2 = ModContent.Request<Texture2D>("RemnantOfTheAncientsMod/Content/Effects/MagicCircle/MagicCircleExterior_2");
            Vector2 origin = new Vector2(texture.Width() * 0.5f, texture.Height() * 0.5f);//0.5
            Vector2 origin2 = new Vector2(texture2.Width() * 0.5f, texture2.Height() * 0.5f);//0.5

            Color colorBase = Color.DarkRed;
            if (Projectile.ai[0] <= 170)
			{
				Color color = new Color(colorBase.R, colorBase.G, colorBase.B, 20);
				Main.spriteBatch.Draw((Texture2D)texture, player.Center - Main.screenPosition, null, color, rotation, origin, 1.2f, SpriteEffects.None, 0f);

                Main.spriteBatch.Draw((Texture2D)texture2, player.Center - Main.screenPosition, null, color, -rotation, origin2, 1.9f, SpriteEffects.None, 0f);
            }

			return true;
		}
	}
}
