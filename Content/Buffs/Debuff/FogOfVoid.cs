using RemnantOfTheAncientsMod.Common.RemPlayer;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

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
			player.moveSpeed *= 0.3f; 
			player.maxRunSpeed *= 0.3f;
			player.accRunSpeed *= 0.3f;
			player.GetModPlayer<DrawEffectPlayer>().FogOfVoidEffect = true;

        }
	}
}
