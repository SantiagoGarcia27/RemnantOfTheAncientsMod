using RemnantOfTheAncientsMod.Common.ReaperSystem;
using Terraria;
using Terraria.ModLoader;

public class CheckSouls : ModPlayer
{
    public override void PostUpdate()
    {
        if (Main.dedServ || Player.whoAmI != Main.myPlayer)
            return;

        foreach (ReaperUpgradeRegister data in Reaper.soulData)
        {
            UISoulDisplay.UpdateButtons(data.npcType, data.mod);
        }
    }
}