using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using static Terraria.ModLoader.ModContent;
using System;

namespace RemnantOfTheAncientsMod.Buffs.Scrolls
{ 
    public class Skeleton : ModBuff
    {
        public override void SetStaticDefaults()
        {
            //DisplayName.SetDefault("Undead");
            //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Mort-vivant");
            //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Muertos viviente");
            //Description.SetDefault("Your senses are heightened");
            //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Tes sens sont exacerbés");
            //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Tus sentidos se agudizan");
            Main.persistentBuff[Type] = true;
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
            Main.persistentBuff[Type] = true;
            player.ammoCost75 = true;
            player.GetDamage(DamageClass.Throwing) *= 1.20f;

            player.GetModPlayer<RemnantPlayer>().UndeadInmunity();
            int Buff = BuffType<Skeleton>();
            player.GetModPlayer<RemnantPlayer>().ScrollInmunity(Buff);
        }
    }
}
  