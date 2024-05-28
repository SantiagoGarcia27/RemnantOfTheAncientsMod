using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria.ID;
using Terraria.GameContent.Creative;
using RemnantOfTheAncientsMod.Content.Tiles.Music_Box;
using Terraria.Localization;
using RemnantOfTheAncientsMod.Common.Global;

namespace RemnantOfTheAncientsMod.Content.Items.Placeables.MusicBox
{
	public class DesertMusicBox : ModItem
	{
		public override void SetStaticDefaults()
		{
            ItemID.Sets.CanGetPrefixes[Type] = false;
            ItemID.Sets.ShimmerTransformToItem[Type] = ItemID.MusicBox;
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
			MusicLoader.AddMusicBox(Mod, MusicLoader.GetMusicSlot(Mod, "Content/Sounds/Music/Desert_Aniquilator"), ItemType<DesertMusicBox>(), TileType<DesertMusicBoxT>());

		}

		public override void SetDefaults()
		{
            Item.DefaultToMusicBox(TileType<DesertMusicBoxT>(), 0);
            Item.GetGlobalItem<CustomTooltip>().MusicBox = true;
		}
	}
}
