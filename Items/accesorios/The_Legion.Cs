using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using static Terraria.ModLoader.ModContent;

namespace opswordsII.Items.accesorios
{
    public class The_Legion : ModItem
    {
     public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("The Legion");
            Tooltip.SetDefault("Increases the summon damage by 5% "
			+ "\n +5 minions"
           
            +"\nMany thanks to"
           +"\n S-Pladison   -NKSHV  -perrogalan"
           +"\n-Banmei   -Scratch Lunin  -DinoZers -scorpion17907" 
            +"\n Legendary Drop");
           
		}

        public override void SetDefaults()
        {

            Item.width = 10;
            Item.height = 14;
            Item.value = 750000;
            Item.rare = -12;
            Item.accessory = true;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {

            if (player.name == "S-Pladison" || player.name == "NKSHV" || player.name == "perrogalan" || player.name == "Banmei" || player.name == "Scratch Lunin" || player.name == "DinoZers" || player.name == "scorpion17907") {
				player.GetDamage(DamageClass.Summon) += 0.25f;
                player.maxMinions+= 8; 
            }
			else {
				player.GetDamage(DamageClass.Summon) *= 1.5f;
                player.maxMinions+= 5;
			}
		}
     }
}

