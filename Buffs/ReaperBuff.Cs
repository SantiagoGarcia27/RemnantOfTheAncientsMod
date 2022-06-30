using opswordsII.World;
using Terraria;
using Terraria.ModLoader;

namespace opswordsII.Buffs
{
    public class ReaperBuff : ModBuff
	    {

		public override void SetStaticDefaults() { 
			DisplayName.SetDefault("Reaper mode");
			Description.SetDefault("");
			Main.buffNoSave[Type] = false;
			Main.buffNoTimeDisplay[Type] = true;
			Main.persistentBuff[Type] = true;
		}
		
		public override void Update(Player player, ref int buffIndex) {
			player.GetDamage(DamageClass.Generic) += 0.50f;
			player.statLifeMax2 = 1;
			if (world1.ReaperMode == true) {
				player.buffTime[buffIndex] = 1;
			}
			else {
				player.DelBuff(buffIndex);
				buffIndex--;
			}
		}
	}
}