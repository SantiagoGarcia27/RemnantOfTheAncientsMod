using RemnantOfTheAncientsMod.Common.ModCompativilitie;
using RemnantOfTheAncientsMod.Common.UtilsTweaks;
using RemnantOfTheAncientsMod.Content.Items.Consumables.ReaperSouls;
using RemnantOfTheAncientsMod.World;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.Localization;

namespace RemnantOfTheAncientsMod.Common.Drops.DropRules
{

    public class RemnantConditions
    { 
        
        public class SlimeReaperSoulRule : IItemDropRuleCondition, IProvideItemConditionDescription
        {
            public bool CanDrop(DropAttemptInfo info) => !Main.LocalPlayer.GetModPlayer<ReaperPlayer>().SoulsUpgrades[ModID.RemnantOfTheAncients][BossID.VanillaID.KingSlime] && Reaper.ReaperMode;
            public bool CanShowItemDropInUI() => Reaper.ReaperMode;
            public string GetConditionDescription() => null;
        }
        public class EyeOfChutuluReaperSoulRule : IItemDropRuleCondition, IProvideItemConditionDescription
        {
            public bool CanDrop(DropAttemptInfo info) => !Main.LocalPlayer.GetModPlayer<ReaperPlayer>().SoulsUpgrades[ModID.RemnantOfTheAncients][BossID.VanillaID.EyeOfChutulu] && Reaper.ReaperMode;
            public bool CanShowItemDropInUI() => Reaper.ReaperMode;
            public string GetConditionDescription() => null;
        }
        public class CorruptReaperSoulRule : IItemDropRuleCondition, IProvideItemConditionDescription
        {
            public bool CanDrop(DropAttemptInfo info) => !info.player.GetModPlayer<ReaperPlayer>().SoulsUpgrades[ModID.RemnantOfTheAncients][BossID.VanillaID.CorruptBoss] && Reaper.ReaperMode && Utils1.CanDropCorruptBoss(info.npc);
            public bool CanShowItemDropInUI() => Reaper.ReaperMode;
            public string GetConditionDescription() => null;
        }
        public class QueenBeeReaperSoulRule : IItemDropRuleCondition, IProvideItemConditionDescription
        {
            public bool CanDrop(DropAttemptInfo info) => !Main.LocalPlayer.GetModPlayer<ReaperPlayer>().SoulsUpgrades[ModID.RemnantOfTheAncients][BossID.VanillaID.QueenBee] && Reaper.ReaperMode;
            public bool CanShowItemDropInUI() => Reaper.ReaperMode;
            public string GetConditionDescription() => null;
        }
        public class SkeletronReaperSoulRule : IItemDropRuleCondition, IProvideItemConditionDescription
        {
            public bool CanDrop(DropAttemptInfo info) => !Main.LocalPlayer.GetModPlayer<ReaperPlayer>().SoulsUpgrades[ModID.RemnantOfTheAncients][BossID.VanillaID.Skeletron] && Reaper.ReaperMode;
            public bool CanShowItemDropInUI() => Reaper.ReaperMode;
            public string GetConditionDescription() => null;
        }
        public class DeerclopsReaperSoulRule : IItemDropRuleCondition, IProvideItemConditionDescription
        {
            public bool CanDrop(DropAttemptInfo info) => !Main.LocalPlayer.GetModPlayer<ReaperPlayer>().SoulsUpgrades[ModID.RemnantOfTheAncients][BossID.VanillaID.Deerclops] && Reaper.ReaperMode;
            public bool CanShowItemDropInUI() => Reaper.ReaperMode;
            public string GetConditionDescription() => null;
        }
        public class DesertAnhilatorReaperSoulRule : IItemDropRuleCondition, IProvideItemConditionDescription
        {
            public bool CanDrop(DropAttemptInfo info) => !Main.LocalPlayer.GetModPlayer<ReaperPlayer>().SoulsUpgrades[ModID.RemnantOfTheAncients][BossID.VanillaID.DesertAnhilator] && Reaper.ReaperMode;
            public bool CanShowItemDropInUI() => Reaper.ReaperMode;
            public string GetConditionDescription() => null;
        }
        public class WallOfFLeshReaperSoulRule : IItemDropRuleCondition, IProvideItemConditionDescription
        {
            public bool CanDrop(DropAttemptInfo info) => !Main.LocalPlayer.GetModPlayer<ReaperPlayer>().SoulsUpgrades[ModID.RemnantOfTheAncients][BossID.VanillaID.WallOfFlesh] && Reaper.ReaperMode;
            public bool CanShowItemDropInUI() => Reaper.ReaperMode;
            public string GetConditionDescription() => null;
        }
        public class FrozenAssaulterReaperSoulRule : IItemDropRuleCondition, IProvideItemConditionDescription
        {
            public bool CanDrop(DropAttemptInfo info) => !Main.LocalPlayer.GetModPlayer<ReaperPlayer>().SoulsUpgrades[ModID.RemnantOfTheAncients][BossID.VanillaID.FrozenAssaulter] && Reaper.ReaperMode;
            public bool CanShowItemDropInUI() => Reaper.ReaperMode;
            public string GetConditionDescription() => null;
        }
        public class QueenSlimeReaperSoulRule : IItemDropRuleCondition, IProvideItemConditionDescription
        {
            public bool CanDrop(DropAttemptInfo info) => !Main.LocalPlayer.GetModPlayer<ReaperPlayer>().SoulsUpgrades[ModID.RemnantOfTheAncients][BossID.VanillaID.QueenSlime] && Reaper.ReaperMode;
            public bool CanShowItemDropInUI() => Reaper.ReaperMode;
            public string GetConditionDescription() => null;
        }
        public class RetinazorReaperSoulRule : IItemDropRuleCondition, IProvideItemConditionDescription
        {
            public bool CanDrop(DropAttemptInfo info) => !Main.LocalPlayer.GetModPlayer<ReaperPlayer>().SoulsUpgrades[ModID.RemnantOfTheAncients][BossID.VanillaID.Retinazor] && Reaper.ReaperMode;
            public bool CanShowItemDropInUI() => Reaper.ReaperMode;
            public string GetConditionDescription() => null;
        }
        public class SpazmatismReaperSoulRule : IItemDropRuleCondition, IProvideItemConditionDescription
        {
            public bool CanDrop(DropAttemptInfo info) => !Main.LocalPlayer.GetModPlayer<ReaperPlayer>().SoulsUpgrades[ModID.RemnantOfTheAncients][BossID.VanillaID.Spazmatism] && Reaper.ReaperMode;
            public bool CanShowItemDropInUI() => Reaper.ReaperMode;
            public string GetConditionDescription() => null;
        }
        public class SkeletronPrimeReaperSoulRule : IItemDropRuleCondition, IProvideItemConditionDescription
        {
            public bool CanDrop(DropAttemptInfo info) => !Main.LocalPlayer.GetModPlayer<ReaperPlayer>().SoulsUpgrades[ModID.RemnantOfTheAncients][BossID.VanillaID.SkeletronPrime] && Reaper.ReaperMode;
            public bool CanShowItemDropInUI() => Reaper.ReaperMode;
            public string GetConditionDescription() => null;
        }
        public class DestroyerReaperSoulRule : IItemDropRuleCondition, IProvideItemConditionDescription
        {
            public bool CanDrop(DropAttemptInfo info) => !Main.LocalPlayer.GetModPlayer<ReaperPlayer>().SoulsUpgrades[ModID.RemnantOfTheAncients][BossID.VanillaID.Destroyer] && Reaper.ReaperMode;
            public bool CanShowItemDropInUI() => Reaper.ReaperMode;
            public string GetConditionDescription() => null;
        }
        public class PlanteraReaperSoulRule : IItemDropRuleCondition, IProvideItemConditionDescription
        {
            public bool CanDrop(DropAttemptInfo info) => !Main.LocalPlayer.GetModPlayer<ReaperPlayer>().SoulsUpgrades[ModID.RemnantOfTheAncients][BossID.VanillaID.Plantera] && Reaper.ReaperMode;
            public bool CanShowItemDropInUI() => Reaper.ReaperMode;
            public string GetConditionDescription() => null;
        }
        public class EmpressOfLightReaperSoulRule : IItemDropRuleCondition, IProvideItemConditionDescription
        {
            public bool CanDrop(DropAttemptInfo info) => !Main.LocalPlayer.GetModPlayer<ReaperPlayer>().SoulsUpgrades[ModID.RemnantOfTheAncients][BossID.VanillaID.EmpressOfLight] && Reaper.ReaperMode;
            public bool CanShowItemDropInUI() => Reaper.ReaperMode;
            public string GetConditionDescription() => null;
        }
        public class InfernalTyrantReaperSoulRule : IItemDropRuleCondition, IProvideItemConditionDescription
        {
            public bool CanDrop(DropAttemptInfo info) => !Main.LocalPlayer.GetModPlayer<ReaperPlayer>().SoulsUpgrades[ModID.RemnantOfTheAncients][BossID.VanillaID.InfernalTyrant] && Reaper.ReaperMode;
            public bool CanShowItemDropInUI() => Reaper.ReaperMode;
            public string GetConditionDescription() => null;
        }
        public class GolemReaperSoulRule : IItemDropRuleCondition, IProvideItemConditionDescription
        {
            public bool CanDrop(DropAttemptInfo info) => !Main.LocalPlayer.GetModPlayer<ReaperPlayer>().SoulsUpgrades[ModID.RemnantOfTheAncients][BossID.VanillaID.Golem] && Reaper.ReaperMode;
            public bool CanShowItemDropInUI() => Reaper.ReaperMode;
            public string GetConditionDescription() => null;
        }
        public class DukeFishronReaperSoulRule : IItemDropRuleCondition, IProvideItemConditionDescription
        {
            public bool CanDrop(DropAttemptInfo info) => !Main.LocalPlayer.GetModPlayer<ReaperPlayer>().SoulsUpgrades[ModID.RemnantOfTheAncients][BossID.VanillaID.DukeFishron] && Reaper.ReaperMode;
            public bool CanShowItemDropInUI() => Reaper.ReaperMode;
            public string GetConditionDescription() => null;
        }
        public class LunaticCultistReaperSoulRule : IItemDropRuleCondition, IProvideItemConditionDescription
        {
            public bool CanDrop(DropAttemptInfo info) => !Main.LocalPlayer.GetModPlayer<ReaperPlayer>().SoulsUpgrades[ModID.RemnantOfTheAncients][BossID.VanillaID.Cultist] && Reaper.ReaperMode;
            public bool CanShowItemDropInUI() => Reaper.ReaperMode;
            public string GetConditionDescription() => null;
        }
        public class MoonLordReaperSoulRule : IItemDropRuleCondition, IProvideItemConditionDescription
        {
            public bool CanDrop(DropAttemptInfo info) => !Main.LocalPlayer.GetModPlayer<ReaperPlayer>().SoulsUpgrades[ModID.RemnantOfTheAncients][BossID.VanillaID.MoonLord] && Reaper.ReaperMode;
            public bool CanShowItemDropInUI() => Reaper.ReaperMode;
            public string GetConditionDescription() => null;
        }
        public class DownedPlantera : IItemDropRuleCondition, IProvideItemConditionDescription
        {
            public bool CanDrop(DropAttemptInfo info) => NPC.downedPlantBoss;
            public bool CanShowItemDropInUI() => NPC.downedPlantBoss;
            public string GetConditionDescription() => null;
        }


        public class IsReaperMode : IItemDropRuleCondition, IProvideItemConditionDescription
        {
            public bool CanDrop(DropAttemptInfo info) => Reaper.ReaperMode;          
            public bool CanShowItemDropInUI() => Reaper.ReaperMode;
            public string GetConditionDescription() => null; 
        }
        public class IsNotReaperMode : IItemDropRuleCondition, IProvideItemConditionDescription
        {
            public bool CanDrop(DropAttemptInfo info) => !Reaper.ReaperMode;
            public bool CanShowItemDropInUI() => !Reaper.ReaperMode;
            public string GetConditionDescription() => null;
        }
        public class IsJurneyRule : IItemDropRuleCondition, IProvideItemConditionDescription
        {
            public bool CanDrop(DropAttemptInfo info) => false;
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
            public bool CanDrop(DropAttemptInfo info) => Main.masterMode && Main.getGoodWorld;
            public bool CanShowItemDropInUI() => true;
            public string GetConditionDescription() => null;
        }
        public class IsHardModeRule : IItemDropRuleCondition, IProvideItemConditionDescription
        {
            public bool CanDrop(DropAttemptInfo info) => Main.hardMode;
            public bool CanShowItemDropInUI() => true;
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

        public class IsEternity : IItemDropRuleCondition, IProvideItemConditionDescription
        {
            public bool CanDrop(DropAttemptInfo info) => SangarUtilities.Common.DificultyUtils.EternityMode || SangarUtilities.Common.DificultyUtils.MasochistMode;
            public bool CanShowItemDropInUI() => SangarUtilities.Common.DificultyUtils.EternityMode || SangarUtilities.Common.DificultyUtils.MasochistMode;
            public string GetConditionDescription() => null;
        }
        public class IsOnlyEternity : IItemDropRuleCondition, IProvideItemConditionDescription
        {
            public bool CanDrop(DropAttemptInfo info) => SangarUtilities.Common.DificultyUtils.EternityMode;
            public bool CanShowItemDropInUI() => SangarUtilities.Common.DificultyUtils.EternityMode;
            public string GetConditionDescription() => null;
        }
        public class IsMasochist: IItemDropRuleCondition, IProvideItemConditionDescription
        {
            public bool CanDrop(DropAttemptInfo info) => SangarUtilities.Common.DificultyUtils.MasochistMode;
            public bool CanShowItemDropInUI() => SangarUtilities.Common.DificultyUtils.MasochistMode;
            public string GetConditionDescription() => null;
        }
        public class IsInfernum : IItemDropRuleCondition, IProvideItemConditionDescription
        {
            public bool CanDrop(DropAttemptInfo info) =>SangarUtilities.Common.DificultyUtils.InfernumMode;
            public bool CanShowItemDropInUI() =>SangarUtilities.Common.DificultyUtils.InfernumMode;
            public string GetConditionDescription() => null;
        }
    }

        
}

