using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using static Terraria.ModLoader.ModContent;
using Terraria.GameContent.Creative;

namespace opswordsII.Items.accesorios
{
    public class Universal_EmblemV2 : ModItem
    {
     public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Universal Emblem V2");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Polish), "Uniwersalny Emblemat V2");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Emblème Universel V2");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Emblema Universal V2");
            
            
			Tooltip.SetDefault("Increases all damage by 25% ");
            Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Polish), "Zwiększa wszystkie obrażenia o 25%");
            Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Augmente tous les dégâts de 25%");
            Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Aumenta todo el daño en un 25%");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;


            /*
             DisplayName.AddTranslation(GameCulture.Russian, "Универсальная эмблема V2");
             DisplayName.AddTranslation(GameCulture.Chinese, "通用徽 V2");
             Tooltip.AddTranslation(GameCulture.Russian, "Увеличивают весь урон на 25%");
             Tooltip.AddTranslation(GameCulture.Chinese, "所有伤害提高25％");
              */


            /*  Mod CalamityMod = ModLoader.GetMod("CalamityMod");
              if (CalamityMod != null)

              Tooltip.SetDefault("Increase all damage by 20% ");
              Tooltip.AddTranslation(GameCulture.Russian, "Увеличивают весь урон на 20%");
              Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Polish), "Zwiększa wszystkie obrażenia o 20%");
              Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Augmente tous les dégâts de 20%");
              DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Aumenta todo el daño en un 20%");
              Tooltip.AddTranslation(GameCulture.Chinese, "所有伤害提高20％"); */
        }

        public override void SetDefaults()
        {

            Item.width = 10;
            Item.height = 14;
            Item.value = 450000;
            Item.rare = 4;
            Item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetDamage(DamageClass.Generic) *= 1.25f;

            /*
            Mod CalamityMod = ModLoader.GetMod("CalamityMod");
    		if (CalamityMod != null)

			player.GetDamage(DamageClass.Generic) += 0.20f; 
            */
        }
                    public override void AddRecipes()  //How to craft this item
        {
            CreateRecipe()
            .AddIngredient(null,"Sorcerer_EmblemV2", 1) 
            .AddIngredient(null,"Ranger_EmblemV2", 1)
            .AddIngredient(null,"Summoner_EmblemV2", 1)
            .AddIngredient(null,"TrowerEmblemV2", 1)
            .AddIngredient(null,"Warrior_EmblemV2", 1)
            .AddIngredient(ItemID.LunarBar, 10)  //you need 10 Wood
            .AddTile(TileID.LunarCraftingStation)   //at work bench
            .Register();
        }


        
    }
}