using System.ComponentModel;
using Terraria.ModLoader.Config;

namespace RemnantOfTheAncientsMod
{
    public class ConfigClient : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ClientSide;
        
        #region ToggleEffects
        [Header("PotionEffects")]
        //[Label("Feather fall effect on kits")]
        [DefaultValue(true)]
        //[//Tooltip("Activate the feather fall effect of potion kits")]
        public bool KitsFeatherFall { get; set; }

        //[Label("Invisibility effect on kits")]
        [DefaultValue(true)]
       // //[//Tooltip("Activate the invisibility effect of potion kits")]
        public bool KitsInvis { get; set; }

        //[Label("Gravitation control effect on kits")]
        [DefaultValue(true)]
       // //[//Tooltip("Activate the gravitation control effect of potion kits")]
        public bool KitsGrav { get; set; }

        //[Label("Inferno effect on kits")]
        [DefaultValue(true)]
       // //[//Tooltip("Activate the inferno effect of potion kits")]
        public bool KitsInferno { get; set; }

        [DefaultValue(true)]
        // //[//Tooltip("Activate the inferno effect of potion kits")]
        public bool KitsGills { get; set; }


        [DefaultValue(true)]
        // //[//Tooltip("Activate the inferno effect of potion kits")]
        public bool AprilFoolsDay { get; set; }
        #endregion

      

        ////[Label("?")]
        //[SliderColor(11, 181, 176, 29)]
        //[Range(1f, 200f)]
        //[Increment(1f)]
        //[DrawTicks]
        //[DefaultValue(200f)]
        //[//Tooltip("Feliz día de los inocentes")]
        //public float xdlevel { get; set; }

    }
}