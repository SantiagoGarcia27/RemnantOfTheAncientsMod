using Terraria;
using Terraria.ModLoader;
using Terraria.Localization;

namespace RemnantOfTheAncientsMod.Buffs.Buffs.Kits
{
    public class Basic_Infusion : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Basic infusion");
            Description.SetDefault("");
            

            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = false; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {
            RemnantPlayer.BasicInfusion(player);
        }
     
    }
}
  