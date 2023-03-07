using Terraria;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Buffs.Buffs
{
	public class CentryBuff : ModBuff
	{
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("Comander Potion");
            Description.SetDefault("");
            Main.buffNoSave[Type] = false;
			Main.buffNoTimeDisplay[Type] = false;
			Main.persistentBuff[Type] = true;
		}
		
		public override void Update(Player player, ref int buffIndex) 
		{
			player.maxTurrets += 1;	
		}
	}
}