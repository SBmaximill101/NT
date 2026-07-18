using Terraria.ModLoader;

namespace NT.ClassContent
{
    public class HandSealWheel : ModSystem
    {
		public static ModKeybind HandSealWheelKeybind { get; private set; }

		public override void Load()
		{
			// Registers a new keybind
			// We localize keybinds by adding a Mods.{ModName}.Keybind.{KeybindName} entry to our localization files. The actual text displayed to english users is in en-US.hjson
			//HandSealWheelKeybind = KeybindLoader.RegisterKeybind(Mod, "HandSealWheel", "v");
		}

		// Please see ExampleMod.cs' Unload() method for a detailed explanation of the unloading process.
		public override void Unload()
		{
			// Not required if your AssemblyLoadContext is unloading properly, but nulling out static fields can help you figure out what's keeping it loaded.
			//HandSealWheelKeybind = null;
		}
	}
}
