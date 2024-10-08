using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;

namespace opswordsII.Items.accesorios
{
    public class Ranger_EmblemV2 : ModItem
    {
     public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Ranger Emblem V2");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Polish), "Godło Strażnika V2");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Ranger Emblem V2");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Emblema de Guardian V2");
			Tooltip.SetDefault("Increases ranged damage by 25%");
            Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Polish), "Zwiększ obrażenia dystansowe o 25%");
            Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Augmente les dégâts de distance de 25%");
             Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Aumenta el daño a distancia en un 25% ");
            


            /* 
            DisplayName.AddTranslation(GameCulture.Russian, "Эмблема рейнджеров V2");
            DisplayName.AddTranslation(GameCulture.Chinese, "游侠标志V2");
            Tooltip.AddTranslation(GameCulture.Russian, "Увеличение урона на расстоянии на 25%");
            Tooltip.AddTranslation(GameCulture.Chinese, "距离伤害提高25％");
            */
		}

        public override void SetDefaults()
        {

            Item.width = 10;
            Item.height = 14;
            Item.value = 350000;
            Item.rare = 10;
            Item.accessory = true;
        }
        public override void AddRecipes()  //How to craft this item
        {
            CreateRecipe()
            .AddIngredient(ItemID.FragmentVortex, 10) 
            .AddIngredient(ItemID.FragmentNebula, 10)
            .AddIngredient(ItemID.FragmentSolar, 10)
            .AddIngredient(ItemID.FragmentStardust, 10)
            .AddIngredient(ItemID.RangerEmblem, 1)
            .AddIngredient(ItemID.LunarBar, 10) //you need 10 Wood
            .AddTile(TileID.LunarCraftingStation)  //at work bench
            .Register();
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetDamage(DamageClass.Ranged) *= 1.25f;

        }
    }
}