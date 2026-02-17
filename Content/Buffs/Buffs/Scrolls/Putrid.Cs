using Terraria;
using Terraria.ModLoader;
using Terraria.Localization;
using static Terraria.ModLoader.ModContent;

namespace RemnantOfTheAncientsMod.Content.Buffs.Buffs.Scrolls
{
    public class Putrid : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.persistentBuff[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
            Main.debuff[Type] = false; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.endurance += 0.10f;
            player.GetCritChance(DamageClass.Generic) += 5;
            player.GetDamage(DamageClass.Summon) += 0.30f;
            int Buff = BuffType<Putrid>();
            player.GetModPlayer<RemnantPlayer>().ScrollImmunity(Buff);
        }
    }
}
  