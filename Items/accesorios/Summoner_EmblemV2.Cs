using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;

namespace opswordsII.Items.accesorios
{
    public class Summoner_EmblemV2 : ModItem
    {
     public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Summoner Emblem V2");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Polish), "Godło Przywoływacza V2");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Emblème D'invocateur V2");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Emblema de Invocador V2");
			Tooltip.SetDefault("Increases the summon damage by 25%");
            Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Polish), "Zwiększ obrażenia przywołania o 25%");
            Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Augmente les dégâts d'invocation de 25%");
            Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Aumenta el daño de las imvocaciones en un 25 ");


            /*
             DisplayName.AddTranslation(GameCulture.Russian, "Эмблема Призывателя V2");
             DisplayName.AddTranslation(GameCulture.Chinese, "召唤者徽记V2");
            Tooltip.AddTranslation(GameCulture.Russian, "Увеличение урона от призыва на 25%");
             Tooltip.AddTranslation(GameCulture.Chinese, "召唤伤害提高25％");
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
            .AddIngredient(ItemID.SummonerEmblem, 1)
            .AddIngredient(ItemID.LunarBar, 10)  //you need 10 Wood
            .AddTile(TileID.LunarCraftingStation)  //at work bench
            
            .Register();
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetDamage(DamageClass.Summon) *= 1.25f;

        }
    }
}