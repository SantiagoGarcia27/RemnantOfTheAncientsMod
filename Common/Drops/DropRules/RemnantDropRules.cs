using RemnantOfTheAncientsMod.Common.ModCompativilitie;
using RemnantOfTheAncientsMod.World;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent.ItemDropRules;

namespace RemnantOfTheAncientsMod.Common.Drops.DropRules
{
    public class RemnantDropRules : ItemDropRule
    {
        public static IItemDropRule ReaperModeCommonDrop(int itemId, int chanceDenominator = 1, int minimumDropped = 1, int maximumDropped = 1, int chanceNumerator = 1)
        {
            return new ItemDropWithConditionRule(itemId, chanceDenominator, minimumDropped, maximumDropped, new RemnantConditions.IsReaperMode(), chanceNumerator);
        }
        public static IItemDropRule ReaperModeCommonDropOnAllPlayers(int itemId, int chanceDenominator = 1)
        {
            return new DropBasedOnMasterMode(DropNothing(), new DropPerPlayerOnThePlayer(itemId, chanceDenominator, 1, 1, new RemnantConditions.IsReaperMode()));
        }
        public static IItemDropRule ReaperModeCommonDropOnAllPlayersWithConditions(IItemDropRuleCondition conditions, int itemId, int chanceDenominator = 1)
        {
            return new DropBasedOnMasterMode(DropNothing(), new DropPerPlayerOnThePlayerWithMultipleConditions(itemId, chanceDenominator, 1, 1,new List<IItemDropRuleCondition>() { conditions, new RemnantConditions.IsReaperMode() }));
        }
        public static IItemDropRule CommonDropOnAllPlayersWithConditions(List<IItemDropRuleCondition> conditions, int itemId, int chanceDenominator = 1)
        {
            return new DropBasedOnMasterMode(DropNothing(), new DropPerPlayerOnThePlayerWithMultipleConditions(itemId, chanceDenominator, 1, 1, conditions));
        }
        public static IItemDropRule CommonDropOnAllPlayersWithConditionsNoAllActive(List<IItemDropRuleCondition> conditions, int itemId, int chanceDenominator = 1)
        {
            return new DropBasedOnMasterMode(DropNothing(), new DropPerPlayerOnThePlayerWithMultipleConditions(itemId, chanceDenominator, 1, 1, conditions));
        }
    }

    public class DropPerPlayerOnThePlayerWithMultipleConditions : CommonDrop
    {
        public List<IItemDropRuleCondition> conditions;

        public DropPerPlayerOnThePlayerWithMultipleConditions(int itemId, int chanceDenominator, int amountDroppedMinimum, int amountDroppedMaximum, List<IItemDropRuleCondition> optionalCondition)
            : base(itemId, chanceDenominator, amountDroppedMinimum, amountDroppedMaximum)
        {
            conditions = optionalCondition;
        }

        public override bool CanDrop(DropAttemptInfo info)
        {
            if (conditions != null)
            {
                foreach(IItemDropRuleCondition condition in conditions) 
                {
                    if (!condition.CanDrop(info)) return false;
                }
                return true;
            }
            return true;
        }
        public override ItemDropAttemptResult TryDroppingItem(DropAttemptInfo info)
        {
            CommonCode.DropItemForEachInteractingPlayerOnThePlayer(info.npc, itemId, info.rng, chanceNumerator, chanceDenominator, info.rng.Next(amountDroppedMinimum, amountDroppedMaximum + 1));
            ItemDropAttemptResult result = default(ItemDropAttemptResult);
            result.State = ItemDropAttemptResultState.Success;
            return result;
        }
    }

    public class DropPerPlayerOnThePlayerWithMultipleConditionsNoAllActive : CommonDrop
    {
        public List<IItemDropRuleCondition> conditions;

        public DropPerPlayerOnThePlayerWithMultipleConditionsNoAllActive(int itemId, int chanceDenominator, int amountDroppedMinimum, int amountDroppedMaximum, List<IItemDropRuleCondition> optionalCondition)
            : base(itemId, chanceDenominator, amountDroppedMinimum, amountDroppedMaximum)
        {
            conditions = optionalCondition;
        }

        public override bool CanDrop(DropAttemptInfo info)
        {
            if (conditions != null)
            {
                foreach (IItemDropRuleCondition condition in conditions)
                {
                    if (condition.CanDrop(info)) return true;
                }
                return false;
            }
            return true;
        }
        public override ItemDropAttemptResult TryDroppingItem(DropAttemptInfo info)
        {
            CommonCode.DropItemForEachInteractingPlayerOnThePlayer(info.npc, itemId, info.rng, chanceNumerator, chanceDenominator, info.rng.Next(amountDroppedMinimum, amountDroppedMaximum + 1));
            ItemDropAttemptResult result = default(ItemDropAttemptResult);
            result.State = ItemDropAttemptResultState.Success;
            return result;
        }
    }
    //public class IsReaperMode : IItemDropRuleCondition, IProvideItemConditionDescription
    //{
    //    public bool CanDrop(DropAttemptInfo info) => DificultyUtils.ReaperMode;

    //    public bool CanShowItemDropInUI() => DificultyUtils.ReaperMode;

    //    public string GetConditionDescription() => null;
    //}
    //public class IsReaperAndNormal : IItemDropRuleCondition, IProvideItemConditionDescription
    //{
    //    public bool CanDrop(DropAttemptInfo info) => DificultyUtils.ReaperMode ? (!Main.expertMode && !Main.masterMode) : false;
    //    public bool CanShowItemDropInUI() => DificultyUtils.ReaperMode ? (!Main.expertMode && !Main.masterMode) : false;
    //    public string GetConditionDescription() => null;
    //}
    //public class IsReaperAndExpert : IItemDropRuleCondition, IProvideItemConditionDescription
    //{
    //    public bool CanDrop(DropAttemptInfo info) => DificultyUtils.ReaperMode ? (Main.expertMode && !Main.masterMode) : false; 
    //    public bool CanShowItemDropInUI() => DificultyUtils.ReaperMode ? (Main.expertMode && !Main.masterMode) : false;
    //    public string GetConditionDescription() => null;
    //}

    //public class IsReaperAndMaster : IItemDropRuleCondition, IProvideItemConditionDescription
    //{
    //    public bool CanDrop(DropAttemptInfo info) => DificultyUtils.ReaperMode ? (!Main.expertMode && Main.masterMode) : false;
    //    public bool CanShowItemDropInUI() => DificultyUtils.ReaperMode ? (!Main.expertMode && Main.masterMode) : false;
    //    public string GetConditionDescription() => null;
    //}


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

