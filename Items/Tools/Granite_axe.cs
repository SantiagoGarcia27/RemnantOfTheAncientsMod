using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using RemnantOfTheAncientsMod.Items.Items;

namespace RemnantOfTheAncientsMod.Items.Tools
{
	public class Granite_axe : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Granite Axe");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Hache granit");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Hacha de granito");
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
			.AddIngredient(ItemID.Granite,9)
			.AddIngredient(ItemID.Wood,3)
			.AddTile(TileID.Anvils)
			.Register();
		}
	}
}