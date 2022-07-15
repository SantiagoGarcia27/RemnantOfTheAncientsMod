using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using opswordsII.Projectiles;
using Terraria.GameContent.Creative;

namespace opswordsII.Items.Lanzables
{
    public class Golden_Shuriken: ModItem
	{
		public override void SetStaticDefaults()
		{
	  	    DisplayName.SetDefault("Golden Shuriken");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Shuriken doré");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Shuriken dorado");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 99;
        }
		public override void SetDefaults()
		{ 
	        Item.damage = 30;           
            Item.DamageType = DamageClass.Throwing;      
            Item.noMelee = true;
            Item.width = 20;
            Item.height = 20;
            Item.useTime = 15;     
            Item.useAnimation = 15;  
            Item.useStyle = ItemUseStyleID.Swing;      
            Item.knockBack = 6;
            Item.value = 4;
            Item.rare = 1;
            Item.reuseDelay = 10;   
            Item.UseSound = SoundID.Item18;
            Item.autoReuse = false;
	        Item.shoot = ModContent.ProjectileType<Golden_ShurikenP>();
            Item.shootSpeed = 10f;    
            Item.useTurn = true;
            Item.maxStack = 999;  
            Item.consumable = true; 
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
