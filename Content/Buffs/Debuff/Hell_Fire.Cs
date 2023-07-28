using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using RemnantOfTheAncientsMod.Content.NPCs;

namespace RemnantOfTheAncientsMod.Content.Buffs.Debuff
{
	public class Hell_Fire : ModBuff
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Hell Fire");
			//Description.SetDefault("...");
			Main.debuff[Type] = true;
			Main.pvpBuff[Type] = true;
			Main.buffNoSave[Type] = true;
			BuffID.Sets.LongerExpertDebuff[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			player.GetModPlayer<RemnantPlayer>().Hell_Fire = true;
		}

		public override void Update(NPC NPC, ref int buffIndex)
		{
			NPC.GetGlobalNPC<RemnantGlobalNPC>().Hell_Fire = true;
		}

	}
}
