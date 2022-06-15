using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
namespace opswordsII.Items.Pociones
{
 public class UltraHealtingPotion : ModItem
    {
       public override void SetStaticDefaults()
        {
          DisplayName.SetDefault("Ultra Healing potion");
          DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Polish), "Mikstura ultra uzdrawiająca");
          DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Potion de guérison ultra");
          DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Ultra poción curativa");
        }
        
            public override void SetDefaults() 
          {
			Item.width = 20;
			Item.height = 26;
			Item.useStyle = ItemUseStyleID.EatFood;
			Item.useAnimation = 17;
			Item.useTime = 17;
			Item.useTurn = true;
			Item.UseSound = SoundID.Item3;
			Item.maxStack = 999;
			Item.consumable = true;
			Item.rare = 9;
			Item.healLife = 300; // While we change the actual healing value in GetHealLife, Item.healLife still needs to be higher than 0 for the item to be considered a healing item
			Item.potion = true; // Makes it so this item applies potion sickness on use and allows it to be used with quick heal
			Item.value = Item.buyPrice(gold: 10);
		}
		
        
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ItemID.SuperHealingPotion,4)
			.AddIngredient(ItemID.LunarBar,1)
			.AddIngredient(null,"Neutrum_Fragment",3)
			.AddTile(TileID.LunarCraftingStation)
			.Register();
		    
        }
    }
}    