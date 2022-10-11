using RemnantOfTheAncientsMod.Items.Fmode;
using Terraria;
using Terraria.GameContent.ItemDropRules;

namespace RemnantOfTheAncientsMod.Drops
{
    internal class DropRules1
    {


    }
    public class SlimeReaperSoulRule : IItemDropRuleCondition, IProvideItemConditionDescription
    {
        public SlimeReaperSoulRule() { }
        public bool CanDrop(DropAttemptInfo info)
        {
            if(!Main.LocalPlayer.GetModPlayer<SlimeReaperSoulPlayer>().SlimeReaperUpgrade) return true;
            return false;
        }
        public bool CanShowItemDropInUI() => true;
        public string GetConditionDescription() => null;
    }
}
