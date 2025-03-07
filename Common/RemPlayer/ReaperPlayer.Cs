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
using System;
using System.Linq;
using RemnantOfTheAncientsMod.Common.UtilsTweaks;
using System.Runtime.InteropServices;


public class ReaperPlayer : ModPlayer
{
    public bool ChaliceOn;
    public static bool ReaperFirstTime;
    public override void ResetEffects()
    {      
        ChaliceOn = false;
    }
    public override void OnEnterWorld()
    {
    }

    public override IEnumerable<Item> AddStartingItems(bool mediumCoreDeath)
    {
        if (mediumCoreDeath) return [new Item(ItemType<Ftoggler>())];
        return [new Item(ItemType<Ftoggler>())];
    }
    public void DropReaperStarterKit()
    {
        if (!ReaperFirstTime)
        {
            Item.NewItem(Player.GetSource_DropAsItem(), Player.position, ItemID.ReaperHood);
            Item.NewItem(Player.GetSource_DropAsItem(), Player.position, ItemID.ReaperRobe);
            Item.NewItem(Player.GetSource_DropAsItem(), Player.position, ItemType<ReaperChalice>());
        }
    }
}
public class ReaperEffectsPlayer : ModPlayer
{
    
  
    private static List<NPC> _hallucinationCandidates = [];
    public bool ReaperSoulBoost;

   

    public Mod TerrariaMod;
    public Mod RemnantOfTheAncientsM;
    public Mod CalamityMod;


    public override void Initialize()
    {
        base.Initialize();
    }
    public override void ResetEffects()
    {
        ReaperSoulBoost = false;
    }

    public override void OnEnterWorld()
    {
        ModLoader.TryGetMod("SangarUtilities", out TerrariaMod);
        ModLoader.TryGetMod("RemnantOfTheAncientsMod", out RemnantOfTheAncientsM);
        ModLoader.TryGetMod("CalamityMod", out CalamityMod);
    }
   
    public override void UpdateEquips()
    {
        if (Player.wingTimeMax > 4) 
            ReaperGlobalItem.ReaperWingsNerf(Player);
    }       
   
    /// <summary>
    /// Set Reaper mode souls config values
    /// </summary>
    /// <param name="type"></param>
    /// <param name="value"></param>
    public static void SetSoulsToggle(int type,int value)
    {
        switch (type)
        {
            case NPCID.KingSlime:
                GetInstance<ConfigReaperSouls>().ToggleKingSlimeSoul = value;
                break;
            case NPCID.SkeletronPrime:
                GetInstance<ConfigReaperSouls>().ToggleSkeletronPrimeSoul = value;
                break;          
        }
    }
    /// <summary>
    /// Set Reaper mode souls config values
    /// </summary>
    /// <param name="type"></param>
    /// <param name="value"></param>
    public static void SetSoulsToggle(int type, bool value)
    {
        if (type == NPCType<DesertAniquilator>())
            GetInstance<ConfigReaperSouls>().ToggleDesertAnhilatorSoul = value;
        else if (type == NPCType<FrozenAssaulter>())
            GetInstance<ConfigReaperSouls>().ToggleFrozenAssaulterSoul = value;
        else if (type == NPCType<InfernalTyrantHead>())
            GetInstance<ConfigReaperSouls>().ToggleInfernalTyrantSoul = value;
        else
        {
            switch (type)
            {
                case NPCID.EyeofCthulhu:
                    GetInstance<ConfigReaperSouls>().ToggleEyeOfChutuluSoul = value;
                    break;
                case NPCID.BrainofCthulhu:
                    GetInstance<ConfigReaperSouls>().ToggleCorruptSoul = value;
                    break;
                case NPCID.QueenBee:
                    GetInstance<ConfigReaperSouls>().ToggleQueenBeeSoul = value;
                    break;
                case NPCID.SkeletronHead:
                    GetInstance<ConfigReaperSouls>().ToggleSkeletronSoul = value;
                    break;
                case NPCID.Deerclops:
                    GetInstance<ConfigReaperSouls>().ToggleDearclopsSoul = value;
                    break;
                case NPCID.WallofFlesh:
                    GetInstance<ConfigReaperSouls>().ToggleWallOfFleshSoul = value;
                    break;
                case NPCID.QueenSlimeBoss:
                    GetInstance<ConfigReaperSouls>().ToggleQueenSlimeSoul = value;
                    break;
                case NPCID.Spazmatism:
                    GetInstance<ConfigReaperSouls>().ToggleSpazmatismSoul = value;
                    break;
                case NPCID.Retinazer:
                    GetInstance<ConfigReaperSouls>().ToggleRetinazorSoul = value;
                    break;
                case NPCID.TheDestroyer:
                    GetInstance<ConfigReaperSouls>().ToggleDestroyerSoul = value;
                    break;
                case NPCID.Plantera:
                    GetInstance<ConfigReaperSouls>().TogglePlanteraSoul = value;
                    break;         
                case NPCID.Golem:
                    GetInstance<ConfigReaperSouls>().ToggleGolemSoul = value;
                    break;
                case NPCID.HallowBoss:
                    GetInstance<ConfigReaperSouls>().ToggleEmpressOfLightSoul = value;
                    break;
                case NPCID.DukeFishron:
                    GetInstance<ConfigReaperSouls>().ToggleDukeFishronSoul = value;
                    break;
                case NPCID.CultistBoss:
                    GetInstance<ConfigReaperSouls>().ToggleLunaticCultistSoul = value;
                    break;
                case NPCID.MoonLordCore:
                    GetInstance<ConfigReaperSouls>().ToggleMoonlordSoul = value;
                    break;
            }
        }
    }
    public override void Kill(double damage, int hitDirection, bool pvp, PlayerDeathReason damageSource)
    {
        float respawnMult = RespawnTimeCalculator();
        float respawnTime = Player.respawnTimer * respawnMult;
        Player.respawnTimer = (int)respawnTime;  
    }
   
    public override void PostUpdateBuffs()
    {
        if (Reaper.ReaperMode)
            Player.AddBuff(BuffType<ReaperBuff>(), 2);
        
    }   
    private float RespawnTimeCalculator()
    {
        bool DesertAnhilatorActivated = SoulIsEnabled(NPCType<DesertAniquilator>());

        bool MoonlordRespawnActivated = SoulIsEnabled(NPCID.MoonLordCore);

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
        if (SoulIsEnabled(NPCID.BrainofCthulhu) && Reaper.ReaperMode)
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
        bool ChaliceOn = Player.GetModPlayer<ReaperPlayer>().ChaliceOn;
        float OgPlayerSpeed = Player.moveSpeed;
        if (Reaper.ReaperMode && ChaliceOn)
        {
            if (SoulIsEnabled(NPCID.KingSlime))
            {
                float speedForce = (SoulsUpgradesLoadedActive(NPCID.KingSlime) / 100) + 1;
                Player.moveSpeed = ReaperSoulBoost ? OgPlayerSpeed + speedForce * 1.2f : OgPlayerSpeed + speedForce;
            }
            if (SoulIsEnabled(NPCID.EyeofCthulhu))
            {
                Player.statLifeMax2 += ReaperSoulBoost ? 20 : 10;
            }
            if (SoulIsEnabled(NPCID.BrainofCthulhu))
            {
                Player.lifeRegen += ReaperSoulBoost ? 10 : 5;
            }
            if (SoulIsEnabled(NPCID.QueenBee))
            {
                Player.honey = true;
                Player.strongBees = true;         
                Player.beeDamage(ReaperSoulBoost? 80:20);    
            }
            if (SoulIsEnabled(NPCID.SkeletronHead))
            {
                Player.statDefense += ReaperSoulBoost ? 10 : 5;
            }
            if (SoulIsEnabled(NPCID.WallofFlesh))
            {
                Player.GetDamage(DamageClass.Generic) *= ReaperSoulBoost ? 1.20f : 1.10f;
            }
            if (SoulIsEnabled(NPCType<FrozenAssaulter>())) 
                Player.GetModPlayer<RemnantPlayer>().FrostInmune();
            if (SoulIsEnabled(NPCID.QueenSlimeBoss)) 
                Player.statLifeMax2 += ReaperSoulBoost ? 30 : 15;
            if (SoulIsEnabled(NPCID.TheDestroyer)) 
                Player.pickSpeed -= ReaperSoulBoost ? 0.7f : 0.35f;
            if (SoulIsEnabled(NPCID.Spazmatism)) 
                Player.GetModPlayer<RemnantPlayer>().FireInmune();
            if (SoulIsEnabled(NPCID.SkeletronPrime))
            {
                float JumpForce = SoulsUpgradesLoadedActive(NPCID.SkeletronPrime);
                Player.findTreasure = true;
                Player.dangerSense = ReaperSoulBoost;
                Player.jumpSpeedBoost += ReaperSoulBoost ? JumpForce * 2 : JumpForce;
            }
            if (SoulIsEnabled(NPCID.HallowBoss))
            {
                Player.empressBrooch = true;
                if (ReaperSoulBoost)
                {
                    Player.wingAccRunSpeed *= 2;
                }
            }

            if (SoulIsEnabled(NPCType<InfernalTyrantHead>()))
            {
                Player.fireWalk = true;
                Player.lavaImmune = true;
                Player.GetDamage(DamageClass.Generic) *= ReaperSoulBoost ? 1.20f : 1.10f;
            }
            if (SoulIsEnabled(NPCID.Golem)) 
                Player.statDefense += ReaperSoulBoost ? 15 : 10;

            if (Reaper.ReaperMode && SoulIsEnabled(NPCID.DukeFishron))
            {
                int damage = ReaperSoulBoost ? 280 : 140;
                Player.GetModPlayer<RemnantPlayer>().AddMinion(ProjectileType<TempestClone>(), damage, 10f);
                Player.aggro -= ReaperSoulBoost? 2400 : 400;            
            }
            if (SoulIsEnabled(NPCID.CultistBoss))
            {
                int damage = ReaperSoulBoost ? 980 : 680;
                Player.GetModPlayer<RemnantPlayer>().AddMinion(ProjectileType<IceMistF>(), damage, 10f); 
            }
        }
    }
    public void ReaperSoulsBoost(Item item)
    {
        bool ChaliceOn = Player.GetModPlayer<ReaperPlayer>().ChaliceOn;
        //float OgPlayerSpeed = Player.moveSpeed;
        if (Reaper.ReaperMode && ChaliceOn)
        {
            if (SoulIsEnabled(NPCID.BrainofCthulhu))
            {
                Player.brainOfConfusionItem = item;
            }
            if (SoulIsEnabled(NPCID.Plantera))
            {
                Player.sporeSac = true;
                Player.SporeSac(item);
                Player.statLifeMax2 += ReaperSoulBoost ? 15 : 10;
            }
            if (SoulIsEnabled(NPCID.Deerclops))
            {
                SpawnHallucination(item);
                if (ReaperSoulBoost)
                {
                    SpawnHallucination(item);
                }
            }
        }
    }
    public static bool SoulIsEnabled(int type) => SoulsUpgradesLoaded(type) && SoulsUpgradesLoadedActive(type) > 0;
    public static bool SoulsUpgradesLoaded(int type) => Main.LocalPlayer.GetModPlayer<ReaperSoulsPlayer>().SoulsUpgradesLoaded[ReaperSoulsPlayer.GetIndexFromLoadedBossById(type)];
    public static float SoulsUpgradesLoadedActive(int type) => Main.LocalPlayer.GetModPlayer<ReaperSoulsPlayer>().SoulsUpgradesLoadedActive[ReaperSoulsPlayer.GetIndexFromLoadedBossById(type)];

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

public class ReaperSoulsPlayer : ModPlayer
{
    public bool[] SoulsUpgradesLoaded =
    [
        false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false,
        false, false, false
    ];
    public float[] SoulsUpgradesLoadedActive =
    [
        0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f,
        0f, 0f, 0f
    ];
    public Dictionary<int, bool> SoulsUpgradesMaybeLoaded = [];
    public Dictionary<int, float> SoulsUpgradesMaybeLoadedActive = [];


    public override void SaveData(TagCompound tag)
    {
        ModLoader.TryGetMod("CalamityMod", out Mod CalamityMod);
        if(CalamityMod != null) FillSoulList();

        for (int i = 0; i < SoulsUpgradesLoaded.Length; i++)
        {
            NPC npc = ContentSamples.NpcsByNetId[GetLoadedBossIdByIndex(i)];
            if (npc.boss)
            {
                string TagName = npc.type + "ReaperUpgrade";
                tag[TagName] = SoulsUpgradesLoaded[GetIndexFromLoadedBossById(npc.type)];
                tag.Add(TagName + "Active", SoulsUpgradesLoadedActive[GetIndexFromLoadedBossById(npc.type)]);
            }         
        }
        if(CalamityMod != null)
        {
            foreach (NPC npc in NpcList.BossList[CalamityMod])
            {
                string TagName = npc.ModNPC.GetType().Name + "ReaperUpgrade";
                tag[TagName] = SoulsUpgradesMaybeLoaded[npc.type];
                tag.Add(TagName + "Active", SoulsUpgradesMaybeLoaded[npc.type]);
            }
        }
    }

    public override void LoadData(TagCompound tag)
    {
        ModLoader.TryGetMod("CalamityMod", out Mod CalamityMod);
        if (CalamityMod != null) FillSoulList();

        for (int i = 0; i < SoulsUpgradesLoaded.Length; i++)
        {
            NPC npc = ContentSamples.NpcsByNetId[GetLoadedBossIdByIndex(i)];
            if (npc.boss)
            {
                string TagDamage = npc.type + "ReaperUpgrade";
                SoulsUpgradesLoaded[GetIndexFromLoadedBossById(npc.type)] = tag.GetBool(TagDamage);
                SoulsUpgradesLoadedActive[GetIndexFromLoadedBossById(npc.type)] = tag.GetFloat(TagDamage + "Active");
            }
        }
        if (CalamityMod != null)
        {
            foreach (NPC npc in NpcList.BossList[CalamityMod])
            {
                string TagDamage = npc.ModNPC.GetType().Name + "ReaperUpgrade";
                SoulsUpgradesMaybeLoaded[npc.type] = tag.GetBool(TagDamage);
                //SoulsUpgradesMaybeLoadedActive[npc.type] = tag.GetFloat(TagDamage + "Active");
            }
        }
    }
    public override void SyncPlayer(int toWho, int fromWho, bool newPlayer)
    {
        ModPacket packet = Mod.GetPacket();
        LoadSoulsToggle();
        for (int i = 0; i < SoulsUpgradesLoaded.Length; i++)
        {
            packet.Write(SoulsUpgradesLoaded[i]);
            packet.Write((byte)SoulsUpgradesLoadedActive[i]);
        }
        for (int i = 0; i < SoulsUpgradesMaybeLoaded.Count; i++)
        {
            packet.Write(SoulsUpgradesMaybeLoaded[i]);
            packet.Write((byte)SoulsUpgradesMaybeLoadedActive[i]);
        }     
        packet.Send(toWho, fromWho);
    }

    public void LoadSoulsToggle()
    {
        SubloadSouls(NPCID.KingSlime, GetInstance<ConfigReaperSouls>().ToggleKingSlimeSoul);
        SubloadSouls(NPCID.EyeofCthulhu, GetInstance<ConfigReaperSouls>().ToggleEyeOfChutuluSoul.ToInt());
        SubloadSouls(NPCID.BrainofCthulhu, GetInstance<ConfigReaperSouls>().ToggleCorruptSoul.ToInt());
        SubloadSouls(NPCID.QueenBee, GetInstance<ConfigReaperSouls>().ToggleQueenBeeSoul.ToInt());
        SubloadSouls(NPCID.SkeletronHead, GetInstance<ConfigReaperSouls>().ToggleSkeletronSoul.ToInt());
        SubloadSouls(NPCID.Deerclops, GetInstance<ConfigReaperSouls>().ToggleDearclopsSoul.ToInt());
        SubloadSouls(NPCType<DesertAniquilator>(), GetInstance<ConfigReaperSouls>().ToggleDesertAnhilatorSoul.ToInt());
        SubloadSouls(NPCID.WallofFlesh, GetInstance<ConfigReaperSouls>().ToggleWallOfFleshSoul.ToInt());
        SubloadSouls(NPCType<FrozenAssaulter>(), GetInstance<ConfigReaperSouls>().ToggleFrozenAssaulterSoul.ToInt());
        SubloadSouls(NPCID.QueenSlimeBoss, GetInstance<ConfigReaperSouls>().ToggleQueenSlimeSoul.ToInt());
        SubloadSouls(NPCID.Spazmatism, GetInstance<ConfigReaperSouls>().ToggleSpazmatismSoul.ToInt());
        SubloadSouls(NPCID.Retinazer, GetInstance<ConfigReaperSouls>().ToggleRetinazorSoul.ToInt());
        SubloadSouls(NPCID.SkeletronPrime, GetInstance<ConfigReaperSouls>().ToggleSkeletronPrimeSoul);
        SubloadSouls(NPCID.TheDestroyer, GetInstance<ConfigReaperSouls>().ToggleDestroyerSoul.ToInt());
        SubloadSouls(NPCID.Plantera, GetInstance<ConfigReaperSouls>().TogglePlanteraSoul.ToInt());
        SubloadSouls(NPCType<InfernalTyrantHead>(), GetInstance<ConfigReaperSouls>().ToggleInfernalTyrantSoul.ToInt());
        SubloadSouls(NPCID.Golem, GetInstance<ConfigReaperSouls>().ToggleGolemSoul.ToInt());
        SubloadSouls(NPCID.HallowBoss, GetInstance<ConfigReaperSouls>().ToggleEmpressOfLightSoul.ToInt());
        SubloadSouls(NPCID.DukeFishron, GetInstance<ConfigReaperSouls>().ToggleDukeFishronSoul.ToInt());
        SubloadSouls(NPCID.CultistBoss, GetInstance<ConfigReaperSouls>().ToggleLunaticCultistSoul.ToInt());
        SubloadSouls(NPCID.MoonLordCore, GetInstance<ConfigReaperSouls>().ToggleMoonlordSoul.ToInt());


        void SubloadSouls(int type, float value)
        {
            SoulsUpgradesLoadedActive[GetIndexFromLoadedBossById(type)] = value;
        }
    }
    public void FillSoulList()
    {
        ModLoader.TryGetMod("CalamityMod", out Mod CalamityMod);
        foreach (NPC npc in NpcList.BossList[CalamityMod])
        {
            SoulsUpgradesMaybeLoaded.TryAdd(npc.type, false);
            SoulsUpgradesMaybeLoadedActive.TryAdd(npc.type, 0f); 
        }
    }
    public static int GetIndexFromLoadedBossById(int id)
    {
        switch (id)
        {
            case NPCID.KingSlime: return 0;
            case NPCID.EyeofCthulhu: return 1;
            case NPCID.BrainofCthulhu: return 2;
            case NPCID.QueenBee: return 3;
            case NPCID.SkeletronHead: return 4;
            case NPCID.Deerclops: return 5;
            case NPCID.WallofFlesh: return 6;
            case NPCID.QueenSlimeBoss: return 7;
            case NPCID.SkeletronPrime: return 8;
            case NPCID.Spazmatism: return 9;
            case NPCID.Retinazer: return 10;
            case NPCID.TheDestroyer: return 11;
            case NPCID.Plantera: return 12;
            case NPCID.Golem: return 13;
            case NPCID.HallowBoss: return 14;
            case NPCID.DukeFishron: return 15;
            case NPCID.CultistBoss: return 16;
            case NPCID.MoonLordCore: return 17;
            default:
                
                if(id == NPCType<DesertAniquilator>()) return 18;
                else if (id == NPCType<FrozenAssaulter>()) return 19;
                else if (id == NPCType<InfernalTyrantHead>()) return 20;
                return -1;
        }
    }
    public static int GetLoadedBossIdByIndex(int Index)
    {
        switch (Index)
        {
            case 0: return NPCID.KingSlime;
            case 1: return NPCID.EyeofCthulhu;
            case 2: return NPCID.BrainofCthulhu;
            case 3: return NPCID.QueenBee;
            case 4: return NPCID.SkeletronHead;
            case 5: return NPCID.Deerclops;
            case 6: return NPCID.WallofFlesh;
            case 7: return NPCID.QueenSlimeBoss;
            case 8: return NPCID.SkeletronPrime;
            case 9: return NPCID.Spazmatism;
            case 10: return NPCID.Retinazer;
            case 11: return NPCID.TheDestroyer;
            case 12: return NPCID.Plantera;
            case 13: return NPCID.Golem;
            case 14: return NPCID.HallowBoss;
            case 15: return NPCID.DukeFishron;
            case 16: return NPCID.CultistBoss;
            case 17: return NPCID.MoonLordCore; 
            case 18: return NPCType<DesertAniquilator>();
            case 19: return NPCType<FrozenAssaulter>();
            case 20: return NPCType<InfernalTyrantHead>();
            default: return -1;
        }
    }
    public bool AllSoulsAreActive()
    {
        ModLoader.TryGetMod("CalamityMod", out Mod CalamityMod);

        if(SoulsUpgradesLoaded.Contains(false))
            return false;
        if (CalamityMod != null)
        {
            if (SoulsUpgradesMaybeLoaded.ContainsValue(false))
                return false;
        } 
        return true;
    }
}