using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;

namespace opswordsII.Items.Tools
{
	public class night_pickaxe : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Night Pickaxe"); 
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Polish), "Nocny kilof");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Pioche de nuit");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Pico de la noche");
			Tooltip.SetDefault("");

			/*DisplayName.AddTranslation(GameCulture.Russian, "Ночная кирка");
			 DisplayName.AddTranslation(GameCulture.Chinese, "夜镐");*/
		}

		public override void SetDefaults()
		{
			Item.damage = 13;
			Item.DamageType = DamageClass.Melee;
			Item.width = 80;
			Item.height = 80;
			Item.useTime = 10; //mientras mas alto sea el useTime mas lenta será el arma. Usa un bajo UseTime para que el arma sea Rapida
			Item.useAnimation = 20;  //Animacion normal de Pico
			Item.pick = 130; //Potencia de Pico. 
			Item.useStyle = 1; //Dejar en 1 para que el personaje use el arma de forma normal
			Item.knockBack = 6; //Retroceso al golpear
			Item.value = 100000; //Precio
			Item.rare = 0;
			Item.UseSound = SoundID.Item1; 
			Item.autoReuse = true; //Autoutilizar.  No autoutilizar -> Cambiar por "false"
		}

		public override void AddRecipes() //Crafteo del objeto
		{
			CreateRecipe()
			.AddIngredient(null,"NightBar",16)
			.AddTile(TileID.DemonAltar)
			.Register();
		
		}
	}
}
