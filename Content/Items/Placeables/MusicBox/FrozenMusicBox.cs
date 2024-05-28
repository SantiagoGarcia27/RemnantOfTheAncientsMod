using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria.ID;
using RemnantOfTheAncientsMod.Content.Tiles.Music_Box;
using Terraria.GameContent.Creative;
using Terraria.Localization;
using RemnantOfTheAncientsMod.Common.Global;

namespace RemnantOfTheAncientsMod.Content.Items.Placeables.MusicBox
{
	public class FrozenMusicBox : ModItem
	{
		public override void SetStaticDefaults()
		{
            ItemID.Sets.CanGetPrefixes[Type] = false;
            ItemID.Sets.ShimmerTransformToItem[Type] = ItemID.MusicBox;
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
			MusicLoader.AddMusicBox(Mod, MusicLoader.GetMusicSlot(Mod, "Content/Sounds/Music/Frozen_Assaulter_p1"), ItemType<FrozenMusicBox>(), TileType<FrozenMusicBoxT>());
		}

		public override void SetDefaults()
		{
            Item.DefaultToMusicBox(TileType<FrozenMusicBoxT>(), 0);
            Item.GetGlobalItem<CustomTooltip>().MusicBox = true;  
		}
	}
}
