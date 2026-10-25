using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using RemnantOfTheAncientsMod.Common.UI;
using SangarUtilities.Common.UtilsTweaks;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Common.ReaperSystem.ReaperUI
{
    public class ReaperSoulUIExtras : ModSystem
    {
        public static Dictionary<Mod, Dictionary<int, UIHoverImageButton>> Buttons = [];  
        public static Asset<Texture2D> GetBossHead(NPC npc)
        {

            ModLoader.TryGetMod("CalamityMod", out Mod CalamityMod);

            int index = -1;
            try
            {
                index = NPCID.Sets.BossHeadTextures[npc.type];
            }
            catch(Exception ex)
            {
                index = -1;
            }
            if (index == -1) return TextureAssets.NpcHeadBoss[1];
            return TextureAssets.NpcHeadBoss[index];
        }
        public static bool SelectList(Mod mod, int npcId)
        {
            ModLoader.TryGetMod("CalamityMod", out Mod CalamityMod);

            int index = ReaperSoulsPlayer.GetIndexFromLoadedBossById(npcId);
            bool SoulsUpgradesConditon = false;
            if (index != -1)
            {
                bool SoulsUpgradesLoaded = Main.LocalPlayer.GetModPlayer<ReaperSoulsPlayer>().SoulsUpgradesLoaded[index];
                SoulsUpgradesConditon = SoulsUpgradesLoaded;
            }
            if (CalamityMod != null && mod == CalamityMod)
            {
                bool SoulsUpgradesMaybeLoaded = Main.LocalPlayer.GetModPlayer<ReaperSoulsPlayer>().SoulsUpgradesMaybeLoaded[npcId];
                SoulsUpgradesConditon = SoulsUpgradesMaybeLoaded;
            }

            return SoulsUpgradesConditon;
        }
        public static float SelectActiveList(Mod mod, int npcId)
        {
            ModLoader.TryGetMod("CalamityMod", out Mod CalamityMod);
            Player player = Main.LocalPlayer;

            int index = ReaperSoulsPlayer.GetIndexFromLoadedBossById(npcId);
            float SoulsUpgradesConditon = 0f;

            if(index != -1)
                SoulsUpgradesConditon = player.GetModPlayer<ReaperSoulsPlayer>().SoulsUpgradesLoadedActive[index];
            else if (CalamityMod != null && mod == CalamityMod)
                SoulsUpgradesConditon = player.GetModPlayer<ReaperSoulsPlayer>().SoulsUpgradesMaybeLoadedActive[npcId];
            return SoulsUpgradesConditon;
        }
        public static bool CheckDistanceForBigIcons(NPC npc, Mod CalamityMod, ref float baseLefthPosition, ref float baseLefthPositionIncrement)
        {
            if(npc.type == NPCID.Deerclops)
            {
                baseLefthPosition += baseLefthPositionIncrement + 10;
                return true;
            }   
            if (CalamityMod != null)
            {
                if (npc.type == CallUtils.TryGetNpcFromMod(CalamityMod, "Providence"))
                {
                    baseLefthPosition += baseLefthPositionIncrement + 30;
                    return true;
                }
                else if (npc.type == CallUtils.TryGetNpcFromMod(CalamityMod, "CeaselessVoid"))
                {
                    baseLefthPosition += baseLefthPositionIncrement + 30;
                    return true;
                }
                else if (npc.type == CallUtils.TryGetNpcFromMod(CalamityMod, "Cryogen"))
                {
                    baseLefthPosition += baseLefthPositionIncrement + 10;
                    return true;
                }
                else if (npc.type == CallUtils.TryGetNpcFromMod(CalamityMod, "DesertScourgeHead"))
                {
                    baseLefthPosition += baseLefthPositionIncrement + 10;
                    return true;
                }
                else if (npc.type == CallUtils.TryGetNpcFromMod(CalamityMod, "HiveMind"))
                {
                    baseLefthPosition += baseLefthPositionIncrement + 20;
                    return true;
                }
                else if (npc.type == CallUtils.TryGetNpcFromMod(CalamityMod, "Crabulon"))
                {
                    baseLefthPosition += baseLefthPositionIncrement + 10;
                    return true;
                }
                else if (npc.type == CallUtils.TryGetNpcFromMod(CalamityMod, "Signus"))
                {
                    baseLefthPosition += baseLefthPositionIncrement + 10;
                    return true;
                }
                else if (npc.type == CallUtils.TryGetNpcFromMod(CalamityMod, "StormWeaverHead"))
                {
                    baseLefthPosition += baseLefthPositionIncrement + 10;
                    return true;
                }
                else if (npc.type == CallUtils.TryGetNpcFromMod(CalamityMod, "DevourerofGodsHead"))
                {
                    baseLefthPosition += baseLefthPositionIncrement - 10;
                    return true;
                }
            }
            return false;
        }


    }

}

