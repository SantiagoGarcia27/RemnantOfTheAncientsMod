using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using opswordsII.Projectiles;
using Terraria.GameContent.Creative;

namespace opswordsII.Items.ammo
{
    public class EndlessDesert : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Endless Desert Quiver");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Carquois du désert sans fin");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Carcaj Desertico eterno");
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
			Item.shoot = ModContent.ProjectileType<DesertArrowP>();
			Item.shootSpeed = 26f;
			Item.ammo = AmmoID.Arrow;
		}


        public override void AddRecipes()
		{
			CreateRecipe()
		    .AddIngredient(ModContent.ItemType<DesertArrow>(),3996)
			.AddTile(TileID.CrystalBall)
			.Register();
		}
	}
}