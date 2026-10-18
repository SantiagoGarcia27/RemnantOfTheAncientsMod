using SangarUtilities.Common.UtilsTweaks;
using Terraria.Localization;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Content.Items.Consumables.ReaperSouls.Calamity
{
    public class DesertScourgeSoul : SoulModel
    {
        public override bool IsLoadingEnabled(Mod mod) => false;
        int SpeedBoost = 30;
        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(SpeedBoost);
        public override int NpcType => CallUtils.TryGetNpcFromMod(RemnantOfTheAncientsMod.CalamityMod, "DesertScourgeHead");
        public override Mod ItemMod => RemnantOfTheAncientsMod.CalamityMod;
        public override float Progression => 1.6f;
    }
  
}

