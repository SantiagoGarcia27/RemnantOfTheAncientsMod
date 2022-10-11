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
            if (Reaper.ReaperMode)
            {
                if (tipo == "MyBoss") i = Main.expertMode ? DamageScale(i, 4, "MyBoss") : Main.masterMode ? DamageScale(i, 5, "MyBoss") : DamageScale(i, 3, "MyBoss");
                else i = Main.expertMode ? DamageScale(i, 2, "a") : Main.masterMode ? DamageScale(i, 2.5, "a") : DamageScale(i, 1.5, "a");
            }
            else if (tipo == "MyBoss") i = Main.expertMode ? DamageScale(i, 2, "MyBoss") : Main.masterMode ? DamageScale(i, 3, "MyBoss") : DamageScale(i, 1, "MyBoss");
            
            return i;
        }
       
        public static double ExpertLifeScale(double i, string tipo)
        {
            if (Reaper.ReaperMode)
            {
                if (Main.expertMode) i = Main.expertMode? LifeScale(i, 2, "a"): LifeScale(i, 1.5, "a");
                if (tipo == "MyBoss") i =Main.expertMode? LifeScale(i, 2, "MyBoss"): LifeScale(i, 1.5, "MyBoss"); 
            }
            else
            {
                i = Main.expertMode ? LifeScale(i, 1, "a" ):Main.masterMode ? LifeScale(i, 1, "a") : LifeScale(i, 1, "a");
                if (tipo == "MyBoss") i = (Main.expertMode)? LifeScale(i, 2, "MyBoss"):(Main.masterMode)? LifeScale(i, 3, "MyBoss"): LifeScale(i, 1, "MyBoss");
            }
            return i;
        }
        public static double LifeScale(double i, double multiplier, string tipo) => i = (tipo != "MyBoss") ? i * multiplier : i * multiplier;

        public static double DamageScale(double i, double multiplier, string tipo) => i = tipo != "MyBoss" ? i * multiplier : i * multiplier;

        public static int DefenseCalculator(int defensa, string npc) => defensa + 6;
        public static int DamageCalculator(Double damagebonus) => (int)(28 * damagebonus);

    }
}