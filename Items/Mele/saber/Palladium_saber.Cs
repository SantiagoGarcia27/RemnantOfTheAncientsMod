using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using static Terraria.ModLoader.ModContent;

namespace opswordsII.Items.Mele.saber
{
	public class Palladium_saber : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Palladium Saber");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Sabre de Palladium");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Sable de Paladio");
		}
		public override void SetDefaults()
		{
			Item.damage = 37;
			Item.DamageType = DamageClass.Melee;
			Item.width = 40;
			Item.height = 80;
			Item.useTime = 20;
			Item.useAnimation = 20;
			Item.useStyle = 1;
			Item.knockBack = 10;
			Item.value = Item.sellPrice(gold: 2);
			Item.rare = 4;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
		}
	public override void AddRecipes()
		{
			CreateRecipe()
			.AddIngredient(ItemID.PalladiumBar,12)
			.AddTile(TileID.Anvils)
			.Register();
		}
	}
}

