using CalamityMod;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using RemnantOfTheAncientsMod.Common.ReaperSystem;
using RemnantOfTheAncientsMod.Common.World;
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
        private Asset<Texture2D> reaperSoulParticle;

        private static readonly FieldInfo backgroundField = typeof(UIPanel).GetField("_backgroundTexture",BindingFlags.Instance | BindingFlags.NonPublic);

        public override void Load()
        {
            reaperMainPanelBackground = Mod.Assets.Request<Texture2D>("Common/UI/MainMenu/ReaperMainPanelBackground");
            reaperMainPanelBorder = Mod.Assets.Request<Texture2D>("Common/UI/MainMenu/reaperMainPanelBorder");
            reaperSecondaryPanel = Mod.Assets.Request<Texture2D>("Common/UI/MainMenu/ReperPanelBackground");
            reaperSoulParticle = Mod.Assets.Request<Texture2D>("Common/UI/MainMenu/ReaperSoulParticle");


            On_UIWorldSelect.Draw += DrawWorldSelect;
            On_UIWorldListItem.DrawPanel += DrawReaperPanel;
            On_UIPanel.DrawSelf += DrawParticles;
        }

        public override void Unload()
        {
            On_UIWorldSelect.Draw -= DrawWorldSelect;
            On_UIWorldListItem.DrawPanel -= DrawReaperPanel;
            On_UIPanel.DrawSelf -= DrawParticles;

            reaperMainPanelBackground = null;
            reaperMainPanelBorder = null;
            reaperSecondaryPanel = null;
            reaperSoulParticle = null;
        }

        private void DrawWorldSelect(On_UIWorldSelect.orig_Draw orig, UIWorldSelect self, SpriteBatch spriteBatch)
        {
            self.ExecuteRecursively(element =>
            {
                if (element is not UIWorldListItem world)
                    return;

                if (!world.Data.TryGetHeaderData<Reaper>(out TagCompound tag))
                    return;

                bool moonlordReaperDefeated = false;
                if (world.Data.TryGetHeaderData<RemnantDownedBossSystem>(out TagCompound tagD))
                {
                    moonlordReaperDefeated = tagD.GetBool("downedMoonlordReaper");
                }

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
                world.BorderColor = moonlordReaperDefeated ? new Color(146, 232, 192) : new Color(76, 75, 75);
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

        private void DrawParticles(On_UIPanel.orig_DrawSelf orig, UIPanel self, SpriteBatch spriteBatch)
        {
            // Dibuja el fondo original primero.
            orig(self, spriteBatch);

            if (self is not UIWorldListItem world)
                return;

            if (!world.Data.TryGetHeaderData<Reaper>(
                out TagCompound tag))
                return;

            if (!tag.GetBool("ReaperMode"))
                return;

            var area = world.GetDimensions();
            Texture2D texture = reaperSoulParticle.Value;

            float time = Main.GlobalTimeWrappedHourly;

            for (int i = 0; i < 25; i++)
            {
                // Distribución fija para evitar saltos aleatorios.
                float randomX = ((i * 37) % 101) / 101f;
                float randomY = ((i * 61) % 97) / 97f;

                float speed = 0.08f + (i % 5) * 0.025f;

                // Movimiento ascendente continuo.
                float progress = (randomY + time * speed) % 1f;

                float x = area.X + 10f +
                    randomX * (area.Width - 20f);

                float y = area.Y + area.Height - 10f -
                    progress * (area.Height - 20f);

                // Aparece y desaparece suavemente.
                float opacity = (float)System.Math.Sin(
                    progress * MathHelper.Pi
                ) * 0.6f;

                float scale = 0.4f + (i % 3) * 0.15f;

                int frameCount = 3;
                float animationFPS = 12f;

                int frame = ((int)(Main.GlobalTimeWrappedHourly * animationFPS) + i) % frameCount;

                int frameWidth = texture.Width;// / frameCount;
                int frameHeight = texture.Height / frameCount;


                Rectangle source = new(0, frame * frameHeight, frameWidth, frameHeight);

                spriteBatch.Draw(texture, new Vector2(x, y), source, Color.White * opacity, 0f, new Vector2(frameWidth, frameHeight) / 2f, scale, SpriteEffects.None, 0f);
                //spriteBatch.Draw(texture, new Vector2(x, y), source, Color.White, 0f, new Vector2(frameWidth, frameHeight) / 2f, 1f, SpriteEffects.None, 0f);
            }
        }
    }
}