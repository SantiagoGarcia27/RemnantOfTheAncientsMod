using Microsoft.Xna.Framework;
using RemnantOfTheAncientsMod.Content.Items.Placeables.MusicBox;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;
using static Terraria.ModLoader.ModContent;

namespace RemnantOfTheAncientsMod.Content.Tiles.Music_Box
{
    public class CrusherMusicBoxT : MusicBoxTile
    {
        public override int MusicBoxItemType => ItemType<CrusherMusicBox>();
        public override Color Color => Color.Gray;
    }
    public class DesertMusicBoxT : MusicBoxTile
    {
        public override int MusicBoxItemType => ItemType<DesertMusicBox>();
        public override Color Color => Color.SandyBrown;
    }
    public class FrozenMusicBoxT : MusicBoxTile
    {
        public override int MusicBoxItemType => ItemType<FrozenMusicBox>();
        public override Color Color => Color.LightCyan;
    }
    public class Frozenp2MusicBoxT : MusicBoxTile
    {
        public override int MusicBoxItemType => ItemType<InfernalMusicBox>();
        public override Color Color => Color.LightCyan;
    }
    public class InfernalMusicBoxT : MusicBoxTile
    {
        public override int MusicBoxItemType => ItemType<InfernalMusicBox>();
        public override Color Color => Color.DarkRed;
    }

    public abstract class MusicBoxTile : ModTile
    {
        public virtual int MusicBoxItemType { get; }
        public virtual Color Color { get; }

        public override void SetStaticDefaults()
        {
            Main.tileFrameImportant[Type] = true;
            Main.tileObsidianKill[Type] = true;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
            TileObjectData.newTile.Origin = new Point16(0, 1);
            TileObjectData.newTile.LavaDeath = false;
            TileObjectData.newTile.DrawYOffset = 2;
            TileObjectData.addTile(Type);
            LocalizedText name = CreateMapEntryName();

            VanillaFallbackOnModDeletion = TileID.MusicBoxes;

            AddMapEntry(Color, name);
        }
        public override void MouseOver(int i, int j)
        {
            Player player = Main.LocalPlayer;
            player.noThrow = 2;
            player.cursorItemIconEnabled = true;
            player.cursorItemIconID = MusicBoxItemType;
        }
    }
}
