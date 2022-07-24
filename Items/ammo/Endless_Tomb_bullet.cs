using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using opswordsII.Projectiles;
using Terraria.GameContent.Creative;

namespace opswordsII.Items.ammo
{
	public class Endless_Tomb_bullet : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Endless Grave Pouch"); 
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Balle tombale sans fin");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Bala de tumba eterna");
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
			Item.damage = 5;
			Item.DamageType = DamageClass.Ranged;
			Item.width = 8;
			Item.height = 8;
			Item.maxStack = 1;
			Item.consumable = false;
			Item.knockBack = 3.5f;
			Item.value = 30;
			Item.rare = ItemRarityID.Green;
			Item.shoot = ModContent.ProjectileType<Tomb_BulletP>();
			Item.shootSpeed = 5f;
			Item.ammo = AmmoID.Bullet;
		}

        public override void AddRecipes()
		{
			CreateRecipe()
			.AddIngredient(ModContent.ItemType<Tomb_bullet>(),3996)
			.AddTile(TileID.CrystalBall)
			.Register();
		}
	}
}