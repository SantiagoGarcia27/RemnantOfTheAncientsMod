using RemnantOfTheAncientsMod.Content.Projectiles.Summon.Minioms.Saplings;
#pragma warning disable CS0105 // The using directive for 'RemnantOfTheAncientsMod.Content.Projectiles.Summon.Minioms.Saplings' appeared previously in this namespace
using RemnantOfTheAncientsMod.Content.Projectiles.Summon.Minioms.Saplings;
#pragma warning restore CS0105 // The using directive for 'RemnantOfTheAncientsMod.Content.Projectiles.Summon.Minioms.Saplings' appeared previously in this namespace
using Terraria;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace  RemnantOfTheAncientsMod.Content.Buffs.Buffs.Minions.acorns
{
	public class SaplingMinionBuff : ModBuff
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Animated Sapling");
			Description.SetDefault("The Animated Sapling will fight for you");
			Main.buffNoSave[Type] = true;
			Main.buffNoTimeDisplay[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			if (player.ownedProjectileCounts[ProjectileType<SaplingMinion>()] > 0)
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