using RemnantOfTheAncientsMod.Items.ReaperSouls;
using RemnantOfTheAncientsMod.World;
using Terraria;
using Terraria.GameContent.ItemDropRules;

namespace RemnantOfTheAncientsMod.Drops
{
    public class RemnantDropRules : ItemDropRule
    {
        public class SlimeReaperSoulRule : IItemDropRuleCondition, IProvideItemConditionDescription
        {
            public bool CanDrop(DropAttemptInfo info) => !Main.LocalPlayer.GetModPlayer<SlimeReaperSoulPlayer>().SlimeReaperUpgrade;
            public bool CanShowItemDropInUI() => true;
            public string GetConditionDescription() => null;
        }
        public class EyeOfChutuluReaperSoulRule : IItemDropRuleCondition, IProvideItemConditionDescription
        {
            public bool CanDrop(DropAttemptInfo info) => !Main.LocalPlayer.GetModPlayer<EyeReaperSoulPlayer>().EyeReaperUpgrade && Reaper.ReaperMode;
            public bool CanShowItemDropInUI() => true;
            public string GetConditionDescription() => null;
        }
        public class CorruptReaperSoulRule : IItemDropRuleCondition, IProvideItemConditionDescription
        {
            public bool CanDrop(DropAttemptInfo info) => !Main.LocalPlayer.GetModPlayer<CorruptReaperSoulPlayer>().CorruptReaperUpgrade && Reaper.ReaperMode;
            public bool CanShowItemDropInUI() => true;
            public string GetConditionDescription() => null;
        }
        public class QueenBeeReaperSoulRule : IItemDropRuleCondition, IProvideItemConditionDescription
        {
            public bool CanDrop(DropAttemptInfo info) => !Main.LocalPlayer.GetModPlayer<BeeReaperSoulPlayer>().BeeReaperUpgrade && Reaper.ReaperMode;
            public bool CanShowItemDropInUI() => true;
            public string GetConditionDescription() => null;
        }
        public class SkeletronReaperSoulRule : IItemDropRuleCondition, IProvideItemConditionDescription
        {
            public bool CanDrop(DropAttemptInfo info) => !Main.LocalPlayer.GetModPlayer<SkeletonReaperSoulPlayer>().SkeletonReaperUpgrade && Reaper.ReaperMode;
            public bool CanShowItemDropInUI() => true;
            public string GetConditionDescription() => null;
        }
        public class DeerclopsReaperSoulRule : IItemDropRuleCondition, IProvideItemConditionDescription
        {
            public bool CanDrop(DropAttemptInfo info) => !Main.LocalPlayer.GetModPlayer<DeerclopsReaperSoulPlayer>().DeerclopsReaperUpgrade && Reaper.ReaperMode;
            public bool CanShowItemDropInUI() => true;
            public string GetConditionDescription() => null;
        }
        public class WallOfFLeshReaperSoulRule : IItemDropRuleCondition, IProvideItemConditionDescription
        {
            public bool CanDrop(DropAttemptInfo info) => !Main.LocalPlayer.GetModPlayer<FleshReaperSoulPlayer>().FleshReaperUpgrade && Reaper.ReaperMode;
            public bool CanShowItemDropInUI() => true;
            public string GetConditionDescription() => null;
        }
        public class QueenSlimeReaperSoulRule : IItemDropRuleCondition, IProvideItemConditionDescription
        {
            public bool CanDrop(DropAttemptInfo info) => !Main.LocalPlayer.GetModPlayer<QueenReaperSoulPlayer>().QueenReaperUpgrade && Reaper.ReaperMode;
            public bool CanShowItemDropInUI() => true;
            public string GetConditionDescription() => null;
        }
        public class RetinazorReaperSoulRule : IItemDropRuleCondition, IProvideItemConditionDescription
        {
            public bool CanDrop(DropAttemptInfo info) => !Main.LocalPlayer.GetModPlayer<RetinazorReaperSoulPlayer>().RetinazorReaperUpgrade && Reaper.ReaperMode;
            public bool CanShowItemDropInUI() => true;
            public string GetConditionDescription() => null;
        }
        public class SpazmatismReaperSoulRule : IItemDropRuleCondition, IProvideItemConditionDescription
        {
            public bool CanDrop(DropAttemptInfo info) => !Main.LocalPlayer.GetModPlayer<SpazmatismReaperSoulPlayer>().SpazmatismReaperUpgrade && Reaper.ReaperMode;
            public bool CanShowItemDropInUI() => true;
            public string GetConditionDescription() => null;
        }
        public class SkeletronPrimeReaperSoulRule : IItemDropRuleCondition, IProvideItemConditionDescription
        {
            public bool CanDrop(DropAttemptInfo info) => !Main.LocalPlayer.GetModPlayer<SkeletronPrimeReaperSoulPlayer>().SkeletronPrimeReaperUpgrade && Reaper.ReaperMode;
            public bool CanShowItemDropInUI() => true;
            public string GetConditionDescription() => null;
        }
        public class DestroyerReaperSoulRule : IItemDropRuleCondition, IProvideItemConditionDescription
        {
            public bool CanDrop(DropAttemptInfo info) => !Main.LocalPlayer.GetModPlayer<DestroyerReaperSoulPlayer>().DestroyerReaperUpgrade && Reaper.ReaperMode;
            public bool CanShowItemDropInUI() => true;
            public string GetConditionDescription() => null;
        }
        public class PlanteraReaperSoulRule : IItemDropRuleCondition, IProvideItemConditionDescription
        {
            public bool CanDrop(DropAttemptInfo info) => !Main.LocalPlayer.GetModPlayer<PlantReaperSoulPlayer>().PlantReaperUpgrade && Reaper.ReaperMode;
            public bool CanShowItemDropInUI() => true;
            public string GetConditionDescription() => null;
        }
        public class EmpressOfLightReaperSoulRule : IItemDropRuleCondition, IProvideItemConditionDescription
        {
            public bool CanDrop(DropAttemptInfo info) => !Main.LocalPlayer.GetModPlayer<EmpressReaperSoulPlayer>().EmpressReaperUpgrade && Reaper.ReaperMode;
            public bool CanShowItemDropInUI() => true;
            public string GetConditionDescription() => null;
        }
        public class GolemReaperSoulRule : IItemDropRuleCondition, IProvideItemConditionDescription
        {
            public bool CanDrop(DropAttemptInfo info) => !Main.LocalPlayer.GetModPlayer<GolemReaperSoulPlayer>().GolemReaperUpgrade && Reaper.ReaperMode;
            public bool CanShowItemDropInUI() => true;
            public string GetConditionDescription() => null;
        }
        public class DukeFishronReaperSoulRule : IItemDropRuleCondition, IProvideItemConditionDescription
        {
            public bool CanDrop(DropAttemptInfo info) => !Main.LocalPlayer.GetModPlayer<DukeReaperSoulPlayer>().DukeReaperUpgrade && Reaper.ReaperMode;
            public bool CanShowItemDropInUI() => true;
            public string GetConditionDescription() => null;
        }
        public class LunaticCultistReaperSoulRule : IItemDropRuleCondition, IProvideItemConditionDescription
        {
            public bool CanDrop(DropAttemptInfo info) => !Main.LocalPlayer.GetModPlayer<CultistReaperSoulPlayer>().CultistReaperUpgrade && Reaper.ReaperMode;
            public bool CanShowItemDropInUI() => true;
            public string GetConditionDescription() => null;
        }
        public class MoonLordReaperSoulRule : IItemDropRuleCondition, IProvideItemConditionDescription
        {
            public bool CanDrop(DropAttemptInfo info) => !Main.LocalPlayer.GetModPlayer<MoonReaperSoulPlayer>().MoonReaperUpgrade && Reaper.ReaperMode;
            public bool CanShowItemDropInUI() => true;
            public string GetConditionDescription() => null;
        }
    }
}
