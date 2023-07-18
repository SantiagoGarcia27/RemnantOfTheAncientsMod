using RemnantOfTheAncientsMod.World;
using Terraria;
using Terraria.GameContent.ItemDropRules;

namespace RemnantOfTheAncientsMod.Common.Drops.DropRules
{
    public class RemnantDropRules : ItemDropRule
    {
        public static IItemDropRule ReaperModeCommonDrop(IItemDropRuleCondition condition, int itemId, int chanceDenominator = 1, int minimumDropped = 1, int maximumDropped = 1, int chanceNumerator = 1)
        {
            return new ItemDropWithConditionRule(itemId, chanceDenominator, minimumDropped, maximumDropped, condition, chanceNumerator);
        }
    }

    public class IsReaperMode : IItemDropRuleCondition, IProvideItemConditionDescription
    {
        public bool CanDrop(DropAttemptInfo info) => Reaper.ReaperMode;

        public bool CanShowItemDropInUI() => Reaper.ReaperMode;

        public string GetConditionDescription() => null;
    }
    public class IsReaperAndNormal : IItemDropRuleCondition, IProvideItemConditionDescription
    {
        public bool CanDrop(DropAttemptInfo info) => Reaper.ReaperMode ? (!Main.expertMode && !Main.masterMode) : false;
        public bool CanShowItemDropInUI() => Reaper.ReaperMode ? (!Main.expertMode && !Main.masterMode) : false;
        public string GetConditionDescription() => null;
    }
    public class IsReaperAndExpert : IItemDropRuleCondition, IProvideItemConditionDescription
    {
        public bool CanDrop(DropAttemptInfo info) => Reaper.ReaperMode ? (Main.expertMode && !Main.masterMode) : false; 
        public bool CanShowItemDropInUI() => Reaper.ReaperMode ? (Main.expertMode && !Main.masterMode) : false;
        public string GetConditionDescription() => null;
    }

    public class IsReaperAndMaster : IItemDropRuleCondition, IProvideItemConditionDescription
    {
        public bool CanDrop(DropAttemptInfo info) => Reaper.ReaperMode ? (!Main.expertMode && Main.masterMode) : false;
        public bool CanShowItemDropInUI() => Reaper.ReaperMode ? (!Main.expertMode && Main.masterMode) : false;
        public string GetConditionDescription() => null;
    }
    public class IsDay : IItemDropRuleCondition, IProvideItemConditionDescription
    {
        public bool CanDrop(DropAttemptInfo info) => Main.dayTime;
        public bool CanShowItemDropInUI() => Main.dayTime;
        public string GetConditionDescription() => null;
    }
    public class IsNight : IItemDropRuleCondition, IProvideItemConditionDescription
    {
        public bool CanDrop(DropAttemptInfo info) => !Main.dayTime;
        public bool CanShowItemDropInUI() => !Main.dayTime;
        public string GetConditionDescription() => null;
    }

    //public class DropBasedOnReaperNormalMode : IItemDropRule, INestedItemDropRule
    //{
    //    public IItemDropRule ruleForNormalMode;

    //    public IItemDropRule ruleForReaperMode;

    //    public List<IItemDropRuleChainAttempt> ChainedRules { get; private set; }

    //    public DropBasedOnReaperNormalMode(IItemDropRule ruleForNormalMode, IItemDropRule ruleForReaperMode)
    //    {
    //        this.ruleForNormalMode = ruleForNormalMode;
    //        this.ruleForReaperMode = ruleForReaperMode;
    //        ChainedRules = new List<IItemDropRuleChainAttempt>();
    //    }

    //    public bool CanDrop(DropAttemptInfo info)
    //    {
    //        if (info.IsReaperMode)
    //        {
    //            return ruleForReaperMode.CanDrop(info);
    //        }

    //        return ruleForNormalMode.CanDrop(info);
    //    }

    //    public ItemDropAttemptResult TryDroppingItem(DropAttemptInfo info)
    //    {
    //        ItemDropAttemptResult result = default;
    //        result.State = ItemDropAttemptResultState.DidNotRunCode;
    //        return result;
    //    }

    //    public ItemDropAttemptResult TryDroppingItem(DropAttemptInfo info, ItemDropRuleResolveAction resolveAction)
    //    {
    //        if (info.IsReaperMode)
    //        {
    //            return resolveAction(ruleForReaperMode, info);
    //        }

    //        return resolveAction(ruleForNormalMode, info);
    //    }

    //    public void ReportDroprates(List<DropRateInfo> drops, DropRateInfoChainFeed ratesInfo)
    //    {
    //        DropRateInfoChainFeed ratesInfo2 = ratesInfo.With(1f);
    //        ratesInfo2.AddCondition(new Conditions.IsExpert());
    //        ruleForReaperMode.ReportDroprates(drops, ratesInfo2);
    //        DropRateInfoChainFeed ratesInfo3 = ratesInfo.With(1f);
    //        ratesInfo3.AddCondition(new Conditions.NotExpert());
    //        ruleForNormalMode.ReportDroprates(drops, ratesInfo3);
    //        Chains.ReportDroprates(ChainedRules, 1f, drops, ratesInfo);
    //    }
    //}
}

