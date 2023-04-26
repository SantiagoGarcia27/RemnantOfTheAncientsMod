using System.ComponentModel;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;

namespace RemnantOfTheAncientsMod
{
    public class ConfigClient : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ClientSide;
        
        #region ToggleEffects
        [Header("Potion Effects")]
        [Label("Feather fall effect on kits")]
        [DefaultValue(true)]
        [Tooltip("Activate the feather fall effect of potion kits")]
        public bool KitsFeatherFall { get; set; }

        [Label("Invisibility effect on kits")]
        [DefaultValue(true)]
        [Tooltip("Activate the invisibility effect of potion kits")]
        public bool KitsInvis { get; set; }

        [Label("Gravitation control effect on kits")]
        [DefaultValue(true)]
        [Tooltip("Activate the gravitation control effect of potion kits")]
        public bool KitsGrav { get; set; }

        [Label("Inferno effect on kits")]
        [DefaultValue(true)]
        [Tooltip("Activate the inferno effect of potion kits")]
        public bool KitsInferno { get; set; }

        [Label("Gills effect on kits")]
        [DefaultValue(true)]
        [Tooltip("Activate the gills effect of potion kits")]
        public bool KitsGills { get; set; }
        #endregion

        #region ReaperSouls
        [Header("Reaper")]

        [Label("Toggle King Slime soul")]
        [SliderColor(11, 181, 176, 29)]
        [Range(0f, 30f)]
        [Increment(1f)]
        [DrawTicks]
        [DefaultValue(30f)]
        [Tooltip("Activate the King Slime soul effect")]
        public float ToggleKingSlimeSoul { get; set; }

        [Label("Toggle Eye of Chutulu soul")]
        [DefaultValue(true)]
        [Tooltip("Activate the Eye of Chutulu soul effect")]
        public bool ToggleEyeOfChutuluSoul { get; set; }

        [Label("Toggle Corrupt soul")]
        [DefaultValue(true)]
        [Tooltip("Activate the Corrupt soul effect")]
        public bool ToggleCorruptSoul { get; set; }

        [Label("Toggle QueenBee soul")]
        [DefaultValue(true)]
        [Tooltip("Activate the Queen Bee soul effect")]
        public bool ToggleQueenBeeSoul { get; set; }

        [Label("Toggle Skeletron soul")]
        [DefaultValue(true)]
        [Tooltip("Activate the Skeletron soul effect")]
        public bool ToggleSkeletronSoul { get; set; }

        [Label("Toggle Dearclops soul")]
        [DefaultValue(true)]
        [Tooltip("Activate the Dearclops soul effect")]
        public bool ToggleDearclopsSoul { get; set; }

        [Label("Toggle Desert Anhilator soul")]
        [DefaultValue(true)]
        [Tooltip("Activate the Desert Anhilator soul effect")]
        public bool ToggleDesertAnhilatorSoul { get; set; }

        [Label("Toggle Wall of flesh soul")]
        [DefaultValue(true)]
        [Tooltip("Activate the Wall of flesh soul effect")]
        public bool ToggleWallOfFleshSoul { get; set; }

        [Label("Toggle Frozen Assaulter soul")]
        [DefaultValue(true)]
        [Tooltip("Activate the Frozen Assaulter soul effect")]
        public bool ToggleFrozenAssaulterSoul { get; set; }

        [Label("Toggle Queen Slime soul")]
        [DefaultValue(true)]
        [Tooltip("Activate the Queen Slime soul effect")]
        public bool ToggleQueenSlimeSoul { get; set; }

        [Label("Toggle Destroyer enrgy cell")]
        [DefaultValue(true)]
        [Tooltip("Activate the Destroyer energy cell effect")]
        public bool ToggleDestroyerSoul { get; set; }

        [Label("Toggle Spazmatism enrgy cell")]
        [DefaultValue(true)]
        [Tooltip("Activate the Spazmatism energy cell effect")]
        public bool ToggleSpazmatismSoul { get; set; }

        [Label("Toggle Retinazor enrgy cell")]
        [DefaultValue(true)]
        [Tooltip("Activate the Retinazor energy cell effect")]
        public bool ToggleRetinazorSoul { get; set; }

        [Label("Toggle Skeletron prime enrgy cell")]
        [SliderColor(11, 181, 176, 29)]
        [Range(0f, 10f)]
        [Increment(1f)]
        [DrawTicks]
        [DefaultValue(10f)]
        [Tooltip("Activate the SkeletronPrime energy cell effect")]
        public float ToggleSkeletronPrimeSoul { get; set; }

        [Label("Toggle Empress of light soul")]
        [DefaultValue(true)]
        [Tooltip("Activate the Empress of light soul effect")]
        public bool ToggleEmpressOfLightSoul { get; set; }

        [Label("Toggle Plantera soul")]
        [DefaultValue(true)]
        [Tooltip("Activate the Plantera soul effect")]
        public bool TogglePlanteraSoul { get; set; }

        [Label("Toggle Infernal Tyrant soul")]
        [DefaultValue(true)]
        [Tooltip("Activate the Infernal Tyrant soul effect")]
        public bool ToggleInfernalTyrantSoul { get; set; }

        [Label("Toggle Golem soul")]
        [DefaultValue(true)]
        [Tooltip("Activate the Golem soul effect")]
        public bool ToggleGolemSoul { get; set; }

        [Label("Toggle Duke Fishron soul")]
        [DefaultValue(true)]
        [Tooltip("Activate the Duke Fishron soul effect")]
        public bool ToggleDukeFishronSoul { get; set; }

        [Label("Toggle Lunatic cultist soul")]
        [DefaultValue(true)]
        [Tooltip("Activate the Lunatic cultis soul effect")]
        public bool ToggleLunaticCultistSoul { get; set; }

        [Label("Toggle Moonlord soul")]
        [DefaultValue(true)]
        [Tooltip("Activate the Moonlord soul effect")]
        public bool ToggleMoonlordSoul { get; set; }
        #endregion

        //[Label("?")]
        //[SliderColor(11, 181, 176, 29)]
        //[Range(1f, 200f)]
        //[Increment(1f)]
        //[DrawTicks]
        //[DefaultValue(200f)]
        //[//Tooltip("Feliz día de los inocentes")]
        //public float xdlevel { get; set; }

    }
}