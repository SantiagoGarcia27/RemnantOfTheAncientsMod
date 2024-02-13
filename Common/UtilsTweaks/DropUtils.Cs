using RemnantOfTheAncientsMod.Common.Drops.DropRules;
using System;
using Terraria.GameContent.ItemDropRules;
using Terraria.ModLoader;

public class DropUtils
{
    public static IItemDropRuleCondition If(Func<bool> lambda)
    {
        return new LambdaDropRuleCondition((DropAttemptInfo _) => lambda());
    }

    public static IItemDropRuleCondition If(Func<bool> lambda, bool ui = true, string desc = null)
    {
        return new LambdaDropRuleCondition(LambdaInfoWrapper, ui, desc);
        bool LambdaInfoWrapper(DropAttemptInfo _)
        {
            return lambda();
        }
    }

    public static IItemDropRuleCondition If(Func<bool> lambda, Func<bool> ui, string desc = null)
    {
        return new LambdaDropRuleCondition2(LambdaInfoWrapper, ui, desc);
        bool LambdaInfoWrapper(DropAttemptInfo _)
        {
            return lambda();
        }
    }

    public static IItemDropRuleCondition If(Func<bool> lambda, Func<bool> ui, Func<string> desc)
    {
        return new LambdaDropRuleCondition3(LambdaInfoWrapper, ui, desc);
        bool LambdaInfoWrapper(DropAttemptInfo _)
        {
            return lambda();
        }
    }

    public static IItemDropRuleCondition If(Func<DropAttemptInfo, bool> lambda)
    {
        return new LambdaDropRuleCondition(lambda);
    }

    public static IItemDropRuleCondition If(Func<DropAttemptInfo, bool> lambda, bool ui = true, string desc = null)
    {
        return new LambdaDropRuleCondition(lambda, ui, desc);
    }

    public static IItemDropRuleCondition If(Func<DropAttemptInfo, bool> lambda, Func<bool> ui, string desc = null)
    {
        return new LambdaDropRuleCondition2(lambda, ui, desc);
    }

    public static IItemDropRuleCondition If(Func<DropAttemptInfo, bool> lambda, Func<bool> ui, Func<string> desc)
    {
        return new LambdaDropRuleCondition3(lambda, ui, desc);
    }
}