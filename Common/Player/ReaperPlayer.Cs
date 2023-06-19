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

public class ReaperPlayer : ModPlayer
{
    public bool ChaliceOn;
    public static bool FWeapons;
    public static bool ReaperFirstTime;
    private static List<NPC> _hallucinationCandidates = new List<NPC>();
    public bool ReaperSoulBoost;
    public override void ResetEffects()
    {
        ReaperSoulBoost = false;
        ChaliceOn = false;
    }
    public override void OnEnterWorld(Player player)
    {
        FWeapons = true;
        ReaperGlobalItem.ReaperWingsNerf(player);
    }
    public override IEnumerable<Item> AddStartingItems(bool mediumCoreDeath)
    {
        if (mediumCoreDeath) return new[] { new Item(ItemType<Ftoggler>()) };
        return new[] { new Item(ItemType<Ftoggler>()), };
    }
    public override void UpdateEquips()
    {
        if (Player.wingTimeMax > 4) ReaperGlobalItem.ReaperWingsNerf(Player);
        //if(RemnantOfTheAncientsMod.DebuggMode) Debugg();
    }
    public override void Kill(double damage, int hitDirection, bool pvp, PlayerDeathReason damageSource)
    {
        Player.respawnTimer *= (int)RespawnCalculator();
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
        bool DesertAnhilatorActivated = (GetInstance<ConfigClient>().ToggleDesertAnhilatorSoul && Player.GetModPlayer<DesertReaperSoulPlayer>().DesertReaperUpgrade) ? true : false;

        bool MoonlordRespawnActivated = (GetInstance<ConfigClient>().ToggleMoonlordSoul && Player.GetModPlayer<MoonReaperSoulPlayer>().MoonReaperUpgrade) ? true : false;

        if (Reaper.ReaperMode)
        {
            if (DesertAnhilatorActivated && !MoonlordRespawnActivated) return ReaperSoulBoost ? 0.7f : 0.8f;
            else if (!DesertAnhilatorActivated && MoonlordRespawnActivated) return ReaperSoulBoost ? 0.5f : 0.7f;
            else if (DesertAnhilatorActivated && MoonlordRespawnActivated) return ReaperSoulBoost ? 0.3f : 0.5f;
        }
        return 1;
    }
    public void ReaperSoulsBoost()
    {
        float OgPlayerSpeed = Player.moveSpeed;
        if (Reaper.ReaperMode && ChaliceOn)
        {
            if (Player.GetModPlayer<SlimeReaperSoulPlayer>().SlimeReaperUpgrade)
            {

                float speedForce = (GetInstance<ConfigClient>().ToggleSkeletronPrimeSoul / 100) + 1;
                Player.moveSpeed = ReaperSoulBoost ? OgPlayerSpeed + speedForce * 1.2f : OgPlayerSpeed + speedForce;
            }
            if (Player.GetModPlayer<EyeReaperSoulPlayer>().EyeReaperUpgrade && GetInstance<ConfigClient>().ToggleEyeOfChutuluSoul)
            {
                Player.statLifeMax2 += ReaperSoulBoost ? 20 : 10;
            }
            if (Player.GetModPlayer<CorruptReaperSoulPlayer>().CorruptReaperUpgrade && GetInstance<ConfigClient>().ToggleCorruptSoul)
            {
                Player.lifeRegen += ReaperSoulBoost ? 10 : 5;
            }
            if (Player.GetModPlayer<BeeReaperSoulPlayer>().BeeReaperUpgrade && GetInstance<ConfigClient>().ToggleQueenBeeSoul)
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
            if (Player.GetModPlayer<SkeletonReaperSoulPlayer>().SkeletonReaperUpgrade && GetInstance<ConfigClient>().ToggleSkeletronSoul)
            {
                Player.statDefense += ReaperSoulBoost ? 10 : 5;
            }
            if (Player.GetModPlayer<FleshReaperSoulPlayer>().FleshReaperUpgrade && GetInstance<ConfigClient>().ToggleWallOfFleshSoul)
            {
                Player.GetDamage(DamageClass.Generic) *= ReaperSoulBoost ? 1.20f : 1.10f;
            }
            if (Player.GetModPlayer<FrozenReaperSoulPlayer>().FrozenReaperUpgrade && GetInstance<ConfigClient>().ToggleFrozenAssaulterSoul) Player.GetModPlayer<RemnantPlayer>().FrostInmune();
            if (Player.GetModPlayer<QueenReaperSoulPlayer>().QueenReaperUpgrade && GetInstance<ConfigClient>().ToggleQueenSlimeSoul) Player.statLifeMax2 += ReaperSoulBoost ? 30 : 15;
            if (Player.GetModPlayer<DestroyerReaperSoulPlayer>().DestroyerReaperUpgrade && GetInstance<ConfigClient>().ToggleDestroyerSoul) Player.pickSpeed -= ReaperSoulBoost ? 0.7f : 0.35f;
            if (Player.GetModPlayer<SpazmatismReaperSoulPlayer>().SpazmatismReaperUpgrade) Player.GetModPlayer<RemnantPlayer>().FirenInmune();
            if (Player.GetModPlayer<SkeletronPrimeReaperSoulPlayer>().SkeletronPrimeReaperUpgrade)
            {
                float JumpForce = GetInstance<ConfigClient>().ToggleSkeletronPrimeSoul;
                Player.findTreasure = true;
                if (ReaperSoulBoost)
                {
                    Player.dangerSense = true;
                }
                Player.jumpSpeedBoost += ReaperSoulBoost ? JumpForce * 2 : JumpForce;
            }
            if (Player.GetModPlayer<EmpressReaperSoulPlayer>().EmpressReaperUpgrade && GetInstance<ConfigClient>().ToggleEmpressOfLightSoul)
            {
                Player.empressBrooch = true;
                if (ReaperSoulBoost)
                {
                    Player.wingAccRunSpeed *= 2;
                }
            }

            if (Player.GetModPlayer<InfernalReaperSoulPlayer>().InfernalReaperUpgrade && GetInstance<ConfigClient>().ToggleInfernalTyrantSoul)
            {
                Player.fireWalk = true;
                Player.lavaImmune = true;
                Player.GetDamage(DamageClass.Generic) *= ReaperSoulBoost ? 1.20f : 1.10f;
            }
            if (Player.GetModPlayer<GolemReaperSoulPlayer>().GolemReaperUpgrade && GetInstance<ConfigClient>().ToggleGolemSoul) Player.statDefense += ReaperSoulBoost ? 15 : 10;

            if (Reaper.ReaperMode && Player.GetModPlayer<DukeReaperSoulPlayer>().DukeReaperUpgrade && GetInstance<ConfigClient>().ToggleDukeFishronSoul)
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
            if (Player.GetModPlayer<CultistReaperSoulPlayer>().CultistReaperUpgrade && GetInstance<ConfigClient>().ToggleLunaticCultistSoul)
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
            if (Player.GetModPlayer<PlantReaperSoulPlayer>().PlantReaperUpgrade && GetInstance<ConfigClient>().TogglePlanteraSoul)
            {
                Player.sporeSac = true;
                Player.SporeSac(item);
                Player.statLifeMax2 += ReaperSoulBoost ? 15 : 10;
            }
            if (Player.GetModPlayer<DeerclopsReaperSoulPlayer>().DeerclopsReaperUpgrade && GetInstance<ConfigClient>().ToggleDearclopsSoul)
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
        if (Player.GetModPlayer<SlimeReaperSoulPlayer>().SlimeReaperUpgrade &&
            Player.GetModPlayer<EyeReaperSoulPlayer>().EyeReaperUpgrade &&
            Player.GetModPlayer<CorruptReaperSoulPlayer>().CorruptReaperUpgrade &&
            Player.GetModPlayer<BeeReaperSoulPlayer>().BeeReaperUpgrade &&
            Player.GetModPlayer<SkeletonReaperSoulPlayer>().SkeletonReaperUpgrade &&
            Player.GetModPlayer<DeerclopsReaperSoulPlayer>().DeerclopsReaperUpgrade &&
            Player.GetModPlayer<DesertReaperSoulPlayer>().DesertReaperUpgrade &&
            Player.GetModPlayer<FleshReaperSoulPlayer>().FleshReaperUpgrade &&
            Player.GetModPlayer<FrozenReaperSoulPlayer>().FrozenReaperUpgrade &&
            Player.GetModPlayer<QueenReaperSoulPlayer>().QueenReaperUpgrade &&
            Player.GetModPlayer<SpazmatismReaperSoulPlayer>().SpazmatismReaperUpgrade &&
            Player.GetModPlayer<RetinazorReaperSoulPlayer>().RetinazorReaperUpgrade &&
            Player.GetModPlayer<DesertReaperSoulPlayer>().DesertReaperUpgrade &&
            Player.GetModPlayer<SkeletronPrimeReaperSoulPlayer>().SkeletronPrimeReaperUpgrade &&
            Player.GetModPlayer<PlantReaperSoulPlayer>().PlantReaperUpgrade &&
            Player.GetModPlayer<EmpressReaperSoulPlayer>().EmpressReaperUpgrade &&
            Player.GetModPlayer<GolemReaperSoulPlayer>().GolemReaperUpgrade &&
            Player.GetModPlayer<InfernalReaperSoulPlayer>().InfernalReaperUpgrade &&
            Player.GetModPlayer<DukeReaperSoulPlayer>().DukeReaperUpgrade &&
            Player.GetModPlayer<CultistReaperSoulPlayer>().CultistReaperUpgrade &&
            Player.GetModPlayer<MoonReaperSoulPlayer>().MoonReaperUpgrade)
        {
            return true;
        }
        bool a = Player.GetModPlayer<BeeReaperSoulPlayer>().BeeReaperUpgrade;

        return false;
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