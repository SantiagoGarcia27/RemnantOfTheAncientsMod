using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using RemnantOfTheAncientsMod.Common.UI.ReaperUI;
using System.Configuration;
using System.Drawing;
using Terraria;
using Terraria.ModLoader;
using Terraria.UI;
using static Terraria.UI.UIElement;

namespace RemnantOfTheAncientsMod.Common.UtilsTweaks
{
    public static class UIUtils
    {
        public static UIHoverImageButton CreateButtom(Asset<Texture2D> Texture,Rectangle rec,string text, MouseEvent mouseEvent)
        {
            UIHoverImageButton button = new UIHoverImageButton(Texture, text);
            
            UIUtils.SetRectangle(button, rec);
            button.OnLeftClick += mouseEvent;
            return button;
        }
        public static UIHoverImageButton CreateButtom(Asset<Texture2D> Texture, float left, float top, float width, float height, string text, MouseEvent mouseEvent)
        {
            UIHoverImageButton button = new UIHoverImageButton(Texture, text);

            UIUtils.SetRectangle(button, left, top, width, height); 
            button.OnLeftClick += mouseEvent;
            return button;
        }
        public static UIHoverImageButton CreateButtom(string Texture, float left, float top, float width, float height, string text, MouseEvent mouseEvent,bool Condition = true, int i = -1,bool Reaper = false)
        {
            Asset<Texture2D> TextureBase = ModContent.Request<Texture2D>(Texture);
            Asset<Texture2D> TextureGray = ModContent.Request<Texture2D>(Texture + "_Blocked");
            Player player = Main.player[Main.myPlayer];
            UIHoverImageButton button = new UIHoverImageButton(TextureBase, text,i,true);

            //if(i != -1 && Reaper)
            //{
            //    Condition = player.GetModPlayer<ReaperPlayer>().SoulsUpgrades[i];
            //}
            SetButtonActive(button, Condition, TextureBase, TextureGray);
            UIUtils.SetRectangle(button, left, top, width, height);
            button.OnLeftClick += mouseEvent;
            return button;
        }
        public static void SetRectangle(UIElement uiElement, float left, float top, float width, float height)
        {
            uiElement.Left.Set(left, 0f);
            uiElement.Top.Set(top, 0f);
            uiElement.Width.Set(width, 0f);
            uiElement.Height.Set(height, 0f);
        }
        public static void SetRectangle(UIElement uiElement, Rectangle rec)
        {
            uiElement.Left.Set(rec.Left, 0f);
            uiElement.Top.Set(rec.Top, 0f);
            uiElement.Width.Set(rec.Width, 0f);
            uiElement.Height.Set(rec.Height, 0f);
        }
        public static void SetButtonActive(UIHoverImageButton button,bool condition, Asset<Texture2D> Texture, Asset<Texture2D> TextureGray)
        {
            if (condition)
            {
                button.SetImage(Texture);
                button.Blocked(false);
            }
            else
            { 
                button.SetImage(TextureGray);
                button.Blocked(true);
            }
        }

    }
}