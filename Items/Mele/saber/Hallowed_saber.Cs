using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using Terraria.Localization;
using static Terraria.ModLoader.ModContent;


namespace opswordsII.Items.Mele.saber
{
	public class Hallowed_saber : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Sabebur");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Sabrebur");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Sablebur");
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}
		public override void SetDefaults()
		{
			Item.damage = 53;
			Item.DamageType = DamageClass.Melee;
			Item.width = 40;
			Item.height = 80;
			Item.useTime = 20;
			Item.useAnimation = 20;
			Item.useStyle = 1;
			Item.knockBack = 10;
			Item.value = Item.sellPrice(gold: 5);
			Item.rare = 5;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
		}
	public override void AddRecipes()
		{
			CreateRecipe()
			.AddIngredient(ItemID.HallowedBar,12)
			.AddTile(TileID.MythrilAnvil)
			.Register();
		}
	}
}

