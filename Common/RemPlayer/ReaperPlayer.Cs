using RemnantOfTheAncientsMod.Content.Projectiles;
using RemnantOfTheAncientsMod;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using System.Collections.Generic;
using Terraria;
using RemnantOfTheAncientsMod.Content.Items.Consumables.DificultChanger;
using RemnantOfTheAncientsMod.Content.Items.Accesories;
using Terraria.ID;
using Terraria.DataStructures;
using RemnantOfTheAncientsMod.Content.Buffs.Buffs;
using Terraria.ModLoader.IO;
using Microsoft.Xna.Framework;
using RemnantOfTheAncientsMod.Common.Global.Items;
using RemnantOfTheAncientsMod.World;
using RemnantOfTheAncientsMod.Content.NPCs.Bosses.DAniquilator;
using RemnantOfTheAncientsMod.Content.NPCs.Bosses.FrozenAssaulter;
using RemnantOfTheAncientsMod.Content.NPCs.Bosses.ITyrant;
using RemnantOfTheAncientsMod.Common.Configs;
using SangarUtilities.Common;

public class ReaperPlayer : ModPlayer
{
    public bool ChaliceOn;
    public static bool FWeapons;
    public static bool ReaperFirstTime;
    private static List<NPC> _hallucinationCandidates = [];
    public bool ReaperSoulBoost;
   

    public Dictionary<Mod, Dictionary<int, bool>> SoulsUpgrades =
    [
        //[false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false],
        //[false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false]
    ];

    public Dictionary<Mod, Dictionary<int, float>> SoulsUpgradesActive =
    [
       // [0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0],
       // [0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0]
    ];

    public Mod TerrariaMod;
    public Mod RemnantOfTheAncientsM;
    public Mod CalamityMod;

    public override void ResetEffects()
    {
        ReaperSoulBoost = false;
        ChaliceOn = false;
    }

    public override void OnEnterWorld()
    {
        FWeapons = true;
        ReaperGlobalItem.ReaperWingsNerf(Player);
        //FillSoulList();

        ModLoader.TryGetMod("SangarUtilities", out TerrariaMod);
        ModLoader.TryGetMod("RemnantOfTheAncientsMod", out RemnantOfTheAncientsM);
        ModLoader.TryGetMod("CalamityMod", out CalamityMod);
    }

    public void FillSoulList()
    {
        foreach (Mod mod in ModLoader.Mods)
        {
             SoulsUpgrades.TryAdd(mod, []);
             SoulsUpgradesActive.TryAdd(mod, []);
        }
        List<NPC> currentList = [];

        foreach (var m in NpcList.BossList.Keys)
        {
            if(m == RemnantOfTheAncientsM)
            {
                NpcList.BossList[m] = 
                [
                    ContentSamples.NpcsByNetId[NPCType<DesertAniquilator>()],
                    ContentSamples.NpcsByNetId[NPCType<FrozenAssaulter>()],
                    ContentSamples.NpcsByNetId[NPCType<InfernalTyrantHead>()],
                ];
            }
            currentList = NpcList.BossList[m];
            foreach (NPC npc in currentList)
            {
               var aux = SoulsUpgrades[m];
               GlobalUtils.AddSecure(ref aux,npc.type,false);   
               SoulsUpgrades[m] = aux;

                var aux2 = SoulsUpgradesActive[m];
                GlobalUtils.AddSecure(ref aux2, npc.type, 0f);
                SoulsUpgradesActive[m] = aux2;
            }
        }

    }
    public override IEnumerable<Item> AddStartingItems(bool mediumCoreDeath)
    {
        if (mediumCoreDeath) return [new Item(ItemType<Ftoggler>())];
        return [new Item(ItemType<Ftoggler>())];
    }
    public override void UpdateEquips()
    {
        if (Player.wingTimeMax > 4) ReaperGlobalItem.ReaperWingsNerf(Player);
    }

    public override void SyncPlayer(int toWho, int fromWho, bool newPlayer)
    {
        ModPacket packet = Mod.GetPacket();
        foreach (var mod in NpcList.BossList.Keys)
        {
            for (int i = 0; i < SoulsUpgrades[mod].Count; i++)
            {
                packet.Write(SoulsUpgrades[mod][i]);
            }
        }
    }
    public override void SaveData(TagCompound tag)
    {
        ModLoader.TryGetMod("SangarUtilities", out TerrariaMod);
        ModLoader.TryGetMod("RemnantOfTheAncientsMod", out RemnantOfTheAncientsM);
        ModLoader.TryGetMod("CalamityMod", out CalamityMod);

        FillSoulList();
        subSave(tag,TerrariaMod);   
        subSave(tag, RemnantOfTheAncientsM);
        subSave(tag, CalamityMod);

        void subSave(TagCompound tag,Mod mod)
        {
            if (mod != null)
            {
                if (SoulsUpgrades[mod].Count < NpcList.BossList[mod].Count)
                {
                    foreach (NPC npc in NpcList.BossList[mod])
                    {
                        Dictionary<int, bool> aux = SoulsUpgrades[mod];
                        GlobalUtils.AddSecure(ref aux, npc.type, false);
                        SoulsUpgrades[mod] = aux;
                    }
                }
                foreach (NPC npc in NpcList.BossList[mod])
                {
                    string name = mod == TerrariaMod ? npc.FullName.Replace(" ", "") : npc.ModNPC.GetType().Name;

                    if (!SoulsUpgrades[mod][npc.type])
                    {
                        bool? value = tag.Get<bool>(name + "ReaperUpgrade");
                        value ??= false;
                        SoulsUpgrades[mod][npc.type] = (bool)value;
                    }
                    tag[name + "ReaperUpgrade"] = SoulsUpgrades[mod][npc.type];           
                }
            }
        }

       /* tag["EyeReaperUpgrade"] = SoulsUpgrades[TerrariaMod][NPCID.EyeOfChutulu];
        tag["CorruptReaperUpgrade"] = SoulsUpgrades[TerrariaMod][NPCID.CorruptBoss];
        tag["BeeReaperUpgrade"] = SoulsUpgrades[TerrariaMod][NPCID.QueenBee];
        tag["SkeletonReaperUpgrade"] = SoulsUpgrades[TerrariaMod][NPCID.Skeletron];
        tag["DeerclopsReaperUpgrade"] = SoulsUpgrades[TerrariaMod][NPCID.Deerclops];
        tag["DesertReaperUpgrade"] = SoulsUpgrades[RemnantOfTheAncientsM][NPCID.DesertAnhilator];
        tag["FleshReaperUpgrade"] = SoulsUpgrades[TerrariaMod][NPCID.WallOfFlesh];
        tag["FrozenReaperUpgrade"] = SoulsUpgrades[RemnantOfTheAncientsM][NPCID.FrozenAssaulter];
        tag["QueenReaperUpgrade"] = SoulsUpgrades[TerrariaMod][NPCID.QueenSlime];
        tag["SpazmatismReaperUpgrade"] = SoulsUpgrades[TerrariaMod][NPCID.Spazmatism];
        tag["RetinazorReaperUpgrade"] = SoulsUpgrades[TerrariaMod][NPCID.Retinazor];
        tag["SkeletronPrimeReaperUpgrade"] = SoulsUpgrades[TerrariaMod][NPCID.SkeletronPrime];
        tag["DestroyerReaperUpgrade"] = SoulsUpgrades[TerrariaMod][NPCID.Destroyer];
        tag["PlantReaperUpgrade"] = SoulsUpgrades[TerrariaMod][NPCID.Plantera];
        tag["InfernalReaperUpgrade"] = SoulsUpgrades[RemnantOfTheAncientsM][NPCID.InfernalTyrant];
        tag["GolemReaperUpgrade"] = SoulsUpgrades[TerrariaMod][NPCID.Golem];
        tag["EmpressReaperUpgrade"] = SoulsUpgrades[TerrariaMod][NPCID.EmpressOfLight];
        tag["DukeReaperUpgrade"] = SoulsUpgrades[TerrariaMod][NPCID.DukeFishron];
        tag["CultistReaperUpgrade"] = SoulsUpgrades[TerrariaMod][NPCID.Cultist];
        tag["MoonReaperUpgrade"] = SoulsUpgrades[TerrariaMod][NPCID.MoonLord];*/











        /* Dictionary<Mod, List<NPC>> BossList = SangarUtilities.Common.NpcList.BossList;
         foreach (Mod mod in BossList.Keys)
         {
             foreach (NPC npc in BossList[mod])
             {
                 string name = mod == TerrariaMod ? npc.FullName.Replace(" ", "") : npc.ModNPC.GetType().Name;
                 tag[name + "ReaperUpgrade"] = SoulsUpgrades[mod][npc.type];
             }
         }*/


        /*     tag["SlimeReaperUpgrade"] = SoulsUpgrades[RemnantOfTheAncientsM][NPCID.KingSlime];
         tag["EyeReaperUpgrade"] = SoulsUpgrades[RemnantOfTheAncientsM][NPCID.EyeOfChutulu];
         tag["CorruptReaperUpgrade"] = SoulsUpgrades[RemnantOfTheAncientsM][NPCID.CorruptBoss];
         tag["BeeReaperUpgrade"] = SoulsUpgrades[RemnantOfTheAncientsM][NPCID.QueenBee];
         tag["SkeletonReaperUpgrade"] = SoulsUpgrades[RemnantOfTheAncientsM][NPCID.Skeletron];
         tag["DeerclopsReaperUpgrade"] = SoulsUpgrades[RemnantOfTheAncientsM][NPCID.Deerclops];
         tag["DesertReaperUpgrade"] = SoulsUpgrades[RemnantOfTheAncientsM][NPCID.DesertAnhilator];
         tag["FleshReaperUpgrade"] = SoulsUpgrades[RemnantOfTheAncientsM][NPCID.WallOfFlesh];
         tag["FrozenReaperUpgrade"] = SoulsUpgrades[RemnantOfTheAncientsM][NPCID.FrozenAssaulter];
         tag["QueenReaperUpgrade"] = SoulsUpgrades[RemnantOfTheAncientsM][NPCID.QueenSlime];
         tag["SpazmatismReaperUpgrade"] = SoulsUpgrades[RemnantOfTheAncientsM][NPCID.Spazmatism];
         tag["RetinazorReaperUpgrade"] = SoulsUpgrades[RemnantOfTheAncientsM][NPCID.Retinazor];
         tag["SkeletronPrimeReaperUpgrade"] = SoulsUpgrades[RemnantOfTheAncientsM][NPCID.SkeletronPrime];
         tag["DestroyerReaperUpgrade"] = SoulsUpgrades[RemnantOfTheAncientsM][NPCID.Destroyer];
         tag["PlantReaperUpgrade"] = SoulsUpgrades[RemnantOfTheAncientsM][NPCID.Plantera];
         tag["InfernalReaperUpgrade"] = SoulsUpgrades[RemnantOfTheAncientsM][NPCID.InfernalTyrant];
         tag["GolemReaperUpgrade"] = SoulsUpgrades[RemnantOfTheAncientsM][NPCID.Golem];
         tag["EmpressReaperUpgrade"] = SoulsUpgrades[RemnantOfTheAncientsM][NPCID.EmpressOfLight];
         tag["DukeReaperUpgrade"] = SoulsUpgrades[RemnantOfTheAncientsM][NPCID.DukeFishron];
         tag["CultistReaperUpgrade"] = SoulsUpgrades[RemnantOfTheAncientsM][NPCID.Cultist];
         tag["MoonReaperUpgrade"] = SoulsUpgrades[RemnantOfTheAncientsM][NPCID.MoonLord];

         if (RemnantOfTheAncientsMod.RemnantOfTheAncientsMod.CalamityMod != null)
         {
             tag["DesertScourgeUpgrade"] = SoulsUpgrades[ModID.Calamity][BossID.CalamityID.DesertScourge];
             tag["CrabulonUpgrade"] = SoulsUpgrades[ModID.Calamity][BossID.CalamityID.Crabulon];
             tag["CalCorruptBossUpgrade"] = SoulsUpgrades[ModID.Calamity][BossID.CalamityID.CalCorruptBoss];
             tag["SlimeGodUpgrade"] = SoulsUpgrades[ModID.Calamity][BossID.CalamityID.SlimeGod];
             tag["CryogenUpgrade"] = SoulsUpgrades[ModID.Calamity][BossID.CalamityID.Cryogen];
             tag["AquaticScourgeGodUpgrade"] = SoulsUpgrades[ModID.Calamity][BossID.CalamityID.AquaticScourge];
             tag["BrimstoneElementalGodUpgrade"] = SoulsUpgrades[ModID.Calamity][BossID.CalamityID.BrimstoneElemental];
             tag["CalamitasCloneUpgrade"] = SoulsUpgrades[ModID.Calamity][BossID.CalamityID.CalamitasClone];
             tag["LeviathanUpgrade"] = SoulsUpgrades[ModID.Calamity][BossID.CalamityID.Leviathan];
             tag["AstrumAureusUpgrade"] = SoulsUpgrades[ModID.Calamity][BossID.CalamityID.AstrumAureus];
             tag["PlaguebringerUpgrade"] = SoulsUpgrades[ModID.Calamity][BossID.CalamityID.Plaguebringer];
             tag["RavagerUpgrade"] = SoulsUpgrades[ModID.Calamity][BossID.CalamityID.Ravager];
             tag["AstrumDeusUpgrade"] = SoulsUpgrades[ModID.Calamity][BossID.CalamityID.AstrumDeus];
             tag["ProfanedGuardianUpgrade"] = SoulsUpgrades[ModID.Calamity][BossID.CalamityID.ProfanedGuardian];
             tag["DragonfollyUpgrade"] = SoulsUpgrades[ModID.Calamity][BossID.CalamityID.Dragonfolly];
             tag["ProvidenceUpgrade"] = SoulsUpgrades[ModID.Calamity][BossID.CalamityID.Providence];
             tag["SormWeaverUpgrade"] = SoulsUpgrades[ModID.Calamity][BossID.CalamityID.SormWeaver];
             tag["CeaselessVoidUpgrade"] = SoulsUpgrades[ModID.Calamity][BossID.CalamityID.CeaselessVoid];
             tag["SignusUpgrade"] = SoulsUpgrades[ModID.Calamity][BossID.CalamityID.Signus];
             tag["PolterghastUpgrade"] = SoulsUpgrades[ModID.Calamity][BossID.CalamityID.Polterghast];
             tag["OldDukeUpgrade"] = SoulsUpgrades[ModID.Calamity][BossID.CalamityID.OldDuke];
             tag["DevourerOfGodsUpgrade"] = SoulsUpgrades[ModID.Calamity][BossID.CalamityID.DevourerOfGods];
             tag["YharonUpgrade"] = SoulsUpgrades[ModID.Calamity][BossID.CalamityID.Yharon];
             tag["ExoMechUpgrade"] = SoulsUpgrades[ModID.Calamity][BossID.CalamityID.ExoMech];
             tag["SupremeCalamitasUpgrade"] = SoulsUpgrades[ModID.Calamity][BossID.CalamityID.SupremeCalamitas];
       } */
    }
    public override void LoadData(TagCompound tag)
    {
        ModLoader.TryGetMod("SangarUtilities", out TerrariaMod);
        ModLoader.TryGetMod("RemnantOfTheAncientsMod", out RemnantOfTheAncientsM);
        ModLoader.TryGetMod("CalamityMod", out CalamityMod);

        subLoad(tag, TerrariaMod);
        subLoad(tag, RemnantOfTheAncientsM);
        subLoad(tag, CalamityMod);

        void subLoad(TagCompound tag, Mod mod)
        {
            if (mod != null)
            {
                if(!SoulsUpgrades.ContainsKey(mod))
                {
                    SoulsUpgrades.TryAdd(mod, new Dictionary<int, bool>());
                }
                if (SoulsUpgrades[mod].Count < NpcList.BossList[mod].Count)
                {
                    foreach (NPC npc in NpcList.BossList[mod])
                    {
                        Dictionary<int, bool> aux = SoulsUpgrades[mod];
                        GlobalUtils.AddSecure(ref aux, npc.type, false);
                        SoulsUpgrades[mod] = aux;
                    }
                }
                List<NPC> currentList = NpcList.BossList[mod];
                foreach (NPC npc in currentList)
                {
                    string name = mod == TerrariaMod ? npc.FullName.Replace(" ", "") : npc.ModNPC.GetType().Name;
                    bool? value = tag.Get<bool>(name + "ReaperUpgrade");
                    value ??= false;
                    SoulsUpgrades[mod][npc.type] = (bool)value;
                }
            }
        }

        /*SoulsUpgrades[RemnantOfTheAncientsM][NPCID.KingSlime] = tag.Get<bool>("SlimeReaperUpgrade");
            SoulsUpgrades[RemnantOfTheAncientsM][NPCID.EyeOfChutulu] = tag.Get<bool>("EyeReaperUpgrade");
            SoulsUpgrades[RemnantOfTheAncientsM][NPCID.CorruptBoss] = tag.Get<bool>("CorruptReaperUpgrade");
            SoulsUpgrades[RemnantOfTheAncientsM][NPCID.QueenBee] = tag.Get<bool>("BeeReaperUpgrade");
            SoulsUpgrades[RemnantOfTheAncientsM][NPCID.Skeletron] = tag.Get<bool>("SkeletonReaperUpgrade");
            SoulsUpgrades[RemnantOfTheAncientsM][NPCID.Deerclops] = tag.Get<bool>("DeerclopsReaperUpgrade");
            SoulsUpgrades[RemnantOfTheAncientsM][NPCID.DesertAnhilator] = tag.Get<bool>("DesertReaperUpgrade");
            SoulsUpgrades[RemnantOfTheAncientsM][NPCID.WallOfFlesh] = tag.Get<bool>("FleshReaperUpgrade");
            SoulsUpgrades[RemnantOfTheAncientsM][NPCID.FrozenAssaulter] = tag.Get<bool>("FrozenReaperUpgrade");
            SoulsUpgrades[RemnantOfTheAncientsM][NPCID.QueenSlime] = tag.Get<bool>("QueenReaperUpgrade");
            SoulsUpgrades[RemnantOfTheAncientsM][NPCID.Spazmatism] = tag.Get<bool>("SpazmatismReaperUpgrade");
            SoulsUpgrades[RemnantOfTheAncientsM][NPCID.Retinazor] = tag.Get<bool>("RetinazorReaperUpgrade");
            SoulsUpgrades[RemnantOfTheAncientsM][NPCID.SkeletronPrime] = tag.Get<bool>("SkeletronPrimeReaperUpgrade");
            SoulsUpgrades[RemnantOfTheAncientsM][NPCID.Destroyer] = tag.Get<bool>("DestroyerReaperUpgrade");
            SoulsUpgrades[RemnantOfTheAncientsM][NPCID.Plantera] = tag.Get<bool>("PlantReaperUpgrade");
            SoulsUpgrades[RemnantOfTheAncientsM][NPCID.InfernalTyrant] = tag.Get<bool>("InfernalReaperUpgrade");
            SoulsUpgrades[RemnantOfTheAncientsM][NPCID.Golem] = tag.Get<bool>("GolemReaperUpgrade");
            SoulsUpgrades[RemnantOfTheAncientsM][NPCID.EmpressOfLight] = tag.Get<bool>("EmpressReaperUpgrade");
            SoulsUpgrades[RemnantOfTheAncientsM][NPCID.DukeFishron] = tag.Get<bool>("DukeReaperUpgrade");
            SoulsUpgrades[RemnantOfTheAncientsM][NPCID.Cultist] = tag.Get<bool>("CultistReaperUpgrade");
            SoulsUpgrades[RemnantOfTheAncientsM][NPCID.MoonLord] = tag.Get<bool>("MoonReaperUpgrade");

            if (RemnantOfTheAncientsMod.RemnantOfTheAncientsMod.CalamityMod != null)
            {

            }*/
    }
    public void LoadSoulsToggle()
    {
        
        SoulsUpgradesActive[TerrariaMod][NPCID.KingSlime] = GetInstance<ConfigReaperSouls>().ToggleKingSlimeSoul;
        SoulsUpgradesActive[TerrariaMod][NPCID.EyeofCthulhu] = GetInstance<ConfigReaperSouls>().ToggleEyeOfChutuluSoul.ToInt();
        SoulsUpgradesActive[TerrariaMod][NPCID.BrainofCthulhu] = GetInstance<ConfigReaperSouls>().ToggleCorruptSoul.ToInt();
        SoulsUpgradesActive[TerrariaMod][NPCID.QueenBee] = GetInstance<ConfigReaperSouls>().ToggleQueenBeeSoul.ToInt();
        SoulsUpgradesActive[TerrariaMod][NPCID.SkeletronHead] = GetInstance<ConfigReaperSouls>().ToggleSkeletronSoul.ToInt();
        SoulsUpgradesActive[TerrariaMod][NPCID.Deerclops] = GetInstance<ConfigReaperSouls>().ToggleDearclopsSoul.ToInt();
        SoulsUpgradesActive[RemnantOfTheAncientsM][NPCType<DesertAniquilator>()] = GetInstance<ConfigReaperSouls>().ToggleDesertAnhilatorSoul.ToInt();
        SoulsUpgradesActive[TerrariaMod][NPCID.WallofFlesh] = GetInstance<ConfigReaperSouls>().ToggleWallOfFleshSoul.ToInt();
        SoulsUpgradesActive[RemnantOfTheAncientsM][NPCType<FrozenAssaulter>()] = GetInstance<ConfigReaperSouls>().ToggleFrozenAssaulterSoul.ToInt();
        SoulsUpgradesActive[TerrariaMod][NPCID.QueenSlimeBoss] = GetInstance<ConfigReaperSouls>().ToggleQueenSlimeSoul.ToInt();
        SoulsUpgradesActive[TerrariaMod][NPCID.Spazmatism] = GetInstance<ConfigReaperSouls>().ToggleSpazmatismSoul.ToInt();
        SoulsUpgradesActive[TerrariaMod][NPCID.Retinazer] = GetInstance<ConfigReaperSouls>().ToggleRetinazorSoul.ToInt();
        SoulsUpgradesActive[TerrariaMod][NPCID.SkeletronPrime] = GetInstance<ConfigReaperSouls>().ToggleSkeletronPrimeSoul;
        SoulsUpgradesActive[TerrariaMod][NPCID.TheDestroyer] = GetInstance<ConfigReaperSouls>().ToggleDestroyerSoul.ToInt();
        SoulsUpgradesActive[TerrariaMod][NPCID.Plantera] = GetInstance<ConfigReaperSouls>().TogglePlanteraSoul.ToInt();
        SoulsUpgradesActive[RemnantOfTheAncientsM][NPCType<InfernalTyrantHead>()] = GetInstance<ConfigReaperSouls>().ToggleInfernalTyrantSoul.ToInt();
        SoulsUpgradesActive[TerrariaMod][NPCID.Golem] = GetInstance<ConfigReaperSouls>().ToggleGolemSoul.ToInt();
        SoulsUpgradesActive[TerrariaMod][NPCID.HallowBoss] = GetInstance<ConfigReaperSouls>().ToggleEmpressOfLightSoul.ToInt();
        SoulsUpgradesActive[TerrariaMod][NPCID.DukeFishron] = GetInstance<ConfigReaperSouls>().ToggleDukeFishronSoul.ToInt();
        SoulsUpgradesActive[TerrariaMod][NPCID.CultistBoss] = GetInstance<ConfigReaperSouls>().ToggleLunaticCultistSoul.ToInt();
        SoulsUpgradesActive[TerrariaMod][NPCID.MoonLordCore] = GetInstance<ConfigReaperSouls>().ToggleMoonlordSoul.ToInt();

       
    }
    public void SetSoulsToggle(int i,float value)
    {
        switch (i)
        {
            case 0:
                GetInstance<ConfigReaperSouls>().ToggleKingSlimeSoul = value;
                break;
            case 1:
                GetInstance<ConfigReaperSouls>().ToggleEyeOfChutuluSoul = (int)value == 1;
                break;
            case 2:
                GetInstance<ConfigReaperSouls>().ToggleCorruptSoul = (int)value == 1;
                break;
            case 3:
                GetInstance<ConfigReaperSouls>().ToggleQueenBeeSoul = (int)value == 1;
                break;
            case 4:
                GetInstance<ConfigReaperSouls>().ToggleSkeletronSoul = (int)value == 1;
                break;
            case 5:
                GetInstance<ConfigReaperSouls>().ToggleDearclopsSoul = (int)value == 1;
                break;
            case 6:
                GetInstance<ConfigReaperSouls>().ToggleDesertAnhilatorSoul = (int)value == 1;
                break;
            case 7:
                GetInstance<ConfigReaperSouls>().ToggleWallOfFleshSoul = (int)value == 1;
                break;
            case 8:
                GetInstance<ConfigReaperSouls>().ToggleFrozenAssaulterSoul = (int)value == 1;
                break;
            case 9:
                GetInstance<ConfigReaperSouls>().ToggleQueenSlimeSoul = (int)value == 1;
                break;
            case 10:
                GetInstance<ConfigReaperSouls>().ToggleSpazmatismSoul = (int)value == 1;
                break;
            case 11:
                GetInstance<ConfigReaperSouls>().ToggleRetinazorSoul = (int)value == 1;
                break;
            case 12:
                GetInstance<ConfigReaperSouls>().ToggleSkeletronPrimeSoul = value;
                break;
            case 13:
                GetInstance<ConfigReaperSouls>().ToggleDestroyerSoul = (int)value == 1;
                break;
            case 14:
                GetInstance<ConfigReaperSouls>().TogglePlanteraSoul = (int)value == 1;
                break;
            case 15:
                GetInstance<ConfigReaperSouls>().ToggleInfernalTyrantSoul = (int)value == 1;
                break;
            case 16:
                GetInstance<ConfigReaperSouls>().ToggleGolemSoul = (int)value == 1;
                break;
            case 17:
                GetInstance<ConfigReaperSouls>().ToggleEmpressOfLightSoul = (int)value == 1;
                break;
            case 18:
                GetInstance<ConfigReaperSouls>().ToggleDukeFishronSoul = (int)value == 1;
                break;
            case 19:
                GetInstance<ConfigReaperSouls>().ToggleLunaticCultistSoul = (int)value == 1;
                break;
            case 20:
                GetInstance<ConfigReaperSouls>().ToggleMoonlordSoul = (int)value == 1;
                break;
        }
        LoadSoulsToggle();
    }
    public override void Kill(double damage, int hitDirection, bool pvp, PlayerDeathReason damageSource)
    {
        float respawnMult = RespawnCalculator();
        float respawnTime = Player.respawnTimer * respawnMult;
        Player.respawnTimer = (int)respawnTime;  
    }
    public void ReaperStarter()
    {
        if (!ReaperFirstTime)
        {
            Item.NewItem(Player.GetSource_DropAsItem(),Player.position, ItemID.ReaperHood);
            Item.NewItem(Player.GetSource_DropAsItem(), Player.position, ItemID.ReaperRobe);
            Item.NewItem(Player.GetSource_DropAsItem(), Player.position, ItemType<ReaperChalice>());
        }
    }
    public override void PostUpdateBuffs()
    {
        if (Reaper.ReaperMode)
            Player.AddBuff(BuffType<ReaperBuff>(), 2);
        
    }   
    private float RespawnCalculator()
    {
        bool DesertAnhilatorActivated = SoulsUpgradesActive[RemnantOfTheAncientsM][NPCType<DesertAniquilator>()] == 1 && SoulsUpgrades[RemnantOfTheAncientsM][NPCType<DesertAniquilator>()];

        bool MoonlordRespawnActivated = SoulsUpgradesActive[TerrariaMod][NPCID.MoonLordCore] == 1 && SoulsUpgrades[TerrariaMod][NPCID.MoonLordCore];

        if (Reaper.ReaperMode)
        {
            if (DesertAnhilatorActivated && !MoonlordRespawnActivated) 
                return ReaperSoulBoost ? 0.7f : 0.8f;
            else if (!DesertAnhilatorActivated && MoonlordRespawnActivated) 
                return ReaperSoulBoost ? 0.5f : 0.7f;
            else if (DesertAnhilatorActivated && MoonlordRespawnActivated) 
                return ReaperSoulBoost ? 0.3f : 0.5f;
        }
        return 1;
    }
    public override bool FreeDodge(Player.HurtInfo info)
    {
        if (SoulsUpgrades[TerrariaMod][NPCID.BrainofCthulhu] && SoulsUpgradesActive[TerrariaMod][NPCID.BrainofCthulhu] == 1 && Reaper.ReaperMode)
        {
            if (Main.rand.NextBool(6))
            {
                Player.BrainOfConfusionDodge();
                Player.SetImmuneTimeForAllTypes(120);
                Dust.QuickDust(Player.position, Color.Gray);
                return true;
            }
        }
        return base.FreeDodge(info);
    }
    public void ReaperSoulsBoost()
    {
        LoadSoulsToggle();

        float OgPlayerSpeed = Player.moveSpeed;
        if (Reaper.ReaperMode && ChaliceOn)
        {
            if (SoulsUpgrades[TerrariaMod][NPCID.KingSlime] && SoulsUpgradesActive[TerrariaMod][NPCID.KingSlime] > 0)
            {
                float speedForce = (SoulsUpgradesActive[RemnantOfTheAncientsM][NPCID.KingSlime] / 100) + 1;
                Player.moveSpeed = ReaperSoulBoost ? OgPlayerSpeed + speedForce * 1.2f : OgPlayerSpeed + speedForce;
            }
            if (SoulsUpgrades[TerrariaMod][NPCID.EyeofCthulhu] && SoulsUpgradesActive[TerrariaMod][NPCID.EyeofCthulhu] == 1)
            {
                Player.statLifeMax2 += ReaperSoulBoost ? 20 : 10;
            }
            if ( SoulsUpgrades[TerrariaMod][NPCID.BrainofCthulhu] && SoulsUpgradesActive[TerrariaMod][NPCID.BrainofCthulhu] == 1)
            {
                Player.lifeRegen += ReaperSoulBoost ? 10 : 5;
            }
            if (SoulsUpgrades[TerrariaMod][NPCID.QueenBee] && SoulsUpgradesActive[TerrariaMod][NPCID.QueenBee] == 1)
            {
                Player.honey = true;
                Player.strongBees = true;         
                Player.beeDamage(ReaperSoulBoost? 80:20);    
            }
            if (SoulsUpgrades[TerrariaMod][NPCID.SkeletronHead] && SoulsUpgradesActive[TerrariaMod][NPCID.SkeletronHead] == 1)
            {
                Player.statDefense += ReaperSoulBoost ? 10 : 5;
            }
            if (SoulsUpgrades[TerrariaMod][NPCID.WallofFlesh] && SoulsUpgradesActive[TerrariaMod][NPCID.WallofFlesh] == 1)
            {
                Player.GetDamage(DamageClass.Generic) *= ReaperSoulBoost ? 1.20f : 1.10f;
            }
            if (SoulsUpgrades[RemnantOfTheAncientsM][NPCType<FrozenAssaulter>()] && SoulsUpgradesActive[RemnantOfTheAncientsM][NPCType<FrozenAssaulter>()] == 1) 
                Player.GetModPlayer<RemnantPlayer>().FrostInmune();
            if (SoulsUpgrades[TerrariaMod][NPCID.QueenSlimeBoss] && SoulsUpgradesActive[TerrariaMod][NPCID.QueenSlimeBoss] == 1) 
                Player.statLifeMax2 += ReaperSoulBoost ? 30 : 15;
            if (SoulsUpgrades[TerrariaMod][NPCID.TheDestroyer] && SoulsUpgradesActive[TerrariaMod][NPCID.TheDestroyer] == 1) 
                Player.pickSpeed -= ReaperSoulBoost ? 0.7f : 0.35f;
            if (SoulsUpgrades[TerrariaMod][NPCID.Spazmatism] && SoulsUpgradesActive[TerrariaMod][NPCID.Spazmatism] == 1) 
                Player.GetModPlayer<RemnantPlayer>().FireInmune();
            if (SoulsUpgrades[TerrariaMod][NPCID.SkeletronPrime])
            {
                float JumpForce = SoulsUpgradesActive[TerrariaMod][NPCID.SkeletronPrime];
                Player.findTreasure = true;
                Player.dangerSense = ReaperSoulBoost;
                Player.jumpSpeedBoost += ReaperSoulBoost ? JumpForce * 2 : JumpForce;
            }
            if (SoulsUpgrades[TerrariaMod][NPCID.HallowBoss] && SoulsUpgradesActive[TerrariaMod][NPCID.HallowBoss] == 1)
            {
                Player.empressBrooch = true;
                if (ReaperSoulBoost)
                {
                    Player.wingAccRunSpeed *= 2;
                }
            }

            if (SoulsUpgrades[RemnantOfTheAncientsM][NPCType<InfernalTyrantHead>()] && SoulsUpgradesActive[RemnantOfTheAncientsM][NPCType<InfernalTyrantHead>()] == 1)
            {
                Player.fireWalk = true;
                Player.lavaImmune = true;
                Player.GetDamage(DamageClass.Generic) *= ReaperSoulBoost ? 1.20f : 1.10f;
            }
            if (SoulsUpgrades[TerrariaMod][NPCID.Golem] && SoulsUpgradesActive[TerrariaMod][NPCID.Golem] == 1) Player.statDefense += ReaperSoulBoost ? 15 : 10;

            if (Reaper.ReaperMode &&SoulsUpgrades[TerrariaMod][NPCID.DukeFishron] && SoulsUpgradesActive[TerrariaMod][NPCID.DukeFishron] == 1)
            {
                if (ReaperSoulBoost)
                {
                    Player.GetModPlayer<RemnantPlayer>().AddMinion(ProjectileType<TempestClone>(), 280, 10f);
                    Player.aggro -= 2400;
                }
                else
                {
                    Player.GetModPlayer<RemnantPlayer>().AddMinion(ProjectileType<TempestClone>(), 140, 10f);
                    Player.aggro -= 400;
                }
            }
            if (SoulsUpgrades[TerrariaMod][NPCID.CultistBoss] && SoulsUpgradesActive[TerrariaMod][NPCID.CultistBoss] == 1)
            {
                if (ReaperSoulBoost)
                {
                    Player.GetModPlayer<RemnantPlayer>().AddMinion(ProjectileType<IceMistF>(), 980, 10f);
                }
                else
                {
                    Player.GetModPlayer<RemnantPlayer>().AddMinion(ProjectileType<IceMistF>(), 680, 10f);
                }
            }
        }
    }
    public void ReaperSoulsBoost(Item item)
    {
        if (Reaper.ReaperMode && ChaliceOn)
        {
            ModLoader.TryGetMod("SangarUtilities", out TerrariaMod);
            Dictionary<int, bool> soulVanilla = SoulsUpgrades[TerrariaMod];
            Dictionary<int, float> soulVanillaActive = SoulsUpgradesActive[TerrariaMod];
            if (soulVanilla[NPCID.BrainofCthulhu] && soulVanillaActive[NPCID.BrainofCthulhu] == 1)
            {
                Player.brainOfConfusionItem = item;
            }
            if (soulVanilla[NPCID.Plantera] && soulVanillaActive[NPCID.Plantera] == 1)
            {
                Player.sporeSac = true;
                Player.SporeSac(item);
                Player.statLifeMax2 += ReaperSoulBoost ? 15 : 10;
            }
            if (soulVanilla[NPCID.Deerclops] && soulVanillaActive[NPCID.Deerclops] == 1)
            {
                SpawnHallucination(item);
                if (ReaperSoulBoost)
                {
                    SpawnHallucination(item);
                }
            }
        }
    }
    public bool AllSoulsAreActive()
    {
        for (int i = 0; i < SoulsUpgrades.Count; i++)
        {
            if (!SoulsUpgrades[RemnantOfTheAncientsM][NPCID.KingSlime]) return false;
        }
        return true;
    }
    private void SpawnHallucination(Item item)
    {
        if (Player.whoAmI != Main.myPlayer) return;

        Player.insanityShadowCooldown = Utils.Clamp(Player.insanityShadowCooldown - 1, 0, 100);

        if (Player.insanityShadowCooldown > 0) return;

        Player.insanityShadowCooldown = Main.rand.Next(20, 101);
        float num = 500f;
        int damage = 10;
        if (Player.getDPS() >= 1) damage = Player.getDPS() / 4; //18;
        _hallucinationCandidates.Clear();
        for (int i = 0; i < 200; i++)
        {
            NPC nPC = Main.npc[i];
            if (nPC.CanBeChasedBy(this) && !(Player.Distance(nPC.Center) > num) && Collision.CanHitLine(Player.position, Player.width, Player.height, nPC.position, nPC.width, nPC.height)) _hallucinationCandidates.Add(nPC);
        }
        if (_hallucinationCandidates.Count != 0)
        {
            Projectile.RandomizeInsanityShadowFor(Main.rand.NextFromCollection(_hallucinationCandidates), isHostile: false, out var spawnposition, out var spawnvelocity, out var ai, out var ai2);
            Projectile.NewProjectile(new EntitySource_ItemUse(Player, item), spawnposition, spawnvelocity, ProjectileID.InsanityShadowFriendly, damage, 0f, Player.whoAmI, ai, ai2);
        }
    }
}