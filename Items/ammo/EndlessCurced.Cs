using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.GameContent.Creative;

namespace RemnantOfTheAncientsMod.Items.ammo
{
    public class EndlessCurced : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Endless Cursed Quiver");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Carquois maudit sans fin");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Carcaj maldito eterno");
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
			Item.damage = 17;
			Item.DamageType = DamageClass.Ranged;
			Item.width = 8;
			Item.height = 8;
			Item.maxStack = 1;
			Item.consumable = false;
			Item.knockBack = 3.5f;
			Item.value = 10;
			Item.rare = ItemRarityID.Green;
			Item.shoot = ProjectileID.CursedArrow;
			Item.shootSpeed = 26f;
			Item.ammo = AmmoID.Arrow;
		}
        public override void AddRecipes()
		{
			CreateRecipe()
		     .AddIngredient(ItemID.CursedArrow,3996)
			.AddTile(TileID.CrystalBall)
			.Register();
		}
	}
}