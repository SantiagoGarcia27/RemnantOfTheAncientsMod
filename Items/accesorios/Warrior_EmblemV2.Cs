using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;

namespace opswordsII.Items.accesorios
{
    public class Warrior_EmblemV2 : ModItem
    {
     public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Warrior Emblem V2");
            
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Polish), "Warrior Emblem V2");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Emblème Guerrier V2");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Emblema de Guerrero V2");   
			Tooltip.SetDefault("Increases melee damage by 25%");
            Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Polish), "Zwiększa obrażenia w walce wręcz o 25%");
            Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Augmente les dégâts de mêlée de 25%");
             Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Aumenta el daño cuerpo a cuerpo en un 25% ");
            

           /* Mod CalamityMod = ModLoader.GetMod("CalamityMod");
    		if (CalamityMod != null)

            Tooltip.SetDefault("Increase melee damage by 20%");
            Tooltip.AddTranslation(GameCulture.Russian, "Увеличение урона в ближнем бою на 20%");
            Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Polish), "Zwiększa obrażenia w walce wręcz o 20%");
            Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Augmente les dégâts de mêlée de 20%");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Aumenta el daño cuerpo a cuerpo en un 20% ");
            Tooltip.AddTranslation(GameCulture.Chinese, "近战伤害提高20％");*/

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
            .AddIngredient(ItemID.WarriorEmblem, 1)
            .AddIngredient(ItemID.LunarBar, 10) //you need 10 Wood
            .AddTile(TileID.LunarCraftingStation)  //at work bench
            .Register();
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetDamage(DamageClass.Melee) *= 1.25f;

            /*Mod CalamityMod = ModLoader.GetMod("CalamityMod");
    		if (CalamityMod != null)

			player.GetDamage(DamageClass.Melee) += 0.20f; */
        }
    }
}