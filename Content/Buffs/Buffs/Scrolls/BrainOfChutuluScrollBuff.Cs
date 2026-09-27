using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace RemnantOfTheAncientsMod.Content.Buffs.Buffs.Scrolls
{
    public class BrainOfChutuluScrollBuff : ModBuff
    {
        public override void SetStaticDefaults()
        {

            Main.persistentBuff[Type] = true;
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = false; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.shadowDodge = true;
            int Buff = BuffType<BrainOfChutuluScrollBuff>();
            player.GetModPlayer<RemnantPlayer>().ScrollImmunity(Buff);
           
        }
    }
}
  