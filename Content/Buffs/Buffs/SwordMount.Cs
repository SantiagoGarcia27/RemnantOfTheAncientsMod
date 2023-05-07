using Terraria;
using Terraria.ModLoader;
using Terraria.Localization;

namespace RemnantOfTheAncientsMod.Content.Buffs.Buffs
{
	public class SwordMount : ModBuff
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Travel sword");
			DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Épée de voyage");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Espada de transporte");
            Description.SetDefault("Who said that melee could not be buff more?");
			Description.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Qui a dit que le corps à corps ne pouvait pas être plus chamois ?");
            Description.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "¿Quien dijo que melee no se podría mejorar mas?");
            Main.buffNoTimeDisplay[Type] = true;
			Main.buffNoSave[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			player.mount.SetMount(ModContent.MountType<Mounts.Sword>(), player);
			player.buffTime[buffIndex] = 10;
		}
	}
}
