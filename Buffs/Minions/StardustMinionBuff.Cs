using RemnantOfTheAncientsMod.Projectiles.Summon.Minioms;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;


namespace RemnantOfTheAncientsMod.Buffs.Minions
{
	public class StardustMinionBuff : ModBuff
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Stardust");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Poussière d'étoiles");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Polvo de estrellas");
            Description.SetDefault("The stardust will fight for you");
            Description.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "La poussière d'étoiles se battra pour vous");
            Description.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "El polvo de estrellas luchará por ti");
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