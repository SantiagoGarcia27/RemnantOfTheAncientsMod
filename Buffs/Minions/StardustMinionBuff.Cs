using RemnantOfTheAncientsMod.Projectiles.Summon.Minioms;
using Terraria;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;


namespace RemnantOfTheAncientsMod.Buffs.Minions
{
	public class StardustMinionBuff : ModBuff
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("StardustMinionBuff");
			Description.SetDefault("The Stard dust will fight for you");
			Main.buffNoSave[Type] = true;
			Main.buffNoTimeDisplay[Type] = true;
		}
		public override void Update(Player player, ref int buffIndex)
		{
			if (player.ownedProjectileCounts[ProjectileType<StardustMinion>()] > 0)
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