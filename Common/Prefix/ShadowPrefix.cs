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
        public Shadow() :
            base(1.20f)
        {

        }
    }
}