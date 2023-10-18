using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;

namespace RemnantOfTheAncientsMod.Common.UtilsTweaks
{
    public static class DistanceUtils
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
        public static Vector2 ToTilePosition(Vector2 NpcPosition)
        {
            return NpcPosition / 16;
        }
        public static float ToCoordenatePosition(float NpcPosition)
        {
            return NpcPosition * 16;
        }
        public static Vector2 ToCoordenatePosition(Vector2 NpcPosition)
        {
            return NpcPosition * 16;
        }
        public static bool PlayerTouchFlour(Player player)
        {
            return Main.tile[(int)(player.Center.X / 16), (int)((player.Center.Y + (2 * 16)) / 16)].HasTile;
        }
        public static bool PlayerTouchFlourLiquid(Player player)
        {
            return Main.tile[(int)(player.Center.X / 16), (int)((player.Center.Y + (2 * 16)) / 16)].LiquidAmount > 0;
        }
        public static bool ProjectileTouchFlour(Projectile projectile)
        {
            return Main.tile[(int)(projectile.Center.X / 16), (int)((projectile.Center.Y + (2 * 16)) / 16)].HasTile;
        }
        public static bool ProjectileTouchWall(Projectile projectile)
        {
            Vector2 coordenate;
            int orientacion = 1 * 16 * projectile.direction;
            coordenate.X = (projectile.Center.X + orientacion) / 16;///** projectile.direction*/ / 16);
            coordenate.Y = projectile.Center.Y / 16;

            return Main.tile[(int)coordenate.X, (int)coordenate.Y].HasTile;
        }
        public static bool ProjectileTouchWall(Projectile projectile, int high)
        {
            List<bool> bloques = new List<bool>();
            bloques.Clear();
            for (int a = 0; a < high; a++)
            {
                if (!Main.tile[(int)(projectile.Center.X + (1 * 16) * projectile.direction / 16), (int)((projectile.Center.Y + (a * 16)) / 16)].HasTile)
                    return false;
            }
            return true;
        }
        public static bool ProjectileTouchFlourLiquid(Projectile projectile)
        {
            return Main.tile[(int)(projectile.Center.X / 16), (int)((projectile.Center.Y + (2 * 16)) / 16)].LiquidAmount > 0;
        }
        public static bool ExistTile(Vector2 pos)
        {
            return Main.tile[(int)pos.X / 16, (int)pos.Y / 16].HasTile;
        }
        public static bool ExistTile(int x, int y)
        {
            return Main.tile[x / 16, y / 16].HasTile;
        }
        public static bool ExistTileSolid(Vector2 pos)
        {
            Tile tile = Main.tile[(int)pos.X / 16, (int)pos.Y / 16];
            return tile.HasTile && Main.tileSolid[tile.TileType];
        }
        public static Vector2 setPositionOnSolidFloor(Vector2 imput)
        {
            Vector2 output;

            if (ExistTileSolid(imput))
            {
                if (ExistTileSolid(imput - ToCoordenatePosition(new Vector2(0, 1))))
                {
                    do
                    {
                        imput = imput - ToCoordenatePosition(new Vector2(0, 1));
                        var a = Main.tile[ToTilePosition(imput).ToPoint()];
                        Dust.QuickDust(ToTilePosition(imput).ToPoint().X, ToTilePosition(imput).ToPoint().Y, Color.Red);
                    }
                    while (ExistTileSolid(imput - ToCoordenatePosition(new Vector2(0, 1))));

                    if (!ExistTileSolid(imput + ToCoordenatePosition(new Vector2(0, 1))))
                    {
                        do
                        {
                            imput = imput + ToCoordenatePosition(new Vector2(0, 1));
                            var a = Main.tile[ToTilePosition(imput).ToPoint()];
                            Dust.QuickDust(ToTilePosition(imput).ToPoint().X, ToTilePosition(imput).ToPoint().Y, Color.Blue);
                        }
                        while (ExistTileSolid(imput + ToCoordenatePosition(new Vector2(0, 1))));
                    }
                }
                output = imput;
            }
            else
            {
                output = imput;
            }  
            return output;
        }
    }
}