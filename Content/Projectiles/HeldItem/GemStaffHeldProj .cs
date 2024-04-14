using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RemnantOfTheAncientsMod.Content.Projectiles.Mage;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Content.Projectiles.HeldItem
{

    public class GemStaffHeldProj : ModProjectile
	{
        public override string Texture => RemnantOfTheAncientsMod.PlaceHolderPath;
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
        public float fade = 1.6f;
        public float rotation = 0;
        public int Charge = 0;
        Vector2 SubVelocity = new Vector2(0, 0);
        public override void AI()
		{
            Player player = Main.player[Projectile.owner];
            if (player.whoAmI == Main.myPlayer)
            {
                if (Projectile.velocity.X != 0 || Projectile.velocity.X != 0)
                {
                    SubVelocity = Projectile.velocity;
                }
                Projectile.velocity = Vector2.Zero;

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
                    int p = Projectile.NewProjectile(Projectile.GetSource_FromAI(), player.position - new Vector2(0f,1.5f)*16, SubVelocity * 2, ModContent.ProjectileType<BigGemBolt>(), (int)(player.HeldItem.damage * 4f), player.HeldItem.knockBack, player.whoAmI);
                    Main.projectile[p].Size *= 2f;
                    Main.projectile[p].scale = 3f;
                    Main.projectile[p].stepSpeed = 10f;

                    switch (Projectile.ai[0])
                    {
                        case ProjectileID.AmethystBolt:
                            Main.projectile[p].localAI[0] = ItemID.LargeAmethyst;
                            Main.projectile[p].localAI[1] = DustID.GemAmethyst;
                            break;
                        case ProjectileID.TopazBolt:
                            Main.projectile[p].localAI[0] = ItemID.LargeTopaz;
                            Main.projectile[p].localAI[1] = DustID.GemTopaz;
                            break;
                        case ProjectileID.EmeraldBolt:
                            Main.projectile[p].localAI[0] = ItemID.LargeEmerald;
                            Main.projectile[p].localAI[1] = DustID.GemEmerald;
                            break;
                        case ProjectileID.SapphireBolt:
                            Main.projectile[p].localAI[0] = ItemID.LargeSapphire;
                            Main.projectile[p].localAI[1] = DustID.GemSapphire;
                            break;
                        case ProjectileID.RubyBolt:
                            Main.projectile[p].localAI[0] = ItemID.LargeRuby;
                            Main.projectile[p].localAI[1] = DustID.GemRuby;
                            break;
                        case ProjectileID.DiamondBolt:
                            Main.projectile[p].localAI[0] = ItemID.LargeDiamond;
                            Main.projectile[p].localAI[1] = DustID.GemDiamond;
                            break;
                        case ProjectileID.AmberBolt:
                            Main.projectile[p].localAI[0] = ItemID.LargeAmber;
                            Main.projectile[p].localAI[1] = DustID.GemAmber;
                            break;
                    }

                    // Main.NewText("Fuego");

                    Projectile.Kill();
                }
            }

                base.AI();
        }
        public void GenerateParticlesProjectile()
        {

        }
        public override bool PreDraw(ref Color lightColor)
        {

            Player player = Main.player[Projectile.owner];
            if (player.whoAmI == Main.myPlayer)
            {
                var texture = ModContent.Request<Texture2D>("RemnantOfTheAncientsMod/Content/Effects/MagicCircle/MagicCircleCenter_5");
                var texture2 = ModContent.Request<Texture2D>("RemnantOfTheAncientsMod/Content/Effects/MagicCircle/MagicCircleExterior_1");
                Vector2 origin = new Vector2(texture.Width() * 0.5f, texture.Height() * 0.5f);//0.5
                Vector2 origin2 = new Vector2(texture2.Width() * 0.5f, texture2.Height() * 0.5f);//0.5

                if (Projectile.ai[0] <= 170 || Projectile.ai[0] <= ProjectileID.AmberBolt)
                {
                    Color BaseColor = GetColor();

                    Color color = new Color(BaseColor.R, BaseColor.G, BaseColor.B, 20) * fade;
                    Main.spriteBatch.Draw((Texture2D)texture, player.Center - Main.screenPosition, null, color, rotation, origin, 1.2f, SpriteEffects.None, 0f);

                    Main.spriteBatch.Draw((Texture2D)texture2, player.Center - Main.screenPosition, null, color, -rotation, origin2, 1.2f, SpriteEffects.None, 0f);
                }
                return true;
            }
            return false;
        }
        public Color GetColor()
        {
            switch (Projectile.ai[1])
            {
                case 0: return Color.Magenta;
                case 1: return Color.Yellow;
                case 2: return Color.Green;
                case 3: return Color.Blue;
                case 4: return Color.Red;
                case 5: return Color.White;
                case 6: return Color.SandyBrown;
                default: return Color.Black;
            }
        }
	}
}
