using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;

namespace opswordsII.Items.accesorios
{
    public class F : ModItem
    {
     public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("F");
			Tooltip.SetDefault("A cosmic power emanates from this object"
            + "\n Activates F mode when equipping"
            + "\n Increases all damage by 50%, but at what cost ...");
            Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Polish), "Z tego obiektu wypływa kosmiczna moc"
            + "\n Aktywuj tryb F"          
            + "\n Zwiększa wszystkie obrażenia o 50%, ale jakim kosztem...");
            Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Une puissance cosmique jaillit de cet objet"
            + "\n Activer le mode F lors de l'équipement"
            + "\n Augmente tous les dégâts de 50%, mais à quel prix...");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Un poder cósmico brota de este objeto"
            + "\n Activar el modo F al equiparse"
            + "\n Aumenta todo el daño en un 50%, pero a que costo...");
            

            /* Tooltip.AddTranslation(GameCulture.Russian, "От этого объекта исходит космическая сила"
            + "\n Активировать режим F при экипировке");
             Tooltip.AddTranslation(GameCulture.Chinese, "宇宙力量从这个物体中产生"
            + "\n 装备时激活F模式");*/
		}

        public override void SetDefaults()
        {

            Item.width = 10;
            Item.height = 14;
            Item.value = 0;
            Item.rare = -11;
            Item.accessory = true;
        }
          public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.statLifeMax2 = 1;
             player.GetDamage(DamageClass.Generic) *= 1.50f;
        }
      
             public override void AddRecipes()
		{
			CreateRecipe()
			.AddRecipeGroup("Tumbas")
			.AddTile(TileID.DemonAltar)
			.Register();
	    }
	}
}
        