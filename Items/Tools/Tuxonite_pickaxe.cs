using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;

namespace opswordsII.Items.Tools
{
	public class Tuxonite_pickaxe : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Tuxonite Pickaxe");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Polish), "Kilof Tuxonite");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Pioche Tuxonite");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Pico de tusonita");
			Tooltip.SetDefault("");

			/*
			 DisplayName.AddTranslation(GameCulture.Russian, "Туксонитовая кирка");
			 DisplayName.AddTranslation(GameCulture.Chinese, "尾矿镐");*/
		}

		public override void SetDefaults()
		{
			Item.damage = 8;
			Item.DamageType = DamageClass.Melee;
			Item.width = 40;
			Item.height = 40;
			Item.useTime = 13; //mientras mas alto sea el useTime mas lenta será el arma. Usa un bajo UseTime para que el arma sea Rapida
			Item.useAnimation = 19;  //Animacion normal de Pico
			Item.pick = 59; //Potencia de Pico. 
			Item.useStyle = 1; //Dejar en 1 para que el personaje use el arma de forma normal
			Item.knockBack = 2; //Retroceso al golpear
			Item.value = 1600; //Precio
			Item.rare = 0;
			Item.UseSound = SoundID.Item1; 
			Item.autoReuse = true; //Autoutilizar.  No autoutilizar -> Cambiar por "false"
		}

		public override void AddRecipes()
		{
			CreateRecipe()
			.AddIngredient(null,"TuxoniteBar",12)
			.AddIngredient(ItemID.Wood,4)
			.AddTile(TileID.Anvils)
			.Register();
		}
	}
}