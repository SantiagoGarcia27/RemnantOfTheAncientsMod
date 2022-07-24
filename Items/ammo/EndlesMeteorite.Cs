using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.GameContent.Creative;

namespace opswordsII.Items.ammo
{
    public class EndlesMeteorite : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Endless Meteorite Pouch");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Pochette de météorite sans fin");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Bolsa de Balas de meteorito eterna");
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
			Item.rare = ItemRarityID.Lime;
			Item.shoot = ProjectileID.MeteorShot;
			Item.shootSpeed = 16f;                
			Item.ammo = AmmoID.Bullet;              	
		}

        public override void AddRecipes()
		{
			CreateRecipe()
		    .AddIngredient(ItemID.MeteorShot,3996)
			.AddTile(TileID.CrystalBall)
			.Register();
		}
	}
}