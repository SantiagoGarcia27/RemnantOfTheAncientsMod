using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.ModLoader;
using Terraria.UI;
using Terraria.GameContent;
using System.Collections.Generic;
using Terraria.Localization;
using RemnantOfTheAncientsMod.Common.Global.Items;
using Terraria.ID;

namespace RemnantOfTheAncientsMod.Common.UI.ChargeBar
{
    // This custom UI will show whenever the player is holding the ExampleCustomResourceWeapon item and will display the player's custom resource amounts that are tracked in ExampleResourcePlayer

    internal class GenericChargeBar : UIState
	{
		// For this bar we'll be using a frame texture and then a gradient inside bar, as it's one of the more simpler approaches while still looking decent.
		// Once this is all set up make sure to go and do the required stuff for most UI's in the ModSystem class.
		private UIText text;
		private UIElement area;
		private UIImage barFrame;
		private Color gradientA;
		private Color gradientB;

		public override void OnInitialize() {
			// Create a UIElement for all the elements to sit on top of, this simplifies the numbers as nested elements can be positioned relative to the top left corner of this element. 
			// UIElement is invisible and has no padding.
			area = new UIElement();
			area.Left.Set(-area.Width.Pixels - (600 * 1.5f) +60, 1f); // Place the resource bar to the left of the hearts.
            area.Top.Set(400, 0f); // Placing it just a bit below the top of the screen. 
			area.Width.Set(81, 0f); // We will be placing the following 2 UIElements within this 182x60 area.
			area.Height.Set(25, 0f);

			barFrame = new UIImage(ModContent.Request<Texture2D>("RemnantOfTheAncientsMod/Common/UI/ChargeBar/GenericChargeBarFrame")); // Frame of our resource bar
			barFrame.Left.Set(0, 0f);//22
			barFrame.Top.Set(0, 0f);
			barFrame.Width.Set(75, 0f);
			barFrame.Height.Set(30, 0f);

			text = new UIText("0/0", 0.8f); // text to show stat
			text.Width.Set(31, 0f);//138
			text.Height.Set(25, 0f);
			text.Top.Set(-20, 0f);
			text.Left.Set(20, 0f);

			gradientA = new Color(176, 176, 176); // A dark purple
			gradientB = new Color(224, 119, 49); // A light purple

			area.Append(text);
			area.Append(barFrame);
			Append(area);
		}

		public override void Draw(SpriteBatch spriteBatch) {
                if (Main.LocalPlayer.HeldItem.stack <= 0 || !Main.LocalPlayer.HeldItem.GetGlobalItem<RemnantGlobalItem>().CanCharge || RemnantPlayer.GenericChargeCouldownMax <= 0 || RemnantPlayer.GenericChargeCouldown == 0)
					return;
            base.Draw(spriteBatch);
        }

        // Here we draw our UI
        protected override void DrawSelf(SpriteBatch spriteBatch) {
           // base.DrawSelf(spriteBatch);

            //var modPlayer = Main.LocalPlayer.GetModPlayer<ExampleResourcePlayer>();
            // Calculate quotient
            float quotient = (float)RemnantPlayer.GenericChargeCouldown / RemnantPlayer.GenericChargeCouldownMax; // Creating a quotient that represents the difference of your currentResource vs your maximumResource, resulting in a float of 0-1f.
			quotient = Utils.Clamp(quotient, 0f, 1f); // Clamping it to 0-1f so it doesn't go over that.

			// Here we get the screen dimensions of the barFrame element, then tweak the resulting rectangle to arrive at a rectangle within the barFrame texture that we will draw the gradient. These values were measured in a drawing program.
			Rectangle hitbox = barFrame.GetInnerDimensions().ToRectangle();
			hitbox.X += 12;
			hitbox.Width -= 24;
			hitbox.Y += 6;//8
			hitbox.Height -= 11;//16

			// Now, using this hitbox, we draw a gradient by drawing vertical lines while slowly interpolating between the 2 colors.
			int left = hitbox.Left;
			int right = hitbox.Right;
			int steps = (int)((right - left) * quotient);
			for (int i = 0; i < steps; i += 1) {
				// float percent = (float)i / steps; // Alternate Gradient Approach
				float percent = (float)i / (right - left);
				Texture2D texture = TextureAssets.MagicPixel.Value;

                spriteBatch.Draw(texture, new Rectangle(left + i, hitbox.Y, 1, hitbox.Height), Color.Lerp(gradientA, gradientB, percent));//TextureAssets.MagicPixel.Value
            }
		}

		public override void Update(GameTime gameTime) {
            if (RemnantPlayer.GenericChargeCouldownMax <= 0 || RemnantPlayer.GenericChargeCouldown <= 0)
                return;
			text.SetText(GenericChargeUISystem.Text.Format(RemnantPlayer.GenericChargeCouldownMax - RemnantPlayer.GenericChargeCouldown,"s"));
			base.Update(gameTime);
		}
	}

    // This class will only be autoloaded/registered if we're not loading on a server
    [Autoload(Side = ModSide.Client)]
	internal class GenericChargeUISystem : ModSystem
	{
		private UserInterface BarUserInterface;

		internal GenericChargeBar CouldownBar;

		public static LocalizedText Text { get; private set; }

		public override void Load() {
			CouldownBar = new();
			BarUserInterface = new();
			BarUserInterface.SetState(CouldownBar);

			string category = "UI.Couldowns";
			Text ??= Mod.GetLocalization($"{category}.CouldownBar");
		}

		public override void UpdateUI(GameTime gameTime) {
			BarUserInterface?.Update(gameTime);
		}

		public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers) {
			int resourceBarIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Resource Bars"));
			if (resourceBarIndex != -1) {
				layers.Insert(resourceBarIndex, new LegacyGameInterfaceLayer(
					"ExampleMod: Example Resource Bar",
					delegate {
						BarUserInterface.Draw(Main.spriteBatch, new GameTime());
						return true;
					},
					InterfaceScaleType.UI)
				);
			}
		}
	}
}
