//using Terraria.ModLoader;

//namespace RemnantOfTheAncientsMod.Common.ModCompativilitie
//{
//    public class ModDificultyCompatibility : ModSystem
//    {
//        public static bool Revengeance = false;
//        public static bool Death = false;
//        public static bool InfernumMode = false;
//        public static bool EternityMode = false;
//        public static bool MasochistMode = false;


//        public override void PostSetupContent()
//        {
//        }
//        public override void PostUpdateWorld()
//        {
//            CheckDificulty();
//            base.PostUpdateWorld();
//        }
//        public void CheckDificulty()
//        {
//            if (RemnantOfTheAncientsMod.CalamityMod != null)
//            {
//                CheckCalamity();
//                if (RemnantOfTheAncientsMod.InfernumMod != null)
//                {
//                    CheckInfernum();
//                }
//            }
//            if(RemnantOfTheAncientsMod.FargosSoulMod != null)
//            {
//                CheckFargos();
//            }
//        }

//        [JITWhenModsEnabled("CalamityMod")]
//        public void CheckCalamity()
//        {
//            if (ModLoader.TryGetMod("CalamityMod", out Mod calamityMod))
//            {
//                Revengeance = (bool)calamityMod.Call("GetDifficultyActive", "revengeance");
//                Death = (bool)calamityMod.Call("GetDifficultyActive", "death");
//            }
//        }

//        [JITWhenModsEnabled("InfernumMode")]
//        public void CheckInfernum()
//        {
//            InfernumMode = (bool)RemnantOfTheAncientsMod.InfernumMod.Call("GetInfernumActive");
//        }

//        [JITWhenModsEnabled("FargowiltasSouls")]
//        public void CheckFargos()
//        {
//            EternityMode = FargowiltasSouls.Core.Systems.WorldSavingSystem.EternityMode;
//            MasochistMode = FargowiltasSouls.Core.Systems.WorldSavingSystem.MasochistModeReal;
//        }
//    }
//}