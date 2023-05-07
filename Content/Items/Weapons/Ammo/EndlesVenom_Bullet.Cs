using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.GameContent.Creative;

namespace RemnantOfTheAncientsMod.Content.Items.Weapons.Ammo
{
	[CloneByReference]
	public class EndlesVenom_Bullet : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Endless Venom Pouch");
			DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Sac éternel de balles empoisonnées");
			DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Bolsa de Balas Envenenadas eterna");
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 99;
		}

		public override void SetDefaults()
		{
			Item.CloneDefaults(ItemID.VenomBullet);
			Item.maxStack = 1;
			Item.consumable = false;            
			Item.value += Item.sellPrice(0,1,0,0);
			Item.rare += 1;
		}

		public override void AddRecipes()
		{
			CreateRecipe()
			.AddIngredient(ItemID.VenomBullet, 3996)
			.AddTile(TileID.CrystalBall)
			.Register();
		}
	}
}