using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using Terraria.Localization;
using Terraria.GameContent.Creative;

namespace RemnantOfTheAncientsMod.Items.Mele.saber
{
    public class gold_saber : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Golden Saber");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Sabre D'or");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Sable de Oro");
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}
		public override void SetDefaults()
		{
			Item.damage = 9;
			Item.DamageType = DamageClass.Melee;
			Item.width = 40;
			Item.height = 80;
			Item.useTime = 20;
			Item.useAnimation = 20;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 10;
			Item.value = Item.sellPrice(silver: 18);
			Item.rare = ItemRarityID.White;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
		}

		public override void AddRecipes()
		{
			CreateRecipe()
			.AddIngredient(ItemID.GoldBar,10)
			.AddTile(TileID.Anvils)
			.Register();
		}
	}
}
