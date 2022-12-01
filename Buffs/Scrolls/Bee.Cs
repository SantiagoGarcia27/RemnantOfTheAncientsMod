using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using static Terraria.ModLoader.ModContent;

namespace RemnantOfTheAncientsMod.Buffs.Scrolls
{
    public class Bee : ModBuff
    {
        public override void SetStaticDefaults()
        {
              DisplayName.SetDefault("Queen Bee");
              Description.SetDefault("");   
              DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "reine des abeilles");
              DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "abeja reina");
             
            Main.buffNoTimeDisplay[Type] = true;
            Main.debuff[Type] = false; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {
            
            player.strongBees = true;
		    player.honey = true;
            //player.Bee = true;
            player.statDefense = 5;
            player.GetArmorPenetration(DamageClass.Generic) = 15;
            player.GetModPlayer<Player1>().ScrollInmunity(BuffType<Bee>());
        }
    }
}
  