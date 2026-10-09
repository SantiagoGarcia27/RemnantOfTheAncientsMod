using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SangarUtilities.Common.UtilsTweaks;
using System;
using Terraria;
using Terraria.ModLoader;

public static class TyranStats
{
    public static int TyrantArmor(int defense, NPC npc)
    {
        // 1. Calculamos el porcentaje de vida actual (0.0 a 1.0)
        float lifePercent = (float)npc.life / npc.lifeMax;

        // 2. Aplicamos una potencia para que la caída sea drástica al inicio.
        // Elevar el porcentaje a la potencia 5 hace que:
        // Al 100% de vida -> 1.0^5 = 1.0  (Defensa total: 999)
        // Al 75% de vida  -> 0.75^5 = 0.23 (Defensa: ~230)
        // Al 50% de vida  -> 0.50^5 = 0.03 (Defensa: ~30)
        float decayFactor = (float)Math.Pow(lifePercent, 5);

        // 3. Definimos el "Suelo" (Mínimo de defensa para que no sea papel al final)
        float minPercent = 0.05f; // Mantener al menos un 15% de la defensa

        if (Main.expertMode || Main.masterMode)
        {
            if (DificultyUtils.ReaperMode) minPercent = 0.00f;
            minPercent = 0.02f;
        }

        // Mezclamos el factor de caída con el mínimo (Lerp manual)
        float finalFactor = MathHelper.Lerp(minPercent, 1f, decayFactor);

        int processedDefense = (int)(defense * finalFactor);

        // 4. Multiplicador de Calamity
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