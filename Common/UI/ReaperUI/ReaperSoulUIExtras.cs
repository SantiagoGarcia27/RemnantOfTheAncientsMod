using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using SangarUtilities.Common;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent;

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


                GlobalUtils.AddSecure(ref BannedIds, CallUtils.GetNpcFromMod(CalamityMod, "PerforatorHive"));

                GlobalUtils.AddSecure(ref BannedIds, CallUtils.GetNpcFromMod(CalamityMod, "PerforatorHeadSmall"));
                GlobalUtils.AddSecure(ref BannedIds, CallUtils.GetNpcFromMod(CalamityMod, "PerforatorBodySmall"));
                GlobalUtils.AddSecure(ref BannedIds, CallUtils.GetNpcFromMod(CalamityMod, "PerforatorTailSmall"));

                GlobalUtils.AddSecure(ref BannedIds, CallUtils.GetNpcFromMod(CalamityMod, "PerforatorHeadMedium"));
                GlobalUtils.AddSecure(ref BannedIds, CallUtils.GetNpcFromMod(CalamityMod, "PerforatorBodyMedium"));
                GlobalUtils.AddSecure(ref BannedIds, CallUtils.GetNpcFromMod(CalamityMod, "PerforatorTailMedium"));

                GlobalUtils.AddSecure(ref BannedIds, CallUtils.GetNpcFromMod(CalamityMod, "PerforatorHeadLarge"));
                GlobalUtils.AddSecure(ref BannedIds, CallUtils.GetNpcFromMod(CalamityMod, "PerforatorBodyLarge"));
                GlobalUtils.AddSecure(ref BannedIds, CallUtils.GetNpcFromMod(CalamityMod, "PerforatorTailLarge"));

                GlobalUtils.AddSecure(ref BannedIds, CallUtils.GetNpcFromMod(CalamityMod, "AstrumDeusBody"));
                GlobalUtils.AddSecure(ref BannedIds, CallUtils.GetNpcFromMod(CalamityMod, "AstrumDeusTail"));

                GlobalUtils.AddSecure(ref BannedIds, CallUtils.GetNpcFromMod(CalamityMod, "DevourerofGodsBody"));
                GlobalUtils.AddSecure(ref BannedIds, CallUtils.GetNpcFromMod(CalamityMod, "DevourerofGodsTail"));

                GlobalUtils.AddSecure(ref BannedIds, CallUtils.GetNpcFromMod(CalamityMod, "AquaticScourgeBody"));
                GlobalUtils.AddSecure(ref BannedIds, CallUtils.GetNpcFromMod(CalamityMod, "AquaticScourgeTail"));

                GlobalUtils.AddSecure(ref BannedIds, CallUtils.GetNpcFromMod(CalamityMod, "SkeletronPrime2"));

                GlobalUtils.AddSecure(ref BannedIds, CallUtils.GetNpcFromMod(CalamityMod, "Cataclysm"));
                GlobalUtils.AddSecure(ref BannedIds, CallUtils.GetNpcFromMod(CalamityMod, "Catastrophe"));

                GlobalUtils.AddSecure(ref BannedIds, CallUtils.GetNpcFromMod(CalamityMod, "DesertScourgeBody"));
                GlobalUtils.AddSecure(ref BannedIds, CallUtils.GetNpcFromMod(CalamityMod, "DesertScourgeTail"));

                GlobalUtils.AddSecure(ref BannedIds, CallUtils.GetNpcFromMod(CalamityMod, "StormWeaverBody"));
                GlobalUtils.AddSecure(ref BannedIds, CallUtils.GetNpcFromMod(CalamityMod, "StormWeaverTail"));

                GlobalUtils.AddSecure(ref BannedIds, CallUtils.GetNpcFromMod(CalamityMod, "ThanatosBody1"));
                GlobalUtils.AddSecure(ref BannedIds, CallUtils.GetNpcFromMod(CalamityMod, "ThanatosBody2"));
                GlobalUtils.AddSecure(ref BannedIds, CallUtils.GetNpcFromMod(CalamityMod, "ThanatosTail"));

                GlobalUtils.AddSecure(ref BannedIds, CallUtils.GetNpcFromMod(CalamityMod, "Artemis"));
                GlobalUtils.AddSecure(ref BannedIds, CallUtils.GetNpcFromMod(CalamityMod, "Apollo"));

                GlobalUtils.AddSecure(ref BannedIds, CallUtils.GetNpcFromMod(CalamityMod, "AresBody"));
                GlobalUtils.AddSecure(ref BannedIds, CallUtils.GetNpcFromMod(CalamityMod, "AresGaussNuke"));
                GlobalUtils.AddSecure(ref BannedIds, CallUtils.GetNpcFromMod(CalamityMod, "AresLaserCannon"));
                GlobalUtils.AddSecure(ref BannedIds, CallUtils.GetNpcFromMod(CalamityMod, "AresPlasmaFlamethrower"));
                GlobalUtils.AddSecure(ref BannedIds, CallUtils.GetNpcFromMod(CalamityMod, "AresTeslaCannon"));

                GlobalUtils.AddSecure(ref BannedIds, CallUtils.GetNpcFromMod(CalamityMod, "Anahita"));

                GlobalUtils.AddSecure(ref BannedIds, CallUtils.GetNpcFromMod(CalamityMod, "THELORDE"));

                GlobalUtils.AddSecure(ref BannedIds, CallUtils.GetNpcFromMod(CalamityMod, "PrimordialWyrmHead"));
                GlobalUtils.AddSecure(ref BannedIds, CallUtils.GetNpcFromMod(CalamityMod, "PrimordialWyrmBody"));
                GlobalUtils.AddSecure(ref BannedIds, CallUtils.GetNpcFromMod(CalamityMod, "PrimordialWyrmTail"));
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

                    if (npc.type == CallUtils.GetNpcFromMod(CalamityMod, "StormWeaverHead"))
                        return ModContent.Request<Texture2D>(CalamityPath + "StormWeaver/StormWeaverHead_Head_Boss");
                    else if (npc.type == CallUtils.GetNpcFromMod(CalamityMod, "DevourerofGodsHead"))
                        return ModContent.Request<Texture2D>(CalamityPath + "DevourerofGods/DevourerofGodsHead_Head_Boss");
                    else if (npc.type == CallUtils.GetNpcFromMod(CalamityMod, "SupremeCalamitas"))
                        return ModContent.Request<Texture2D>(CalamityPath + "SupremeCalamitas/HoodedHeadIcon");
                    else if (npc.type == CallUtils.GetNpcFromMod(CalamityMod, "Providence"))
                        return ModContent.Request<Texture2D>("RemnantOfTheAncientsMod/Common/UI/ReaperUI/Textures/NPC_Head_Boss_Providence");
                }
            }
            int idex = npc.GetBossHeadTextureIndex();
            return TextureAssets.NpcHeadBoss[idex];
        }
        public static bool SelectList(Mod mod, int npcId)
        {
            ModLoader.TryGetMod("CalamityMod", out Mod CalamityMod);

            bool SoulsUpgradesLoaded = Main.LocalPlayer.GetModPlayer<ReaperSoulsPlayer>().SoulsUpgradesLoaded[ReaperSoulsPlayer.GetIndexFromLoadedBossById(npcId)];
            bool SoulsUpgradesConditon = SoulsUpgradesLoaded;

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
            float SoulsUpgradesConditon = player.GetModPlayer<ReaperSoulsPlayer>().SoulsUpgradesLoadedActive[index];

            if (CalamityMod != null && mod == CalamityMod)
                SoulsUpgradesConditon = player.GetModPlayer<ReaperSoulsPlayer>().SoulsUpgradesMaybeLoadedActive[npcId];
            return SoulsUpgradesConditon;
        }
        public static bool CheckCalamityMod(NPC npc, Mod CalamityMod, ref float baseLefthPosition, ref float baseLefthPositionIncrement)
        {
            if (CalamityMod != null)
            {
                if (npc.type == CallUtils.GetNpcFromMod(CalamityMod, "Providence"))
                {
                    baseLefthPosition += baseLefthPositionIncrement + 30;
                    return true;
                }
                else if (npc.type == CallUtils.GetNpcFromMod(CalamityMod, "CeaselessVoid"))
                {
                    baseLefthPosition += baseLefthPositionIncrement + 30;
                    return true;
                }
                else if (npc.type == CallUtils.GetNpcFromMod(CalamityMod, "Cryogen"))
                {
                    baseLefthPosition += baseLefthPositionIncrement + 10;
                    return true;
                }
                else if (npc.type == CallUtils.GetNpcFromMod(CalamityMod, "DesertScourgeHead"))
                {
                    baseLefthPosition += baseLefthPositionIncrement + 10;
                    return true;
                }
                else if (npc.type == CallUtils.GetNpcFromMod(CalamityMod, "HiveMind"))
                {
                    baseLefthPosition += baseLefthPositionIncrement + 20;
                    return true;
                }
                else if (npc.type == CallUtils.GetNpcFromMod(CalamityMod, "Crabulon"))
                {
                    baseLefthPosition += baseLefthPositionIncrement + 10;
                    return true;
                }
                else if (npc.type == CallUtils.GetNpcFromMod(CalamityMod, "Signus"))
                {
                    baseLefthPosition += baseLefthPositionIncrement + 10;
                    return true;
                }
                else if (npc.type == CallUtils.GetNpcFromMod(CalamityMod, "StormWeaverHead"))
                {
                    baseLefthPosition += baseLefthPositionIncrement + 10;
                    return true;
                }
                else if (npc.type == CallUtils.GetNpcFromMod(CalamityMod, "DevourerofGodsHead"))
                {
                    baseLefthPosition += baseLefthPositionIncrement - 10;
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }


    }

}

