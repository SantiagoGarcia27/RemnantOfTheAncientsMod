using RemnantOfTheAncientsMod.World;
using Terraria;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Content.Buffs.Buffs
{
    public class ReaperBuff : ModBuff
	{

		public override void SetStaticDefaults()
		{	
            Main.buffNoSave[Type] = true;
			Main.buffNoTimeDisplay[Type] = true;
			Main.persistentBuff[Type] = false;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			player.GetDamage(DamageClass.Generic) += 0.50f;
			player.statLifeMax2 = 1;
            player.maxMinions++;
            player.statManaMax2 += 20;
			if (Reaper.ReaperMode) 
				player.buffTime[buffIndex] = 12;
			else
			{
				player.DelBuff(buffIndex);
				buffIndex--;
			}
		}
	}
}