using RemnantOfTheAncientsMod.NPCs;
using Terraria;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.ID;

namespace RemnantOfTheAncientsMod.Buffs.Debuff
{
	// Ethereal Flames is an example of a buff that causes constant loss of life.
	// See ExamplePlayer.UpdateBadLifeRegen and ExampleGlobalNPC.UpdateLifeRegen for more information.
	public class Marble_erosion : ModBuff
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Marble rosion");
			Main.debuff[Type] = true;
			Main.pvpBuff[Type] = true;
			Main.buffNoSave[Type] = true;
			BuffID.Sets.LongerExpertDebuff[Type] = true;
		}

		public override void Update(NPC npc, ref int buffIndex)
		{
			npc.GetGlobalNPC<GlobalNPC1>().Marble_Erosion = true;
		}

	}
}
