using RemnantOfTheAncientsMod.Common.Global.NPCs;
using RemnantOfTheAncientsMod.Content.NPCs;
using Terraria;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Content.Buffs.Debuff
{
	public class Javalina : ModBuff
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Javalina");
			//Description.SetDefault("Losing life");
		}

		public override void Update(NPC npc, ref int buffIndex)
		{
			npc.GetGlobalNPC<RemnantGlobalNPC>().Javalina = true;
		}
	}
}
