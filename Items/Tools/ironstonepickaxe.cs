using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;

namespace opswordsII.Items.Tools
{
	public class ironstonepickaxe : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Ironstone Pickaxe");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Polish), "Kilof Skamieniałego Żelaza");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Pioche en fer pétrifié");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Pico de hierro petrificado");
			Tooltip.SetDefault("");
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;

			/* DisplayName.AddTranslation(GameCulture.Russian, "Окаменелая железная кирка");
			 DisplayName.AddTranslation(GameCulture.Chinese, "硅化铁镐");*/
		}

		public override void SetDefaults()
		{
			Item.damage = 4;
			Item.DamageType = DamageClass.Melee;
			Item.width = 40;
			Item.height = 40;
			Item.useTime = 8; //mientras mas alto sea el useTime mas lenta será el arma. Usa un bajo UseTime para que el arma sea Rapida
			Item.useAnimation = 8;  //Animacion normal de Pico
			Item.pick = 49; //Potencia de Pico. 
			Item.useStyle = 1; //Dejar en 1 para que el personaje use el arma de forma normal
			Item.knockBack = 6; //Retroceso al golpear
			Item.value = 1000; //Precio
			Item.rare = 0;
			Item.UseSound = SoundID.Item1; 
			Item.autoReuse = true; //Autoutilizar.  No autoutilizar -> Cambiar por "false"
			Item.useTurn = true;
		}

		public override void AddRecipes() //Crafteo del objeto
		{
		}
	}
}