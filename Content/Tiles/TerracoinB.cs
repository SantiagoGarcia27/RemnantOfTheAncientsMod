using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace RemnantOfTheAncientsMod.Content.Tiles
{
    public class TerracoinB : ModTile
	{
		public override void SetStaticDefaults()
		{
			Main.tileShine[Type] = 0;
			Main.tileSolid[Type] = true;
			Main.tileSolidTop[Type] = true;
			Main.tileFrameImportant[Type] = true;
			TileObjectData.newTile.CopyFrom(TileObjectData.Style1x1);
			TileObjectData.newTile.StyleHorizontal = true;
			TileObjectData.newTile.LavaDeath = false;
			TileObjectData.addTile(Type);

			AddMapEntry(Color.DarkOliveGreen, Language.GetText("MapObject.MetalBar"));
            VanillaFallbackOnModDeletion = TileID.PlatinumCoinPile;
        }
	}
}