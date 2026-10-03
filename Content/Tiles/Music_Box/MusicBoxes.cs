using Microsoft.Xna.Framework;
using RemnantOfTheAncientsMod.Content.Items.Placeables.MusicBox;
using static Terraria.ModLoader.ModContent;

namespace RemnantOfTheAncientsMod.Content.Tiles.Music_Box
{
    public class DesertMusicBoxT : MusicBoxTileBase
    {
        public override int MusicBoxItemType => ItemType<DesertMusicBox>();
        public override Color Color => Color.SandyBrown;
    }

    public class FrozenMusicBoxT : MusicBoxTileBase
    {
        public override int MusicBoxItemType => ItemType<FrozenMusicBox>();
        public override Color Color => Color.LightCyan;
    }
    public class Frozenp2MusicBoxT : MusicBoxTileBase
    {
        public override int MusicBoxItemType => ItemType<InfernalMusicBox>();
        public override Color Color => Color.LightCyan;
    }
    public class InfernalMusicBoxT : MusicBoxTileBase
    {
        public override int MusicBoxItemType => ItemType<InfernalMusicBox>();
        public override Color Color => Color.DarkRed;
    }
}
