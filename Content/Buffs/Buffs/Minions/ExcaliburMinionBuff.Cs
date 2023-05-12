using RemnantOfTheAncientsMod.Content.Projectiles;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace RemnantOfTheAncientsMod.Content.Buffs.Buffs.Minions
{
	public class ExcaliburMinionBuff: ModBuff
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Excalibur");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Excalibur");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Excalibur");
            Description.SetDefault("The excalibur will fight for you");
            Description.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "La excalibur se battra pour vous");
            Description.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "La excalibur luchará por ti");
            Main.buffNoSave[Type] = true;
			Main.buffNoTimeDisplay[Type] = true;
		}
		public override void Update(Player player, ref int buffIndex)
		{
			if (player.ownedProjectileCounts[ProjectileType<ExcaliburMinion>()] > 0)
			{
				///player.buffTime[buffIndex] = 18000;
			}
			else
			{
				player.DelBuff(buffIndex);
				buffIndex--;
			}
		}
	}
}