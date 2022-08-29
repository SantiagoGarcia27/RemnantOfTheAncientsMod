using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.GameContent.Creative;

namespace RemnantOfTheAncientsMod.Items.ammo
{
    public class EndlessHoly : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Endless Holy Quiver");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Carquois sacré sans fin");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Carcaj Sagrado eterno");
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
			Item.damage = 13;
			Item.DamageType = DamageClass.Ranged;
			Item.width = 8;
			Item.height = 8;
			Item.maxStack = 1;
			Item.consumable = false;            
			Item.knockBack = 3.5f;
			Item.value = 10;
			Item.rare = ItemRarityID.Green;
			Item.shoot = ProjectileID.HolyArrow;   
			Item.shootSpeed = 26f;
			Item.ammo = AmmoID.Arrow;
		}


        public override void AddRecipes()
		{
			CreateRecipe()
		     .AddIngredient(ItemID.HolyArrow,3996)
			.AddTile(TileID.CrystalBall)
			.Register();
		}
	}
}