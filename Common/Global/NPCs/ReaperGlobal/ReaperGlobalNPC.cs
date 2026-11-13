using Microsoft.Xna.Framework;
using RemnantOfTheAncientsMod.Common.ReaperSystem;
using RemnantOfTheAncientsMod.Common.RemPlayer;
using RemnantOfTheAncientsMod.Common.UtilsTweaks;
using RemnantOfTheAncientsMod.Common.World;
using RemnantOfTheAncientsMod.Content.NPCs;
using RemnantOfTheAncientsMod.Content.NPCs.Bosses.ITyrant;
using RemnantOfTheAncientsMod.Content.Projectiles.Multiclass;
using SangarUtilities.Common.UtilsTweaks;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Common.Global.NPCs.ReaperGlobal
{
    public class ReaperGlobalNpc : GlobalNPC
    {
        public override bool InstancePerEntity => true;

        public override void SetDefaults(NPC npc)
        {
            if (Reaper.ReaperMode && npc.boss)
            {
                ModifyBossStats(npc);
            }

            if (Reaper.ReaperMode)
            {
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
            }
        }

        
        public List<int> destroyer =
        [
            NPCID.TheDestroyer ,
            NPCID.TheDestroyerBody,
            NPCID.TheDestroyerTail
        ];

     
        public static void ModifyBossStats(NPC npc)
        {
            npc.lifeMax = (int)(npc.lifeMax * RemnantGlobalNPC.LifeBonus);
            npc.damage = (int)(npc.damage * RemnantGlobalNPC.DamageBonus);
        }
        int primePhase = 0;
        public override void OnSpawn(NPC npc, IEntitySource source)
        {
            if (Reaper.ReaperMode)
            {
                if(Main.netMode == NetmodeID.MultiplayerClient) return;

                if ((npc.type == NPCID.TheDestroyerBody || npc.type == NPCID.TheDestroyerTail) &&
                    source is EntitySource_Parent parentSource && parentSource.Entity is NPC head && head.type == NPCID.TheDestroyer)
                {
                    npc.lifeMax = head.lifeMax;
                    npc.life = head.life;
                    npc.netUpdate = true;
                }

                if (npc.boss)
                {
                    npc.life = npc.lifeMax;
                    npc.netUpdate = true;
                }

                
                if (npc.type == NPCID.SkeletronHead)
                {
                    for (int i = 0; i < 2; i++)
                    {
                        NPC.SpawnOnPlayer(Main.player[npc.target].whoAmI, NPCID.SkeletronHand);
                    }
                }
                if (npc.type == NPCID.Deerclops) RemnantPlayer.ApplyBuffToAllPlayers(BuffID.Hunger, 0, 1, 30);   
                
                if (npc.type == NPCID.Golem)
                {
                    NPC.NewNPC(npc.GetSource_FromAI(), (int)npc.position.X, (int)npc.position.Y, NPCID.GolemFistLeft);
                    NPC.NewNPC(npc.GetSource_FromAI(), (int)npc.position.X, (int)npc.position.Y, NPCID.GolemFistRight);
                }
                if (npc.type == NPCID.GolemFistLeft || npc.type == NPCID.GolemFistRight) npc.life = npc.lifeMax;
                
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
                if(destroyer.Contains(npc.type))
                {
                    npc.scale *= 2f;
                    npc.Size *= 2f;
                }
            }
            base.OnSpawn(npc, source);
        }

        int attackCounter = 0;
        private List<NPC> NpcList = [];       
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

            if (!Reaper.ReaperMode) return;
            
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
                            if (owner.type == NPCID.BrainofCthulhu) Owner = owner;
                        }
                    }

                    List<Vector2> Postions = ReaperGlobalNPC_Utilities.GetPositionsAround(Owner, 25 * 15f, NPC.CountNPCS(NPCID.Creeper));
                    ReaperGlobalNPC_Utilities.GetNPCList(NPCID.Creeper, out List<NPC> CreepersList);

                    for (int a = 0; a < NPC.CountNPCS(NPCID.Creeper); a++)
                    {
                        CreepersList[a].position = Postions[a];
                    }
                }
                if (npc.type == NPCID.QueenBee)
                {
                    if (attackCounter++ >= 600) attackCounter = 0;
                    if (attackCounter == 200) NPC.NewNPC(npc.GetSource_FromAI(), (int)npc.Center.X, (int)npc.Center.Y,NPCID.BigHornetFatty);
                        
                    Point NpcFloor = Utils.ToTileCoordinates(npc.Center);
                    if (Main.tile[NpcFloor.X, NpcFloor.Y + 1].LiquidAmount > 0 && Main.tile[NpcFloor.X, NpcFloor.Y + 1].LiquidType == LiquidID.Honey)
                    {
                        if (npc.life >= npc.lifeMax - 5) return;  
                        npc.life += 5;
                        npc.HealEffect(5);        
                    }
                }
                if (npc.type == NPCID.SkeletronHead)
                {
                    foreach(NPC npcAux in Main.ActiveNPCs)
                    {
                        if (npcAux.type != NPCID.SkeletronHand) continue; 
                        if (!npcAux.dontTakeDamage) npc.dontTakeDamage = NPC.AnyNPCs(NPCID.SkeletronHand);
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
                        RemnantPlayer.ApplyBuffToAllPlayers(BuffID.Hunger, 0, 0, Utils1.FormatTimeToTick(0, 0, 0, 30));
                        RemnantPlayer.ApplyBuffToAllPlayers(BuffID.Darkness, 0, 0, Utils1.FormatTimeToTick(0, 0, 0, 30));
                        RemnantPlayer.ApplyBuffToAllPlayers(BuffID.Blackout, 0, 0, Utils1.FormatTimeToTick(0, 0, 0, 30));
                        player.dontStarveShader = true;    
                    }
                }
                if (npc.type == NPCID.QueenSlimeBoss)
                {    
                    RemnantPlayer.ApplyBuffToAllPlayers(BuffID.WindPushed, 0, 0, Utils1.FormatTimeToTick(0, 0, 0, 1));
                    RemnantPlayer.ApplyBuffToAllPlayers(BuffID.VortexDebuff, 0, 0, Utils1.FormatTimeToTick(0, 0, 0, 1));
                    RemnantPlayer.ApplyBuffToAllPlayers(BuffID.QueenSlimeMount, 0, 0, Utils1.FormatTimeToTick(0, 0, 0, 1));
                }
                if (npc.type == NPCID.WallofFleshEye)
                {
                    if (attackCounter++ >= 100) attackCounter = 0;

                    if (attackCounter < 50 || attackCounter > 55) return;
                        
                    targetProj = SearchTargetWallOfFlesh(npc);

                    if (targetProj == null) return;
                        
                    GeneralDataStorage.InterceptionDroneTarget = targetProj;
                    if (!targetProj.active) return;
                        
                    Vector2 velocity = targetProj.position - npc.position;
                    updateRotation(npc, true);
                    var p = Projectile.NewProjectile(npc.GetSource_FromAI(), npc.position, velocity, ModContent.ProjectileType<InterceptionBream>(), 0, 0, Main.myPlayer);
                    Main.projectile[p].Size = new(60f, 60f);
                    Main.projectile[p].hostile = true;
                    Main.projectile[p].friendly = false;
                    Main.projectile[p].velocity *= 3;
                         
                }
                if (npc.type == NPCID.Spazmatism)
                {
                    if (npc.localAI[2] < 1f) return;

                    if (npc.ai[2] != 100f && npc.ai[2] != 50f && npc.ai[2] != 150f && npc.ai[2] != 0f && npc.ai[2] != 200f) return;
                        
                    for (int i = 0; i < 3; i++)
                    {
                        ShootIa(npc, npc.damage, ProjectileID.CursedFlameHostile, player, 10f + i, new(-0.5f + i, 0.5f + i), i * 10);
                    } 
                }
                if (npc.type == NPCID.Retinazer)
                {

                    if (!NPC.AnyNPCs(NPCID.Spazmatism)) return;
                        
                    if (attackCounter++ >= 200) attackCounter = 0;

                    if (attackCounter != 100) return;
                        
                    foreach(NPC npcAux in Main.ActiveNPCs)
                    {
                        if (npcAux.type != NPCID.Spazmatism) continue;
                                    
                        updateRotation(npc, false);
                        var p = ShootIa(npc, 10, ModContent.ProjectileType<HealthBream>(), npcAux, 10f);
                        Main.projectile[p].Size = new(60f, 60f);
                    }  
                         
                }

                bool[] IsInmune = new bool[4];
                
                if (npc.type == NPCID.SkeletronPrime)
                {
                    int handAmmount = 8;

                    if (NPC.AnyNPCs(NPCID.PrimeCannon) || NPC.AnyNPCs(NPCID.PrimeLaser) || NPC.AnyNPCs(NPCID.PrimeSaw) || NPC.AnyNPCs(NPCID.PrimeVice))
                    {
                        foreach(NPC npcAux in Main.ActiveNPCs)
                        {
                            for (int i = 0; i < SkeletronPrimeHands.Count; i++)
                            {
                                bool inmune = SkeletronPrimeHands.Contains(npcAux.type) && !npcAux.dontTakeDamage;
                                IsInmune[i] = inmune;
                            }
                        }

                        bool headInmune = !IsInmune[0] && !IsInmune[1] && !IsInmune[2] && !IsInmune[3];
                        npc.dontTakeDamage = headInmune;
                    }
                    else
                    {
                        npc.dontTakeDamage = false;
                    }

                    if (!npc.dontTakeDamage)
                    {
                        if (npc.life >= npc.lifeMax * 0.75f)
                        {
                            if (attackCounter++ >= 200) attackCounter = 0;    
                            if (attackCounter == 180)
                            {
                                for (int i = -2; i < 5; i++)
                                {
                                    Vector2 pos = (player.Center - npc.Center).RotatedBy(i * 0.2f);
                                    int proj = Projectile.NewProjectile(npc.GetSource_FromAI(), npc.Center, pos, ProjectileID.PinkLaser, 20, 1, Main.myPlayer);
                                    Main.projectile[proj].velocity /= 2;
                                }
                            }
                        }
                        else
                        {
                            if (primePhase <= 4) primePhase++;
                            if (primePhase < 5)
                            {
                                int handId = primePhase switch
                                {
                                    1 => NPCID.PrimeLaser,
                                    2 => NPCID.PrimeCannon,
                                    3 => NPCID.PrimeVice,
                                    4 => NPCID.PrimeSaw,
                                    _ => throw new NotImplementedException()
                                };

                                for (int i = -(handAmmount / 2); i < handAmmount / 2; i++)
                                {
                                    int a = NPC.NewNPC(npc.GetSource_FromAI(), (int)npc.Center.X, (int)npc.Center.Y, handId, Main.myPlayer);
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
                        foreach(NPC npcAux in Main.ActiveNPCs)
                        {
                            if (npcAux.type != NPCID.PlanterasHook) continue;
                            if (attackCounter == 100) 
                                npcAux.velocity = player.Center + new Vector2(Main.rand.Next(-6, 7).ToCoordinatePosition(), Main.rand.Next(-6, 7).ToCoordinatePosition()) - npc.Center;  
                        }
                    }
                    if (attackCounter++ >= 110) attackCounter = 0;
                }
                if (npc.type == NPCID.DukeFishron)
                {
                    if (attackCounter++ >= 400) 
                    { 
                        attackCounter = 0;
                        if (NPC.CountNPCS(ModContent.NPCType<MiniDuke>()) < 3)
                        {
                            int n = NPC.NewNPC(npc.GetSource_FromAI(), (int)npc.position.X, (int)npc.position.Y + (Main.rand.Next(-4, 5).ToCoordinatePosition()), ModContent.NPCType<MiniDuke>());
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
                        if (attackCounter == 100 || attackCounter == 110)
                        {
                            int type = attackCounter == 100 ? ProjectileID.CultistBossFireBall : ProjectileID.CultistBossFireBallClone;
                            float velocityMultiplier = attackCounter == 100 ? 0.5f : 0.25f;

                            for (int i = 0; i < 5; i++)
                            {
                                int p = Projectile.NewProjectile(npc.GetSource_FromAI(), npc.position + new Vector2(i * 50, 0), (Main.player[npc.target].position - npc.position).RotatedBy(i * 10), ProjectileID.CultistBossFireBall, 30, 0, 255);
                                Main.projectile[p].velocity *= velocityMultiplier;
                            }
                        }
                    }
                    if (attackCounter++ >= 200) attackCounter = 0;
                }
                if (npc.type == NPCID.MoonLordCore)
                {
                    RemnantPlayer.ApplyBuffToAllPlayers(BuffID.Horrified, 0, 0, Utils1.FormatTimeToTick(0, 0, 0, 1));
                    RemnantPlayer.ApplyBuffToAllPlayers(BuffID.Weak, 0, 0, Utils1.FormatTimeToTick(Second: 1));
                    RemnantPlayer.ApplyBuffToAllPlayers(BuffID.Blackout, 0, 0, Utils1.FormatTimeToTick(0, 0, 0, 1));
                    RemnantPlayer.ApplyBuffToAllPlayers(BuffID.ChaosState, 0, 0, Utils1.FormatTimeToTick(0, 0, 0, 1));
                    RemnantPlayer.ApplyBuffToAllPlayers(BuffID.NoBuilding, 0, 0, Utils1.FormatTimeToTick(0, 0, 0, 1));

                    if (NPC.AnyNPCs(NPCID.Dryad)) RemnantPlayer.ApplyBuffToAllPlayers(BuffID.DryadsWard, 0, 0, Utils1.FormatTimeToTick(0, 0, 0, 1));
                   
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

                if (attackCounter++ < 800) attackCounter = 0;
            }

            base.AI(npc);
        }
       

        public override bool PreKill(NPC npc)
        {
           
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
     
        

        public static NPC target;
        public static Projectile targetProj;
        private void updateRotation(NPC npc, bool isProjectile)
        {
            if (isProjectile)
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

        public float Range = 40f.ToCoordinatePosition();
        private NPC SearchForTeamTarget(NPC npc)
        {
            foreach(NPC npcAux in Main.ActiveNPCs)
            {
                if (npcAux.friendly != npc.friendly || npcAux == npc) continue;
                float distance = Vector2.Distance(npc.Center, npcAux.Center);
                if (distance < Range) return npcAux;
            }

            return null;
        }
        private Projectile SearchForProjectileTarget(NPC npc)
        {    
            foreach (Projectile proj in Main.ActiveProjectiles)
            {
                if (proj.friendly == npc.friendly) continue;
                float distance = Vector2.Distance(npc.Center, proj.Center);
                if (distance < Range) return proj;  
            }
            
            return null;
        }
        public static void ShootIa(NPC npc, int dammage, int type, Player player, float Speed, Vector2 position, float angle)
        {
            Vector2 NpcPosition = new(npc.position.X + (npc.width / 2), npc.position.Y + (npc.height / 2));
            float rotation = (float)Math.Atan2(NpcPosition.Y - (player.position.Y + (player.height * position.X)), NpcPosition.X - (player.position.X + (player.width * position.Y)));
            Vector2 direction;
            direction.X = (float)(Math.Cos(rotation) * Speed * -1);
            direction.Y = (float)(Math.Sin(rotation) * Speed * -1);
            direction = direction.RotatedBy(angle);
            Projectile.NewProjectile(npc.GetSource_FromAI(), NpcPosition, direction, type, dammage, 0f, player.whoAmI);
        }
        public static int ShootIa(NPC npc, int dammage, int type, NPC target, float Speed)
        {
            Vector2 NpcPosition = new(npc.position.X, npc.position.Y);
            float rotation = (float)Math.Atan2(NpcPosition.Y - (target.position.Y + (target.height)), NpcPosition.X - (target.position.X + (target.width)));
            Vector2 direction;
            direction.X = (float)(Math.Cos(rotation) * Speed * -1);
            direction.Y = (float)(Math.Sin(rotation) * Speed * -1);
            var p = Projectile.NewProjectile(npc.GetSource_FromAI(), NpcPosition, direction, type, dammage, 0f, npc.target);
            Main.projectile[p].hostile = true;
            Main.projectile[p].friendly = false;
            return p;
        }
        public List<int> SlimesList =
        [
            -25, -26, -10, -9, -8, -7, -6, -5, -4, -3, -2, -1, 1, 16, 71, 81, 121, 138, 147, 183, 184, 204, 225, 244, 302, 535, 537
        ];

        public override void OnHitByItem(NPC npc, Player player, Item item, NPC.HitInfo hit, int damageDone)
        {
            if (!DificultyUtils.ReaperMode) return;
            
            if (npc.type == NPCID.Creeper)
            {
                if (npc.life <= 0 && Main.rand.NextBool(4))
                {   
                    player.AddBuff(BuffID.Confused, Utils1.FormatTimeToTick(0, 0, 0, 4));
                    player.AddBuff(BuffID.Slow, Utils1.FormatTimeToTick(0, 0, 0, 2));
                }
            }
            

            base.OnHitByItem(npc, player, item, hit, damageDone);
        }

        public override void OnHitByProjectile(NPC npc, Projectile projectile, NPC.HitInfo hit, int damageDone)
        {  
            if (!Reaper.ReaperMode) return;
            
            Player player = Main.player[projectile.owner];

            if (npc.type == NPCID.Creeper)
            {
                projectile.penetrate = 1;

                if (npc.life <= 0 && Main.rand.NextBool(4))
                {
                    player.AddBuff(BuffID.Confused, Utils1.FormatTimeToTick(0, 0, 0, 4));
                    player.AddBuff(BuffID.Slow, Utils1.FormatTimeToTick(0, 0, 0, 2));
                }
            }

            base.OnHitByProjectile(npc, projectile, hit, damageDone);
        }

        public override void HitEffect(NPC npc, NPC.HitInfo hit)
        {
            base.HitEffect(npc, hit);
            if (Main.netMode == NetmodeID.MultiplayerClient) return;
            if (!Reaper.ReaperMode) return;
            
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
                
            if (npc.type == NPCID.Deerclops)
            {
                if (npc.life <= 0)
                {
                    Main.windSpeedCurrent = 0;
                    Main.StopRain();
                }
            }
        }

        bool DearclopsDead = false;
        public override bool CheckDead(NPC npc)
        {
            if (!Reaper.ReaperMode) return base.CheckDead(npc);
            
            if (npc.type == NPCID.MoonLordCore)
            {
                if (npc.life <= 1)
                {

                    if (!RemnantDownedBossSystem.downedMoonlordReaper)
                    {
                        if (Main.netMode == NetmodeID.Server)
                        {
                            NetMessage.SendData(MessageID.WorldData);
                        }
                        RemnantDownedBossSystem.downedMoonlordReaper = true;
                    }

                    RemnantPlayer.ClearBuffToAllPlayers(BuffID.Horrified);
                    RemnantPlayer.ClearBuffToAllPlayers(BuffID.Weak);
                    RemnantPlayer.ClearBuffToAllPlayers(BuffID.Blackout);
                    RemnantPlayer.ClearBuffToAllPlayers(BuffID.ChaosState);
                    RemnantPlayer.ClearBuffToAllPlayers(BuffID.NoBuilding);
                }
            }
            if (npc.type == NPCID.Deerclops)
            {
                if (npc.life <= 1 || (DificultyUtils.InfernumMode))
                {
                    DearclopsDead = true; 
                    RemnantPlayer.ClearBuffToAllPlayers(BuffID.Blackout);
                    RemnantPlayer.ClearBuffToAllPlayers(BuffID.Darkness); 
                }
            }
            if (npc.type == NPCID.QueenSlimeBoss)
            {
                if (npc.life <= 1)
                {
                    RemnantPlayer.ClearBuffToAllPlayers(BuffID.WindPushed);
                    RemnantPlayer.ClearBuffToAllPlayers(BuffID.VortexDebuff);
                    RemnantPlayer.ClearBuffToAllPlayers(BuffID.QueenSlimeMount);
                }
            }
            return base.CheckDead(npc);
        }     
    }
}