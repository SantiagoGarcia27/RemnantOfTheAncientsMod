using Terraria;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Content.Buffs.Buffs
{
	public class FishBuff : ModBuff
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Fish");
			Description.SetDefault("");
			Main.buffNoSave[Type] = false;
			Main.buffNoTimeDisplay[Type] = false;
			Main.persistentBuff[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			PlayerBuffEffects.FishPotion(player);
		}
	}
}