using Terraria;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Buffs.Buffs.Kits
{
    public class Ranger_Infusion : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ranger infusion");
            Description.SetDefault("");
            

            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = false; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {
            RemnantPlayer.RangerInfusion(player);
        }
     
    }
}
  