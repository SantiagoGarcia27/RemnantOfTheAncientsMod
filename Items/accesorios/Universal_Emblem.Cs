using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using static Terraria.ModLoader.ModContent;

namespace opswordsII.Items.accesorios
{
    public class Universal_Emblem : ModItem
    {
     public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Universal Emblem");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Polish), "Uniwersalny Emblemat");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Emblème Universel");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Emblema Universal");   
			Tooltip.SetDefault("Increases all damage by 15%");
            Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Polish), "Zwiększa wszystkie obrażenia o 15%");
            Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Augmente tous les dégâts de 15%");
             Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Aumenta todo el daño en un 15%");
          
            /* 
             DisplayName.AddTranslation(GameCulture.Russian, "Универсальная эмблема");
             DisplayName.AddTranslation(GameCulture.Chinese, "通用徽");
             Tooltip.AddTranslation(GameCulture.Russian, "Увеличивают весь урон на 15%");
             Tooltip.AddTranslation(GameCulture.Chinese, "所有伤害提高15％");
             */



		}

        public override void SetDefaults()
        {

            Item.width = 10;
            Item.height = 14;
            Item.value = 370000;
            Item.rare = 4;
            Item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetDamage(DamageClass.Generic) *= 1.15f;
        }
    public override void AddRecipes()  //How to craft this item
        {
            CreateRecipe()
            .AddIngredient(ItemID.RangerEmblem, 1)  
            .AddIngredient(ItemID.SorcererEmblem, 1)
            .AddIngredient(ItemID.SummonerEmblem, 1)
            .AddIngredient(ItemID.WarriorEmblem, 1)
            .AddIngredient(null,"TrowerEmblem", 1)
            .AddIngredient(ItemID.HallowedBar, 10)
            .AddTile(TileID.MythrilAnvil)   //at work bench
            .Register();
        }
    }
}