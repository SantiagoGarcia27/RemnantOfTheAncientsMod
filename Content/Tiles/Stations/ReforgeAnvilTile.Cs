using Microsoft.Xna.Framework;
using RemnantOfTheAncientsMod.Common.UI.AdvanceReforgeUI;
using Terraria;
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
			//TileObjectData.newTile.Height = 3;
			//TileObjectData.newTile.Width = 4;
			TileObjectData.newTile.CoordinateHeights = new int[] { 16, 16, 16 };
			TileObjectData.newTile.StyleHorizontal = true;
			TileObjectData.newTile.StyleWrapLimit = 111;
			TileObjectData.addTile(Type);
			//dustType = -1;
			//disableSmartCursor = true;
			LocalizedText name = CreateMapEntryName();
			//name.SetDefault("Table");
			AddMapEntry(new Color(190, 124, 43), name);//13,88,130
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

		//public override void KillMultiTile(int i, int j, int frameX, int frameY)
		//{
		//	Item.NewItem(new EntitySource_TileBreak(i, j), i * 16, j * 16, 32, 16, ItemType<AncientT>());
		//}
	}
}