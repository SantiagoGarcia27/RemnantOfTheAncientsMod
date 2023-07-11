using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Prefixe
{
    public class Supersonic : ModPrefix
    {
        // We declare a custom *virtual* property here, so that another type, ExampleDerivedPrefix, could override it and change the effective power for itself.
        public virtual float Power => 1f;

        // Change your category this way, defaults to PrefixCategory.Custom. Affects which items can get this prefix.
        public override PrefixCategory Category => PrefixCategory.Accessory;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Supersonic");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "(Supersonique)");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "(Supersónico)");
        }


        // See documentation for vanilla weights and more information.
        // In case of multiple
        //
        //
        //
        // es with similar functions this can be used with a switch/case to provide different chances for different prefixes
        // Note: a weight of 0f might still be rolled. See CanRoll to exclude prefixes.
        // Note: if you use PrefixCategory.Custom, actually use ModItem.ChoosePrefix instead.
        public override float RollChance(Item item)
        {
            return new ChanceRoll().HighTierReforgeChance;//5
        }

        // Determines if it can roll at all.
        // Use this to control if a prefix can be rolled or not.
        public override bool CanRoll(Item item)
        {
            return true;
        }

        public override void ModifyValue(ref float valueMult)
        {
            valueMult *= 1f + 0.50f;
        }

        // This is used to modify most other stats of items which have this modifier.
        public override void Apply(Item item)
        {
            if (item.rare <= RemnantOfTheAncientsMod.MaxRarity - 3) item.rare += 1;
        }
    }
}