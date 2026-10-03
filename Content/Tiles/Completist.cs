using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;


namespace RemnantOfTheAncientsMod.Content.Tiles
{
	public class Completist : ModTile
	{
		public override void SetStaticDefaults()
		{
			Main.tileFrameImportant[Type] = true;
			Main.tileObsidianKill[Type] = true;

			TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
			TileObjectData.addTile(Type);
			LocalizedText name = CreateMapEntryName();

			AddMapEntry(Color.Gold, name);
            VanillaFallbackOnModDeletion = TileID.GolfTrophies;
        }
	}
}