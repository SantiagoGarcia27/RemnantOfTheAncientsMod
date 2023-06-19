using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;
using Microsoft.Xna.Framework;
using static Terraria.ModLoader.ModContent;
using Terraria.DataStructures;
using RemnantOfTheAncientsMod.Content.Items.Items;

namespace RemnantOfTheAncientsMod.Content.Tiles
{
	public class TerracoinB : ModTile
	{
		public override void SetStaticDefaults()
		{
			Main.tileShine[Type] = 0;//1100 Default
			Main.tileSolid[Type] = true;
			Main.tileSolidTop[Type] = true;
			Main.tileFrameImportant[Type] = true;
			TileObjectData.newTile.CopyFrom(TileObjectData.Style1x1);
			TileObjectData.newTile.StyleHorizontal = true;
			TileObjectData.newTile.LavaDeath = false;
			TileObjectData.addTile(Type);

			AddMapEntry(new Color(200, 200, 200), Language.GetText("MapObject.MetalBar"));
		}

		public override bool Drop(int i, int j)
		{
			Tile t = Main.tile[i, j];
			int style = t.TileFrameX / 18;
			if (style == 0) // It can be useful to share a single tile with multiple styles. This code will let you drop the appropriate bar if you had multiple.
			{
				Item.NewItem(new EntitySource_TileBreak(i, j), i * 16, j * 16, 16, 16, ItemType<Terracoin>());
			}
			return base.Drop(i, j);
		}
	}
}