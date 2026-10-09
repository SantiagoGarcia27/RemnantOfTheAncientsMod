using Terraria;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace RemnantOfTheAncientsMod.Content.Buffs.Buffs.Scrolls
{ 
    public class Skeleton : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.persistentBuff[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
            Main.debuff[Type] = false; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.sonarPotion = true;
            player.dangerSense = true;
            player.findTreasure = true;
            player.nightVision = true;
            player.detectCreature = true;
            Main.persistentBuff[Type] = true;
            player.ammoCost75 = true;
            player.GetDamage(DamageClass.Throwing) += .20f;

            player.GetModPlayer<RemnantPlayer>().UnreadInmunity();
            int Buff = BuffType<Skeleton>();
            player.GetModPlayer<RemnantPlayer>().ScrollImmunity(Buff);
        }
    }
}
  