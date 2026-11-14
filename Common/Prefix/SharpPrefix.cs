using Terraria;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Common.Prefix
{
    public class Sharp : ModPrefix
    {
        public virtual float Power => 1f;
        public override PrefixCategory Category => PrefixCategory.Accessory;
        public override float RollChance(Item item) => 0;  
        public override bool CanRoll(Item item) => true;
        public override void ModifyValue(ref float valueMult)
        {
            valueMult *= 1f + 0.50f;
        }
        public override void Apply(Item item)
        {
            if (item.rare <= RemnantOfTheAncientsMod.MaxRarity - 3) item.rare += 1;
        }
    }
}