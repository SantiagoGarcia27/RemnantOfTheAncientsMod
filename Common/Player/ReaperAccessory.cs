using RemnantOfTheAncientsMod.Content.Items.Accesories;
using RemnantOfTheAncientsMod.World;
using Terraria;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod
{
	internal class ReaperAccessory : ModAccessorySlot
	{

		public bool inUse = false;
		public override string FunctionalBackgroundTexture => "RemnantOfTheAncientsMod/Common/Player/ReaperAccessory";
		public override string FunctionalTexture => "RemnantOfTheAncientsMod/Content/Items/Accesories/ReaperChalice";

		public override bool CanAcceptItem(Item checkItem, AccessorySlotType context)
		{
			if (checkItem.type == ModContent.ItemType<ReaperChalice>())
			{
				inUse = true;
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
		public override bool IsEnabled()
		{
			if (Reaper.ReaperMode) return true;
			else return false;
		}

	}
}
