using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Content.Tiles.Master_Relic.Infernum
{
    [JITWhenModsEnabled("InfernumMode")]
    public class Desert_Relic_Infernum_Tile : BaseInfernumBossRelic
    {
        //public override int DropItemID => ModContent.ItemType<BereftVassalRelic>();

        public override string RelicTextureName => "RemnantOfTheAncientsMod/Content/Tiles/Master_Relic/Infernum/" + this.Name;
    }
}
