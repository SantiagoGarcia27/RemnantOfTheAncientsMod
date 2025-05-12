using RemnantOfTheAncientsMod.World;
using System.Collections.Generic;
using Terraria.ModLoader;
using Terraria;
using CalamityMod;
using Terraria.ID;
using RemnantOfTheAncientsMod.Common.UtilsTweaks;
using SangarUtilities.Common.UtilsTweaks;

namespace RemnantOfTheAncientsMod.Content.NPCs.Bosses.ITyrant
{
    public class InfernalTyrantAuxiliaryClass
    {

        [JITWhenModsEnabled("CalamityMod")]
        public static void CalamityLifeScale(NPC npc, int life)
        {
            life = Reaper.ReaperMode ? life * 2 : life;


            if (npc.type == ModContent.NPCType<InfernalTyrantHead>())
                CalamityUtils.SetLifeBonus(npc, life, 1.5f, 1.7f, 1.8f);
            else if (npc.type == ModContent.NPCType<InfernalTyrantBody>() || npc.type == ModContent.NPCType<InfernalTyrantTail>())
                npc.LifeMaxNERB(life, (int)(life * 1.5), (int)(life * 0.8));
        }
        public static void LifeSpeed(Worm worm)
        {
            
            float lifePercentage = MathUtils.GetPorcentage(worm.NPC.life, worm.NPC.lifeMax);;
            bool isReaperMode = Reaper.ReaperMode;

            if (Main.player[Main.myPlayer].HasBuff(BuffID.Stinky))
            {
                worm.MoveSpeed = 1f;
                worm.Acceleration = 0.1f;
            }
            else if (AllPlayersDead())
            {
                worm.MoveSpeed = 10f;
                worm.Acceleration = 1f;
            }
            else
            {
                if (lifePercentage < 5f && isReaperMode)
                {
                    worm.Acceleration = Main.netMode != NetmodeID.MultiplayerClient ? 1.4f : 1.2f;
                }
                else if (lifePercentage < 10f)
                {
                    worm.Acceleration = 1.1f;
                }
                else
                {
                    worm.Acceleration = 0.15f;

                    if (lifePercentage < 25f)
                    {
                        worm.MoveSpeed = Main.netMode != NetmodeID.MultiplayerClient ? 70f : 60f;
                    }
                    else if (lifePercentage < 50f)
                    {
                        worm.MoveSpeed = Main.netMode != NetmodeID.MultiplayerClient ? 50f : 40f;
                    }
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
       
    }
    public class GenericVariables
    {
        public static int SpawnCounter = 0;
        public static int TimeInmune = 200;
        public static bool IsSpawned = false;
        public static List<bool> SizeChanged = [false, false, false];
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
