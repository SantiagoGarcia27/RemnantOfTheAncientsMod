using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using RemnantOfTheAncientsMod.Common.UtilsTweaks;
using RemnantOfTheAncientsMod.Content.Items.Weapons.Magic;
using RemnantOfTheAncientsMod.Content.Projectiles.BossProjectiles.Gemstone;
using Terraria;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Content.Projectiles.HeldItem
{
	public class WandOfCavernsHeldProjectile : ModProjectile
	{
		public override string Texture
		{
		
			get {
                string path = $"{base.Texture}_{Projectile.ai[0]}";
				return path;
			}
		}
		public override void SetStaticDefaults()
		{
		}

		public override void SetDefaults()
		{
			Projectile.width = (int)0.5f.ToCoordinatePosition();
			Projectile.height = 2.ToCoordinatePosition();
			Projectile.friendly = true;
			Projectile.hostile = false;
			Projectile.penetrate = -1;
			Projectile.timeLeft = 999100;
			Projectile.alpha = 0;
			Projectile.light = 0;
			Projectile.ignoreWater = true;
			Projectile.tileCollide = false;
			Projectile.extraUpdates = 1;
			AIType = -1;

		}
		float posY = 0;
		bool isAnimation;
		int animationTime = Utils1.FormatTimeToTick(Second: 2f);
        int animationTimer = 0;

    
        public override void AI()
        {
            Player owner = Main.player[Projectile.owner];
            
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(-45f);

            if (owner.HeldItem.type != ModContent.ItemType<WandOfCaverns>())
            {
                Projectile.Kill();
                return;
            }

            int offset = 1.ToCoordinatePosition();
            int offsetY = 1.ToCoordinatePosition();

            // Comienza la animación al hacer click
            if (!isAnimation && owner.controlUseItem)
            {
                isAnimation = true;
                posY = 0;
                animationTimer = 0;
            }

            // La animación completa dura animationTime
            if (isAnimation)
            {
                float speed = offsetY / (animationTime / 2f);
                animationTimer++;
                if(animationTimer < animationTime / 2) 
                    posY -= speed;
                else  {
                    posY += speed;
                    if (posY >= 0)
                    {
                        isAnimation = false;
                        shoot();
                    }
                }
            }

            Projectile.Center = owner.Center + new Vector2(offset * owner.direction,posY);

            base.AI();
        }

        public void shoot()
        {
            Player owner = Main.player[Projectile.owner];
            if (owner.ownedProjectileCounts[ModContent.ProjectileType<GemstoneCrusherProj_Sapphire>()] < 5 && owner.HeldItem.type == ModContent.ItemType<WandOfCaverns>())
            {

                int p = Projectile.NewProjectile(Projectile.GetSource_FromThis(), owner.Center + new Vector2(16 * owner.direction, 0), Vector2.Zero, ModContent.ProjectileType<GemstoneCrusherProj_Sapphire>(), 10, 0, owner.whoAmI);
                Main.projectile[p].friendly = true;
                Main.projectile[p].hostile = false;
                Main.projectile[p].timeLeft = Utils1.FormatTimeToTick(Second: 65);
            }
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D texture = ModContent.Request<Texture2D>(Texture, AssetRequestMode.ImmediateLoad).Value;
            Vector2 position = (Projectile.position - Main.screenPosition) + new Vector2(Projectile.width * 0.5f, Projectile.height - texture.Height * 0.5f + 2f);
            Main.spriteBatch.Draw(texture, position, new Rectangle(0, 0, texture.Width, texture.Height), lightColor, Projectile.rotation, texture.Size() * 0.5f, Projectile.scale, SpriteEffects.None, 0f);
            return false;
        }
	}
}
