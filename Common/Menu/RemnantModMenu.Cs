using Terraria;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.ModLoader;
using Terraria.Audio;
using Terraria.ID;

namespace RemnantOfTheAncientsMod.Common.Menu
{
    public class RemnantModMenu : ModMenu
    {
        private const string menuAssetPath = "RemnantOfTheAncientsMod/Common/Menu"; 

        public override Asset<Texture2D> Logo => ModContent.Request<Texture2D>($"{menuAssetPath}/Logo");

        public override Asset<Texture2D> SunTexture => ModContent.Request<Texture2D>($"{menuAssetPath}/PlaceHolder");

        public override Asset<Texture2D> MoonTexture => ModContent.Request<Texture2D>($"{menuAssetPath}/PlaceHolder");

       // public override int Music => MusicLoader.GetMusicSlot(Mod, "Assets/Music/MysteriousMystery");

        public override ModSurfaceBackgroundStyle MenuBackgroundStyle => ModContent.GetInstance<MenuBackground>();

        public override string DisplayName => "Remnant Of The Ancients Style";

        public override void OnSelected()
        {
            SoundEngine.PlaySound(SoundID.Thunder); // Plays a thunder sound when this ModMenu is selected
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
            return true;
        }
    }
}