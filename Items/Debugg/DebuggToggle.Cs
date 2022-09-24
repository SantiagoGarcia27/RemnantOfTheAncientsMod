using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.Chat;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Items.Debugg
{
    public class DebuggToggle : ModItem
	{
        public override void SetStaticDefaults() => DisplayName.SetDefault("DebuggToggle");

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
            bool n = RemnantOfTheAncientsMod.DebuggMode ? true : false;
            string text = RemnantOfTheAncientsMod.DebuggMode ? "Debugg activado!" : "Debugg desactivado";
            RemnantOfTheAncientsMod.DebuggMode = n;

            if (Main.netMode == NetmodeID.SinglePlayer)
            {
                Main.NewText(text, new Color(175, 75, 255));
            }
            else if (Main.netMode == NetmodeID.Server)
            {
                ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral(text), new Color(175, 75, 255));
                NetMessage.SendData(MessageID.WorldData); 
            }

            SoundEngine.PlaySound(SoundID.Item15, player.Center);

            return true;
        }

    }
}