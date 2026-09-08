using Microsoft.Xna.Framework;
using PlayerProxyLib.Common;
using RemnantOfTheAncientsMod.Common.Extensions;
using RemnantOfTheAncientsMod.Common.Global.NPCs;
using RemnantOfTheAncientsMod.Common.UtilsTweaks;
using RemnantOfTheAncientsMod.Content.NPCs;
using RemnantOfTheAncientsMod.Content.Projectiles.BossProjectile.Desert;
using RemnantOfTheAncientsMod.Content.Projectiles.BossProjectiles.Desert;
using SangarUtilities.Common.UtilsTweaks;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using static DesertAnnihilator_Animation;

public class ShootData
{
    public int type { get; set; }
    public ProjectileType projectileType { get; set; }
    public enum ProjectileType
    {
        Projectile = 0,
        NPC = 1,
    }
}

class DesertAnnihilator_Attack
{
    public NPC npc {  get; set; }
    public int shootTimeline = -1;
    int shootTimelineMax = -1;

    private List<int> lightSpawn =
    [
        NPCID.Antlion,
        NPCID.FlyingAntlion,
        NPCID.WalkingAntlion,
        NPCID.TombCrawlerHead,
    ];

    private List<int> heavySpawn =
    [
        NPCID.Antlion,
        NPCID.GiantFlyingAntlion,
        NPCID.GiantWalkingAntlion,
        NPCID.DuneSplicerHead,
    ];

    int tornadoInterval = Utils1.FormatTimeToTick(Second: 8);
    int markDelay
    {
        get
        {
            if (DificultyUtils.EternityMode || DificultyUtils.MasochistMode) return 1;
            if (DificultyUtils.InfernumMode || DificultyUtils.LegendaryMode) return 1;
            if (DificultyUtils.ReaperMode || DificultyUtils.Revengeance || DificultyUtils.Death) return 2;
            return 3;
        }
    }

    public DesertAnnihilator_Aux auxiliaryModule { get; set; }
    public DesertAnnihilator_Animation animationModule { get; set; }

    public DesertAnnihilator_Attack(NPC npc, DesertAnnihilator_Aux aux, DesertAnnihilator_Animation animation)
    {
        this.npc = npc;
        auxiliaryModule = aux;
        animationModule = animation;
    }

    public void AttackIA(NPC currentNpc, Player target)
    {
        TornadoAI();

        if (Main.expertMode) ShootAI(target);
    }
    public void DesertTp()
    {
        npc.alpha = 0;

        int tpDirection = Main.rand.NextBool() ? -1 : 1;

        Vector2 tileDistance = new(30f, -5f);
        Vector2 tpDistance = new Vector2(tileDistance.X, tileDistance.Y).ToCoordenatePosition();

        npc.Center = DesertAnnihilator_Aux.GetSecurePosition(Main.player[npc.target].Center + new Vector2(tpDirection * tpDistance.X, tpDistance.Y));
        npc.netUpdate = true;
        GenerateTpParticles(appear: true);
    }
    public void ShootAI(Player target)
    {
        shootTimelineMax = GetMaxShootTimeline();
        if (shootTimeline++ >= shootTimelineMax) shootTimeline = 0;

        ShootData data = new()
        {
            type = DificultyUtils.ReaperMode ? ModContent.NPCType<DesertTyphoonParry>() : ModContent.ProjectileType<DesertTyphoon>(),
            projectileType = DificultyUtils.ReaperMode ? ShootData.ProjectileType.NPC : ShootData.ProjectileType.Projectile
        };

        ShootManager(timePercent: 10, () => ShootHelper((int)(20 * RemnantGlobalNPC.DamageBonus), data, target, 12f, rotationGrades: Main.rand.Next(-20, 20)));

        ShootManager(timePercent: 50, () =>
        {
            for (int i = -1; i <= 1; i++)
                ShootHelper((int)(20 * RemnantGlobalNPC.DamageBonus), data, target, 12f, rotationGrades: i * 60 + Main.rand.Next(-20, 20));
        });

        if (auxiliaryModule.BossIsInRage)
        {
            ShootManager(timePercent: 80, () =>
            {
                for (int i = -2; i <= 2; i++)
                    ShootHelper((int)(20 * RemnantGlobalNPC.DamageBonus), data, target, 12f, rotationGrades: i * 60 + Main.rand.Next(-30, 30));
            });
            ShootManager(timePercent: 85, () =>
            {
                for (int i = -2; i <= 2; i++)
                    ShootHelper((int)(20 * RemnantGlobalNPC.DamageBonus), data, target, 12f, rotationGrades: i * 60 + Main.rand.Next(-30, 30));
            });
            ShootManager(timePercent: 90, () =>
            {
                for (int i = -2; i <= 2; i++)
                    ShootHelper((int)(20 * RemnantGlobalNPC.DamageBonus), data, target, 12f, rotationGrades: i * 60 + Main.rand.Next(-30, 30));
            });
        }

        if (shootTimeline == MathUtils.GetValueFromPorcentage(shootTimelineMax, 40)) DesertTp();

        if (shootTimeline.Between(
            (int)MathUtils.GetValueFromPorcentage(shootTimelineMax, 39) - 60,
            (int)MathUtils.GetValueFromPorcentage(shootTimelineMax, 40) - 1)) 
            GenerateTpParticles();
    }
    private void ShootManager(int timePercent, Action shootMethod)
    {
        float timeMark = MathUtils.GetValueFromPorcentage(shootTimelineMax, timePercent);
        float timeMarkTelegraph = timeMark - Utils1.FormatTimeToTick(Second: 1);
        if (shootTimeline == timeMarkTelegraph) animationModule.CurrentTexture = TextureType.Shoot;
        if (shootTimeline == timeMark)
        {
            shootMethod();
            animationModule.CurrentTexture = TextureType.Default;
        }
    }
    public void ShootHelper(int dammage, ShootData shoot, Player player, float Speed, float rotationGrades = 0f)
    {
        Vector2 direction = player.Center - npc.Center;
        direction.Normalize();
        direction = direction.RotatedBy(MathHelper.ToRadians(rotationGrades));
        direction *= Speed;

        if (shoot.projectileType == ShootData.ProjectileType.Projectile)
        {
            Projectile.NewProjectile(npc.GetSource_FromAI(), npc.Center, direction, shoot.type, dammage, 0f, Main.myPlayer);
        }
        else if (shoot.projectileType == ShootData.ProjectileType.NPC)
        {
            var n = NPC.NewNPC(npc.GetSource_FromAI(), (int)npc.Center.X, (int)npc.Center.Y, shoot.type);
            Main.npc[n].lifeMax = 10;
            Main.npc[n].damage = dammage;
            Main.npc[n].velocity = direction;
        }
    }


    int markDelayTicks => Utils1.FormatTimeToTick(Second: markDelay);
    int tornadoCounter = Utils1.FormatTimeToTick(Second: 8);
    int spawnIndex = 0;
    List<(Vector2 Position, int Timer)> pendingTornados = [];

    public void TornadoAI()
    {
        tornadoCounter--;

        if (tornadoCounter <= 0)
        {
            tornadoCounter = tornadoInterval;

            int numberOfTornados = DificultyUtils.ReaperMode ? 3 : 1;

            for (int i = 0; i < numberOfTornados; i++)
            {
                float randomX = Main.rand.NextBool()
                    ? Main.rand.Next(-50, -20).ToCoordinatePosition()
                    : Main.rand.Next(20, 50).ToCoordinatePosition();

                float randomY = npc.Bottom.Y + Main.rand.Next(-3, 3).ToCoordinatePosition();

                Vector2 spawnPosition = new(auxiliaryModule.CurrentTarget.Center.X + randomX, randomY);

                Projectile.NewProjectile(Projectile.GetSource_None(), spawnPosition, Vector2.Zero, ModContent.ProjectileType<SandnadoMarkClone>(), 0, 0, Main.myPlayer, ai0: markDelay);

                pendingTornados.Add((spawnPosition, markDelayTicks));
            }

            foreach (Player player in Main.player)
            {
                if (!player.active || player.dead || player.ghost || player.IsProxyPlayer()) continue;
                Vector2 pos = player.position;
                Projectile.NewProjectile(Projectile.GetSource_None(), pos, Vector2.Zero, ModContent.ProjectileType<SandnadoMarkClone>(), 0, 0, Main.myPlayer, ai0: markDelay);

                pendingTornados.Add((pos, markDelayTicks));
            }
        }
        else if (tornadoCounter < Utils1.FormatTimeToTick(Second: 1) && tornadoCounter > 0) animationModule.CurrentTexture = TextureType.Shoot;

        // Actualizar cada tornado pendiente
        for (int i = pendingTornados.Count - 1; i >= 0; i--)
        {
            var tornado = pendingTornados[i];
            tornado.Timer--;

            int enemyId = DificultyUtils.ReaperMode || Main.masterMode ? heavySpawn[spawnIndex] : lightSpawn[spawnIndex];
            if (tornado.Timer <= 0)
            {
                int index = NPC.NewNPC(npc.GetSource_FromAI(), (int)tornado.Position.X, (int)tornado.Position.Y - 16, enemyId);
                int vidaMax = (int)(Main.npc[index].lifeMax * 0.75f);
                Main.npc[index].lifeMax = vidaMax;
                Main.npc[index].life = vidaMax;

                pendingTornados.RemoveAt(i);
                animationModule.CurrentTexture = TextureType.Default;
            }
            else
            {
                pendingTornados[i] = tornado;
            }
        }
        if (spawnIndex++ >= lightSpawn.Count - 1) spawnIndex = 0;
    }
    private int GetMaxShootTimeline()
    {
        int maxTimeline = Utils1.FormatTimeToTick(Second: 10);
        if (DificultyUtils.ReaperMode) maxTimeline -= Utils1.FormatTimeToTick(0, 0, 0, 2);
        if (auxiliaryModule.BossIsInRage || DificultyUtils.InfernumMode) maxTimeline -= Utils1.FormatTimeToTick(0, 0, 0, 4);
        return maxTimeline;
    }

    [JITWhenModsEnabled("FargowiltasSouls")]
    public void EternityIA(NPC currentNpc, Player player)
    {
        if(npc == null) npc = currentNpc;

        if (!DificultyUtils.MasochistMode && !DificultyUtils.EternityMode) return;

        if (shootTimeline == MathUtils.GetValueFromPorcentage(shootTimelineMax, 20))
        {
            npc.netUpdate = true;

            if (Main.netMode != NetmodeID.MultiplayerClient)
                Projectile.NewProjectile(npc.GetSource_FromThis(), npc.Center, Vector2.Zero, CallUtils.TryGetProjectileFromMod(RemnantOfTheAncientsMod.RemnantOfTheAncientsMod.FargosSoulMod, "GlowRing"), 0, 0f, Main.myPlayer, npc.whoAmI, -19);

            if (npc.HasValidTarget)
                SoundEngine.PlaySound(in SoundID.ForceRoarPitched, Main.player[npc.target].Center);

        }
        if (shootTimeline == MathUtils.GetValueFromPorcentage(shootTimelineMax, 20) - 10)
        {
            float ofset = 100f.ToCoordinatePosition();
            float ofsetY = player.position.Y - ofset;

            Vector2 start = new(player.position.X - ofset, ofsetY);
            Vector2 end = new(player.position.X + ofset, ofsetY);

            int numberOfProjectiles = DificultyUtils.EternityMode ? 40 : 80;

            List<Vector2> points = GeneratePoints(start, end, numberOfProjectiles);

            int damage = 100;
            int proj = RemnantOfTheAncientsMod.RemnantOfTheAncientsMod.ParticleMeterChoice() ? ProjectileID.RollingCactus : ModContent.ProjectileType<CactusBoulderClone>();

            foreach (var point in points)
            {
                var p = Projectile.NewProjectile(npc.GetSource_FromAI(), new Vector2(point.X, point.Y + (100f * 16)), Vector2.Zero, CallUtils.TryGetProjectileFromMod(RemnantOfTheAncientsMod.RemnantOfTheAncientsMod.FargosSoulMod, "WOFReticle"), 0, 0f, Main.myPlayer);
                Main.projectile[p].scale = 0.5f;
                Projectile.NewProjectile(npc.GetSource_FromAI(), point, Vector2.Zero, proj, damage, 0f, Main.myPlayer);
            }
            static List<Vector2> GeneratePoints(Vector2 start, Vector2 end, int pointCount)
            {
                List<Vector2> points = [];

                for (int i = 0; i < pointCount; i++)
                {
                    float t = i / (float)(pointCount - 1);
                    Vector2 point = Vector2.Lerp(start, end, t);
                    points.Add(point);
                }

                return points;
            }
        }
    }
    public void GenerateTpParticles(bool appear = false)
    {
        if(npc == null) return;
        int particleCount = RemnantOfTheAncientsMod.RemnantOfTheAncientsMod.ParticleMeter(45);//25

        int width = (int)(npc.width * npc.scale);
        int height = (int)(npc.height * npc.scale);

        if (!appear) npc.alpha = 150;

        for (int i = 0; i < particleCount; i++)
        {
            Vector2 dustPosition = npc.position - new Vector2(Main.rand.Next(width / 2), Main.rand.Next(height / 2));
            int index = Dust.NewDust(dustPosition, width, height, DustID.Sand, 0, 0, 100, default, 3f);
            Dust dust = Main.dust[index];
            dust.velocity = npc.velocity * 0.2f;
            dust.noGravity = true;
        }

    }
}