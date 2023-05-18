using Terraria;
using Terraria.ModLoader;
using Terraria.Localization;
using static Terraria.ModLoader.ModContent;

namespace RemnantOfTheAncientsMod.Content.Buffs.Buffs.Scrolls
{
    public class Bee : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Queen Bee");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "reine des abeilles");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "abeja reina");
            Description.SetDefault("The bees give you their power");
            Description.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Les abeilles vous donnent leur pouvoir");
            Description.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Las abejas te dan su poder");
            Main.persistentBuff[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
            Main.debuff[Type] = false; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {
            
            player.strongBees = true;
		    player.honey = true;
            //player.Bee = true;
            player.statDefense = 5;
            player.GetArmorPenetration(DamageClass.Generic) = 15;
            int Buff = BuffType<Bee>();
            player.GetModPlayer<RemnantPlayer>().ScrollInmunity(Buff);
        }
    }
}
  