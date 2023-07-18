using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.GameContent.Creative;
using RemnantOfTheAncientsMod.Content.Projectiles.Ranger;

namespace RemnantOfTheAncientsMod.Content.Items.Weapons.Ammo.Bullet
{
	public class WrathSlimeBullet : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Wrath Slime Bullet");
			DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Balle Visqueuse de Colère");
			DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Bala de Slime Furioso");
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 99;
		}

		public override void SetDefaults()
		{
			Item.damage = 6;
			Item.DamageType = DamageClass.Ranged;
			Item.width = 8;
			Item.height = 8;
			Item.maxStack = 9999;
			Item.consumable = true;
			Item.knockBack = 3.5f;
			Item.value = 10;
			Item.rare = 2;
			Item.shoot = ModContent.ProjectileType<bullettestp>();
			Item.shootSpeed = 16f;
			Item.ammo = AmmoID.Bullet;
		}
		public override void AddRecipes()
		{
			CreateRecipe(100)
			.AddIngredient(ItemID.MusketBall, 100)
			.AddIngredient(ItemID.Meteorite, 5)
			.AddIngredient(ItemID.PinkGel, 5)
			.AddTile(TileID.Hellforge)
			.Register();
		}
	}
}