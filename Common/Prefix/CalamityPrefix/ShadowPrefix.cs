using CalamityMod.Prefixes;
using Terraria;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Prefix.CalamityPrefix
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
        public Shadow() : base(1.10f)
        {

        }
        public override float RollChance(Item item)
        {
            return 0;
        }
    }
}