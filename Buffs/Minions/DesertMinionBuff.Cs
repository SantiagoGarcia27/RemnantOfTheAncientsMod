using RemnantOfTheAncientsMod.Projectiles.Minioms;
using Terraria;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace RemnantOfTheAncientsMod.Buffs.Minions
{
	public class DesertMinionBuff : ModBuff
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Baby Desert Aniquilator");
			Description.SetDefault("The Baby Desert will fight for you");
			Main.buffNoSave[Type] = true;
			Main.buffNoTimeDisplay[Type] = true;

		}

		public override void Update(Player player, ref int buffIndex)
		{
			RemnantPlayer Modplayer = player.RemnantOfTheAncientsMod();
			if (player.ownedProjectileCounts[ProjectileType<DesertMinion>()] > 0)
			{
				Modplayer.DesertMinion = true;
			}
			if(!Modplayer.DesertMinion)
			{
                player.DelBuff(buffIndex);
                buffIndex--;
            }
			else
			{
                player.buffTime[buffIndex] = 18000;
            }
		}
	}
}