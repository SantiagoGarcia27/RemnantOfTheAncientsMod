using Terraria;
using Terraria.ModLoader;
using Terraria.Localization;
using static Terraria.ModLoader.ModContent;
using System;

namespace RemnantOfTheAncientsMod.Buffs.Scrolls
{
    public class QueenSlimeScrollBuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Queen's blessing");
            Description.SetDefault("");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Bénédiction de la reine");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Bendición de la reina");
            Main.persistentBuff[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
            Main.debuff[Type] = false; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.statLifeMax2 += (player.statLifeMax * 50) / 100;
            player.GetDamage(DamageClass.Summon) += 0.10f;
            player.moveSpeed += 0.50f;
            player.GetModPlayer<RemnantPlayer>().ScrollInmunity(BuffType<QueenSlimeScrollBuff>());
        }
    }
}
  