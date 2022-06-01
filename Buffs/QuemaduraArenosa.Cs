using opswordsII.NPCs;
using Terraria;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.ID;

namespace opswordsII.Buffs
{
	// Ethereal Flames is an example of a buff that causes constant loss of life.
	// See ExamplePlayer.UpdateBadLifeRegen and ExampleGlobalNPC.UpdateLifeRegen for more information.
	public class QuemaduraArenosa : ModBuff
	{
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("Quemadura de arena");
			Description.SetDefault("Estas perdiendo Vida");
			Main.debuff[Type] = true;
			Main.pvpBuff[Type] = true;
			Main.buffNoSave[Type] = true;
			BuffID.Sets.LongerExpertDebuff[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex) {
			player.GetModPlayer<Player1>().qArenosa = true;
		}

		public override void Update(NPC NPC, ref int buffIndex) {
			NPC.GetGlobalNPC<GlobalNPC1>().qArenosa = true;
		}

	}
}
