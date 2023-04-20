using Terraria;
using Terraria.ModLoader;
using Terraria.Localization;
using static Terraria.ModLoader.ModContent;

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
            Description.SetDefault("You're breaking down");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "You're breaking down");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Te estás descomponiendo");

            Main.persistentBuff[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
            Main.debuff[Type] = false; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.endurance += 0.10f;
            player.GetCritChance(DamageClass.Generic) += 5;
            player.GetDamage(DamageClass.Summon) += 0.30f;
            int Buff = BuffType<Putrid>();
            player.GetModPlayer<RemnantPlayer>().ScrollInmunity(Buff);
        }
    }
}
  