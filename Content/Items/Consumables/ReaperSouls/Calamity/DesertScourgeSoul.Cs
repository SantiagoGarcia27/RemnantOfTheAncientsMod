using RemnantOfTheAncientsMod.Common;
using RemnantOfTheAncientsMod.Content.Items.Consumables.Pociones.Models;
using Terraria.Localization;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Content.Items.Consumables.ReaperSouls.Calamity
{
    public class DesertScourgeSoul : SoulModel
    {
        public override bool IsLoadingEnabled(Mod mod) => false;
        int SpeedBoost = 30;
        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(SpeedBoost);
        public override int SoulID => BossID.CalamityID.DesertScourge;
        public override string ItemMod => "CalamityMod";
    }
  
}

