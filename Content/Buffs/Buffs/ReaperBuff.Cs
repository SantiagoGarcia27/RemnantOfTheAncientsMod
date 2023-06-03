using RemnantOfTheAncientsMod.World;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Content.Buffs.Buffs
{
	public class ReaperBuff : ModBuff
	{

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Reaper mode");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French),"Mode faucheuse");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Modo segador");
            Description.SetDefault("You feel hollow");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Tu te sens creux");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Te sientes hueco");
            Main.buffNoSave[Type] = false;
			Main.buffNoTimeDisplay[Type] = true;
			Main.persistentBuff[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			player.GetDamage(DamageClass.Generic) += 0.50f;
			player.statLifeMax2 = 1;
			player.maxMinions++;
			if (Reaper.ReaperMode) player.buffTime[buffIndex] = 1;
			else
			{
				player.DelBuff(buffIndex);
				buffIndex--;
			}
		}
	}
}