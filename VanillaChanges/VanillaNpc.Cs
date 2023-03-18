using RemnantOfTheAncientsMod.World;
using System;
using Terraria;
using Terraria.ModLoader;
using static Terraria.ModLoader.PlayerDrawLayer;

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
            double scale = 1.0;
            if (Reaper.ReaperMode)
            {
                switch (Main.expertMode, Main.masterMode)
                {
                    case (true, true):
                        scale = 1.7;
                        break;
                    case (true, false):
                        scale = 1.5;
                        break;
                }
            }
            return i * scale;
        }
        public static double ExpertLifeScale(double i)
        {
            double scale;
            if (Reaper.ReaperMode)
            {
                scale = Main.masterMode ? 4 : Main.expertMode ? 3 : 2;
            }
            else
            {
                scale = Main.masterMode ? 2 : Main.expertMode ? 1.5 : 1;
            }
            return i *= scale;
        }
        public static int DefenseCalculator(int defensa) => defensa + 6;
        public static int DamageCalculator(double damagebonus) => (int)(28 * damagebonus);

    }
}