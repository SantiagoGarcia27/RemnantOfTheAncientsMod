using Microsoft.Xna.Framework;
using System.Reflection;
using Terraria;
using Terraria.Audio;
using Terraria.Chat;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Items.Debugg
{
    public class ExpertToggler : ModItem
	{
		public override bool IsLoadingEnabled(Mod mod)
		{
			return ModLoader.TryGetMod("RemnantOfTheAncientsModDebugMod", out mod);
		}
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("ExpertToggler");
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
            FieldInfo currentGameModeInfoField = typeof(Main).Assembly.GetType("Terraria.Main").GetField("_currentGameModeInfo", BindingFlags.Static | BindingFlags.NonPublic);
            GameModeData currentGameModeInfo = (GameModeData)currentGameModeInfoField.GetValue(null);
            currentGameModeInfo = currentGameModeInfo == GameModeData.ExpertMode ? GameModeData.NormalMode : GameModeData.ExpertMode;
            currentGameModeInfoField.SetValue(null, currentGameModeInfo);
            string text = Main.expertMode ? "Expert mode activado!" : "Expert mode desactivado";

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