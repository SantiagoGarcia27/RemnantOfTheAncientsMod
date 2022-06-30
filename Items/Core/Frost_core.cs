using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using MonoMod.Cil;
using static Mono.Cecil.Cil.OpCodes;

namespace opswordsII.Items.Core
{
    [AutoloadEquip(EquipType.Balloon)]
    public class Frost_core: ModItem
    {
        

		// This IL editing (Intermediate Language editing) example is walked through in the guide: https://github.com/tModLoader/tModLoader/wiki/Expert-IL-Editing#example---hive-pack-upgrade
     public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Frozen Assaulter Core");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Polish), "Mrożony rdzeń Assaulter");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Noyau d'assaillant gelé");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Núcleo de asaltante congelado");
			Tooltip.SetDefault("Increases magic damage by 10%"
                +"\n Movement speed increases and you become immune to frozen and chilled debuffs while in snow");
            Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Polish),"Zwiększ obrażenia magiczne o 10%" 
                +"\n Szybkość ruchu jest zwiększona i jesteś odporny na debuff Frozen i Chilled, gdy jesteś na śniegu");
            Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Augmente les dégâts magiques de 10%"
            +"\n La vitesse de déplacement est augmentée et vous êtes immunisé contre le debuff Frozen et Chilled lorsque vous êtes dans la neige");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Aumenta el daño magico en un 25%"
            +"\n La velocidad de movimiento aumenta y eres inmune a la desventaja de Congelado y Helado cuando estás en la nieve.");
           

            /*
            DisplayName.AddTranslation(GameCulture.Russian, "Ледяное ядро ​​штурмовика");
            DisplayName.AddTranslation(GameCulture.Chinese, "冰冻突击队核心");
            Tooltip.AddTranslation(GameCulture.Russian, "Скорость передвижения увеличена, и вы невосприимчивы к дебаффу Frozen и Chilled, когда находитесь в снегу.");
            Tooltip.AddTranslation(GameCulture.Chinese, "运动速度加快，在雪中时，您可以免受冷冻和冰冻的减益");
             */
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

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
                player.GetDamage(DamageClass.Magic) += 0.10f;
            	if (player.ZoneSnow) {
				player.moveSpeed += 1.50f;
                player.buffImmune[BuffID.Chilled] = true;
			    player.buffImmune[BuffID.Frozen] = true;

			}

        }
    }
}