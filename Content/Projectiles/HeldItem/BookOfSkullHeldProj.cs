using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Mono.Cecil;
using RemnantOfTheAncientsMod.Content.Projectiles.Mage;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Content.Projectiles.HeldItem
{

    public class BookOfSkullHeldProj : ModProjectile
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
            Vector2 SubVelocity = new Vector2(Projectile.ai[0], Projectile.ai[1]);

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
            if (Charge >= 180)
            {
               
                Projectile.position += Vector2.Normalize(Projectile.velocity) * 1f;
                //if (Main.rand.NextFloat() <= (float)1 / 10)
                //{
              
                   
                int p = Projectile.NewProjectile(Projectile.GetSource_FromAI(), player.position, SubVelocity * 5, ProjectileID.BookOfSkullsSkull,(int)(player.HeldItem.damage *(5f + Projectile.ai[2])), player.HeldItem.knockBack, player.whoAmI);
                Main.projectile[p].stepSpeed = 5f;
                Main.projectile[p].Size *= 2f;
                Main.projectile[p].scale = 5f;
                Main.projectile[p].GetAlpha(Color.Red);
                   // Main.NewText("Fuego");
                
                Projectile.Kill();
            }
                base.AI();
        }
        public override bool PreDraw(ref Color lightColor)
		{
			
			Player player = Main.player[Main.myPlayer];
            var texture = ModContent.Request<Texture2D>("RemnantOfTheAncientsMod/Content/Effects/MagicCircle/MagicCircleCenter_6");
            var texture2 = ModContent.Request<Texture2D>("RemnantOfTheAncientsMod/Content/Effects/MagicCircle/MagicCircleExterior_3");
            Vector2 origin = new Vector2(texture.Width() * 0.5f, texture.Height() * 0.5f);//0.5
            Vector2 origin2 = new Vector2(texture2.Width() * 0.5f, texture2.Height() * 0.5f);//0.5

            Color colorBase = Color.DarkSlateGray;
            if (Projectile.ai[0] <= 170)
			{
				Color color = new Color(colorBase.R, colorBase.G, colorBase.B, 20);
				Main.spriteBatch.Draw((Texture2D)texture, player.Center - Main.screenPosition, null, color, rotation, origin, 1.2f, SpriteEffects.None, 0f);

                Main.spriteBatch.Draw((Texture2D)texture2, player.Center - Main.screenPosition, null, color, -rotation, origin2, 1.2f, SpriteEffects.None, 0f);
            }

			return true;
		}
	}
}
