using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Numerics;
using Terraria;
using Vector2 = Microsoft.Xna.Framework.Vector2;

namespace RemnantOfTheAncientsMod.Common.UtilsTweaks
{
    public static class DistanceUtils
    {
        public static Vector2 GetDistance(Entity request, Entity target)
        {
            float x = request.position.X - target.position.X;
            float y = request.position.Y - target.position.Y;
            return new Vector2(x, y);
        }

        public static Tile GetFloor(this Entity entity, int depth = 1) => Main.tile[(int)entity.Center.X.ToTilePosition(), (int)(entity.Center.Y.ToTilePosition() + (depth + 1))];

        public static T ToTilePosition<T>(this T value) where T : INumber<T> => value / T.CreateChecked(16);
        public static Vector2 ToTilePosition(this Vector2 position) => position / 16;
        
        public static T ToCoordinatePosition<T>(this T value) where T : INumber<T> => value * T.CreateChecked(16);
        public static Vector2 ToCoordenatePosition(this Vector2 position) =>  position * 16;

        public static bool TouchFlour(this Entity entity) => entity.GetFloor().HasTile;
        
        
        public static bool TouchFlour(Projectile projectile)
        {
            Tile tile = Main.tile[(int)(projectile.Center.X / 16), (int)((projectile.Center.Y + (2 * 16)) / 16)];
            return tile.HasTile && tile.IsTileSolidGround();
        }
        public static bool IsTileSolidGround(this Tile tile)
        {
            if (tile == null || !tile.HasUnactuatedTile) return false;
            if (Main.tileSolid[tile.TileType]) return true;
            
            return Main.tileSolidTop[tile.TileType];  
        }
        public static bool TouchFlourLiquid(Entity entity) => entity.GetFloor().LiquidAmount > 0;
        
        public static int GetPlayerFlour(Player player,int depth = 1)
        {
            Tile tile = player.GetFloor(depth);
            return tile.HasTile ? tile.TileType : -1; 
        }
       

        public static bool TouchWall(Projectile projectile)
        {
            Vector2 coordenate;
            int orientacion = 1 * 16 * projectile.direction;
            coordenate.X = (projectile.Center.X + orientacion) / 16;
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
        public static bool ExistTileSolid(Vector2 pos, bool ignorePlataform = false)
        {
            Tile tile = Main.tile[(int)pos.X / 16, (int)pos.Y / 16];

            bool solid = tile.HasTile && Main.tileSolid[tile.TileType] && !tile.IsActuated;
            if(ignorePlataform)
            {
                solid = solid && !Main.tileSolidTop[tile.TileType];
            }
            return solid;
        }
        public static bool ExistFluid(Vector2 pos) 
        {
            return Main.tile[new Point((int)pos.X / 16, (int)(pos.Y) / 16)].LiquidAmount > 0;
        }
        public static bool ExistFluid(Vector2 pos, int liquidId)
        {
            Tile tile = Main.tile[(new Point((int)pos.X / 16, (int)(pos.Y) / 16))];
            return tile.LiquidAmount > 0 && tile.LiquidType == liquidId;
        }
        /* public static Vector2 SetPositionOnSolidFloor(Vector2 imput)
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
         }*/
        public static Vector2 SetPositionOnSolidFloor(Vector2 input)
        {
            Vector2 tileSize = ToCoordenatePosition(new Vector2(0, 1));
            Vector2 originalPos = input;

            for (int i = 0; i < 100; i++)
            {
                if (ExistTileSolid(input + tileSize))
                {
                    return input;
                }

                input += tileSize;
            }
            if (ExistTileSolid(input))
            {
                input = originalPos;
                for (int i = 0; i < 100; i++)
                {
                    if (ExistTileSolid(input - tileSize))
                    {
                        return input;
                    }

                    input -= tileSize;
                }
            }

            return input;
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


        public static bool TryFindGround(Vector2 pos, int maxDistance, out Point groundTile)
        {
            Point tilePos = pos.ToTileCoordinates();

            for (int y = tilePos.Y; y < tilePos.Y + maxDistance && y < Main.maxTilesY; y++)
            {
                Tile tile = Framing.GetTileSafely(tilePos.X, y);

                if (tile.HasTile &&
                    Main.tileSolid[tile.TileType] &&
                    !Main.tileSolidTop[tile.TileType])
                {
                    groundTile = new Point(tilePos.X, y);
                    return true;
                }
            }

            groundTile = Point.Zero;
            return false;
        }
    }
}