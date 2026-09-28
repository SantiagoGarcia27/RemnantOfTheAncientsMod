using RemnantOfTheAncientsMod.Content.Tiles.Master_Relic.Infernum;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Content.Items.Placeables.Relics.Infernum
{
    [JITWhenModsEnabled("InfernumMode")]
    public class Crusher_Relic_Infernum : BaseRelicItem
    {
        public override string DisplayNameToUse => "Infernal Gemstone Crusher Relic";

        public override int TileID => ModContent.TileType<Crusher_Relic_Infernum_Tile>();
    }
}
