using Microsoft.Xna.Framework;
using RemnantOfTheAncientsMod;
using RemnantOfTheAncientsMod.Common.Global.NPCs;
using RemnantOfTheAncientsMod.Common.UtilsTweaks;
using RemnantOfTheAncientsMod.Content.NPCs.Bosses.ITyrant;
using RemnantOfTheAncientsMod.Content.Projectiles.BossProjectile;
using SangarUtilities.Common.UtilsTweaks;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

class InfernalTyrant_AttackModule
{
    internal int shootTimeline = -1;
    internal int shootTimelineMax = -1;

    internal NPC npc => auxModule.npc;

    internal InfernalTyrant_AnimationModule animationModule { get; set; }
    internal InfernalTyrant_AuxiliaryModule auxModule { get; set; }
    
    public InfernalTyrant_AttackModule(InfernalTyrant_AnimationModule animationModule, InfernalTyrant_AuxiliaryModule auxiliaryModule)
    {
        this.animationModule = animationModule;
        this.auxModule = auxiliaryModule;
    }

    private void ShootManager(int timePercent, Action shootMethod)
    {
        float timeMark = MathUtils.GetValueFromPorcentage(shootTimelineMax, timePercent);
        float timeMarkTelegraph = timeMark - Utils1.FormatTimeToTick(Second: 1);
        if (shootTimeline == timeMarkTelegraph) animationModule.ShootTelegraphing = true;
        if (shootTimeline == timeMark)
        {
            shootMethod();
            animationModule.ShootTelegraphing = false;
        }
    }

    public void DoAttacks(Player target)
    {
        int attackCounter = auxModule.attackCounter;
        switch (attackCounter)
        {
            case 200:
                SpikeIa((int)(40 * RemnantGlobalNPC.DamageBonus), false, -3, -3);
                break;
            case 250:
                FireBallIa(12f, (int)(70 * RemnantGlobalNPC.DamageBonus), ModContent.ProjectileType<InfernalBallF>(), "*", 4, 3, target, 0f);
                FireBallIa(12f, (int)(70 * RemnantGlobalNPC.DamageBonus), ModContent.ProjectileType<InfernalBallF>(), "/", 4, 3, target, 0f);
                break;
            case 300:
                SummonIa(NPCID.Demon);
                break;
            case 400:
                SpikeIa((int)(90 * RemnantGlobalNPC.DamageBonus), true, 3, -3);
                break;
            case 430:
                SpikeIa((int)(40 * RemnantGlobalNPC.DamageBonus), false, 3, -3);
                break;
            case 600:
                for (int i = -2; i <= 3; i++)
                    FireBallIa(12f, (int)(30 * RemnantGlobalNPC.DamageBonus), ModContent.ProjectileType<InfernalBall>(), "*", i - 1, i + 2, target, 0);
                break;
            case 700:
                if (RemnantOfTheAncientsMod.RemnantOfTheAncientsMod.InfernumMod != null)
                {
                    if (DificultyUtils.InfernumMode)
                    {
                        for (int i = 0; i <= 10; i++)
                        {
                            TornadoIa((int)(70 * RemnantGlobalNPC.DamageBonus), CallUtils.TryGetProjectileFromMod(RemnantOfTheAncientsMod.RemnantOfTheAncientsMod.CalamityMod, "Flarenado"), target, i);
                        }
                    }
                }
                SummonIa(NPCID.RedDevil);
                break;
        }

        if (auxModule.IsPhase2)
        {
            switch (attackCounter)
            {
                case 550:
                    for (int i = -3; i <= 3; i++)
                        FireBallIa(12f, (int)(45 * RemnantGlobalNPC.DamageBonus), ModContent.ProjectileType<InfernalBallF>(), "*", 2, 2, target, i * 0.45f);
                    break;
                case 450:
                    SpikeIa((int)(50 * RemnantGlobalNPC.DamageBonus), true, -3, -3);
                    SpikeIa((int)(50 * RemnantGlobalNPC.DamageBonus), true, 3, 3);
                    break;
                case 320:
                    for (int i = -1; i <= 1; i++)
                        FireBallIa(12f, (int)(55 * RemnantGlobalNPC.DamageBonus), ModContent.ProjectileType<InfernalBall>(), "*", 3, 3, target, i * 0.15f);
                    break;
                case 180:
                    SpikeIa((int)(60 * RemnantGlobalNPC.DamageBonus), false, -2, 2);
                    SummonIa(NPCID.Demon);
                    break;
            }
        }

        if (auxModule.IsEnraged)
        {
            switch (attackCounter)
            {
                case 580:
                    for (int i = 0; i < 5; i++)
                        FireBallIa(12f, (int)(60 * RemnantGlobalNPC.DamageBonus), ModContent.ProjectileType<InfernalBallF>(), "*", 2, 2, target, i * 1.256f);
                    break;
                case 500:
                    SpikeIa((int)(60 * RemnantGlobalNPC.DamageBonus), true, -3, 3);
                    SpikeIa((int)(60 * RemnantGlobalNPC.DamageBonus), false, 3, 3);
                    break;
                case 350:
                    for (int i = -3; i <= 3; i++)
                        FireBallIa(12f, (int)(40 * RemnantGlobalNPC.DamageBonus), ModContent.ProjectileType<InfernalBall>(), "*", i, i + 1, target, i * 0.2f);
                    break;
                case 270:
                    SpikeIa((int)(80 * RemnantGlobalNPC.DamageBonus), true, -2, -2);
                    SpikeIa((int)(80 * RemnantGlobalNPC.DamageBonus), true, 2, -2);
                    SpikeIa((int)(80 * RemnantGlobalNPC.DamageBonus), true, 0, 3);
                    break;
                case 150:
                    SpikeIa((int)(70 * RemnantGlobalNPC.DamageBonus), true, -3, -3);
                    SpikeIa((int)(70 * RemnantGlobalNPC.DamageBonus), true, 3, -3);
                    break;
                case 50:
                    for (int i = -2; i <= 2; i++)
                        FireBallIa(12f, (int)(50 * RemnantGlobalNPC.DamageBonus), ModContent.ProjectileType<InfernalBallF>(), "*", 3, 2, target, i * 0.3f);
                    break;
            }
        }
    }

    public void FireBallIa(float Speed, int damage, int type, string signo1, int cordx, int cordy, Player player, float grades)
    {
        if (npc == null || !npc.active) return;

        Vector2 vector8 = new Vector2(npc.position.X + (npc.width * cordx), npc.position.Y + (npc.height * cordy));
        if (signo1 == "/")
            vector8 = new Vector2(npc.position.X + (npc.width / cordx), npc.position.Y + (npc.height / cordy));

        SoundEngine.PlaySound(SoundID.DD2_BetsyFireballShot, npc.Center);

        Vector2 direction = (player.Center - npc.Center).SafeNormalize(Vector2.UnitX);
        direction = direction.RotatedBy(grades);

        int projectile = Projectile.NewProjectile(npc.GetSource_FromAI(), vector8, direction * 12, type, damage, 0f, 0);
        Main.projectile[projectile].timeLeft = 300;
    }
    public void TornadoIa(int damage, int type, Player player, int i)
    {
        if(npc == null || !npc.active) return;

        Vector2 spawn = DistanceUtils.SearchLiquidCoordenates(LiquidID.Lava, 10, player);

        Vector2 fixedSpawn = new(spawn.X, DistanceUtils.CheckHigh(spawn));
        Vector2 ppos = player.position;
        Vector2 position = new(player.Center.X + (40 * 16f), fixedSpawn.Y - i * 16 * 4);

        SoundEngine.PlaySound(SoundID.DD2_BetsyFireballShot, npc.Center);
        int projectile = Projectile.NewProjectile(npc.GetSource_FromAI(), position, Vector2.Zero, type, damage, 0f, player.whoAmI, 1);
        Main.projectile[projectile].timeLeft = 1300;
    }
    public void SpikeIa(int damage, bool isStrong, int cordx, int cordy)
    {
        if (npc == null || !npc.active) return;

        int type = isStrong ? ModContent.ProjectileType<InfernalSpikeF>() : ModContent.ProjectileType<InfernalSpike>();
        SoundEngine.PlaySound(SoundID.DD2_BetsyFireballShot, npc.Center);
        Vector2 spikeDirection = Vector2.Normalize(new Vector2(cordx, cordy));
        int projectile = Projectile.NewProjectile(npc.GetSource_FromAI(), npc.Center, spikeDirection * 14f, type, damage, 0f, Main.myPlayer);
        Main.projectile[projectile].timeLeft = 1500;
    }

    public void SummonIa(int npcType)
    {
        if (npc == null || !npc.active) return;

        NPC.NewNPC(npc.GetSource_FromAI(), (int)npc.position.X, (int)npc.position.Y, npcType);
    }


}