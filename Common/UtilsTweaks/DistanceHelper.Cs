using Microsoft.Xna.Framework;
using Terraria;

namespace RemnantOfTheAncientsMod.Common.UtilsTweaks
{
    public static class DistanceHelper
    {
        public static Vector2 GetDistance(NPC request, NPC target)
        {
            float x = request.position.X - target.position.X;
            float y = request.position.Y - target.position.Y;
            return new Vector2(x, y);
        }
        public static Vector2 GetDistance(NPC request, Player target)
        {
            float x = request.position.X - target.position.X;
            float y = request.position.Y - target.position.Y;
            return new Vector2(x, y);
        }
        public static Vector2 GetDistance(NPC request, Projectile target)
        {
            float x = request.position.X - target.position.X;
            float y = request.position.Y - target.position.Y;
            return new Vector2(x, y);
        }

        public static Vector2 GetDistance(Projectile request, NPC target)
        {
            float x = request.position.X - target.position.X;
            float y = request.position.Y - target.position.Y;
            return new Vector2(x, y);
        }
        public static Vector2 GetDistance(Projectile request, Projectile target)
        {
            float x = request.position.X - target.position.X;
            float y = request.position.Y - target.position.Y;
            return new Vector2(x, y);
        }
        public static Vector2 GetDistance(Projectile request, Player target)
        {
            float x = request.position.X - target.position.X;
            float y = request.position.Y - target.position.Y;
            return new Vector2(x, y);
        }
        public static Vector2 GetDistance(Player request, NPC target)
        {
            float x = request.position.X - target.position.X;
            float y = request.position.Y - target.position.Y;
            return new Vector2(x, y);
        }
        public static Vector2 GetDistance(Player request, Projectile target)
        {
            float x = request.position.X - target.position.X;
            float y = request.position.Y - target.position.Y;
            return new Vector2(x, y);
        }
        public static Vector2 GetDistance(Player request, Player target)
        {
            float x = request.position.X - target.position.X;
            float y = request.position.Y - target.position.Y;
            return new Vector2(x, y);
        }
        public static float ToTilePosition(float NpcPosition)
        {
            return NpcPosition / 16;
        }
        public static bool PlayerTouchFlour(Player player)
        {
            return Main.tile[(int)(player.Center.X / 16), (int)((player.Center.Y + (2 * 16)) / 16)].HasTile;
        }
    }
}