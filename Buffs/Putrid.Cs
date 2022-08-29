using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using static Terraria.ModLoader.ModContent;

namespace RemnantOfTheAncientsMod.Buffs
{
    public class Putrid : ModBuff
    {
        public override void SetStaticDefaults()
        {
              DisplayName.SetDefault("Putrid");
              Description.SetDefault("");
             DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Putride");
              DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Podrido");

             
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = false; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {
           player.endurance += 0.10f;
            player.GetCritChance(DamageClass.Generic) += 5;
            player.GetDamage(DamageClass.Summon) += 0.30f;

            player.buffImmune[BuffType<Bee>()] = true;
            player.buffImmune[BuffType<AoD>()] = true;
            player.buffImmune[BuffType<Infernal>()] = true;
            player.buffImmune[BuffType<MasterD>()] = true;
            player.buffImmune[BuffType<Eye>()] = true;
            player.buffImmune[BuffType<Skeleton>()] = true;
            player.buffImmune[BuffType<Slim>()] = true;
        }
    }
}
  