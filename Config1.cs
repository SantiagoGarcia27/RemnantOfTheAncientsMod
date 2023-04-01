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


        [Label("Ultimate potion feather fall effect")]
        [DefaultValue(true)]
        [Tooltip("Activate the feather fall effect of Ultimate potion kit")]
        public bool UltimatePotionFeatherFall { get; set; }

        [Label("Ultimate potion invisivility effect")]
        [DefaultValue(true)]
        [Tooltip("Activate the feather invisivility of Ultimate potion kit")]
        public bool UltimatePotionInvis { get; set; }

        /*[Label("ReaperFirsTime")]
		[DefaultValue(true)]
		[Tooltip("")]
		public bool ReaperFirsTimeConf { get; set; }

		[Label("Game Mode")]
		[SliderColor(11, 181, 176, 29)]
		[Range(0f, 3f)]
		[Increment(1f)]
		[DrawTicks]
		[DefaultValue(0f)]
		[Tooltip("Reduces graphical aspects of the mod in order to improve game performance.")]
		public float GameModeDebugg { get; set; }*/
    }
}