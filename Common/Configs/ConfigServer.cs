using System.ComponentModel;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;

namespace RemnantOfTheAncientsMod
{
    public class ConfigServer : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ServerSide;
        #region Performance

        //[Header("Performance")]

       //[Label("Lag Reducer Mode")]
        [SliderColor(11, 181, 176, 29)]
        [Range(0f, 3f)]
        [Increment(1f)]
        [DrawTicks]
        [DefaultValue(0f)]
       // [//Tooltip("Reduces graphical aspects of the mod in order to improve game performance." +
        //"\n 0 - Disable anti lagg" +
       //"\n 1 - 50% of mod effects" +
        //"\n 2 - 25% of mod effects" +
       // "\n 3 - Disable mod effects")]

        public float LagReducer { get; set; }
        #endregion

        #region VanillaChanges
      //  [Header("Vanilla Changes")]

       //[Label("Vanilla Weapons Changes")]
        [DefaultValue(true)]
        //[//Tooltip("Activate the balance changes of vanilla items"
        //+ "\n(deactivate this if it generates incompatibility with some other mod)")]
        public bool VanillaWeaponsChangesConf { get; set; }

       //[Label("Drop tombs on death")]
        [SliderColor(11, 181, 176, 29)]
        [Range(0f, 2f)]
        [Increment(1f)]
        [DrawTicks]
        [DefaultValue(2f)]
       // [//Tooltip("Defines if the player drops graves upon death" +
       /* "\n 0- Disable all of the tombs" +
        "\n 1 - 50% Chance to drop tombs" +
        "\n 2 - 100% Chance to drop tombs")]*/
        public float DropTombstomOnDeadtConf { get; set; }

       //[Label("Max stack for items")]
        [SliderColor(11, 181, 176, 29)]
        [Range(0f, 9999f)]
        [Increment(100f)]
        [DrawTicks]
        [DefaultValue(999f)]
       // [//Tooltip("Defines the max stack of items")]
        public float MaxItemStackConf { get; set; }
        #endregion


        //[Label("?")]
        //[SliderColor(11, 181, 176, 29)]
        //[Range(1f, 200f)]
        //[Increment(1f)]
        //[DrawTicks]
        //[DefaultValue(200f)]
        //[//Tooltip("Feliz día de los inocentes")]
        //public float xdlevel { get; set; }
        #region ModsCompativilities
        [JITWhenModsEnabled("TerrariaOverhaul")]
        //[Header("Terraria Overhaul")]

       //[Label("Melee weapons cost maná")]
        [DefaultValue(true)]
        [Tooltip("Activate the mechanic of melee cost mana"+
            "\nRequires recharging")]
        [ReloadRequired]
        public bool OverhaulMeleeManaCostConfig { get; set; }
        #endregion
    }
}