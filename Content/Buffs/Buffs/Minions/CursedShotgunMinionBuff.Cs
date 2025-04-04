using RemnantOfTheAncientsMod.Common.UtilsTweaks;
using RemnantOfTheAncientsMod.Content.NPCs;
using RemnantOfTheAncientsMod.Content.Projectiles.Summon.Minioms;
using Terraria;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace RemnantOfTheAncientsMod.Content.Buffs.Buffs.Minions
{
    public class CursedShotgunMinionBuff : ModBuff
    {
        public override void SetStaticDefaults()
        {   
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            Main.buffNoTimeDisplay[Type] = true;
            if (player.ownedProjectileCounts[ProjectileType<CursedShotgunMinion>()] > 0)
            {
                player.buffTime[buffIndex] = (int)Utils1.FormatTimeToTick(1, 0, 0, 0);
            }
            else
            {
               player.DelBuff(buffIndex);
               buffIndex--;
            }
        }
    }
}