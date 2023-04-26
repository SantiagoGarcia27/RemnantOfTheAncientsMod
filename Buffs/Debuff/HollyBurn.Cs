using RemnantOfTheAncientsMod.NPCs;
using Terraria;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.ID;

namespace RemnantOfTheAncientsMod.Buffs.Debuff
{
	// Ethereal Flames is an example of a buff that causes constant loss of life.
	// See ExamplePlayer.UpdateBadLifeRegen and ExampleGlobalNPC.UpdateLifeRegen for more information.
	public class HollyBurn : ModBuff
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Holly Burn");
			Main.debuff[Type] = true;
			Main.pvpBuff[Type] = true;
			Main.buffNoSave[Type] = true;
			BuffID.Sets.LongerExpertDebuff[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			player.GetModPlayer<RemnantPlayer>().hBurn = true;
		}

		public override void Update(NPC npc, ref int buffIndex)
		{
			npc.GetGlobalNPC<GlobalNPC1>().hBurn = true;
		}

	}
}
