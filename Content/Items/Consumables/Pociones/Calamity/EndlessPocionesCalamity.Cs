using RemnantOfTheAncientsMod.Common.UtilsTweaks;
using RemnantOfTheAncientsMod.Content.Items.Consumables.Pociones.Models;
using SangarUtilities.Common.UtilsTweaks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Content.Items.Consumables.Pociones
{
    public class EndlessCalciumPotion : BasePotions
    {
        public override bool IsLoadingEnabled(Mod mod) => ModLoader.TryGetMod("CalamityMod", out mod);
        public override Item ItemBase => new(CallUtils.GetItemFromMod(RemnantOfTheAncientsMod.CalamityMod, "CalciumPotion"));
        public override char GramaticalCorrection => 'F';
    }

    public class EndlessAnechoicCoating : BasePotions
    {
        public override bool IsLoadingEnabled(Mod mod) => ModLoader.TryGetMod("CalamityMod", out mod);

        public override Item ItemBase => new(CallUtils.GetItemFromMod(RemnantOfTheAncientsMod.CalamityMod, "AnechoicCoating"));
        public override char GramaticalCorrection => 'F';
    }
    public class EndlessBoundingPotion : BasePotions
    {
        public override bool IsLoadingEnabled(Mod mod) => ModLoader.TryGetMod("CalamityMod", out mod);
        public override Item ItemBase => new(CallUtils.GetItemFromMod(RemnantOfTheAncientsMod.CalamityMod, "BoundingPotion"));
        public override char GramaticalCorrection => 'F';
    }
    public class EndlessPotionofOmniscience : BasePotions
    {
        public override bool IsLoadingEnabled(Mod mod) => ModLoader.TryGetMod("CalamityMod", out mod);
        public override Item ItemBase => new(CallUtils.GetItemFromMod(RemnantOfTheAncientsMod.CalamityMod, "PotionofOmniscience"));
        public override char GramaticalCorrection => 'F';
    }
    //Calamity revisar
    /*public class EndlessTeslaPotion : BasePotions
    {
        public override bool IsLoadingEnabled(Mod mod) => ModLoader.TryGetMod("CalamityMod", out mod);
        public override Item ItemBase => new(CallUtils.GetItemFromMod(RemnantOfTheAncientsMod.CalamityMod, "TeslaPotion"));
        public override char GramaticalCorrection => 'F';
    }*/
    public class EndlessZenPotion : BasePotions
    {
        public override bool IsLoadingEnabled(Mod mod) => ModLoader.TryGetMod("CalamityMod", out mod);
        public override Item ItemBase => new(CallUtils.GetItemFromMod(RemnantOfTheAncientsMod.CalamityMod, "ZenPotion"));
        public override char GramaticalCorrection => 'F';
    }
    public class EndlessZergPotion : BasePotions
    {
        public override bool IsLoadingEnabled(Mod mod) => ModLoader.TryGetMod("CalamityMod", out mod);
        public override Item ItemBase => new(CallUtils.GetItemFromMod(RemnantOfTheAncientsMod.CalamityMod, "ZergPotion"));
        public override char GramaticalCorrection => 'F';
    }
    public class EndlessSoaringPotion : BasePotions
    {
        public override bool IsLoadingEnabled(Mod mod) => ModLoader.TryGetMod("CalamityMod", out mod);
        public override Item ItemBase => new(CallUtils.GetItemFromMod(RemnantOfTheAncientsMod.CalamityMod, "SoaringPotion"));
        public override char GramaticalCorrection => 'F';
    }
    public class EndlessPhotosynthesisPotion : BasePotions
    {
        public override bool IsLoadingEnabled(Mod mod) => ModLoader.TryGetMod("CalamityMod", out mod);
        public override Item ItemBase => new(CallUtils.GetItemFromMod(RemnantOfTheAncientsMod.CalamityMod, "PhotosynthesisPotion"));
        public override char GramaticalCorrection => 'F';
    }
    public class EndlessFlaskOfCrumbling : BasePotions
    {
        public override bool IsLoadingEnabled(Mod mod) => ModLoader.TryGetMod("CalamityMod", out mod);
        public override Item ItemBase => new(CallUtils.GetItemFromMod(RemnantOfTheAncientsMod.CalamityMod, "FlaskOfCrumbling"));
        public override char GramaticalCorrection => 'm';
    }
    public class EndlessFlaskOfBrimstone : BasePotions
    {
        public override bool IsLoadingEnabled(Mod mod) => ModLoader.TryGetMod("CalamityMod", out mod);
        public override Item ItemBase => new(CallUtils.GetItemFromMod(RemnantOfTheAncientsMod.CalamityMod, "FlaskOfBrimstone"));

        public override char GramaticalCorrection => 'M';
    }
    public class EndlessFlaskOfHolyFlames : BasePotions
    {
        public override bool IsLoadingEnabled(Mod mod) => ModLoader.TryGetMod("CalamityMod", out mod);
        public override Item ItemBase => new(CallUtils.GetItemFromMod(RemnantOfTheAncientsMod.CalamityMod, "FlaskOfHolyFlames"));

        public override char GramaticalCorrection => 'M';
    }

    public class EndlessGravityNormalizerPotion : BasePotions
    {
        public override bool IsLoadingEnabled(Mod mod) => ModLoader.TryGetMod("CalamityMod", out mod);
        public override Item ItemBase => new(CallUtils.GetItemFromMod(RemnantOfTheAncientsMod.CalamityMod, "GravityNormalizerPotion"));

        public override char GramaticalCorrection => 'F';
    }

    public class EndlessAstralInjection : BasePotions
    {
        public override bool IsLoadingEnabled(Mod mod) => ModLoader.TryGetMod("CalamityMod", out mod);
        public override Item ItemBase => new(CallUtils.GetItemFromMod(RemnantOfTheAncientsMod.CalamityMod, "AstralInjection"));

        public override char GramaticalCorrection => 'F';
    }

    public class EndlessCeaselessHungerPotion : BasePotions
    {
        public override bool IsLoadingEnabled(Mod mod) => ModLoader.TryGetMod("CalamityMod", out mod);
        public override Item ItemBase => new(CallUtils.GetItemFromMod(RemnantOfTheAncientsMod.CalamityMod, "CeaselessHungerPotion"));

        public override char GramaticalCorrection => 'F';
    }

    public class EndlessSulphurskinPotion : BasePotions
    {
        public override bool IsLoadingEnabled(Mod mod) => ModLoader.TryGetMod("CalamityMod", out mod);
        public override Item ItemBase => new(CallUtils.GetItemFromMod(RemnantOfTheAncientsMod.CalamityMod, "SulphurskinPotion"));

        public override char GramaticalCorrection => 'F';
    }
}
