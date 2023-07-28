using RemnantOfTheAncientsMod.World;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Common.Global
{
    public class NpcChanges1 : GlobalNPC
    {

        //public override void ModifyHitPlayer(NPC npc, Player target, ref int damage, ref bool crit)
        //{
        //    /*if (npc.type == NPCID.GreenSlime)
        //    {
        //        damage *= 2;
        //    }*/
        //}
        public static double ExpertDamageScale(double i)
        {
            return Reaper.ReaperMode ? i * 2 : i;
        }
        public static double ExpertLifeScale(double i)
        { 
            return Reaper.ReaperMode? i*2 : i;
        }
        public static int DefenseCalculator(int defensa) => defensa + 6;
        public static int DamageCalculator(double damagebonus) => (int)(28 * damagebonus);
    }
}