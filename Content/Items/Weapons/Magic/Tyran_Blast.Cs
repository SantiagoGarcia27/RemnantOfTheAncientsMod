using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.GameContent.Creative;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using RemnantOfTheAncientsMod.Content.Projectiles.BossProjectile;

namespace RemnantOfTheAncientsMod.Content.Items.Weapons.Magic
{
	public class Tyran_Blast : ModItem
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Tyrant blast");
			//DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Polish), "Wybuch tyrana");
			//DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Explosion de tyran");
			//DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Rafaga tirana");
			Item.staff[Item.type] = true;
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
			Item.damage = 60;
			Item.noMelee = true;
			Item.DamageType = DamageClass.Magic;
			Item.channel = true;
			Item.mana = 9;
			Item.rare = ItemRarityID.Lime;
			Item.width = 72;
			Item.height = 74;
			Item.useTime = 15;
			Item.UseSound = SoundID.Item34;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.shootSpeed = 14f;
			Item.useAnimation = 15;
			Item.shoot = ModContent.ProjectileType<InfernalBall_f>();
			Item.value = Item.sellPrice(gold: 5);
			Item.autoReuse = true;
		}

		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			Vector2 target = Main.screenPosition + new Vector2(Main.mouseX, Main.mouseY);
			float ceilingLimit = target.Y;
			for (int i = 0; i < 3; i++)
			{
				position = player.Center + new Vector2((-(float)Main.rand.Next(0, 401) * player.direction), -10f);
				position.Y -= (100 * i);
				Vector2 heading = target - position;
				heading.Normalize();
				heading *= new Vector2(velocity.X, velocity.Y).Length();
				velocity.X = heading.X;
				velocity.Y = heading.Y + Main.rand.Next(-40, 41) * 0.02f;
				Projectile.NewProjectile(source, position.X, position.Y, velocity.X, velocity.Y, type, damage * 2, knockback, player.whoAmI, 0f, ceilingLimit);
				if (Main.rand.NextFloat() <= (float)1 / 5) Projectile.NewProjectile(source, position.X, position.Y, velocity.X, velocity.Y, ModContent.ProjectileType<InfernalBallF_f>(), Item.damage * 2, 20, player.whoAmI);
			}
			return false;
		}
	}
}