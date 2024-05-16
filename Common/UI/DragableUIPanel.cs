using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;

namespace RemnantOfTheAncientsMod.Common.UI.ReaperUI
{
	public class DragableUIPanel : UIPanel
	{

		private Vector2 offset;
        private int _cornerSize = 12;
        private int _barSize = 4;
        private bool dragging;

        public Asset<Texture2D> _borderTexture;
        public Asset<Texture2D> _backgroundTexture;

        public new Color BorderColor = Color.White;
        public new Color BackgroundColor = Color.White;
        public override void LeftMouseDown(UIMouseEvent evt) {
			base.LeftMouseDown(evt);
			DragStart(evt);
		}
        public DragableUIPanel(Asset<Texture2D> backgroundTexture, Asset<Texture2D> borderTexture = null)
        {
            SetPadding(_cornerSize);
            _needsTextureLoading = true;
            _borderTexture = borderTexture;
            _backgroundTexture = backgroundTexture;
        }

        public DragableUIPanel()
        {
            SetPadding(_cornerSize);
            _needsTextureLoading = true;
        }
        public override void LeftMouseUp(UIMouseEvent evt) {
			base.LeftMouseUp(evt);
			DragEnd(evt);
		}

		private void DragStart(UIMouseEvent evt) {
			offset = new Vector2(evt.MousePosition.X - Left.Pixels, evt.MousePosition.Y - Top.Pixels);
			dragging = true;
		}

		private void DragEnd(UIMouseEvent evt) {
			Vector2 endMousePosition = evt.MousePosition;
			dragging = false;

			Left.Set(endMousePosition.X - offset.X, 0f);
			Top.Set(endMousePosition.Y - offset.Y, 0f);

			Recalculate();
		}
        private bool _needsTextureLoading;
        public override void Update(GameTime gameTime) {
			base.Update(gameTime);

			if (ContainsPoint(Main.MouseScreen)) {
				Main.LocalPlayer.mouseInterface = true;
			}

			if (dragging) {
				Left.Set(Main.mouseX - offset.X, 0f); 
				Top.Set(Main.mouseY - offset.Y, 0f);
				Recalculate();
			}
			var parentSpace = Parent.GetDimensions().ToRectangle();
			if (!GetDimensions().ToRectangle().Intersects(parentSpace)) {
				Left.Pixels = Utils.Clamp(Left.Pixels, 0, parentSpace.Right - Width.Pixels);
				Top.Pixels = Utils.Clamp(Top.Pixels, 0, parentSpace.Bottom - Height.Pixels);
				Recalculate();
			}
		}
        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            if (_needsTextureLoading)
            {
                _needsTextureLoading = false;
                LoadTextures();
            }

            if (_backgroundTexture != null)
                DrawPanel(spriteBatch, _backgroundTexture.Value, BackgroundColor);

            if (_borderTexture != null)
                DrawPanel(spriteBatch, _borderTexture.Value, BorderColor);
        }
        private void LoadTextures()
        {
            if (_borderTexture == null)
                _borderTexture = Main.Assets.Request<Texture2D>("Images/UI/PanelBorder");

            if (_backgroundTexture == null)
                _backgroundTexture = Main.Assets.Request<Texture2D>("Images/UI/PanelBackground");
        }
        private void DrawPanel(SpriteBatch spriteBatch, Texture2D texture, Color color)
        {
            CalculatedStyle dimensions = GetDimensions();
            Point point = new Point((int)dimensions.X, (int)dimensions.Y);
            Point point2 = new Point(point.X + (int)dimensions.Width - _cornerSize, point.Y + (int)dimensions.Height - _cornerSize);
            int width = point2.X - point.X - _cornerSize;
            int height = point2.Y - point.Y - _cornerSize;
            spriteBatch.Draw(texture, new Rectangle(point.X, point.Y, _cornerSize, _cornerSize), new Rectangle(0, 0, _cornerSize, _cornerSize), color);
            spriteBatch.Draw(texture, new Rectangle(point2.X, point.Y, _cornerSize, _cornerSize), new Rectangle(_cornerSize + _barSize, 0, _cornerSize, _cornerSize), color);
            spriteBatch.Draw(texture, new Rectangle(point.X, point2.Y, _cornerSize, _cornerSize), new Rectangle(0, _cornerSize + _barSize, _cornerSize, _cornerSize), color);
            spriteBatch.Draw(texture, new Rectangle(point2.X, point2.Y, _cornerSize, _cornerSize), new Rectangle(_cornerSize + _barSize, _cornerSize + _barSize, _cornerSize, _cornerSize), color);
            spriteBatch.Draw(texture, new Rectangle(point.X + _cornerSize, point.Y, width, _cornerSize), new Rectangle(_cornerSize, 0, _barSize, _cornerSize), color);
            spriteBatch.Draw(texture, new Rectangle(point.X + _cornerSize, point2.Y, width, _cornerSize), new Rectangle(_cornerSize, _cornerSize + _barSize, _barSize, _cornerSize), color);
            spriteBatch.Draw(texture, new Rectangle(point.X, point.Y + _cornerSize, _cornerSize, height), new Rectangle(0, _cornerSize, _cornerSize, _barSize), color);
            spriteBatch.Draw(texture, new Rectangle(point2.X, point.Y + _cornerSize, _cornerSize, height), new Rectangle(_cornerSize + _barSize, _cornerSize, _cornerSize, _barSize), color);
            spriteBatch.Draw(texture, new Rectangle(point.X + _cornerSize, point.Y + _cornerSize, width, height), new Rectangle(_cornerSize, _cornerSize, _barSize, _barSize), color);
        }
    }
}
