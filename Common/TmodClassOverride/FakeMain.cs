using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent;
using Terraria.Localization;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ID;

public class FakeMain : ModSystem
{
    public override void Load()
    {
        On_Main.DrawInvasionProgress += DrawInvasionProgress;
    }

    public override void Unload()
    {
        On_Main.DrawInvasionProgress -= DrawInvasionProgress;
    }


    private static int invasionProgress 
    { 
        get => Main.invasionProgress;
        set => Main.invasionProgress = value;
    }

    private static int invasionProgressMode
    {
        get => Main.invasionProgressMode;
        set => Main.invasionProgressMode = value;
    }

    private static bool invasionProgressNearInvasion
    {
        get => Main.invasionProgressNearInvasion;
        set => Main.invasionProgressNearInvasion = value;
    }
    private static int invasionProgressDisplayLeft
    {
        get => Main.invasionProgressDisplayLeft;
        set => Main.invasionProgressDisplayLeft = value;
    }
    private float invasionProgressAlpha
    {
        get => Main.invasionProgressAlpha;
        set => Main.invasionProgressAlpha = value;
    }
    private static int invasionProgressIcon
    {
        get => Main.invasionProgressIcon;
        set => Main.invasionProgressIcon = value;
    }
    private static int invasionProgressWave
    {
        get => Main.invasionProgressWave;
        set => Main.invasionProgressWave = value;
    }

    private static int invasionProgressMax
    {
        get => Main.invasionProgressMax;
        set => Main.invasionProgressMax = value;
    }

    private static int screenWidth
    {
        get => Main.screenWidth;
        set => Main.screenWidth = value;
    }

    private static int screenHeight
    {
        get => Main.screenHeight;
        set => Main.screenHeight = value;
    }

    private static SpriteBatch spriteBatch
    {
        get => Main.spriteBatch;
        set => Main.spriteBatch = value;
    }
    
    private static Texture2D TextureAssets_Extra(int index)
    {
        return TextureAssets.Extra[index].Value;
    }

    private void DrawInvasionProgress(On_Main.orig_DrawInvasionProgress orig)
    {
      
        if (invasionProgress == -1)
            return;

        if (invasionProgressMode == 2 && invasionProgressNearInvasion && invasionProgressDisplayLeft < 160)
            invasionProgressDisplayLeft = 160;

        if (!Main.gamePaused && invasionProgressDisplayLeft > 0)
            invasionProgressDisplayLeft--;

        if (invasionProgressDisplayLeft > 0)
            invasionProgressAlpha += 0.05f;
        else
            invasionProgressAlpha -= 0.05f;

        if (invasionProgressMode == 0)
        {
            invasionProgressDisplayLeft = 0;
            invasionProgressAlpha = 0f;
        }

        if (invasionProgressAlpha < 0f)
            invasionProgressAlpha = 0f;

        if (invasionProgressAlpha > 1f)
            invasionProgressAlpha = 1f;

        if (invasionProgressAlpha <= 0f)
            return;

        float scale = 0.5f + invasionProgressAlpha * 0.5f;
        Texture2D value = TextureAssets.Extra[ExtrasID.EventIconGoblinArmy].Value;
        string text = "";
        Color c = Color.White;

        if (invasionProgressIcon == 1)
        {
            value = TextureAssets.Extra[ExtrasID.EventIconFrostMoon].Value;
            text = Lang.inter[83].Value;
            c = new Color(64, 109, 164) * 0.5f;
        }
        else if (invasionProgressIcon == 2)
        {
            value = TextureAssets.Extra[ExtrasID.EventIconPumpkinMoon].Value;
            text = Lang.inter[84].Value;
            c = new Color(112, 86, 114) * 0.5f;
        }
        else if (invasionProgressIcon == 3)
        {
            value = TextureAssets.Extra[ExtrasID.EventIconOldOnesArmy].Value;
            text = Language.GetTextValue("DungeonDefenders2.InvasionProgressTitle");
            c = new Color(88, 0, 160) * 0.5f;
        }
        else if (invasionProgressIcon == 7)
        {
            value = TextureAssets.Extra[ExtrasID.EventIconMartianMadness].Value;
            text = Lang.inter[85].Value;
            c = new Color(165, 160, 155) * 0.5f;
        }
        else if (invasionProgressIcon == 6)
        {
            value = TextureAssets.Extra[ExtrasID.EventIconPirateInvasion].Value;
            text = Lang.inter[86].Value;
            c = new Color(148, 122, 72) * 0.5f;
        }
        else if (invasionProgressIcon == 5)
        {
            value = TextureAssets.Extra[ExtrasID.EventIconSnowLegion].Value;
            text = Lang.inter[87].Value;
            c = new Color(173, 135, 140) * 0.5f;
        }
        else if (invasionProgressIcon == 4)
        {
            value = TextureAssets.Extra[ExtrasID.EventIconGoblinArmy].Value;
            text = Lang.inter[88].Value;
            c = new Color(94, 72, 131) * 0.5f;
        }

        int barWidth = (int)(200f * scale);
        int barHeight = (int)(45f * scale);

        Vector2 barPosition = new Vector2(screenWidth - 120, screenHeight - 40);

        Utils.DrawInvBG(R: new Rectangle((int)barPosition.X - barWidth / 2, (int)barPosition.Y - barHeight / 2, barWidth, barHeight), sb: spriteBatch, c: new Color(63, 65, 151, 255) * 0.785f);

        float progressRatio = MathHelper.Clamp((float)invasionProgress / (float)invasionProgressMax, 0f, 1f);

        float progressBarWidth = 169f * scale;
        float progressBarHeight = 8f * scale;

        Vector2 progressBarSize = new Vector2(progressBarWidth, progressBarHeight);
        Vector2 progressPosition = barPosition + Vector2.UnitY * progressBarHeight + Vector2.UnitX * 1f;

        Texture2D barTexture = TextureAssets.ColorBar.Value;

        if (invasionProgressWave > 0)
        {
            string progressText = Language.GetTextValue(arg1: (invasionProgressMax != 0) ? 
                ((int)((float)invasionProgress * 100f / (float)invasionProgressMax) + "%") : 
                Language.GetTextValue("Game.InvasionPoints", invasionProgress), key: "Game.WaveMessage", arg0: invasionProgressWave);

            if (invasionProgressMax == 0) progressRatio = 1f;

            Utils.DrawBorderString(spriteBatch, progressText, progressPosition, Color.White * invasionProgressAlpha, scale, 0.5f, 1f);

            spriteBatch.Draw(barTexture, barPosition, null, Color.White * invasionProgressAlpha, 0f, new Vector2(barTexture.Width / 2, 0f), scale, SpriteEffects.None, 0f);

            progressPosition += Vector2.UnitX * (progressRatio - 0.5f) * progressBarWidth;

            DrawProgressBar(progressPosition, progressBarSize, progressRatio);
        }
        else
        {
            string progressText = ((invasionProgressMax != 0) ? 
                ((int)((float)invasionProgress * 100f / (float)invasionProgressMax) + "%") : 
                invasionProgress.ToString());

            progressText = Language.GetTextValue("Game.WaveCleared", progressText);

            if (invasionProgressMax != 0)
            {
                spriteBatch.Draw(barTexture, barPosition, null, Color.White * invasionProgressAlpha, 0f, new Vector2(barTexture.Width / 2, 0f), scale, SpriteEffects.None, 0f);
               
                Vector2 progressTextSize = FontAssets.MouseText.Value.MeasureString(progressText);
                float progressTextScale = scale;

                if (progressTextSize.Y > 22f) progressTextScale *= 22f / progressTextSize.Y;

                Utils.DrawBorderString(spriteBatch, progressText, progressPosition + new Vector2(0f, -4f), Color.White * invasionProgressAlpha, progressTextScale, 0.5f, 1f);
                progressPosition += Vector2.UnitX * (progressRatio - 0.5f) * progressBarWidth;

                DrawProgressBar(progressPosition, progressBarSize, progressRatio);
            }
        }

        Vector2 vector6 = FontAssets.MouseText.Value.MeasureString(text);
        float num13 = 120f;

        if (vector6.X > 200f)
            num13 += vector6.X - 200f;

        Rectangle r3 = Utils.CenteredRectangle(new Vector2(screenWidth - num13, screenHeight - 80), (vector6 + new Vector2(value.Width + 12, 6f)) * scale);
        Utils.DrawInvBG(spriteBatch, r3, c);
        spriteBatch.Draw(value, r3.Left() + Vector2.UnitX * scale * 8f, null, Color.White * invasionProgressAlpha, 0f, new Vector2(0f, value.Height / 2), scale * 0.8f, SpriteEffects.None, 0f);
        Utils.DrawBorderString(spriteBatch, text, r3.Right() + Vector2.UnitX * scale * -22f, Color.White * invasionProgressAlpha, scale * 0.9f, 1f, 0.4f);
    }

    private void DrawProgressBar(Vector2 progressPosition, Vector2 progressBarSize, float progressRatio)
    {
        spriteBatch.Draw(TextureAssets.MagicPixel.Value, progressPosition, new Rectangle(0, 0, 1, 1), new Color(255, 241, 51) * invasionProgressAlpha, 0f, new Vector2(1f, 0.5f), new Vector2(progressBarSize.X * progressRatio, progressBarSize.Y), SpriteEffects.None, 0f);
        spriteBatch.Draw(TextureAssets.MagicPixel.Value, progressPosition, new Rectangle(0, 0, 1, 1), new Color(255, 165, 0, 127) * invasionProgressAlpha, 0f, new Vector2(1f, 0.5f), new Vector2(2f, progressBarSize.Y), SpriteEffects.None, 0f);
        spriteBatch.Draw(TextureAssets.MagicPixel.Value, progressPosition, new Rectangle(0, 0, 1, 1), Color.Black * invasionProgressAlpha, 0f, new Vector2(0f, 0.5f), new Vector2(progressBarSize.X * (1f - progressRatio), progressBarSize.Y), SpriteEffects.None, 0f);
    }

}