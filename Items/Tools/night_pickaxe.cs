using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using RemnantOfTheAncientsMod.Items.Items;

namespace RemnantOfTheAncientsMod.Items.Tools
{
    public class night_pickaxe : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Night Pickaxe"); 
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Pioche de nuit");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Pico de la noche");
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
			Item.damage = 13;
			Item.DamageType = DamageClass.Melee;
			Item.width = 80;
			Item.height = 80;
			Item.useTime = 10; 
			Item.useAnimation = 20;
			Item.pick = 130;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 6;
			Item.value = 100000;
			Item.rare = ItemRarityID.White;
			Item.UseSound = SoundID.Item1; 
			Item.autoReuse = true;
			Item.useTurn = true;
		}

		public override void AddRecipes()
		{
			CreateRecipe()
			.AddIngredient(ModContent.ItemType<NightBar>(),16)
			.AddTile(TileID.DemonAltar)
			.Register();
		
		}
	}
}
