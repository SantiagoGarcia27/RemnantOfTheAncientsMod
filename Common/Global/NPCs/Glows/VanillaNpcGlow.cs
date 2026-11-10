using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ModLoader;

public class VanillaNpcGlow : GlobalNPC
{
    public override void PostDraw(NPC npc, SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
    {
        if (npc.ModNPC != null) return;

        string TextureGlowPath = $"RemnantOfTheAncientsMod/Common/Global/NPCs/Glows/{npc.type}_Glow";
 
        if (ModContent.RequestIfExists<Texture2D>(TextureGlowPath, out var asset, AssetRequestMode.ImmediateLoad))
        {
            Texture2D texture = asset.Value;
            if (texture != null)
            {
                SpriteEffects effect = npc.spriteDirection == 1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
                Vector2 origin = npc.frame.Size() * 0.5f;
                Vector2 position = npc.Center - Main.screenPosition + new Vector2(0f, npc.gfxOffY);
                spriteBatch.Draw(texture, position, npc.frame, Color.White, npc.rotation, origin, npc.scale, effect, 0f);
            }
        }
        
        base.PostDraw(npc, spriteBatch, screenPos, drawColor);
    }
}