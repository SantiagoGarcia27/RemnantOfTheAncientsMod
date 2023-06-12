using Terraria.ID;
using RemnantOfTheAncientsMod.Common.Global;
using RemnantOfTheAncientsMod.World;
using Terraria;
using Terraria.ModLoader;
using System.Collections.Generic;
using Terraria.DataStructures;
using System;
using Microsoft.Xna.Framework;
using RemnantOfTheAncientsMod.Content.Projectiles.Multiclass;
using RemnantOfTheAncientsMod.Content.NPCs;
using RemnantOfTheAncientsMod;
using RemnantOfTheAncientsMod.Common.UtilsTweaks;

public class ReaperGlobalNpc : GlobalNPC
{
    public override bool InstancePerEntity => true;
    public override void SetDefaults(NPC npc)
    {
        if (npc.boss)
        {
            ModifyBossStats(npc, ref npc.damage, npc.lifeMax);
        }

        if (Reaper.ReaperMode)
        {
            if (destroyer.Contains(npc.type))
            {
                npc.scale *= 2f;
            }
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
            if(npc.type == NPCID.Creeper)
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
    public List<int> destroyer = new List<int>
    {
        NPCID.TheDestroyer ,
        NPCID.TheDestroyerBody,
        NPCID.TheDestroyerTail
    };
    public static void ModifyBossStats(NPC npc, ref int damage, int lifeMax)
    {
        npc.lifeMax = (int)NpcChanges1.ExpertLifeScale(lifeMax);
        npc.damage = (int)NpcChanges1.ExpertDamageScale(damage);
    }
    public override void OnSpawn(NPC npc, IEntitySource source)
    {
        if (Reaper.ReaperMode)
        {
            if (npc.type == NPCID.SkeletronHead)
            {
                for (int i = 0; i < 3; i++)
                {
                    NPC.SpawnOnPlayer(Main.player[npc.target].whoAmI, NPCID.SkeletronHand);
                }
            }
            if (npc.type == NPCID.Deerclops)
            {
                if (Main.netMode == NetmodeID.MultiplayerClient)
                {
                    RemnantPlayer.ApplyBuffToAllPlayers(BuffID.Hunger, 0, 1, 30);
                }
                else
                {
                    Main.player[npc.target].AddBuff(BuffID.Hunger, Utils1.FormatTime(0, 0, 0, 30));
                }
                // Main.dontStarveWorld = true;
            }
            if (npc.type == NPCID.Golem)
            {
                NPC.NewNPC(npc.GetSource_FromAI(), (int)npc.position.X, (int)npc.position.Y, NPCID.GolemFistLeft);
                NPC.NewNPC(npc.GetSource_FromAI(), (int)npc.position.X, (int)npc.position.Y, NPCID.GolemFistRight);
            }
            if (npc.type == NPCID.GolemFistLeft || npc.type == NPCID.GolemFistRight)
            {
                npc.life = npc.lifeMax;
            }
            if (npc.type == NPCID.HallowBoss)
            {
                Main.dayTime = true;
                Main.time = 5000.0;
                Netcode.SyncWorld();
            }
            //if(npc.type == NPCID.MoonLordCore)
            //{
            //    for (int i = 0; i < 2; i++)
            //    {
            //      int a =  NPC.NewNPC(npc.GetSource_FromAI(), (int)npc.position.X, (int)npc.position.Y, NPCID.MoonLordHand);
            //        Main.npc[a].velocity *= 2;
            //        NpcList.Add(Main.npc[a]);
            //    }
            //}
            base.OnSpawn(npc, source);
        }
    }
    int attackCounter = 0;
    List<NPC> NpcList = new List<NPC>();
    public override void AI(NPC npc)
    {
        base.AI(npc);

        List<int> ReaperAffectedNPCs = new List<int>()
        {
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
        };

        List<int> SkeletronPrimeHands = new List<int>()
        {
            NPCID.PrimeCannon,
            NPCID.PrimeLaser,
            NPCID.PrimeSaw,
            NPCID.PrimeVice
        };

        if (Reaper.ReaperMode && ReaperAffectedNPCs.Contains(npc.type))
        {
            if (npc.target < 0 || npc.target >= 255 || Main.player[npc.target].dead || !Main.player[npc.target].active)
            {
                npc.TargetClosest();
            }
            Player player = Main.player[npc.target];
            if (npc.type == NPCID.Creeper)
            {
                for (int i = 0; i < Main.maxNPCs; i++)
                {
                    if (Main.npc[i].type == NPCID.BrainofCthulhu)
                    {
                        List<Vector2> Postions = GetPositionsAroundNPC(Main.npc[i], 25 * 15f, GetEnemyCount(NPCID.Creeper));
                        List<NPC> CreepersList = GetNPCList(NPCID.Creeper);

                        for (int a = 0; a < GetEnemyCount(NPCID.Creeper); a++)
                        {
                            CreepersList[a].position = Postions[a];
                        }
                    }
                }
            }
            if (npc.type == NPCID.QueenBee)
            {
                if (attackCounter < 300)
                {
                    attackCounter++;
                }
                else
                {
                    attackCounter = 0;
                }

                if (attackCounter == 200)
                {
                    NPC.NewNPC(NPC.GetSource_None(), (int)npc.Center.X, (int)npc.Center.Y, -57);
                }
                Point NpcFloor = Utils.ToTileCoordinates(npc.Center);
                if (Main.tile[NpcFloor.X, NpcFloor.Y + 1].LiquidAmount > 0 && Main.tile[NpcFloor.X, NpcFloor.Y + 1].LiquidType == LiquidID.Honey)
                {
                    if (npc.life < npc.lifeMax - 5)
                    {
                        npc.life += 5;
                        npc.HealEffect(5);
                    }
                }
            }
            if (npc.type == NPCID.SkeletronHead)
            {
                for (int i = 0; i < Main.maxNPCs; i++)
                {
                    if (Main.npc[i].type == NPCID.SkeletronHand)
                    {
                        if (Main.npc[i].dontTakeDamage != true)
                        {
                            npc.dontTakeDamage = NPC.AnyNPCs(NPCID.SkeletronHand);
                        }
                    }
                }
            }
            if (npc.type == NPCID.Deerclops)
            {
                Main.windSpeedCurrent = 2f;
                Main.screenMaximized = false;

                Main.StartRain();
                Main.SyncRain();

                if (Main.netMode == NetmodeID.MultiplayerClient)
                {
                    RemnantPlayer.ApplyBuffToAllPlayers(BuffID.Hunger, 0, 0, 30);
                    RemnantPlayer.ApplyBuffToAllPlayers(BuffID.Darkness, 0, 0, 30);
                    RemnantPlayer.ApplyBuffToAllPlayers(BuffID.Blackout, 0, 0, 30);
                }
                else
                {
                    Main.player[npc.target].AddBuff(BuffID.Hunger, Utils1.FormatTime(0, 0, 0, 30));
                    Main.player[npc.target].AddBuff(BuffID.Darkness, Utils1.FormatTime(0, 0, 0, 30));
                    Main.player[npc.target].AddBuff(BuffID.Blackout, Utils1.FormatTime(0, 0, 0, 30));
                }
            }
            if (npc.type == NPCID.QueenSlimeBoss)
            {
                if (Main.netMode == NetmodeID.MultiplayerClient)
                {
                    RemnantPlayer.ApplyBuffToAllPlayers(BuffID.WindPushed, 0, 0, 30);
                    RemnantPlayer.ApplyBuffToAllPlayers(BuffID.VortexDebuff, 0, 0, 30);
                    RemnantPlayer.ApplyBuffToAllPlayers(BuffID.QueenSlimeMount, 0, 0, 30);
                }
                else
                {
                    Main.player[npc.target].AddBuff(BuffID.WindPushed, Utils1.FormatTime(0, 0, 0, 30));
                    Main.player[npc.target].AddBuff(BuffID.VortexDebuff, Utils1.FormatTime(0, 0, 0, 30));
                    Main.player[npc.target].AddBuff(BuffID.QueenSlimeMount, Utils1.FormatTime(0, 0, 0, 30));
                }
            }
            if (npc.type == NPCID.WallofFleshEye)
            {
                if (attackCounter < 100)
                {
                    attackCounter++;
                }
                else
                {
                    attackCounter = 0;
                }

                if (attackCounter >= 50 && attackCounter <= 60)
                {
                    for (int i = 0; i < Main.maxProjectiles; i++)
                    {
                        if (Main.projectile[i].friendly && !Main.projectile[i].minion && Main.projectile[i].damage > 0 && npc.Distance(Main.projectile[i].position) < 100 * 14f)
                        {
                            var t = Main.projectile[i].Name;
                            updateRotation(npc, true);
                            var p = Projectile.NewProjectile(Projectile.GetSource_None(), npc.position, (Main.projectile[i].position - npc.position), ModContent.ProjectileType<InterceptionBream>(), 0, 0, Main.myPlayer);//ShootIa(npc, npc.damage / 2, ModContent.ProjectileType<InterceptionBream>(), Main.npc[i], 20f);
                            Main.projectile[p].Size = new(60f, 60f);
                            Main.projectile[p].hostile = true;
                            Main.projectile[p].friendly = false;
                            Main.projectile[p].velocity *= 3;
                        }
                    }
                }
            }
            if (npc.type == NPCID.Spazmatism)
            {
                if (npc.localAI[2] >= 1f)
                {
                    if (npc.ai[2] == 100f || npc.ai[2] == 50f || npc.ai[2] == 150f || npc.ai[2] == 0f || npc.ai[2] == 200f)
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            ShootIa(npc, npc.damage, ProjectileID.CursedFlameHostile, player, 10f + i, new(-0.5f + i, 0.5f + i), i * 10);
                        }
                    }
                }
            }
            if (npc.type == NPCID.Retinazer)
            {

                if (NPC.AnyNPCs(NPCID.Spazmatism))
                {
                    if (attackCounter < 200)
                    {
                        attackCounter++;
                    }
                    else
                    {
                        attackCounter = 0;
                    }

                    if (attackCounter == 100)
                    {
                        for (int i = 0; i < Main.maxNPCs; i++)
                        {
                            if (Main.npc[i].type == NPCID.Spazmatism)
                            {
                                updateRotation(npc, false);
                                var p = ShootIa(npc, npc.damage / 2, ModContent.ProjectileType<HealthBream>(), Main.npc[i], 10f);
                                Main.projectile[p].Size = new(60f, 60f);
                            }
                        }
                    }
                }
            }

            bool[] IsInmune = new bool[4];
            if (npc.type == NPCID.SkeletronPrime)
            {
                if (NPC.AnyNPCs(NPCID.PrimeCannon) || NPC.AnyNPCs(NPCID.PrimeLaser) || NPC.AnyNPCs(NPCID.PrimeSaw) || NPC.AnyNPCs(NPCID.PrimeVice))
                {
                    for (int a = 0; a < Main.maxNPCs; a++)
                    {
                        for (int i = 0; i < SkeletronPrimeHands.Count; i++)
                        {
                            if (SkeletronPrimeHands.Contains(Main.npc[a].type) && !Main.npc[a].dontTakeDamage)
                            {
                                IsInmune[i] = true;
                            }
                            else
                            {
                                IsInmune[i] = false;
                            }
                        }
                    }
                    if (!IsInmune[0] && !IsInmune[1] && !IsInmune[2] && !IsInmune[3])
                    {
                        npc.dontTakeDamage = true;
                    }
                    else
                    {
                        npc.dontTakeDamage = false;
                    }
                }
                else
                {
                    npc.dontTakeDamage = false;
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
                    for (int i = 0; i < Main.maxNPCs; i++)
                    {
                        if (Main.npc[i].type == NPCID.PlanterasHook)
                        {
                            if (attackCounter == 100)
                            {
                                Main.npc[i].velocity = player.position + new Vector2(Main.rand.Next(-6, 6) * 16, Main.rand.Next(-6, 6) * 16) - npc.position;
                            }
                        }
                    }
                }
                if (attackCounter < 110)
                {
                    attackCounter++;
                }
                else
                {
                    attackCounter = 0;
                }
            }
            if (npc.type == NPCID.DukeFishron)
            {
                if (attackCounter == 50)
                {
                    if (NPC.CountNPCS(ModContent.NPCType<MiniDuke>()) < 3)
                    {
                        int n = NPC.NewNPC(npc.GetSource_FromAI(), (int)npc.position.X, (int)npc.position.Y + (Main.rand.Next(-4, 4) * 16), ModContent.NPCType<MiniDuke>());
                        Main.npc[n].lifeMax = 1000;
                        Main.npc[n].life = 1000;
                    }
                }
                if (attackCounter < 400)
                {
                    attackCounter++;
                }
                else
                {
                    attackCounter = 0;
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
                    if (attackCounter == 100)
                    {
                        for (int i = 0; i < 5; i++)
                        {
                            int p = Projectile.NewProjectile(npc.GetSource_FromAI(), npc.position + new Vector2(i * 50, 0), (Main.player[npc.target].position - npc.position).RotatedBy(i * 10), ProjectileID.CultistBossFireBall, 30, 0, 255);
                            Main.projectile[p].velocity /= 2;
                        }
                    }
                    if (attackCounter == 110)
                    {
                        for (int i = 0; i < 5; i++)
                        {
                            int p = Projectile.NewProjectile(npc.GetSource_FromAI(), npc.position + new Vector2(i * 50, 0), (Main.player[npc.target].position - npc.position).RotatedBy(i * 10), ProjectileID.CultistBossFireBallClone, 30, 0, 255);
                            Main.projectile[p].velocity /= 4;
                        }
                    }
                }
                if (attackCounter < 200)
                {
                    attackCounter++;
                }
                else
                {
                    attackCounter = 0;
                }
            }
            if (npc.type == NPCID.MoonLordCore)
            {
                if (Main.netMode == NetmodeID.MultiplayerClient)
                {
                    RemnantPlayer.ApplyBuffToAllPlayers(BuffID.Horrified, 0, 0, 30);
                    RemnantPlayer.ApplyBuffToAllPlayers(BuffID.Weak, 0, 0, 30);
                    RemnantPlayer.ApplyBuffToAllPlayers(BuffID.Blackout, 0, 0, 30);
                    RemnantPlayer.ApplyBuffToAllPlayers(BuffID.ChaosState, 0, 0, 30);
                    RemnantPlayer.ApplyBuffToAllPlayers(BuffID.NoBuilding, 0, 0, 30);
                    if (NPC.AnyNPCs(NPCID.Dryad))
                    {
                        RemnantPlayer.ApplyBuffToAllPlayers(BuffID.DryadsWardDebuff, 0, 0, 30);
                    }
                }
                else
                {
                    Main.player[npc.target].AddBuff(BuffID.Horrified, Utils1.FormatTime(0, 0, 0, 30));
                    Main.player[npc.target].AddBuff(BuffID.Weak, Utils1.FormatTime(0, 0, 0, 30));
                    Main.player[npc.target].AddBuff(BuffID.Blackout, Utils1.FormatTime(0, 0, 0, 30));
                    Main.player[npc.target].AddBuff(BuffID.ChaosState, Utils1.FormatTime(0, 0, 0, 30));
                    Main.player[npc.target].AddBuff(BuffID.NoBuilding, Utils1.FormatTime(0, 0, 0, 30));
                    if (NPC.AnyNPCs(NPCID.Dryad))
                    {
                        Main.player[npc.target].AddBuff(BuffID.DryadsWardDebuff, Utils1.FormatTime(0, 0, 0, 30));
                    }
                }
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
            if (attackCounter < 800)
            {
                attackCounter++;
            }
            else
            {
                attackCounter = 0;
            }
        }
    }
    public int GetHootAmount(NPC npc)
    {
        if (npc.life <= Utils1.GetValueFromPorcentage(npc.lifeMax, 50))
        {
            if (npc.life <= Utils1.GetValueFromPorcentage(npc.lifeMax, 25))
            {
                return 1;
            }
            return 2;
        }
        else return 3;
    }
    public int GetEnemyCount(int npcid)
    {
        int EnemyCount = 0;
        for (int i = 0; i < Main.maxNPCs; i++)
        {
            if (Main.npc[i].type == npcid)
            {
                EnemyCount++;
            }
        }
        return EnemyCount;
    }
    private List<NPC> GetNPCList(int npcid) 
    {
        List<NPC> CreepersList = new List<NPC> { };
        for(int i = 0; i < Main.maxNPCs; i++)
        {
            if (Main.npc[i].type == npcid)
            {
                CreepersList.Add(Main.npc[i]);
            }
        }
        return CreepersList;
    }
    List<Vector2> GetPositionsAroundNPC(NPC npc, float radius, int numPositions)
    {
        List<Vector2> positions = new List<Vector2>();
 
        float angleSpacing = MathHelper.TwoPi / numPositions;

        for (int i = 0; i < numPositions; i++)
        {
            float angle = angleSpacing * i;

            float x = radius * MathF.Cos(angle);
            float y = radius * MathF.Sin(angle);

            Vector2 position = npc.Center + new Vector2(x, y);

            positions.Add(position);
        }

        return positions;
    }


    public static NPC target;
    public static Projectile targetProj;
    private void updateRotation(NPC npc, bool p)
    {
        if (p)
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

    public float Range = 40f * 16f;
    private NPC SearchForTeamTarget(NPC npc)
    {
        for (int i = 0; i < Main.maxNPCs; i++)
        {
            if (Main.npc[i].friendly == npc.friendly && Main.npc[i].active && Main.npc[i].type != npc.type)
            {
                float distance = Vector2.Distance(npc.position, Main.npc[i].position);
                if (distance < Range)
                {
                    // var a = Main.projectile[i].Name;
                    return Main.npc[i];
                }
            }
        }
        return null;
    }
    private Projectile SearchForProjectileTarget(NPC npc)
    {
        for (int i = 0; i < Main.maxProjectiles; i++)
        {
            if (Main.projectile[i].friendly == !npc.friendly && Main.projectile[i].active)
            {
                float distance = Vector2.Distance(npc.position, Main.npc[i].position);
                if (distance < Range)
                {
                    // var a = Main.projectile[i].Name;
                    return Main.projectile[i];
                }
            }
        }
        return null;
    }
    public static void ShootIa(NPC npc, int dammage, int type, Player player, float Speed, Vector2 position, float angle)
    {
        Vector2 NpcPosition = new Vector2(npc.position.X + (npc.width / 2), npc.position.Y + (npc.height / 2));
        float rotation = (float)Math.Atan2(NpcPosition.Y - (player.position.Y + (player.height * position.X)), NpcPosition.X - (player.position.X + (player.width * position.Y)));
        Vector2 direction;
        direction.X = (float)(Math.Cos(rotation) * Speed * -1);
        direction.Y = (float)(Math.Sin(rotation) * Speed * -1);
        direction.RotatedBy(angle);
        Projectile.NewProjectile(npc.GetSource_FromAI(), NpcPosition, direction, type, dammage, 0f, Main.myPlayer);
    }
    public static void ShootIa(NPC npc, int dammage, int type, NPC target, float Speed, Vector2 position, float angle)
    {
        Vector2 NpcPosition = new Vector2(npc.position.X /*+ (npc.width / 2)*/, npc.position.Y /*+ (npc.height / 2)*/);
        float rotation = (float)Math.Atan2(NpcPosition.Y - (target.position.Y + (target.height * position.X)), NpcPosition.X - (target.position.X + (target.width * position.Y)));
        Vector2 direction;
        direction.X = (float)(Math.Cos(rotation) * Speed * -1);
        direction.Y = (float)(Math.Sin(rotation) * Speed * -1);
        direction.RotatedBy(angle);
        var p = Projectile.NewProjectile(npc.GetSource_FromAI(), NpcPosition, direction, type, dammage, 0f, Main.myPlayer);
        Main.projectile[p].hostile = true;
        Main.projectile[p].friendly = false;
    }
    public static int ShootIa(NPC npc, int dammage, int type, NPC target, float Speed)
    {
        Vector2 NpcPosition = new Vector2(npc.position.X /*+ (npc.width / 2)*/, npc.position.Y /*+ (npc.height / 2)*/);
        float rotation = (float)Math.Atan2(NpcPosition.Y - (target.position.Y + (target.height)), NpcPosition.X - (target.position.X + (target.width)));
        Vector2 direction;
        direction.X = (float)(Math.Cos(rotation) * Speed * -1);
        direction.Y = (float)(Math.Sin(rotation) * Speed * -1);
        var p = Projectile.NewProjectile(npc.GetSource_FromAI(), NpcPosition, direction, type, dammage, 0f, Main.myPlayer);
        Main.projectile[p].hostile = true;
        Main.projectile[p].friendly = false;
        return p;
    }


    public override void HitEffect(NPC npc, int hitDirection, double damage)
    {
        List<int> SlimesList = new List<int>()
        {
            -25,
            -26,
            -10,
            -9,
            -8,
            -7,
            -6,
            -5,
            -4,
            -3,
            -2,
            -1,
            1,
            16,
            59,
            71,
            81,
            138,
            147,
            183,
            184,
            204,
            225,
            244,
            302,
            535,
            537
        };

        base.HitEffect(npc, hitDirection, damage);
        if (Reaper.ReaperMode)
        {
            if (npc.type == NPCID.KingSlime)
            {
                int choice = Main.rand.Next(SlimesList.Count);
                var p = NPC.NewNPC(npc.GetSource_FromAI(), (int)npc.position.X, (int)npc.position.Y, SlimesList[choice]);
                Main.npc[p].GetGlobalNPC<RemnantGlobalNPC>().CanDrop = false;
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

                    var p = Projectile.NewProjectile(npc.GetSource_FromAI(), npc.position, direction * projectileSpeed, ProjectileID.CursedFlameHostile, 50, 0f, Main.myPlayer);
                    angle += angleSpacing;
                }
            }
            if (npc.type == NPCID.Creeper)
            {
                if (npc.life <= 0)
                {
                    if (Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        Main.player[Main.myPlayer].AddBuff(BuffID.Confused, Utils1.FormatTime(0, 0, 0, 4));
                        Main.player[Main.myPlayer].AddBuff(BuffID.Slow, Utils1.FormatTime(0, 0, 0, 2));
                    }
                    else
                    {
                        Main.player[npc.target].AddBuff(BuffID.Confused, Utils1.FormatTime(0, 0, 0, 4));
                        Main.player[npc.target].AddBuff(BuffID.Slow, Utils1.FormatTime(0, 0, 0, 2));
                    }
                }
            }
            if (npc.type == NPCID.Deerclops)
            {
                if (npc.life <= 0)
                {
                    Main.windSpeedCurrent= 0;
                    Main.StopRain();
                }
            }
        }
    }
    public override void OnHitByProjectile(NPC npc, Projectile projectile, int damage, float knockback, bool crit)
    {
        base.OnHitByProjectile(npc, projectile, damage, knockback, crit);
        if (Reaper.ReaperMode)
        {
            if(npc.type == NPCID.Creeper)
            {
                projectile.penetrate = 1;
            }
        }
    }
}