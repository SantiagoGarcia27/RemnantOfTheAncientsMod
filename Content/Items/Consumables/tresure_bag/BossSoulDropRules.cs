using RemnantOfTheAncientsMod.Common.ReaperSystem;
using RemnantOfTheAncientsMod.Content.NPCs.Bosses.DesertAnnihilator;
using RemnantOfTheAncientsMod.Content.NPCs.Bosses.FrozenAssaulter;
using RemnantOfTheAncientsMod.Content.NPCs.Bosses.GemstoneCrusher;
using RemnantOfTheAncientsMod.Content.NPCs.Bosses.ITyrant;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using static Terraria.ModLoader.ModContent;

namespace RemnantOfTheAncientsMod.Content.Items.Consumables.tresure_bag
{
    public class CrusherReaperSoul : IItemDropRuleCondition, IProvideItemConditionDescription
    {
        public bool CanDrop(DropAttemptInfo info) => !Main.LocalPlayer.GetModPlayer<ReaperSoulsPlayer>().SoulsUpgradesLoaded[ReaperSoulsPlayer.GetIndexFromLoadedBossById(NPCType<GemstoneCrusher>())] && Reaper.ReaperMode;
        public bool CanShowItemDropInUI() => true;
        public string GetConditionDescription() => null;
    }
    public class DesertReaperSoul : IItemDropRuleCondition, IProvideItemConditionDescription
    {
        public bool CanDrop(DropAttemptInfo info) => !Main.LocalPlayer.GetModPlayer<ReaperSoulsPlayer>().SoulsUpgradesLoaded[ReaperSoulsPlayer.GetIndexFromLoadedBossById(NPCType<DesertAnnihilator>())] && Reaper.ReaperMode;
        public bool CanShowItemDropInUI() => true;
        public string GetConditionDescription() => null;
    }
    public class FrozenReaperSoul : IItemDropRuleCondition, IProvideItemConditionDescription
    {
        public bool CanDrop(DropAttemptInfo info) => !Main.LocalPlayer.GetModPlayer<ReaperSoulsPlayer>().SoulsUpgradesLoaded[ReaperSoulsPlayer.GetIndexFromLoadedBossById(NPCType<FrozenAssaulter>())] && Reaper.ReaperMode;
        public bool CanShowItemDropInUI() => true;
        public string GetConditionDescription() => null;
    }
    public class InfernalReaperSoul : IItemDropRuleCondition, IProvideItemConditionDescription
    {
        public bool CanDrop(DropAttemptInfo info) => !Main.LocalPlayer.GetModPlayer<ReaperSoulsPlayer>().SoulsUpgradesLoaded[ReaperSoulsPlayer.GetIndexFromLoadedBossById(NPCType<InfernalTyrantHead>())] && Reaper.ReaperMode;
        public bool CanShowItemDropInUI() => true;
        public string GetConditionDescription() => null;
    }
}