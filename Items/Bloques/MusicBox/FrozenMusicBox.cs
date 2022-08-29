using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria.ID;
using RemnantOfTheAncientsMod.Tiles.Music_Box;
using Terraria.GameContent.Creative;

namespace RemnantOfTheAncientsMod.Items.Bloques.MusicBox
{
	public class FrozenMusicBox : ModItem
	{
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("FrozenMusicBox");
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
			MusicLoader.AddMusicBox(Mod, MusicLoader.GetMusicSlot(Mod, "Sounds/Music/Frozen_Assaulter_p1"), ItemType<FrozenMusicBox>(), TileType<FrozenMusicBoxT>());
		}

		public override void SetDefaults() {
			Item.useStyle = ItemUseStyleID.Swing;
			Item.useTurn = true;
			Item.useAnimation = 15;
			Item.useTime = 10;
			Item.autoReuse = true;
			Item.consumable = true;
			Item.createTile = TileType<FrozenMusicBoxT>();
			Item.width = 24;
			Item.height = 24;
			Item.rare = ItemRarityID.LightRed;
			Item.value = 100000;
			Item.accessory = true;
		}
	}
}
