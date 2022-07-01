using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.Localization;

namespace opswordsII.Items.Mele.Gem.Geode
{
	public class The_Geode_white : ModItem
	{
		public override void SetStaticDefaults()
		{
			
	        DisplayName.SetDefault("The Geode");

            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Polish), "The Geode");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "La géode");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "La geoda");

			
		}
		public override void SetDefaults()
		{
			Item.damage = 30;
			Item.DamageType = DamageClass.Melee;
			Item.width = 40;
			Item.height = 40;
			Item.useTime = 18;
			Item.useAnimation = 18;
			Item.useStyle = 1;
			Item.knockBack = 5;
			Item.value = 1200;
			Item.rare = ItemRarityID.Orange;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;

		}

		public override void AddRecipes()
		{
			CreateRecipe()
			.AddIngredient(null,"The_Geode",1)
            .AddIngredient(ItemID.LargeDiamond,1)
			.AddTile(TileID.Anvils)
			.Register();
		}
	}
}
