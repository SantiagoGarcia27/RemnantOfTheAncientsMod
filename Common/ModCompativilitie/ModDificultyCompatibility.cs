//using Terraria.ModLoader;

//namespace RemnantOfTheAncients.Common.ModCompativilitie
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
//            if (RemnantOfTheAncients.CalamityMod != null)
//            {
//                CheckCalamity();
//                if (DificultyUtils.InfernumMode != null)
//                {
//                    CheckInfernum();
//                }
//            }
//            if(RemnantOfTheAncients.FargosSoulMod != null)
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
//            InfernumMode = (bool)DificultyUtils.InfernumMode.Call("GetInfernumActive");
//        }

//        [JITWhenModsEnabled("FargowiltasSouls")]
//        public void CheckFargos()
//        {
//            EternityMode = FargowiltasSouls.Core.Systems.WorldSavingSystem.EternityMode;
//            MasochistMode = FargowiltasSouls.Core.Systems.WorldSavingSystem.MasochistModeReal;
//        }
//    }
//}