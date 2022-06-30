using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria.ID;
using opswordsII.Tiles.Music_Box;
using Terraria.GameContent.Creative;

namespace opswordsII.Items.Bloques.MusicBox
{
	public class InfernalMusicBox : ModItem
	{
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("InfernalMusicBox");
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
			MusicLoader.AddMusicBox(Mod, MusicLoader.GetMusicSlot(Mod, "Sounds/Music/Infernal_Tyrant"), ItemType<InfernalMusicBox>(), TileType<InfernalMusicBoxT>());
		}

		public override void SetDefaults() {
			Item.useStyle = ItemUseStyleID.Swing;
			Item.useTurn = true;
			Item.useAnimation = 15;
			Item.useTime = 10;
			Item.autoReuse = true;
			Item.consumable = true;
			Item.createTile = TileType<InfernalMusicBoxT>();
			Item.width = 24;
			Item.height = 24;
			Item.rare = ItemRarityID.LightRed;
			Item.value = 100000;
			Item.accessory = true;
		}
	}
}
