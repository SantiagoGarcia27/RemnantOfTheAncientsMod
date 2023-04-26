using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Prefixe
{
    public class Regenerative : ModPrefix
    {
        public virtual float Power => 1f;
        public override PrefixCategory Category => PrefixCategory.Accessory;

        public override void SetStaticDefaults()
        {
            //DisplayName.SetDefault("Regenerative");
            //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "(Régénérateur)");
            //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "(Regenerativo)");
        }

        public override float RollChance(Item item)
        {
            return new ChanceRoll().CommonReforgeChance;
        }

        // Determines if it can roll at all.
        // Use this to control if a prefix can be rolled or not.
        public override bool CanRoll(Item item)
        {
            return true;
        }
        public override void Apply(Item item)
        {
            if (item.rare <= RemnantOfTheAncientsMod.MaxRarity) item.rare -= 1;
        }
        // Modify the cost of items with this modifier with this function.
        public override void ModifyValue(ref float valueMult)
        {
            valueMult *= 1f + 0.10f * Power;
        }
    }

}