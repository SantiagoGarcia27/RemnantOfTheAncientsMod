using CalamityMod.Prefixes;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace RemnantOfTheAncientsMod.Prefixe
{
    [JITWhenModsEnabled("CalamityMod")]
    [ExtendsFromMod("CalamityMod")]
    public class Shadow : RogueAccessoryPrefix
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return ModLoader.TryGetMod("CalamityMod", out mod);
        }
        public override string Name => "Shadow";
        //public override void SetStaticDefaults()
        //{
        //    DisplayName.SetDefault("Shadow");
        //    DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "(Ombre)");
        //    DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "(Sombrio)");
        //}
        //public override float RollChance(Item item)
        //{
        //    return new ChanceRoll().HighTierReforgeChance;//5
        //}


        public Shadow() :
            base(1.20f)
        {

        }
        // public override void Apply(Item item)
        //{
        //    if (item.rare <= RemnantOfTheAncientsMod.MaxRarity - 3) item.rare += 1;
        //}
    }
}