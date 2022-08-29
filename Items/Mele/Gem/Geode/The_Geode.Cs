using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.GameContent.Creative;
using RemnantOfTheAncientsMod.Items.Items;

namespace RemnantOfTheAncientsMod.Items.Mele.Gem.Geode
{
	public class The_Geode : ModItem
	{
		public override void SetStaticDefaults()
		{
	        DisplayName.SetDefault("The Geode");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Polish), "The Geode");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "La géode");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "La geoda");

			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}
		public override void SetDefaults()
		{
			Item.damage = 25;
			Item.DamageType = DamageClass.Melee;
			Item.width = 40;
			Item.height = 40;
			Item.useTime = 18;
			Item.useAnimation = 18;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 5;
			Item.value = 1200;
			Item.rare = ItemRarityID.Orange;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = false;

		}

		public override void AddRecipes()
		{
			CreateRecipe()
			.AddIngredient(ModContent.ItemType<Amethyst_Sword>())
            .AddIngredient(ModContent.ItemType<Topaz_Sword>())
			.AddIngredient(ModContent.ItemType<Sapphire_Sword>())
			.AddIngredient(ModContent.ItemType<Ruby_Sword>())
			.AddIngredient(ModContent.ItemType<Emerald_Sword>())
			.AddIngredient(ModContent.ItemType<Diamond_Sword>())
			.AddIngredient(ModContent.ItemType<BoneBar>(),2)
			.AddIngredient(ItemID.StoneBlock,10)
			.AddTile(TileID.Anvils)
			.Register();
		}
	}
}
