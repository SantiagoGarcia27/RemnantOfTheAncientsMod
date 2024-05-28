using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria.ID;
using Terraria.GameContent.Creative;
using RemnantOfTheAncientsMod.Common.Global;
using RemnantOfTheAncientsMod.Content.Tiles.Music_Box;

namespace RemnantOfTheAncientsMod.Content.Items.Placeables.MusicBox
{
    public class InfernalMusicBox : ModItem
	{
		public override void SetStaticDefaults()
		{
            ItemID.Sets.CanGetPrefixes[Type] = false;
            ItemID.Sets.ShimmerTransformToItem[Type] = ItemID.MusicBox;
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
			MusicLoader.AddMusicBox(Mod, MusicLoader.GetMusicSlot(Mod, "Content/Sounds/Music/Infernal_Tyrant"), ItemType<InfernalMusicBox>(), TileType<InfernalMusicBoxT>());
		}

		public override void SetDefaults()
		{
            Item.DefaultToMusicBox(TileType<InfernalMusicBoxT>(), 0);
			Item.GetGlobalItem<CustomTooltip>().MusicBox = true;
		}
	}
}
