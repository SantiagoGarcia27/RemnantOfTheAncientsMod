using RemnantOfTheAncientsMod.World;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.VanillaChanges
{
    public class NpcChanges1 : GlobalNPC
    { 
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