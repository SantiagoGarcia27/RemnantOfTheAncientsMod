using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Content.Tiles
{
    public class TuxoniteBrickT : ModTile
	{
		public override void SetStaticDefaults() {
			Main.tileSolid[Type] = true;
			Main.tileMergeDirt[Type] = true;
			Main.tileBlockLight[Type] = true;

			AddMapEntry(new Color(200, 200, 200));
		}
	}
}