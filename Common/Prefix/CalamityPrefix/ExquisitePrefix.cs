using CalamityMod.Prefixes;
using Terraria;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Prefix.CalamityPrefix
{
    [JITWhenModsEnabled("CalamityMod")]
    [ExtendsFromMod("CalamityMod")]


    [LegacyName(["ExquisitePrefix"])]

    public class Exquisite : RogueWeaponPrefix
    {
        public override string Name => "Exquisite";
        public override float damageMult => 1.20f;

        public override float useTimeMult => 0.8f;

        public override int critBonus => 10;

        public override float shootSpeedMult => 1.2f;

        public override float stealthDmgMult => 1.20f; 
        public override float RollChance(Item item)
        {
            return 0;
        }
    }
}