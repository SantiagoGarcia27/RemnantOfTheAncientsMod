using RemnantOfTheAncientsMod.World;
using System;
using Terraria;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.VanillaChanges
{
    public class NpcChanges1 : GlobalNPC
    {

        public override void ModifyHitPlayer(NPC npc, Player target, ref int damage, ref bool crit)
        {
            /*if (npc.type == NPCID.GreenSlime)
            {
                damage *= 2;
            }*/
        }
        public static double ExpertDamageScale(double i)
        {
            if (Reaper.ReaperMode)
            {
                if (Main.masterMode)
                {
                    i = DamageScale(i, 1.7, true);
                }
                else if (Main.expertMode)
                {
                    i = DamageScale(i, 1.5, true);
                }  
                else
                {
                    i = DamageScale(i, 1, true);
                }
            }
            return i;
        }
       
        public static double ExpertLifeScale(double i, bool IsBoss)
        {
            if (Reaper.ReaperMode)
            {
                if (Main.masterMode)
                {
                    LifeScale(i, 4, IsBoss);
                }
                else if (Main.expertMode)
                {
                    LifeScale(i, 3, IsBoss);
                }
                else
                {
                    LifeScale(i, 2, IsBoss);
                }
            }
            else
            {
                if (Main.masterMode)
                {
                    LifeScale(i, 2, IsBoss);
                }
                else if (Main.expertMode)
                {
                    LifeScale(i, 1.5, IsBoss);
                }
                else
                {
                    LifeScale(i, 1, IsBoss);
                }
            }
            return i;
        }
        public static double LifeScale(double i, double multiplier, bool IsBoss)
        {
            if (IsBoss)
            {
                i *= multiplier;
            }
            else
            {
                i *= multiplier;
            }
            return i;
        }

        public static double DamageScale(double i, double multiplier, bool IsBoss)
        {
            double a = i* multiplier;
            return a;
        }

        public static int DefenseCalculator(int defensa, string npc) => defensa + 6;
        public static int DamageCalculator(double damagebonus) => (int)(28 * damagebonus);

    }
}