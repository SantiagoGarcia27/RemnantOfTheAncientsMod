using Terraria;
using Terraria.ModLoader;
using Terraria.Localization;

namespace RemnantOfTheAncientsMod.Content.Buffs.Buffs.Kits
{
    public class Combat_infusion : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Combat infusion");
            Description.SetDefault("Definitive offensive buff");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Infusion de combat");
            Description.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Buff d'attaque définitif");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Infusión de combate");
            Description.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Mejora ofensiva definitiva");

            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = false; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {
            RemnantPlayer.CombatInfusion(player);
        }
    }
}
  