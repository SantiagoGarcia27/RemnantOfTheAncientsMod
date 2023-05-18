using Terraria;
using Terraria.ModLoader;
using Terraria.Localization;
using static Terraria.ModLoader.ModContent;

namespace RemnantOfTheAncientsMod.Content.Buffs.Buffs.Scrolls
{
    public class QueenSlimeScrollBuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Queen's blessing");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Bénédiction de la reine");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Bendición de la reina");
            Description.SetDefault("You feel blessed");
            Description.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Tu te sens béni");
            Description.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Te sientes bendecido");
            Main.persistentBuff[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
            Main.debuff[Type] = false; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.statLifeMax2 += (player.statLifeMax * 20) / 100;
            player.GetDamage(DamageClass.Summon) += 0.05f;
            player.moveSpeed += 0.50f;
            player.maxMinions++;
            int Buff = BuffType<QueenSlimeScrollBuff>();
            player.GetModPlayer<RemnantPlayer>().ScrollInmunity(Buff);
        }
    }
}
  