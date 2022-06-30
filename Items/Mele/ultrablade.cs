using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria.Localization;
using opswordsII.Projectiles;

namespace opswordsII.Items.Mele
{
	public class ultrablade : ModItem
	{
		public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("UltraBlade");
		    Tooltip.SetDefault("");
		}
	    public override void SetDefaults()
		{ 
	    Item.damage = 340;                                         
		Item.DamageType = DamageClass.Melee;
        Item.width = 40;
        Item.height = 40;
	    Item.useTime = 18;
	    Item.useAnimation = 18;
        Item.useStyle = 1;
        Item.knockBack =1;
        Item.autoReuse = true;
        Item.UseSound = SoundID.Item100; //46 
		Item.value = Item.sellPrice(gold: 11);
		Item.rare = 10;
		Item.scale = 1.20f;
	    Item.shoot = ModContent.ProjectileType<UltraBladeS>(); 
        Item.shootSpeed = 10f;
	    Item.noUseGraphic = false;
		}
        public override void AddRecipes()	   
     	{
            CreateRecipe()		
	        .AddIngredient(ItemID.InfluxWaver, 1)
	        .AddIngredient(ItemID.Meowmere, 1)
	        .AddIngredient(ItemID.TerraBlade, 1)
	        .AddTile(TileID.LunarCraftingStation)
			.Register();
	    }
	}
}
