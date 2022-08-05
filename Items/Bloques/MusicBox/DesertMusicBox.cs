using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria.ID;
using Terraria.GameContent.Creative;
using opswordsII.Tiles.Music_Box;

namespace opswordsII.Items.Bloques.MusicBox
{
	public class DesertMusicBox : ModItem
	{
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("DesertMusicBox");
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
			MusicLoader.AddMusicBox(Mod, MusicLoader.GetMusicSlot(Mod, "Sounds/Music/Desert_Aniquilator"), ItemType<DesertMusicBox>(), TileType<DesertMusicBoxT>());
			
		}

		public override void SetDefaults() {
			Item.useStyle = ItemUseStyleID.Swing;
			Item.useTurn = true;
			Item.useAnimation = 15;
			Item.useTime = 10;
			Item.autoReuse = true;
			Item.consumable = true;
			Item.createTile = TileType<DesertMusicBoxT>();
			Item.width = 24;
			Item.height = 24;
			Item.rare = ItemRarityID.LightRed;
			Item.value = 100000;
			Item.accessory = true;
		}
	}
}
