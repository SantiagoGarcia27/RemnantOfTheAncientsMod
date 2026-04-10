using RemnantOfTheAncientsMod.Prefixe;
using System.Collections.Generic;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Prefixe
{
    public class Healthy : ModPrefix
    {
     
        public virtual float Power => 1f;

        public override PrefixCategory Category => PrefixCategory.Accessory;
        public override float RollChance(Item item) => new ChanceRoll().CommonReforgeChance;
        public override bool CanRoll(Item item) => true;
        public override void Apply(Item item)
        {
            if (item.rare <= RemnantOfTheAncientsMod.MaxRarity) item.rare -= 1;
        }
        public override void ModifyValue(ref float valueMult)
        {
            valueMult *= 1f + 0.10f * Power;
        }
    }
    public class Healer : Healthy
    {
        public override float Power => base.Power * 2f;
        public override float RollChance(Item item) => new ChanceRoll().CommonReforgeChance;
        public override bool CanRoll(Item item) => true;
    }
    public class Atletic : Healthy
    {
        public override float Power => base.Power * 3f;
        public override float RollChance(Item item) => new ChanceRoll().CommonReforgeChance;
        public override bool CanRoll(Item item) => true;
    }
    public class Gigant : Healthy
    {
        public override float Power => base.Power * 4f;
        public override float RollChance(Item item) => new ChanceRoll().CommonReforgeChance;
        public override bool CanRoll(Item item) => true;
    }
    public class Titanic : ModPrefix
    {
        public  float Power =>  5f;
        public override PrefixCategory Category => PrefixCategory.Accessory;
        public override float RollChance(Item item) => new ChanceRoll().CommonReforgeChance;
        public override bool CanRoll(Item item) => true;

        public override void ModifyValue(ref float valueMult)
        {
            valueMult *= 1f + 0.10f * Power;
        }
        public override void Apply(Item item)
        {
            if (item.rare <= RemnantOfTheAncientsMod.MaxRarity - 3) item.rare += 1;
        }
    }
}

