using Microsoft.Xna.Framework;
using RemnantOfTheAncientsMod;
using RemnantOfTheAncientsMod.Common.Enums;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

public class PlataformModel
{
    public static int[] getBiomeBlocks(Player player)
    {
        if (player.ZoneForest)
        {
            return [0, 0];
        }
        else if (player.ZoneCorrupt)
        {
            return [5, 18];
        }
        else if (player.ZoneCrimson)
        {
            return [1, 19];
        }
        else if (player.ZoneJungle)
        {
            return [2, 21];
        }
        else if (player.ZoneHallow)
        {
            return [3, 20];
        }
        else if (player.ZoneDungeon)
        {
            return [9, 13];
        }
        else if (player.ZoneSkyHeight)
        {
            return [31, 12];
        }
        else if (player.ZoneMarble)
        {
            return [29, 12];
        }
        else if (player.ZoneBeach)
        {
            return [17, 17];
        }
        else if (player.ZoneSnow)
        {
            return [19, 9];
        }
        else if (player.ZoneDesert)
        {
            return [25, 16];
        }
        else if (player.ZoneUnderworldHeight)
        {
            return [13, 7];
        }
        return [0, 12];
    }
    public static void TileSafe(int x, int y, int type = 0, int style = 0)
    {
        Tile tile = Main.tile[x, y];
        if (type == TileID.Torches || type == TileID.Platforms)
        {
            WorldGen.PlaceTile(x, y, type, mute: true, forced: false, -1, style);
            return;
        }

        if (tile == null || !tile.HasTile)
        {
            tile.ResetToType((ushort)type);
        }
    }
    public static void HoldItem(Player player, float Range)
    {
        if (Main.netMode == NetmodeID.SinglePlayer)
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
        else
        {
            Vector2 mouse = Main.MouseWorld;
            int side = Math.Sign(mouse.X - player.Center.X);   
            if (side == -1)
                mouse.X += side * 16;

            bool Derecha = player.Center.X < mouse.X;
            int min = !Derecha ? -(int)Range : 0;
            int max = !Derecha ? 0 : (int)Range;

          

            for (int x = min; x <= max; x++)
            {
                int xPosition = (int)(x + mouse.X / 16.0f);
                int yPosition = (int)(mouse.Y / 16.0f);


               

                if (!Main.tile[xPosition, yPosition].HasTile)
                {
                    int d = Dust.NewDust(new Vector2(xPosition, yPosition) * 16, 10, 10, DustID.WoodFurniture, 0, 0, 0, default, 1);
                    Main.dust[d].noGravity = true;
                }
            }
        }

    }

    internal static bool InGameWorld(int x, int y)
    {
        if (x > 39 && x < Main.maxTilesX - 39 && y > 39)
        {
            return y < Main.maxTilesY - 39;
        }
        return false;
    }
    internal static void SquareUpdate(int x, int y)
    {
        if (Main.netMode != NetmodeID.Server)
        {
            NetMessage.SendTileSquare(-1, x, y, 1, (TileChangeType)0);
        }
    }
    public static void SavePacket(Player player, int plataform, int torch, int x, int y, int range)
    {
        //ModPacket packet = ModContent.GetInstance<RemnantOfTheAncientsMod.RemnantOfTheAncientsMod>().GetPacket();
        //packet.Write((byte)GlobalEnum.packetType.placePltaform);
        //packet.Write(plataform);
        //packet.Write(torch);
        //packet.Write(x);
        //packet.Write(y);
        //packet.Write(range);
        //int index = player.whoAmI;
        //packet.Write(index);
        //packet.Send();
    }

    public static void BuildPlataform(Player player, int plataformType, int torchType, int playerPosX, int playerPosY, int range)
    {

        if (player.whoAmI == Main.CurrentPlayer.whoAmI)
        {


            for (int i = 0; i < range; i++)
            {
                int x = playerPosX + (i * player.direction);
                int y = playerPosY;
                //TileSafe(x, y);
                if (!InGameWorld(x, y))
                {
                    continue;
                }

                WorldGen.PlaceTile(x, y, TileID.Platforms, mute: true, forced: false, -1, plataformType);

                TileSafe(x, y, TileID.Platforms, plataformType);
                SquareUpdate(x, y);
                if (i % 10 == 0)
                {
                    WorldGen.PlaceTile(x, y, TileID.Torches, mute: true, forced: false, -1, torchType);
                    TileSafe(x, y - 1, TileID.Torches, torchType);
                    SquareUpdate(x, y - 1);
                }
            }
        }
    }
}