using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using opswordsII;
using MonoMod.Cil;
using static Mono.Cecil.Cil.OpCodes;
using static Terraria.ModLoader.ModContent;

namespace opswordsII.Items.Core
{
    [AutoloadEquip(EquipType.Balloon)]
    public class Infernal_core: ModItem
    {
        

		
     public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Infernal Tyrant Core");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Noyau du tyran infernal");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Núcleo de Tirano infernal");
			Tooltip.SetDefault("Increases melee damage by 10%"
                +"\n Inflict Hellfire damage on attack"
                +"\n Movement speed increases and you become immune to fire blocks and On Fire debuffs");
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
                player.GetDamage(DamageClass.Melee) += 0.10f;
                Player1 player1 = player.GetModPlayer<Player1>();
                player1.hasInfernal_core = true;
				player.moveSpeed += 1.50f;
                player.buffImmune[BuffID.OnFire] = true;
			

        }
    }
}