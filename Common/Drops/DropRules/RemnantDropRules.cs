using System;
using System.Collections.Generic;
using Terraria.GameContent.ItemDropRules;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Common.Drops.DropRules
{
    public static class RemnantDropRules
    {
        public static IItemDropRule ReaperModeCommonDrop(int itemId, int chanceDenominator = 1, int minimumDropped = 1, int maximumDropped = 1, int chanceNumerator = 1)
        {
            return new ItemDropWithConditionRule(itemId, chanceDenominator, minimumDropped, maximumDropped, new RemnantConditions.IsReaperMode(), chanceNumerator);
        }
        public static IItemDropRule ReaperModeCommonDropOnAllPlayers(int itemId, int chanceDenominator = 1)
        {
            return new DropBasedOnMasterMode(ItemDropRule.DropNothing(), new DropPerPlayerOnThePlayer(itemId, chanceDenominator, 1, 1, new RemnantConditions.IsReaperMode()));
        }
        public static IItemDropRule ReaperModeCommonDropOnAllPlayersWithConditions(IItemDropRuleCondition conditions, int itemId, int chanceDenominator = 1)
        {
            IItemDropRule drop = new DropPerPlayerOnThePlayerWithMultipleConditions(itemId, chanceDenominator, 1, 1, new List<IItemDropRuleCondition>() { conditions, new RemnantConditions.IsReaperMode() });
            return new DropBasedOnMasterMode(drop, drop);
        }
        public static IItemDropRule CommonDropOnAllPlayersWithConditions(List<IItemDropRuleCondition> conditions, int itemId, int chanceDenominator = 1)
        {
            return new DropBasedOnMasterMode(ItemDropRule.DropNothing(), new DropPerPlayerOnThePlayerWithMultipleConditions(itemId, chanceDenominator, 1, 1, conditions));
        }
        public static IItemDropRule CommonDropOnAllPlayersWithConditionsNoAllActive(List<IItemDropRuleCondition> conditions, int itemId, int chanceDenominator = 1)
        {
            return new DropBasedOnMasterMode(ItemDropRule.DropNothing(), new DropPerPlayerOnThePlayerWithMultipleConditions(itemId, chanceDenominator, 1, 1, conditions));
        }
        public static IItemDropRule AddIf(this ILoot loot, Func<bool> lambda, int itemID, int dropRateInt = 1, int minQuantity = 1, int maxQuantity = 1, bool ui = true, string desc = null)
        {
            return loot.Add(ItemDropRule.ByCondition(DropUtils.If(lambda, ui, desc), itemID, dropRateInt, minQuantity, maxQuantity));
        }
        public static IItemDropRule InfernumModeCommonDrop(int itemId, int chanceDenominator = 1, int minimumDropped = 1, int maximumDropped = 1, int chanceNumerator = 1)
        {
            return new ItemDropWithConditionRule(itemId, chanceDenominator, minimumDropped, maximumDropped, new RemnantConditions.IsInfernum(), chanceNumerator);
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
                foreach (IItemDropRuleCondition condition in conditions)
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


    public class LambdaDropRuleCondition : IItemDropRuleCondition, IProvideItemConditionDescription
    {
        private readonly Func<DropAttemptInfo, bool> conditionLambda;

        private readonly bool visibleInUI;

        private readonly string description;

        internal LambdaDropRuleCondition(Func<DropAttemptInfo, bool> lambda, bool ui = true, string desc = null)
        {
            conditionLambda = lambda;
            visibleInUI = ui;
            description = desc;
        }

        public bool CanDrop(DropAttemptInfo info)
        {
            return conditionLambda(info);
        }

        public bool CanShowItemDropInUI()
        {
            return visibleInUI;
        }

        public string GetConditionDescription()
        {
            return description;
        }
    }

    internal class LambdaDropRuleCondition2 : IItemDropRuleCondition, IProvideItemConditionDescription
    {
        private readonly Func<DropAttemptInfo, bool> conditionLambda;

        private readonly Func<bool> visibleInUI;

        private readonly string description;

        internal LambdaDropRuleCondition2(Func<DropAttemptInfo, bool> lambda, Func<bool> ui, string desc = null)
        {
            conditionLambda = lambda;
            visibleInUI = ui;
            description = desc;
        }

        public bool CanDrop(DropAttemptInfo info)
        {
            return conditionLambda(info);
        }

        public bool CanShowItemDropInUI()
        {
            return visibleInUI();
        }

        public string GetConditionDescription()
        {
            return description;
        }
    }

    internal class LambdaDropRuleCondition3 : IItemDropRuleCondition, IProvideItemConditionDescription
    {
        private readonly Func<DropAttemptInfo, bool> conditionLambda;

        private readonly Func<bool> visibleInUI;

        private readonly Func<string> description;

        internal LambdaDropRuleCondition3(Func<DropAttemptInfo, bool> lambda, Func<bool> ui, Func<string> desc)
        {
            conditionLambda = lambda;
            visibleInUI = ui;
            description = desc;
        }

        public bool CanDrop(DropAttemptInfo info)
        {
            return conditionLambda(info);
        }

        public bool CanShowItemDropInUI()
        {
            return visibleInUI();
        }

        public string GetConditionDescription()
        {
            return description();
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

