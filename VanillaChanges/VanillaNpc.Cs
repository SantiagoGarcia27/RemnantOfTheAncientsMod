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
        public static double ExpertDamageScale(double i, string tipo)
        {
            if (world1.ReaperMode)
            {
                if (Main.expertMode) i = DamageScale(i, 2, "a");
                else if (Main.masterMode) DamageScale(i, 2.5, "a");
                else i = DamageScale(i, 1.5, "a");
                if (tipo == "MyBoss")
                {
                    if (Main.expertMode) i = DamageScale(i, 4, "MyBoss");
                    else if (Main.masterMode) DamageScale(i, 5, "MyBoss");
                    else i = DamageScale(i, 3, "MyBoss");
                } 
            }
            else
            {
                if (tipo == "MyBoss")
                {
                    if (Main.expertMode) i = DamageScale(i, 2, "MyBoss");
                    else if (Main.masterMode) i = DamageScale(i, 3, "MyBoss");
                    else i = DamageScale(i, 1, "MyBoss");
                }
            }
            return i;
        }
       
        public static double ExpertLifeScale(double i, string tipo)
        {
            if (world1.ReaperMode)
            {
                if (Main.expertMode) i = LifeScale(i, 2, "a");
                else i = LifeScale(i, 1.5, "a");
                if (tipo == "MyBoss")
                {
                    if (Main.expertMode) i = LifeScale(i, 2, "MyBoss");
                    else i = LifeScale(i, 1.5, "MyBoss");
                }
            }
            else
            {
                if (Main.expertMode) i = LifeScale(i, 1, "a");
                if (Main.masterMode) i = LifeScale(i, 1, "a");
                if (tipo == "MyBoss")
                {
                    if (Main.expertMode) i = LifeScale(i, 2, "MyBoss");
                    else if (Main.masterMode) i = LifeScale(i, 3, "MyBoss");
                    else i = LifeScale(i, 1, "MyBoss");
                }
            }
            return i;
        }
        public static double LifeScale(double i, double multiplier, string tipo)
        {
            if (tipo != "MyBoss") i = i * multiplier;
            else i = i * multiplier;
            return i;
        }

        public static double DamageScale(double i, double multiplier, string tipo)
        {
            if (tipo != "MyBoss") i = i * multiplier;
            else i = i * multiplier;
            return i;
        }

        public static int DefenseCalculator(int defensa,string npc)
        {
            return defensa + 6;
        }
        public static int DamageCalculator(int daño, string npc, Double damagebonus)
        {
            daño = 28;
            return (int)(daño * damagebonus);
        }
        
    }
}