using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;

public static class PlataformModel
{
    public static int[] getBiomeBlocks(Player player)
    {
        if (player.ZoneForest)
        {
            return new int[] { 0, 0 };
        }
        else if (player.ZoneCorrupt)
        {
            if (WorldGen.crimson)
            {
                return new int[] { 1, 19 };
            }
            else
            {
                return new int[] { 5, 18 };
            }
        }
        else if (player.ZoneJungle)
        {
            return new int[] { 2, 21 };
        }
        else if (player.ZoneHallow)
        {
            return new int[] { 3, 20 };
        }
        else if (player.ZoneDungeon)
        {
            return new int[] { 9, 13 };
        }
        else if (player.ZoneSkyHeight)
        {
            return new int[] { 31, 12 };
        }
        else if (player.ZoneMarble)
        {
            return new int[] { 29, 12 };
        }
        else if (player.ZoneBeach)
        {
            return new int[] { 17, 17 };
        }
        else if (player.ZoneSnow)
        {
            return new int[] { 19, 9 };
        }
        else if (player.ZoneDesert)
        {
            return new int[] { 25, 16 };
        }
        else if (player.ZoneUnderworldHeight)
        {
            return new int[] { 13, 7 };
        }
        return new int[] { 0, 12 };
    }
    public static void TileSafe(int x, int y)
    {
        if (Main.tile[x, y] == null)
        {
            Tile val = Main.tile[x, y];
            val.ResetToType(0);
        }
    }
    public static void HoldItem(Player player, float Range)
    {
        if (player.whoAmI == Main.myPlayer)
        {
            for (int i = 0; i <= Range; i++)
            {
                int x = Player.tileTargetX + (i * player.direction);
                int y = Player.tileTargetY;

                if (!Main.tile[x, y].HasTile)
                {
                    int d = Dust.NewDust(new Vector2(x, y) * 16, 10, 10, DustID.WoodFurniture, 0, 0, 0, default, 1);
                    Main.dust[d].noGravity = true;
                }
            }
        }
    }
}