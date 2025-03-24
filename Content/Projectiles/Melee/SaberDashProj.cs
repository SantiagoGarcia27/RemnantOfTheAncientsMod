using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RemnantOfTheAncientsMod.Common.Global.Items.WeaponsModels;
using RemnantOfTheAncientsMod.Common.UtilsTweaks;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Content.Projectiles.Melee
{
    public class SaberDashProj : ModProjectile
	{
        public override string Texture => getTextue();
        public override void SetStaticDefaults()
		{
        }
		public override void SetDefaults()
		{
            Item item = ContentSamples.ItemsByType[(int)Projectile.ai[2]];
            Projectile.width = 22;
			Projectile.height = 22;
			Projectile.aiStyle = -1;
			Projectile.friendly = true;
			Projectile.penetrate = -1;
			Projectile.timeLeft = 1000;
			Projectile.tileCollide = false;
			Projectile.hide = false;
			Projectile.DamageType = DamageClass.Melee;

        }

		float counter = 0;
		int counterMax = 0;
        bool FullCharge = false;
        public override void AI()
		{
			Player player = Main.player[Projectile.owner];
            Item BaseItem = ContentSamples.ItemsByType[(int)Projectile.ai[2]];
			

            Projectile.Center = player.Center;

			if (Main.mouseRightRelease && !Main.mouseRight)
			{
				Projectile.Kill();
			}

			if (Projectile.rotation <= 2 && Projectile.rotation >= -2)
			{
				float chargeSpeedBonus = player.GetAttackSpeed(DamageClass.Melee);
                counter += 0.02f * player.direction * chargeSpeedBonus;
				Projectile.rotation = counter;
            }

			else
			{
				Color color = new Color(255, 255, 255);
				if (!FullCharge)
				{
					Lighting.AddLight(Projectile.Center, TorchID.Bone);
					Rectangle rectangle = new Rectangle((int)(Projectile.Center.X - 10), (int)(Projectile.Center.Y - 10), 20, 20);
					CombatText.NewText(rectangle, Color.Cyan, "!!!", true);
					FullCharge = true;
                }

				if (Main.mouseLeft)
				{
					DashPlayer.JumpDash(player, Projectile.ai[0], Projectile.ai[1]);
					counter = 0;

					Vector2 velocity = player.velocity * 0.9f;
					velocity.Y = 0;

                   

                    Projectile.NewProjectile(Projectile.GetSource_None(), Projectile.Center, velocity, ModContent.ProjectileType<SwordSlash>(), Projectile.damage, Projectile.knockBack, Main.myPlayer, Projectile.ai[0], Projectile.ai[1]);
                    SaberGlobalItem.DashEffect(player, (int)Projectile.ai[2]);
                    Projectile.Kill();
				}
			}
		}
        public override void OnSpawn(IEntitySource source)
        {
            counterMax = getMaxCouter();
            base.OnSpawn(source);
        }
        private  int getMaxCouter()
		{
			int maxCounter = (int)Utils1.FormatTimeToTick(0, 0, 0, 5);
            Item itemBase = ContentSamples.ItemsByType[(int)Projectile.ai[2]];
            maxCounter -= itemBase.rare * 2;
			return maxCounter;
        }
		public string getTextue()
		{
            Item item = ContentSamples.ItemsByType[(int)Projectile.ai[2]];
            string Texture = item.type < ItemID.Count ? "Terraria/Images/Item_" + item.type : ItemLoader.GetItem(item.type).Texture;
			return Texture;
        }

        public override void PostDraw(Color lightColor)
        {
            Player player = Main.player[Projectile.owner];

			SpriteEffects effect = player.direction == 1 ? SpriteEffects.FlipVertically : SpriteEffects.FlipVertically | SpriteEffects.FlipHorizontally;
			Texture2D texture = ModContent.Request<Texture2D>(Texture).Value;
			Main.spriteBatch.Draw(texture, Projectile.Center - Main.screenPosition, null, Color.White, Projectile.rotation , texture.Size() * 0.5f, Projectile.scale, effect, 1f);
            base.PostDraw(lightColor);
        }
    }
}