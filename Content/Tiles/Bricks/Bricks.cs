using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Content.Tiles.Bricks
{
    public class NightBrickT : BaseBrickTile
    {
        internal override Color MapColor => Color.MediumPurple;
        internal override int FallbackTileID => TileID.CobaltBrick;
    }

    public class JungleBrickT : BaseBrickTile
    {
        internal override Color MapColor => Color.GreenYellow;
        internal override int FallbackTileID => TileID.GreenMossBrick;
    }
    public class ReinforcedIronBrickT : BaseBrickTile
    {
        internal override Color MapColor => Color.SlateGray;
        internal override int FallbackTileID => TileID.IronBrick;
    }

    public class TuxoniteBrickT : BaseBrickTile
    {
        internal override Color MapColor => Color.CornflowerBlue;
        internal override int FallbackTileID => TileID.PlatinumBrick;
    }

    public abstract class BaseBrickTile : ModTile
    {
        internal virtual Color MapColor { get; }
        internal virtual int FallbackTileID { get; }
        public override void SetStaticDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = true;
            Main.tileBlockLight[Type] = true;

            AddMapEntry(MapColor, Language.GetText("MapObject.MetalBar"));

            VanillaFallbackOnModDeletion = TileID.GoldBrick;
        }
    }
}
