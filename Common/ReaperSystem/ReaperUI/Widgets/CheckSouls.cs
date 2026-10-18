using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

public class CheckSouls : ModPlayer
{
    public override void PostUpdate()
    {
        Player player = Main.player[Main.myPlayer];
        bool[] LoadedSouls = player.GetModPlayer<ReaperSoulsPlayer>().SoulsUpgradesLoaded;
        Dictionary<int, bool> MaybeLoadedSouls = player.GetModPlayer<ReaperSoulsPlayer>().SoulsUpgradesMaybeLoaded;

        ModLoader.TryGetMod("SangarUtilities", out Mod TerrariaMod);
        ModLoader.TryGetMod("RemnantOfTheAncientsMod", out Mod RemnantOfTheAncientsM);
        ModLoader.TryGetMod("CalamityMod", out Mod CalamityMod);
        Mod mod = TerrariaMod;

        for (int i = 0; i < LoadedSouls.Length; i++)
        {
            int type = ReaperSoulsPlayer.GetLoadedBossIdByIndex(i);
            NPC npc = ContentSamples.NpcsByNetId[type];

            if (i >= LoadedSouls.Length - 3)
                mod = RemnantOfTheAncientsM;
            UISoulDisplay.UpdateButtons(npc.type, mod);
        }
        if (CalamityMod != null)
        {
            foreach (int id in MaybeLoadedSouls.Keys)
            {
                NPC npc = ContentSamples.NpcsByNetId[id];
                UISoulDisplay.UpdateButtons(npc.type, CalamityMod);
            }
        }

        base.PostUpdate();
    }
}