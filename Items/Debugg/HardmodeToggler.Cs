using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;

namespace opswordsII.Items.Debugg
{
	public class HardmodeToggler : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("HardmodeToggler3000");
		}

			public override void SetDefaults()
		{
			Item.width = 20;
			Item.height = 20;
			Item.rare = 5;
			Item.useAnimation = 20;
			Item.useTime = 20;
			Item.useStyle = 4;
			Item.UseSound = SoundID.Item60;

        }
        public override bool? UseItem(Player player)
		{
			if (Main.netMode != 1)
			{
				if (Main.hardMode == true)
				{
					Main.hardMode = false;
				}
                else
                {
                    Main.hardMode = true;
                }	
                Netcode.SyncWorld();
			}
			return true;
		}

	}
}