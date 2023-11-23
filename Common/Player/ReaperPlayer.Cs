using RemnantOfTheAncientsMod.Content.Items.Consumables.ReaperSouls;
using RemnantOfTheAncientsMod.Content.Projectiles;
using RemnantOfTheAncientsMod.World;
using RemnantOfTheAncientsMod;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using System.Collections.Generic;
using Terraria;
using RemnantOfTheAncientsMod.Common.Global;
using RemnantOfTheAncientsMod.Content.Items.Consumables.DificultChanger;
using RemnantOfTheAncientsMod.Content.Items.Accesories;
using Terraria.ID;
using Terraria.DataStructures;
using RemnantOfTheAncientsMod.Content.Buffs.Buffs;
using Terraria.ModLoader.IO;
using Microsoft.Xna.Framework;
using RemnantOfTheAncientsMod.Common.Global.Items;

public class ReaperPlayer : ModPlayer
{
    public bool ChaliceOn;
    public static bool FWeapons;
    public static bool ReaperFirstTime;
    private static List<NPC> _hallucinationCandidates = new List<NPC>();
    public bool ReaperSoulBoost;
    //public bool SlimeReaperUpgrade = false;
    //public bool EyeReaperUpgrade = false;
    //public bool CorruptReaperUpgrade = false;
    //public bool BeeReaperUpgrade = false;
    //public bool SkeletonReaperUpgrade = false;
    //public bool DeerclopsReaperUpgrade = false;
    //public bool DesertReaperUpgrade = false;
    //public bool FleshReaperUpgrade = false;
    //public bool FrozenReaperUpgrade = false;
    //public bool QueenReaperUpgrade = false;
    //public bool SpazmatismReaperUpgrade = false;
    //public bool RetinazorReaperUpgrade = false;
    //public bool SkeletronPrimeReaperUpgrade = false;
    //public bool DestroyerReaperUpgrade = false;
    //public bool PlantReaperUpgrade = false;
    //public bool InfernalReaperUpgrade = false;
    //public bool GolemReaperUpgrade = false;
    //public bool EmpressReaperUpgrade = false;
    //public bool DukeReaperUpgrade = false;
    //public bool CultistReaperUpgrade = false;
    //public bool MoonReaperUpgrade = false;

    public List<bool> SoulsUpgrades = new List<bool>()
    {
        false,false,false,false,
        false,false,false,false,
        false,false,false,false,
        false,false,false,false,
        false,false,false,false,
        false
    };
    public List<float> SoulsUpgradesActive = new List<float>()
    {
        0,0,0,0,
        0,0,0,0,
        0,0,0,0,
        0,0,0,0,
        0,0,0,0,
        0
    };
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
        if (mediumCoreDeath) return new[] { new Item(ItemType<Ftoggler>()) };
        return new[] { new Item(ItemType<Ftoggler>()), };
    }
    public override void UpdateEquips()
    {
        if (Player.wingTimeMax > 4) ReaperGlobalItem.ReaperWingsNerf(Player);
    }

    //SoulsUpgrades[0] = SlimeReaperUpgrade;
    //SoulsUpgrades[1] = EyeReaperUpgrade;
    //SoulsUpgrades[2] = CorruptReaperUpgrade;
    //SoulsUpgrades[3] = BeeReaperUpgrade;
    //SoulsUpgrades[4] = SkeletonReaperUpgrade;
    //SoulsUpgrades[5] = DeerclopsReaperUpgrade;
    //SoulsUpgrades[6] = DesertReaperUpgrade;
    //SoulsUpgrades[7] = FleshReaperUpgrade;
    //SoulsUpgrades[8] = FrozenReaperUpgrade;
    //SoulsUpgrades[9] = QueenReaperUpgrade;
    //SoulsUpgrades[10] = SpazmatismReaperUpgrade;
    //SoulsUpgrades[11] = RetinazorReaperUpgrade;
    //SoulsUpgrades[12] = SkeletronPrimeReaperUpgrade;
    //SoulsUpgrades[13] = DestroyerReaperUpgrade;
    //SoulsUpgrades[14] = PlantReaperUpgrade;
    //SoulsUpgrades[15] = InfernalReaperUpgrade;
    //SoulsUpgrades[16] = GolemReaperUpgrade;
    //SoulsUpgrades[17] = EmpressReaperUpgrade;
    //SoulsUpgrades[18] = DukeReaperUpgrade;
    //SoulsUpgrades[19] = CultistReaperUpgrade;
    //SoulsUpgrades[20] = MoonReaperUpgrade;

    public override void SyncPlayer(int toWho, int fromWho, bool newPlayer)
    {
        ModPacket packet = Mod.GetPacket();
        for (int i = 0; i < SoulsUpgrades.Count; i++)
        {
            packet.Write(SoulsUpgrades[i]);
        }

    }
    public override void SaveData(TagCompound tag)
    {
        tag["SlimeReaperUpgrade"] = SoulsUpgrades[0];
        tag["EyeReaperUpgrade"] = SoulsUpgrades[1];
        tag["CorruptReaperUpgrade"] = SoulsUpgrades[2];
        tag["BeeReaperUpgrade"] = SoulsUpgrades[3];
        tag["SkeletonReaperUpgrade"] = SoulsUpgrades[4];
        tag["DeerclopsReaperUpgrade"] = SoulsUpgrades[5];
        tag["DesertReaperUpgrade"] = SoulsUpgrades[6];
        tag["FleshReaperUpgrade"] = SoulsUpgrades[7];
        tag["FrozenReaperUpgrade"] = SoulsUpgrades[8];
        tag["QueenReaperUpgrade"] = SoulsUpgrades[9];
        tag["SpazmatismReaperUpgrade"] = SoulsUpgrades[10];
        tag["RetinazorReaperUpgrade"] = SoulsUpgrades[11];
        tag["SkeletronPrimeReaperUpgrade"] = SoulsUpgrades[12];
        tag["DestroyerReaperUpgrade"] = SoulsUpgrades[13];
        tag["PlantReaperUpgrade"] = SoulsUpgrades[14];
        tag["InfernalReaperUpgrade"] = SoulsUpgrades[15];
        tag["GolemReaperUpgrade"] = SoulsUpgrades[16];
        tag["EmpressReaperUpgrade"] = SoulsUpgrades[17];
        tag["DukeReaperUpgrade"] = SoulsUpgrades[18];
        tag["CultistReaperUpgrade"] = SoulsUpgrades[19];
        tag["MoonReaperUpgrade"] = SoulsUpgrades[20];

    }
    public override void LoadData(TagCompound tag)
    {
        SoulsUpgrades[0] = tag.Get<bool>("SlimeReaperUpgrade");
        SoulsUpgrades[1] = tag.Get<bool>("EyeReaperUpgrade");
        SoulsUpgrades[2] = tag.Get<bool>("CorruptReaperUpgrade");
        SoulsUpgrades[3] = tag.Get<bool>("BeeReaperUpgrade");
        SoulsUpgrades[4] = tag.Get<bool>("SkeletonReaperUpgrade");
        SoulsUpgrades[5] = tag.Get<bool>("DeerclopsReaperUpgrade");
        SoulsUpgrades[6] = tag.Get<bool>("DesertReaperUpgrade");
        SoulsUpgrades[7] = tag.Get<bool>("FleshReaperUpgrade");
        SoulsUpgrades[8] = tag.Get<bool>("FrozenReaperUpgrade");
        SoulsUpgrades[9] = tag.Get<bool>("QueenReaperUpgrade");
        SoulsUpgrades[10] = tag.Get<bool>("SpazmatismReaperUpgrade");
        SoulsUpgrades[11] = tag.Get<bool>("RetinazorReaperUpgrade");
        SoulsUpgrades[12] = tag.Get<bool>("SkeletronPrimeReaperUpgrade");
        SoulsUpgrades[13] = tag.Get<bool>("DestroyerReaperUpgrade");
        SoulsUpgrades[14] = tag.Get<bool>("PlantReaperUpgrade");
        SoulsUpgrades[15] = tag.Get<bool>("InfernalReaperUpgrade");
        SoulsUpgrades[16] = tag.Get<bool>("GolemReaperUpgrade");
        SoulsUpgrades[17] = tag.Get<bool>("EmpressReaperUpgrade");
        SoulsUpgrades[18] = tag.Get<bool>("DukeReaperUpgrade");
        SoulsUpgrades[19] = tag.Get<bool>("CultistReaperUpgrade");
        SoulsUpgrades[20] = tag.Get<bool>("MoonReaperUpgrade");
    }
    public void LoadSoulsToggle()
    {
        SoulsUpgradesActive[0] = GetInstance<ConfigClient>().ToggleKingSlimeSoul;
        SoulsUpgradesActive[1] = GetInstance<ConfigClient>().ToggleEyeOfChutuluSoul.ToInt();
        SoulsUpgradesActive[2] = GetInstance<ConfigClient>().ToggleCorruptSoul.ToInt();
        SoulsUpgradesActive[3] = GetInstance<ConfigClient>().ToggleQueenBeeSoul.ToInt();
        SoulsUpgradesActive[4] = GetInstance<ConfigClient>().ToggleSkeletronSoul.ToInt();
        SoulsUpgradesActive[5] = GetInstance<ConfigClient>().ToggleDearclopsSoul.ToInt();
        SoulsUpgradesActive[6] = GetInstance<ConfigClient>().ToggleDesertAnhilatorSoul.ToInt();
        SoulsUpgradesActive[7] = GetInstance<ConfigClient>().ToggleWallOfFleshSoul.ToInt();
        SoulsUpgradesActive[8] = GetInstance<ConfigClient>().ToggleFrozenAssaulterSoul.ToInt();
        SoulsUpgradesActive[9] = GetInstance<ConfigClient>().ToggleQueenSlimeSoul.ToInt();
        SoulsUpgradesActive[10] = GetInstance<ConfigClient>().ToggleSpazmatismSoul.ToInt();
        SoulsUpgradesActive[11] = GetInstance<ConfigClient>().ToggleRetinazorSoul.ToInt();
        SoulsUpgradesActive[12] = GetInstance<ConfigClient>().ToggleSkeletronPrimeSoul;
        SoulsUpgradesActive[13] = GetInstance<ConfigClient>().ToggleDestroyerSoul.ToInt();
        SoulsUpgradesActive[14] = GetInstance<ConfigClient>().TogglePlanteraSoul.ToInt();
        SoulsUpgradesActive[15] = GetInstance<ConfigClient>().ToggleInfernalTyrantSoul.ToInt();
        SoulsUpgradesActive[16] = GetInstance<ConfigClient>().ToggleGolemSoul.ToInt();
        SoulsUpgradesActive[17] = GetInstance<ConfigClient>().ToggleEmpressOfLightSoul.ToInt();
        SoulsUpgradesActive[18] = GetInstance<ConfigClient>().ToggleDukeFishronSoul.ToInt();
        SoulsUpgradesActive[19] = GetInstance<ConfigClient>().ToggleLunaticCultistSoul.ToInt();
        SoulsUpgradesActive[20] = GetInstance<ConfigClient>().ToggleMoonlordSoul .ToInt();
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
            Player.QuickSpawnItem(Player.GetSource_DropAsItem(), ItemID.ReaperHood);
            Player.QuickSpawnItem(Player.GetSource_DropAsItem(), ItemID.ReaperRobe);
            Player.QuickSpawnItem(Player.GetSource_DropAsItem(), ItemType<ReaperChalice>());
        }
    }
    public override void PreUpdateBuffs()
    {
        if (Reaper.ReaperMode) Player.AddBuff(BuffType<ReaperBuff>(), 1);
    }
    private float RespawnCalculator()
    {
        bool DesertAnhilatorActivated = (SoulsUpgradesActive[6] == 1 && SoulsUpgrades[6]) ? true : false;

        bool MoonlordRespawnActivated = (SoulsUpgradesActive[20] == 1 && SoulsUpgrades[20]) ? true : false;

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
        if (SoulsUpgrades[2] && GetInstance<ConfigClient>().ToggleCorruptSoul && Reaper.ReaperMode)
        {
            if (Main.rand.NextBool(6))
            {
                //FreeDodge(info);
                Player.BrainOfConfusionDodge();
                Player.SetImmuneTimeForAllTypes(120);
                Dust.QuickDust(Player.position, Color.Gray);
                return true;
            }
        }
        return base.FreeDodge(info);
    }
    //public override bool PreHurt(bool pvp, bool quiet, ref int damage, ref int hit.HitDirection, ref bool crit, ref bool customDamage, ref bool playSound, ref bool genGore, ref PlayerDeathReason damageSource, ref int cooldownCounter)
    //{
    //    if (SoulsUpgrades[2] && GetInstance<ConfigClient>().ToggleCorruptSoul)
    //    {
    //        if (Main.rand.NextBool(4))
    //        {
    //            Player.SetImmuneTimeForAllTypes(120);
    //          //  Player.shadowDodge = true;
    //            Player.BrainOfConfusionDodge();
    //            Dust.QuickDust(Player.position, Color.Gray);

    //            return false;
    //        }
    //        else
    //        {
    //            return base.PreHurt(pvp, quiet, ref damage, ref hit.HitDirection, ref crit, ref customDamage, ref playSound, ref genGore, ref damageSource, ref cooldownCounter);
    //        }
    //    }
    //    else
    //    {
    //        return base.PreHurt(pvp, quiet, ref damage, refhit.HitDirection, ref crit, ref customDamage, ref playSound, ref genGore, ref damageSource, ref cooldownCounter);
    //    }
    //}
    //public override void Hurt(bool pvp, bool quiet, double damage, inthit.HitDirection, bool crit, int cooldownCounter)
    //{
    //    if (SoulsUpgrades[2] && GetInstance<ConfigClient>().ToggleCorruptSoul)
    //    {
    //        if (Main.rand.NextBool(1))
    //        {
    //            Player.SetImmuneTimeForAllTypes(120);
    //            Player.shadowDodge= true;
    //            Player.BrainOfConfusionDodge();
    //            Dust.QuickDust(Player.position, Color.Gray);

    //            return;
    //        }
    //        else
    //        {
    //            base.Hurt(pvp, quiet, damage,hit.HitDirection, crit, cooldownCounter);
    //        }
    //    }

    //}
    public void ReaperSoulsBoost()
    {
        //UpdateSoulsList();
        LoadSoulsToggle();

        float OgPlayerSpeed = Player.moveSpeed;
        if (Reaper.ReaperMode && ChaliceOn)
        {
            if (SoulsUpgrades[0])
            {
                float speedForce = (SoulsUpgradesActive[0] / 100) + 1;
                Player.moveSpeed = ReaperSoulBoost ? OgPlayerSpeed + speedForce * 1.2f : OgPlayerSpeed + speedForce;
            }
            if (SoulsUpgrades[1] && SoulsUpgradesActive[1] == 1)
            {
                Player.statLifeMax2 += ReaperSoulBoost ? 20 : 10;
            }
            if (SoulsUpgrades[2] && SoulsUpgradesActive[2] == 1)
            {
                Player.lifeRegen += ReaperSoulBoost ? 10 : 5;
            }
            if (SoulsUpgrades[3] && SoulsUpgradesActive[3] == 1)
            {
                Player.honey = true;
                Player.strongBees = true;
                if (ReaperSoulBoost)
                {
                    Player.beeDamage(80);
                }
                else
                {
                    Player.beeDamage(20);
                }
            }
            if (SoulsUpgrades[4] && SoulsUpgradesActive[4] == 1)
            {
                Player.statDefense += ReaperSoulBoost ? 10 : 5;
            }
            if (SoulsUpgrades[7] && SoulsUpgradesActive[7] == 1)
            {
                Player.GetDamage(DamageClass.Generic) *= ReaperSoulBoost ? 1.20f : 1.10f;
            }
            if (SoulsUpgrades[8] && SoulsUpgradesActive[8] == 1) Player.GetModPlayer<RemnantPlayer>().FrostInmune();
            if (SoulsUpgrades[9] && SoulsUpgradesActive[9] == 1) Player.statLifeMax2 += ReaperSoulBoost ? 30 : 15;
            if (SoulsUpgrades[13] && SoulsUpgradesActive[13] == 1) Player.pickSpeed -= ReaperSoulBoost ? 0.7f : 0.35f;
            if (SoulsUpgrades[10] && SoulsUpgradesActive[10] == 1) Player.GetModPlayer<RemnantPlayer>().FirenInmune();
            if (SoulsUpgrades[12])
            {
                float JumpForce = SoulsUpgradesActive[12];
                Player.findTreasure = true;
                if (ReaperSoulBoost)
                {
                    Player.dangerSense = true;
                }
                Player.jumpSpeedBoost += ReaperSoulBoost ? JumpForce * 2 : JumpForce;
            }
            if (SoulsUpgrades[17] && SoulsUpgradesActive[17] == 1)
            {
                Player.empressBrooch = true;
                if (ReaperSoulBoost)
                {
                    Player.wingAccRunSpeed *= 2;
                }
            }

            if (SoulsUpgrades[15] && SoulsUpgradesActive[15] == 1)
            {
                Player.fireWalk = true;
                Player.lavaImmune = true;
                Player.GetDamage(DamageClass.Generic) *= ReaperSoulBoost ? 1.20f : 1.10f;
            }
            if (SoulsUpgrades[16] && SoulsUpgradesActive[16] == 1) Player.statDefense += ReaperSoulBoost ? 15 : 10;

            if (Reaper.ReaperMode && SoulsUpgrades[18] && SoulsUpgradesActive[18] == 1)
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
            if (SoulsUpgrades[19] && SoulsUpgradesActive[19] == 1)
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
            if (SoulsUpgrades[2] && SoulsUpgradesActive[2] == 1)
            {

                Player.brainOfConfusionItem = item;
            }
            if (SoulsUpgrades[14] && SoulsUpgradesActive[14] == 1)
            {
                Player.sporeSac = true;
                Player.SporeSac(item);
                Player.statLifeMax2 += ReaperSoulBoost ? 15 : 10;
            }
            if (SoulsUpgrades[5] && SoulsUpgradesActive[5] == 1)
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
            if (!SoulsUpgrades[0]) return false;
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