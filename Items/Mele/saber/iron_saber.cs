using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using static Terraria.ModLoader.ModContent;

namespace opswordsII.Items.Mele.saber
{
	public class iron_saber : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Iron Saber");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Sabre de Fer");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Sable de Hierro");
		}
		public override void SetDefaults()
		{
			Item.damage = 6;
			Item.DamageType = DamageClass.Melee;
			Item.width = 40;
			Item.height = 80;
			Item.useTime = 20;
			Item.useAnimation = 20;
			Item.useStyle = 1;
			Item.knockBack = 10;
			Item.value = Item.sellPrice(silver: 4);
			Item.rare = 0;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
		}

		public override void AddRecipes()
		{
			CreateRecipe()
			.AddIngredient(ItemID.IronBar,10)
			.AddTile(TileID.Anvils)
			
			.Register();
		}
	}
}