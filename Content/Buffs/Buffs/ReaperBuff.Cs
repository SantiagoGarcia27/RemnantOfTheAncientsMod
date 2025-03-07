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