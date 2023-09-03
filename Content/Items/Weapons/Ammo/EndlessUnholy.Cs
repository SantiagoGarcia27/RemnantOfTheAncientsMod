using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.GameContent.Creative;

namespace RemnantOfTheAncientsMod.Content.Items.Weapons.Ammo
{
	[CloneByReference]
	public class EndlessUnholy : ModItem
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("ModelEndless Unholy Quiver");
			//DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Carquois impie sans fin");
			//DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Carcaj infame eterno");
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
			Item.CloneDefaults(ItemID.UnholyArrow);
			Item.maxStack = 1;
			Item.consumable = false;             
			Item.value += Item.sellPrice(0,1,0,0);
			Item.rare += 1;			        
		}

		public override void AddRecipes()
		{
			CreateRecipe()
			.AddIngredient(ItemID.UnholyArrow, 3996)
			.AddTile(TileID.CrystalBall)
			.Register();
		}
	}
}