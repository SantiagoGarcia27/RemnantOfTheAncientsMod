using Terraria;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Content.Buffs.Buffs
{
	public class Night_Bleasing_Buff : ModBuff
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Night Bleasing");
			//Description.SetDefault("Increase defense by 5");
			Main.buffNoSave[Type] = false;
			Main.buffNoTimeDisplay[Type] = false;
			Main.persistentBuff[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			player.statDefense += 5;
		}
	}
}