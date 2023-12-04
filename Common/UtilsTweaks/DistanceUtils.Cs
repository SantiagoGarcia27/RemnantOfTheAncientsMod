using Microsoft.CodeAnalysis;
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
        public static int GetPlayerFlour(Player player)
        {
            return Main.tile[(int)(player.Center.X / 16), (int)((player.Center.Y + (2 * 16)) / 16)].TileType;
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
        public static Tile? SearchLiquid(int liquid, float range)
        {
            for (int i = (int)-range; i < range; i++)
            {
                for (int j = (int)-range; j < range; j++)
                {
                    if (Main.tile[i, j].LiquidAmount > 0 && Main.tile[i, j].LiquidType == liquid)
                    {
                        return Main.tile[i, j];
                    }
                }
            }
            return null;
        }
        public static Vector2 SearchLiquidCoordenates(int liquid, float range, Player center)
        {
            for (int i = (int)-range; i < range; i++)
            {
                for (int j = (int)-range; j < range; j++)
                {
                    float LiquidAmount = Main.tile[(int)ToTilePosition(center.Center.X + i), (int)ToTilePosition(center.Center.Y + j)].LiquidAmount;
                    if (LiquidAmount > 0)
                    {
                        if (Main.tile[(int)ToTilePosition(center.Center.X + i), (int)ToTilePosition(center.Center.Y + j)].LiquidType == liquid)
                        {
                            return new(center.Center.X + i, center.Center.Y + j);
                        }
                    }
                }
            }
            return center.Center - new Vector2(5 * 16, 5 * 16);
        }
        public static int checkHigh(Vector2 imput)
        {
            Tile t = Main.tile[(int)ToTilePosition(imput).X, (int)ToTilePosition(imput).Y];
            bool TileHasBlock = t.HasTile;
            bool TileHasLiquid = t.LiquidAmount > 0;
            if( TileHasBlock || TileHasLiquid ) 
            {
                return (int)imput.Y;
            }
            else
            {
                imput.Y += 1;
                checkHigh(imput);
            }
            return (int)imput.Y;
        }
    }
}