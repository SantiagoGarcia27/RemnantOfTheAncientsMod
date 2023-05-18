using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using Terraria.Localization;
using static Terraria.ModLoader.ModContent;
using Microsoft.Xna.Framework;
using RemnantOfTheAncientsMod.Content.NPCs;
using Terraria.Chat;
using RemnantOfTheAncientsMod.Content.NPCs.Bosses.DAniquilator;

namespace RemnantOfTheAncientsMod.Content.Items.Tools.Utilidad
{
	public class SuperDummy : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Advanced Dummy");
			DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Super factice");
			DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Muñeco de entrenamiento avanzado");
			Tooltip.SetDefault("Right click to change mode");
			Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Clic droit pour changer de mode");
			Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Clic derecho para cambiar modo");
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }
		
		public override void SetDefaults()
		{
			Item.width = 20;
			Item.height = 20;
			Item.rare = ItemRarityID.Pink;
			Item.useAnimation = 20;
			Item.useTime = 20;
			Item.maxStack = 1;
			Item.useStyle = ItemUseStyleID.HoldUp;
			Item.UseSound = SoundID.Item60;
			Item.consumable = false;
		}

        public override bool AltFunctionUse(Player player) => true;
        public override bool? UseItem(Player player)
		{

            if (player.altFunctionUse == 2)
			{
				if(RemnantPlayer.DummyMode != 3)
				{
					RemnantPlayer.DummyMode++;
                }
				else
				{
					RemnantPlayer.DummyMode = 0;
                }

                switch (RemnantPlayer.DummyMode)
				{
					case 0:
						ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral("Small with no defense"), Color.Red);
						break;
					case 1:
						ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral("Small with player defense"), Color.Red);
						break;
					case 2:
						ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral("Gigant with no defense"), Color.Red);
						break;
					case 3:
						ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral("Gigant with player defense"), Color.Red);
						break;
				}

			}
			else
			{

                switch (RemnantPlayer.DummyMode)
                {
                    case 0:
						CheckDummyAlive(0);
                        break;
                    case 1:
                        CheckDummyAlive(1);
                        break;
                    case 2:
                        CheckDummyAlive(2);
                        break;
                    case 3:
                        CheckDummyAlive(3);
                        break;
                }
            }
			return true;
		}

		public void CheckDummyAlive(int Ai1)
		{
			Player player = Main.player[Main.myPlayer];

            if (!NPC.AnyNPCs(ModContent.NPCType<SuperDummyNPC>()))
			{
                if (player.whoAmI == Main.myPlayer)
                {
                    int x = (int)Main.MouseWorld.X - 9;
                    int y = (int)Main.MouseWorld.Y - 20;
                    if (Main.netMode == NetmodeID.SinglePlayer)
                    {
                        NPC.NewNPC(NPC.GetSource_None(), x, y, NPCType<SuperDummyNPC>(),0,Ai1);
                    }
                    else
                    {
                        ModPacket packet2 = Mod.GetPacket();
                        packet2.Write((byte)8);
                        packet2.Write(x);
                        packet2.Write(y);
                        packet2.Send();
                    }
                }
            }
			else
			{
                if (Main.myPlayer == player.whoAmI)
                {
                    if (Main.netMode == NetmodeID.SinglePlayer)
                    {
                        DeleteDummie();
                    }
                    else
                    {
                        ModPacket packet = Mod.GetPacket();
                        packet.Write((byte)9);
                        packet.Send();
                    }
                }
            }
		}
        public static void DeleteDummie()
        {
            for (int i = 0; i < 200; i++)
            {
                NPC npc = Main.npc[i];
                if (npc.type == NPCType<SuperDummyNPC>() && npc.active)
                {
                    npc.life = 0;
                    npc.active = false;
                    if (Main.netMode == NetmodeID.Server)
                    {
                        NetMessage.SendData(MessageID.SyncNPC, -1, -1, null, i);
                    }
                }
            }
        }
        public override void AddRecipes()
		{
			CreateRecipe()
			.AddIngredient(ItemID.TargetDummy)
            .AddIngredient(ItemID.Wire, 10)
            .AddTile(TileID.TinkerersWorkbench)
			.Register();
		}
	}
}
