using RemnantOfTheAncientsMod.Content.Projectiles.Summon.Minioms.Saplings;
using Terraria;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace  RemnantOfTheAncientsMod.Content.Buffs.Buffs.Minions.acorns
{
	public class GoldenAcornBuff : ModBuff
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Animated Golden Sapling");
			Description.SetDefault("The Animated Golden Sapling will fight for you");
			Main.buffNoSave[Type] = true;
			Main.buffNoTimeDisplay[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			if (player.ownedProjectileCounts[ProjectileType<SaplingGoldMinion>()] > 0)
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