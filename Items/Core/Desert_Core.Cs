using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;

namespace opswordsII.Items.Core
{
    [AutoloadEquip(EquipType.Balloon)]
    public class Desert_Core: ModItem
    {
     public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Desert Annihilator Core");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Polish), "Desert Annihilator Core");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Noyau d'aniquilateur du désert");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Núcleo del aniquilador del desierto");
            
			
            Tooltip.SetDefault("Increases your max number of minions by 2"
            +"\n Increases all damage by 8% and life regeneration while in desert"
            + "\n Your max life increases by 50 points while in sandstorms");

            Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Polish), "Zwiększ swoją maksymalną liczbę stworów o 2"
            + "\n Zwiększa wszystkie obrażenia o 8% i regenerację życia na pustyni. "
            + "\n W burzach piaskowych życie wzrasta o 50 punktów");

            Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Augmentez votre nombre maximum de serviteurs de 2"
            +"\n Augmente tous les dégâts de 8% et la régénération de la vie dans le désert. "
            +"\n Dans les tempêtes de sable, votre vie augmente de 50 points");

            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Incrementa tu número máximo de minions en 2" 
            +"\n Aumenta todo el daño en un 8% y la regeneración de vida en el desierto. "
            +"\n En las tormentas de arena tu vida aumenta en 50 puntos");

		}

        public override void SetDefaults()
        {

            Item.width = 10;
            Item.height = 14;
            Item.value = 45000;
            Item.rare = 10;
            Item.expert = true;
            Item.accessory = true;
        }

        public override void UpdateAccessory(Player p, bool hideVisual)
        {

            p.maxMinions+= 2;

            	if (p.ZoneDesert) {
				// 100% melee damage decrease
				 p.GetDamage(DamageClass.Generic) += 0.08f;
                  p.lifeRegen += 2;

                 if (p.ZoneSandstorm)
                 p.statLifeMax2 += 50;
			}

        }
    }
}