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
    internal class ReaperSoulsUIState : UIState
	{
		public DragableUIPanel CoinCounterPanel;
		public UIPanel SoulTogglePanel;
		public UISoulDisplay MoneyDisplay;
		public static List<UIHoverImageButton> Buttons = new List<UIHoverImageButton>()
		{
			null,null,null,null,null,null,null,null,null,null,
			null,null,null,null,null,null,null,null,null,null,null
		};

		public override void OnInitialize()
		{
			float baseLefthPosition = 80f;
			float baseTopPosition = 20f;

			float baseLefthPositionIncrement = 40;
			float baseTopPositionIncrement = 40;

			CoinCounterPanel = new DragableUIPanel();
			CoinCounterPanel.SetPadding(0);

			UIUtils.SetRectangle(CoinCounterPanel, left: 400f, top: 0f, width: Main.screenWidth / 3, height: Main.screenHeight / 3);//left: 400f, top: 100f, width: 170f, height: 70f
			CoinCounterPanel.BackgroundColor = new Color(73, 94, 171);

			SoulTogglePanel = new UIPanel();
			SoulTogglePanel.SetPadding(0);

			UIUtils.SetRectangle(SoulTogglePanel, left: 0f, top: 120f, width: CoinCounterPanel.Width.GetValue(1), height: CoinCounterPanel.Height.GetValue(1) / 2f);
			SoulTogglePanel.BackgroundColor = new Color(73, 94, 171);
			CoinCounterPanel.Append(SoulTogglePanel);

			Player player = Main.player[Main.myPlayer];

			for (int i = 0; i <= 20; i++)
			{
				Buttons[i] = UIUtils.CreateButtom($"RemnantOfTheAncientsMod/Common/UI/ReaperUI/Textures/NPC_Head_Boss_{i}", baseLefthPosition, baseTopPosition, 22f, 22f, "", new MouseEvent(SetSoulClicked), false, i, true);
				SoulTogglePanel.Append(Buttons[i]);
				if (i == 5)
				{
					baseLefthPosition += baseLefthPositionIncrement + 20;
				}
				else if (i == 7)
				{
					baseLefthPosition = 110;
					baseTopPosition += baseTopPositionIncrement;
				}
				else if (i == 14)
				{
					baseLefthPosition = 120;
					baseTopPosition += baseTopPositionIncrement;
				}
				else if (i == 15)
				{
					baseLefthPosition += baseLefthPositionIncrement + 10;
				}
				else
				{
					baseLefthPosition += baseLefthPositionIncrement;
				}
			}
			MoneyDisplay = new UISoulDisplay();
			UIUtils.SetRectangle(MoneyDisplay, 15f, 20f, 100f, 40f);
			CoinCounterPanel.Append(MoneyDisplay);

			Append(CoinCounterPanel);
		}
		public static void SetSoulClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			Player player = Main.player[Main.myPlayer];
			SoundEngine.PlaySound(SoundID.MenuOpen);
		}
	}

	public class UISoulDisplay : UIElement
	{
		public UISoulDisplay()
		{
		}
		public static void UpdateButtons(int i)
		{
			Player player = Main.player[Main.myPlayer];
			string Texture = $"RemnantOfTheAncientsMod/Common/UI/ReaperUI/Textures/NPC_Head_Boss_{i}";
			Asset<Texture2D> TextureBase = ModContent.Request<Texture2D>(Texture);
			Asset<Texture2D> TextureGray = ModContent.Request<Texture2D>(Texture + "_Blocked");

			if (player.GetModPlayer<ReaperPlayer>().SoulsUpgrades[i])
			{
				ReaperSoulsUIState.Buttons[i].SetImage(TextureBase);
			}
			else
			{
				ReaperSoulsUIState.Buttons[i].SetImage(TextureGray);
			}
		}

		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			CalculatedStyle innerDimensions = GetInnerDimensions();
			float shopx = innerDimensions.X;
			float shopy = innerDimensions.Y;

			Vector2 titlePos = new(shopx + (float)(24 * 7), shopy + 25f);
			Utils.DrawBorderStringFourWay(spriteBatch, FontAssets.ItemStack.Value, Language.GetTextValue("Mods.RemnantOfTheAncientsMod.UI.ReaperTitle"), titlePos.X, titlePos.Y, Color.White, Color.Black, new Vector2(0.3f), 3.75f);
			DrawTitle(spriteBatch, titlePos);
		}
		private void DrawTitle(SpriteBatch spriteBatch, Vector2 TitlePos)
		{
			for (int j = -1; j < 2; j += 2)
			{
				Main.instance.LoadItem(ModContent.ItemType<ReaperChalice>());
				Texture2D texture = TextureAssets.Item[ModContent.ItemType<ReaperChalice>()].Value;
				spriteBatch.Draw(texture, TitlePos + new Vector2((150 * Utils1.GetSign(j)) + 80, 0), null, Color.White, 0f, texture.Size() / 2f, 3.5f, SpriteEffects.None, 0f);
			}
		}

	}

	public class CheckSouls : ModPlayer
	{
		public override void PostUpdate()
		{
			Player player = Main.player[Main.myPlayer];

			for (int i = 0; i < player.GetModPlayer<ReaperPlayer>().SoulsUpgrades.Count; i++)
			{
				UISoulDisplay.UpdateButtons(i);
			}
			base.PostUpdate();
		}
	}
}
