using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;

public class ReaperGlobalNPC_Utilities
{

    public static List<NPC> GetNPCList(int npcid, out List<NPC> CreepersList)
    {
        CreepersList = [];
        for (int i = 0; i < Main.maxNPCs; i++)
        {
            if (Main.npc[i].type == npcid)
            {
                CreepersList.Add(Main.npc[i]);
            }
        }
        return CreepersList;
    }
    public static List<Vector2> GetPositionsAround(Player player, float radius, int numPositions, float angle, out List<Vector2> positions)
    {
        positions = [];

        float angleSpacing = MathHelper.TwoPi / numPositions;

        for (int i = 0; i < numPositions; i++)
        {
            float Angle = (angleSpacing * i) + angle;

            float x = radius * MathF.Cos(Angle);
            float y = radius * MathF.Sin(Angle);

            Vector2 position = player.Center + new Vector2(x, y);

            positions.Add(position);
        }

        return positions;
    }
    public static List<Vector2> GetPositionsAround(NPC npc, float radius, int numPositions)
    {
        if (npc == null) return null;

        GetPositionsAround(npc.Center, radius, numPositions, out List<Vector2> positions);
        return positions;
    }



    public static List<Vector2> GetPositionsAround(Vector2 center, float radius, int numPositions, out List<Vector2> positions)
    {
        positions = [];

        float angleSpacing = MathHelper.TwoPi / numPositions;

        for (int i = 0; i < numPositions; i++)
        {
            float angle = angleSpacing * i;

            float x = radius * MathF.Cos(angle);
            float y = radius * MathF.Sin(angle);

            Vector2 position = center + new Vector2(x, y);

            positions.Add(position);
        }

        return positions;
    }
}