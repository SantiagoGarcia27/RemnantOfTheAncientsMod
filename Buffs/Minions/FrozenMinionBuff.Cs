using Terraria;
using Terraria.ModLoader;
using RemnantOfTheAncientsMod.Projectiles.Summon.Minioms;
using static Terraria.ModLoader.ModContent;
using RemnantOfTheAncientsMod.Projectiles.Summon.Summon.Minioms;
using Terraria.Localization;

namespace RemnantOfTheAncientsMod.Buffs.Minions
{
	public class FrozenMinionBuff : ModBuff
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Baby Frozen Assaulter");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Agresseur de bébé congelé");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Asaltante Congelado miniatura");
            Description.SetDefault("The Baby Frozen will fight for you");
            Description.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "La Agresseur de bébé congelé se battra pour vous");
            Description.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "El Asaltante Congelado miniatura luchará por ti");
            Main.buffNoSave[Type] = true;
			Main.buffNoTimeDisplay[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			if (player.ownedProjectileCounts[ProjectileType<FrozenMinion>()] > 0)
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