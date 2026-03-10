using Terraria;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Prefixe
{
    public class BerserkProjectile : ModPrefix
    {
        public virtual float Power => 1f;

        public override PrefixCategory Category => PrefixCategory.AnyWeapon;

        public override float RollChance(Item item)
        {
            return 0;
        }

        public override bool CanRoll(Item item)
        {
            return item.CountsAsClass<MeleeDamageClass>() && item.noMelee;
        }

        public override void SetStats(ref float damageMult, ref float knockbackMult, ref float useTimeMult, ref float scaleMult, ref float shootSpeedMult, ref float manaMult, ref int critBonus)
        {
            damageMult *= 1f + 0.20f * Power;
            useTimeMult *= 0.70f;
            critBonus += 10;
            knockbackMult *= 1.20f;
            shootSpeedMult *= 1.20f;
        }

        public override void ModifyValue(ref float valueMult)
        {
            valueMult *= 1f + 0.10f * Power;
        }
    }
}
