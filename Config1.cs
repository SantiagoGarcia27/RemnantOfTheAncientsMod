using System.ComponentModel;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;

namespace RemnantOfTheAncientsMod
{
	public class ConfigClient1 : ModConfig
	{
		public override ConfigScope Mode => ConfigScope.ServerSide;

		[Label("Lag Reducer Mode")]
		[SliderColor(11, 181, 176, 29)]
		[Range(0f, 3f)]
		[Increment(1f)]
		[DrawTicks]
		[DefaultValue(0f)]
		[Tooltip("Reduces graphical aspects of the mod in order to improve game performance.")]
		public float LagReducer { get; set; }

		[Label("Vanilla Weapons Changes")]
		[DefaultValue(true)]
		[Tooltip("Activate the balance changes of vanilla items"
		+ "\n(deactivate this if it generates incompatibility with some other mod)")]
		public bool VanillaWeaponsChangesConf { get; set; }


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
        //[Label("?")]
        //[SliderColor(11, 181, 176, 29)]
        //[Range(1f, 200f)]
        //[Increment(1f)]
        //[DrawTicks]
        //[DefaultValue(200f)]
        //[Tooltip("Feliz día de los inocentes")]
        //public float xdlevel { get; set; }

    }
}