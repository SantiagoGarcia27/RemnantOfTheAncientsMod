using Microsoft.CodeAnalysis;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

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
        public static bool TouchFlour(Player player)
        {
            return Main.tile[(int)(player.Center.X / 16), (int)((player.Center.Y + (2 * 16)) / 16)].HasTile;
        }
        public static bool TouchFlour(Projectile projectile)
        {
            return Main.tile[(int)(projectile.Center.X / 16), (int)((projectile.Center.Y + (2 * 16)) / 16)].HasTile;
        }
        public static bool TouchFlourLiquid(Player player)
        {
            return Main.tile[(int)(player.Center.X / 16), (int)((player.Center.Y + (2 * 16)) / 16)].LiquidAmount > 0;
        }
        public static bool TouchFlourLiquid(Projectile projectile)
        {
            return Main.tile[(int)(projectile.Center.X / 16), (int)((projectile.Center.Y + (2 * 16)) / 16)].LiquidAmount > 0;
        }

        public static int GetPlayerFlour(Player player)
        {
            return Main.tile[(int)(player.Center.X / 16), (int)((player.Center.Y + (2 * 16)) / 16)].TileType;
        }
       

        public static bool TouchWall(Projectile projectile)
        {
            Vector2 coordenate;
            int orientacion = 1 * 16 * projectile.direction;
            coordenate.X = (projectile.Center.X + orientacion) / 16;///** projectile.direction*/ / 16);
            coordenate.Y = projectile.Center.Y / 16;

            return Main.tile[(int)coordenate.X, (int)coordenate.Y].HasTile;
        }
        public static bool TouchWall(Projectile projectile, int high)
        {
            List<bool> bloques = [];
            bloques.Clear();
            for (int a = 0; a < high; a++)
            {
                if (!Main.tile[(int)(projectile.Center.X + (1 * 16) * projectile.direction / 16), (int)((projectile.Center.Y + (a * 16)) / 16)].HasTile)
                    return false;
            }
            return true;
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
            return tile.HasTile && Main.tileSolid[tile.TileType] && !tile.IsActuated;
        }
        public static bool ExistFluid(Vector2 pos) 
        {
            return Main.tile[(new Point((int)pos.X / 16, (int)(pos.Y) / 16))].LiquidAmount > 0;
        }
        public static bool ExistFluid(Vector2 pos, int liquidId)
        {
            Tile tile = Main.tile[(new Point((int)pos.X / 16, (int)(pos.Y) / 16))];
            return tile.LiquidAmount > 0 && tile.LiquidType == liquidId;
        }
        public static Vector2 SetPositionOnSolidFloor(Vector2 imput)
        {
            if (ExistTileSolid(imput) && ExistTileSolid(imput - ToCoordenatePosition(new Vector2(0, 1))))
            {
                do
                {
                    imput -= ToCoordenatePosition(new Vector2(0, 1));
                }
                while (ExistTileSolid(imput - ToCoordenatePosition(new Vector2(0, 1))));

                if (!ExistTileSolid(imput + ToCoordenatePosition(new Vector2(0, 1))))
                {
                    do
                    {
                        imput += ToCoordenatePosition(new Vector2(0, 1));
                    }
                    while (ExistTileSolid(imput + ToCoordenatePosition(new Vector2(0, 1))));
                }
            }
            return imput;
        }
        public static Tile? SearchLiquid(int liquid, int range)
        {
            int i, j;
            
            i = j = -range;

            while (Main.tile[i, j].LiquidAmount > 0 && Main.tile[i, j].LiquidType == liquid && i < range)
            {
                if (j >= range)
                {
                    i++;
                    j = 0;
                }
                else
                {
                    j++;
                }
            }

            //for (int i = (int)-range; i < range; i++)
            //{
            //    for (int j = (int)-range; j < range; j++)
            //    {
            //        if (Main.tile[i, j].LiquidAmount > 0 && Main.tile[i, j].LiquidType == liquid)
            //        {
            return i >= range && i >= range ? null : Main.tile[i, j];
            //    }
            //}
       // }
           // return null;
        }
        public static Vector2 SearchLiquidCoordenates(int liquid, float range, Player center)
        {
            for (int i = (int)-range; i < range; i++)
            {
                for (int j = (int)-range; j < range; j++)
                {
                    Tile tile = Main.tile[(int)ToTilePosition(center.Center.X + i), (int)ToTilePosition(center.Center.Y + j)];
                    if (tile.LiquidAmount > 0 && tile.LiquidType == liquid)
                    {
                        return new(center.Center.X + i, center.Center.Y + j);
                    }
                }
            }
            return center.Center - new Vector2(5 * 16, 5 * 16);
        }
        public static int CheckHigh(Vector2 imput)
        {
            Tile t = Main.tile[(int)ToTilePosition(imput).X, (int)ToTilePosition(imput).Y];  
            if(t.HasTile || t.LiquidAmount > 0) 
                return (int)imput.Y;         
            else
            {
                imput.Y += 1;
                CheckHigh(imput);
            }
            return (int)imput.Y;
        }
    }
}