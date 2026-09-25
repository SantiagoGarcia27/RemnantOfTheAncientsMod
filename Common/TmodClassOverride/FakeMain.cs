using Microsoft.Xna.Framework;
using PlayerProxyLib.Common;
using Terraria;
using Terraria.ModLoader;

public class FakeMain : ModSystem
{


    public static Vector2 MouseWorld(Player player)
    {
        if (player.IsProxyPlayer())
            return player.GetMouseWorld();

        return Main.MouseWorld;
    }
}