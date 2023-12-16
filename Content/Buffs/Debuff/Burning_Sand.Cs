using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using RemnantOfTheAncientsMod.Common.Global.NPCs;

namespace RemnantOfTheAncientsMod.Content.Buffs.Debuff
{
    public class Burning_Sand : ModBuff
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Burning Sand");
			//Description.SetDefault("");
			Main.debuff[Type] = true;
			Main.pvpBuff[Type] = true;
			Main.buffNoSave[Type] = true;
			BuffID.Sets.LongerExpertDebuff[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			player.GetModPlayer<RemnantPlayer>().Burn_Sand = true;
		}

		public override void Update(NPC npc, ref int buffIndex)
		{
			npc.GetGlobalNPC<RemnantGlobalNPC>().Burn_Sand = true;
		}

	}
}
