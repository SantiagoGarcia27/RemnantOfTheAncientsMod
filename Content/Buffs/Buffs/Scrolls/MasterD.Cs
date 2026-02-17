using Terraria;
using Terraria.ModLoader;
using Terraria.Localization;
using static Terraria.ModLoader.ModContent;

namespace RemnantOfTheAncientsMod.Content.Buffs.Buffs.Scrolls
{
    public class MasterD : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.persistentBuff[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
            Main.debuff[Type] = false;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.maxMinions += 1;
            player.GetDamage(DamageClass.Summon) += 0.05f;
            player.moveSpeed += 0.50f;
            int Buff = BuffType<MasterD>();
            player.GetModPlayer<RemnantPlayer>().ScrollImmunity(Buff);
        }
    }
}
  