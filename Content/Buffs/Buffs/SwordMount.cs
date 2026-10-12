using RemnantOfTheAncientsMod.Content.NPCs.Mounts;
using Terraria;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Content.Buffs.Buffs
{
	public class SwordMount : ModBuff
	{
		public override void SetStaticDefaults()
		{
            Main.buffNoTimeDisplay[Type] = true;
			Main.buffNoSave[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			player.mount.SetMount(ModContent.MountType<Sword>(), player);
			player.buffTime[buffIndex] = 10;
		}
	}
}
