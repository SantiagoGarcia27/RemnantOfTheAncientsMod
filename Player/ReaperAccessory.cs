using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using RemnantOfTheAncientsMod.VanillaChanges;
using Microsoft.Xna.Framework;

namespace RemnantOfTheAncientsMod
{
    internal class ReaperAccessory : ModAccessorySlot

    {
        public override string FunctionalBackgroundTexture => "RemnantOfTheAncientsMod/Player/ReaperAccessory";
        public override string FunctionalTexture => "";
        public override bool CanAcceptItem(Item checkItem, AccessorySlotType context) => CustomRarity.Reaper > 0;


        public override void OnMouseHover(AccessorySlotType context)
        {
            switch (context)
            {
                case AccessorySlotType.FunctionalSlot:
                case AccessorySlotType.VanitySlot:
                    Main.hoverItemName = "Reaper Chalice";
                    break;
                case AccessorySlotType.DyeSlot:
                    Main.hoverItemName = "Reaper Chalice Dye";
                    break;
            }
        }
    }
}  


