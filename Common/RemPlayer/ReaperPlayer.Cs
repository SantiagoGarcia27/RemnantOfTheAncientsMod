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

    public Dictionary<int, bool> SoulsUpgrades = [];
    public Dictionary<int, float> SoulsUpgradesActive = [];

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
        ChaliceOn = false;
    }

    public override void OnEnterWorld()
    {
        FWeapons = true;

        ModLoader.TryGetMod("SangarUtilities", out TerrariaMod);
        ModLoader.TryGetMod("RemnantOfTheAncientsMod", out RemnantOfTheAncientsM);
        ModLoader.TryGetMod("CalamityMod", out CalamityMod);
    }

    public void FillSoulList()
    {
        foreach (var m in NpcList.BossList.Keys)
        {
            if (m == RemnantOfTheAncientsM)
            {
                NpcList.BossList[m] =
                [
                    ContentSamples.NpcsByNetId[NPCType<DesertAniquilator>()],
                    ContentSamples.NpcsByNetId[NPCType<FrozenAssaulter>()],
                    ContentSamples.NpcsByNetId[NPCType<InfernalTyrantHead>()],
                ];
            }
            List<NPC> currentList = NpcList.BossList[m];
            foreach (NPC npc in currentList)
            {
                if(!SoulsUpgrades.ContainsKey(npc.type))
                    SoulsUpgrades.TryAdd(npc.type, false);
                else if (SoulsUpgrades[npc.type] != true)
                    SoulsUpgrades.TryAdd(npc.type,false);
                 SoulsUpgradesActive.TryAdd(npc.type, 0f);
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
        if (Player.wingTimeMax > 4) 
            ReaperGlobalItem.ReaperWingsNerf(Player);
    }

    public override void SyncPlayer(int toWho, int fromWho, bool newPlayer)
    {
        ModPacket packet = Mod.GetPacket();
        foreach (var mod in NpcList.BossList.Keys)
        {
            for (int i = 0; i < SoulsUpgrades.Count; i++)
            {
                packet.Write(SoulsUpgrades[i]);
            }
        }
        packet.Send(toWho, fromWho);
    }
    public override void SaveData(TagCompound tag)
    {
        ModLoader.TryGetMod("SangarUtilities", out TerrariaMod);
        ModLoader.TryGetMod("RemnantOfTheAncientsMod", out RemnantOfTheAncientsM);
        ModLoader.TryGetMod("CalamityMod", out CalamityMod);

        FillSoulList();

        foreach (Mod mod in ModLoader.Mods)
        {
            foreach (NPC npc in NpcList.BossList[mod])
            {
                string name = mod == TerrariaMod ? npc.type.ToString() : npc.ModNPC.GetType().Name;
                tag[name + "ReaperUpgrade"] = SoulsUpgrades[npc.type];
            }
        }

    }
    public override void LoadData(TagCompound tag)
    {
        ModLoader.TryGetMod("SangarUtilities", out TerrariaMod);
        ModLoader.TryGetMod("RemnantOfTheAncientsMod", out RemnantOfTheAncientsM);
        ModLoader.TryGetMod("CalamityMod", out CalamityMod);

        FillSoulList();

        foreach (Mod mod in ModLoader.Mods)
        {
            foreach (NPC npc in NpcList.BossList[mod])
            {
                string name = mod == TerrariaMod ? npc.type.ToString() : npc.ModNPC.GetType().Name;
                bool? value = tag.Get<bool>(name + "ReaperUpgrade");
                value ??= false;
                SoulsUpgrades[npc.type] = (bool)value;
            }

        }
    }
        
    public void LoadSoulsToggle()
    {
        SoulsUpgradesActive[NPCID.KingSlime] = GetInstance<ConfigReaperSouls>().ToggleKingSlimeSoul;
        SoulsUpgradesActive[NPCID.EyeofCthulhu] = GetInstance<ConfigReaperSouls>().ToggleEyeOfChutuluSoul.ToInt();
        SoulsUpgradesActive[NPCID.BrainofCthulhu] = GetInstance<ConfigReaperSouls>().ToggleCorruptSoul.ToInt();
        SoulsUpgradesActive[NPCID.QueenBee] = GetInstance<ConfigReaperSouls>().ToggleQueenBeeSoul.ToInt();
        SoulsUpgradesActive[NPCID.SkeletronHead] = GetInstance<ConfigReaperSouls>().ToggleSkeletronSoul.ToInt();
        SoulsUpgradesActive[NPCID.Deerclops] = GetInstance<ConfigReaperSouls>().ToggleDearclopsSoul.ToInt();
        SoulsUpgradesActive[NPCType<DesertAniquilator>()] = GetInstance<ConfigReaperSouls>().ToggleDesertAnhilatorSoul.ToInt();
        SoulsUpgradesActive[NPCID.WallofFlesh] = GetInstance<ConfigReaperSouls>().ToggleWallOfFleshSoul.ToInt();
        SoulsUpgradesActive[NPCType<FrozenAssaulter>()] = GetInstance<ConfigReaperSouls>().ToggleFrozenAssaulterSoul.ToInt();
        SoulsUpgradesActive[NPCID.QueenSlimeBoss] = GetInstance<ConfigReaperSouls>().ToggleQueenSlimeSoul.ToInt();
        SoulsUpgradesActive[NPCID.Spazmatism] = GetInstance<ConfigReaperSouls>().ToggleSpazmatismSoul.ToInt();
        SoulsUpgradesActive[NPCID.Retinazer] = GetInstance<ConfigReaperSouls>().ToggleRetinazorSoul.ToInt();
        SoulsUpgradesActive[NPCID.SkeletronPrime] = GetInstance<ConfigReaperSouls>().ToggleSkeletronPrimeSoul;
        SoulsUpgradesActive[NPCID.TheDestroyer] = GetInstance<ConfigReaperSouls>().ToggleDestroyerSoul.ToInt();
        SoulsUpgradesActive[NPCID.Plantera] = GetInstance<ConfigReaperSouls>().TogglePlanteraSoul.ToInt();
        SoulsUpgradesActive[NPCType<InfernalTyrantHead>()] = GetInstance<ConfigReaperSouls>().ToggleInfernalTyrantSoul.ToInt();
        SoulsUpgradesActive[NPCID.Golem] = GetInstance<ConfigReaperSouls>().ToggleGolemSoul.ToInt();
        SoulsUpgradesActive[NPCID.HallowBoss] = GetInstance<ConfigReaperSouls>().ToggleEmpressOfLightSoul.ToInt();
        SoulsUpgradesActive[NPCID.DukeFishron] = GetInstance<ConfigReaperSouls>().ToggleDukeFishronSoul.ToInt();
        SoulsUpgradesActive[NPCID.CultistBoss] = GetInstance<ConfigReaperSouls>().ToggleLunaticCultistSoul.ToInt();
        SoulsUpgradesActive[NPCID.MoonLordCore] = GetInstance<ConfigReaperSouls>().ToggleMoonlordSoul.ToInt();

       
    }
/// <summary>
/// Set Reaper mode souls config values
/// </summary>
/// <param name="type"></param>
/// <param name="value"></param>
    public void SetSoulsToggle(int type,int value)
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
        LoadSoulsToggle();
    }
    /// <summary>
    /// Set Reaper mode souls config values
    /// </summary>
    /// <param name="type"></param>
    /// <param name="value"></param>
    public void SetSoulsToggle(int type, bool value)
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
        LoadSoulsToggle();
    }
    public override void Kill(double damage, int hitDirection, bool pvp, PlayerDeathReason damageSource)
    {
        float respawnMult = RespawnTimeCalculator();
        float respawnTime = Player.respawnTimer * respawnMult;
        Player.respawnTimer = (int)respawnTime;  
    }
    public void DropReaperStarterKit()
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
    private float RespawnTimeCalculator()
    {
        bool DesertAnhilatorActivated = SoulsUpgradesActive[NPCType<DesertAniquilator>()] == 1 && SoulsUpgrades[NPCType<DesertAniquilator>()];

        bool MoonlordRespawnActivated = SoulsUpgradesActive[NPCID.MoonLordCore] == 1 && SoulsUpgrades[NPCID.MoonLordCore];

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
        if (SoulsUpgrades[NPCID.BrainofCthulhu] && SoulsUpgradesActive[NPCID.BrainofCthulhu] == 1 && Reaper.ReaperMode)
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
            if (SoulsUpgrades[NPCID.KingSlime] && SoulsUpgradesActive[NPCID.KingSlime] > 0)
            {
                float speedForce = (SoulsUpgradesActive[NPCID.KingSlime] / 100) + 1;
                Player.moveSpeed = ReaperSoulBoost ? OgPlayerSpeed + speedForce * 1.2f : OgPlayerSpeed + speedForce;
            }
            if (SoulsUpgrades[NPCID.EyeofCthulhu] && SoulsUpgradesActive[NPCID.EyeofCthulhu] == 1)
            {
                Player.statLifeMax2 += ReaperSoulBoost ? 20 : 10;
            }
            if ( SoulsUpgrades[NPCID.BrainofCthulhu] && SoulsUpgradesActive[NPCID.BrainofCthulhu] == 1)
            {
                Player.lifeRegen += ReaperSoulBoost ? 10 : 5;
            }
            if (SoulsUpgrades[NPCID.QueenBee] && SoulsUpgradesActive[NPCID.QueenBee] == 1)
            {
                Player.honey = true;
                Player.strongBees = true;         
                Player.beeDamage(ReaperSoulBoost? 80:20);    
            }
            if (SoulsUpgrades[NPCID.SkeletronHead] && SoulsUpgradesActive[NPCID.SkeletronHead] == 1)
            {
                Player.statDefense += ReaperSoulBoost ? 10 : 5;
            }
            if (SoulsUpgrades[NPCID.WallofFlesh] && SoulsUpgradesActive[NPCID.WallofFlesh] == 1)
            {
                Player.GetDamage(DamageClass.Generic) *= ReaperSoulBoost ? 1.20f : 1.10f;
            }
            if (SoulsUpgrades[NPCType<FrozenAssaulter>()] && SoulsUpgradesActive[NPCType<FrozenAssaulter>()] == 1) 
                Player.GetModPlayer<RemnantPlayer>().FrostInmune();
            if (SoulsUpgrades[NPCID.QueenSlimeBoss] && SoulsUpgradesActive[NPCID.QueenSlimeBoss] == 1) 
                Player.statLifeMax2 += ReaperSoulBoost ? 30 : 15;
            if (SoulsUpgrades[NPCID.TheDestroyer] && SoulsUpgradesActive[NPCID.TheDestroyer] == 1) 
                Player.pickSpeed -= ReaperSoulBoost ? 0.7f : 0.35f;
            if (SoulsUpgrades[NPCID.Spazmatism] && SoulsUpgradesActive[NPCID.Spazmatism] == 1) 
                Player.GetModPlayer<RemnantPlayer>().FireInmune();
            if (SoulsUpgrades[NPCID.SkeletronPrime])
            {
                float JumpForce = SoulsUpgradesActive[NPCID.SkeletronPrime];
                Player.findTreasure = true;
                Player.dangerSense = ReaperSoulBoost;
                Player.jumpSpeedBoost += ReaperSoulBoost ? JumpForce * 2 : JumpForce;
            }
            if (SoulsUpgrades[NPCID.HallowBoss] && SoulsUpgradesActive[NPCID.HallowBoss] == 1)
            {
                Player.empressBrooch = true;
                if (ReaperSoulBoost)
                {
                    Player.wingAccRunSpeed *= 2;
                }
            }

            if (SoulsUpgrades[NPCType<InfernalTyrantHead>()] && SoulsUpgradesActive[NPCType<InfernalTyrantHead>()] == 1)
            {
                Player.fireWalk = true;
                Player.lavaImmune = true;
                Player.GetDamage(DamageClass.Generic) *= ReaperSoulBoost ? 1.20f : 1.10f;
            }
            if (SoulsUpgrades[NPCID.Golem] && SoulsUpgradesActive[NPCID.Golem] == 1) Player.statDefense += ReaperSoulBoost ? 15 : 10;

            if (Reaper.ReaperMode &&SoulsUpgrades[NPCID.DukeFishron] && SoulsUpgradesActive[NPCID.DukeFishron] == 1)
            {
                int damage = ReaperSoulBoost ? 280 : 140;
                Player.GetModPlayer<RemnantPlayer>().AddMinion(ProjectileType<TempestClone>(), damage, 10f);
                Player.aggro -= ReaperSoulBoost? 2400 : 400;            
            }
            if (SoulsUpgrades[NPCID.CultistBoss] && SoulsUpgradesActive[NPCID.CultistBoss] == 1)
            {
                int damage = ReaperSoulBoost ? 980 : 680;
                Player.GetModPlayer<RemnantPlayer>().AddMinion(ProjectileType<IceMistF>(), damage, 10f); 
            }
        }
    }
    public void ReaperSoulsBoost(Item item)
    {
        if (Reaper.ReaperMode && ChaliceOn)
        {
            ModLoader.TryGetMod("SangarUtilities", out TerrariaMod);
            Dictionary<int, bool> soulVanilla = SoulsUpgrades;
            Dictionary<int, float> soulVanillaActive = SoulsUpgradesActive;
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
    public static bool AllSoulsAreActive()
    {
        foreach (Mod mod in NpcList.BossList.Keys)
        {
            if(Main.LocalPlayer.GetModPlayer<ReaperPlayer>().SoulsUpgrades.ContainsValue(false))
                return false;
            foreach (KeyValuePair<int, bool> npc in Main.LocalPlayer.GetModPlayer<ReaperPlayer>().SoulsUpgrades)
            {
                if (!npc.Value)
                    return false;
            }
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