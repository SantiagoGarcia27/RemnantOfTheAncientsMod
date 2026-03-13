using RemnantOfTheAncientsMod.Common.UtilsTweaks;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Prefixe
{
    public class Cursed : ModPrefix
    {
        public virtual float Power => 1f;
        public override PrefixCategory Category => PrefixCategory.Accessory;

        public override float RollChance(Item item) => 0;
        
        public override bool CanRoll(Item item) =>true;       
        
        public override void Apply(Item item)
        {
            if (item.rare >= ItemRarityID.White) item.rare -= 1;
        }
        // Modify the cost of items with this modifier with this function.
        public override void ModifyValue(ref float valueMult)
        {
            valueMult *= 1f + 0.10f * Power;
        }
        public override IEnumerable<TooltipLine> GetTooltipLines(Item item)
        {
            return LocalizationHelper.GetAccesoryPrefixDescription(Mod, damage: 5, defense: -3);
        }
    }

}