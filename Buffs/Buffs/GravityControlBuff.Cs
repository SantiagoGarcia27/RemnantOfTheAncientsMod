using Terraria;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Buffs.Buffs
{
	public class GravityControlBuff : ModBuff
	{
		public override void SetStaticDefaults() {
//DisplayName.SetDefault("Gravity control");
            //Description.SetDefault("");
            Main.buffNoSave[Type] = false;
Main.buffNoTimeDisplay[Type] = false;
Main.persistentBuff[Type] = true;
		}
		
		public override void Update(Player player, ref int buffIndex) 
		{
RemnantPlayer.GravityControlPotion(player);
		}
	}
}