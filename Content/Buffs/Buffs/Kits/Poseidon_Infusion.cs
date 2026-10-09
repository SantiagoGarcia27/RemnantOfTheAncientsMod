using Terraria;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Content.Buffs.Buffs.Kits
{
    public class Poseidon_Infusion : ModBuff
    {
        public override void SetStaticDefaults()
        {
           // //DisplayName.SetDefault("Poseidon infusion");
            ////Description.SetDefault("");
            

            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = false; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {
            PlayerBuffEffects.PoseidonInfusion(player);
        }
     
    }
}
  