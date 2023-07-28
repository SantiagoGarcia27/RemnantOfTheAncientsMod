using Terraria;
using Terraria.ModLoader;
using Terraria.Localization;
using static Terraria.ModLoader.ModContent;

namespace RemnantOfTheAncientsMod.Content.Buffs.Buffs.Scrolls
{
    public class MasterD : ModBuff
    {
        public override void SetStaticDefaults()
        {
           // //DisplayName.SetDefault("Master of the Desert");
           // //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Maître du désert");
           // //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Maestro del desierto");
            ////Description.SetDefault("You feel with leadership");
            //Description.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Vous vous sentez avec le leadership");
            //Description.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Te sientes con liderazgo");
            Main.persistentBuff[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
            Main.debuff[Type] = false; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.maxMinions += 1;
            player.GetDamage(DamageClass.Summon) += 0.05f;
            player.moveSpeed += 0.50f;
            int Buff = BuffType<MasterD>();
            player.GetModPlayer<RemnantPlayer>().ScrollInmunity(Buff);
        }
    }
}
  