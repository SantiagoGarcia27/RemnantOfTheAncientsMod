using CalamityMod.Prefixes;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Prefix.CalamityPrefix
{
    [JITWhenModsEnabled("CalamityMod")]
    [ExtendsFromMod("CalamityMod")]
    public class Exquisite : RogueWeaponPrefix
    {
        public override string Name => "Exquisite";
        public Exquisite() :
            base(1.20f, 0.8f, 10, 1.2f, 1.20f)
        {

        }
    }
}