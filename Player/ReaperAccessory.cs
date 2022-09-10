using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using RemnantOfTheAncientsMod.VanillaChanges;
using RemnantOfTheAncientsMod.Items.Fmode;
using Microsoft.Xna.Framework;

namespace RemnantOfTheAncientsMod
{
	internal class ReaperAccessory : ModAccessorySlot

	{
		public bool inUse=false;
		public override string FunctionalBackgroundTexture => "RemnantOfTheAncientsMod/Player/ReaperAccessory";

		public override bool CanAcceptItem(Item checkItem, AccessorySlotType context)
		{
			if (ModContent.ItemType<ReaperChalice>() > 0) {
				inUse=true;
				return true;
			}
			return false;
		}
		

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
