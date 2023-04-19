using System.ComponentModel;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;

namespace RemnantOfTheAncientsMod
{
    public class ConfigServer : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ServerSide;
        #region Performance

        [Header("Performance")]

        [Label("Lag Reducer Mode")]
        [SliderColor(11, 181, 176, 29)]
        [Range(0f, 3f)]
        [Increment(1f)]
        [DrawTicks]
        [DefaultValue(0f)]
        [Tooltip("Reduces graphical aspects of the mod in order to improve game performance.")]

        public float LagReducer { get; set; }
        #endregion

        #region VanillaChanges
        [Header("Vanilla Changes")]

        [Label("Vanilla Weapons Changes")]
        [DefaultValue(true)]
        [Tooltip("Activate the balance changes of vanilla items"
        + "\n(deactivate this if it generates incompatibility with some other mod)")]
        public bool VanillaWeaponsChangesConf { get; set; }
        #endregion


        //[Label("?")]
        //[SliderColor(11, 181, 176, 29)]
        //[Range(1f, 200f)]
        //[Increment(1f)]
        //[DrawTicks]
        //[DefaultValue(200f)]
        //[Tooltip("Feliz día de los inocentes")]
        //public float xdlevel { get; set; }
        #region ModsCompativilities
        [JITWhenModsEnabled("TerrariaOverhaul")]
        [Header("Terraria Overhaul")]

        [Label("Melee weapons cost maná")]
        [DefaultValue(true)]
        [Tooltip("Activate the mechanic of melee cost mana"+
            "\nRequires recharging")]
        [ReloadRequired]
        public bool OverhaulMeleeManaCostConfig { get; set; }
        #endregion
    }
}