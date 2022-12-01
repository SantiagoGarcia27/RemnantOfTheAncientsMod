using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using static Terraria.ModLoader.ModContent;
using RemnantOfTheAncientsMod.Buffs.Debuff;

namespace RemnantOfTheAncientsMod.Buffs.Scrolls
{
    public class Infernal : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Infernal");
            Description.SetDefault("");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Infernal");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Infernal");

            Main.buffNoTimeDisplay[Type] = true;
            Main.debuff[Type] = false;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.inferno = true;
            player.lifeForce = true;
            player.GetDamage(DamageClass.Generic) *= 1.10f;
            player.buffImmune[BuffType<Bee>()] = true;
            player.buffImmune[BuffType<AoD>()] = true;
            player.buffImmune[BuffType<Eye>()] = true;
            player.buffImmune[BuffType<MasterD>()] = true;
            player.buffImmune[BuffType<Putrid>()] = true;
            player.buffImmune[BuffType<Skeleton>()] = true;
            player.buffImmune[BuffType<Slim>()] = true;
        }
    }
}
  