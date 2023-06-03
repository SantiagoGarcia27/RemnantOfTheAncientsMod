using Terraria;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod
{
    [JITWhenModsEnabled("CalamityMod")]
    [ExtendsFromMod("CalamityMod")]
    public static class CalamityUtils
    {
        public static float CurrentStealth() 
        {
            if (ModLoader.TryGetMod("CalamityMod", out Mod calamityMod))
            {
               return (float)calamityMod.Call("GetCurrentStealth");
            }
            return 0f;
        }
        public static float MaxStealth()
        {
            if (ModLoader.TryGetMod("CalamityMod", out Mod calamityMod))
            {
                return (float)calamityMod.Call("GetMaxStealth");
            }
            return 0f;
        }

        public static bool IsStealthStrikeAvailable()
        {
            if (ModLoader.TryGetMod("CalamityMod", out Mod calamityMod)) 
            {
                float cs = CurrentStealth();
                float ms = MaxStealth();
                if (CurrentStealth() == MaxStealth())
                {
                    return true;
                }
            }
            return false;
        }

        public static bool IsDificultyActive(string dificulty)
        {
            if (ModLoader.TryGetMod("CalamityMod", out Mod calamityMod))
            {
                return (bool)calamityMod.Call("GetDifficultyActive", dificulty);
            }
            else return false;
        }

        public static bool IsBossDefeated(string boss)
        {
            if (ModLoader.TryGetMod("CalamityMod", out Mod calamityMod))
            {
                return (bool)calamityMod.Call("GetBossDowned", boss);
            }
            return false;
        }
        //public static void SetDamageReduction(NPC npc,float damageReductionNormal, float damageReductionRevenge, float damageReductionDeath, float damageReductionBossrush)
        //{
        //    if (ModLoader.TryGetMod("CalamityMod", out Mod calamityMod))
        //    {
        //        if(IsDificultyActive("bossrush")) calamityMod.Call("SetDamageReduction", npc, damageReductionBossrush);
        //        else if (IsDificultyActive("death")) calamityMod.Call("SetDamageReduction", npc, damageReductionDeath);
        //        else if (IsDificultyActive("revengeance")) calamityMod.Call("SetDamageReduction", npc, damageReductionRevenge);
        //        else calamityMod.Call("SetDamageReduction", npc, damageReductionNormal);
        //    }
        //}
        public static int SetLifeBonus(NPC npc, float Normal, float Revenge, float Death, float Bossrush)
        {
            if (IsDificultyActive("bossrush")) 
                return (int)(npc.lifeMax * Bossrush);
            else if (IsDificultyActive("death")) 
                return (int)(npc.lifeMax * Death);
            else if (IsDificultyActive("revengeance"))
                return (int)(npc.lifeMax * Revenge);
            else 
                return (int)(npc.lifeMax * Normal);
        }
        //public static void SetCalamityInmunities(NPC npc,bool[] effect)
        //{
        //    if (ModLoader.TryGetMod("CalamityMod", out Mod calamityMod))
        //    {
        //        if (effect[0] == true) calamityMod.Call("SetDamageReduction", npc,

        //                }
        //}
    }
}