using CalamityMod;
using RemnantOfTheAncientsMod.Common.World;
using SangarUtilities.Common.UtilsTweaks;
using System.IO;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Content.NPCs.Bosses.ITyrant
{
    public class InfernalTyrant_AuxiliaryModule
    {
        public Player currentTarget { get; set; }
        public NPC npc { get; set; }
        public int attackCounter { get; set; }
        public int attackCounterMaxValue { get; set; } = 800;
        public bool IsPhase2 => MathUtils.GetPorcentage(npc.life, npc.lifeMax) < 50f;

        public bool IsEnraged => MathUtils.GetPorcentage(npc.life, npc.lifeMax) < 25f;

        [JITWhenModsEnabled("CalamityMod")]
        public static void CalamityLifeScale(NPC npc, int life)
        {
            life = Reaper.ReaperMode ? life * 2 : life;


            if (npc.type == ModContent.NPCType<InfernalTyrantHead>())
				Common.CalamityUtils.SetLifeBonus(npc, life, 1.5f, 1.7f, 1.8f);
            else if (npc.type == ModContent.NPCType<InfernalTyrantBody>() || npc.type == ModContent.NPCType<InfernalTyrantTail>())
                npc.LifeMaxNERB(life, (int)(life * 1.5), (int)(life * 0.8));
        }
        public static void LifeSpeed(TyrantBaseWorm worm)
        {
            
            float lifePercentage = MathUtils.GetPorcentage(worm.NPC.life, worm.NPC.lifeMax);;
            bool isReaperMode = Reaper.ReaperMode;

            if (AllPlayersDead())
            {
                worm.MoveSpeed = 10f;
                worm.Acceleration = 1f;
            }
            else
            {
                if (lifePercentage < 5f && isReaperMode)
                {
                    worm.Acceleration = 1.4f;
                    worm.MoveSpeed = 100f;
                }
                else if (lifePercentage < 10f)
                {
                    worm.Acceleration = 1.1f;
                    worm.MoveSpeed = 80f;
                }
                else
                {
                    worm.Acceleration = 0.15f;

                    if (lifePercentage < 25f)
                    {
                        worm.MoveSpeed = 70f;
                    }
                    else if (lifePercentage < 50f)
                    {
                        worm.MoveSpeed = 60f;
                    }
                    else worm.MoveSpeed = 50f;
                }
            }
        } 
        public static bool AllPlayersDead()
        {
            bool allPlayersDead = true;
            foreach (Player player in Main.player)
            {
                if (!player.dead)
                {
                    allPlayersDead = false;
                    break;
                }
            }
            return allPlayersDead;
        }

        public static int MaxSegmentCount(int MinSegmentLength = 15)
        {
            int scale = 0;
            if (DificultyUtils.MasochistMode) scale = 35;
            else if (DificultyUtils.EternityMode) scale = 30;
            else if (DificultyUtils.InfernumMode) scale = 30;
            else if (DificultyUtils.Death) scale = 24;
            else if (DificultyUtils.Revengeance) scale = 14;
            else if (Main.masterMode) scale = 8;
            else if (Main.expertMode) scale = 4;

            return MinSegmentLength + scale;
        }

        public void SendExtraAI(BinaryWriter writer)
        {
            writer.Write(attackCounter);  
        }

        public void ReceiveExtraAI(BinaryReader reader)
        {
            attackCounter = reader.ReadInt32();
        }

    }
    public class GenericVariables
    {
        public static int SpawnCounter = 0;
        public static int TimeInmune = 200;
        public static bool IsSpawned = false;
    }
    public static class BaseStats
    {
        public static int LifeMax = SetMaxLife(30000);

        private static int SetMaxLife(int life)
        {
            return Reaper.ReaperMode ? life * 2 : life;
        }

    }
}
