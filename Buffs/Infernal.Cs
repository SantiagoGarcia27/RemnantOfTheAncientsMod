using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using static Terraria.ModLoader.ModContent;

namespace RemnantOfTheAncientsMod.Buffs
{
    public class Infernal : ModBuff
    {
        public override void SetStaticDefaults()
        {
              DisplayName.SetDefault("Infernal");
              Description.SetDefault("");
             DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Infernal");
              DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Infernal");
             
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = false; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {
			 player.inferno = true;
             player.lifeForce = true;
             player.GetDamage(DamageClass.Generic) *= 1.10f;

             player.buffImmune[BuffType<Buffs.Bee>()] = true;
            player.buffImmune[BuffType<Buffs.AoD>()] = true;
            player.buffImmune[BuffType<Buffs.Eye>()] = true;
            player.buffImmune[BuffType<Buffs.MasterD>()] = true;
            player.buffImmune[BuffType<Buffs.Putrid>()] = true;
            player.buffImmune[BuffType<Buffs.Skeleton>()] = true;
            player.buffImmune[BuffType<Buffs.Slim>()] = true;
        }
    }
}
  