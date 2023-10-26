using RemnantOfTheAncientsMod.Common.UtilsTweaks;
using RemnantOfTheAncientsMod.Content.Items.Consumables.Pociones.Models;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Content.Items.Consumables.Pociones
{
    public class EndlessCalciumPotion : BasePotions
    {
        public override bool IsLoadingEnabled(Mod mod) => ModLoader.TryGetMod("CalamityMod", out mod);
        public override Item ItemBase => new(ExternalModCallUtils.GetItemFromMod(RemnantOfTheAncientsMod.CalamityMod, "CalciumPotion"));   
    }

    public class EndlessAnechoicCoating : BasePotions
    {
        public override bool IsLoadingEnabled(Mod mod) => ModLoader.TryGetMod("CalamityMod", out mod);
        public override Item ItemBase => new(ExternalModCallUtils.GetItemFromMod(RemnantOfTheAncientsMod.CalamityMod, "AnechoicCoating"));
    }
    public class EndlessBoundingPotion : BasePotions
    {
        public override bool IsLoadingEnabled(Mod mod) => ModLoader.TryGetMod("CalamityMod", out mod);
        public override Item ItemBase => new(ExternalModCallUtils.GetItemFromMod(RemnantOfTheAncientsMod.CalamityMod, "BoundingPotion"));
    }
    public class EndlessPotionofOmniscience : BasePotions
    {
        public override bool IsLoadingEnabled(Mod mod) => ModLoader.TryGetMod("CalamityMod", out mod);
        public override Item ItemBase => new(ExternalModCallUtils.GetItemFromMod(RemnantOfTheAncientsMod.CalamityMod, "PotionofOmniscience"));
    }
    public class EndlessTeslaPotion : BasePotions
    {
        public override bool IsLoadingEnabled(Mod mod) => ModLoader.TryGetMod("CalamityMod", out mod);
        public override Item ItemBase => new(ExternalModCallUtils.GetItemFromMod(RemnantOfTheAncientsMod.CalamityMod, "TeslaPotion"));
    }
    public class EndlessZenPotion : BasePotions
    {
        public override bool IsLoadingEnabled(Mod mod) => ModLoader.TryGetMod("CalamityMod", out mod);
        public override Item ItemBase => new(ExternalModCallUtils.GetItemFromMod(RemnantOfTheAncientsMod.CalamityMod, "ZenPotion"));
    }
    public class EndlessZergPotion : BasePotions
    {
        public override bool IsLoadingEnabled(Mod mod) => ModLoader.TryGetMod("CalamityMod", out mod);
        public override Item ItemBase => new(ExternalModCallUtils.GetItemFromMod(RemnantOfTheAncientsMod.CalamityMod, "ZergPotion"));
    }
    public class EndlessSoaringPotion : BasePotions
    {
        public override bool IsLoadingEnabled(Mod mod) => ModLoader.TryGetMod("CalamityMod", out mod);
        public override Item ItemBase => new(ExternalModCallUtils.GetItemFromMod(RemnantOfTheAncientsMod.CalamityMod, "SoaringPotion"));
    }
    public class EndlessPhotosynthesisPotion : BasePotions
    {
        public override bool IsLoadingEnabled(Mod mod) => ModLoader.TryGetMod("CalamityMod", out mod);
        public override Item ItemBase => new(ExternalModCallUtils.GetItemFromMod(RemnantOfTheAncientsMod.CalamityMod, "PhotosynthesisPotion"));
    }
    public class EndlessFlaskOfCrumbling : BasePotions
    {
        public override bool IsLoadingEnabled(Mod mod) => ModLoader.TryGetMod("CalamityMod", out mod);
        public override Item ItemBase => new(ExternalModCallUtils.GetItemFromMod(RemnantOfTheAncientsMod.CalamityMod, "FlaskOfCrumbling"));
    }
    public class EndlessFlaskOfBrimstone : BasePotions
    {
        public override bool IsLoadingEnabled(Mod mod) => ModLoader.TryGetMod("CalamityMod", out mod);
        public override Item ItemBase => new(ExternalModCallUtils.GetItemFromMod(RemnantOfTheAncientsMod.CalamityMod, "FlaskOfBrimstone"));
    }
    public class EndlessFlaskOfHolyFlames : BasePotions
    {
        public override bool IsLoadingEnabled(Mod mod) => ModLoader.TryGetMod("CalamityMod", out mod);
        public override Item ItemBase => new(ExternalModCallUtils.GetItemFromMod(RemnantOfTheAncientsMod.CalamityMod, "FlaskOfHolyFlames"));
    }

    public class EndlessGravityNormalizerPotion : BasePotions
    {
        public override bool IsLoadingEnabled(Mod mod) => ModLoader.TryGetMod("CalamityMod", out mod);
        public override Item ItemBase => new(ExternalModCallUtils.GetItemFromMod(RemnantOfTheAncientsMod.CalamityMod, "GravityNormalizerPotion"));
    }

    public class EndlessAstralInjection : BasePotions
    {
        public override bool IsLoadingEnabled(Mod mod) => ModLoader.TryGetMod("CalamityMod", out mod);
        public override Item ItemBase => new(ExternalModCallUtils.GetItemFromMod(RemnantOfTheAncientsMod.CalamityMod, "AstralInjection"));
    }

    public class EndlessCeaselessHungerPotion : BasePotions
    {
        public override bool IsLoadingEnabled(Mod mod) => ModLoader.TryGetMod("CalamityMod", out mod);
        public override Item ItemBase => new(ExternalModCallUtils.GetItemFromMod(RemnantOfTheAncientsMod.CalamityMod, "CeaselessHungerPotion"));
    }
}
