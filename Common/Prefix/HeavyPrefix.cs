using Terraria;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Prefixe
{
    public class Heavy : ModPrefix
    {
        public override PrefixCategory Category => PrefixCategory.AnyWeapon;

        public override float RollChance(Item item)
        {
            return 2f;
        }

        public override bool CanRoll(Item item)
        {
            return true;
        }

        public override void SetStats(ref float damageMult, ref float knockbackMult, ref float useTimeMult, ref float scaleMult, ref float shootSpeedMult, ref float manaMult, ref int critBonus)
        {
            damageMult *= 1.25f;
            useTimeMult *= 1.50f;
        }

        public override void Apply(Item item)
        {
            if (item.CountsAsClass<SummonDamageClass>())
            {
                item.shootSpeed *= 0.50f;
            }
        }

        public override void ModifyValue(ref float valueMult)
        {
            valueMult *= 1f;
        }
    }
}
