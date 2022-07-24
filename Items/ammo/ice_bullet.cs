using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using opswordsII.Projectiles;
using Terraria.GameContent.Creative;

namespace opswordsII.Items.ammo
{
	public class ice_bullet : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Frozen Bullet"); 
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Balle gelée");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Bala Helada");
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 99;
		}

		public override void SetDefaults()
		{
			Item.damage = 10;
			Item.DamageType = DamageClass.Ranged;
			Item.width = 8;
			Item.height = 8;
			Item.maxStack = 9999;
			Item.consumable = true;             
			Item.knockBack = 3.5f;
			Item.value = 30;
			Item.rare = ItemRarityID.Green;
			Item.shoot = ModContent.ProjectileType<ice_bulletP>();   
			Item.shootSpeed = 10f;                 
			Item.ammo = AmmoID.Bullet;            
		}

        public override void AddRecipes()
		{
			CreateRecipe(100)
			.AddIngredient(ItemID.MusketBall, 100)
			.AddIngredient(ItemID.LivingFrostFireBlock, 1)
			.AddTile(TileID.IceMachine)
			.Register();
		}
	}
}