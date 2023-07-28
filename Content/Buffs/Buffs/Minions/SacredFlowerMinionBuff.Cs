using RemnantOfTheAncientsMod.Content.Projectiles.Summon.Minioms.SunFlower;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace  RemnantOfTheAncientsMod.Content.Buffs.Buffs.Minions
{
	public class SacredFlowerMinionBuff : ModBuff
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Sacred flower");
           // //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Fleur sacrée");
           // //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Flor sagrada");
            ////Description.SetDefault("The sacred flower will protect you");
            //Description.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Le fleur sacrée te protégera");
            //Description.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "La flor sagrada te protegerá");
            Main.buffNoSave[Type] = true;
			Main.buffNoTimeDisplay[Type] = true;
            Main.persistentBuff[Type] = true;
        }
		public override void Update(Player player, ref int buffIndex)
		{
			if (player.ownedProjectileCounts[ProjectileType<SacredFlowerMinion>()] > 0)
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