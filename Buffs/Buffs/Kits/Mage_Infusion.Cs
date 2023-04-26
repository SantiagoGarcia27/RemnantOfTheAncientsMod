using Terraria;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Buffs.Buffs.Kits
{
    public class Mage_Infusion : ModBuff
    {
        public override void SetStaticDefaults()
        {
            //DisplayName.SetDefault("Mage infusion");
            //Description.SetDefault("");
            

            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = false; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {
            RemnantPlayer.MageInfusion(player);
        }
     
    }
}
  