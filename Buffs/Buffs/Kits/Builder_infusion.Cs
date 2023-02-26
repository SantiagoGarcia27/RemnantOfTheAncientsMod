using Terraria;
using Terraria.ModLoader;
using Terraria.Localization;

namespace RemnantOfTheAncientsMod.Buffs.Buffs.Kits
{
    public class Builder_infusion : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Builder infusion");
            Description.SetDefault("");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Infusion de combat");
            Description.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Infusión de combate");
            Description.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "");

            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = false; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.calmed = true;
            player.tileSpeed += 0.25f;
            player.wallSpeed += 0.25f;
            player.blockRange += 1;

            player.buffImmune[1] = true;
            player.buffImmune[106] = true;
            player.buffImmune[107] = true;
        }
    }
}
  