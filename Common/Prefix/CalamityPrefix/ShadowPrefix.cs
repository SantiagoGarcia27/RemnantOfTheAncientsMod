using CalamityMod.Prefixes;
using Terraria;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Prefix.CalamityPrefix
{
    [JITWhenModsEnabled("CalamityMod")]
    [ExtendsFromMod("CalamityMod")]

    [LegacyName(["ShadowPrefix"])]
    public class Shadow : RogueAccessoryPrefix
    {
        public override float stealthGenBonus => 0.10f;
        public override bool IsLoadingEnabled(Mod mod)
        {
            return ModLoader.TryGetMod("CalamityMod", out mod);
        }
        public override string Name => "Shadow"; 
        public override float RollChance(Item item)
        {
            return 0;
        }
    }
}