using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using static Terraria.ModLoader.ModContent;

namespace opswordsII.Buffs
{
    public class MasterD : ModBuff
    {
        public override void SetStaticDefaults()
        {
              DisplayName.SetDefault("Master of the Desert");
              Description.SetDefault("");
             DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Maître du désert");
              DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Maestro del desierto");
             
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = false; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {
             player.maxMinions+= 2;
            player.moveSpeed += 0.05f;

            player.buffImmune[BuffType<Buffs.Bee>()] = true;
            player.buffImmune[BuffType<Buffs.AoD>()] = true;
            player.buffImmune[BuffType<Buffs.Infernal>()] = true;
            player.buffImmune[BuffType<Buffs.Eye>()] = true;
            player.buffImmune[BuffType<Buffs.Putrid>()] = true;
            player.buffImmune[BuffType<Buffs.Skeleton>()] = true;
            player.buffImmune[BuffType<Buffs.Slim>()] = true;
        }
    }
}
  