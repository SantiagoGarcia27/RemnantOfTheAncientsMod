using RemnantOfTheAncientsMod.Common.ModCompativilitie;
using RemnantOfTheAncientsMod.Common.UtilsTweaks;
using RemnantOfTheAncientsMod.Content.Items.Consumables.ReaperSouls;
using RemnantOfTheAncientsMod.Content.NPCs.Bosses.DAniquilator;
using RemnantOfTheAncientsMod.Content.NPCs.Bosses.FrozenAssaulter;
using RemnantOfTheAncientsMod.Content.NPCs.Bosses.ITyrant;
using RemnantOfTheAncientsMod.World;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Common.Drops.DropRules
{

    public class RemnantConditions
    { 
        
        public class SlimeReaperSoulRule : IItemDropRuleCondition, IProvideItemConditionDescription
        {
            public bool CanDrop(DropAttemptInfo info) => !Main.LocalPlayer.GetModPlayer<ReaperPlayer>().SoulsUpgrades[NPCID.KingSlime] && Reaper.ReaperMode;
            public bool CanShowItemDropInUI() => Reaper.ReaperMode;
            public string GetConditionDescription() => null;
        }
        public class EyeOfChutuluReaperSoulRule : IItemDropRuleCondition, IProvideItemConditionDescription
        {
            public bool CanDrop(DropAttemptInfo info) => !Main.LocalPlayer.GetModPlayer<ReaperPlayer>().SoulsUpgrades[NPCID.EyeofCthulhu] && Reaper.ReaperMode;
            public bool CanShowItemDropInUI() => Reaper.ReaperMode;
            public string GetConditionDescription() => null;
        }
        public class CorruptReaperSoulRule : IItemDropRuleCondition, IProvideItemConditionDescription
        {

            public bool CanDrop(DropAttemptInfo info) => !Main.LocalPlayer.GetModPlayer<ReaperPlayer>().SoulsUpgrades[NPCID.EyeofCthulhu] && Reaper.ReaperMode && Utils1.CanDropCorruptBoss(info.npc);         
            public bool CanShowItemDropInUI() => Reaper.ReaperMode;
            public string GetConditionDescription() => null;
        }
        public class QueenBeeReaperSoulRule : IItemDropRuleCondition, IProvideItemConditionDescription
        {
            public bool CanDrop(DropAttemptInfo info) => !Main.LocalPlayer.GetModPlayer<ReaperPlayer>().SoulsUpgrades[NPCID.QueenBee] && Reaper.ReaperMode;
            public bool CanShowItemDropInUI() => Reaper.ReaperMode;
            public string GetConditionDescription() => null;
        }
        public class SkeletronReaperSoulRule : IItemDropRuleCondition, IProvideItemConditionDescription
        {
            public bool CanDrop(DropAttemptInfo info) => !Main.LocalPlayer.GetModPlayer<ReaperPlayer>().SoulsUpgrades[NPCID.Skeleton] && Reaper.ReaperMode;
            public bool CanShowItemDropInUI() => Reaper.ReaperMode;
            public string GetConditionDescription() => null;
        }
        public class DeerclopsReaperSoulRule : IItemDropRuleCondition, IProvideItemConditionDescription
        {
            public bool CanDrop(DropAttemptInfo info) => !Main.LocalPlayer.GetModPlayer<ReaperPlayer>().SoulsUpgrades[NPCID.Deerclops] && Reaper.ReaperMode;
            public bool CanShowItemDropInUI() => Reaper.ReaperMode;
            public string GetConditionDescription() => null;
        }
        public class DesertAnhilatorReaperSoulRule : IItemDropRuleCondition, IProvideItemConditionDescription
        {
            public bool CanDrop(DropAttemptInfo info) => !Main.LocalPlayer.GetModPlayer<ReaperPlayer>().SoulsUpgrades[ModContent.NPCType<DesertAniquilator>()] && Reaper.ReaperMode;
            public bool CanShowItemDropInUI() => Reaper.ReaperMode;
            public string GetConditionDescription() => null;
        }
        public class WallOfFLeshReaperSoulRule : IItemDropRuleCondition, IProvideItemConditionDescription
        {
            public bool CanDrop(DropAttemptInfo info) => !Main.LocalPlayer.GetModPlayer<ReaperPlayer>().SoulsUpgrades[NPCID.WallofFlesh] && Reaper.ReaperMode;
            public bool CanShowItemDropInUI() => Reaper.ReaperMode;
            public string GetConditionDescription() => null;
        }
        public class FrozenAssaulterReaperSoulRule : IItemDropRuleCondition, IProvideItemConditionDescription
        {
            public bool CanDrop(DropAttemptInfo info) => !Main.LocalPlayer.GetModPlayer<ReaperPlayer>().SoulsUpgrades[ModContent.NPCType<FrozenAssaulter>()] && Reaper.ReaperMode;
            public bool CanShowItemDropInUI() => Reaper.ReaperMode;
            public string GetConditionDescription() => null;
        }
        public class QueenSlimeReaperSoulRule : IItemDropRuleCondition, IProvideItemConditionDescription
        {
            public bool CanDrop(DropAttemptInfo info) => !Main.LocalPlayer.GetModPlayer<ReaperPlayer>().SoulsUpgrades[NPCID.QueenSlimeBoss] && Reaper.ReaperMode;
            public bool CanShowItemDropInUI() => Reaper.ReaperMode;
            public string GetConditionDescription() => null;
        }
        public class RetinazorReaperSoulRule : IItemDropRuleCondition, IProvideItemConditionDescription
        {
            public bool CanDrop(DropAttemptInfo info) => !Main.LocalPlayer.GetModPlayer<ReaperPlayer>().SoulsUpgrades[NPCID.Retinazer] && Reaper.ReaperMode;
            public bool CanShowItemDropInUI() => Reaper.ReaperMode;
            public string GetConditionDescription() => null;
        }
        public class SpazmatismReaperSoulRule : IItemDropRuleCondition, IProvideItemConditionDescription
        {
            public bool CanDrop(DropAttemptInfo info) => !Main.LocalPlayer.GetModPlayer<ReaperPlayer>().SoulsUpgrades[NPCID.Spazmatism] && Reaper.ReaperMode;
            public bool CanShowItemDropInUI() => Reaper.ReaperMode;
            public string GetConditionDescription() => null;
        }
        public class SkeletronPrimeReaperSoulRule : IItemDropRuleCondition, IProvideItemConditionDescription
        {
            public bool CanDrop(DropAttemptInfo info) => !Main.LocalPlayer.GetModPlayer<ReaperPlayer>().SoulsUpgrades[NPCID.SkeletronPrime] && Reaper.ReaperMode;
            public bool CanShowItemDropInUI() => Reaper.ReaperMode;
            public string GetConditionDescription() => null;
        }
        public class DestroyerReaperSoulRule : IItemDropRuleCondition, IProvideItemConditionDescription
        {
            public bool CanDrop(DropAttemptInfo info) => !Main.LocalPlayer.GetModPlayer<ReaperPlayer>().SoulsUpgrades[NPCID.TheDestroyer] && Reaper.ReaperMode;
            public bool CanShowItemDropInUI() => Reaper.ReaperMode;
            public string GetConditionDescription() => null;
        }
        public class PlanteraReaperSoulRule : IItemDropRuleCondition, IProvideItemConditionDescription
        {
            public bool CanDrop(DropAttemptInfo info) => !Main.LocalPlayer.GetModPlayer<ReaperPlayer>().SoulsUpgrades[NPCID.Plantera] && Reaper.ReaperMode;
            public bool CanShowItemDropInUI() => Reaper.ReaperMode;
            public string GetConditionDescription() => null;
        }
        public class EmpressOfLightReaperSoulRule : IItemDropRuleCondition, IProvideItemConditionDescription
        {
            public bool CanDrop(DropAttemptInfo info) => !Main.LocalPlayer.GetModPlayer<ReaperPlayer>().SoulsUpgrades[NPCID.HallowBoss] && Reaper.ReaperMode;
            public bool CanShowItemDropInUI() => Reaper.ReaperMode;
            public string GetConditionDescription() => null;
        }
        public class InfernalTyrantReaperSoulRule : IItemDropRuleCondition, IProvideItemConditionDescription
        {
            public bool CanDrop(DropAttemptInfo info) => !Main.LocalPlayer.GetModPlayer<ReaperPlayer>().SoulsUpgrades[ModContent.NPCType<InfernalTyrantHead>()] && Reaper.ReaperMode;
            public bool CanShowItemDropInUI() => Reaper.ReaperMode;
            public string GetConditionDescription() => null;
        }
        public class GolemReaperSoulRule : IItemDropRuleCondition, IProvideItemConditionDescription
        {
            public bool CanDrop(DropAttemptInfo info) => !Main.LocalPlayer.GetModPlayer<ReaperPlayer>().SoulsUpgrades[NPCID.Golem] && Reaper.ReaperMode;
            public bool CanShowItemDropInUI() => Reaper.ReaperMode;
            public string GetConditionDescription() => null;
        }
        public class DukeFishronReaperSoulRule : IItemDropRuleCondition, IProvideItemConditionDescription
        {
            public bool CanDrop(DropAttemptInfo info) => !Main.LocalPlayer.GetModPlayer<ReaperPlayer>().SoulsUpgrades[NPCID.DukeFishron] && Reaper.ReaperMode;
            public bool CanShowItemDropInUI() => Reaper.ReaperMode;
            public string GetConditionDescription() => null;
        }
        public class LunaticCultistReaperSoulRule : IItemDropRuleCondition, IProvideItemConditionDescription
        {
            public bool CanDrop(DropAttemptInfo info) => !Main.LocalPlayer.GetModPlayer<ReaperPlayer>().SoulsUpgrades[NPCID.CultistBoss] && Reaper.ReaperMode;
            public bool CanShowItemDropInUI() => Reaper.ReaperMode;
            public string GetConditionDescription() => null;
        }
        public class MoonLordReaperSoulRule : IItemDropRuleCondition, IProvideItemConditionDescription
        {
            public bool CanDrop(DropAttemptInfo info) => !Main.LocalPlayer.GetModPlayer<ReaperPlayer>().SoulsUpgrades[NPCID.MoonLordCore] && Reaper.ReaperMode;
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

