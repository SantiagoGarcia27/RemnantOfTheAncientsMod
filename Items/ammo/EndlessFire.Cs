using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using RemnantOfTheAncientsMod.Projectiles;
using Terraria.GameContent.Creative;

namespace RemnantOfTheAncientsMod.Items.ammo
{
	public class EndlessFire : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Endless Flaming Pouch"); 
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Balle enflammée sans fin");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Saco Infinito de Bala llameante ");
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
			Item.damage = 9;
			Item.DamageType = DamageClass.Ranged;
			Item.width = 8;
			Item.height = 8;
			Item.maxStack = 1;
			Item.consumable = false;
			Item.knockBack = 3.5f;
			Item.value = 10;
			Item.rare = ItemRarityID.Green;
			Item.shoot = ModContent.ProjectileType<Fibullet>();
			Item.shootSpeed = 7f;
			Item.ammo = AmmoID.Bullet;
		}
        public override void AddRecipes()
		{
			CreateRecipe()
		    .AddIngredient(ModContent.ItemType<FireBullet>(),3996)
			.AddTile(TileID.CrystalBall)
			.Register();
		}
	}
}