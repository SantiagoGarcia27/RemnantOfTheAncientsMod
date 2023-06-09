using Terraria;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria.Localization;
using RemnantOfTheAncientsMod.Content.Projectiles.Summon.Minioms;

namespace RemnantOfTheAncientsMod.Content.Buffs.Buffs.Minions
{
	public class IronBallistaBuff : ModBuff
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Iron Ballista");
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
			if (player.ownedProjectileCounts[ProjectileType<IronBallistMinion>()] > 0)
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