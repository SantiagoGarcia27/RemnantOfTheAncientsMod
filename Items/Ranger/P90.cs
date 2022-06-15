using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Localization;

namespace opswordsII.Items.Ranger
{
	public class P90 : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("P90");
			Tooltip.SetDefault("Fast and effective");
            Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Polish), "Szybki i skuteczny");
            Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Rapide et efficace");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Rapida y efectiva");
		}
		public override void SetDefaults()
		{
            Item.damage = 11;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 80; //Tamaño de Ancho
            Item.height = 40; //Tamaño de Alto
            Item.useTime = 4; //mientras mas alto sea el useTime mas lenta será el arma. Usa un bajo UseTime para que el arma sea Rapida
            Item.useAnimation = 4;
            Item.useStyle = 5; //Dejar en 5 para que el personaje use el arma de forma normal
            Item.noMelee = true;
            Item.knockBack = 1;
            Item.value = 100000; //Precio
            Item.rare = 6;
   		    Item.UseSound = SoundID.Item10;
            Item.autoReuse = true;
            Item.shoot = 10; //dejar en 10 para que dispare balas normales
            Item.shootSpeed = 100f;
            Item.useAmmo = AmmoID.Bullet;
			Item.expert = true;
		}

		 public override Vector2? HoldoutOffset()
		{
			return new Vector2(-20, 0);
		}

		public override void AddRecipes()
		{
			CreateRecipe()
			.AddIngredient(ItemID.MechanicalWheelPiece, 1)
			.AddRecipeGroup("IronBar", 20)
			.AddIngredient(ItemID.SoulofMight, 5)
			.AddIngredient(ItemID.HallowedBar, 10)
			.AddTile(TileID.MythrilAnvil)
			.Register();
		}
	}
}
