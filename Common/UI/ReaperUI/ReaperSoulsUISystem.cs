using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Terraria.UI;

namespace RemnantOfTheAncientsMod.Common.UI.ReaperUI
{
    [Autoload(Side = ModSide.Client)] // This attribute makes this class only load on a particular side. Naturally this makes sense here since UI should only be a thing clientside. Be wary though that accessing this class serverside will error
	public class ReaperSoulsUISystem : ModSystem
	{
		private UserInterface userInterface;
		internal ReaperSoulsUIState reaperSoulsUI;
		private static bool Visible = false;
        
		// These two methods will set the state of our custom UI, causing it to show or hide
		public void ShowMyUI() {
			userInterface?.SetState(reaperSoulsUI);
			Visible = true;
		}
		
		public void HideMyUI() {
			userInterface?.SetState(null);
			Visible = false;

        }
		public bool IsVisible() => Visible;

		public override void Load() {
			// Create custom interface which can swap between different UIStates
			userInterface = new UserInterface();
			// Creating custom UIState
			reaperSoulsUI = new ReaperSoulsUIState();

			// Activate calls Initialize() on the UIState if not initialized, then calls OnActivate and then calls Activate on every child element
			reaperSoulsUI.Activate();
		}

		public override void UpdateUI(GameTime gameTime) {
			// Here we call .Update on our custom UI and propagate it to its state and underlying elements
			if (userInterface?.CurrentState != null) 
				userInterface?.Update(gameTime);
		}

		// Adding a custom layer to the vanilla layer list that will call .Draw on your interface if it has a state
		// Setting the InterfaceScaleType to UI for appropriate UI scaling
		public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers) {
			
			
			
			int mouseTextIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Mouse Text"));
			if (mouseTextIndex != -1) {
				layers.Insert(mouseTextIndex, new LegacyGameInterfaceLayer(
					"ExampleMod: Coins Per Minute",
					delegate {
						if (userInterface?.CurrentState != null)
							userInterface.Draw(Main.spriteBatch, new GameTime());
						return true;
					},
					InterfaceScaleType.UI)
				);
			}
		}
	}
}
