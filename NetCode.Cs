using Terraria;
using Terraria.ID;

namespace RemnantOfTheAncientsMod
{
    public class Netcode
    {

        public static void SyncWorld()
        {
            if (Main.netMode == NetmodeID.Server)
            {
                NetMessage.SendData(MessageID.WorldData, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
            }
        }
    }
}