using RemnantOfTheAncientsMod.Common.ModCompativilitie;
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
        public static IItemDropRule ReaperModeVsNormal(int itemId, int chanceDenominatorInNormal, int chanceDenominatorInReaper)
        {
            return new DropBasedOnReaperMode(ItemDropRule.Common(itemId, chanceDenominatorInNormal), ItemDropRule.Common(itemId, chanceDenominatorInReaper));
        }
        public static IItemDropRule ReaperModeVsNormal(int? itemId, int chanceDenominatorInNormal, int chanceDenominatorInReaper)
        {
            return new DropBasedOnReaperMode(ItemDropRule.Common((int)itemId, chanceDenominatorInNormal), ItemDropRule.Common((int)itemId, chanceDenominatorInReaper));
        }
        public static IItemDropRule ReaperModeVsNormal(int itemId, int chanceDenominatorInNormal, int chanceDenominatorInReaper, int minimumDropped, int maximumDropped)
        {
            return new DropBasedOnReaperMode(ItemDropRule.Common(itemId, chanceDenominatorInNormal, minimumDropped, maximumDropped), ItemDropRule.Common(itemId, chanceDenominatorInReaper, minimumDropped, maximumDropped));
        }
        public static IItemDropRule ReaperModeVsNormal(int itemId, int chanceDenominatorInNormal, int chanceDenominatorInReaper, int minimumDropped, int maximumDropped, int minimumDroppedReaper, int maximumDroppedReaper)
        {
            return new DropBasedOnReaperMode(ItemDropRule.Common(itemId, chanceDenominatorInNormal, minimumDropped, maximumDropped), ItemDropRule.Common(itemId, chanceDenominatorInReaper, minimumDroppedReaper, maximumDroppedReaper));
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

        public static IItemDropRule NormalvsExpert(int itemId, int chanceDenominatorInNormal, int chanceDenominatorInExpert)
        {
            return new DropBasedOnReaperMode(ItemDropRule.Common(itemId, chanceDenominatorInNormal), ItemDropRule.Common(itemId, chanceDenominatorInExpert));
        }

        public static IItemDropRule CommonDropOnAllPlayersWithConditions(List<IItemDropRuleCondition> conditions, int itemId, int chanceDenominator = 1)
        {
            return new DropBasedOnMasterMode(ItemDropRule.DropNothing(), new DropPerPlayerOnThePlayerWithMultipleConditions(itemId, chanceDenominator, 1, 1, conditions));
        }
        public static IItemDropRule CommonDropOnAllPlayersWithConditionsNoAllActive(List<IItemDropRuleCondition> conditions, int itemId, int chanceDenominator = 1)
        {
            return new DropBasedOnMasterMode(new DropPerPlayerOnThePlayerWithMultipleConditionsNoAllActive(itemId, chanceDenominator, 1, 1, conditions), new DropPerPlayerOnThePlayerWithMultipleConditionsNoAllActive(itemId, chanceDenominator, 1, 1, conditions));
        }
        public static IItemDropRule AddIff(this ILoot loot, Func<bool> lambda, int itemID, int dropRateInt = 1, int minQuantity = 1, int maxQuantity = 1, bool ui = true, string desc = null)
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
            ItemDropAttemptResult result = default;
            result.State = ItemDropAttemptResultState.Success;
            return result;
        }
    }

    public class DropPerPlayerOnThePlayerWithMultipleConditionsNoAllActive(int itemId, int chanceDenominator, int amountDroppedMinimum, int amountDroppedMaximum, List<IItemDropRuleCondition> optionalCondition) : CommonDrop(itemId, chanceDenominator, amountDroppedMinimum, amountDroppedMaximum)
    {
        public List<IItemDropRuleCondition> conditions = optionalCondition;

        public override bool CanDrop(DropAttemptInfo info)
        {
            if (conditions != null)
            {
                foreach (IItemDropRuleCondition condition in conditions)
                {
                    if (condition.CanDrop(info))
                    {
                        return true;
                    }
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

    public class DropBasedOnMasterMode : IItemDropRule, INestedItemDropRule
    {
        public IItemDropRule ruleForDefault;

        public IItemDropRule ruleForMasterMode;

        public List<IItemDropRuleChainAttempt> ChainedRules { get; private set; }

        public DropBasedOnMasterMode(IItemDropRule ruleForDefault, IItemDropRule ruleForMasterMode)
        {
            this.ruleForDefault = ruleForDefault;
            this.ruleForMasterMode = ruleForMasterMode;
            ChainedRules = new List<IItemDropRuleChainAttempt>();
        }

        public bool CanDrop(DropAttemptInfo info)
        {
            if (info.IsMasterMode)
            {
                return ruleForMasterMode.CanDrop(info);
            }

            return ruleForDefault.CanDrop(info);
        }

        public ItemDropAttemptResult TryDroppingItem(DropAttemptInfo info)
        {
            ItemDropAttemptResult result = default(ItemDropAttemptResult);
            result.State = ItemDropAttemptResultState.DidNotRunCode;
            return result;
        }

        public ItemDropAttemptResult TryDroppingItem(DropAttemptInfo info, ItemDropRuleResolveAction resolveAction)
        {
            if (info.IsMasterMode)
            {
                return resolveAction(ruleForMasterMode, info);
            }

            return resolveAction(ruleForDefault, info);
        }

        public void ReportDroprates(List<DropRateInfo> drops, DropRateInfoChainFeed ratesInfo)
        {
            DropRateInfoChainFeed ratesInfo2 = ratesInfo.With(1f);
            ratesInfo2.AddCondition(new Conditions.IsMasterMode());
            ruleForMasterMode.ReportDroprates(drops, ratesInfo2);
            DropRateInfoChainFeed ratesInfo3 = ratesInfo.With(1f);
            ratesInfo3.AddCondition(new Conditions.NotMasterMode());
            ruleForDefault.ReportDroprates(drops, ratesInfo3);
            Chains.ReportDroprates(ChainedRules, 1f, drops, ratesInfo);
        }
    }
    public class DropBasedOnExpertMode : IItemDropRule, INestedItemDropRule
    {
        public IItemDropRule ruleForNormalMode;
        public IItemDropRule ruleForExpertMode;

        public List<IItemDropRuleChainAttempt> ChainedRules { get; private set; }

        public DropBasedOnExpertMode(IItemDropRule ruleForNormalMode, IItemDropRule ruleForExpertMode)
        {
            this.ruleForNormalMode = ruleForNormalMode;
            this.ruleForExpertMode = ruleForExpertMode;
            ChainedRules = new List<IItemDropRuleChainAttempt>();
        }

        public bool CanDrop(DropAttemptInfo info)
        {
            if (info.IsExpertMode)
                return ruleForExpertMode.CanDrop(info);

            return ruleForNormalMode.CanDrop(info);
        }

        public ItemDropAttemptResult TryDroppingItem(DropAttemptInfo info)
        {
            ItemDropAttemptResult result = default(ItemDropAttemptResult);
            result.State = ItemDropAttemptResultState.DidNotRunCode;
            return result;
        }

        public ItemDropAttemptResult TryDroppingItem(DropAttemptInfo info, ItemDropRuleResolveAction resolveAction)
        {
            if (info.IsExpertMode)
                return resolveAction(ruleForExpertMode, info);

            return resolveAction(ruleForNormalMode, info);
        }

        public void ReportDroprates(List<DropRateInfo> drops, DropRateInfoChainFeed ratesInfo)
        {
            DropRateInfoChainFeed ratesInfo2 = ratesInfo.With(1f);
            ratesInfo2.AddCondition(new Conditions.IsExpert());
            ruleForExpertMode.ReportDroprates(drops, ratesInfo2);
            DropRateInfoChainFeed ratesInfo3 = ratesInfo.With(1f);
            ratesInfo3.AddCondition(new Conditions.NotExpert());
            ruleForNormalMode.ReportDroprates(drops, ratesInfo3);
            Chains.ReportDroprates(ChainedRules, 1f, drops, ratesInfo);
        }
    }

    public class DropBasedOnReaperMode(IItemDropRule ruleForNormalMode, IItemDropRule ruleForReaperMode) : IItemDropRule, INestedItemDropRule
    {
        public IItemDropRule ruleForNormalMode = ruleForNormalMode;
        public IItemDropRule ruleForReaperMode = ruleForReaperMode;

        public List<IItemDropRuleChainAttempt> ChainedRules { get; private set; } = [];

        public bool CanDrop(DropAttemptInfo info)
        {
            if (DificultyUtils.ReaperMode)
                return ruleForReaperMode.CanDrop(info);

            return ruleForNormalMode.CanDrop(info);
        }

        public ItemDropAttemptResult TryDroppingItem(DropAttemptInfo info)
        {
            ItemDropAttemptResult result = default;
            result.State = ItemDropAttemptResultState.DidNotRunCode;
            return result;
        }

        public ItemDropAttemptResult TryDroppingItem(DropAttemptInfo info, ItemDropRuleResolveAction resolveAction)
        {
            if (DificultyUtils.ReaperMode)
                return resolveAction(ruleForReaperMode, info);

            return resolveAction(ruleForNormalMode, info);
        }

        public void ReportDroprates(List<DropRateInfo> drops, DropRateInfoChainFeed ratesInfo)
        {
            DropRateInfoChainFeed ratesInfo2 = ratesInfo.With(1f);
            ratesInfo2.AddCondition(new RemnantConditions.IsReaperMode());
            ruleForReaperMode.ReportDroprates(drops, ratesInfo2);
            DropRateInfoChainFeed ratesInfo3 = ratesInfo.With(1f);
            ratesInfo3.AddCondition(new RemnantConditions.IsNotReaperMode());
            ruleForNormalMode.ReportDroprates(drops, ratesInfo3);
            Chains.ReportDroprates(ChainedRules, 1f, drops, ratesInfo);
        }
    }
}

