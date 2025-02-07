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
using RemnantOfTheAncientsMod.Common;
using RemnantOfTheAncientsMod.World;

public class ReaperPlayer : ModPlayer
{
    public bool ChaliceOn;
    public static bool FWeapons;
    public static bool ReaperFirstTime;
    private static List<NPC> _hallucinationCandidates = [];
    public bool ReaperSoulBoost;
   

    public List<bool[]> SoulsUpgrades =
    [
        [false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false],
        [false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false]
    ];

    public List<float[]> SoulsUpgradesActive =
    [
        [0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0],
        [0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0]
    ];
    public override void ResetEffects()
    {
        ReaperSoulBoost = false;
        ChaliceOn = false;
    }

    public override void OnEnterWorld()
    {
        FWeapons = true;
        ReaperGlobalItem.ReaperWingsNerf(Player);
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
        for (int i = 0; i < SoulsUpgrades[ModID.RemnantOfTheAncients].Length; i++)
        {
            packet.Write(SoulsUpgrades[ModID.RemnantOfTheAncients][i]);
        }
        if(RemnantOfTheAncientsMod.RemnantOfTheAncientsMod.CalamityMod != null)
        {
            for (int i = 0; i < SoulsUpgrades[ModID.Calamity].Length; i++)
            {
                packet.Write(SoulsUpgrades[ModID.Calamity][i]);
            }
        }

    }
    public override void SaveData(TagCompound tag)
    {
        tag["SlimeReaperUpgrade"] = SoulsUpgrades[ModID.RemnantOfTheAncients][BossID.VanillaID.KingSlime];
        tag["EyeReaperUpgrade"] = SoulsUpgrades[ModID.RemnantOfTheAncients][BossID.VanillaID.EyeOfChutulu];
        tag["CorruptReaperUpgrade"] = SoulsUpgrades[ModID.RemnantOfTheAncients][BossID.VanillaID.CorruptBoss];
        tag["BeeReaperUpgrade"] = SoulsUpgrades[ModID.RemnantOfTheAncients][BossID.VanillaID.QueenBee];
        tag["SkeletonReaperUpgrade"] = SoulsUpgrades[ModID.RemnantOfTheAncients][BossID.VanillaID.Skeletron];
        tag["DeerclopsReaperUpgrade"] = SoulsUpgrades[ModID.RemnantOfTheAncients][BossID.VanillaID.Deerclops];
        tag["DesertReaperUpgrade"] = SoulsUpgrades[ModID.RemnantOfTheAncients][BossID.VanillaID.DesertAnhilator];
        tag["FleshReaperUpgrade"] = SoulsUpgrades[ModID.RemnantOfTheAncients][BossID.VanillaID.WallOfFlesh];
        tag["FrozenReaperUpgrade"] = SoulsUpgrades[ModID.RemnantOfTheAncients][BossID.VanillaID.FrozenAssaulter];
        tag["QueenReaperUpgrade"] = SoulsUpgrades[ModID.RemnantOfTheAncients][BossID.VanillaID.QueenSlime];
        tag["SpazmatismReaperUpgrade"] = SoulsUpgrades[ModID.RemnantOfTheAncients][BossID.VanillaID.Spazmatism];
        tag["RetinazorReaperUpgrade"] = SoulsUpgrades[ModID.RemnantOfTheAncients][BossID.VanillaID.Retinazor];
        tag["SkeletronPrimeReaperUpgrade"] = SoulsUpgrades[ModID.RemnantOfTheAncients][BossID.VanillaID.SkeletronPrime];
        tag["DestroyerReaperUpgrade"] = SoulsUpgrades[ModID.RemnantOfTheAncients][BossID.VanillaID.Destroyer];
        tag["PlantReaperUpgrade"] = SoulsUpgrades[ModID.RemnantOfTheAncients][BossID.VanillaID.Plantera];
        tag["InfernalReaperUpgrade"] = SoulsUpgrades[ModID.RemnantOfTheAncients][BossID.VanillaID.InfernalTyrant];
        tag["GolemReaperUpgrade"] = SoulsUpgrades[ModID.RemnantOfTheAncients][BossID.VanillaID.Golem];
        tag["EmpressReaperUpgrade"] = SoulsUpgrades[ModID.RemnantOfTheAncients][BossID.VanillaID.EmpressOfLight];
        tag["DukeReaperUpgrade"] = SoulsUpgrades[ModID.RemnantOfTheAncients][BossID.VanillaID.DukeFishron];
        tag["CultistReaperUpgrade"] = SoulsUpgrades[ModID.RemnantOfTheAncients][BossID.VanillaID.Cultist];
        tag["MoonReaperUpgrade"] = SoulsUpgrades[ModID.RemnantOfTheAncients][BossID.VanillaID.MoonLord];

        if (RemnantOfTheAncientsMod.RemnantOfTheAncientsMod.CalamityMod != null)
        {
            tag["DesertScourgeUpgrade"] = SoulsUpgrades[ModID.Calamity][BossID.CalamityID.DesertScourge];
            tag["CrabulonUpgrade"] = SoulsUpgrades[ModID.Calamity][BossID.CalamityID.Crabulon];
            tag["CalCorruptBossUpgrade"] = SoulsUpgrades[ModID.Calamity][BossID.CalamityID.CalCorruptBoss];
            tag["SlimeGodUpgrade"] = SoulsUpgrades[ModID.Calamity][BossID.CalamityID.SlimeGod];
        }
    }
    public override void LoadData(TagCompound tag)
    {
       SoulsUpgrades[ModID.RemnantOfTheAncients][BossID.VanillaID.KingSlime] = tag.Get<bool>("SlimeReaperUpgrade");
       SoulsUpgrades[ModID.RemnantOfTheAncients][BossID.VanillaID.EyeOfChutulu] = tag.Get<bool>("EyeReaperUpgrade");
       SoulsUpgrades[ModID.RemnantOfTheAncients][BossID.VanillaID.CorruptBoss] = tag.Get<bool>("CorruptReaperUpgrade");
       SoulsUpgrades[ModID.RemnantOfTheAncients][BossID.VanillaID.QueenBee] = tag.Get<bool>("BeeReaperUpgrade");
       SoulsUpgrades[ModID.RemnantOfTheAncients][BossID.VanillaID.Skeletron] = tag.Get<bool>("SkeletonReaperUpgrade");
       SoulsUpgrades[ModID.RemnantOfTheAncients][BossID.VanillaID.Deerclops] = tag.Get<bool>("DeerclopsReaperUpgrade");
       SoulsUpgrades[ModID.RemnantOfTheAncients][BossID.VanillaID.DesertAnhilator] = tag.Get<bool>("DesertReaperUpgrade");
       SoulsUpgrades[ModID.RemnantOfTheAncients][BossID.VanillaID.WallOfFlesh] = tag.Get<bool>("FleshReaperUpgrade");
       SoulsUpgrades[ModID.RemnantOfTheAncients][BossID.VanillaID.FrozenAssaulter] = tag.Get<bool>("FrozenReaperUpgrade");
       SoulsUpgrades[ModID.RemnantOfTheAncients][BossID.VanillaID.QueenSlime] = tag.Get<bool>("QueenReaperUpgrade");
       SoulsUpgrades[ModID.RemnantOfTheAncients][BossID.VanillaID.Spazmatism] = tag.Get<bool>("SpazmatismReaperUpgrade");
       SoulsUpgrades[ModID.RemnantOfTheAncients][BossID.VanillaID.Retinazor] = tag.Get<bool>("RetinazorReaperUpgrade");
       SoulsUpgrades[ModID.RemnantOfTheAncients][BossID.VanillaID.SkeletronPrime] = tag.Get<bool>("SkeletronPrimeReaperUpgrade");
       SoulsUpgrades[ModID.RemnantOfTheAncients][BossID.VanillaID.Destroyer] = tag.Get<bool>("DestroyerReaperUpgrade");
       SoulsUpgrades[ModID.RemnantOfTheAncients][BossID.VanillaID.Plantera] = tag.Get<bool>("PlantReaperUpgrade");
       SoulsUpgrades[ModID.RemnantOfTheAncients][BossID.VanillaID.InfernalTyrant] = tag.Get<bool>("InfernalReaperUpgrade");
       SoulsUpgrades[ModID.RemnantOfTheAncients][BossID.VanillaID.Golem] = tag.Get<bool>("GolemReaperUpgrade");
       SoulsUpgrades[ModID.RemnantOfTheAncients][BossID.VanillaID.EmpressOfLight] = tag.Get<bool>("EmpressReaperUpgrade");
       SoulsUpgrades[ModID.RemnantOfTheAncients][BossID.VanillaID.DukeFishron] = tag.Get<bool>("DukeReaperUpgrade");
       SoulsUpgrades[ModID.RemnantOfTheAncients][BossID.VanillaID.Cultist] = tag.Get<bool>("CultistReaperUpgrade");
       SoulsUpgrades[ModID.RemnantOfTheAncients][BossID.VanillaID.MoonLord] = tag.Get<bool>("MoonReaperUpgrade");
    }
    public void LoadSoulsToggle()
    {
        SoulsUpgradesActive[ModID.RemnantOfTheAncients][BossID.VanillaID.KingSlime] = GetInstance<ConfigClient>().ToggleKingSlimeSoul;
        SoulsUpgradesActive[ModID.RemnantOfTheAncients][BossID.VanillaID.EyeOfChutulu] = GetInstance<ConfigClient>().ToggleEyeOfChutuluSoul.ToInt();
        SoulsUpgradesActive[ModID.RemnantOfTheAncients][BossID.VanillaID.CorruptBoss] = GetInstance<ConfigClient>().ToggleCorruptSoul.ToInt();
        SoulsUpgradesActive[ModID.RemnantOfTheAncients][BossID.VanillaID.QueenBee] = GetInstance<ConfigClient>().ToggleQueenBeeSoul.ToInt();
        SoulsUpgradesActive[ModID.RemnantOfTheAncients][BossID.VanillaID.Skeletron] = GetInstance<ConfigClient>().ToggleSkeletronSoul.ToInt();
        SoulsUpgradesActive[ModID.RemnantOfTheAncients][BossID.VanillaID.Deerclops] = GetInstance<ConfigClient>().ToggleDearclopsSoul.ToInt();
        SoulsUpgradesActive[ModID.RemnantOfTheAncients][BossID.VanillaID.DesertAnhilator] = GetInstance<ConfigClient>().ToggleDesertAnhilatorSoul.ToInt();
        SoulsUpgradesActive[ModID.RemnantOfTheAncients][BossID.VanillaID.WallOfFlesh] = GetInstance<ConfigClient>().ToggleWallOfFleshSoul.ToInt();
        SoulsUpgradesActive[ModID.RemnantOfTheAncients][BossID.VanillaID.FrozenAssaulter] = GetInstance<ConfigClient>().ToggleFrozenAssaulterSoul.ToInt();
        SoulsUpgradesActive[ModID.RemnantOfTheAncients][BossID.VanillaID.QueenSlime] = GetInstance<ConfigClient>().ToggleQueenSlimeSoul.ToInt();
        SoulsUpgradesActive[ModID.RemnantOfTheAncients][BossID.VanillaID.Spazmatism] = GetInstance<ConfigClient>().ToggleSpazmatismSoul.ToInt();
        SoulsUpgradesActive[ModID.RemnantOfTheAncients][BossID.VanillaID.Retinazor] = GetInstance<ConfigClient>().ToggleRetinazorSoul.ToInt();
        SoulsUpgradesActive[ModID.RemnantOfTheAncients][BossID.VanillaID.SkeletronPrime] = GetInstance<ConfigClient>().ToggleSkeletronPrimeSoul;
        SoulsUpgradesActive[ModID.RemnantOfTheAncients][BossID.VanillaID.Destroyer] = GetInstance<ConfigClient>().ToggleDestroyerSoul.ToInt();
        SoulsUpgradesActive[ModID.RemnantOfTheAncients][BossID.VanillaID.Plantera] = GetInstance<ConfigClient>().TogglePlanteraSoul.ToInt();
        SoulsUpgradesActive[ModID.RemnantOfTheAncients][BossID.VanillaID.InfernalTyrant] = GetInstance<ConfigClient>().ToggleInfernalTyrantSoul.ToInt();
        SoulsUpgradesActive[ModID.RemnantOfTheAncients][BossID.VanillaID.Golem] = GetInstance<ConfigClient>().ToggleGolemSoul.ToInt();
        SoulsUpgradesActive[ModID.RemnantOfTheAncients][BossID.VanillaID.EmpressOfLight] = GetInstance<ConfigClient>().ToggleEmpressOfLightSoul.ToInt();
        SoulsUpgradesActive[ModID.RemnantOfTheAncients][BossID.VanillaID.DukeFishron] = GetInstance<ConfigClient>().ToggleDukeFishronSoul.ToInt();
        SoulsUpgradesActive[ModID.RemnantOfTheAncients][BossID.VanillaID.Cultist] = GetInstance<ConfigClient>().ToggleLunaticCultistSoul.ToInt();
        SoulsUpgradesActive[ModID.RemnantOfTheAncients][BossID.VanillaID.MoonLord] = GetInstance<ConfigClient>().ToggleMoonlordSoul.ToInt();
    }
    public void SetSoulsToggle(int i,float value)
    {
        switch (i)
        {
            case 0:
                GetInstance<ConfigClient>().ToggleKingSlimeSoul = value;
                break;
            case 1:
                GetInstance<ConfigClient>().ToggleEyeOfChutuluSoul = (int)value == 1;
                break;
            case 2:
                GetInstance<ConfigClient>().ToggleCorruptSoul = (int)value == 1;
                break;
            case 3:
                GetInstance<ConfigClient>().ToggleQueenBeeSoul = (int)value == 1;
                break;
            case 4:
                GetInstance<ConfigClient>().ToggleSkeletronSoul = (int)value == 1;
                break;
            case 5:
                GetInstance<ConfigClient>().ToggleDearclopsSoul = (int)value == 1;
                break;
            case 6:
                GetInstance<ConfigClient>().ToggleDesertAnhilatorSoul = (int)value == 1;
                break;
            case 7:
                GetInstance<ConfigClient>().ToggleWallOfFleshSoul = (int)value == 1;
                break;
            case 8:
                GetInstance<ConfigClient>().ToggleFrozenAssaulterSoul = (int)value == 1;
                break;
            case 9:
                GetInstance<ConfigClient>().ToggleQueenSlimeSoul = (int)value == 1;
                break;
            case 10:
                GetInstance<ConfigClient>().ToggleSpazmatismSoul = (int)value == 1;
                break;
            case 11:
                GetInstance<ConfigClient>().ToggleRetinazorSoul = (int)value == 1;
                break;
            case 12:
                GetInstance<ConfigClient>().ToggleSkeletronPrimeSoul = value;
                break;
            case 13:
                GetInstance<ConfigClient>().ToggleDestroyerSoul = (int)value == 1;
                break;
            case 14:
                GetInstance<ConfigClient>().TogglePlanteraSoul = (int)value == 1;
                break;
            case 15:
                GetInstance<ConfigClient>().ToggleInfernalTyrantSoul = (int)value == 1;
                break;
            case 16:
                GetInstance<ConfigClient>().ToggleGolemSoul = (int)value == 1;
                break;
            case 17:
                GetInstance<ConfigClient>().ToggleEmpressOfLightSoul = (int)value == 1;
                break;
            case 18:
                GetInstance<ConfigClient>().ToggleDukeFishronSoul = (int)value == 1;
                break;
            case 19:
                GetInstance<ConfigClient>().ToggleLunaticCultistSoul = (int)value == 1;
                break;
            case 20:
                GetInstance<ConfigClient>().ToggleMoonlordSoul = (int)value == 1;
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
        bool DesertAnhilatorActivated = SoulsUpgradesActive[ModID.RemnantOfTheAncients][BossID.VanillaID.DesertAnhilator] == 1 &&SoulsUpgrades[ModID.RemnantOfTheAncients][BossID.VanillaID.DesertAnhilator];

        bool MoonlordRespawnActivated = SoulsUpgradesActive[ModID.RemnantOfTheAncients][BossID.VanillaID.MoonLord] == 1 &&SoulsUpgrades[ModID.RemnantOfTheAncients][BossID.VanillaID.MoonLord];

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
        if (SoulsUpgrades[ModID.RemnantOfTheAncients][BossID.VanillaID.CorruptBoss] && SoulsUpgradesActive[ModID.RemnantOfTheAncients][BossID.VanillaID.CorruptBoss] == 1 && Reaper.ReaperMode)
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
            if (SoulsUpgrades[ModID.RemnantOfTheAncients][BossID.VanillaID.KingSlime])
            {
                float speedForce = (SoulsUpgradesActive[ModID.RemnantOfTheAncients][BossID.VanillaID.KingSlime] / 100) + 1;
                Player.moveSpeed = ReaperSoulBoost ? OgPlayerSpeed + speedForce * 1.2f : OgPlayerSpeed + speedForce;
            }
            if (SoulsUpgrades[ModID.RemnantOfTheAncients][BossID.VanillaID.EyeOfChutulu] && SoulsUpgradesActive[ModID.RemnantOfTheAncients][BossID.VanillaID.EyeOfChutulu] == 1)
            {
                Player.statLifeMax2 += ReaperSoulBoost ? 20 : 10;
            }
            if (SoulsUpgrades[ModID.RemnantOfTheAncients][BossID.VanillaID.CorruptBoss] && SoulsUpgradesActive[ModID.RemnantOfTheAncients][BossID.VanillaID.CorruptBoss] == 1)
            {
                Player.lifeRegen += ReaperSoulBoost ? 10 : 5;
            }
            if (SoulsUpgrades[ModID.RemnantOfTheAncients][BossID.VanillaID.QueenBee] && SoulsUpgradesActive[ModID.RemnantOfTheAncients][BossID.VanillaID.QueenBee] == 1)
            {
                Player.honey = true;
                Player.strongBees = true;         
                Player.beeDamage(ReaperSoulBoost? 80:20);    
            }
            if (SoulsUpgrades[ModID.RemnantOfTheAncients][BossID.VanillaID.Skeletron] && SoulsUpgradesActive[ModID.RemnantOfTheAncients][BossID.VanillaID.Skeletron] == 1)
            {
                Player.statDefense += ReaperSoulBoost ? 10 : 5;
            }
            if (SoulsUpgrades[ModID.RemnantOfTheAncients][BossID.VanillaID.WallOfFlesh] && SoulsUpgradesActive[ModID.RemnantOfTheAncients][BossID.VanillaID.WallOfFlesh] == 1)
            {
                Player.GetDamage(DamageClass.Generic) *= ReaperSoulBoost ? 1.20f : 1.10f;
            }
            if (SoulsUpgrades[ModID.RemnantOfTheAncients][BossID.VanillaID.FrozenAssaulter] && SoulsUpgradesActive[ModID.RemnantOfTheAncients][BossID.VanillaID.FrozenAssaulter] == 1) Player.GetModPlayer<RemnantPlayer>().FrostInmune();
            if (SoulsUpgrades[ModID.RemnantOfTheAncients][BossID.VanillaID.QueenSlime] && SoulsUpgradesActive[ModID.RemnantOfTheAncients][BossID.VanillaID.QueenSlime] == 1) Player.statLifeMax2 += ReaperSoulBoost ? 30 : 15;
            if (SoulsUpgrades[ModID.RemnantOfTheAncients][BossID.VanillaID.Destroyer] && SoulsUpgradesActive[ModID.RemnantOfTheAncients][BossID.VanillaID.Destroyer] == 1) Player.pickSpeed -= ReaperSoulBoost ? 0.7f : 0.35f;
            if (SoulsUpgrades[ModID.RemnantOfTheAncients][BossID.VanillaID.Spazmatism] && SoulsUpgradesActive[ModID.RemnantOfTheAncients][BossID.VanillaID.Spazmatism] == 1) Player.GetModPlayer<RemnantPlayer>().FireInmune();
            if (SoulsUpgrades[ModID.RemnantOfTheAncients][BossID.VanillaID.SkeletronPrime])
            {
                float JumpForce = SoulsUpgradesActive[ModID.RemnantOfTheAncients][BossID.VanillaID.SkeletronPrime];
                Player.findTreasure = true;
                Player.dangerSense = ReaperSoulBoost;
                Player.jumpSpeedBoost += ReaperSoulBoost ? JumpForce * 2 : JumpForce;
            }
            if (SoulsUpgrades[ModID.RemnantOfTheAncients][BossID.VanillaID.EmpressOfLight] && SoulsUpgradesActive[ModID.RemnantOfTheAncients][BossID.VanillaID.EmpressOfLight] == 1)
            {
                Player.empressBrooch = true;
                if (ReaperSoulBoost)
                {
                    Player.wingAccRunSpeed *= 2;
                }
            }

            if (SoulsUpgrades[ModID.RemnantOfTheAncients][BossID.VanillaID.InfernalTyrant] && SoulsUpgradesActive[ModID.RemnantOfTheAncients][BossID.VanillaID.InfernalTyrant] == 1)
            {
                Player.fireWalk = true;
                Player.lavaImmune = true;
                Player.GetDamage(DamageClass.Generic) *= ReaperSoulBoost ? 1.20f : 1.10f;
            }
            if (SoulsUpgrades[ModID.RemnantOfTheAncients][BossID.VanillaID.Golem] && SoulsUpgradesActive[ModID.RemnantOfTheAncients][BossID.VanillaID.Golem] == 1) Player.statDefense += ReaperSoulBoost ? 15 : 10;

            if (Reaper.ReaperMode &&SoulsUpgrades[ModID.RemnantOfTheAncients][BossID.VanillaID.DukeFishron] && SoulsUpgradesActive[ModID.RemnantOfTheAncients][BossID.VanillaID.DukeFishron] == 1)
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
            if (SoulsUpgrades[ModID.RemnantOfTheAncients][BossID.VanillaID.Cultist] && SoulsUpgradesActive[ModID.RemnantOfTheAncients][BossID.VanillaID.Cultist] == 1)
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
            if (SoulsUpgrades[ModID.RemnantOfTheAncients][BossID.VanillaID.CorruptBoss] && SoulsUpgradesActive[ModID.RemnantOfTheAncients][BossID.VanillaID.CorruptBoss] == 1)
            {
                Player.brainOfConfusionItem = item;
            }
            if (SoulsUpgrades[ModID.RemnantOfTheAncients][BossID.VanillaID.Plantera] && SoulsUpgradesActive[ModID.RemnantOfTheAncients][BossID.VanillaID.Plantera] == 1)
            {
                Player.sporeSac = true;
                Player.SporeSac(item);
                Player.statLifeMax2 += ReaperSoulBoost ? 15 : 10;
            }
            if (SoulsUpgrades[ModID.RemnantOfTheAncients][BossID.VanillaID.Deerclops] && SoulsUpgradesActive[ModID.RemnantOfTheAncients][BossID.VanillaID.Deerclops] == 1)
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
            if (!SoulsUpgrades[ModID.RemnantOfTheAncients][BossID.VanillaID.KingSlime]) return false;
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