using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using static Terraria.ModLoader.ModContent;

namespace RemnantOfTheAncientsMod.Buffs
{
    public class Skeleton : ModBuff
    {
        public override void SetStaticDefaults()
        {
              DisplayName.SetDefault("Undead");
              Description.SetDefault("");
             DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Mort-vivant");
              DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Muertos viviente");
             
            Main.buffNoTimeDisplay[Type] = true;
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

            player.GetModPlayer<Player1>().UndeadInmunity();
            player.GetModPlayer<Player1>().ScrollInmunity(BuffType<Skeleton>());
        }
    }
}
  