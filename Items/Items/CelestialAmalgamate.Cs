using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;

namespace opswordsII.Items.Items
{
	public class CelestialAmalgamate : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Celestial Amalgam");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Polish), "Niebiański amalgamat");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Amalgame céleste");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Amalgama Celestial");
			
			Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(10, 9));//5,4
			ItemID.Sets.AnimatesAsSoul[Item.type] = true;
		}

			public override void SetDefaults()
		{
			Item.width = 26; //Tamaño inGame 
			Item.height = 26;
			Item.maxStack = 99; //Cantidad maxima en un espacio de inventario
			Item.value = 6000; //Precio
			Item.rare = 9;
		}

		public override void AddRecipes()
		{
			CreateRecipe()
			.AddIngredient(ItemID.FragmentSolar,1)
            .AddIngredient(ItemID.FragmentStardust,1)
			.AddIngredient(ItemID.FragmentNebula,1)
			.AddIngredient(ItemID.FragmentVortex,1)
			.AddIngredient(null,"Neutrum_Fragment", 1)
			.AddTile(TileID.LunarCraftingStation)        
			.Register();
		    
        }
	}
}