using CalamityMod;
using CalamityMod.NPCs.BrimstoneElemental;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using RemnantOfTheAncientsMod.Common.UtilsTweaks;
using RemnantOfTheAncientsMod.Content.Buffs.Debuff;
using RemnantOfTheAncientsMod.Content.NPCs;
using RemnantOfTheAncientsMod.Content.NPCs.Bosses.ITyrant;
using RemnantOfTheAncientsMod.Content.Projectiles.BossProjectiles.Calamity;
using RemnantOfTheAncientsMod.Content.Projectiles.Multiclass;
using RemnantOfTheAncientsMod.Content.Projectiles.Ranger;
using RemnantOfTheAncientsMod.World;
using SangarUtilities.Common.UtilsTweaks;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Common.Global.NPCs
{
    public class ReaperGlobalNpc : GlobalNPC
    {
        public override bool InstancePerEntity => true;
        public override void SetDefaults(NPC npc)
        {
            if (RemnantOfTheAncientsMod.CalamityMod != null)
                FillList();
            if (npc.boss)
            {
                ModifyBossStats(npc);
            }

            if (Reaper.ReaperMode)
            {
                if (destroyer.Contains(npc.type))
                {
                    npc.scale *= 2f;
                }
                if (npc.type == NPCID.EyeofCthulhu)
                {
                    npc.buffImmune[BuffID.OnFire] = true;
                    npc.buffImmune[BuffID.Frostburn] = true;
                    npc.buffImmune[BuffID.Poisoned] = true;
                    npc.defense += 5;
                }
                if (npc.type == NPCID.SkeletronHand)
                {
                    npc.scale = 1.5f;
                }
                if (npc.type == NPCID.SkeletronHead)
                {
                    npc.scale = 0.8f;
                }
                if (npc.type == NPCID.Creeper)
                {
                    npc.damage = (int)(npc.damage * 0.1f);
                    npc.velocity *= 0.1f;
                    npc.defense += 5;
                }
                if (npc.type == NPCID.WallofFleshEye)
                {
                    npc.scale /= 2;
                }
                if (npc.type == NPCID.PrimeSaw)
                {
                    npc.scale = 1.5f;
                }
                if (npc.type == NPCID.PrimeVice)
                {
                    npc.scale = 1.5f;
                }
                if (npc.type == NPCID.SkeletronPrime)
                {
                    npc.scale = 0.8f;
                }


                if (RemnantOfTheAncientsMod.CalamityMod != null)
                {
                    if (desertScourge.Contains(npc.type))
                    {
                        npc.scale *= 1.5f;
                    }
                    if (desertNuisance.Contains(npc.type))
                    {
                        npc.scale *= 0.25f;
                    }
                    if (npc.type == CallUtils.TryGetNpcFromMod(RemnantOfTheAncientsMod.CalamityMod, "DankCreeper"))
                    {
                        if (Reaper.ReaperMode) npc.lifeMax /= 2;
                    }
                    if (perforatorWormSamll.Contains(npc.type))
                    {
                        if (Reaper.ReaperMode)
                        {
                            for (int buff = 0; buff < BuffLoader.BuffCount; buff++)
                                npc.buffImmune[buff] = true;
                        }
                    }

                    if (npc.type == CallUtils.TryGetNpcFromMod(RemnantOfTheAncientsMod.CalamityMod, "PerforatorBodyLarge"))
                    {
                        if (Reaper.ReaperMode)
                        {
                            npc.immortal = true;
                            npc.dontTakeDamage = true;
                        }
                    }
                    if (npc.type == CallUtils.TryGetNpcFromMod(RemnantOfTheAncientsMod.CalamityMod, "Leviathan"))
                    {
                        if (Reaper.ReaperMode)
                        {
                            npc.Size = new(650, 200);
                            npc.scale = 1.5f;
                        }
                    }
                    if (AstrumDeus.Contains(npc.type))
                    {
                        npc.alpha = 255;
                    }
                }
            }
        }

        [JITWhenModsEnabled("CalamityMod")]
        private void FillList()
        {
            desertScourge =
            [
                CallUtils.TryGetNpcFromMod(RemnantOfTheAncientsMod.CalamityMod, "DesertScourgeHead"),
                CallUtils.TryGetNpcFromMod(RemnantOfTheAncientsMod.CalamityMod, "DesertScourgeBody"),
                CallUtils.TryGetNpcFromMod(RemnantOfTheAncientsMod.CalamityMod, "DesertScourgeTail")
            ];
            desertNuisance =
            [
                CallUtils.TryGetNpcFromMod(RemnantOfTheAncientsMod.CalamityMod, "DesertNuisanceHead"),
                CallUtils.TryGetNpcFromMod(RemnantOfTheAncientsMod.CalamityMod, "DesertNuisanceBody"),
                CallUtils.TryGetNpcFromMod(RemnantOfTheAncientsMod.CalamityMod, "DesertNuisanceTail")
            ];
            perforatorWormSamll =
            [
                CallUtils.TryGetNpcFromMod(RemnantOfTheAncientsMod.CalamityMod, "PerforatorHeadSmall"),
                CallUtils.TryGetNpcFromMod(RemnantOfTheAncientsMod.CalamityMod, "PerforatorBodySmall"),
                CallUtils.TryGetNpcFromMod(RemnantOfTheAncientsMod.CalamityMod, "PerforatorTailSmall")
            ];
            perforatorWormMedium =
            [
                CallUtils.TryGetNpcFromMod(RemnantOfTheAncientsMod.CalamityMod, "PerforatorHeadMedium"),
                CallUtils.TryGetNpcFromMod(RemnantOfTheAncientsMod.CalamityMod, "PerforatorBodyMedium"),
                CallUtils.TryGetNpcFromMod(RemnantOfTheAncientsMod.CalamityMod, "PerforatorTailMedium")
            ];
            perforatorWormBig =
            [
                CallUtils.TryGetNpcFromMod(RemnantOfTheAncientsMod.CalamityMod, "PerforatorHeadLarge"),
                CallUtils.TryGetNpcFromMod(RemnantOfTheAncientsMod.CalamityMod, "PerforatorBodyLarge"),
                CallUtils.TryGetNpcFromMod(RemnantOfTheAncientsMod.CalamityMod, "PerforatorTailLarge")
            ];
            AstrumDeus =
            [
                CallUtils.TryGetNpcFromMod(RemnantOfTheAncientsMod.CalamityMod, "AstrumDeusHead"),
                CallUtils.TryGetNpcFromMod(RemnantOfTheAncientsMod.CalamityMod, "AstrumDeusBody"),
                CallUtils.TryGetNpcFromMod(RemnantOfTheAncientsMod.CalamityMod, "AstrumDeusTail")
            ];
            StormWeaver =
            [
                CallUtils.TryGetNpcFromMod(RemnantOfTheAncientsMod.CalamityMod, "StormWeaverHead"),
                CallUtils.TryGetNpcFromMod(RemnantOfTheAncientsMod.CalamityMod, "StormWeaverBody"),
                CallUtils.TryGetNpcFromMod(RemnantOfTheAncientsMod.CalamityMod, "StormWeaverTail")
            ];
            DevourerOFGods =
            [
                CallUtils.TryGetNpcFromMod(RemnantOfTheAncientsMod.CalamityMod, "DevourerofGodsHead"),
                CallUtils.TryGetNpcFromMod(RemnantOfTheAncientsMod.CalamityMod, "DevourerofGodsBody"),
                CallUtils.TryGetNpcFromMod(RemnantOfTheAncientsMod.CalamityMod, "DevourerofGodsTail")
            ];
            PrimordialWyrm =
            [
                CallUtils.TryGetNpcFromMod(RemnantOfTheAncientsMod.CalamityMod, "PrimordialWyrmHead"),
                CallUtils.TryGetNpcFromMod(RemnantOfTheAncientsMod.CalamityMod, "PrimordialWyrmBody"),
                CallUtils.TryGetNpcFromMod(RemnantOfTheAncientsMod.CalamityMod, "PrimordialWyrmTail")
            ];

            CalamityAffectedNPCs =
            [
                CallUtils.TryGetNpcFromMod(RemnantOfTheAncientsMod.CalamityMod, "DesertScourgeHead"),
                CallUtils.TryGetNpcFromMod(RemnantOfTheAncientsMod.CalamityMod, "Crabulon"),
                CallUtils.TryGetNpcFromMod(RemnantOfTheAncientsMod.CalamityMod, "HiveMind"),
                CallUtils.TryGetNpcFromMod(RemnantOfTheAncientsMod.CalamityMod, "DankCreeper"),
                CallUtils.TryGetNpcFromMod(RemnantOfTheAncientsMod.CalamityMod, "PerforatorHive"),
                CallUtils.TryGetNpcFromMod(RemnantOfTheAncientsMod.CalamityMod, "PerforatorHeadSmall"),
                CallUtils.TryGetNpcFromMod(RemnantOfTheAncientsMod.CalamityMod, "PerforatorBodySmall"),
                CallUtils.TryGetNpcFromMod(RemnantOfTheAncientsMod.CalamityMod, "PerforatorTailSmall"),
                CallUtils.TryGetNpcFromMod(RemnantOfTheAncientsMod.CalamityMod, "PerforatorHeadMedium"),
                CallUtils.TryGetNpcFromMod(RemnantOfTheAncientsMod.CalamityMod, "PerforatorBodyMedium"),
                CallUtils.TryGetNpcFromMod(RemnantOfTheAncientsMod.CalamityMod, "PerforatorTailMedium"),
                CallUtils.TryGetNpcFromMod(RemnantOfTheAncientsMod.CalamityMod, "PerforatorHeadLarge"),
                CallUtils.TryGetNpcFromMod(RemnantOfTheAncientsMod.CalamityMod, "PerforatorBodyLarge"),
                CallUtils.TryGetNpcFromMod(RemnantOfTheAncientsMod.CalamityMod, "PerforatorTailLarge"),
                CallUtils.TryGetNpcFromMod(RemnantOfTheAncientsMod.CalamityMod, "SlimeGodCore"),
                CallUtils.TryGetNpcFromMod(RemnantOfTheAncientsMod.CalamityMod, "SplitCrimulanPaladin"),
                CallUtils.TryGetNpcFromMod(RemnantOfTheAncientsMod.CalamityMod, "CrimulanPaladin"),
                CallUtils.TryGetNpcFromMod(RemnantOfTheAncientsMod.CalamityMod, "EbonianPaladin"),
                CallUtils.TryGetNpcFromMod(RemnantOfTheAncientsMod.CalamityMod, "SplitEbonianPaladin"),
                CallUtils.TryGetNpcFromMod(RemnantOfTheAncientsMod.CalamityMod, "Cryogen"),
                CallUtils.TryGetNpcFromMod(RemnantOfTheAncientsMod.CalamityMod, "AquaticScourgeHead"),
                CallUtils.TryGetNpcFromMod(RemnantOfTheAncientsMod.CalamityMod, "BrimstoneElemental"),
                CallUtils.TryGetNpcFromMod(RemnantOfTheAncientsMod.CalamityMod, "CalamitasClone"),
                CallUtils.TryGetNpcFromMod(RemnantOfTheAncientsMod.CalamityMod, "Cataclysm"),
                CallUtils.TryGetNpcFromMod(RemnantOfTheAncientsMod.CalamityMod, "Catastrophe"),
                CallUtils.TryGetNpcFromMod(RemnantOfTheAncientsMod.CalamityMod, "SoulSeeker"),
                CallUtils.TryGetNpcFromMod(RemnantOfTheAncientsMod.CalamityMod, "GreatSandShark"),
                CallUtils.TryGetNpcFromMod(RemnantOfTheAncientsMod.CalamityMod, "Leviathan"),
                CallUtils.TryGetNpcFromMod(RemnantOfTheAncientsMod.CalamityMod, "Anahita"),
                CallUtils.TryGetNpcFromMod(RemnantOfTheAncientsMod.CalamityMod, "AstrumAureus"),
                CallUtils.TryGetNpcFromMod(RemnantOfTheAncientsMod.CalamityMod, "PlaguebringerGoliath"),
                CallUtils.TryGetNpcFromMod(RemnantOfTheAncientsMod.CalamityMod, "RavagerBody"),
                CallUtils.TryGetNpcFromMod(RemnantOfTheAncientsMod.CalamityMod, "AstrumDeusHead"),
                CallUtils.TryGetNpcFromMod(RemnantOfTheAncientsMod.CalamityMod, "ProfanedGuardianCommander"),
                CallUtils.TryGetNpcFromMod(RemnantOfTheAncientsMod.CalamityMod, "ProfanedGuardianDefender"),
                CallUtils.TryGetNpcFromMod(RemnantOfTheAncientsMod.CalamityMod, "ProfanedGuardianHealer"),
                CallUtils.TryGetNpcFromMod(RemnantOfTheAncientsMod.CalamityMod, "Dragonfolly"),
                CallUtils.TryGetNpcFromMod(RemnantOfTheAncientsMod.CalamityMod, "Providence"),
                CallUtils.TryGetNpcFromMod(RemnantOfTheAncientsMod.CalamityMod, "StormWeaverHead"),
                CallUtils.TryGetNpcFromMod(RemnantOfTheAncientsMod.CalamityMod, "CeaselessVoid"),
                CallUtils.TryGetNpcFromMod(RemnantOfTheAncientsMod.CalamityMod, "Signus"),
                CallUtils.TryGetNpcFromMod(RemnantOfTheAncientsMod.CalamityMod, "Polterghast"),
                CallUtils.TryGetNpcFromMod(RemnantOfTheAncientsMod.CalamityMod, "OldDuke"),
                CallUtils.TryGetNpcFromMod(RemnantOfTheAncientsMod.CalamityMod, "DevourerofGodsHead"),
                CallUtils.TryGetNpcFromMod(RemnantOfTheAncientsMod.CalamityMod, "Yharon"),
                CallUtils.TryGetNpcFromMod(RemnantOfTheAncientsMod.CalamityMod, "Apollo"),
                CallUtils.TryGetNpcFromMod(RemnantOfTheAncientsMod.CalamityMod, "Artemis"),
                CallUtils.TryGetNpcFromMod(RemnantOfTheAncientsMod.CalamityMod, "AresBody"),
                CallUtils.TryGetNpcFromMod(RemnantOfTheAncientsMod.CalamityMod, "ThanatosHead"),
                CallUtils.TryGetNpcFromMod(RemnantOfTheAncientsMod.CalamityMod, "SupremeCalamitas")
            ];
        }
        public List<int> destroyer =
        [
            NPCID.TheDestroyer ,
            NPCID.TheDestroyerBody,
            NPCID.TheDestroyerTail
        ];

        public List<int> desertScourge = [];
        public List<int> desertNuisance = [];
        public List<int> perforatorWormSamll = [];
        public List<int> perforatorWormMedium = [];
        public List<int> perforatorWormBig = [];
        public List<int> AstrumDeus = [];
        public List<int> StormWeaver = [];
        public List<int> DevourerOFGods = [];
        public List<int> PrimordialWyrm = [];
        public List<int> CalamityAffectedNPCs = [];
        public static void ModifyBossStats(NPC npc)
        {
            npc.lifeMax *= Reaper.ReaperMode ? 2 : 1;
            npc.damage *= Reaper.ReaperMode ? 2 : 1;
        }
        int primePhase = 0;
        public override void OnSpawn(NPC npc, IEntitySource source)
        {
            if (Reaper.ReaperMode)
            {
                ModNPC modNpc = npc.ModNPC;
                if (modNpc != null)
                {
                    if (RemnantOfTheAncientsMod.CalamityMod != null && modNpc.Mod == RemnantOfTheAncientsMod.CalamityMod)
                        OnSpawnCalamity(npc, source);
                }
                if (npc.type == NPCID.SkeletronHead)
                {
                    for (int i = 0; i < 2; i++)
                    {
                        NPC.SpawnOnPlayer(Main.player[npc.target].whoAmI, NPCID.SkeletronHand);
                    }
                }
                if (npc.type == NPCID.Deerclops)
                {
                    if (Main.netMode == NetmodeID.MultiplayerClient)
                    {
                        RemnantPlayer.ApplyBuffToAllPlayers(BuffID.Hunger, 0, 1, 30);
                    }
                    else
                    {
                        Main.player[npc.target].AddBuff(BuffID.Hunger, Utils1.FormatTimeToTick(0, 0, 0, 30));
                    }
                    // Main.dontStarveWorld = true;
                }
                if (npc.type == NPCID.Golem)
                {
                    NPC.NewNPC(npc.GetSource_FromAI(), (int)npc.position.X, (int)npc.position.Y, NPCID.GolemFistLeft);
                    NPC.NewNPC(npc.GetSource_FromAI(), (int)npc.position.X, (int)npc.position.Y, NPCID.GolemFistRight);
                }
                if (npc.type == NPCID.GolemFistLeft || npc.type == NPCID.GolemFistRight)
                {
                    npc.life = npc.lifeMax;
                }
                if (npc.type == NPCID.HallowBoss)
                {
                    Main.dayTime = true;
                    Main.time = 5000.0;
                    Netcode.SyncWorld();
                }
                if(npc.type == NPCID.GolemHeadFree && npc.ai[3] >= 0)
                {
                    int i = NPC.NewNPC(npc.GetSource_FromAI(), (int)npc.position.X - 13 * 16, (int)npc.position.Y, NPCID.GolemHeadFree, ai3:-1);
                    Main.npc[i].alpha = 50;
                }
            }
            base.OnSpawn(npc, source);
        }

        [JITWhenModsEnabled("CalamityMod")]
        public void OnSpawnCalamity(NPC npc, IEntitySource source)
        {
            if (npc.type == CallUtils.TryGetNpcFromMod(RemnantOfTheAncientsMod.CalamityMod, "DesertScourgeHead"))
            {
                NPC.SpawnOnPlayer(npc.FindClosestPlayer(), CallUtils.TryGetNpcFromMod(RemnantOfTheAncientsMod.CalamityMod, "DesertNuisanceHeadYoung"));
                NPC.SpawnOnPlayer(npc.FindClosestPlayer(), CallUtils.TryGetNpcFromMod(RemnantOfTheAncientsMod.CalamityMod, "DesertNuisanceHeadYoung"));
            }

            if (npc.type == CallUtils.TryGetNpcFromMod(RemnantOfTheAncientsMod.CalamityMod, "HiveMind"))
            {
                int ammount = 20;
                for (int i = 0; i <= ammount; i++)
                {
                    GetPositionsAround(Main.player[npc.FindClosestPlayer()], 25 * 15f, ammount + 1, out List<Vector2> Postions);
                    var p = NPC.NewNPC(npc.GetSource_FromAI(), (int)Postions[i].X * 10, (int)Postions[i].Y * 10, CallUtils.TryGetNpcFromMod(RemnantOfTheAncientsMod.CalamityMod, "DankCreeper"));
                    NPC.NewNPC(npc.GetSource_FromAI(), (int)Postions[i].X, (int)Postions[i].Y, CallUtils.TryGetNpcFromMod(RemnantOfTheAncientsMod.CalamityMod, "DankCreeper"));
                    NPC.NewNPC(npc.GetSource_FromAI(), (int)Postions[i].X, (int)Postions[i].Y, CallUtils.TryGetNpcFromMod(RemnantOfTheAncientsMod.CalamityMod, "DankCreeper"));
                    Main.npc[p].aiStyle = -1;
                }
            }
            if (npc.type == CallUtils.TryGetNpcFromMod(RemnantOfTheAncientsMod.CalamityMod, "DankCreeper"))
            {
                GetNPCList(CallUtils.TryGetNpcFromMod(RemnantOfTheAncientsMod.CalamityMod, "DankCreeper"), out List<NPC> CreepersList);
                GetPositionsAround(Main.player[npc.FindClosestPlayer()], 25 * 15f, CreepersList.Count, out List<Vector2> Postions);

                for (int a = 0; a < NPC.CountNPCS(CallUtils.TryGetNpcFromMod(RemnantOfTheAncientsMod.CalamityMod, "DankCreeper")); a++)
                {
                    CreepersList[a].position = Postions[a];
                }
            }
            if (npc.type == CallUtils.TryGetNpcFromMod(RemnantOfTheAncientsMod.CalamityMod, "RavagerBody"))
            {
                for (int i = 0; i < 2; i++)
                {
                    NPC.NewNPC(npc.GetSource_FromAI(), (int)npc.Center.X, (int)npc.Center.Y + i * 10, CallUtils.TryGetNpcFromMod(RemnantOfTheAncientsMod.CalamityMod, "RavagerClawLeft"));
                    NPC.NewNPC(npc.GetSource_FromAI(), (int)npc.Center.X, (int)npc.Center.Y - i * 10, CallUtils.TryGetNpcFromMod(RemnantOfTheAncientsMod.CalamityMod, "RavagerClawRight"));
                }
            }
            if (npc.type == CallUtils.TryGetNpcFromMod(RemnantOfTheAncientsMod.CalamityMod, "ProfanedGuardianCommander"))
            {
                int a = NPC.NewNPC(npc.GetSource_FromAI(), (int)npc.Center.X - 50, (int)npc.Center.Y, CallUtils.TryGetNpcFromMod(RemnantOfTheAncientsMod.CalamityMod, "ProfanedGuardianDefender"));
                Main.npc[a].lifeMax /= 2;
                a = NPC.NewNPC(npc.GetSource_FromAI(), (int)npc.Center.X - 1350, (int)npc.Center.Y, CallUtils.TryGetNpcFromMod(RemnantOfTheAncientsMod.CalamityMod, "ProfanedGuardianHealer"));
                Main.npc[a].lifeMax /= 2;
            }
            if (npc.type == CallUtils.TryGetNpcFromMod(RemnantOfTheAncientsMod.CalamityMod, "Providence"))
            {
                Main.dayTime = false;
                Netcode.SyncWorld();
                npc.defDefense = 0;
                npc.Calamity().DR = 0;
            }   
        }


        int attackCounter = 0;
        List<NPC> NpcList = [];

        public override bool PreAI(NPC npc)
        {
            if (Reaper.ReaperMode)
            {
                if (DificultyUtils.InfernumMode)
                {
                    if (destroyer.Contains(npc.type))
                    {
                        npc.scale *= 2f;
                    }
                    AI(npc);
                }
                if (RemnantOfTheAncientsMod.CalamityMod != null /*&& npc.type == CallUtils.TryGetNpcFromMod(RemnantOfTheAncients.CalamityMod, "DankCreeper")*/)
                    PreAiCalamity(npc);
            }
            AI(npc);
            return base.PreAI(npc);
        }

        float ang = 0;
        [JITWhenModsEnabled("CalamityMod")]
        bool PreAiCalamity(NPC npc)
        {
            Player player = Main.player[npc.FindClosestPlayer()];

            if (npc.type == CallUtils.TryGetNpcFromMod(RemnantOfTheAncientsMod.CalamityMod, "DankCreeper"))
            {
                GetNPCList(CallUtils.TryGetNpcFromMod(RemnantOfTheAncientsMod.CalamityMod, "DankCreeper"), out List<NPC> CreepersList);
                GetPositionsAround(Main.player[npc.FindClosestPlayer()], 25 * 15f, CreepersList.Count, ang, out List<Vector2> Postions);
                npc.Center = Postions[CreepersList.IndexOf(npc)];
                ang += 0.08f;
                return false;
            }
            else if (DevourerOFGods.Contains(npc.type))
            {
                if (!UniqueToggledFlag)
                {

                    var a = NPC.NewNPC(npc.GetSource_FromAI(), (int)npc.position.X, (int)npc.position.Y, ModContent.NPCType<DevourerOfGodsArmor>());
                    Main.npc[a].netUpdate = true;
                    Main.npc[a].localAI[2] = npc.whoAmI;

                    UniqueToggledFlag = true;
                }

            }

            return base.PreAI(npc);
        }
        public NPC Owner = null;
        public override void AI(NPC npc)
        {
            List<int> ReaperAffectedNPCs =
            [
                NPCID.BrainofCthulhu,
                NPCID.Creeper,
                NPCID.QueenBee,
                NPCID.SkeletronHead,
                NPCID.SkeletronHand,
                NPCID.Deerclops,
                NPCID.WallofFlesh,
                NPCID.WallofFleshEye,
                NPCID.Spazmatism,
                NPCID.Retinazer,
                NPCID.SkeletronPrime,
                NPCID.PrimeCannon,
                NPCID.PrimeLaser,
                NPCID.PrimeSaw,
                NPCID.PrimeVice,
                NPCID.QueenSlimeBoss,
                NPCID.Plantera,
                NPCID.Golem,
                NPCID.DukeFishron,
                NPCID.HallowBoss,
                NPCID.CultistBoss,
                NPCID.MoonLordCore
            ];

            List<int> SkeletronPrimeHands =
            [
                NPCID.PrimeCannon,
                NPCID.PrimeLaser,
                NPCID.PrimeSaw,
                NPCID.PrimeVice
            ];



            if (Reaper.ReaperMode)
            {
                if (RemnantOfTheAncientsMod.CalamityMod != null)
                {
                    if (CalamityAffectedNPCs.Contains(npc.type))
                        CalamityAi(npc);
                }
                if (ReaperAffectedNPCs.Contains(npc.type))
                {
                    if (npc.target < 0 || npc.target >= 255 || Main.player[npc.target].dead || !Main.player[npc.target].active)
                    {
                        npc.TargetClosest();
                    }
                    Player player = Main.player[npc.target];
                    if (npc.type == NPCID.Creeper)
                    {
                        if (Owner == null)
                        {
                            foreach (NPC owner in Main.ActiveNPCs)
                            {
                                if (owner.type == NPCID.BrainofCthulhu)
                                {
                                    Owner = owner;
                                }
                            }
                        }

                        List<Vector2> Postions = GetPositionsAround(Owner, 25 * 15f, NPC.CountNPCS(NPCID.Creeper));
                        GetNPCList(NPCID.Creeper, out List<NPC> CreepersList);

                        for (int a = 0; a < NPC.CountNPCS(NPCID.Creeper); a++)
                        {
                            CreepersList[a].position = Postions[a];
                        }
                    }
                    if (npc.type == NPCID.QueenBee)
                    {
                        if (attackCounter < 600)
                        {
                            attackCounter++;
                        }
                        else
                        {
                            attackCounter = 0;
                        }

                        if (attackCounter == 200)
                        {
                            NPC.NewNPC(NPC.GetSource_None(), (int)npc.Center.X, (int)npc.Center.Y,NPCID.BigHornetFatty);
                        }
                        Point NpcFloor = Utils.ToTileCoordinates(npc.Center);
                        if (Main.tile[NpcFloor.X, NpcFloor.Y + 1].LiquidAmount > 0 && Main.tile[NpcFloor.X, NpcFloor.Y + 1].LiquidType == LiquidID.Honey)
                        {
                            if (npc.life < npc.lifeMax - 5)
                            {
                                npc.life += 5;
                                npc.HealEffect(5);
                            }
                        }
                    }
                    if (npc.type == NPCID.SkeletronHead)
                    {
                        for (int i = 0; i < Main.maxNPCs; i++)
                        {
                            if (Main.npc[i].type == NPCID.SkeletronHand)
                            {
                                if (Main.npc[i].dontTakeDamage != true)
                                {
                                    npc.dontTakeDamage = NPC.AnyNPCs(NPCID.SkeletronHand);
                                }
                            }
                        }
                    }
                    if (npc.type == NPCID.Deerclops)
                    {
                        Main.windSpeedCurrent = 2f;
                        Main.screenMaximized = false;

                        Main.StartRain();
                        Main.SyncRain();
                        if (!DearclopsDead)
                        {
                            if (Main.netMode == NetmodeID.MultiplayerClient)
                            {
                                RemnantPlayer.ApplyBuffToAllPlayers(BuffID.Hunger, 0, 0, 30);
                                RemnantPlayer.ApplyBuffToAllPlayers(BuffID.Darkness, 0, 0, 30);
                                RemnantPlayer.ApplyBuffToAllPlayers(BuffID.Blackout, 0, 0, 30);
                                player.dontStarveShader = true;
                            }
                            else
                            {
                                Main.player[npc.target].AddBuff(BuffID.Hunger, Utils1.FormatTimeToTick(0, 0, 0, 30));
                                Main.player[npc.target].AddBuff(BuffID.Darkness, Utils1.FormatTimeToTick(0, 0, 0, 30));
                                Main.player[npc.target].AddBuff(BuffID.Blackout, Utils1.FormatTimeToTick(0, 0, 0, 30));
                                Main.player[npc.target].dontStarveShader = true;
                            }
                        }
                    }
                    if (npc.type == NPCID.QueenSlimeBoss)
                    {
                        if (Main.netMode == NetmodeID.MultiplayerClient)
                        {
                            RemnantPlayer.ApplyBuffToAllPlayers(BuffID.WindPushed, 0, 0, 30);
                            RemnantPlayer.ApplyBuffToAllPlayers(BuffID.VortexDebuff, 0, 0, 30);
                            RemnantPlayer.ApplyBuffToAllPlayers(BuffID.QueenSlimeMount, 0, 0, 30);
                        }
                        else
                        {
                            Main.player[npc.target].AddBuff(BuffID.WindPushed, Utils1.FormatTimeToTick(0, 0, 0, 30));
                            Main.player[npc.target].AddBuff(BuffID.VortexDebuff, Utils1.FormatTimeToTick(0, 0, 0, 30));
                            Main.player[npc.target].AddBuff(BuffID.QueenSlimeMount, Utils1.FormatTimeToTick(0, 0, 0, 30));

                        }
                    }
                    if (npc.type == NPCID.WallofFleshEye)
                    {
                        if (attackCounter < 100)
                        {
                            attackCounter++;
                        }
                        else
                        {
                            attackCounter = 0;
                        }
                        if (attackCounter >= 50 && attackCounter <= 55)
                        {
                            targetProj = SearchTargetWallOfFlesh(npc);

                            if (targetProj != null)
                            {
                                GeneralDataStorage.InterceptionDroneTarget = targetProj;
                                if (targetProj.active)
                                {
                                    Vector2 velocity = targetProj.position - npc.position;
                                    updateRotation(npc, true);
                                    var p = Projectile.NewProjectile(Projectile.GetSource_None(), npc.position, velocity, ModContent.ProjectileType<InterceptionBream>(), 0, 0, Main.myPlayer);
                                    Main.projectile[p].Size = new(60f, 60f);
                                    Main.projectile[p].hostile = true;
                                    Main.projectile[p].friendly = false;
                                    Main.projectile[p].velocity *= 3;
                                }
                            }
                        }
                    }
                    if (npc.type == NPCID.Spazmatism)
                    {
                        if (npc.localAI[2] >= 1f)
                        {
                            if (npc.ai[2] == 100f || npc.ai[2] == 50f || npc.ai[2] == 150f || npc.ai[2] == 0f || npc.ai[2] == 200f)
                            {
                                for (int i = 0; i < 3; i++)
                                {
                                    ShootIa(npc, npc.damage, ProjectileID.CursedFlameHostile, player, 10f + i, new(-0.5f + i, 0.5f + i), i * 10);
                                }
                            }
                        }
                    }
                    if (npc.type == NPCID.Retinazer)
                    {

                        if (NPC.AnyNPCs(NPCID.Spazmatism))
                        {
                            if (attackCounter < 200)
                            {
                                attackCounter++;
                            }
                            else
                            {
                                attackCounter = 0;
                            }

                            if (attackCounter == 100)
                            {
                                for (int i = 0; i < Main.maxNPCs; i++)
                                {
                                    if (Main.npc[i].type == NPCID.Spazmatism)
                                    {
                                        updateRotation(npc, false);
                                        var p = ShootIa(npc, 10, ModContent.ProjectileType<HealthBream>(), Main.npc[i], 10f);
                                        Main.projectile[p].Size = new(60f, 60f);
                                    }
                                }
                            }
                        }
                    }

                    bool[] IsInmune = new bool[4];
                
                    if (npc.type == NPCID.SkeletronPrime)
                    {
                        int handAmmount = 8;

                        if (NPC.AnyNPCs(NPCID.PrimeCannon) || NPC.AnyNPCs(NPCID.PrimeLaser) || NPC.AnyNPCs(NPCID.PrimeSaw) || NPC.AnyNPCs(NPCID.PrimeVice))
                        {
                            for (int a = 0; a < Main.maxNPCs; a++)
                            {
                                for (int i = 0; i < SkeletronPrimeHands.Count; i++)
                                {
                                    if (SkeletronPrimeHands.Contains(Main.npc[a].type) && !Main.npc[a].dontTakeDamage)
                                    {
                                        IsInmune[i] = true;
                                    }
                                    else
                                    {
                                        IsInmune[i] = false;
                                    }
                                }
                            }
                            if (!IsInmune[0] && !IsInmune[1] && !IsInmune[2] && !IsInmune[3])
                            {
                                npc.dontTakeDamage = true;
                            }
                            else
                            {
                                npc.dontTakeDamage = false;
                            }
                        }
                        else
                        {
                            npc.dontTakeDamage = false;
                        }

                        if (!npc.dontTakeDamage)
                        {
                            if (npc.life >= npc.lifeMax * 0.75f)
                            {
                                if (attackCounter < 200)
                                {
                                    attackCounter++;
                                }
                                else
                                {
                                    attackCounter = 0;
                                }
                                if (attackCounter == 180)
                                {
                                    for (int i = -2; i < 5; i++)
                                    {
                                        Vector2 pos = (player.Center - npc.Center).RotatedBy(i * 0.2f);
                                        int proj = Projectile.NewProjectile(Projectile.GetSource_None(), npc.Center, pos, ProjectileID.PinkLaser, 20, 1, Main.myPlayer);
                                        Main.projectile[proj].velocity /= 2;
                                    }
                                }
                            }
                            else
                            {
                                if (primePhase <= 4) primePhase++;
                                if (primePhase < 5)
                                {
                                    int handId = NPCID.PrimeSaw;
                                    switch (primePhase)
                                    {
                                        case 1:
                                            handId = NPCID.PrimeLaser;
                                            break;
                                        case 2:
                                            handId = NPCID.PrimeCannon;
                                            break;
                                        case 3:
                                            handId = NPCID.PrimeVice;
                                            break;
                                        case 4:
                                            handId = NPCID.PrimeSaw; 
                                            break;
                                    }
                                    for (int i = -(int)(handAmmount / 2); i < (int)(handAmmount / 2); i++)
                                    {
                                        var a = NPC.NewNPC(npc.GetSource_FromAI(), (int)npc.Center.X, (int)npc.Center.Y, handId, Main.myPlayer);
                                        Main.npc[a].ai[0] = i;
                                        Main.npc[a].ai[1] = npc.whoAmI;
                                        Main.npc[a].target = npc.target;
                                        Main.npc[a].netUpdate = true;
                                    }
                                    
                                }
                            }
                        }
                    }
                    if (npc.type == NPCID.Plantera)
                    {
                        int maxHook = GetHootAmount(npc);
                        if (NpcList.Count > maxHook)
                        {
                            NpcList[NpcList.Count - 1].dontTakeDamage = false;
                            NpcList[NpcList.Count - 1].life = 0;
                            NpcList[NpcList.Count - 1].scale = 5;
                            NpcList.RemoveAt(NpcList.Count - 1);
                        }
                        else if (NpcList.Count == 0)
                        {
                            for (int i = 0; i < Main.maxNPCs; i++)
                            {
                                if (Main.npc[i].type == NPCID.PlanterasHook && Main.npc[i].active)
                                {
                                    NpcList.Add(Main.npc[i]);
                                }
                            }
                        }

                        if (NPC.AnyNPCs(NPCID.PlanterasHook))
                        {
                            for (int i = 0; i < Main.maxNPCs; i++)
                            {
                                if (Main.npc[i].type == NPCID.PlanterasHook)
                                {
                                    if (attackCounter == 100)
                                    {
                                        Main.npc[i].velocity = player.position + new Vector2(Main.rand.Next(-6, 6) * 16, Main.rand.Next(-6, 6) * 16) - npc.position;
                                    }
                                }
                            }
                        }
                        if (attackCounter < 110)
                        {
                            attackCounter++;
                        }
                        else
                        {
                            attackCounter = 0;
                        }
                    }
                    if (npc.type == NPCID.DukeFishron)
                    {
                        if (attackCounter < 400)
                        {
                            attackCounter++;
                        }
                        else
                        {
                            attackCounter = 0;
                            if (NPC.CountNPCS(ModContent.NPCType<MiniDuke>()) < 3)
                            {
                                int n = NPC.NewNPC(npc.GetSource_FromAI(), (int)npc.position.X, (int)npc.position.Y + (Main.rand.Next(-4, 4) * 16), ModContent.NPCType<MiniDuke>());
                                Main.npc[n].lifeMax = 1000;
                                Main.npc[n].life = 1000;
                            }
                        }
                    }
                    if (npc.type == NPCID.HallowBoss)
                    {
                        Main.dayTime = true;
                        Main.time = 27000.0;
                        Netcode.SyncWorld();
                    }
                    if (npc.type == NPCID.CultistBoss)
                    {
                        if (!npc.dontTakeDamage)
                        {
                            if (attackCounter == 100)
                            {
                                for (int i = 0; i < 5; i++)
                                {
                                    int p = Projectile.NewProjectile(npc.GetSource_FromAI(), npc.position + new Vector2(i * 50, 0), (Main.player[npc.target].position - npc.position).RotatedBy(i * 10), ProjectileID.CultistBossFireBall, 30, 0, 255);
                                    Main.projectile[p].velocity /= 2;
                                }
                            }
                            if (attackCounter == 110)
                            {
                                for (int i = 0; i < 5; i++)
                                {
                                    int p = Projectile.NewProjectile(npc.GetSource_FromAI(), npc.position + new Vector2(i * 50, 0), (Main.player[npc.target].position - npc.position).RotatedBy(i * 10), ProjectileID.CultistBossFireBallClone, 30, 0, 255);
                                    Main.projectile[p].velocity /= 4;
                                }
                            }
                        }
                        if (attackCounter < 200)
                        {
                            attackCounter++;
                        }
                        else
                        {
                            attackCounter = 0;
                        }
                    }
                    if (npc.type == NPCID.MoonLordCore)
                    {
                        if (Main.netMode == NetmodeID.MultiplayerClient)
                        {
                            RemnantPlayer.ApplyBuffToAllPlayers(BuffID.Horrified, 0, 0, 30);
                            RemnantPlayer.ApplyBuffToAllPlayers(BuffID.Weak, 0, 0, 30);
                            RemnantPlayer.ApplyBuffToAllPlayers(BuffID.Blackout, 0, 0, 30);
                            RemnantPlayer.ApplyBuffToAllPlayers(BuffID.ChaosState, 0, 0, 30);
                            RemnantPlayer.ApplyBuffToAllPlayers(BuffID.NoBuilding, 0, 0, 30);
                            if (NPC.AnyNPCs(NPCID.Dryad))
                            {
                                RemnantPlayer.ApplyBuffToAllPlayers(BuffID.DryadsWard, 0, 0, 30);
                            }
                        }
                        else
                        {
                            Main.player[npc.target].AddBuff(BuffID.Horrified, Utils1.FormatTimeToTick(0, 0, 0, 1));
                            Main.player[npc.target].AddBuff(BuffID.Weak, Utils1.FormatTimeToTick(0, 0, 0, 1));
                            Main.player[npc.target].AddBuff(BuffID.Blackout, Utils1.FormatTimeToTick(0, 0, 0, 1));
                            Main.player[npc.target].AddBuff(BuffID.ChaosState, Utils1.FormatTimeToTick(0, 0, 0, 1));
                            Main.player[npc.target].AddBuff(BuffID.NoBuilding, Utils1.FormatTimeToTick(0, 0, 0, 1));
                            if (NPC.AnyNPCs(NPCID.Dryad))
                            {
                                Main.player[npc.target].AddBuff(BuffID.DryadsWard, Utils1.FormatTimeToTick(0, 0, 0, 30));
                            }
                        }
                        if (attackCounter == 110)
                        {
                            for (int i = 0; i < 2; i++)
                            {
                                int p = NPC.NewNPC(npc.GetSource_FromAI(), (int)npc.position.X, (int)npc.position.Y, NPCID.SolarCrawltipedeHead);
                                Main.npc[p].lifeMax = 100;
                                Main.npc[p].life = 50;
                                Main.npc[p].defDefense = 0;
                                Main.npc[p].defense = 0;
                                Main.npc[p].dontTakeDamage = false;
                            }
                        }
                    }
                    if (attackCounter < 800)
                    {
                        attackCounter++;
                    }
                    else
                    {
                        attackCounter = 0;
                    }
                }
            }

            base.AI(npc);



        }
        bool UniqueToggledFlag = false;
        bool SecondUniqueToggledFlag = false;

        [JITWhenModsEnabled("CalamityMod")]
        public void CalamityAi(NPC npc)
        {
            if (npc.type == CallUtils.TryGetNpcFromMod(RemnantOfTheAncientsMod.CalamityMod, "DesertScourgeHead"))
            {
                if (npc.life <= MathUtils.GetValueFromPorcentage(npc.lifeMax, 70) && !UniqueToggledFlag)
                {
                    NPC.SpawnOnPlayer(npc.FindClosestPlayer(), CallUtils.TryGetNpcFromMod(RemnantOfTheAncientsMod.CalamityMod, "DesertNuisanceHeadYoung"));
                    UniqueToggledFlag = true;
                }
            }
            if (npc.type == CallUtils.TryGetNpcFromMod(RemnantOfTheAncientsMod.CalamityMod, "Crabulon"))
            {
                if (attackCounter++ >= 200)
                {
                    int maxProj = 4;
                    for (int i = 0; i < maxProj / 2; i++)
                    {
                        Vector2 velocity = (Main.player[npc.target].Center - npc.Center).RotatedBy((360 / maxProj / 2) * i);
                        var p = Projectile.NewProjectile(Projectile.GetSource_None(), npc.Center, velocity, CallUtils.TryGetProjectileFromMod(RemnantOfTheAncientsMod.CalamityMod, "MushBomb"), 10, 10, Main.myPlayer);
                        Main.projectile[p].timeLeft = 60;
                        Main.projectile[p].ignoreWater = true;
                    }
                    for (int i = 0; i < maxProj / 2; i++)
                    {
                        Vector2 velocity = (Main.player[npc.target].Center - npc.Center).RotatedBy((-360 / maxProj / 2) * i);
                        var p = Projectile.NewProjectile(Projectile.GetSource_None(), npc.Center, velocity, CallUtils.TryGetProjectileFromMod(RemnantOfTheAncientsMod.CalamityMod, "MushBomb"), 10, 10, Main.myPlayer);
                        Main.projectile[p].timeLeft = 60;
                        Main.projectile[p].ignoreWater = true;
                    }
                    attackCounter = 0;
                }
                if (npc.life <= MathUtils.GetValueFromPorcentage(npc.lifeMax, 50) && !UniqueToggledFlag)
                {
                    for (int i = 0; i <= 2; i++)
                    {
                        NPC.SpawnOnPlayer(npc.FindClosestPlayer(), NPCID.GiantFungiBulb);
                    }
                    for (int i = 0; i < 5; i++)
                    {
                        NPC.SpawnOnPlayer(npc.FindClosestPlayer(), NPCID.ZombieMushroomHat);
                        NPC.NewNPC(npc.GetSource_FromAI(), (int)npc.Center.X + Main.rand.Next(-100, 100), (int)npc.Center.Y, NPCID.ZombieMushroomHat);
                    }
                    UniqueToggledFlag = true;
                }
            }
            if (npc.type == perforatorWormMedium[1])
            {
                if (attackCounter++ >= 300)
                {
                    var p = Projectile.NewProjectile(Projectile.GetSource_None(), npc.Center, new Vector2(5, 0).RotatedBy(npc.rotation), CallUtils.TryGetProjectileFromMod(RemnantOfTheAncientsMod.CalamityMod, "IchorBlob"), npc.damage, 10, Main.myPlayer, 0f, Main.LocalPlayer.Center.Y);
                    Main.projectile[p].hostile = true;
                    Main.projectile[p].friendly = false;
                    p = Projectile.NewProjectile(Projectile.GetSource_None(), npc.Center, new Vector2(-5, 0).RotatedBy(npc.rotation), CallUtils.TryGetProjectileFromMod(RemnantOfTheAncientsMod.CalamityMod, "IchorBlob"), npc.damage, 10, Main.myPlayer, 0f, Main.LocalPlayer.Center.Y);
                    Main.projectile[p].hostile = true;
                    Main.projectile[p].friendly = false;
                    attackCounter = 0;
                }
            }

            if (npc.type == CallUtils.TryGetNpcFromMod(RemnantOfTheAncientsMod.CalamityMod, "Cryogen"))
            {
                float lifeRatio = npc.life / (float)npc.lifeMax;
                bool phase5 = lifeRatio < (DificultyUtils.Death ? 0.5f : DificultyUtils.Revengeance ? 0.45f : 0.3f);
                if (phase5 && !UniqueToggledFlag && npc.boss)
                {
                    int n = NPC.NewNPC(npc.GetSource_FromAI(), (int)Main.player[npc.target].Center.X + Main.rand.Next(2, 10) * 10 * (int)Math.Pow(-1, Main.rand.Next(2)), (int)Main.player[npc.target].Center.Y + Main.rand.Next(2, 10) * 10 * (int)Math.Pow(-1, Main.rand.Next(2)), CallUtils.TryGetNpcFromMod(RemnantOfTheAncientsMod.CalamityMod, "Cryogen"));
                    Main.npc[n].lifeMax = 1000;
                    Main.npc[n].boss = false;
                    Main.npc[n].life = 1000;
                    Main.npc[n].alpha = 200;
                    Main.npc[n].scale /= 2;
                    UniqueToggledFlag = true;
                }
            }
            if (npc.type == CallUtils.TryGetNpcFromMod(RemnantOfTheAncientsMod.CalamityMod, "AquaticScourgeHead"))
            {
                Main.player[npc.target].AddBuff(CallUtils.TryGetBuffFromMod(RemnantOfTheAncientsMod.CalamityMod, "DoGExtremeGravity"), 1);
                Main.player[npc.target].AddBuff(BuffID.Darkness, 1);
                Main.player[npc.target].AddBuff(BuffID.Blackout, 1);
                foreach (var p in Main.player)
                {
                    if (p != Main.player[npc.target] && p.Distance(npc.Center) < 2000)
                        Main.player[npc.FindClosestPlayer()].AddBuff(BuffID.Confused, 1);
                }
            }
            if (npc.type == CallUtils.TryGetNpcFromMod(RemnantOfTheAncientsMod.CalamityMod, "BrimstoneElemental"))
            {
                BrimstoneElemental brimmy = npc.ModNPC<BrimstoneElemental>();
                if (npc.ai[0] == 1f)
                {
                    // Avoid cheap bullshit
                    npc.damage = 0;

                    npc.chaseable = true;
                    Vector2 position = new Vector2(npc.ai[1] * 16f - (npc.width / 2), npc.ai[2] * 16f - (npc.height / 2));
                    for (int m = 0; m < 5; m++)
                    {
                        int dust = Dust.NewDust(position, npc.width, npc.height, DustID.LifeDrain, 0f, -1f, 90, default, 2f);
                        Main.dust[dust].noGravity = true;
                        Main.dust[dust].fadeIn = 1f;
                    }
                    npc.alpha += DificultyUtils.BossRush ? 15 : DificultyUtils.Death ? 5 : DificultyUtils.Revengeance ? 4 : Main.expertMode ? 3 : 2;
                    if (npc.alpha >= 255)
                    {
                        int spawnType = brimmy.currentMode == 3 ? NPCID.AngryNimbus : ModContent.NPCType<Brimling>();
                        int enemyCount = brimmy.currentMode == 3 ? 3 : 3; // 3 angry nimbi if cloud, otherwise 1 brimling
                        if (Main.netMode != NetmodeID.MultiplayerClient && NPC.CountNPCS(spawnType) < (DificultyUtils.Death ? 1 : 2) && DificultyUtils.Revengeance && brimmy.currentMode != 2) // dont spawn anything if gfb rare sand
                        {
                            for (int i = 0; i < enemyCount; i++)
                                NPC.NewNPC(npc.GetSource_FromAI(), (int)npc.Center.X, (int)npc.Center.Y, spawnType);
                        }
                        SoundEngine.PlaySound(SoundID.Item8, npc.Center);
                        npc.alpha = 255;
                        npc.position = position;
                        for (int n = 0; n < 15; n++)
                        {
                            int warpDust = Dust.NewDust(npc.position, npc.width, npc.height, DustID.LifeDrain, 0f, -1f, 90, default, 3f);
                            Main.dust[warpDust].noGravity = true;
                        }
                        npc.ai[0] = 2f;
                        npc.netUpdate = true;
                    }
                }
            }
            if (npc.type == CallUtils.TryGetNpcFromMod(RemnantOfTheAncientsMod.CalamityMod, "CalamitasClone"))
            {
                int count = NPC.CountNPCS(CallUtils.TryGetNpcFromMod(RemnantOfTheAncientsMod.CalamityMod, "SoulSeeker"));
                float lifePercentage = MathUtils.GetPorcentage(npc.life, npc.lifeMax);
                int maxCount = (int)(7 + ((100 - lifePercentage) / 10));
                if (count < maxCount)
                {
                    if (attackCounter++ >= 200 - ((100 - lifePercentage)))
                    {
                        NPC.SpawnOnPlayer(npc.target, CallUtils.TryGetNpcFromMod(RemnantOfTheAncientsMod.CalamityMod, "SoulSeeker"));
                        attackCounter = 0;
                    }
                }
            }
            if (npc.type == CallUtils.TryGetNpcFromMod(RemnantOfTheAncientsMod.CalamityMod, "GreatSandShark"))
            {
                if (attackCounter++ >= 200)
                {
                    Main.player[npc.target].velocity.X += -Main.player[npc.target].velocity.X * 2f;
                    attackCounter = 0;
                }
            }
            if (npc.type == CallUtils.TryGetNpcFromMod(RemnantOfTheAncientsMod.CalamityMod, "AstrumAureus"))
            {
                if (npc.oldVelocity.Y > 3f && npc.velocity.Y <= 0f)
                {
                    int ammount = 3;
                    for (int i = 0; i < ammount; i++)
                    {
                        var p = Projectile.NewProjectile(npc.GetSource_FromAI(), npc.position + new Vector2(npc.width / ammount * i, npc.scale), Vector2.Zero, ModContent.ProjectileType<FrozenNovaExplosion>(), 0, 0, Main.myPlayer);
                        Main.projectile[p].ai[2] = 5;
                        Main.projectile[p].hostile = true;
                        Main.projectile[p].friendly = false;
                        Main.projectile[p].Size = new(400, 400);
                        Main.projectile[p].scale = 6f;
                        Main.projectile[p].GetAlpha(Color.Purple);
                    }
                }
            }
            if (npc.type == CallUtils.TryGetNpcFromMod(RemnantOfTheAncientsMod.CalamityMod, "PlaguebringerGoliath"))
            {

                if (!HasElectricShield)
                {
                    if (attackCounter++ >= Utils1.FormatTimeToTick(0, 0, 0, 20))
                    {
                        HasElectricShield = true;
                        electricShieldLife = electricShieldLifeMax;
                        attackCounter = 0;
                    }

                }

                if (HasElectricShield)
                {
                    foreach (var proj in Main.projectile)
                    {
                        if (proj.active && !proj.hostile && proj.friendly && !proj.minion)
                        {
                            float distance = npc.Distance(proj.Center);
                            if (distance < 194f)
                            {
                                proj.friendly = false;
                                proj.hostile = true;
                                proj.velocity *= -1;

                                if (electricShieldLife <= 0)
                                {
                                    HasElectricShield = false;
                                }
                                else
                                {
                                    electricShieldLife--;
                                }
                            }
                        }
                    }
                }
            }
            if (npc.type == CallUtils.TryGetNpcFromMod(RemnantOfTheAncientsMod.CalamityMod, "Bumblefuck"))
            {
                npc.velocity *= 1.02f;
            }
            if (npc.type == CallUtils.TryGetNpcFromMod(RemnantOfTheAncientsMod.CalamityMod, "CeaselessVoid"))
            {

                if (Main.netMode == NetmodeID.MultiplayerClient)
                {
                    foreach (Player player in Main.player)
                    {
                        if (player.whoAmI == npc.target && player.active && !player.dead && npc.Center.Distance(player.position) < 1000)
                        {
                            player.AddBuff(BuffID.Slow, 120);
                        }
                    }
                }
                else
                {
                    Main.LocalPlayer.AddBuff(ModContent.BuffType<FogOfVoid>(), 120);
                }
            }
            else if (npc.type == CallUtils.TryGetNpcFromMod(RemnantOfTheAncientsMod.CalamityMod, "StormWeaverHead"))
            {
                if (Main.LocalPlayer.infernoCounter % 20 == 0)
                {
                    var a = Projectile.NewProjectile(npc.GetSource_FromAI(), npc.Center, (npc.velocity * 0.5f).RotatedByRandom(0.12f), CallUtils.TryGetProjectileFromMod(RemnantOfTheAncientsMod.CalamityMod, "ElementalFire"), 50, 0, Main.myPlayer);
                    Main.projectile[a].friendly = false;
                    Main.projectile[a].hostile = true;
                }
            }
            else if (npc.type == CallUtils.TryGetNpcFromMod(RemnantOfTheAncientsMod.CalamityMod, "Signus"))
            {
                if (UniqueToggledFlag)
                {
                    if (Timmer1 < Utils1.FormatTimeToTick(0, 0, 0, 30))
                    {
                        npc.position = Main.player[npc.target].Center - new Vector2(10, 600);
                        npc.alpha += npc.alpha < 255 ? 1 : 0;
                        npc.dontTakeDamage = true;
                        Timmer1++;

                        if (attackCounter++ >= Utils1.FormatTimeToTick(0, 0, 0, 4))
                        {

                            Vector2 position1 = new(Main.player[npc.target].Center.X + 1200, Main.player[npc.target].Center.Y);
                            Vector2 position2 = new(Main.player[npc.target].Center.X - 1200, Main.player[npc.target].Center.Y);
                            Projectile.NewProjectile(npc.GetSource_FromAI(), position1, new Vector2(-10, 0), ModContent.ProjectileType<SignusMimic>(), 200, 0, Main.myPlayer);
                            Projectile.NewProjectile(npc.GetSource_FromAI(), position2, new Vector2(10, 0), ModContent.ProjectileType<SignusMimic>(), 200, 0, Main.myPlayer);
                            attackCounter = 0;
                        }
                    }
                    else
                    {
                        npc.dontTakeDamage = false;
                        npc.alpha = 10;
                    }
                }
                if (!UniqueToggledFlag && npc.life <= npc.lifeMax / 2)
                {
                    UniqueToggledFlag = true;
                }
            }
            else if (npc.type == CallUtils.TryGetNpcFromMod(RemnantOfTheAncientsMod.CalamityMod, "Polterghast"))
            {
                if (UniqueToggledFlag)
                {
                    if (!SecondUniqueToggledFlag && npc.life <= npc.lifeMax / 4)
                    {
                        SecondUniqueToggledFlag = true;
                        for (int i = 0; i < 10; i++)
                            NPC.NewNPC(NPC.GetSource_None(), (int)npc.position.X + Main.rand.Next(-30, 30), (int)npc.position.Y + Main.rand.Next(-100, 100), CallUtils.TryGetNpcFromMod(RemnantOfTheAncientsMod.CalamityMod, "PolterPhantom"));
                    }
                }
                else
                {
                    if (npc.life <= npc.lifeMax / 2)
                    {
                        UniqueToggledFlag = true;
                        NPC.NewNPC(NPC.GetSource_None(), (int)npc.position.X + Main.rand.Next(-30, 30), (int)npc.position.Y + Main.rand.Next(-100, 100), CallUtils.TryGetNpcFromMod(RemnantOfTheAncientsMod.CalamityMod, "PolterPhantom"));
                    }
                }
            }
            else if (npc.type == CallUtils.TryGetNpcFromMod(RemnantOfTheAncientsMod.CalamityMod, "OldDuke"))
            {
                if (attackCounter < 400)
                {
                    attackCounter++;
                }
                else
                {
                    attackCounter = 0;
                    if (NPC.CountNPCS(ModContent.NPCType<MiniDuke>()) < 7)
                    {
                        int n = NPC.NewNPC(npc.GetSource_FromAI(), (int)npc.position.X, (int)npc.position.Y + (Main.rand.Next(-4, 4) * 16), ModContent.NPCType<MiniDuke>());
                        Main.npc[n].lifeMax = 5000;
                        Main.npc[n].life = 5000;
                    }
                }
            }
          
            else if (npc.type == CallUtils.TryGetNpcFromMod(RemnantOfTheAncientsMod.CalamityMod, "Yharon"))
            {
                if (npc.life <= MathUtils.GetValueFromPorcentage(npc.lifeMax, 40) && !UniqueToggledFlag)
                {
                    //Calamity revisar
                    for (int i = 0; i < 2; i++)
                    {
                        var a = NPC.NewNPC(npc.GetSource_FromAI(), (int)npc.Center.X, (int)npc.Center.Y, CallUtils.TryGetNpcFromMod(RemnantOfTheAncientsMod.CalamityMod, "Bumblefuck"));
                        Main.npc[a].lifeMax /= 4;
                        Main.npc[a].life /= 4;
                    }
                    UniqueToggledFlag = true;
                }
            }
            if (npc.type == CallUtils.TryGetNpcFromMod(RemnantOfTheAncientsMod.CalamityMod, "AresBody"))
            {
                if (npc.alpha < 255)
                {
                    electricShieldLifeMax = 50;
                    if (!HasElectricShield)
                    {
                        if (attackCounter++ >= Utils1.FormatTimeToTick(0, 0, 0, 2))
                        {
                            HasElectricShield = true;
                            electricShieldLife = electricShieldLifeMax;
                            attackCounter = 0;
                        }

                    }

                    if (HasElectricShield)
                    {
                        foreach (var proj in Main.projectile)
                        {
                            if (proj.active && !proj.hostile && proj.friendly && !proj.minion)
                            {
                                float distance = npc.Distance(proj.Center);
                                if (distance < 194f)
                                {
                                    proj.friendly = false;
                                    proj.hostile = true;
                                    proj.velocity *= -1;

                                    if (electricShieldLife <= 0)
                                    {
                                        HasElectricShield = false;
                                    }
                                    else
                                    {
                                        electricShieldLife--;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            else if (npc.type == CallUtils.TryGetNpcFromMod(RemnantOfTheAncientsMod.CalamityMod, "Artemis"))
            {
                if (npc.alpha < 255)
                {
                    if (attackCounter++ >= Utils1.FormatTimeToTick(0, 0, 0, 30))
                    {
                        // Projectile.NewProjectile(npc.GetSource_FromAI(), npc.Center + new Vector2(0, 50), new Vector2(10, 0) * npc.direction, ProjectileID.WoodenArrowHostile, 60, 0f, npc.whoAmI);
                        // Projectile.NewProjectile(npc.GetSource_FromAI(), npc.Center + new Vector2(0, -50), new Vector2(10,0) * npc.direction, ProjectileID.Bullet, 60,0f,npc.whoAmI);
                        for (int i = 0; i < 5; i++)
                        {
                            var n = NPC.NewNPC(npc.GetSource_FromAI(), (int)npc.Center.X + Main.rand.Next(-500, 500), (int)npc.Center.Y + Main.rand.Next(-500, 500), NPCID.Retinazer, npc.whoAmI);
                            Main.npc[n].scale = 0.3f;
                            Main.npc[n].lifeMax /= 6;
                            Main.npc[n].life /= 6;
                            Main.npc[n].boss = false;
                        }
                        attackCounter = 0;
                    }
                }
            }
            else if (npc.type == CallUtils.TryGetNpcFromMod(RemnantOfTheAncientsMod.CalamityMod, "SupremeCalamitas"))
            {
                if (attackCounter >=  Utils1.FormatTimeToTick(0, 0, 0, 1))
                {
                    foreach (var proj in Main.projectile)
                    {
                        if (proj.active)
                        {
                            if (npc.Center.Distance(proj.Center) < 100 && proj.friendly && !proj.hostile)
                            {
                                Vector2 fixedVelocity = proj.velocity;
                                if(proj.velocity.X > 20) 
                                    fixedVelocity.X = 20;
                                if (proj.velocity.Y > 20) 
                                    fixedVelocity.Y = 20;
                                if (proj.velocity.X < -20)
                                    fixedVelocity.X = -20;
                                if (proj.velocity.Y < -20)
                                    fixedVelocity.Y = -20;

                                if (Math.Abs(proj.velocity.Y) > Math.Abs(proj.velocity.X))
                                    npc.velocity = fixedVelocity - new Vector2(15, 0);
                                else
                                    npc.velocity = fixedVelocity - new Vector2(0, 15);
                                //npc.velocity.Normalize();
                                attackCounter = 0;
                            }
                        }
                    }
                }
                else
                {
                    attackCounter++;
                }
                
            }
            if (AstrumDeus.Contains(npc.type))
            {
                npc.alpha = 200;
            }
        } 

        public override bool PreKill(NPC npc)
        {
            if (RemnantOfTheAncientsMod.CalamityMod != null)
            {
                if (npc.type == CallUtils.TryGetNpcFromMod(RemnantOfTheAncientsMod.CalamityMod, "Cryogen"))
                {
                    if (!npc.boss)
                        return false;
                }
            }
            if(npc.type == ModContent.NPCType<InfernalTyrantHead>())
            {
                if (!npc.boss)
                    return false;
            }
            if(npc.type == NPCID.Retinazer)
            {
                if (!npc.boss)
                    return false;
            }
            return base.PreKill(npc);
        }
        private Projectile SearchTargetWallOfFlesh(NPC npc)
        {
            for (int i = 0; i < Main.maxProjectiles; i++)
            {
                float dist = npc.Distance(Main.projectile[i].position);
                float viewRange = 100 * 14f;
                if (dist < viewRange)
                {
                    if (Main.projectile[i].active && !Main.projectile[i].minion && !Main.projectile[i].sentry)
                    {
                        if (!Main.projectile[i].hostile && Main.projectile[i].friendly)
                        {
                            Projectile p = Main.projectile[i];
                            GeneralDataStorage.InterceptionRayEnemyTarget = p;
                            return p;
                        }
                    }
                }
            }
            return null;
        }
        public int GetHootAmount(NPC npc)
        {
            if (npc.life <= MathUtils.GetValueFromPorcentage(npc.lifeMax, 50))
            {
                if (npc.life <= MathUtils.GetValueFromPorcentage(npc.lifeMax, 25))
                {
                    return 1;
                }
                return 2;
            }
            else return 3;
        }
        private List<NPC> GetNPCList(int npcid, out List<NPC> CreepersList)
        {
            CreepersList = [];
            for (int i = 0; i < Main.maxNPCs; i++)
            {
                if (Main.npc[i].type == npcid)
                {
                    CreepersList.Add(Main.npc[i]);
                }
            }
            return CreepersList;
        }
        List<Vector2> GetPositionsAround(NPC npc, float radius, int numPositions)
        {
            if (npc == null) return null;

            List<Vector2> positions = [];

            float angleSpacing = MathHelper.TwoPi / numPositions;
            
            for (int i = 0; i < numPositions; i++)
            {
                float angle = angleSpacing * i;

                float x = radius * MathF.Cos(angle);
                float y = radius * MathF.Sin(angle);

                Vector2 position = npc.Center + new Vector2(x, y);

                positions.Add(position);
            }

            return positions;
        }
        List<Vector2> GetPositionsAround(Player player, float radius, int numPositions, out List<Vector2> positions)
        {
            positions = [];

            float angleSpacing = MathHelper.TwoPi / numPositions;

            for (int i = 0; i < numPositions; i++)
            {
                float angle = angleSpacing * i;

                float x = radius * MathF.Cos(angle);
                float y = radius * MathF.Sin(angle);

                Vector2 position = player.Center + new Vector2(x, y);

                positions.Add(position);
            }

            return positions;
        }
        List<Vector2> GetPositionsAround(Player player, float radius, int numPositions, float angle, out List<Vector2> positions)
        {
            positions = [];

            float angleSpacing = (MathHelper.TwoPi) / numPositions;

            for (int i = 0; i < numPositions; i++)
            {
                float Angle = (angleSpacing  * i) + angle;

                float x = radius * MathF.Cos(Angle);
                float y = radius * MathF.Sin(Angle);

                Vector2 position = player.Center + new Vector2(x, y);

                positions.Add(position);
            }

            return positions;
        }

        public static NPC target;
        public static Projectile targetProj;
        private void updateRotation(NPC npc, bool p)
        {
            if (p)
            {
                if (SearchForProjectileTarget(npc) != null)
                {
                    targetProj = SearchForProjectileTarget(npc);
                    npc.rotation = (npc.Center - targetProj.Center).ToRotation();
                }
            }
            else
            {
                if (SearchForTeamTarget(npc) != null)
                {
                    target = SearchForTeamTarget(npc);
                    npc.rotation = (npc.Center - target.Center).ToRotation();
                }
            }
        }

        public float Range = 40f * 16f;
        private NPC SearchForTeamTarget(NPC npc)
        {
            for (int i = 0; i < Main.maxNPCs; i++)
            {
                if (Main.npc[i].friendly == npc.friendly && Main.npc[i].active && Main.npc[i] != npc)
                {
                    float distance = Vector2.Distance(npc.position, Main.npc[i].position);
                    if (distance < Range)
                    {
                        // var a = Main.projectile[i].Name;
                        return Main.npc[i];
                    }
                }
            }
            return null;
        }
        private Projectile SearchForProjectileTarget(NPC npc)
        {
            for (int i = 0; i < Main.maxProjectiles; i++)
            {
                if (Main.projectile[i].friendly == !npc.friendly && Main.projectile[i].active)
                {
                    float distance = Vector2.Distance(npc.position, Main.npc[i].position);
                    if (distance < Range)
                    {
                        // var a = Main.projectile[i].Name;
                        return Main.projectile[i];
                    }
                }
            }
            return null;
        }
        public static void ShootIa(NPC npc, int dammage, int type, Player player, float Speed, Vector2 position, float angle)
        {
            Vector2 NpcPosition = new Vector2(npc.position.X + (npc.width / 2), npc.position.Y + (npc.height / 2));
            float rotation = (float)Math.Atan2(NpcPosition.Y - (player.position.Y + (player.height * position.X)), NpcPosition.X - (player.position.X + (player.width * position.Y)));
            Vector2 direction;
            direction.X = (float)(Math.Cos(rotation) * Speed * -1);
            direction.Y = (float)(Math.Sin(rotation) * Speed * -1);
            direction.RotatedBy(angle);
            Projectile.NewProjectile(npc.GetSource_FromAI(), NpcPosition, direction, type, dammage, 0f, Main.myPlayer);
        }
        public static void ShootIa(NPC npc, int dammage, int type, NPC target, float Speed, Vector2 position, float angle)
        {
            Vector2 NpcPosition = new Vector2(npc.position.X /*+ (npc.width / 2)*/, npc.position.Y /*+ (npc.height / 2)*/);
            float rotation = (float)Math.Atan2(NpcPosition.Y - (target.position.Y + (target.height * position.X)), NpcPosition.X - (target.position.X + (target.width * position.Y)));
            Vector2 direction;
            direction.X = (float)(Math.Cos(rotation) * Speed * -1);
            direction.Y = (float)(Math.Sin(rotation) * Speed * -1);
            direction.RotatedBy(angle);
            var p = Projectile.NewProjectile(npc.GetSource_FromAI(), NpcPosition, direction, type, dammage, 0f, Main.myPlayer);
            Main.projectile[p].hostile = true;
            Main.projectile[p].friendly = false;
        }
        public static int ShootIa(NPC npc, int dammage, int type, NPC target, float Speed)
        {
            Vector2 NpcPosition = new Vector2(npc.position.X /*+ (npc.width / 2)*/, npc.position.Y /*+ (npc.height / 2)*/);
            float rotation = (float)Math.Atan2(NpcPosition.Y - (target.position.Y + (target.height)), NpcPosition.X - (target.position.X + (target.width)));
            Vector2 direction;
            direction.X = (float)(Math.Cos(rotation) * Speed * -1);
            direction.Y = (float)(Math.Sin(rotation) * Speed * -1);
            var p = Projectile.NewProjectile(npc.GetSource_FromAI(), NpcPosition, direction, type, dammage, 0f, Main.myPlayer);
            Main.projectile[p].hostile = true;
            Main.projectile[p].friendly = false;
            return p;
        }
        public List<int> SlimesList =
        [
            -25,
            -26,
            -10,
            -9,
            -8,
            -7,
            -6,
            -5,
            -4,
            -3,
            -2,
            -1,
            1,
            16,
            71,
            81,
            121,
            138,
            147,
            183,
            184,
            204,
            225,
            244,
            302,
            535,
            537
        ];

        public override void HitEffect(NPC npc, NPC.HitInfo hit)
        {
            base.HitEffect(npc, hit);
            if (Reaper.ReaperMode)
            {
                if (npc.type == NPCID.KingSlime)
                {
                    int choice = Main.rand.Next(SlimesList.Count);
                    var p = NPC.NewNPC(npc.GetSource_FromAI(), (int)npc.position.X, (int)npc.position.Y, SlimesList[choice]);
                    Main.npc[p].GetGlobalNPC<RemnantGlobalNPC>().CanDrop = false;


                    if(RemnantOfTheAncientsMod.CalamityMod != null)
                    {
                        ListUtils.AddSecure(ref SlimesList, CallUtils.TryGetNpcFromMod(RemnantOfTheAncientsMod.CalamityMod, "EbonianBlightSlime"));
                        ListUtils.AddSecure(ref SlimesList, CallUtils.TryGetNpcFromMod(RemnantOfTheAncientsMod.CalamityMod, "CrimulanBlightSlime"));
                        if (CalamityUtils.IsBossDefeated("HiveMind") || CalamityUtils.IsBossDefeated("PerforatorHive"))
                        {
                           ListUtils.AddSecure(ref SlimesList, CallUtils.TryGetNpcFromMod(RemnantOfTheAncientsMod.CalamityMod, "AeroSlime"));
                        }
                        if (Main.hardMode)
                        {
                           ListUtils.AddSecure(ref SlimesList, CallUtils.TryGetNpcFromMod(RemnantOfTheAncientsMod.CalamityMod, "IrradiatedSlime"));
                            if (CalamityUtils.IsBossDefeated("Cryogen") && NPC.downedMechBossAny)
                            {
                               ListUtils.AddSecure(ref SlimesList, CallUtils.TryGetNpcFromMod(RemnantOfTheAncientsMod.CalamityMod, "CryoSlime"));
                            }
                            if (NPC.downedPlantBoss)
                            {
                               ListUtils.AddSecure(ref SlimesList, CallUtils.TryGetNpcFromMod(RemnantOfTheAncientsMod.CalamityMod, "PerennialSlime"));
                            }
                            if (NPC.downedGolemBoss)
                            {
                               ListUtils.AddSecure(ref SlimesList, CallUtils.TryGetNpcFromMod(RemnantOfTheAncientsMod.CalamityMod, "PestilentSlime"));
                            }
                            if (CalamityUtils.IsBossDefeated("AstrumDeus"))
                            {
                               ListUtils.AddSecure(ref SlimesList, CallUtils.TryGetNpcFromMod(RemnantOfTheAncientsMod.CalamityMod, "AstralSlime"));
                            }
                            if (NPC.downedMoonlord)
                            {
                               ListUtils.AddSecure(ref SlimesList, CallUtils.TryGetNpcFromMod(RemnantOfTheAncientsMod.CalamityMod, "GammaSlime"));  
                                if (CalamityUtils.IsBossDefeated("Providence"))
                                {
                                   ListUtils.AddSecure(ref SlimesList, CallUtils.TryGetNpcFromMod(RemnantOfTheAncientsMod.CalamityMod, "BloomSlime"));
                                }
                            }
                        }
                    }
                }
                if (npc.type == NPCID.EaterofWorldsHead && npc.life <= 0)
                {
                    int NumberOfProjectiles = 8;
                    float angle = 0f;
                    float angleSpacing = MathHelper.TwoPi / NumberOfProjectiles;
                    float projectileSpeed = 8f;

                    for (int i = 0; i < NumberOfProjectiles; i++)
                    {
                        Vector2 direction = new Vector2(1f, 0f).RotatedBy(angle);

                        Projectile.NewProjectile(npc.GetSource_FromAI(), npc.position, direction * projectileSpeed, ProjectileID.CursedFlameHostile, 50, 0f, Main.myPlayer);
                        angle += angleSpacing;
                    }
                }
                if (npc.type == NPCID.Creeper)
                {
                    if (npc.life <= 0)
                    {
                        if (Main.rand.NextBool(4))
                        {
                            if (Main.netMode != NetmodeID.MultiplayerClient)
                            {
                                Main.player[Main.myPlayer].AddBuff(BuffID.Confused, Utils1.FormatTimeToTick(0, 0, 0, 4));
                                Main.player[Main.myPlayer].AddBuff(BuffID.Slow, Utils1.FormatTimeToTick(0, 0, 0, 2));
                            }
                            else
                            {
                                Main.player[npc.target].AddBuff(BuffID.Confused, Utils1.FormatTimeToTick(0, 0, 0, 4));
                                Main.player[npc.target].AddBuff(BuffID.Slow, Utils1.FormatTimeToTick(0, 0, 0, 2));
                            }
                        }
                    }
                }
                if (npc.type == NPCID.Deerclops)
                {
                    if (npc.life <= 0)
                    {
                        Main.windSpeedCurrent = 0;
                        Main.StopRain();
                    }
                }

            }
        }

        bool DearclopsDead = false;
        public override bool CheckDead(NPC npc)
        {
            if (Reaper.ReaperMode)
            {
                if (npc.type == NPCID.MoonLordCore)
                {
                    if (npc.life <= 1)
                    {
                        if (Main.netMode == NetmodeID.MultiplayerClient)
                        {
                            RemnantPlayer.ClearBuffToAllPlayers(BuffID.Horrified);
                            RemnantPlayer.ClearBuffToAllPlayers(BuffID.Weak);
                            RemnantPlayer.ClearBuffToAllPlayers(BuffID.Blackout);
                            RemnantPlayer.ClearBuffToAllPlayers(BuffID.ChaosState);
                            RemnantPlayer.ClearBuffToAllPlayers(BuffID.NoBuilding);
                        }
                        else
                        {
                            Main.player[Main.myPlayer].ClearBuff(BuffID.Horrified);
                            Main.player[Main.myPlayer].ClearBuff(BuffID.Weak);
                            Main.player[Main.myPlayer].ClearBuff(BuffID.Blackout);
                            Main.player[Main.myPlayer].ClearBuff(BuffID.ChaosState);
                            Main.player[Main.myPlayer].ClearBuff(BuffID.NoBuilding);
                        }
                    }
                }
                if (npc.type == NPCID.Deerclops)
                {
                    if (npc.life <= 1 || (DificultyUtils.InfernumMode))
                    {
                        DearclopsDead = true;
                        if (Main.netMode == NetmodeID.MultiplayerClient)
                        {
                            RemnantPlayer.ClearBuffToAllPlayers(BuffID.Blackout);
                            RemnantPlayer.ClearBuffToAllPlayers(BuffID.Darkness);
                        }
                        else
                        {
                            Main.player[Main.myPlayer].ClearBuff(BuffID.Blackout);
                            Main.player[Main.myPlayer].ClearBuff(BuffID.Darkness);
                        }
                    }
                }
                if (npc.type == NPCID.QueenSlimeBoss)
                {
                    if (npc.life <= 1)
                    {
                        if (Main.netMode == NetmodeID.MultiplayerClient)
                        {
                            RemnantPlayer.ClearBuffToAllPlayers(BuffID.WindPushed);
                            RemnantPlayer.ClearBuffToAllPlayers(BuffID.VortexDebuff);
                            RemnantPlayer.ClearBuffToAllPlayers(BuffID.QueenSlimeMount);
                        }
                        else
                        {
                            Main.player[Main.myPlayer].ClearBuff(BuffID.WindPushed);
                            Main.player[Main.myPlayer].ClearBuff(BuffID.VortexDebuff);
                            Main.player[Main.myPlayer].ClearBuff(BuffID.QueenSlimeMount);
                        }
                    }
                }
                if(RemnantOfTheAncientsMod.CalamityMod != null)
                {
                    int idCrimulan = CallUtils.TryGetNpcFromMod(RemnantOfTheAncientsMod.CalamityMod, "SplitCrimulanPaladin");
                    if (npc.type == idCrimulan || npc.type == CallUtils.TryGetNpcFromMod(RemnantOfTheAncientsMod.CalamityMod, "SplitEbonianPaladin"))
                    {
                        var p = NPC.NewNPC(npc.GetSource_FromAI(), (int)npc.Center.X, (int)npc.Center.Y, NPCID.KingSlime);
                        Main.npc[p].lifeMax = 500;
                        Main.npc[p].life = 500;
                        Main.npc[p].scale = 0.5f; 
                    }
                }
            }
            return base.CheckDead(npc);
        }

        public override void OnHitByProjectile(NPC npc, Projectile projectile, NPC.HitInfo hit, int damageDone)
        {
            base.OnHitByProjectile(npc, projectile, hit, damageDone);
            if (Reaper.ReaperMode)
            {
                if (npc.type == NPCID.Creeper)
                {
                    projectile.penetrate = 1;
                }
            }
        }
        public override bool PreDraw(NPC npc, SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            return base.PreDraw(npc, spriteBatch, screenPos, drawColor);
        }
        public override void PostDraw(NPC npc, SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            if(RemnantOfTheAncientsMod.CalamityMod != null)
                postDrawCalamity(npc, spriteBatch, screenPos, drawColor);
            base.PostDraw(npc, spriteBatch, screenPos, drawColor);
        }
        int frameCounter = 0;
        int Timmer1 = 0;
        int Timmer2 = 0;
        bool HasElectricShield = false;
        int electricShieldLifeMax = 10;
        int electricShieldLife = 10;
        [JITWhenModsEnabled("CalamityMod")]
        void postDrawCalamity(NPC npc, SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            if (Reaper.ReaperMode)
            {
                if (npc.type == CallUtils.TryGetNpcFromMod(RemnantOfTheAncientsMod.CalamityMod, "PlaguebringerGoliath"))
                {
                    if (HasElectricShield)
                    {
                        //Calamity revisar
                        Asset<Texture2D> texture = TextureAssets.Projectile[CallUtils.TryGetProjectileFromMod(RemnantOfTheAncientsMod.CalamityMod, "TeslaAura")];
                        Vector2 origin = new(texture.Width() * 0.5f, texture.Height() * 0.5f);//0.5  
                        Color color = Color.Green;
                        animateEffects(npc, 1f, texture, color);
                    }
                }
                else if (npc.type == CallUtils.TryGetNpcFromMod(RemnantOfTheAncientsMod.CalamityMod, "AresBody"))
                {
                    if (HasElectricShield)
                    {
                        //Calamity revisar
                        Asset<Texture2D> texture = TextureAssets.Projectile[CallUtils.TryGetProjectileFromMod(RemnantOfTheAncientsMod.CalamityMod, "TeslaAura")];
                        Vector2 origin = new(texture.Width() * 0.5f, texture.Height() * 0.5f);//0.5  
                        Color color = Color.Gray;
                        animateEffects(npc, 1f, texture, color);
                    }
                }
            }
            
        }
        void animateEffects(NPC npc, float rotation, Asset<Texture2D> sprite, Color drawColour)
        {
            Vector2 frames = new(3, 6); 
            Vector2 size = new(sprite.Width(), sprite.Height());
            Vector2 origin = new(size.X * 0.5f / frames.X, size.Y * 0.5f / frames.Y);

            //if(Main.LocalPlayer.infernoCounter % 2 == 0)
            frameCounter++;

            if (frameCounter > 3)
            {
                Timmer1++;
                frameCounter = 0;
            }

            if (Timmer1 >= frames.Y)
            {
                Timmer1 = 0;
                Timmer2++;
            }
            if (Timmer2 >= frames.X)
                Timmer2 = 0;

            Rectangle sourceRect = sprite.Frame(3, 6, Timmer2, Timmer1);
            float opacity = 0.1f * electricShieldLife;
            Main.EntitySpriteDraw((Texture2D)sprite, npc.Center - Main.screenPosition, sourceRect, drawColour * opacity, rotation, origin, 2f, SpriteEffects.None, 0);
           // Main.LocalPlayer.position = npc.Center - Main.screenPosition;
        }
    }
}