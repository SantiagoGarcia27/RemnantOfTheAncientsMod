using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Content.Buffs.Buffs
{
	public class CentryBuff : ModBuff
	{
		public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("Comander Potion");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Potion de commandant");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Poción de comandante");
            Description.SetDefault("Increased max number of sentries");
            Description.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Augmentation du nombre maximum de sentinelles");
            Description.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Aumenta el número máximo de centinelas");
			Main.buffNoSave[Type] = false;
			Main.buffNoTimeDisplay[Type] = false;
			Main.persistentBuff[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			player.maxTurrets += 1;
		}
	}
}