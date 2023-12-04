using Terraria;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace RemnantOfTheAncientsMod.Content.Buffs.Buffs.Scrolls
{
    public class Bee : ModBuff
    {
        public override void SetStaticDefaults()
        {
          
            Main.persistentBuff[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
            Main.debuff[Type] = false; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {
            
            player.strongBees = true;
		    player.honey = true;
            //player.Bee = true;
            player.statDefense += 5;
            player.GetArmorPenetration(DamageClass.Generic) = 15;
            int Buff = BuffType<Bee>();
            player.GetModPlayer<RemnantPlayer>().ScrollInmunity(Buff);
           
        }
    }
}
  