using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.GameContent.Bestiary;
using Terraria.UI;

/// <summary>
/// Draws the NPC bestiary portrait uniformly scaled and centered.
/// Uses the bestiary icon system with a reduced iconbox to normalize sizes,
/// and falls back to manual sprite drawing if no bestiary entry exists.
/// </summary>
public class UIBossNpcDisplay : UIElement
{
    private readonly int _npcType;
    private readonly BestiaryEntry _bestiaryEntry;
    private const float UniformBoxSize = 100f;

    public UIBossNpcDisplay(NPC npc)
    {
        _npcType = npc.type;
        _bestiaryEntry = Main.BestiaryDB.FindEntryByNPCID(_npcType);
        OverflowHidden = true;
    }

    protected override void DrawSelf(SpriteBatch spriteBatch)
    {
        CalculatedStyle dims = GetDimensions();

        if (_bestiaryEntry != null)
        {
            // Center a fixed-size box inside the element so every NPC renders at the same scale
            int boxX = (int)(dims.X + (dims.Width - UniformBoxSize) / 2f);
            int boxY = (int)(dims.Y + (dims.Height - UniformBoxSize) / 2f);
            Rectangle iconBox = new(boxX, boxY, (int)UniformBoxSize, (int)UniformBoxSize);

            BestiaryUICollectionInfo collectionInfo = _bestiaryEntry.UIInfoProvider.GetEntryUICollectionInfo();

            EntryIconDrawSettings drawSettings = new()
            {
                iconbox = iconBox,
                IsPortrait = true,
                IsHovered = false
            };

            _bestiaryEntry.Icon.Update(collectionInfo, iconBox, drawSettings);
            _bestiaryEntry.Icon.Draw(collectionInfo, spriteBatch, drawSettings);
        }
        else
        {
            // Fallback: draw the NPC texture first frame
            Texture2D npcTexture = TextureAssets.Npc[_npcType].Value;
            int frameCount = Main.npcFrameCount[_npcType];
            int frameHeight = npcTexture.Height / frameCount;
            Rectangle sourceRect = new(0, 0, npcTexture.Width, frameHeight);

            float maxW = dims.Width - 20f;
            float maxH = dims.Height - 10f;

            float fitTarget = UniformBoxSize / System.Math.Max(sourceRect.Width, sourceRect.Height);
            float fitArea = System.Math.Min(maxW / sourceRect.Width, maxH / sourceRect.Height);
            float scale = System.Math.Min(fitTarget, fitArea);

            Vector2 center = new(dims.X + dims.Width / 2f, dims.Y + dims.Height / 2f);
            Vector2 origin = new(sourceRect.Width / 2f, sourceRect.Height / 2f);

            spriteBatch.Draw(npcTexture, center, sourceRect, Color.White, 0f, origin, scale, SpriteEffects.None, 0f);
        }
    }
}