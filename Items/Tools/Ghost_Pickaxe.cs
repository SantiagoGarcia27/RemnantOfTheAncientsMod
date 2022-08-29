using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using RemnantOfTheAncientsMod.Dusts;

namespace RemnantOfTheAncientsMod.Items.Tools
{
	public class Ghost_Pickaxe : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Ghost Pickaxe");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Polish), "Duch Kilof");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Pioche fantôme");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Pico fantasmal");
			Tooltip.SetDefault("");
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;

			/* DisplayName.AddTranslation(GameCulture.Russian, "Призрачная кирка");
			  DisplayName.AddTranslation(GameCulture.Chinese, "鬼镐");*/
		}

		public override void SetDefaults()
		{
			Item.damage = 10;
			Item.DamageType = DamageClass.Melee;
			Item.width = 40;
			Item.height = 40;
			Item.useTime = 15; //mientras mas alto sea el useTime mas lenta será el arma. Usa un bajo UseTime para que el arma sea Rapida
			Item.useAnimation = 20;  //Animacion normal de Pico
			Item.pick = 72; //Potencia de Pico. 
			Item.useStyle = 1; //Dejar en 1 para que el personaje use el arma de forma normal
			Item.knockBack = 6; //Retroceso al golpear
			Item.value = 100000; //Precio
			Item.rare = 0;
			Item.UseSound = SoundID.Item1; 
			Item.autoReuse = true; //Autoutilizar.  No autoutilizar -> Cambiar por "false"
			Item.useTurn = true;
		}

		public override void MeleeEffects(Player player, Rectangle hitbox)
		{
			if (Main.rand.Next(10) == 0)
			{
				int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, ModContent.DustType<Ghost>());
			}
		}
	}
}