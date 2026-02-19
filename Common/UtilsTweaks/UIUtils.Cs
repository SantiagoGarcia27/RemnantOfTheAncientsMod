using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using RemnantOfTheAncientsMod.Common.Global.NPCs;
using RemnantOfTheAncientsMod.Common.UI.ReaperUI;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.UI;
using static Terraria.UI.UIElement;

namespace RemnantOfTheAncientsMod.Common.UtilsTweaks
{
    public static class UIUtils
    {
        public static UIHoverImageButton CreateButtom(Asset<Texture2D> Texture,Rectangle rec,string text, MouseEvent mouseEvent)
        {
            UIHoverImageButton button = new(Texture, text);

            SetRectangle(button, rec);
            button.OnLeftClick += mouseEvent;
            return button;
        }
        public static UIHoverImageButton CreateButtom(Asset<Texture2D> Texture, float left, float top, float width, float height, string text, MouseEvent mouseEvent)
        {
            UIHoverImageButton button = new(Texture, text);

            SetRectangle(button, left, top, width, height); 
            button.OnLeftClick += mouseEvent;
            return button;
        }
        public static UIHoverImageButton CreateButtom(string Texture, float left, float top, float width, float height, string text, MouseEvent mouseEvent,bool Condition = true, NPC npc = null,bool Reaper = false)
        {
            Asset<Texture2D> TextureBase = ModContent.Request<Texture2D>(Texture);
            UIHoverImageButton button = new(TextureBase, text,npc,true);
            SetButtonActive(button, Condition, npc);
            UIUtils.SetRectangle(button, left, top, width, height);
            button.OnLeftClick += mouseEvent;
            return button;
        }
        public static UIHoverImageButton CreateButtom(Asset<Texture2D> Texture, float left, float top, float width, float height, string text, MouseEvent mouseEvent, bool Condition = true, NPC npc = null, bool Reaper = false)
        {
            UIHoverImageButton button = new(Texture, text, npc, true);
            SetButtonActive(button, Condition, npc);
            SetRectangle(button, left, top, width, height);
            button.OnLeftClick += mouseEvent;
            return button;
        }
        public static UIHoverImageButton CreateButtom(Asset<Texture2D> Texture, Asset<Texture2D> TextureBlocked, float left, float top, float width, float height, string text, MouseEvent mouseEvent, bool Condition = true, NPC npc = null, bool Reaper = false)
        {
            UIHoverImageButton button = new(Texture, text, npc, true);
            SetButtonActive(button, Condition, npc);
            SetRectangle(button, left, top, width, height);
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
        public static void SetPanel(ref DragableUIPanel uiElement, Rectangle rec,Color color)
        {
            SetRectangle(uiElement, rec);
            uiElement.BackgroundColor = color;
        }
        public static void SetPanel(ref DragableUIPanel uiElement, float left, float top, float width, float height, Color color)
        {
            SetRectangle(uiElement, left, top, width, height);
            uiElement.BackgroundColor = color;
        }
        public static void SetPanel(ref UIPanel uiElement, Rectangle rec, Color color)
        {
            SetRectangle(uiElement, rec);
            uiElement.BackgroundColor = color;
        }
        public static void SetPanel(ref UIPanel uiElement, float left, float top, float width, float height, Color color)
        {
            SetRectangle(uiElement, left, top, width, height);
            uiElement.BackgroundColor = color;
        }
        public static void SetButtonActive(UIHoverImageButton button, bool condition, NPC npc)
        {
            button.Blocked(!condition);
            button.Npc(npc);
        }
        public static void SetButtonActive(UIHoverImageButton button,bool condition, Asset<Texture2D> Texture, Asset<Texture2D> TextureGray,NPC npc)
        {
            button.SetImage(Texture);
            button.Blocked(!condition);
            button.Npc(npc);
        }
        public static int GetBossAmmount(Mod mod,List<int> list,List<int> BannedIds = null)
        {
            int iconAmmount = 0;
            foreach (int id in list)
            {
                if (ContentSamples.NpcsByNetId[id].boss && !BannedIds.Contains(id) && RemnantGlobalNPC.GetMod(id) == mod)
                {
                    iconAmmount++;
                }
            }
            return iconAmmount;
        }
    }
}