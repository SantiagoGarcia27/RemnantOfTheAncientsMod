using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using RemnantOfTheAncientsMod.World;
using SangarUtilities.Common.UtilsTweaks;

namespace RemnantOfTheAncientsMod.Content.Buffs.Debuff
{
    public class DarksignBuff : ModBuff
	{
		public override void SetStaticDefaults()
		{
			Main.debuff[Type] = true;
			Main.pvpBuff[Type] = true;
			Main.buffNoSave[Type] = true;
			BuffID.Sets.LongerExpertDebuff[Type] = false;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			int lifeDecreased = (int)MathUtils.GetValueFromPorcentage(player.statLifeMax2, 20);

            if(!Reaper.ReaperMode) player.statLifeMax2 -= lifeDecreased;
            player.GetDamage(DamageClass.Generic) += .2f;
        }

		public override void Update(NPC npc, ref int buffIndex)
		{
		}

	}
}
