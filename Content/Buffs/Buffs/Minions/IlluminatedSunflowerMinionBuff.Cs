using RemnantOfTheAncientsMod.Content.Projectiles.Summon.Minioms;
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
			DisplayName.SetDefault("Illuminated Sunflower");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Tournesol illuminé");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Girasol iluminado");
            Description.SetDefault("The sunflower will protect you");
            Description.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Le tournesol te protégera");
            Description.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "El girasol te protegerá");
            Main.buffNoSave[Type] = true;
			Main.buffNoTimeDisplay[Type] = true;
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
}