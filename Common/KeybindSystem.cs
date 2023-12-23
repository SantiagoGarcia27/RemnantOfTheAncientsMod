using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Common
{
	// Acts as a container for keybinds registered by this mod.
	// See Common/Players/ExampleKeybindPlayer for usage.
	public class KeybindSystem : ModSystem
	{
		public static ModKeybind TogleReaperInterface { get; private set; }

        public override void Load() {
            // Registers a new keybind
            // We localize keybinds by adding a Mods.{ModName}.Keybind.{KeybindName} entry to our localization files. The actual text displayed to english users is in en-US.hjson
            TogleReaperInterface = KeybindLoader.RegisterKeybind(Mod, "ReaperModeInterface", "P");	
		}

		// Please see ExampleMod.cs' Unload() method for a detailed explanation of the unloading process.
		public override void Unload() {
            // Not required if your AssemblyLoadContext is unloading properly, but nulling out static fields can help you figure out what's keeping it loaded.
            TogleReaperInterface = null;
        }
	}

    public class FargosKeybindSystem : ModSystem
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return ModLoader.TryGetMod("FargowiltasSouls", out Mod FargosSoulMod);
        }

        public static ModKeybind TogleFrostBarrier { get; private set; }
        public static ModKeybind ToggNightTp { get; private set; }
        public override void Load()
        {
            if (ModLoader.TryGetMod("FargowiltasSouls", out Mod FargosSoulMod))
            {
                TogleFrostBarrier = KeybindLoader.RegisterKeybind(Mod, "FrostBarrier", "F");
                ToggNightTp = KeybindLoader.RegisterKeybind(Mod, "ToggNightTp", "L");
            }
        }

        // Please see ExampleMod.cs' Unload() method for a detailed explanation of the unloading process.
        public override void Unload()
        {
            TogleFrostBarrier = null;
            ToggNightTp = null;
        }
    }
}
