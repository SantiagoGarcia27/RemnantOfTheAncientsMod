using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;

namespace opswordsII.Items.accesorios
{
    public class TrowerEmblem : ModItem
    {
     public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Thrower Emblem");
            
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Polish), "Godło Rzucającego");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Emblème du Lanceur");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Emblema de Lanzador");
			Tooltip.SetDefault("Increases throwing damage by 15% ");
            Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Polish), "Zwiększa obrażenia od rzucania o 15%");
            Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Augmente les dégâts de lancer de 15%");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Aumenta el daño lanzable en un 15%  ");
           


            /*
            DisplayName.AddTranslation(GameCulture.Russian, "Эмблема метателя");
            DisplayName.AddTranslation(GameCulture.Chinese, "投掷者徽记");
            Tooltip.AddTranslation(GameCulture.Russian, "Увеличение урона от метания на 15%");
            Tooltip.AddTranslation(GameCulture.Chinese, "投掷伤害提高15％");
             */
		}

        public override void SetDefaults()
        {

            Item.width = 10;
            Item.height = 14;
            Item.value = 100000;
            Item.rare = 4;
            Item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetDamage(DamageClass.Throwing) *= 1.15f;

        }
    }
}