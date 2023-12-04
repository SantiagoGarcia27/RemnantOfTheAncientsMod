﻿using RemnantOfTheAncientsMod.Content.Tiles.Master_Relic.Infernum;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Content.Items.Placeables.Relics.Infernum
{
    [JITWhenModsEnabled("InfernumMode")]
    public class Desert_Relic_Infernum : BaseRelicItem
    {
        public override string DisplayNameToUse => "Infernal Adult Eidolon Wyrm Relic";

        public override int TileID => ModContent.TileType<Desert_Relic_Infernum_Tile>();
    }
}
