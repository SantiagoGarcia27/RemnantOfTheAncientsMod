using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using static Terraria.ModLoader.ModContent;
using Microsoft.Xna.Framework;
using Terraria.GameContent.Creative;
using Terraria.DataStructures;

namespace RemnantOfTheAncientsMod.Content.Items.Weapons.Magic
{
	public class StarStormTome : ModItem
	{
		public override void SetStaticDefaults()
		{
			// //DisplayName.SetDefault("Star Storm Tome");
			//DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Polish), "Star Tome");
			//DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Tome étoile");
			//DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Tomo de estrellas");
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
			Item.damage = 30;
			Item.noMelee = true;
			Item.DamageType = DamageClass.Magic;
			Item.channel = true;
			Item.mana = 5;
			Item.rare = ItemRarityID.LightRed;
			Item.width = 30;
			Item.height = 32;
			Item.useTime = 19;
			Item.useStyle = ItemUseStyleID.HoldUp;
			Item.shootSpeed = 14f;
			Item.useAnimation = 19;
			Item.shoot = ProjectileID.HallowStar;
			Item.value = Item.buyPrice(gold: 10);
			Item.autoReuse = true;
		}
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			Vector2 target = Main.screenPosition + new Vector2(Main.mouseX, Main.mouseY);
			float ceilingLimit = target.Y;
			if (ceilingLimit > player.Center.Y - 200f)
			{
				ceilingLimit = player.Center.Y - 200f;
			}
			for (int i = 0; i < 5; i++)
			{
				position = player.Center + new Vector2((-(float)Main.rand.Next(0, 401) * player.direction), -600f);
				position.Y -= (100 * i);
				Vector2 heading = target - position;
				if (heading.Y < 0f)
				{
					heading.Y *= -1f;
				}
				if (heading.Y < 20f)
				{
					heading.Y = 20f;
				}
				heading.Normalize();
				heading *= velocity.Length();
				heading.Y += Main.rand.Next(-40, 41) * 0.02f;
				Projectile.NewProjectile(source, position, heading, type, damage * 2, knockback, player.whoAmI, 0f, ceilingLimit);
			}
			return false;
		}

		public override void AddRecipes()
		{
			CreateRecipe()
			.AddIngredient(ItemType<StarTome>(), 1)
			.AddIngredient(ItemID.PixieDust, 10)
			.AddIngredient(ItemID.UnicornHorn, 5)
			.AddIngredient(ItemID.SoulofLight, 5)
			.AddTile(TileID.Bookcases)
			.Register();
		}
	}
}