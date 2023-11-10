using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;

namespace RemnantOfTheAncientsMod.Content.Items.Tools
{
	public class Marble_axe : ModItem
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Marble Axe");
			// //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Hache marbre");
			// //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Hacha de marmol");
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
			Item.damage = 9;
			Item.DamageType = DamageClass.Melee;
			Item.width = 40;
			Item.height = 40;
			Item.useTime = 15;
			Item.useAnimation = 23;
			Item.axe = 10;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 5;
			Item.value = 1300;
			Item.rare = ItemRarityID.White;
			Item.UseSound = SoundID.Item1;
			Item.scale = 1.28f;
			Item.autoReuse = true;
			Item.useTurn = true;
		}

		public override void AddRecipes()
		{
			CreateRecipe()
			.AddIngredient(ItemID.Marble, 9)
             .AddRecipeGroup("Wood", 3)
            .AddTile(TileID.Anvils)
			.Register();
		}
	}
}