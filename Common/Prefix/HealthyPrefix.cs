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

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Healthy");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "(Healthy)");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "(Saludable)");
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
    public class Healer : Healthy
    {
        public override float Power => base.Power * 2f;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Healer");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "(Guérisseur)");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "(Sanador)");
        }
        public override float RollChance(Item item)
        {
            return new ChanceRoll().CommonReforgeChance;
        }
    }
    public class Atletic : Healthy
    {
        public override float Power => base.Power * 3f;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Athletic");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "(Athlétique)");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "(Atletico)");
        }
        public override float RollChance(Item item)
        {
            return new ChanceRoll().RareReforgeChance;
        }
        public override void Apply(Item item)
        {
            if (item.rare <= RemnantOfTheAncientsMod.MaxRarity) item.rare += 0;
        }
    }
    public class Gigant : Healthy
    {
        public override float Power => base.Power * 4f;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Giant");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "(Géant)");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "(Gigante)");
        }
        public override float RollChance(Item item)
        {
            return new ChanceRoll().EpicReforgeChance;
        }
        public override void Apply(Item item)
        {
            if (item.rare <= RemnantOfTheAncientsMod.MaxRarity - 1) item.rare += 0;
        }
    }
    public class Titanic : ModPrefix
    {
        public  float Power =>  5f;
        public override PrefixCategory Category => PrefixCategory.Accessory;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Titanic");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "(Titanesque)");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "(Titánico)");
        }
        public override float RollChance(Item item)
        {
            return new ChanceRoll().HighTierReforgeChance;
        }
       

        // Determines if it can roll at all.
        // Use this to control if a prefix can be rolled or not.
        public override bool CanRoll(Item item)
        {
            return true;
        }

        // Modify the cost of items with this modifier with this function.
        public override void ModifyValue(ref float valueMult)
        {
            valueMult *= 1f + 0.10f * Power;
        }

        // This is used to modify most other stats of items which have this modifier.
        public override void Apply(Item item)
        {
            if (item.rare <= RemnantOfTheAncientsMod.MaxRarity - 3) item.rare += 1;
        }
    }
}

