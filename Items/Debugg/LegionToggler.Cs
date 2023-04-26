/*using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;

namespace RemnantOfTheAncientsMod.Items.Debugg
{
	public class LegionToggler : ModItem
	{
		/*public override bool Autoload(ref string name)
		{
		return ModLoader.GetMod("RemnantOfTheAncientsModDebugMod") != null;
		}*/
		/*public override void SetStaticDefaults()
		{
//DisplayName.SetDefault("LegionToggler3000");
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
        public override bool UseItem(Player player)
		{
if (Main.netMode != 1)
{
	if (world1.OneInMiddleMillion == true)
	{
		Main.NewText("Legion desactivado");
		world1.OneInMiddleMillion = false;
	}
	else
	{
		Main.NewText("Legion activado");
		world1.OneInMiddleMillion = true;
	}
	
                Netcode.SyncWorld();
}
return true;
		}

	}
}*/