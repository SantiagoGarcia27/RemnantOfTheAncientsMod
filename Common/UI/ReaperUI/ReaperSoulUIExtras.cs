using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using SangarUtilities.Common;
using SangarUtilities.Common.UtilsTweaks;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Common.UI.ReaperUI
{
    public class ReaperSoulUIExtras : ModSystem
    {
        public static Dictionary<Mod, Dictionary<int, UIHoverImageButton>> Buttons = [];
        public static List<int> BannedIds =
        [
           NPCID.MartianSaucerCore,
            NPCID.DD2EterniaCrystal, NPCID.DD2Betsy,NPCID.DD2DarkMageT1,NPCID.DD2DarkMageT3,NPCID.DD2OgreT2,NPCID.DD2OgreT3,
            NPCID.Everscream,NPCID.IceQueen,NPCID.SantaNK1,
            NPCID.PirateShip,
            NPCID.LunarTowerNebula,NPCID.LunarTowerSolar,NPCID.LunarTowerStardust,NPCID.LunarTowerVortex,
            NPCID.Pumpking,NPCID.MourningWood,
            NPCID.MoonLordHead,NPCID.MoonLordHand,
            NPCID.TorchGod
        ];
        public static void OnInitialize()
        {
            ModLoader.TryGetMod("CalamityMod", out Mod CalamityMod);

            if (CalamityMod != null)
            {
                Buttons.TryAdd(CalamityMod, []);


                ListUtils.AddSecure(ref BannedIds, CallUtils.TryGetNpcFromMod(CalamityMod, "PerforatorHive"));

                ListUtils.AddSecure(ref BannedIds, CallUtils.TryGetNpcFromMod(CalamityMod, "PerforatorHeadSmall"));
                ListUtils.AddSecure(ref BannedIds, CallUtils.TryGetNpcFromMod(CalamityMod, "PerforatorBodySmall"));
                ListUtils.AddSecure(ref BannedIds, CallUtils.TryGetNpcFromMod(CalamityMod, "PerforatorTailSmall"));

                ListUtils.AddSecure(ref BannedIds, CallUtils.TryGetNpcFromMod(CalamityMod, "PerforatorHeadMedium"));
                ListUtils.AddSecure(ref BannedIds, CallUtils.TryGetNpcFromMod(CalamityMod, "PerforatorBodyMedium"));
                ListUtils.AddSecure(ref BannedIds, CallUtils.TryGetNpcFromMod(CalamityMod, "PerforatorTailMedium"));

                ListUtils.AddSecure(ref BannedIds, CallUtils.TryGetNpcFromMod(CalamityMod, "PerforatorHeadLarge"));
                ListUtils.AddSecure(ref BannedIds, CallUtils.TryGetNpcFromMod(CalamityMod, "PerforatorBodyLarge"));
                ListUtils.AddSecure(ref BannedIds, CallUtils.TryGetNpcFromMod(CalamityMod, "PerforatorTailLarge"));

                ListUtils.AddSecure(ref BannedIds, CallUtils.TryGetNpcFromMod(CalamityMod, "AstrumDeusBody"));
                ListUtils.AddSecure(ref BannedIds, CallUtils.TryGetNpcFromMod(CalamityMod, "AstrumDeusTail"));

                ListUtils.AddSecure(ref BannedIds, CallUtils.TryGetNpcFromMod(CalamityMod, "DevourerofGodsBody"));
                ListUtils.AddSecure(ref BannedIds, CallUtils.TryGetNpcFromMod(CalamityMod, "DevourerofGodsTail"));

                ListUtils.AddSecure(ref BannedIds, CallUtils.TryGetNpcFromMod(CalamityMod, "AquaticScourgeBody"));
                ListUtils.AddSecure(ref BannedIds, CallUtils.TryGetNpcFromMod(CalamityMod, "AquaticScourgeTail"));

                ListUtils.AddSecure(ref BannedIds, CallUtils.TryGetNpcFromMod(CalamityMod, "Cataclysm"));
                ListUtils.AddSecure(ref BannedIds, CallUtils.TryGetNpcFromMod(CalamityMod, "Catastrophe"));

                ListUtils.AddSecure(ref BannedIds, CallUtils.TryGetNpcFromMod(CalamityMod, "DesertScourgeBody"));
                ListUtils.AddSecure(ref BannedIds, CallUtils.TryGetNpcFromMod(CalamityMod, "DesertScourgeTail"));

                ListUtils.AddSecure(ref BannedIds, CallUtils.TryGetNpcFromMod(CalamityMod, "StormWeaverBody"));
                ListUtils.AddSecure(ref BannedIds, CallUtils.TryGetNpcFromMod(CalamityMod, "StormWeaverTail"));

                ListUtils.AddSecure(ref BannedIds, CallUtils.TryGetNpcFromMod(CalamityMod, "ThanatosBody1"));
                ListUtils.AddSecure(ref BannedIds, CallUtils.TryGetNpcFromMod(CalamityMod, "ThanatosBody2"));
                ListUtils.AddSecure(ref BannedIds, CallUtils.TryGetNpcFromMod(CalamityMod, "ThanatosTail"));

                ListUtils.AddSecure(ref BannedIds, CallUtils.TryGetNpcFromMod(CalamityMod, "Artemis"));
                ListUtils.AddSecure(ref BannedIds, CallUtils.TryGetNpcFromMod(CalamityMod, "Apollo"));

                ListUtils.AddSecure(ref BannedIds, CallUtils.TryGetNpcFromMod(CalamityMod, "AresBody"));
                ListUtils.AddSecure(ref BannedIds, CallUtils.TryGetNpcFromMod(CalamityMod, "AresGaussNuke"));
                ListUtils.AddSecure(ref BannedIds, CallUtils.TryGetNpcFromMod(CalamityMod, "AresLaserCannon"));
                ListUtils.AddSecure(ref BannedIds, CallUtils.TryGetNpcFromMod(CalamityMod, "AresPlasmaFlamethrower"));
                ListUtils.AddSecure(ref BannedIds, CallUtils.TryGetNpcFromMod(CalamityMod, "AresTeslaCannon"));

                ListUtils.AddSecure(ref BannedIds, CallUtils.TryGetNpcFromMod(CalamityMod, "Anahita"));

                ListUtils.AddSecure(ref BannedIds, CallUtils.TryGetNpcFromMod(CalamityMod, "THELORDE"));

                ListUtils.AddSecure(ref BannedIds, CallUtils.TryGetNpcFromMod(CalamityMod, "PrimordialWyrmHead"));
                ListUtils.AddSecure(ref BannedIds, CallUtils.TryGetNpcFromMod(CalamityMod, "PrimordialWyrmBody"));
                ListUtils.AddSecure(ref BannedIds, CallUtils.TryGetNpcFromMod(CalamityMod, "PrimordialWyrmTail"));
            }

        }
        public static Asset<Texture2D> GetBossHead(NPC npc)
        {
            ModLoader.TryGetMod("CalamityMod", out Mod CalamityMod);

            if (npc.type == NPCID.Golem)
                return TextureAssets.NpcHeadBoss[5];
            else if (npc.type == NPCID.BrainofCthulhu)
                return TextureAssets.NpcHeadBoss[2];
            else if (npc.type == NPCID.MoonLordCore)
                return TextureAssets.NpcHeadBoss[8];
            else if (npc.type == NPCID.DukeFishron)
                return TextureAssets.NpcHeadBoss[4];

            ModNPC ModNpc = npc.ModNPC;
            if (CalamityMod != null && ModNpc != null)
            {
                if (ModNpc.Mod == CalamityMod)
                {
                    string CalamityPath = "CalamityMod/NPCs/";

                    if (npc.type == CallUtils.TryGetNpcFromMod(CalamityMod, "StormWeaverHead"))
                        return ModContent.Request<Texture2D>(CalamityPath + "StormWeaver/StormWeaverHead_Head_Boss");
                    else if (npc.type == CallUtils.TryGetNpcFromMod(CalamityMod, "DevourerofGodsHead"))
                        return ModContent.Request<Texture2D>(CalamityPath + "DevourerofGods/DevourerofGodsHead_Head_Boss");
                    else if (npc.type == CallUtils.TryGetNpcFromMod(CalamityMod, "SupremeCalamitas"))
                        return ModContent.Request<Texture2D>(CalamityPath + "SupremeCalamitas/HoodedHeadIcon");
                    else if (npc.type == CallUtils.TryGetNpcFromMod(CalamityMod, "Providence"))
                        return ModContent.Request<Texture2D>("RemnantOfTheAncientsMod/Common/UI/ReaperUI/Textures/NPC_Head_Boss_Providence");
                    else if(npc.type == CallUtils.TryGetNpcFromMod(CalamityMod, "DesertScourgeHead"))
                        return ModContent.Request<Texture2D>(CalamityPath + "DesertScourge/DesertScourgeHead_Head_Boss");
                }
            }
            int idex = npc.GetBossHeadTextureIndex();
            return TextureAssets.NpcHeadBoss[idex];
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

