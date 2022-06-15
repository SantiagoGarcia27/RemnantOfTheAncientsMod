using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using static Terraria.ModLoader.ModContent;

namespace opswordsII.Buffs
{
    public class AoD : ModBuff
    {
        public override void SetStaticDefaults()
        {
              DisplayName.SetDefault("Dodge Apprentice");
              Description.SetDefault("");
             DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Apprenti Dodge");
              DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Aprendiz del esquiva");
            
             
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = false; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.shadowDodge = true;
           
           
            player.buffImmune[BuffType<Bee>()] = true;
            player.buffImmune[BuffType<Eye>()] = true;
            player.buffImmune[BuffType<Infernal>()] = true;
            player.buffImmune[BuffType<MasterD>()] = true;
            player.buffImmune[BuffType<Putrid>()] = true;
            player.buffImmune[BuffType<Skeleton>()] = true;
            player.buffImmune[BuffType<Slim>()] = true;
        }
    }
}
  