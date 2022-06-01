using System;
using System.IO;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;


namespace opswordsII
{
    public class Netcode
    {

        public static void SyncWorld()
		{
			if (Main.netMode == 2)
			{
				NetMessage.SendData(7, -1, -1, (NetworkText)null, 0, 0f, 0f, 0f, 0, 0, 0);
			}
		}
    }
}