using FargowiltasSouls.Content.NPCs.EternityModeNPCs.VanillaEnemies.Snow;
using Microsoft.Xna.Framework;
using RemnantOfTheAncientsMod.Common.ModCompativilitie;
using RemnantOfTheAncientsMod.Common.UtilsTweaks;
using RemnantOfTheAncientsMod.Content.Projectiles.Ranger;
using RemnantOfTheAncientsMod.World;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Content.NPCs.Bosses.FrozenAssaulter
{

    public class FrozenAssaulterInfernum
    {
        static int intercalate = 0;
        public static List <bool> phaseChange = new List<bool>()
        {
            false,false,false,false
        };
        public static void InfernumAi(Player target, float distance, NPC npc,int currentPhase, int attackCounter)
        {


           
            if (currentPhase == 1 || currentPhase == 2 || currentPhase == 4)
            {
                int BasicShootTimmer = 30;
                int CubeShootTimmer = 100;
                int numberOfPoints = DificultyUtils.ReaperMode? 20 : 10;

                phase1Model(npc, BasicShootTimmer, CubeShootTimmer, attackCounter, numberOfPoints);
            }

            if (currentPhase == 2)
            {
                if (!phaseChange[1])
                {
                    for (int i = 0; i < 4; i++)
                    {
                        int npcid = ExternalModCallUtils.GetNpcFromMod(RemnantOfTheAncientsMod.CalamityMod, "IceClasper");
                        int a = NPC.NewNPC(npc.GetSource_FromAI(), (int)npc.position.X, (int)npc.position.Y, npcid, 0, 0);
                        Main.npc[a].lifeMax *= 3;
                        Main.npc[a].life *= 3;
                        Main.npc[a].Opacity /= 3;
                        Main.npc[a].damage += 30;
                    }
                    phaseChange[1] = true;
                }
            }
            else if (currentPhase == 3)
            {
                phaseChange[1] = false;
                if (attackCounter % 20 == 0)
                {
                    ShootMainAttackAi(npc, 20, intercalate >= 10, new int[] { 0, intercalate++ });
                    //intercalate = intercalate >= 10 ? 0 : intercalate++;
                }
                phaseChange[2] = false;
            }
            else if (currentPhase == 4)
            {
                if (!phaseChange[2])
                {
                    phaseChange[2] = true;
                    for (int i = 0; i < 5; i++)
                    {
                        int npcid = ExternalModCallUtils.GetNpcFromMod(RemnantOfTheAncientsMod.CalamityMod, "IceClasper");
                        int a = NPC.NewNPC(npc.GetSource_FromAI(), (int)npc.position.X, (int)npc.position.Y, npcid, 0, 0);
                        Main.npc[a].lifeMax *= 3;
                        Main.npc[a].life *= 3;
                        Main.npc[a].Opacity /= 3;
                        Main.npc[a].damage += 30;
                    }
                    for (int i = 0; i < 3; i++)
                    {
                        int npcid = ModContent.NPCType<IGolem>();
                        int a = NPC.NewNPC(npc.GetSource_FromAI(), (int)npc.position.X +(i *10 *16), (int)npc.position.Y, npcid, 0, 0);
                        Main.npc[a].Opacity /= 3;
                    }
                }
            }
        }

        public  static void phase1Model(NPC npc, int n1, int n2, int attackCounter,int numberOfPoints)
        {
           
            if (attackCounter % n1 == 0)
            {
                ShootMainAttackAi(npc, numberOfPoints, intercalate == 0, new int[]{ 1, 0 });
                float radius = 1 * 16;
                for (int i = 0; i < numberOfPoints; i++)
                {
                    float angle = (float)(2 * Math.PI * i / numberOfPoints);
                    float x = (float)(npc.Center.X + radius * Math.Cos(angle));
                    float y = (float)(npc.Center.Y + radius * Math.Sin(angle));
                    int proj = ExternalModCallUtils.GetProjectileFromMod(RemnantOfTheAncientsMod.CalamityMod, "IceBlast");

                    if (DificultyUtils.EternityMode || DificultyUtils.MasochistMode && Main.rand.NextBool(DificultyUtils.MasochistMode ? 2 : 5))
                    {
                        proj = ProjectileID.FrostBeam;
                    }

                    shootIceCubeIa(DificultyUtils.ReaperMode ? 5 : 20, proj, 30f, angle + (intercalate * 50), 0, npc.target,npc, x, y);

                }
                intercalate = intercalate == 0 ? 1 : 0;
            }
            if (attackCounter % n2 == 0)
            {
                shootIceCubeIa(DificultyUtils.ReaperMode ? 10 : 40, ModContent.ProjectileType<FrozenHommingIceBlock>(), 30f, 0, 90, npc.target,npc);
            }
        }
        public static void ShootMainAttackAi(NPC npc, int numberOfPoints, bool intercalateConditiion, int[] intercalateValues)
        {
            float radius = 1 * 16;
            for (int i = 0; i < numberOfPoints; i++)
            {
                float angle = (float)(2 * Math.PI * i / numberOfPoints);
                float x = (float)(npc.Center.X + radius * Math.Cos(angle));
                float y = (float)(npc.Center.Y + radius * Math.Sin(angle));

                int proj = ExternalModCallUtils.GetProjectileFromMod(RemnantOfTheAncientsMod.CalamityMod, "IceBlast");

                if ((DificultyUtils.EternityMode || DificultyUtils.MasochistMode) && Main.rand.NextBool(DificultyUtils.MasochistMode ? 2 : 5))
                {
                    proj = ProjectileID.FrostBeam;
                }
                else
                {
                    proj = ExternalModCallUtils.GetProjectileFromMod(RemnantOfTheAncientsMod.CalamityMod, "IceBlast");
                }
                shootIceCubeIa(DificultyUtils.ReaperMode ? 5 : 20, proj, 30f, angle + (intercalate * 10), 0, npc.target, npc, x, y);

            }
            intercalate = intercalateConditiion ? intercalateValues[0] : intercalateValues[1];
        }

        public static void shootIceCubeIa(int damage, int type, float speed, float grades, float Orotation, int target,NPC npc, float x = 0, float y = 0)
        {
            x = x != 0 ? x : npc.position.X;
            y = y != 0 ? y : npc.position.Y;

            Vector2 position = new Vector2((float)(x + (npc.width / 2)), y + (npc.height / 2));
            Vector2 direction = Vector2.UnitX * speed;
            direction = direction.RotatedBy(grades);
            int i = Projectile.NewProjectile(npc.GetSource_FromAI(), position, direction, type, damage, 0f, Main.myPlayer);
            Main.projectile[i].timeLeft = 1200;
            Main.projectile[i].hostile = true;
            Main.projectile[i].friendly = false;
            Main.projectile[i].rotation += Orotation;
            Main.projectile[i].localAI[2] = target;
        }
    }
}
       