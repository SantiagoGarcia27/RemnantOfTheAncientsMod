using RemnantOfTheAncientsMod.Content.NPCs;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using RemnantOfTheAncientsMod.Common.Global.NPCs;

namespace RemnantOfTheAncientsMod.Content.Buffs.Debuff
{
	public class Marble_erosion : ModBuff
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Marble rosion");
			Main.debuff[Type] = true;
			Main.pvpBuff[Type] = true;
			Main.buffNoSave[Type] = true;
			BuffID.Sets.LongerExpertDebuff[Type] = true;
		}

		public override void Update(NPC npc, ref int buffIndex)
		{
			npc.GetGlobalNPC<RemnantGlobalNPC>().Marble_Erosion = true;
			npc.defDefense -= 2;
		}
		public override void Update(Player player, ref int buffIndex)
		{
			player.GetModPlayer<RemnantPlayer>().Marble_Erosion = true;
			player.statDefense -= 2;
		}
	}
}
