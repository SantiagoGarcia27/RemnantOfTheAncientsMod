using RemnantOfTheAncientsMod.Items.Fmode;
using Terraria;
using Terraria.GameContent.ItemDropRules;

namespace RemnantOfTheAncientsMod.Drops
{
    public class DropRules1 : ItemDropRule
    {



        /* public class SlimeReaperSoulRule : IItemDropRuleCondition, IProvideItemConditionDescription
         {
             public SlimeReaperSoulRule() { }
             public bool CanDrop(DropAttemptInfo info)
             {
                 if(!Main.LocalPlayer.GetModPlayer<SlimeReaperSoulPlayer>().SlimeReaperUpgrade) return true;
                 return false;
             }
             public bool CanShowItemDropInUI() => true;
             public string GetConditionDescription() => null;
         }*/

        public class SlimeReaperSoulRule : IItemDropRuleCondition, IProvideItemConditionDescription
        {
            public bool CanDrop(DropAttemptInfo info) => !Main.LocalPlayer.GetModPlayer<SlimeReaperSoulPlayer>().SlimeReaperUpgrade;
            public bool CanShowItemDropInUI() => true;
            public string GetConditionDescription() => null;
        }
    }
}
