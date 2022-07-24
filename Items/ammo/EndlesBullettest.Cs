using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using opswordsII.Projectiles;
using Terraria.GameContent.Creative;

namespace opswordsII.Items.ammo
{
    public class EndlesBullettest : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Endless Angry Slime Pouch");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Pochette de Slime en Colère Sans Fin");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Saco Infinito de Bala de Slime Furioso");
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
			Item.damage = 6;
			Item.DamageType = DamageClass.Ranged;
			Item.width = 8;
			Item.height = 8;
			Item.maxStack = 1;
			Item.consumable = false;             
			Item.knockBack = 3.5f;
			Item.value = 10;
			Item.rare = ItemRarityID.Green;
			Item.shoot = ModContent.ProjectileType<bullettestp>();   
			Item.shootSpeed = 16f;                  
			Item.ammo = AmmoID.Bullet;   
		}
        public override void AddRecipes()
		{
			CreateRecipe()
		    .AddIngredient(ModContent.ItemType<bullettest>(),3996)
			.AddTile(TileID.CrystalBall)
			.Register();
		}
	}
}