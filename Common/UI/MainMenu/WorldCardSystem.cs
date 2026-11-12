using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using RemnantOfTheAncientsMod.Common.ReaperSystem;
using System.Reflection;
using Terraria;
using Terraria.GameContent;
using Terraria.GameContent.UI.Elements;
using Terraria.GameContent.UI.States;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace RemnantOfTheAncientsMod.Common.UI.MainMenu
{
    public class WorldCardSystem : ModSystem
    {
        private Asset<Texture2D> reaperMainPanelBackground;
        private Asset<Texture2D> reaperMainPanelBorder;
        private Asset<Texture2D> reaperSecondaryPanel;

        private static readonly FieldInfo backgroundField = typeof(UIPanel).GetField("_backgroundTexture",BindingFlags.Instance | BindingFlags.NonPublic);

        public override void Load()
        {
            reaperMainPanelBackground = Mod.Assets.Request<Texture2D>("Common/UI/MainMenu/ReaperMainPanelBackground");
            reaperMainPanelBorder = Mod.Assets.Request<Texture2D>("Common/UI/MainMenu/reaperMainPanelBorder");
            reaperSecondaryPanel = Mod.Assets.Request<Texture2D>("Common/UI/MainMenu/ReperPanelBackground");

            On_UIWorldSelect.Draw += DrawWorldSelect;
            On_UIWorldListItem.DrawPanel += DrawReaperPanel;
        }

        public override void Unload()
        {
            On_UIWorldSelect.Draw -= DrawWorldSelect;
            On_UIWorldListItem.DrawPanel -= DrawReaperPanel;

            reaperMainPanelBackground = null;
            reaperMainPanelBorder = null;
            reaperSecondaryPanel = null;
        }

        private void DrawWorldSelect(On_UIWorldSelect.orig_Draw orig, UIWorldSelect self, SpriteBatch spriteBatch)
        {
            self.ExecuteRecursively(element =>
            {
                if (element is not UIWorldListItem world)
                    return;

                if (!world.Data.TryGetHeaderData<Reaper>(out TagCompound tag))
                    return;

                bool reaperEnabled = tag.GetBool("ReaperMode");

                // Solo para este mundo
                if (!reaperEnabled)
                    return;

                backgroundField?.SetValue(world, reaperMainPanelBackground);

                //world.BackgroundColor = Color.White;

                float pulse = 0.5f + 0.5f * (float)System.Math.Sin(Main.GlobalTimeWrappedHourly * 2f);

                Color DarkerColor = new Color(36, 35, 35);
                Color LighterColor = new Color(66, 65, 65);

                world.BackgroundColor = Color.White;
                //world.BackgroundColor = Color.Lerp(DarkerColor, LighterColor, pulse) * 0.85f;

                //world.BorderColor = Color.Transparent;
                world.BorderColor = new Color(76, 75, 75);
            });

            orig(self, spriteBatch);
        }







        private void DrawReaperPanel(On_UIWorldListItem.orig_DrawPanel orig, UIWorldListItem self, SpriteBatch spriteBatch, Vector2 position, float width)
        {
            if (!self.Data.TryGetHeaderData<Reaper>(out TagCompound tag)  || !tag.GetBool("ReaperMode"))
            {
                orig(self, spriteBatch, position, width);
                return;
            }

            Texture2D texture = reaperSecondaryPanel.Value;

            int slice = texture.Width / 3;
            int height = texture.Height;

            Color color = Color.White;

            // Extremo izquierdo
            spriteBatch.Draw(texture, position, new Rectangle(0, 0, slice, height), color);

            // Centro estirado
            spriteBatch.Draw(texture, position + new Vector2(slice, 0), new Rectangle(slice, 0, slice, height), color, 0f, Vector2.Zero, new Vector2((width - slice * 2f) / slice, 1f), SpriteEffects.None, 0f);

            // Extremo derecho
            spriteBatch.Draw(texture, position + new Vector2(width - slice, 0), new Rectangle(slice * 2, 0, slice, height), color);
        }
    }
}