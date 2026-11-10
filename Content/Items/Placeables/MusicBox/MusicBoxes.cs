using RemnantOfTheAncientsMod.Common.Global;
using RemnantOfTheAncientsMod.Content.Tiles.Music_Box;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace RemnantOfTheAncientsMod.Content.Items.Placeables.MusicBox
{
	public class CrusherMusicBox : MusicBoxItem
    {
        public override int TileType => TileType<CrusherMusicBoxT>();
        public override int ItemType => ItemType<CrusherMusicBox>();
        public override string MusicPath => "Content/Sounds/Music/Gemstone_Crusher";
	}
	public class DesertMusicBox : MusicBoxItem
    {
        public override int TileType => TileType<DesertMusicBoxT>();
        public override int ItemType => ItemType<DesertMusicBox>();
        public override string MusicPath => "Content/Sounds/Music/Desert_Aniquilator";
	}
	public class FrozenMusicBox : MusicBoxItem
    {
        public override int TileType => TileType<FrozenMusicBoxT>();
        public override int ItemType => ItemType<FrozenMusicBox>();
        public override string MusicPath => "Content/Sounds/Music/Frozen_Assaulter_p1";
	}
	public class Frozenp2MusicBox : MusicBoxItem
    {
        public override int TileType => TileType<Frozenp2MusicBoxT>();
        public override int ItemType => ItemType<Frozenp2MusicBox>();
        public override string MusicPath => "Content/Sounds/Music/Frozen_Assaulter_p2";
	}
	public class InfernalMusicBox : MusicBoxItem
    {
        public override int TileType => TileType<InfernalMusicBoxT>();
        public override int ItemType => ItemType<InfernalMusicBox>();
        public override string MusicPath => "Content/Sounds/Music/Infernal_Tyrant";
	}

	public abstract class MusicBoxItem : ModItem
    {
        public virtual int TileType { get; set; }
        public virtual int ItemType { get; set; }
        public virtual string MusicPath { get; set; }

        public override void SetStaticDefaults()
        {
            ItemID.Sets.CanGetPrefixes[Type] = false;
            ItemID.Sets.ShimmerTransformToItem[Type] = ItemID.MusicBox;
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;

            MusicLoader.AddMusicBox(Mod, MusicLoader.GetMusicSlot(Mod, MusicPath), ItemType, TileType);
        }

        public override void SetDefaults()
        {
            Item.DefaultToMusicBox(TileType, 0);
            Item.GetGlobalItem<CustomTooltip>().MusicBox = true;
        }
    }
}
