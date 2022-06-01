using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;

namespace opswordsII.Items.accesorios
{
    public class magic_stick : ModItem
    {
     public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Magic Stick");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Polish), "Magiczny Kij");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Bâton Magique");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Palo Mágico");
			Tooltip.SetDefault("Increase magic damage by 5%");
            Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Polish), "Zwiększ obrażenia magiczne o 5%");
            Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Augmente les dégâts magiques de 5%");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Aumenta el daño magico en un 5%");
            

            /*
            DisplayName.AddTranslation(GameCulture.Russian, "Волшебная палочка");
            DisplayName.AddTranslation(GameCulture.Chinese, "魔术棒");
            Tooltip.AddTranslation(GameCulture.Russian, "Увеличение магического урона на 5%");
            Tooltip.AddTranslation(GameCulture.Chinese, "魔法伤害提高5％");*/
		}

        public override void SetDefaults()
        {

            Item.width = 10;
            Item.height = 14;
            Item.value = 100000;
            Item.rare = 2;
            Item.accessory = true;
        }
          public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetDamage(DamageClass.Magic) *= 1.05f;

        }
    }
}