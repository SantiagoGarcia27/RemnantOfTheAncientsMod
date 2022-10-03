using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using static Terraria.ModLoader.ModContent;

namespace RemnantOfTheAncientsMod.Buffs
{
    public class MasterD : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Master of the Desert");
            Description.SetDefault("");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Maître du désert");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Maestro del desierto");

            Main.buffNoTimeDisplay[Type] = true;
            Main.debuff[Type] = false; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.maxMinions += 2;
            player.moveSpeed += 0.05f;
            player.GetModPlayer<Player1>().ScrollInmunity(BuffType<MasterD>());
        }
    }
}
  