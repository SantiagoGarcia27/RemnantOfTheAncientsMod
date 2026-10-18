using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using RemnantOfTheAncientsMod.Common.ReaperSystem.ReaperUI;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.UI;

public class UISoulDisplay : UIElement
{
    public UISoulDisplay()
    {
    }

    public static void UpdateButtons(int npcId, Mod mod)
    {
        if (!Main.gameMenu)
        {
            //if (!BannedIds.Contains(npcId))
            {
                NPC npc = ContentSamples.NpcsByNetId[npcId];
                Asset<Texture2D> TextureBase = ReaperSoulUIExtras.GetBossHead(npc);

               // VerifyButtomsIndex(mod);

                bool playerHaveSoul = ReaperSoulUIExtras.SelectList(mod, npcId);
                float playerHaveSoulActive = ReaperSoulUIExtras.SelectActiveList(mod, npcId);

                if (ReaperSoulsUIState.Buttons.TryGetValue(mod, out var modButtons) && modButtons.TryGetValue(npc.type, out var btn) && btn != null)
                {
                    btn.SetImage(TextureBase);
                    if (playerHaveSoul)
                    {
                        btn.Blocked(false);
                        btn.Active(playerHaveSoulActive > 0);
                    }
                    else
                    {
                        btn.Blocked(true);
                        btn.Active(false);
                    }
                }

                // Refresh detail panel if the selected boss state changed
                if (ReaperSoulsUIState.SelectedNpcType == npcId)
                    ReaperSoulsUIState.RefreshDetailPanel();
            }
        }
    }

    protected override void DrawSelf(SpriteBatch spriteBatch)
    {
        CalculatedStyle innerDimensions = GetInnerDimensions();
        //DragableUIPanel bgPanel = BackgroundPanel;

        //if (bgPanel != null)
        {
            string language = LanguageManager.Instance.ActiveCulture.Name;
            string path = "RemnantOfTheAncientsMod/Common/UI/ReaperUI/";

            float centerX = innerDimensions.X + innerDimensions.Width / 2f;
            float centerY = innerDimensions.Y + innerDimensions.Height / 2f;

            ModContent.RequestIfExists(path + "TileBackground", out Asset<Texture2D> backgroundTexture);
            ModContent.RequestIfExists(path + "SoulsText_" + language, out Asset<Texture2D> TitleTexture);

            if (backgroundTexture != null && backgroundTexture.Value != null)
            {
                float texW = backgroundTexture.Value.Width;
                float texH = backgroundTexture.Value.Height;
                float scaleW = (innerDimensions.Width - 10f) / texW;
                float scaleH = innerDimensions.Height / texH;
                float scale = System.Math.Min(scaleW, scaleH);

                Vector2 origin = backgroundTexture.Size() / 2f;
                spriteBatch.Draw(backgroundTexture.Value, new Vector2(centerX, centerY), null, Color.White, 0f, origin, scale, SpriteEffects.None, 0f);
            }

            if (TitleTexture != null && TitleTexture.Value != null)
            {
                float texW = TitleTexture.Value.Width;
                float texH = TitleTexture.Value.Height;
                float scaleW = (innerDimensions.Width - 20f) / texW;
                float scaleH = (innerDimensions.Height - 8f) / texH;
                float scale = System.Math.Min(System.Math.Min(scaleW, scaleH), 1f);

                Vector2 origin = TitleTexture.Size() / 2f;
                spriteBatch.Draw(TitleTexture.Value, new Vector2(centerX, centerY), null, Color.White, 0f, origin, scale, SpriteEffects.None, 0f);
            }
        }
    }
}
