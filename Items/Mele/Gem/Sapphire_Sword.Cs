using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.GameContent.Creative;

namespace opswordsII.Items.Mele.Gem
{
	public class Sapphire_Sword : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Sapphire Sword");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Épée de saphir");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Espada de zafiro");
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;

		}
		public override void SetDefaults()
		{
			Item.damage = 16;
			Item.DamageType = DamageClass.Melee;
			Item.width = 40;
			Item.height = 40;
			Item.useTime = 18;
			Item.useAnimation = 18;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 1;
			Item.value = 1000;
			Item.rare = ItemRarityID.Blue;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = false;

		}

		public override void AddRecipes()
		{
			CreateRecipe()
			.AddIngredient(ItemID.SilverBar,8)
            .AddIngredient(ItemID.Sapphire,10)
			.AddTile(TileID.Anvils)
			.Register();
		}
	}
}