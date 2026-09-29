using RemnantOfTheAncientsMod.Common.Global;
using RemnantOfTheAncientsMod.Content.Tiles.Music_Box;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace RemnantOfTheAncientsMod.Content.Items.Placeables.MusicBox
{
	public class Frozenp2MusicBox : ModItem
	{
		public override void SetStaticDefaults()
		{
            ItemID.Sets.CanGetPrefixes[Type] = false;
            ItemID.Sets.ShimmerTransformToItem[Type] = ItemID.MusicBox;
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
			MusicLoader.AddMusicBox(Mod, MusicLoader.GetMusicSlot(Mod, "Content/Sounds/Music/Frozen_Assaulter_p2"), ItemType<Frozenp2MusicBox>(), TileType<Frozenp2MusicBoxT>());
		}

		public override void SetDefaults()
		{
            Item.DefaultToMusicBox(TileType<Frozenp2MusicBoxT>(), 0);
            Item.GetGlobalItem<CustomTooltip>().MusicBox = true;
		}
	}
}
