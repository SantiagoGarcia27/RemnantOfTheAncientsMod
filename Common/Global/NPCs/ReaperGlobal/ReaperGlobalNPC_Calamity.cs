using CalamityMod;
using CalamityMod.NPCs.BrimstoneElemental;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using RemnantOfTheAncientsMod.Common.ReaperSystem;
using RemnantOfTheAncientsMod.Common.UtilsTweaks;
using RemnantOfTheAncientsMod.Content.Buffs.Debuff;
using RemnantOfTheAncientsMod.Content.NPCs;
using RemnantOfTheAncientsMod.Content.NPCs.Bosses.Calamity;
using RemnantOfTheAncientsMod.Content.Projectiles.BossProjectiles.Calamity;
using RemnantOfTheAncientsMod.Content.Projectiles.Ranger;
using SangarUtilities.Common.UtilsTweaks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Common.Global.NPCs.ReaperGlobal
{
    public class ReaperGlobalNPC_Calamity : GlobalNPC
    {
        private List<int> desertScourge = [];
        private List<int> desertNuisance = [];
        private List<int> perforatorWormSamll = [];
        private List<int> perforatorWormMedium = [];
        private List<int> perforatorWormBig = [];
        private List<int> AstrumDeus = [];
        private List<int> StormWeaver = [];
        private List<int> DevourerOFGods = [];
        private List<int> PrimordialWyrm = [];
        private List<int> CalamityAffectedNPCs = [];
        private int attackCounter = 0;
        private bool UniqueToggledFlag = false;
        private bool SecondUniqueToggledFlag = false;

        public override void SetDefaults(NPC npc)
        {
            if (RemnantOfTheAncientsMod.CalamityMod == null) return;

            FillList();

            
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
            
        
            base.SetDefaults(npc);
        }

        public override void AI(NPC npc)
        {
            if (!Reaper.ReaperMode) return;

            ModNPC modNpc = npc.ModNPC;

            if (modNpc == null || RemnantOfTheAncientsMod.CalamityMod == null || modNpc.Mod != RemnantOfTheAncientsMod.CalamityMod) return;
     
            if (CalamityAffectedNPCs.Contains(npc.type)) CalamityAi(npc);

            if (attackCounter++ >= 800) attackCounter = 0;
           
            base.AI(npc);
        }

        [JITWhenModsEnabled("CalamityMod")]
        public void CalamityAi(NPC npc)
        {
            Player target = Main.player[npc.target];

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
                        var p = Projectile.NewProjectile(Entity.GetSource_None(), npc.Center, velocity, CallUtils.TryGetProjectileFromMod(RemnantOfTheAncientsMod.CalamityMod, "MushBomb"), 10, 10, Main.myPlayer);
                        Main.projectile[p].timeLeft = 60;
                        Main.projectile[p].ignoreWater = true;
                    }
                    for (int i = 0; i < maxProj / 2; i++)
                    {
                        Vector2 velocity = (Main.player[npc.target].Center - npc.Center).RotatedBy((-360 / maxProj / 2) * i);
                        var p = Projectile.NewProjectile(Entity.GetSource_None(), npc.Center, velocity, CallUtils.TryGetProjectileFromMod(RemnantOfTheAncientsMod.CalamityMod, "MushBomb"), 10, 10, Main.myPlayer);
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
                        NPC.NewNPC(npc.GetSource_FromAI(), (int)npc.Center.X + Main.rand.Next(-100, 101), (int)npc.Center.Y, NPCID.ZombieMushroomHat);
                    }
                    UniqueToggledFlag = true;
                }
            }
            if (npc.type == perforatorWormMedium[1])
            {
                if (attackCounter++ >= 300)
                {
                    var p = Projectile.NewProjectile(Entity.GetSource_None(), npc.Center, new Vector2(5, 0).RotatedBy(npc.rotation), CallUtils.TryGetProjectileFromMod(RemnantOfTheAncientsMod.CalamityMod, "IchorBlob"), npc.damage, 10, target.whoAmI, 0f, target.Center.Y);
                    Main.projectile[p].hostile = true;
                    Main.projectile[p].friendly = false;
                    p = Projectile.NewProjectile(Entity.GetSource_None(), npc.Center, new Vector2(-5, 0).RotatedBy(npc.rotation), CallUtils.TryGetProjectileFromMod(RemnantOfTheAncientsMod.CalamityMod, "IchorBlob"), npc.damage, 10, target.whoAmI, 0f, target.Center.Y);
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
                    int n = NPC.NewNPC(npc.GetSource_FromAI(), (int)Main.player[npc.target].Center.X + Main.rand.Next(2, 11) * 10 * (int)Math.Pow(-1, Main.rand.Next(2)), (int)Main.player[npc.target].Center.Y + Main.rand.Next(2, 10) * 10 * (int)Math.Pow(-1, Main.rand.Next(2)), CallUtils.TryGetNpcFromMod(RemnantOfTheAncientsMod.CalamityMod, "Cryogen"));
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

                                if (electricShieldLife-- <= 0) HasElectricShield = false;
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

                if (Main.netMode == NetmodeID.MultiplayerClient) return;

                foreach (Player player in Main.ActivePlayers)
                {
                    if (npc.Center.Distance(player.position) > 100.ToCoordinatePosition()) continue;

                    if (player.dead) continue;

                    if (player.whoAmI == npc.target) player.AddBuff(ModContent.BuffType<FogOfVoid>(), 120);
                    else player.AddBuff(BuffID.Slow, 120);
                }
            }
            else if (npc.type == CallUtils.TryGetNpcFromMod(RemnantOfTheAncientsMod.CalamityMod, "StormWeaverHead"))
            {
                if (Main.GameUpdateCount % 3 == 0)
                {
                    int projId = CallUtils.TryGetProjectileFromMod(RemnantOfTheAncientsMod.CalamityMod, "EssenceFire");
                    float x = Math.Sign(npc.velocity.X) * Math.Min(5, Math.Abs(npc.velocity.X));
                    float y = Math.Sign(npc.velocity.Y) * Math.Min(5, Math.Abs(npc.velocity.Y));
                    Vector2 velocity = new(x, y);
                    var a = Projectile.NewProjectile(npc.GetSource_FromAI(), npc.Center, velocity.RotatedByRandom(0.4f), projId, 50, 0, target.whoAmI);
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
                            NPC.NewNPC(Entity.GetSource_None(), (int)npc.position.X + Main.rand.Next(-30, 31), (int)npc.position.Y + Main.rand.Next(-100, 101), CallUtils.TryGetNpcFromMod(RemnantOfTheAncientsMod.CalamityMod, "PolterPhantom"));
                    }
                }
                else
                {
                    if (npc.life <= npc.lifeMax / 2)
                    {
                        UniqueToggledFlag = true;
                        NPC.NewNPC(Entity.GetSource_None(), (int)npc.position.X + Main.rand.Next(-30, 31), (int)npc.position.Y + Main.rand.Next(-100, 101), CallUtils.TryGetNpcFromMod(RemnantOfTheAncientsMod.CalamityMod, "PolterPhantom"));
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
                        int n = NPC.NewNPC(npc.GetSource_FromAI(), (int)npc.position.X, (int)npc.position.Y + (Main.rand.Next(-4, 5).ToCoordinatePosition()), ModContent.NPCType<MiniDuke>());
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
            else if (npc.type == CallUtils.TryGetNpcFromMod(RemnantOfTheAncientsMod.CalamityMod, "AresBody"))
            {
                if (npc.alpha >= 255) return;

                electricShieldLifeMax = 50;

                if (!HasElectricShield)
                {
                    if (attackCounter++ < Utils1.FormatTimeToTick(0, 0, 0, 2)) return;

                    HasElectricShield = true;
                    electricShieldLife = electricShieldLifeMax;
                    attackCounter = 0;
                }
                else
                {
                    foreach (Projectile proj in Main.ActiveProjectiles)
                    {
                        if (proj.hostile || !proj.friendly || proj.minion) continue;

                        float distance = npc.Distance(proj.Center);
                        if (distance < 194f)
                        {
                            proj.friendly = false;
                            proj.hostile = true;
                            proj.velocity *= -1;

                            if (electricShieldLife-- <= 0)
                                HasElectricShield = false;
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
                        for (int i = 0; i < 5; i++)
                        {
                            var n = NPC.NewNPC(npc.GetSource_FromAI(), (int)npc.Center.X + Main.rand.Next(-500, 501), (int)npc.Center.Y + Main.rand.Next(-500, 501), NPCID.Retinazer, npc.whoAmI);
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
                if (attackCounter >= Utils1.FormatTimeToTick(0, 0, 0, 1))
                {
                    foreach (var proj in Main.projectile)
                    {
                        if (proj.active)
                        {
                            if (npc.Center.Distance(proj.Center) < 100 && proj.friendly && !proj.hostile)
                            {
                                Vector2 fixedVelocity = proj.velocity;
                                if (proj.velocity.X > 20)
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

        public override bool PreAI(NPC npc)
        {
            if (!Reaper.ReaperMode || RemnantOfTheAncientsMod.CalamityMod == null) return base.PreAI(npc);
            PreAiCalamity(npc);

            return base.PreAI(npc);
        }
        float ang = 0;
        [JITWhenModsEnabled("CalamityMod")]
        bool PreAiCalamity(NPC npc)
        {
            Player player = Main.player[npc.FindClosestPlayer()];

            if (npc.type == CallUtils.TryGetNpcFromMod(RemnantOfTheAncientsMod.CalamityMod, "DankCreeper"))
            {
                ReaperGlobalNPC_Utilities.GetNPCList(CallUtils.TryGetNpcFromMod(RemnantOfTheAncientsMod.CalamityMod, "DankCreeper"), out List<NPC> CreepersList);
                ReaperGlobalNPC_Utilities.GetPositionsAround(player, 25 * 15f, CreepersList.Count, ang, out List<Vector2> Postions);
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

        public override void PostDraw(NPC npc, SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            if (RemnantOfTheAncientsMod.CalamityMod == null) return;
            PostDrawCalamity(npc, spriteBatch, screenPos, drawColor);
        }

        int frameCounter = 0;
        int Timmer1 = 0;
        int Timmer2 = 0;
        bool HasElectricShield = false;
        int electricShieldLifeMax = 10;
        int electricShieldLife = 10;
        [JITWhenModsEnabled("CalamityMod")]
        void PostDrawCalamity(NPC npc, SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
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
                        AnimateEffects(npc, 1f, texture, color);
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
                        AnimateEffects(npc, 1f, texture, color);
                    }
                }
            }

        }
        void AnimateEffects(NPC npc, float rotation, Asset<Texture2D> sprite, Color drawColour)
        {
            Vector2 frames = new(3, 6);
            Vector2 size = new(sprite.Width(), sprite.Height());
            Vector2 origin = new(size.X * 0.5f / frames.X, size.Y * 0.5f / frames.Y);

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
        }


        public override void OnSpawn(NPC npc, IEntitySource source)
        {
            ModNPC modNpc = npc.ModNPC;
            if (modNpc != null && RemnantOfTheAncientsMod.CalamityMod != null && modNpc.Mod == RemnantOfTheAncientsMod.CalamityMod)
            {
                OnSpawnCalamity(npc, source);
            }
            base.OnSpawn(npc, source);
        }

        [JITWhenModsEnabled("CalamityMod")]
        public void OnSpawnCalamity(NPC npc, IEntitySource source)
        {
            Player player = Main.player[npc.FindClosestPlayer()];
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
                    ReaperGlobalNPC_Utilities.GetPositionsAround(player.Center, 25 * 15f, ammount + 1, out List<Vector2> Postions);

                    var p = NPC.NewNPC(npc.GetSource_FromAI(), (int)Postions[i].X, (int)Postions[i].Y, CallUtils.TryGetNpcFromMod(RemnantOfTheAncientsMod.CalamityMod, "DankCreeper"));
                    NPC.NewNPC(npc.GetSource_FromAI(), (int)Postions[i].X, (int)Postions[i].Y, CallUtils.TryGetNpcFromMod(RemnantOfTheAncientsMod.CalamityMod, "DankCreeper"));
                    NPC.NewNPC(npc.GetSource_FromAI(), (int)Postions[i].X, (int)Postions[i].Y, CallUtils.TryGetNpcFromMod(RemnantOfTheAncientsMod.CalamityMod, "DankCreeper"));
                    Main.npc[p].aiStyle = -1;
                }
            }
            if (npc.type == CallUtils.TryGetNpcFromMod(RemnantOfTheAncientsMod.CalamityMod, "DankCreeper"))
            {
                ReaperGlobalNPC_Utilities.GetNPCList(CallUtils.TryGetNpcFromMod(RemnantOfTheAncientsMod.CalamityMod, "DankCreeper"), out List<NPC> CreepersList);
                ReaperGlobalNPC_Utilities.GetPositionsAround(player.Center, 25 * 15f, CreepersList.Count, out List<Vector2> Postions);

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


        public override bool CheckDead(NPC npc)
        {
            if (!Reaper.ReaperMode || RemnantOfTheAncientsMod.CalamityMod == null) return base.CheckDead(npc);

            int idCrimulan = CallUtils.TryGetNpcFromMod(RemnantOfTheAncientsMod.CalamityMod, "SplitCrimulanPaladin");
            if (npc.type == idCrimulan || npc.type == CallUtils.TryGetNpcFromMod(RemnantOfTheAncientsMod.CalamityMod, "SplitEbonianPaladin"))
            {
                var p = NPC.NewNPC(npc.GetSource_FromAI(), (int)npc.Center.X, (int)npc.Center.Y, NPCID.KingSlime);
                Main.npc[p].lifeMax = 500;
                Main.npc[p].life = 500;
                Main.npc[p].scale = 0.5f;
            }
            
            return base.CheckDead(npc);
        }

        public override bool PreKill(NPC npc)
        {
            if (RemnantOfTheAncientsMod.CalamityMod == null) return base.PreKill(npc);
            {
                if (npc.type == CallUtils.TryGetNpcFromMod(RemnantOfTheAncientsMod.CalamityMod, "Cryogen"))
                {
                    if (!npc.boss) return false;
                }
            }
            return base.PreKill(npc);
        }
    }
}
