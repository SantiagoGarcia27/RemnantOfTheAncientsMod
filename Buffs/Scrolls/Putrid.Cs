using Terraria;
using Terraria.ModLoader;
using Terraria.Localization;
using static Terraria.ModLoader.ModContent;
using System;

namespace RemnantOfTheAncientsMod.Buffs.Scrolls
{
    public class Putrid : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Putrid");
            Description.SetDefault("");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Putride");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Podrido");

            Main.persistentBuff[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
            Main.debuff[Type] = false; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.endurance += 0.10f;
            player.GetCritChance(DamageClass.Generic) += 5;
            player.GetDamage(DamageClass.Summon) += 0.30f;
            player.GetModPlayer<Player1>().ScrollInmunity(BuffType<Putrid>());
        }
    }
}
  