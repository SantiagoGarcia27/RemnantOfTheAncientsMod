using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Localization;

namespace opswordsII.Items.Ranger
{
	public class minigun: ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("mini/gun");
			Tooltip.SetDefault("A small pistol");
			DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "una pistola pequeña");
		}
		public override void SetDefaults()
		{
        Item.damage = 30;
        Item.DamageType = DamageClass.Ranged;
        Item.width =2; //Tamaño de Ancho
        Item.height = 2; //Tamaño de Alto
        Item.useTime = 25; //mientras mas alto sea el useTime mas lenta será el arma. Usa un bajo UseTime para que el arma sea Rapida
        Item.useAnimation = 25;//30
        Item.useStyle = 5; //Dejar en 5 para que el personaje use el arma de forma normal
        Item.noMelee = true;
		Item.scale = 0.68f;
        Item.knockBack = 5;
        Item.value = 20000; //Precio
        Item.rare = 1;
		Item.UseSound = SoundID.Item38;
        Item.autoReuse = false;
        Item.shoot = 10;
        Item.shootSpeed = 20f;
        Item.useAmmo = AmmoID.Bullet;

		/*Mod OmniSwing = ModLoader.TryGetMod("OmniSwing");
    		if (OmniSwing != null)
			Item.damage = 28;*/
		}
            public override Vector2? HoldoutOffset()
		{
			return new Vector2(-10, 0);
		}
		public override void AddRecipes()
		{
			CreateRecipe()
			.AddIngredient(ItemID.FlintlockPistol, 2)
			.AddTile(TileID.WorkBenches)
			.Register();
		}
	}
}
