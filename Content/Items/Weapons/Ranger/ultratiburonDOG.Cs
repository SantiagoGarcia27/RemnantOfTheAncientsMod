using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria.Localization;
using Terraria.GameContent.Creative;
using Terraria.DataStructures;
using RemnantOfTheAncientsMod.Content.Items.Items;
using RemnantOfTheAncientsMod.Content.Projectiles.Ranger;

namespace RemnantOfTheAncientsMod.Content.Items.Weapons.Ranger
{
	public class ultratiburonDOG : ModItem
	{
		public override bool IsLoadingEnabled(Mod mod)
		{
			return ModLoader.TryGetMod("CalamityMod", out mod);
		}
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Punisher of Gods");
			DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Punisher of Gods");
			DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Castigadora de Dioses");

			Tooltip.SetDefault(""
			+ "\n90% chance of not spending ammo ");
			Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), ""
			+ "\n90% de chances de ne pas dépenser de munitions ");
			Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), ""
			+ "\nProbabilidad del 90% de no gastar munición ");
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}
		public override void SetDefaults()
		{
			Item.damage = 110;//155
			Item.DamageType = DamageClass.Ranged;
			Item.width = 80;
			Item.height = 40;
			Item.useTime = 2;
			Item.useAnimation = 10;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.noMelee = true;
			Item.knockBack = 1;
			Item.value = Item.sellPrice(1, 50, 2, 0);
			Item.rare = ItemRarityID.Red;
			Item.UseSound = SoundID.Item12;
			Item.autoReuse = true;
			Item.shoot = ProjectileID.PurificationPowder;
			Item.shootSpeed = 100f;
			Item.useAmmo = AmmoID.Bullet;

		}
		public override bool CanConsumeAmmo(Item ammo, Player player)
		{
			return Main.rand.NextFloat() >= .90f;
		}
		public override Vector2? HoldoutOffset()
		{
			return new Vector2(-30, 0);
		}
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (Main.rand.NextFloat() <= (float)5 / 100) Projectile.NewProjectile(source, position, velocity / 4, ProjectileType<MisilDevourer>(), damage * 2, 1, player.whoAmI,0,8);
            return true;
        }
        [JITWhenModsEnabled("CalamityMod")]
        public override void AddRecipes()
		{
			if (RemnantOfTheAncientsMod.CalamityMod != null)
			{
				Recipe recipe = CreateRecipe()

				.AddIngredient(ItemType<ultratiburon>());
				recipe.AddIngredient(RemnantOfTheAncientsMod.CalamityMod.Find<ModItem>("CosmiliteBar"), 15);
				recipe.AddIngredient(RemnantOfTheAncientsMod.CalamityMod.Find<ModItem>("Phantoplasm"), 5);
				recipe.AddIngredient(RemnantOfTheAncientsMod.CalamityMod.Find<ModItem>("EndothermicEnergy"), 10);
				recipe.AddIngredient(RemnantOfTheAncientsMod.CalamityMod.Find<ModItem>("NightmareFuel"), 10);
				recipe.AddIngredient(RemnantOfTheAncientsMod.CalamityMod.Find<ModItem>("GalacticaSingularity"), 15);
				recipe.AddIngredient(ItemType<CelestialAmalgamate>(), 3);
                recipe.AddTile(Utils1.GetTileFromMod(RemnantOfTheAncientsMod.CalamityMod, "CosmicAnvil"));
                recipe.Register();
			}
		}

	}
}
