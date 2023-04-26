using Terraria;
using Terraria.ModLoader;
using Terraria.Localization;
using static Terraria.ModLoader.ModContent;
using System;

namespace RemnantOfTheAncientsMod.Buffs.Scrolls
{
    public class Slim : ModBuff
    {
        public override void SetStaticDefaults()
        {
            //DisplayName.SetDefault("King's blessing");
            //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Bénédiction du ro");
            //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Bendición del rey");
            //Description.SetDefault("You feel lighter");
            //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Tu te sens plus léger");
            //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Te sientes más ligero");
            Main.persistentBuff[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
            Main.debuff[Type] = false; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.GetDamage(DamageClass.Summon) += 0.05f;
            player.moveSpeed += 0.50f;
            int Buff = BuffType<Slim>();
            player.GetModPlayer<RemnantPlayer>().ScrollInmunity(Buff);
        }
    }
}
  