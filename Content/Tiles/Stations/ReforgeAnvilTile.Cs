using Microsoft.Xna.Framework;
using RemnantOfTheAncientsMod.Common.UI.AdvanceReforgeUI;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace RemnantOfTheAncientsMod.Content.Tiles.Stations
{
	public class ReforgeAnvilTile : ModTile
	{
		public override void SetStaticDefaults()
		{
			Main.tileFrameImportant[Type] = true;
			Main.tileNoAttach[Type] = true;
			Main.tileLavaDeath[Type] = true;
			TileObjectData.newTile.CopyFrom(TileObjectData.Style3x2);
			TileObjectData.newTile.CoordinateHeights = [16, 16, 16];
			TileObjectData.newTile.StyleHorizontal = true;
			TileObjectData.newTile.StyleWrapLimit = 111;
			TileObjectData.addTile(Type);
			LocalizedText name = CreateMapEntryName();

            VanillaFallbackOnModDeletion = TileID.Anvils;
            AddMapEntry(Color.Gray, name);
		}

		public override bool RightClick(int i, int j)
		{
			var uiSystem = ModContent.GetInstance<AdvanceReforgeUISystem>();
			if (uiSystem.IsVisible())
			{
                Main.playerInventory = false;
                uiSystem.HideMyUI();
			}
			else
			{
				Main.playerInventory = true;
				uiSystem.ShowMyUI();
			}


			return true;
		}
	}
}