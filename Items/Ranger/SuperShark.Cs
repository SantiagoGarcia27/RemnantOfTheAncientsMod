using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Localization;

namespace opswordsII.Items.Ranger
{
	public class SuperShark : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Supershark");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Polish), "superrekin");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "superrequin");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Supertiburón");
			
			Tooltip.SetDefault(""
			+ "\n40% chance of not spending ammo ");
            Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Polish), ""
			+ "\n40% szans na nie wydawanie amunicji ");
            Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), ""
			+ "\n40% de chances de ne pas dépenser de munitions ");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), ""
			+ "\nProbabilidad del 40% de no gastar munición ");
           
		   
		   
		   
		   /* Tooltip.AddTranslation(GameCulture.Chinese, ""
			+ "\n40％的机会不花弹药");
			Tooltip.AddTranslation(GameCulture.Russian, ""
			+ "\n40% шанс не тратить боеприпасы");*/
		}

		public override void SetDefaults()
		{
            Item.damage = 70;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 80; //Tamaño de Ancho
            Item.height = 40; //Tamaño de Alto
            Item.useTime = 9; //mientras mas alto sea el useTime mas lenta será el arma. Usa un bajo UseTime para que el arma sea Rapida
            Item.useAnimation = 10;
            Item.useStyle = 5; //Dejar en 5 para que el personaje use el arma de forma normal
            Item.noMelee = true;
            Item.knockBack = 1;
            Item.value = 100000; //Precio
            Item.rare = 10;
   		    Item.UseSound = SoundID.Item31;
            Item.autoReuse = true;
            Item.shoot = 5; //dejar en 10 para que dispare balas normales
            Item.shootSpeed = 100f;
            Item.useAmmo = AmmoID.Bullet;
			
		}
         /*  public override bool CanConsumeAmmo(Player player)
		{
			return Main.rand.NextFloat() >= .40f;
		}
			public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
		{
			Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(5));
			speedX = perturbedSpeed.X;
			speedY = perturbedSpeed.Y;
			return true;
		}*/
		  public override Vector2? HoldoutOffset()
		{
			return new Vector2(-20, 0);
		}

		public override void AddRecipes()
		{
			CreateRecipe()
			.AddIngredient(ItemID.Megashark, 1)
			.AddIngredient(null,"LuminiteFin", 20)
			.AddIngredient(null,"CelestialAmalgamate",5)
			.AddTile(TileID.LunarCraftingStation)
			.Register();
		}
	}
}
