using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using opswordsII.Projectiles;

namespace opswordsII.Items.Lanzables
{
	public class Golden_Shuriken: ModItem
	{
		public override void SetStaticDefaults()
		{
	  	    DisplayName.SetDefault("Golden Shuriken");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Shuriken doré");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Shuriken dorado");
		}
		public override void SetDefaults()
		{ 
	        Item.damage = 30;           //this is the item damage
            Item.DamageType = DamageClass.Throwing;         //this make the item do throwing damage
            Item.noMelee = true;
            Item.width = 20;
            Item.height = 20;
            Item.useTime = 15;       //this is how fast you use the item
            Item.useAnimation = 15;   //this is how fast the animation when the item is used
            Item.useStyle = 1;      
            Item.knockBack = 6;
            Item.value = 4;
            Item.rare = 1;
            Item.reuseDelay = 10;    //this is the item delay
            Item.UseSound = SoundID.Item18;
            Item.autoReuse = false;
	        Item.shoot = ModContent.ProjectileType<Golden_ShurikenP>();
            Item.shootSpeed = 10f;     //projectile speed
            Item.useTurn = true;
            Item.maxStack = 999;       //this is the max stack of this item
            Item.consumable = true;  //this make the item consumable when used
            Item.noUseGraphic = true;
        }
        public override void AddRecipes()	   
     	{
            CreateRecipe()		
	        .AddRecipeGroup("GoldBar",1)
            .AddIngredient(ItemID.Shuriken,100)
	        .AddTile(TileID.Anvils)
			.Register();
	    }
	}
}
