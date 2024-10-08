using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using static Terraria.ModLoader.ModContent;


namespace opswordsII.Items.Mele.saber
{
	public class Adamantite_saber : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Adamantite Saber");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Sabre de Adamantite");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Sable de Adamantita");
		}
		public override void SetDefaults()
		{
			Item.damage = 46;
			Item.DamageType = DamageClass.Melee;
			Item.width = 40;
			Item.height = 80;
			Item.useTime = 20;
			Item.useAnimation = 20;
			Item.useStyle = 1;
			Item.knockBack = 10;
			Item.value = Item.sellPrice(gold: 3);
			Item.rare = 4;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
		}
	public override void AddRecipes()
		{
			CreateRecipe()
			.AddIngredient(ItemID.AdamantiteBar,10)
			.AddTile(TileID.MythrilAnvil)
			.Register();
		}
	}
}

