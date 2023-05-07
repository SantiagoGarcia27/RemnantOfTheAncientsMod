using Terraria;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Content.Buffs.Buffs
{
	public class SuperSenseBuff : ModBuff
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Super Sense");
			Description.SetDefault("");
			Main.buffNoSave[Type] = false;
			Main.buffNoTimeDisplay[Type] = false;
			Main.persistentBuff[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			RemnantPlayer.SuperSenseBuff(player);
		}
	}
}