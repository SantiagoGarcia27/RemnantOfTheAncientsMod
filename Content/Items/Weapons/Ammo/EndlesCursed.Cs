using Terraria.Localization;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;

namespace RemnantOfTheAncientsMod.Content.Items.Weapons.Ammo
{
	[CloneByReference]
	public class EndlesCursed : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Endless Cursed Pouch");
			DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Pochette incurvée sans fin");
			DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Bolsa de Balas de malditas eterna");
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}
        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.CursedBullet);
            Item.maxStack = 1;
            Item.consumable = false;
            Item.value += Item.sellPrice(0, 1, 0, 0);
            Item.rare += 1;
        }

        public override void AddRecipes()
		{
			CreateRecipe()
			.AddIngredient(ItemID.CursedBullet, 3996)
			.AddTile(TileID.CrystalBall)
			.Register();
		}
	}
}