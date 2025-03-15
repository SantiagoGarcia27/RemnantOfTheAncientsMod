using System;
using System.ComponentModel;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;

namespace RemnantOfTheAncientsMod.Common.Configs
{
    public class ConfigReaperSouls : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ClientSide;
        #region ReaperSouls

       
        [Header("ReaperSouls")]

        [SliderColor(11, 181, 176, 29)]
        [Range(0f, 30f)]
        [Increment(1f)]
        [DrawTicks]
        [DefaultValue(30f)]
        public float ToggleKingSlimeSoul { get; set; }


        [DefaultValue(true)]
        public bool ToggleEyeOfChutuluSoul { get; set; }

      
        [DefaultValue(true)]    
        public bool ToggleCorruptSoul { get; set; }

       
        [DefaultValue(true)]
        public bool ToggleQueenBeeSoul { get; set; }


        [DefaultValue(true)]
        public bool ToggleSkeletronSoul { get; set; }


        [DefaultValue(true)]
        public bool ToggleDearclopsSoul { get; set; }

        [DefaultValue(true)]
        public bool ToggleDesertAnhilatorSoul { get; set; }

        [DefaultValue(true)]
        public bool ToggleWallOfFleshSoul { get; set; }

        [DefaultValue(true)]
        public bool ToggleFrozenAssaulterSoul { get; set; }

        [DefaultValue(true)]
        public bool ToggleQueenSlimeSoul { get; set; }

        [DefaultValue(true)]
        public bool ToggleDestroyerSoul { get; set; }

        [DefaultValue(true)]
        public bool ToggleSpazmatismSoul { get; set; }


        [DefaultValue(true)]
        public bool ToggleRetinazorSoul { get; set; }


        [SliderColor(11, 181, 176, 29)]
        [Range(0f, 10f)]
        [Increment(1f)]
        [DrawTicks]
        [DefaultValue(10f)]
        public float ToggleSkeletronPrimeSoul { get; set; }


        [DefaultValue(true)]
        public bool ToggleEmpressOfLightSoul { get; set; }
  
        [DefaultValue(true)]
        public bool TogglePlanteraSoul { get; set; }

        [DefaultValue(true)]
        public bool ToggleInfernalTyrantSoul { get; set; }

        [DefaultValue(true)] 
        public bool ToggleGolemSoul { get; set; }

        [DefaultValue(true)]
        public bool ToggleDukeFishronSoul { get; set; }

        [DefaultValue(true)]
        public bool ToggleLunaticCultistSoul { get; set; }

        [DefaultValue(true)]
        public bool ToggleMoonlordSoul { get; set; }
        #endregion

        #region CalamityReaperSouls
        [JITWhenModsEnabled("CalamityMod")]
        [Header("CalamityReaperSouls")]

        [DefaultValue(true)]
        public bool ToggleDesertScourgeSoul { get; set; }

        [DefaultValue(true)]
        public bool ToggleCrabulonSoul { get; set; }

        [DefaultValue(true)]
        public bool ToggleCalamityCorruptBossSoul { get; set; }

        [DefaultValue(true)]
        public bool ToggleSlimeGodSoul { get; set; }

        [DefaultValue(true)]
        public bool ToggleCryogenSoul { get; set; }

        [DefaultValue(true)]
        public bool ToggleAquaticSourgeSoul { get; set; }

        [DefaultValue(true)]
        public bool ToggleBrimstonElementalSoul { get; set; }

        [DefaultValue(true)]
        public bool ToggleCalamitasCloneSoul { get; set; }

        [DefaultValue(true)]
        public bool ToggleLeviatanSoul { get; set; }

        [DefaultValue(true)]
        public bool ToggleAstrumAureusSoul { get; set; }

        [DefaultValue(true)]
        public bool ToggleAstrumDeusSoul { get; set; }

        [DefaultValue(true)]
        public bool TogglePlaguebringerGoliathSoul { get; set; }

        [DefaultValue(true)]
        public bool ToggleRavagerSoul { get; set; }

        [DefaultValue(true)]
        public bool ToggleDragonFollySoul { get; set; }

        [DefaultValue(true)]
        public bool ToggleProfanedGuardianCommanderSoul { get; set; }

        [DefaultValue(true)]
        public bool ToggleProvidencedSoul { get; set; }

        [DefaultValue(true)]
        public bool ToggleSignusSoul { get; set; }

        [DefaultValue(true)]
        public bool ToggleStormWeaverSoul { get; set; }

        [DefaultValue(true)]
        public bool ToggleCeaselessVoidSoul { get; set; }

        [DefaultValue(true)]
        public bool TogglePolterghastSoul { get; set; }

        [DefaultValue(true)]
        public bool ToggleOldDukeSoul { get; set; }

        [DefaultValue(true)]
        public bool ToggleDevourerofGodsSoul { get; set; }

        [DefaultValue(true)]
        public bool ToggleYharonSoul { get; set; }

        [DefaultValue(true)]
        public bool ToggleExoMechSoul { get; set; }

        [DefaultValue(true)]
        public bool ToggleSuprmeCalamitasSoul { get; set; }

        #endregion
    }
}
