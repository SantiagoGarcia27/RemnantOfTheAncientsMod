using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using RemnantOfTheAncientsMod.Common.RemPlayer;

namespace RemnantOfTheAncientsMod.Content.Buffs.Debuff
{
    public class FogOfVoid : ModBuff
	{
		public override void SetStaticDefaults()
		{
			Main.debuff[Type] = true;
			Main.pvpBuff[Type] = true;
			Main.buffNoSave[Type] = true;
			BuffID.Sets.LongerExpertDebuff[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			player.moveSpeed *= 0.01f; 
			player.maxRunSpeed *= 0.01f;
			player.accRunSpeed *= 0.01f;
			player.GetModPlayer<DrawEffectPlayer>().FogOfVoidEffect = true;

        }

		public override void Update(NPC npc, ref int buffIndex)
		{
			npc.velocity *= 0.25f;
		}

	}
}
