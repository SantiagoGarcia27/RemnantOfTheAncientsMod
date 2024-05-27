using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.ModLoader;
using Terraria.UI;
using Terraria.GameContent;
using System.Collections.Generic;
using Terraria.Localization;
using Terraria.ID;
using RemnantOfTheAncientsMod.Common.UtilsTweaks;

namespace RemnantOfTheAncientsMod.Common.UI.ChargeBar
{

    internal class GenericAmmoBar : UIState
	{
		
		private UIText textAmmo;
		private UIElement area;
		private UIImage barFrame;
		private Color gradientA;
		private Color gradientB;

		public override void OnInitialize() {
			
			area = new UIElement();
			area.Left.Set(-area.Width.Pixels - (600 * 1.5f) +60, 1f);
            area.Top.Set(400, 0f); 
			area.Width.Set(81, 0f); 
			area.Height.Set(25, 0f);

            barFrame = new UIImage(ModContent.Request<Texture2D>("RemnantOfTheAncientsMod/Common/UI/ChargeBar/GenericChargeBarFrame")); // Frame of our resource bar
            barFrame.Left.Set(0, 0f);//22
            barFrame.Top.Set(0, 0f);
            barFrame.Width.Set(75, 0f);
            barFrame.Height.Set(30, 0f);

            textAmmo = new UIText("0/0", 0.8f); // text to show stat
            textAmmo.Width.Set(31, 0f);//138
            textAmmo.Height.Set(25, 0f);
            textAmmo.Top.Set(-20, 0f);
            textAmmo.Left.Set(20, 0f);

			gradientA = new Color(176, 176, 176); // A dark purple
			gradientB = new Color(224, 119, 49); // A light purple

			area.Append(textAmmo);
			area.Append(barFrame);
			Append(area);
		}

		public override void Draw(SpriteBatch spriteBatch) {
                if (Main.LocalPlayer.HeldItem.stack <= 0 || RemnantPlayer.GenericAmmoAmmountMax <= 0 || RemnantPlayer.GenericAmmoAmmount == RemnantPlayer.GenericAmmoAmmountMax)
					return;
            base.Draw(spriteBatch);
        }

		// Here we draw our UI
		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			base.DrawSelf(spriteBatch);

			float quotient = (float)RemnantPlayer.GenericAmmoAmmount / RemnantPlayer.GenericAmmoAmmountMax; 
			quotient = Utils.Clamp(quotient, 0f, 1f); 

			Rectangle hitbox = barFrame.GetInnerDimensions().ToRectangle();
			hitbox.X += 8;
			hitbox.Width -= 16;
			hitbox.Y += 3;
			hitbox.Height -= 12;

			int left = hitbox.Left;
			int right = hitbox.Right;
			int steps = (int)((right - left) * quotient);
			int id = ItemID.HighVelocityBullet;
            Main.instance.LoadItem(id);
			Texture2D texture = TextureAssets.Item[id].Value;
			for (int i = 0; i < steps; i += 1)
			{
				float percent = (float)i / (right - left);
				spriteBatch.Draw(texture, new Rectangle(left + i, hitbox.Y, 1, hitbox.Height), Color.Lerp(gradientA, gradientB, percent));
			}

		}

		public override void Update(GameTime gameTime) {
            if (RemnantPlayer.GenericAmmoAmmountMax <= 0)
                return;
			Player player = Main.LocalPlayer;

            Item item = null;
			int index = Utils1.SearchPlayerAmmoSlot(player, player.HeldItem.useAmmo,ref item);

            textAmmo.SetText(GenericAmmoCouldownUISystem.Text.Format(RemnantPlayer.GenericAmmoAmmount + "/" + RemnantPlayer.GenericAmmoAmmountMax + $"[I:{item.type}]"));
            base.Update(gameTime);
		}
	}

    [Autoload(Side = ModSide.Client)]
	internal class GenericAmmoCouldownUISystem : ModSystem
	{
		private UserInterface BarUserInterface;

		internal GenericAmmoBar CouldownBar;

		public static LocalizedText Text { get; private set; }

		public override void Load() {
			CouldownBar = new();
			BarUserInterface = new();
			BarUserInterface.SetState(CouldownBar);

			string category = "UI.Couldowns";
			Text ??= Mod.GetLocalization($"{category}.AmmoBar");
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
