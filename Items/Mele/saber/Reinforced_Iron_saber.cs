using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using static Terraria.ModLoader.ModContent;
using opswordsII.Items.Items;

namespace opswordsII.Items.Mele.saber
{
	public class Reinforced_Iron_saber : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Reinforced Iron Saber");
			DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Sabre de fer renforcé");
			DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Sable de Hierro Reforzado");
		}
		public override void SetDefaults()
		{
			Item.damage = 19;
			Item.DamageType = DamageClass.Melee;
			Item.width = 40;
			Item.height = 80;
			Item.useTime = 30;
			Item.useAnimation = 30;
			Item.useStyle = 1;
			Item.knockBack = 10;
			Item.value = Item.sellPrice(gold: 4);
			Item.rare = 0;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
		}
	public override void AddRecipes()
		{
			CreateRecipe()
			.AddIngredient(ModContent.ItemType<Reinforced_ironBar>(), 10)
			.AddTile(TileID.Anvils)
			.Register();
		}
	}
}

