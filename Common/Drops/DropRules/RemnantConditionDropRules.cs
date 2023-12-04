using RemnantOfTheAncientsMod.Common.ModCompativilitie;
using RemnantOfTheAncientsMod.Common.UtilsTweaks;
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
        public bool CanDrop(DropAttemptInfo info) => !Main.LocalPlayer.GetModPlayer<ReaperPlayer>().SoulsUpgrades[0] && DificultyUtils.ReaperMode;
        public bool CanShowItemDropInUI() => DificultyUtils.ReaperMode;
        public string GetConditionDescription() => null;
    }
    public class EyeOfChutuluReaperSoulRule : IItemDropRuleCondition, IProvideItemConditionDescription
    {
        public bool CanDrop(DropAttemptInfo info) => !Main.LocalPlayer.GetModPlayer<ReaperPlayer>().SoulsUpgrades[1] && DificultyUtils.ReaperMode;
        public bool CanShowItemDropInUI() => DificultyUtils.ReaperMode;
        public string GetConditionDescription() => null;
    }
    public class CorruptReaperSoulRule : IItemDropRuleCondition, IProvideItemConditionDescription
    {
        public bool CanDrop(DropAttemptInfo info) => !info.player.GetModPlayer<ReaperPlayer>().SoulsUpgrades[2] && DificultyUtils.ReaperMode && Utils1.CanDropCorruptBoss(info.npc);
        public bool CanShowItemDropInUI() => DificultyUtils.ReaperMode;
        public string GetConditionDescription() => null;
    }
    public class QueenBeeReaperSoulRule : IItemDropRuleCondition, IProvideItemConditionDescription
    {
        public bool CanDrop(DropAttemptInfo info) => !Main.LocalPlayer.GetModPlayer<ReaperPlayer>().SoulsUpgrades[3] && DificultyUtils.ReaperMode;
        public bool CanShowItemDropInUI() => DificultyUtils.ReaperMode;
        public string GetConditionDescription() => null;
    }
    public class SkeletronReaperSoulRule : IItemDropRuleCondition, IProvideItemConditionDescription
    {
        public bool CanDrop(DropAttemptInfo info) => !Main.LocalPlayer.GetModPlayer<ReaperPlayer>().SoulsUpgrades[4] && DificultyUtils.ReaperMode;
        public bool CanShowItemDropInUI() => DificultyUtils.ReaperMode;
        public string GetConditionDescription() => null;
    }
    public class DeerclopsReaperSoulRule : IItemDropRuleCondition, IProvideItemConditionDescription
    {
        public bool CanDrop(DropAttemptInfo info) => !Main.LocalPlayer.GetModPlayer<ReaperPlayer>().SoulsUpgrades[5] && DificultyUtils.ReaperMode;
        public bool CanShowItemDropInUI() => DificultyUtils.ReaperMode;
        public string GetConditionDescription() => null;
    }
    public class DesertAnhilatorReaperSoulRule : IItemDropRuleCondition, IProvideItemConditionDescription
    {
        public bool CanDrop(DropAttemptInfo info) => !Main.LocalPlayer.GetModPlayer<ReaperPlayer>().SoulsUpgrades[6] && DificultyUtils.ReaperMode;
        public bool CanShowItemDropInUI() => DificultyUtils.ReaperMode;
        public string GetConditionDescription() => null;
    }
    public class WallOfFLeshReaperSoulRule : IItemDropRuleCondition, IProvideItemConditionDescription
    {
        public bool CanDrop(DropAttemptInfo info) => !Main.LocalPlayer.GetModPlayer<ReaperPlayer>().SoulsUpgrades[7] && DificultyUtils.ReaperMode;
        public bool CanShowItemDropInUI() => DificultyUtils.ReaperMode;
        public string GetConditionDescription() => null;
    }
    public class FrozenAssaulterReaperSoulRule : IItemDropRuleCondition, IProvideItemConditionDescription
    {
        public bool CanDrop(DropAttemptInfo info) => !Main.LocalPlayer.GetModPlayer<ReaperPlayer>().SoulsUpgrades[8] && DificultyUtils.ReaperMode;
        public bool CanShowItemDropInUI() => DificultyUtils.ReaperMode;
        public string GetConditionDescription() => null;
    }
    public class QueenSlimeReaperSoulRule : IItemDropRuleCondition, IProvideItemConditionDescription
    {
        public bool CanDrop(DropAttemptInfo info) => !Main.LocalPlayer.GetModPlayer<ReaperPlayer>().SoulsUpgrades[9] && DificultyUtils.ReaperMode;
        public bool CanShowItemDropInUI() => DificultyUtils.ReaperMode;
        public string GetConditionDescription() => null;
    }
    public class RetinazorReaperSoulRule : IItemDropRuleCondition, IProvideItemConditionDescription
    {
        public bool CanDrop(DropAttemptInfo info) => !Main.LocalPlayer.GetModPlayer<ReaperPlayer>().SoulsUpgrades[11] && DificultyUtils.ReaperMode;
        public bool CanShowItemDropInUI() => DificultyUtils.ReaperMode;
        public string GetConditionDescription() => null;
    }
    public class SpazmatismReaperSoulRule : IItemDropRuleCondition, IProvideItemConditionDescription
    {
        public bool CanDrop(DropAttemptInfo info) => !Main.LocalPlayer.GetModPlayer<ReaperPlayer>().SoulsUpgrades[10] && DificultyUtils.ReaperMode;
        public bool CanShowItemDropInUI() => DificultyUtils.ReaperMode;
        public string GetConditionDescription() => null;
    }
    public class SkeletronPrimeReaperSoulRule : IItemDropRuleCondition, IProvideItemConditionDescription
    {
        public bool CanDrop(DropAttemptInfo info) => !Main.LocalPlayer.GetModPlayer<ReaperPlayer>().SoulsUpgrades[12] && DificultyUtils.ReaperMode;
        public bool CanShowItemDropInUI() => DificultyUtils.ReaperMode;
        public string GetConditionDescription() => null;
    }
    public class DestroyerReaperSoulRule : IItemDropRuleCondition, IProvideItemConditionDescription
    {
        public bool CanDrop(DropAttemptInfo info) => !Main.LocalPlayer.GetModPlayer<ReaperPlayer>().SoulsUpgrades[11] && DificultyUtils.ReaperMode;
        public bool CanShowItemDropInUI() => DificultyUtils.ReaperMode;
        public string GetConditionDescription() => null;
    }
    public class PlanteraReaperSoulRule : IItemDropRuleCondition, IProvideItemConditionDescription
    {
        public bool CanDrop(DropAttemptInfo info) => !Main.LocalPlayer.GetModPlayer<ReaperPlayer>().SoulsUpgrades[14] && DificultyUtils.ReaperMode;
        public bool CanShowItemDropInUI() => DificultyUtils.ReaperMode;
        public string GetConditionDescription() => null;
    }
    public class EmpressOfLightReaperSoulRule : IItemDropRuleCondition, IProvideItemConditionDescription
    {
        public bool CanDrop(DropAttemptInfo info) => !Main.LocalPlayer.GetModPlayer<ReaperPlayer>().SoulsUpgrades[17] && DificultyUtils.ReaperMode;
        public bool CanShowItemDropInUI() => DificultyUtils.ReaperMode;
        public string GetConditionDescription() => null;
    }
    public class InfernalTyrantReaperSoulRule : IItemDropRuleCondition, IProvideItemConditionDescription
    {
        public bool CanDrop(DropAttemptInfo info) => !Main.LocalPlayer.GetModPlayer<ReaperPlayer>().SoulsUpgrades[15] && DificultyUtils.ReaperMode;
        public bool CanShowItemDropInUI() => DificultyUtils.ReaperMode;
        public string GetConditionDescription() => null;
    }
    public class GolemReaperSoulRule : IItemDropRuleCondition, IProvideItemConditionDescription
    {
        public bool CanDrop(DropAttemptInfo info) => !Main.LocalPlayer.GetModPlayer<ReaperPlayer>().SoulsUpgrades[16] && DificultyUtils.ReaperMode;
        public bool CanShowItemDropInUI() => DificultyUtils.ReaperMode;
        public string GetConditionDescription() => null;
    }
    public class DukeFishronReaperSoulRule : IItemDropRuleCondition, IProvideItemConditionDescription
    {
        public bool CanDrop(DropAttemptInfo info) => !Main.LocalPlayer.GetModPlayer<ReaperPlayer>().SoulsUpgrades[18] && DificultyUtils.ReaperMode;
        public bool CanShowItemDropInUI() => DificultyUtils.ReaperMode;
        public string GetConditionDescription() => null;
    }
    public class LunaticCultistReaperSoulRule : IItemDropRuleCondition, IProvideItemConditionDescription
    {
        public bool CanDrop(DropAttemptInfo info) => !Main.LocalPlayer.GetModPlayer<ReaperPlayer>().SoulsUpgrades[19] && DificultyUtils.ReaperMode;
        public bool CanShowItemDropInUI() => DificultyUtils.ReaperMode;
        public string GetConditionDescription() => null;
    }
    public class MoonLordReaperSoulRule : IItemDropRuleCondition, IProvideItemConditionDescription
    {
        public bool CanDrop(DropAttemptInfo info) => !Main.LocalPlayer.GetModPlayer<ReaperPlayer>().SoulsUpgrades[20] && DificultyUtils.ReaperMode;
        public bool CanShowItemDropInUI() => DificultyUtils.ReaperMode;
        public string GetConditionDescription() => null;
    }
    public class IsReaperModeRule : IItemDropRuleCondition, IProvideItemConditionDescription
    {
        public bool CanDrop(DropAttemptInfo info) => DificultyUtils.ReaperMode;
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

