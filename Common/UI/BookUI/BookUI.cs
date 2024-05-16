using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using RemnantOfTheAncientsMod.Common.UtilsTweaks;
using RemnantOfTheAncientsMod.Content.Items.Accesories;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.GameContent.UI.Elements;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.UI;

namespace RemnantOfTheAncientsMod.Common.UI.ReaperUI
{
    internal class BookUIState : UIState
	{
		public DragableUIPanel BookPanel;	
        public UIBookDisplay BookDisplay;

        public override void OnInitialize()
		{
            Asset<Texture2D> BackgroundTexture = ModContent.GetInstance<RemnantOfTheAncientsMod>().Assets.Request<Texture2D>("Common/UI/BookUI/PanelBackground");
            BookPanel = new DragableUIPanel(BackgroundTexture);
            BookPanel.SetPadding(0);

			UIUtils.SetRectangle(BookPanel, left: 400f, top: 0f, width: Main.screenWidth / 2, height: Main.screenHeight / 2);//left: 400f, top: 100f, width: 170f, height: 70f
           //BookPanel.BackgroundColor = new Color(214, 214, 212);
			
			BookDisplay = new UIBookDisplay();
			UIUtils.SetRectangle(BookDisplay, 15f, 20f, 100f, 40f);
            BookPanel.Append(BookDisplay);

			Append(BookPanel);
		}
	}

	public class UIBookDisplay : UIElement
	{
		public UIBookDisplay()
		{
		}
		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			CalculatedStyle innerDimensions = GetInnerDimensions();
			float shopx = innerDimensions.X;
			float shopy = innerDimensions.Y;

			Vector2 titlePos = new(shopx + 24 * 7, shopy + 25f);
			Utils.DrawBorderStringFourWay(spriteBatch, FontAssets.ItemStack.Value, Language.GetTextValue("Mods.RemnantOfTheAncientsMod.UI.AscendedReforgesTitle"), titlePos.X, titlePos.Y, Color.White, Color.Black, new Vector2(0.3f), 2.75f);
            titlePos = new(shopx , shopy + 25f * 7);
            Utils.DrawBorderStringFourWay(spriteBatch, FontAssets.ItemStack.Value, Language.GetTextValue("Mods.RemnantOfTheAncientsMod.UI.AscendedReforgesText"), titlePos.X, titlePos.Y, Color.DarkGray, Color.Black, new Vector2(0.3f), 1f);
            //DrawTitle(spriteBatch, titlePos);
        }	
	}
}
