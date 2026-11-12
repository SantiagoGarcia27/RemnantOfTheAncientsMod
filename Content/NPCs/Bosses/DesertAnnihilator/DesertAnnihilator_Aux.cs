using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;

class DesertAnnihilator_Aux
{
    public NPC npc {  get; set; }

    public Player CurrentTarget => npc != null ? Main.player[npc.target] : Main.player[0];
    private bool _BossIsInRage = false;
    public bool BossIsInRage
    {
        get => _BossIsInRage;
        set
        {
            _BossIsInRage = value;
            if (value) SoundEngine.PlaySound(SoundID.Roar);
        }
    }

    public DesertAnnihilator_Aux(NPC npc)
    {
        this.npc = npc;
    }
    /*public static Vector2 GetSecurePosition(Vector2 position)
    {
        Vector2 newPos;
        int blockIncrement = 0;

        position = new(Math.Abs(position.X), Math.Abs(position.Y));

        if (!CoordHasTile(position) && !CoordHasLiquid(position)) return position;

        do
        {
            float positionY = position.Y - (blockIncrement++).ToCoordinatePosition();
            newPos = new(position.X, positionY);
        } while (CoordHasTile(newPos) || CoordHasLiquid(newPos));

        if (newPos.Y < 0) newPos.Y *= -1;
        return newPos;
    }*/

    public static Vector2 GetSecurePosition(NPC npc, Vector2 position)
    {
        const int maxRadius = 40;
        const int tileSize = 16;


        bool IsSafe(Vector2 center)
        {
            Vector2 topLeft = center - npc.Size / 2f;

            if (topLeft.X < 0 || topLeft.Y < 0 ||
                topLeft.X + npc.width > Main.maxTilesX * 16 ||
                topLeft.Y + npc.height > Main.maxTilesY * 16)
                return false;

            return !CoordHasTile(npc.Size, topLeft)
                && !CoordHasLiquid(npc.Size, topLeft);
        }

        if (IsSafe(position))
            return position;

        for (int radius = 1; radius <= maxRadius; radius++)
        {
            int offset = radius * tileSize;

            // Bordes superior e inferior.
            for (int x = -radius; x <= radius; x++)
            {
                float offsetX = x * tileSize;

                Vector2 top = position + new Vector2(offsetX, -offset);
                if (IsSafe(top))
                    return top;

                Vector2 bottom = position + new Vector2(offsetX, offset);
                if (IsSafe(bottom))
                    return bottom;
            }

            // Bordes izquierdo y derecho, sin repetir esquinas.
            for (int y = -radius + 1; y < radius; y++)
            {
                float offsetY = y * tileSize;

                Vector2 left = position + new Vector2(-offset, offsetY);
                if (IsSafe(left))
                    return left;

                Vector2 right = position + new Vector2(offset, offsetY);
                if (IsSafe(right))
                    return right;
            }
        }
        Main.NewText("No se encontró una posición segura");
        return Main.player[npc.target]?.Center ?? position ;
    }

    public static bool CoordHasTile(Vector2 size, Vector2 pos, int offset = 2)
    {
        return Collision.SolidCollision(pos - new Vector2(offset), (int)size.X + offset * 2, (int)size.Y + offset * 2);
    }

    public static bool CoordHasLiquid(Vector2 size, Vector2 pos)
    {
        int width = (int)size.X;
        int height = (int)size.Y;

        return Collision.LavaCollision(pos, width, height) || Collision.WetCollision(pos, width, height);
    }
}