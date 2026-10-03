using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace RemnantOfTheAncientsMod.Content.Tiles.Bars
{
    public class BoneBarB : MetalBarModel
    {
        public override Color MapColor => Color.DarkGray;
    }
    public class JungleBarB : MetalBarModel
    {
        public override Color MapColor => Color.GreenYellow;
    }

    public class NightBarB : MetalBarModel
    {
        public override Color MapColor => Color.MediumPurple;
    }

    public class ReinforcedIronBarB : MetalBarModel
    {
        public override Color MapColor => Color.SlateGray;
    }

    public class TuxoniteBarB : MetalBarModel
    {
        public override Color MapColor => Color.CornflowerBlue;
    }

 

    public abstract class MetalBarModel : ModTile
    {
        public abstract Color MapColor { get; }

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

            AddMapEntry(MapColor, Language.GetText("MapObject.MetalBar"));

            VanillaFallbackOnModDeletion = TileID.MetalBars;
        }
    }
}
