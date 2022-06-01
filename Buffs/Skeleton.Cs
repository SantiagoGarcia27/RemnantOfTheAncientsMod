using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using static Terraria.ModLoader.ModContent;

namespace opswordsII.Buffs
{
    public class Skeleton : ModBuff
    {
        public override void SetStaticDefaults()
        {
              DisplayName.SetDefault("Undead");
              Description.SetDefault("");
             DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Mort-vivant");
              DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Muertos viviente");
             
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = false; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {
             player.sonarPotion = true;
             player.dangerSense = true;
             player.findTreasure = true;
             player.nightVision = true;
             player.detectCreature = true;

            player.ammoCost75 = true;
            player.GetDamage(DamageClass.Throwing) *= 1.20f;

            player.buffImmune[BuffID.Burning] = true;
			player.buffImmune[BuffID.BrokenArmor] = true;
            player.buffImmune[BuffID.Bleeding] = true;
            player.buffImmune[BuffID.Silenced] = true;
            player.buffImmune[BuffID.Darkness] = true;
            player.buffImmune[BuffID.Blackout] = true;
            player.buffImmune[BuffID.Cursed] = true;
            player.buffImmune[BuffID.Confused] = true;
            player.buffImmune[BuffID.Slow] = true;
            player.buffImmune[BuffID.Weak] = true;
            player.buffImmune[BuffID.Horrified] = true;
            player.buffImmune[BuffID.Burning] = true;
            player.buffImmune[BuffID.Suffocation] = true;
            player.buffImmune[BuffID.ChaosState] = true;
            player.buffImmune[BuffID.TheTongue] = true;
             player.buffImmune[BuffID.CursedInferno] = true;
            player.buffImmune[BuffID.Ichor] = true;

            player.buffImmune[BuffType<Buffs.Bee>()] = true;
            player.buffImmune[BuffType<Buffs.AoD>()] = true;
            player.buffImmune[BuffType<Buffs.Infernal>()] = true;
            player.buffImmune[BuffType<Buffs.MasterD>()] = true;
            player.buffImmune[BuffType<Buffs.Putrid>()] = true;
            player.buffImmune[BuffType<Buffs.Eye>()] = true;
            player.buffImmune[BuffType<Buffs.Slim>()] = true;
        }
    }
}
  