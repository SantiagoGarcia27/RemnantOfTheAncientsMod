using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using static Terraria.ModLoader.ModContent;

namespace RemnantOfTheAncientsMod.Buffs
{
    public class Slim : ModBuff
    {
        public override void SetStaticDefaults()
        {
              DisplayName.SetDefault("King's blessing");
              Description.SetDefault("");
             DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Bénédiction du ro");
              DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Bendición del rey");
             
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = false; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {
             player.GetDamage(DamageClass.Summon) += 0.05f;
            player.moveSpeed += 0.50f;

            player.buffImmune[BuffType<Buffs.Bee>()] = true;
            player.buffImmune[BuffType<Buffs.AoD>()] = true;
            player.buffImmune[BuffType<Buffs.Infernal>()] = true;
            player.buffImmune[BuffType<Buffs.MasterD>()] = true;
            player.buffImmune[BuffType<Buffs.Putrid>()] = true;
            player.buffImmune[BuffType<Buffs.Skeleton>()] = true;
            player.buffImmune[BuffType<Buffs.Eye>()] = true;
        }
    }
}
  