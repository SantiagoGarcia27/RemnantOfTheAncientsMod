using RemnantOfTheAncientsMod.Content.Projectiles.Summon.Minioms.SunFlower;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace RemnantOfTheAncientsMod.Content.Buffs.Buffs.Minions
{
	public class YggdrasilMinionBuff : ModBuff
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Yggdrasil");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Yggdrasil");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Yggdrasil");
            Description.SetDefault("The Yggdrasil will protect you");
            Description.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Yggdrasil te protégera");
            Description.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Yggdrasil te protegerá");
            Main.buffNoSave[Type] = true;
			Main.buffNoTimeDisplay[Type] = true;
            Main.persistentBuff[Type] = true;
        }
		public override void Update(Player player, ref int buffIndex)
		{
			if (player.ownedProjectileCounts[ProjectileType<YggdrasilMinion>()] > 0)
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