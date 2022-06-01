using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using Microsoft.Xna.Framework;
using opswordsII.Projectiles;
using Terraria.DataStructures;

namespace opswordsII.Items.Magic
{
	public class Tyran_Blast : ModItem
	{
		public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("Tyrant blast");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Polish), "Wybuch tyrana");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Explosion de tyran");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Rafaga tirana");
			Item.staff[Item.type] = true;
		}

		public override void SetDefaults()
		{
			Item.damage = 60;
			Item.noMelee = true;
			Item.DamageType = DamageClass.Magic;
			Item.channel = true; //Channel so that you can held the weapon [Important]
			Item.mana = 9;
			Item.rare = 7;
			Item.width = 46;
			Item.height = 54;
			Item.useTime = 15;
			Item.UseSound = SoundID.Item34;
			Item.useStyle = ItemUseStyleID.HoldUp;
			Item.shootSpeed = 14f;
			Item.useAnimation = 15;
			Item.shoot = ModContent.ProjectileType<InfernalBall_f>();
			Item.value = Item.sellPrice(gold: 5);
			Item.autoReuse = true;
		}

		/*public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			Vector2 target = Main.screenPosition + new Vector2((float)Main.mouseX, (float)Main.mouseY);
			float ceilingLimit = target.Y;
			/*if (ceilingLimit > player.Center.Y - 200f)//200
			{
				ceilingLimit = player.Center.Y - 200f;
			}
			for (int i = 0; i < 3; i++)
			{
				float numberProjectiles = 1 + Main.rand.Next(1);
				position = player.Center + new Vector2((-(float)Main.rand.Next(0, 401) * player.direction), -10f);//-600
				position.Y -= (100 * i);//100
				float rotation = MathHelper.ToRadians(4);
				Vector2 heading = target - position;
				heading.Normalize();
				heading *= new Vector2(perturbedSpeed).Length();
				Vector2 perturbedSpeed = velocity.RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))) * .2f;
				speedX = heading.X;
				speedY = heading.Y + Main.rand.Next(-40, 41) * 0.02f;
				Projectile.NewProjectile(source, position, perturbedSpeed, 336, damage, knockback, player.whoAmI, 0f, ceilingLimit);
				if (Main.rand.NextFloat () <= (float) 1 / 5) Projectile.NewProjectile(source, position, perturbedSpeed, ModContent.ProjectileType<InfernalBallF_f>(), Item.damage*2, 20, player.whoAmI);
			}
			return false;
		}*/
	}
}
