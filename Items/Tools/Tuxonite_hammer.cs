using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;

namespace opswordsII.Items.Tools
{
	public class Tuxonite_hammer : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Tuxonite Hammer");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Polish), "Młot Tuxonite");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Marteau Tuxonite");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Martillo de tusonita");
			Tooltip.SetDefault("");

			/*
			DisplayName.AddTranslation(GameCulture.Russian, "Туксонитовый молот");
			 DisplayName.AddTranslation(GameCulture.Chinese, "毒x锤");*/
		}

		public override void SetDefaults()
		{
			Item.damage = 11;
			Item.DamageType = DamageClass.Melee;
			Item.width = 40;
			Item.height = 40;
			Item.useTime = 18; //mientras mas alto sea el useTime mas lenta será el arma. Usa un bajo UseTime para que el arma sea Rapida
			Item.useAnimation = 26;  //Animacion normal de Pico
			Item.hammer = 60; //Potencia de Pico. 
			Item.useStyle = 1; //Dejar en 1 para que el personaje use el arma de forma normal
			Item.knockBack = 6; //Retroceso al golpear
			Item.value = 1300; //Precio
			Item.rare = 0;
			Item.UseSound = SoundID.Item1; 
			Item.autoReuse = true; //Autoutilizar.  No autoutilizar -> Cambiar por "false"
		}

		public override void AddRecipes()
		{
			CreateRecipe()
			.AddIngredient(null,"TuxoniteBar",10)
			.AddIngredient(ItemID.Wood,3)
			.AddTile(TileID.Anvils)
			.Register();
		}
	}
}