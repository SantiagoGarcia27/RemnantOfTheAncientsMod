using Terraria;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Content.Buffs.Buffs
{
	public class Sand_Weapons : ModBuff
	{
		public override void SetStaticDefaults() {
DisplayName.SetDefault("[c/48E16F:Wepon Imbue: Burning Sand]");
            Description.SetDefault("");
            Main.buffNoSave[Type] = false;
Main.buffNoTimeDisplay[Type] = false;
Main.persistentBuff[Type] = true;
		}
		
		public override void Update(Player player, ref int buffIndex) {
RemnantPlayer player1 = player.GetModPlayer<RemnantPlayer>();
player1.SandWeapons = true;
player.GetModPlayer<RemnantPlayer>().VanillaFlaskInmune();
		}
	}
}