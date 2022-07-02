using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;

namespace opswordsII.Items.Debugg
{
	public class ExpertToggler : ModItem
	{
		/*public override bool Autoload(ref string name)
		{
		return ModLoader.GetMod("OpswordsIIDebugMod") != null;
		}*/
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("ExpertToggler");
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
				if (Main.expertMode == true)
				{
					Main.GameMode = 1;
					Main.NewText("Expert mode desactivado");
				}
                else
                {
				
					Main.NewText("Expert mode activado");
				}	
                Netcode.SyncWorld();
			}
			return true;
		}

	}
}