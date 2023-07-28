using Terraria;
using Terraria.ModLoader;
using Terraria.Localization;
using static Terraria.ModLoader.ModContent;

namespace RemnantOfTheAncientsMod.Content.Buffs.Buffs.Scrolls
{
    public class Infernal : ModBuff
    {
        public override void SetStaticDefaults()
        {
           // //DisplayName.SetDefault("Infernal");
           // //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Infernal");
           // //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Infernal");
            ////Description.SetDefault("The flames of hell cover you");
            //Description.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Les flammes de l'enfer te couvrent");
            //Description.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Las llamas del infierno te recubren");
            Main.persistentBuff[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
            Main.debuff[Type] = false;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.inferno = true;
            player.lifeForce = true;
            player.GetDamage(DamageClass.Generic) *= 1.10f;
            int Buff = BuffType<Infernal>();
            player.GetModPlayer<RemnantPlayer>().ScrollInmunity(Buff);
        }
    }
}
  