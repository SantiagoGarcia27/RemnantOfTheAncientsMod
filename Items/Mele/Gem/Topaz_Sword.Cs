using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.GameContent.Creative;

namespace opswordsII.Items.Mele.Gem
{
	public class Topaz_Sword : ModItem
	{
		public override void SetStaticDefaults()
		{
	        DisplayName.SetDefault("Topaz Sword");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Épée de topaze");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Espada de Topacio");
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}
		public override void SetDefaults()
		{
			Item.damage = 12;
			Item.DamageType = DamageClass.Melee;
			Item.width = 40;
			Item.height = 40;
			Item.useTime = 18;
			Item.useAnimation = 18;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 1;
			Item.value = 1200;
			Item.rare = ItemRarityID.Blue;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = false;
		}

		public override void AddRecipes()
		{
			CreateRecipe()
			.AddIngredient(ItemID.TinBar,8)
            .AddIngredient(ItemID.Topaz,10)
			.AddTile(TileID.Anvils)
			.Register();
		}
	}
}
