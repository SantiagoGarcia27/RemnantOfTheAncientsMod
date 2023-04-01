using RemnantOfTheAncientsMod.NPCs;
using Terraria;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Buffs.Debuff
{
	public class Javalina : ModBuff
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Javalina");
			Description.SetDefault("Losing life");
		}

		public override void Update(NPC npc, ref int buffIndex)
		{
			npc.GetGlobalNPC<GlobalNPC1>().Javalina = true;
		}
	}
}
