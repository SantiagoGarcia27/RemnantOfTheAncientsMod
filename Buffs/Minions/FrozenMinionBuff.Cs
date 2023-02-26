using Terraria;
using Terraria.ModLoader;
using RemnantOfTheAncientsMod.Projectiles.Minioms;
using static Terraria.ModLoader.ModContent;

namespace RemnantOfTheAncientsMod.Buffs.Minions
{
    public class FrozenMinionBuff : ModBuff
	    {

		public override void SetStaticDefaults() {
			DisplayName.SetDefault("Baby Frozen Assaulter");
			Description.SetDefault("The Baby Frozen will fight for you");
			Main.buffNoSave[Type] = true;
			Main.buffNoTimeDisplay[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex) {
			if (player.ownedProjectileCounts[ProjectileType<FrozenMinion>()] > 0) {
				player.buffTime[buffIndex] = 18000;
			}
			else {
				player.DelBuff(buffIndex);
				buffIndex--;
			}
		}
	}
}