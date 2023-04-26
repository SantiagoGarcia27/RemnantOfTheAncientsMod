using RemnantOfTheAncientsMod.NPCs;
using RemnantOfTheAncientsMod;
using Terraria;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.ID;

namespace RemnantOfTheAncientsMod.Buffs.Debuff
{
	// Ethereal Flames is an example of a buff that causes constant loss of life.
	// See ExamplePlayer.UpdateBadLifeRegen and ExampleGlobalNPC.UpdateLifeRegen for more information.
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
			NPC.GetGlobalNPC<GlobalNPC1>().Hell_Fire = true;
		}

	}
}
