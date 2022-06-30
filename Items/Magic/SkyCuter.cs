using System;
using System.IO;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;
using Terraria.Localization;
using static Terraria.ModLoader.ModContent;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using opswordsII;
using opswordsII.Projectiles;


namespace opswordsII.Items.Magic
{
	public class SkyCuter : ModItem
	{
		public override void SetStaticDefaults() {
		Item.staff[Item.type] = true;

		DisplayName.SetDefault("SkyCutter");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Polish), "Przecinak nieba");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Coupe du ciel");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Cortador estelar");
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults() {
			Item.damage = 40;
			Item.noMelee = true;
			Item.DamageType = DamageClass.Magic;
			Item.channel = true; //Channel so that you can hold the weapon [Important]
			Item.mana = 5;
			Item.rare = ItemRarityID.Lime;
			Item.width = 28;
			Item.height = 30;
			Item.useTime = 20;
			Item.UseSound = SoundID.Item13;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.shootSpeed = 14f;
			Item.useAnimation = 20;
			Item.autoReuse = true;
			Item.shoot = ModContent.ProjectileType<SkyCutterS>(); //Laser
			Item.value = Item.sellPrice(gold: 3);
		}
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			Vector2 target = Main.screenPosition + new Vector2(Main.mouseX, Main.mouseY);
			float ceilingLimit = target.Y;
			if (ceilingLimit > player.Center.Y - 200f)
			{
				ceilingLimit = player.Center.Y - 200f;
			}
			// Loop these functions 3 times.
			for (int i = 0; i < 3; i++)
			{
				position = player.Center - new Vector2(Main.rand.NextFloat(401) * player.direction, 600f);
				position.Y -= 100 * i;
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

		public override void AddRecipes() {
			CreateRecipe()
			.AddIngredient(ItemID.SkyFracture, 1)
			.AddIngredient(ItemID.SpectreStaff, 1)
			.AddTile(TileID.MythrilAnvil)
			.Register();
		}
	}
}
