using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Content.Tiles
{
    public class NightBricksT : ModTile
	{
		public override void SetStaticDefaults() {
			Main.tileSolid[Type] = true;
			Main.tileMergeDirt[Type] = true;
			Main.tileBlockLight[Type] = true;

			//DustType = ModContent.DustType<Sparkle>();

			AddMapEntry(Color.Purple);
		}

		//public override void NumDust(int i, int j, bool fail, ref int num) {
		//	num = fail ? 1 : 3;
		//}

		//public override void ChangeWaterfallStyle(ref int style) {
		//	style = ModContent.GetInstance<ExampleWaterfallStyle>().Slot;
		//}
	}
}