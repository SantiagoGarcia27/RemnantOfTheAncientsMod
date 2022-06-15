using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;

namespace opswordsII.Items.accesorios
{
    public class TrowerEmblemV2 : ModItem
    {
     public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Thrower Emblem V2");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Polish), "Godło Rzucającego V2");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Emblème du Lanceur V2");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Emblema de Lanzador V2");
			Tooltip.SetDefault("Increases throwing damage by 25% ");
            Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Polish), "Zwiększa obrażenia od rzucania o 25%");
            Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Augmente les dégâts de lancer de 25%");
             Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Aumenta el daño lanzable en un 25%  ");
            

            /*
            DisplayName.AddTranslation(GameCulture.Russian, "Эмблема метателя V2");
            DisplayName.AddTranslation(GameCulture.Chinese, "投掷者徽记 V2");
            Tooltip.AddTranslation(GameCulture.Russian, "Увеличение урона от метания на 25%");
            Tooltip.AddTranslation(GameCulture.Chinese, "投掷伤害提高25％");
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
            .AddIngredient(ItemID.FragmentStardust, 10)
            .AddIngredient(null,"Neutrum_Fragment",10)
            .AddIngredient(null,"TrowerEmblem",1)
            .AddIngredient(ItemID.LunarBar, 10) //you need 10 Wood
            .AddTile(TileID.LunarCraftingStation)   //at work bench
            .Register();
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetDamage(DamageClass.Throwing) *= 1.25f;

        }
    }
}