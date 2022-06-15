using Terraria;
using Terraria.ModLoader;
using Terraria.Localization;

namespace opswordsII.Buffs
{
	public class SwordMount : ModBuff
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Espada de transporte");
			Description.SetDefault("La forma de viajar mas eficaz");
			Main.buffNoTimeDisplay[Type] = true;
			Main.buffNoSave[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			player.mount.SetMount(ModContent.MountType<Mounts.Sword>(), player);
			player.buffTime[buffIndex] = 10;
		}
	}
}
