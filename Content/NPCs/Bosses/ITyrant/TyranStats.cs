using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SangarUtilities.Common.UtilsTweaks;
using System;
using Terraria;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Content.NPCs.Bosses.ITyrant
{
    public static class TyranStats
    {
        public static int TyrantArmor(int defense, NPC npc)
        {
            float lifePercent = (float)npc.life / npc.lifeMax;

            float decayFactor = (float)Math.Pow(lifePercent, 5);

            float minPercent = 0.05f;

            if (Main.expertMode || Main.masterMode)
            {
                minPercent = 0.02f;
                if (DificultyUtils.ReaperMode) minPercent = 0.00f;
            }

            float finalFactor = MathHelper.Lerp(minPercent, 1f, decayFactor);

            int processedDefense = (int)(defense * finalFactor);

            return RemnantOfTheAncientsMod.RemnantOfTheAncientsMod.CalamityMod != null ? processedDefense * 2 : processedDefense;
        }


        public static void DrawGlow(NPC npc, string NpcName)
        {
            SpriteEffects effects = npc.spriteDirection == 1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None;

            var glowTexture = (Texture2D)ModContent.Request<Texture2D>("RemnantOfTheAncientsMod/Content/NPCs/Bosses/ITyrant/" + NpcName + "_Glow");
            Vector2 origin = new(glowTexture.Width * 0.5f, glowTexture.Height / Main.npcFrameCount[npc.type] * 0.5f);
            Vector2 position = npc.Center - Main.screenPosition + new Vector2(0f, npc.gfxOffY);
            Color color = Utils.MultiplyRGBA(new Color(127 - npc.alpha, 127 - npc.alpha, 127 - npc.alpha, 0), Color.LightYellow);

            Main.EntitySpriteDraw(glowTexture, position, npc.frame, color, npc.rotation, origin, npc.scale, effects, 0);
        }
    }
}