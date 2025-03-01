using RemnantOfTheAncientsMod.Content.Projectiles.Summon.Minioms.SunFlower;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace  RemnantOfTheAncientsMod.Content.Buffs.Buffs.Minions
{
	public class IlluminatedSunflowerMinionBuff : ModBuff
	{
		public override void SetStaticDefaults()
		{
            Main.buffNoSave[Type] = true;
			Main.buffNoTimeDisplay[Type] = true;
            Main.persistentBuff[Type] = true;
        }
		public override void Update(Player player, ref int buffIndex)
		{
			if (player.ownedProjectileCounts[ProjectileType<IlluminatedSunFlowerMinion>()] > 0)
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
    public class IlluminatedSunflowerBuff : ModBuff
    {
        public override string Texture => getTextura();

        private string getTextura()
        {
            string name = nameof(IlluminatedSunflowerMinionBuff);
            return "RemnantOfTheAncientsMod/Content/Buffs/Buffs/Minions/" + name;
        }


        int defensBonus = 5;

        public override void SetStaticDefaults()
        {
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
            Main.persistentBuff[Type] = true;
        }
        public override void Update(Player player, ref int buffIndex)
        {
           player.statDefense += defensBonus;
        }
    }
}