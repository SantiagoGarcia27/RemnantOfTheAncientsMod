using Terraria;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using RemnantOfTheAncientsMod.Content.Projectiles.Summon.Minioms.Saplings;

namespace  RemnantOfTheAncientsMod.Content.Buffs.Buffs.Minions.acorns
{
	public class TungstenAcornBuff : ModBuff
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Animated Tungsten Sapling");
			Description.SetDefault("The Animated Tungsten Sapling will fight for you");
			Main.buffNoSave[Type] = true;
			Main.buffNoTimeDisplay[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			if (player.ownedProjectileCounts[ProjectileType<SaplingTungstenMinion>()] > 0)
			{
				player.buffTime[buffIndex] = 18000;
			}
			else
			{
				player.DelBuff(buffIndex);
				buffIndex--;
			}
		}
	}
}