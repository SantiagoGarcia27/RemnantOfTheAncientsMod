using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using static Terraria.ModLoader.ModContent;
using Terraria.GameContent.Creative;
using RemnantOfTheAncientsMod.Content.Projectiles;
using RemnantOfTheAncientsMod.Common.Global;
using Terraria.DataStructures;
using RemnantOfTheAncientsMod.Common.UtilsTweaks;
using RemnantOfTheAncientsMod.Content.Items.Items;

namespace RemnantOfTheAncientsMod.Content.Items.Weapons.Melee.saber
{
	public class Night_saber : ModItem
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Night Saber");
			//DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Sabre de la Nuit");
			//DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Sable de la Noche");
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}
		public override void SetDefaults()
		{
			Item.CloneDefaults(ItemID.NightsEdge);
			Item.damage -= 3;
			Item.DamageType = DamageClass.Melee;
			Item.width = 40;
			Item.height = 80;
			Item.useTime -= 3;
			Item.useAnimation -= 3;
			Item.knockBack += 5;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
			Item.scale = 1;
			Item.GetGlobalItem<CustomTooltip>().Saber = true;
			Item.shoot = ProjectileID.NightsEdge;
		}
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			float adjustedItemScale = player.GetAdjustedItemScale(Item); // Get the melee scale of the player and item.
			Projectile.NewProjectile(source, player.MountedCenter, new Vector2(player.direction, 0f), ProjectileID.NightsEdge, damage, knockback, player.whoAmI, player.direction * player.gravDir, player.itemAnimationMax, adjustedItemScale + 0.4f);
			NetMessage.SendData(MessageID.PlayerControls, -1, -1, null, player.whoAmI); // Sync the changes in multiplayer.

			return base.Shoot(player, source, position, velocity, type, damage, knockback);
		}
		public override void MeleeEffects(Player player, Rectangle hitbox)
		{
			if (Main.rand.NextBool(3))
			{
				Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, DustID.Demonite);
			}
			if (Main.rand.NextBool(3))
			{
				Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, DustID.Shadowflame);
			}
			if (Main.rand.NextBool(7))
			{
				Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, DustID.WaterCandle, 0f, 0f, 1, Color.MediumPurple);
			}
			if (Main.rand.NextBool(2))
			{
				Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, DustID.Water_Corruption);
			}
			if (Main.rand.NextBool(3))
			{
				Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, DustID.Water_Cavern);
			}
		}
		public override bool AltFunctionUse(Player player) => true;
		public override bool CanUseItem(Player player)
		{
			if (player.altFunctionUse == 2)
			{
				if (DistanceUtils.PlayerTouchFlour(player))
				{
					float adjustedItemScale = player.GetAdjustedItemScale(Item); // Get the melee scale of the player and item.
					Projectile.NewProjectile(Projectile.GetSource_None(), player.MountedCenter, new Vector2(player.direction, 0f), ProjectileID.NightsEdge, Item.damage + 10, Item.knockBack, player.whoAmI, player.direction * player.gravDir, player.itemAnimationMax, adjustedItemScale + 0.7f);
					NetMessage.SendData(MessageID.PlayerControls, -1, -1, null, player.whoAmI); // Sync the changes in multiplayer.

					DashPlayer.JumpDash(player, 1f, 0.75f);
					var p = Projectile.NewProjectile(Projectile.GetSource_None(), player.position, new Vector2(0, 0), ModContent.ProjectileType<DamageHitbox>(), Item.damage, 2f, Main.myPlayer, 3, 1);
					Main.projectile[p].width = Item.width * 2;
					Main.projectile[p].height = Item.height * 2;
					Main.projectile[p].scale = Item.scale;
					Main.projectile[p].timeLeft = 100;
				}
			}
			return true;
		}
		public override void AddRecipes()
		{
			if (RemnantOfTheAncientsMod.CalamityMod != null)
			{
				CreateRecipe()
				.AddRecipeGroup("CorruptSaber")
				.AddIngredient(ItemType<Grass_saber>())
				.AddIngredient(ItemID.Muramasa, 1)
				.AddIngredient(ItemType<Fire_saber>())
				.AddIngredient(ExternalModCallUtils.GetItemFromMod(RemnantOfTheAncientsMod.CalamityMod, "PurifiedGel"), 5)
				.AddTile(TileID.DemonAltar)
				.Register();

				Recipe.Create(ItemID.NightsEdge)
				.AddIngredient(ModContent.ItemType<NightBar>(), 10)
				.AddIngredient(ExternalModCallUtils.GetItemFromMod(RemnantOfTheAncientsMod.CalamityMod, "PurifiedGel"), 5)
				.AddTile(TileID.DemonAltar)
				.Register();
			}
			else
			{
				CreateRecipe()
				.AddRecipeGroup("CorruptSaber")
				.AddIngredient(ItemType<Grass_saber>())
				.AddIngredient(ItemID.Muramasa, 1)
				.AddIngredient(ItemType<Fire_saber>())
				.AddTile(TileID.DemonAltar)
				.Register();

				CreateRecipe()
				 .AddIngredient(ModContent.ItemType<NightBar>(), 10)
				 .AddTile(TileID.DemonAltar)
				 .Register();
			}
		}
	}
}

