using CalamityMod.Items.Weapons.Ranged;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.Graphics.Effects;
using Terraria.ID;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Common.Menu
{
    public class RemnantModMenu : ModMenu
    {
        private const string menuAssetPath = "RemnantOfTheAncientsMod/Common/Menu"; 

        public override Asset<Texture2D> Logo => ModContent.Request<Texture2D>($"{menuAssetPath}/Logo");

        public override Asset<Texture2D> SunTexture => ModContent.Request<Texture2D>(RemnantOfTheAncientsMod.PlaceHolderPath);

        public override Asset<Texture2D> MoonTexture => ModContent.Request<Texture2D>(RemnantOfTheAncientsMod.PlaceHolderPath);

     //  public override int Music => MusicLoader.GetMusicSlot(Mod, "Content/Sounds/Music/Menu");

        public override ModSurfaceBackgroundStyle MenuBackgroundStyle => ModContent.GetInstance<MenuBackground>();

        public override string DisplayName => "Remnant Of The Ancients Style";

        public override int Music => SetMusic();

        private Asset<Texture2D> skyTexture;

        public override void Load()
        {
            skyTexture = Mod.Assets.Request<Texture2D>("Common/Background/GalearisSky", AssetRequestMode.ImmediateLoad);

            On_Main.DrawStarsInBackground += DrawRemnantSky;
        }

        public override void Unload()
        {
            On_Main.DrawStarsInBackground -= DrawRemnantSky;
        }

        private static int SetMusic()
        {
            int result;
            if (RemnantOfTheAncientsMod.RemnantOfTheAncientsMusic != null)
                result = MusicLoader.GetMusicSlot(RemnantOfTheAncientsMod.RemnantOfTheAncientsMusic, "Content/Music/Menu");
            else result =  MusicID.MenuMusic;
            return result; 
        }
        private const string SkyKey = "RemnantOfTheAncientsMod:GalearisSky";
        public override void OnSelected()
        {
            SoundEngine.PlaySound(SoundID.Chat); // Plays a thunder sound when this ModMenu is selected

            if (SkyManager.Instance[SkyKey] == null)
            {
                SkyManager.Instance[SkyKey] = new GalearisSky();
            }

            SkyManager.Instance.Activate(SkyKey);

            Mod.Logger.Info(
                $"RemnantSky registrado: {SkyManager.Instance[SkyKey] != null}"
            );

            Mod.Logger.Info(
                $"RemnantSky activo: {SkyManager.Instance[SkyKey].IsActive()}"
            );
        }
        public override void OnDeselected()
        {
            if (SkyManager.Instance[SkyKey] != null)
            {
                SkyManager.Instance.Deactivate(SkyKey);
            }
        }
        public override bool PreDrawLogo(SpriteBatch spriteBatch, ref Vector2 logoDrawCenter, ref float logoRotation, ref float logoScale, ref Color drawColor)
        {
            Texture2D texture = ModContent.Request<Texture2D>($"RemnantOfTheAncientsMod/Common/Menu/Background", (AssetRequestMode)2).Value;
            Vector2 drawOffset = Vector2.Zero;
            Vector2 Scale = new((float)(double)Main.screenWidth / texture.Width, (float)(double)Main.screenHeight / texture.Height);           
            float scale = Scale.X;
            if (Scale.X != Scale.Y)
            {
                if (Scale.Y > Scale.X)
                {
                    scale = Scale.Y;
                    drawOffset.X -= (texture.Width * scale - Main.screenWidth) * 0.5f;
                }
                else
                {
                    drawOffset.Y -= (texture.Height * scale - Main.screenHeight) * 0.5f;
                }
            }
            //if(scale == 0)
            //{
            //    scale = 0.1f;
            //    // scale = 0.417f;
            //}
            spriteBatch.Draw(texture, drawOffset, null, Color.White, 0f, Vector2.Zero, scale, 0, 0f);
            drawColor = Color.White;
            return true;
        }

        private void DrawRemnantSky(
       On_Main.orig_DrawStarsInBackground orig,
       Main self,
       Main.SceneArea sceneArea,
       bool artificial)
        {
            if (Main.gameMenu && IsSelected && !artificial)
            {
                Main.spriteBatch.Draw(
                    skyTexture.Value,
                    new Rectangle(
                        0,
                        0,
                        Main.screenWidth,
                        Main.screenHeight
                    ),
                    Color.White
                );
            }

            orig(self, sceneArea, artificial);
        }
    }
}