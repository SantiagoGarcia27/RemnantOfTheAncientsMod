using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RemnantOfTheAncientsMod.Common.Global.Items;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent;
using Terraria.GameContent.UI.Elements;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.UI;

namespace RemnantOfTheAncientsMod.Common.UI.ChargeBar
{ 
    internal class GenericChargeBar : UIState
	{
		private UIText text;
		private UIElement area;
		private UIImage barFrame;
		private Color gradientA;
		private Color gradientB;

		public override void OnInitialize() {
            area = new UIElement();
            area.Left.Set(0, 0f);
            area.Top.Set(0, 0f);
            area.Width.Set(81, 0f);
            area.Height.Set(25, 0f);

            barFrame = new UIImage(ModContent.Request<Texture2D>("RemnantOfTheAncientsMod/Common/UI/ChargeBar/GenericChargeBarFrame")); // Frame of our resource bar
            barFrame.Left.Set(0, 0f);//22
            barFrame.Top.Set(0, 0f);
            barFrame.Width.Set(75, 0f);
            barFrame.Height.Set(30, 0f);

            text = new UIText("0/0", 0.8f); // text to show stat
            text.Width.Set(31, 0f);//138
            text.Height.Set(25, 0f);
            text.Top.Set(-22, 0f);
            text.Left.Set(20, 0f);

            gradientA = new Color(176, 176, 176); // A dark purple
            gradientB = new Color(224, 119, 49); // A light purple

            area.Append(barFrame);
            area.Append(text);
            Append(area);
        }

		public override void Draw(SpriteBatch spriteBatch)
		{
			RemnantPlayer remnantPlayer = Main.LocalPlayer.GetModPlayer<RemnantPlayer>();
            Item item = Main.LocalPlayer.HeldItem;
			if (item.IsAir)
			{
				return;
			}
			int stack = item.stack;
			bool canCharge = Main.LocalPlayer.HeldItem.GetGlobalItem<RemnantGlobalItem>().CanCharge;
			float CouldownMax = remnantPlayer.GenericChargeCouldownMax;
			float Couldown = remnantPlayer.GenericChargeCouldown;

			if (stack <= 0 || !canCharge || CouldownMax <= 0 || Couldown == 0)
				return;

			base.Draw(spriteBatch);
		}

        // Here we draw our UI
        protected override void DrawSelf(SpriteBatch spriteBatch) {
            RemnantPlayer remnantPlayer = Main.LocalPlayer.GetModPlayer<RemnantPlayer>();
            float quotient = (float)remnantPlayer.GenericChargeCouldown / remnantPlayer.GenericChargeCouldownMax; 
			quotient = Utils.Clamp(quotient, 0f, 1f);

			Rectangle hitbox = barFrame.GetInnerDimensions().ToRectangle();
			hitbox.X += 12;
			hitbox.Width -= 24;
			hitbox.Y += 6;
			hitbox.Height -= 11;

			int left = hitbox.Left;
			int right = hitbox.Right;
			int steps = (int)((right - left) * quotient);
			for (int i = 0; i < steps; i += 1) {
				float percent = (float)i / (right - left);
				Texture2D texture = TextureAssets.MagicPixel.Value;

                spriteBatch.Draw(texture, new Rectangle(left + i, hitbox.Y, 1, hitbox.Height), Color.Lerp(gradientA, gradientB, percent));//TextureAssets.MagicPixel.Value
            }
		}

		public override void Update(GameTime gameTime) {
			RemnantPlayer remnantPlayer = Main.LocalPlayer.GetModPlayer<RemnantPlayer>();
            Player player = Main.LocalPlayer;

			Vector2 screenPos = (player.position - Main.screenPosition) / Main.UIScale;
			float barX = screenPos.X + (player.width / 2f / Main.UIScale) - (area.Width.Pixels / 2f);
			float barY = screenPos.Y - 60;
			area.Left.Set(barX, 0f);
			area.Top.Set(barY, 0f);

			if (remnantPlayer.GenericChargeCouldownMax > 0 && remnantPlayer.GenericChargeCouldown > 0)
			{
				text.SetText(GenericChargeUISystem.Text.Format(remnantPlayer.GenericChargeCouldownMax - remnantPlayer.GenericChargeCouldown,"s"));
			}
			base.Update(gameTime);
		}
	}

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
					"RemantOfTheAncients: Charge Bar",
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
