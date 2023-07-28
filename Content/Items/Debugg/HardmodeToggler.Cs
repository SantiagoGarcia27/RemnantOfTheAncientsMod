using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using Microsoft.Xna.Framework;
using Terraria.Chat;

namespace RemnantOfTheAncientsMod.Content.Items.Debugg
{
	public class HardmodeToggler : ModItem
	{
		public override bool IsLoadingEnabled(Mod mod)
		{
			return ModLoader.TryGetMod("OpswordsIIDebugMod", out mod);
		}
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("HardmodeToggler3000");
		}

		public override void SetDefaults()
		{
			Item.width = 20;
			Item.height = 20;
			Item.rare = ItemRarityID.Pink;
			Item.useAnimation = 20;
			Item.useTime = 20;
			Item.useStyle = ItemUseStyleID.HoldUp;
			Item.UseSound = SoundID.Item60;
		}
		public override bool? UseItem(Player player)
		{
			string text = "";
			if (Main.netMode != NetmodeID.MultiplayerClient)
			{
				if (Main.hardMode == true) Main.hardMode = false;
				else Main.hardMode = true;

				text = Main.hardMode ? "Hard mode activado!" : "Hard mode desactivado";
				Netcode.SyncWorld();
			}
			if (Main.netMode == NetmodeID.SinglePlayer) Main.NewText(text, new Color(175, 75, 255));
			else if (Main.netMode == NetmodeID.Server)
			{
				ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral(text), new Color(175, 75, 255));
				NetMessage.SendData(MessageID.WorldData);
			}
			return true;
		}

	}
}