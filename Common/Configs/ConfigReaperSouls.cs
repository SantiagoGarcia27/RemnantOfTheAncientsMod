using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    }
}
