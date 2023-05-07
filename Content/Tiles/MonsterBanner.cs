/*using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RemnantOfTheAncientsMod.Content.Items.Weapons.Banners;
using RemnantOfTheAncientsMod.NPCs;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace RemnantOfTheAncientsMod.Content.Tiles
{
	public class MonsterBanner : ModTile
	{
		public override void SetStaticDefaults()
		{
Main.tileFrameImportant[Type] = true;
Main.tileNoAttach[Type] = true;
Main.tileLavaDeath[Type] = true;
TileObjectData.newTile.CopyFrom(TileObjectData.Style1x2Top);
TileObjectData.newTile.Height = 3;
TileObjectData.newTile.CoordinateHeights = new int[]{ 16, 16, 16 };
TileObjectData.newTile.StyleHorizontal = true;
TileObjectData.newTile.StyleWrapLimit = 111;
TileObjectData.addTile(Type);
//dustType = -1;
//disableSmartCursor = true;
ModTranslation name = CreateMapEntryName();
name.SetDefault("Banner");
AddMapEntry(new Color(13, 88, 130), name);
		}

		public override void KillMultiTile(int i, int j, int frameX, int frameY)
		{
int style = frameX / 18;
string item;
switch (style)
{
	case 0:
		item = "ArmoredSlimeBanner";
		break;
	case 1:
		item = "ArmoredzombieBanner";
		break;
	default:
		return;
}
Item.NewItem(new EntitySource_TileBreak(i, j), i * 16, j * 16, 16, 48, ModContent.ItemType<ArmoredSlimeBanner>());
		}

		public override void NearbyEffects(int i, int j, bool closer)
		{
if (closer)
{
	Player player = Main.LocalPlayer;
	int style = Main.tile[i, j].TileFrameX / 18;
	string type;
	switch (style)
	{
		case 0:
type = "ArmoredSlime";
break;
		case 1:
type = "Armoredzombie";
break;
		default:
return;
	}
	
	player.HasNPCBannerBuff(ModContent.ItemType<type>()) = true;
	player.HasBanner = true;
		
}
		}

		public override void SetSpriteEffects(int i, int j, ref SpriteEffects spriteEffects)
		{
if (i % 2 == 1)
{
	spriteEffects = SpriteEffects.FlipHorizontally;
}
		}
	}
}*/