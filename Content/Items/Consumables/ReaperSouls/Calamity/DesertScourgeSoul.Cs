using RemnantOfTheAncientsMod.Common;
using RemnantOfTheAncientsMod.Content.Items.Consumables.Pociones.Models;
using SangarUtilities.Common;
using SangarUtilities.Common.UtilsTweaks;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Content.Items.Consumables.ReaperSouls.Calamity
{
    public class DesertScourgeSoul : SoulModel
    {
        public override bool IsLoadingEnabled(Mod mod) => false;
        int SpeedBoost = 30;
        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(SpeedBoost);
        public override NPC BaseNpc => ContentSamples.NpcsByNetId[CallUtils.TryGetNpcFromMod(RemnantOfTheAncientsMod.CalamityMod, "DesertScourgeHead")];
        public override Mod ItemMod => RemnantOfTheAncientsMod.CalamityMod;
    }
  
}

