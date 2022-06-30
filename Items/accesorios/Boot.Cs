using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;

namespace opswordsII.Items.accesorios
{
    [AutoloadEquip(EquipType.Shoes)]
    public class Boot : ModItem
    {
     public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Boot");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Polish), "Bagażnik");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Botte");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Bota");
		}

        public override void SetDefaults()
        {

            Item.width = 10;
            Item.height = 14;
            Item.value = 50000;
            Item.rare = 1;
            Item.accessory = true;
            Item.defense = 3;
        }
          public override void UpdateAccessory(Player player, bool hideVisual)
        {

        }
        public override void AddRecipes()  //How to craft this item
        {
            CreateRecipe()
            .AddIngredient(ItemID.Leather, 3) 
            .AddTile(TileID.WorkBenches)  //at work bench
            .Register();
        }
    }
}