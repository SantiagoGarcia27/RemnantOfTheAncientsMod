using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;

namespace opswordsII.Items.Tools
{
	public class Tuxonite_axe : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Tuxonite Axe");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Polish), "Topór Tuxonite");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Hache Tuxonite");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Hacha de tusonita");
			Tooltip.SetDefault("");
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;

			/* DisplayName.AddTranslation(GameCulture.Russian, "Туксонитовый топор");
			DisplayName.AddTranslation(GameCulture.Chinese, "毒x斧");*/
		}

		public override void SetDefaults()
		{
			Item.damage = 9;
			Item.DamageType = DamageClass.Melee;
			Item.width = 40;
			Item.height = 40;
			Item.useTime = 15; //mientras mas alto sea el useTime mas lenta será el arma. Usa un bajo UseTime para que el arma sea Rapida
			Item.useAnimation = 23;  //Animacion normal de Pico
			Item.axe = 14; //69
			Item.useStyle = 1; //Dejar en 1 para que el personaje use el arma de forma normal
			Item.knockBack = 5; //Retroceso al golpear
			Item.value = 1300; //Precio
			Item.rare = 0;
			Item.UseSound = SoundID.Item1; 
			Item.scale = 1.28f;
			Item.autoReuse = true; //Autoutilizar.  No autoutilizar -> Cambiar por "false"
		}

		public override void AddRecipes()
		{
			CreateRecipe()
			.AddIngredient(null,"TuxoniteBar",9)
			.AddIngredient(ItemID.Wood,3)
			.AddTile(TileID.Anvils)
			.Register();
		}
	}
}