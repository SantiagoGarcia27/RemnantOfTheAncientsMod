using RemnantOfTheAncientsMod.Content.Items.Consumables.ReaperSouls;
using RemnantOfTheAncientsMod.World;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent.ItemDropRules;

namespace RemnantOfTheAncientsMod.Common.Drops.DropRules
{
    public class RemnantConditionDropRules : ItemDropRule
    {
        public static IItemDropRule ReaperModeCommonDrop(IItemDropRuleCondition condition, int itemId, int chanceDenominator = 1, int minimumDropped = 1, int maximumDropped = 1, int chanceNumerator = 1)
        {
            return new ItemDropWithConditionRule(itemId, chanceDenominator, minimumDropped, maximumDropped, condition, chanceNumerator);
        }
    }
    public class SlimeReaperSoulRule : IItemDropRuleCondition, IProvideItemConditionDescription
    {
        public bool CanDrop(DropAttemptInfo info) => !Main.LocalPlayer.GetModPlayer<ReaperPlayer>().SoulsUpgrades[0] && Reaper.ReaperMode;
        public bool CanShowItemDropInUI() => true;
        public string GetConditionDescription() => null;
    }
    public class EyeOfChutuluReaperSoulRule : IItemDropRuleCondition, IProvideItemConditionDescription
    {
        public bool CanDrop(DropAttemptInfo info) => !Main.LocalPlayer.GetModPlayer<ReaperPlayer>().SoulsUpgrades[1] && Reaper.ReaperMode;
        public bool CanShowItemDropInUI() => true;
        public string GetConditionDescription() => null;
    }
    public class CorruptReaperSoulRule : IItemDropRuleCondition, IProvideItemConditionDescription
    {
        public bool CanDrop(DropAttemptInfo info) => !Main.LocalPlayer.GetModPlayer<ReaperPlayer>().SoulsUpgrades[2] && Reaper.ReaperMode;
        public bool CanShowItemDropInUI() => true;
        public string GetConditionDescription() => null;
    }
    public class QueenBeeReaperSoulRule : IItemDropRuleCondition, IProvideItemConditionDescription
    {
        public bool CanDrop(DropAttemptInfo info) => !Main.LocalPlayer.GetModPlayer<ReaperPlayer>().SoulsUpgrades[3] && Reaper.ReaperMode;
        public bool CanShowItemDropInUI() => true;
        public string GetConditionDescription() => null;
    }
    public class SkeletronReaperSoulRule : IItemDropRuleCondition, IProvideItemConditionDescription
    {
        public bool CanDrop(DropAttemptInfo info) => !Main.LocalPlayer.GetModPlayer<ReaperPlayer>().SoulsUpgrades[4] && Reaper.ReaperMode;
        public bool CanShowItemDropInUI() => true;
        public string GetConditionDescription() => null;
    }
    public class DeerclopsReaperSoulRule : IItemDropRuleCondition, IProvideItemConditionDescription
    {
        public bool CanDrop(DropAttemptInfo info) => !Main.LocalPlayer.GetModPlayer<ReaperPlayer>().SoulsUpgrades[5] && Reaper.ReaperMode;
        public bool CanShowItemDropInUI() => true;
        public string GetConditionDescription() => null;
    }
    public class DesertAnhilatorReaperSoulRule : IItemDropRuleCondition, IProvideItemConditionDescription
    {
        public bool CanDrop(DropAttemptInfo info) => !Main.LocalPlayer.GetModPlayer<ReaperPlayer>().SoulsUpgrades[6] && Reaper.ReaperMode;
        public bool CanShowItemDropInUI() => true;
        public string GetConditionDescription() => null;
    }
    public class WallOfFLeshReaperSoulRule : IItemDropRuleCondition, IProvideItemConditionDescription
    {
        public bool CanDrop(DropAttemptInfo info) => !Main.LocalPlayer.GetModPlayer<ReaperPlayer>().SoulsUpgrades[7] && Reaper.ReaperMode;
        public bool CanShowItemDropInUI() => true;
        public string GetConditionDescription() => null;
    }
    public class FrozenAssaulterReaperSoulRule : IItemDropRuleCondition, IProvideItemConditionDescription
    {
        public bool CanDrop(DropAttemptInfo info) => !Main.LocalPlayer.GetModPlayer<ReaperPlayer>().SoulsUpgrades[8] && Reaper.ReaperMode;
        public bool CanShowItemDropInUI() => true;
        public string GetConditionDescription() => null;
    }
    public class QueenSlimeReaperSoulRule : IItemDropRuleCondition, IProvideItemConditionDescription
    {
        public bool CanDrop(DropAttemptInfo info) => !Main.LocalPlayer.GetModPlayer<ReaperPlayer>().SoulsUpgrades[9] && Reaper.ReaperMode;
        public bool CanShowItemDropInUI() => true;
        public string GetConditionDescription() => null;
    }
    public class RetinazorReaperSoulRule : IItemDropRuleCondition, IProvideItemConditionDescription
    {
        public bool CanDrop(DropAttemptInfo info) => !Main.LocalPlayer.GetModPlayer<ReaperPlayer>().SoulsUpgrades[11] && Reaper.ReaperMode;
        public bool CanShowItemDropInUI() => true;
        public string GetConditionDescription() => null;
    }
    public class SpazmatismReaperSoulRule : IItemDropRuleCondition, IProvideItemConditionDescription
    {
        public bool CanDrop(DropAttemptInfo info) => !Main.LocalPlayer.GetModPlayer<ReaperPlayer>().SoulsUpgrades[10] && Reaper.ReaperMode;
        public bool CanShowItemDropInUI() => true;
        public string GetConditionDescription() => null;
    }
    public class SkeletronPrimeReaperSoulRule : IItemDropRuleCondition, IProvideItemConditionDescription
    {
        public bool CanDrop(DropAttemptInfo info) => !Main.LocalPlayer.GetModPlayer<ReaperPlayer>().SoulsUpgrades[12] && Reaper.ReaperMode;
        public bool CanShowItemDropInUI() => true;
        public string GetConditionDescription() => null;
    }
    public class DestroyerReaperSoulRule : IItemDropRuleCondition, IProvideItemConditionDescription
    {
        public bool CanDrop(DropAttemptInfo info) => !Main.LocalPlayer.GetModPlayer<ReaperPlayer>().SoulsUpgrades[11] && Reaper.ReaperMode;
        public bool CanShowItemDropInUI() => true;
        public string GetConditionDescription() => null;
    }
    public class PlanteraReaperSoulRule : IItemDropRuleCondition, IProvideItemConditionDescription
    {
        public bool CanDrop(DropAttemptInfo info) => !Main.LocalPlayer.GetModPlayer<ReaperPlayer>().SoulsUpgrades[13] && Reaper.ReaperMode;
        public bool CanShowItemDropInUI() => true;
        public string GetConditionDescription() => null;
    }
    public class EmpressOfLightReaperSoulRule : IItemDropRuleCondition, IProvideItemConditionDescription
    {
        public bool CanDrop(DropAttemptInfo info) => !Main.LocalPlayer.GetModPlayer<ReaperPlayer>().SoulsUpgrades[14] && Reaper.ReaperMode;
        public bool CanShowItemDropInUI() => true;
        public string GetConditionDescription() => null;
    }
    public class InfernalTyrantReaperSoulRule : IItemDropRuleCondition, IProvideItemConditionDescription
    {
        public bool CanDrop(DropAttemptInfo info) => !Main.LocalPlayer.GetModPlayer<ReaperPlayer>().SoulsUpgrades[15] && Reaper.ReaperMode;
        public bool CanShowItemDropInUI() => true;
        public string GetConditionDescription() => null;
    }
    public class GolemReaperSoulRule : IItemDropRuleCondition, IProvideItemConditionDescription
    {
        public bool CanDrop(DropAttemptInfo info) => !Main.LocalPlayer.GetModPlayer<ReaperPlayer>().SoulsUpgrades[16] && Reaper.ReaperMode;
        public bool CanShowItemDropInUI() => true;
        public string GetConditionDescription() => null;
    }
    public class DukeFishronReaperSoulRule : IItemDropRuleCondition, IProvideItemConditionDescription
    {
        public bool CanDrop(DropAttemptInfo info) => !Main.LocalPlayer.GetModPlayer<ReaperPlayer>().SoulsUpgrades[18] && Reaper.ReaperMode;
        public bool CanShowItemDropInUI() => true;
        public string GetConditionDescription() => null;
    }
    public class LunaticCultistReaperSoulRule : IItemDropRuleCondition, IProvideItemConditionDescription
    {
        public bool CanDrop(DropAttemptInfo info) => !Main.LocalPlayer.GetModPlayer<ReaperPlayer>().SoulsUpgrades[19] && Reaper.ReaperMode;
        public bool CanShowItemDropInUI() => true;
        public string GetConditionDescription() => null;
    }
    public class MoonLordReaperSoulRule : IItemDropRuleCondition, IProvideItemConditionDescription
    {
        public bool CanDrop(DropAttemptInfo info) => !Main.LocalPlayer.GetModPlayer<ReaperPlayer>().SoulsUpgrades[20] && Reaper.ReaperMode;
        public bool CanShowItemDropInUI() => true;
        public string GetConditionDescription() => null;
    }
    public class IsReaperModeRule : IItemDropRuleCondition, IProvideItemConditionDescription
    {
        public bool CanDrop(DropAttemptInfo info) => Reaper.ReaperMode;
        public bool CanShowItemDropInUI() => true;
        public string GetConditionDescription() => null;
    }
    public class IsJurneyRule : IItemDropRuleCondition, IProvideItemConditionDescription
    {
        public bool CanDrop(DropAttemptInfo info) => Main.masterMode;
        public bool CanShowItemDropInUI() => true;
        public string GetConditionDescription() => null;
    }
    public class IsNormalRule : IItemDropRuleCondition, IProvideItemConditionDescription
    {
        public bool CanDrop(DropAttemptInfo info) => !Main.expertMode && !Main.masterMode;
        public bool CanShowItemDropInUI() => true;
        public string GetConditionDescription() => null;
    }
    public class IsExpertRule : IItemDropRuleCondition, IProvideItemConditionDescription
    {
        public bool CanDrop(DropAttemptInfo info) => Main.expertMode;
        public bool CanShowItemDropInUI() => true;
        public string GetConditionDescription() => null;
    }
    public class IsMasterRule : IItemDropRuleCondition, IProvideItemConditionDescription
    {
        public bool CanDrop(DropAttemptInfo info) => Main.masterMode;
        public bool CanShowItemDropInUI() => true;
        public string GetConditionDescription() => null;
    }
    public class IsLegendayRule : IItemDropRuleCondition, IProvideItemConditionDescription
    {
        public bool CanDrop(DropAttemptInfo info) => Main.expertMode;
        public bool CanShowItemDropInUI() => true;
        public string GetConditionDescription() => null;
    }
    public class IsHardModeRule : IItemDropRuleCondition, IProvideItemConditionDescription
    {
        public bool CanDrop(DropAttemptInfo info) => Main.hardMode;
        public bool CanShowItemDropInUI() => true;
        public string GetConditionDescription() => null;
    }
}

