using Microsoft.Xna.Framework;
using RemnantOfTheAncientsMod.Content.NPCs;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;
using static RemnantOfTheAncientsMod.Netcode;
using static Terraria.ModLoader.ModContent;

namespace RemnantOfTheAncientsMod.Content.Items.Tools.Utilidad
{
	public class SuperDummy : ModItem
	{
		public override void SetStaticDefaults()
		{
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
                RemnantPlayer remnantPlayer = player.GetModPlayer<RemnantPlayer>();
                remnantPlayer.DummyMode = remnantPlayer.DummyMode == 3 ? 0 : remnantPlayer.DummyMode + 1;
 
				string Size = "Small";
				string Defense = "no";

                if (remnantPlayer.DummyMode == 1 || remnantPlayer.DummyMode == 3)
					Defense= "Player";
				else if(remnantPlayer.DummyMode == 2 || remnantPlayer.DummyMode == 3)
					Size = "Gigant";

				Main.NewText(Size + " with " + Defense+" defense");
			}
			else
			{
				CheckDummyAlive(player);
			}
			return true;
		}

		public void CheckDummyAlive(Player player)
		{
			if (!NPC.AnyNPCs(NPCType<SuperDummyNPC>()))
			{
				if (player.whoAmI == Main.myPlayer)
				{
					Vector2 mouse = FakeMain.MouseWorld(player);

                    int x = (int)mouse.X - 9;
					int y = (int)mouse.Y - 20;
					if (Main.netMode != NetmodeID.MultiplayerClient)
					{
						int index = NPC.NewNPC(Terraria.Entity.GetSource_None(), x, y, NPCType<SuperDummyNPC>(),0, Main.LocalPlayer.GetModPlayer<RemnantPlayer>().DummyMode);
						Main.npc[index].releaseOwner = (short)player.whoAmI;
                    }
					else
					{

						int mode = Main.player[player.whoAmI].GetModPlayer<RemnantPlayer>().DummyMode;
						var netMessage = Mod.GetPacket();
						netMessage.Write((byte)RemnantOfTheAncientsModMessageType.SpawnSuperDummy);
						netMessage.Write(x);
						netMessage.Write(y);
						netMessage.Write(mode);
                        netMessage.Write(player.whoAmI);
                        netMessage.Send();
					}
				}
			}
			else
			{
				if (Main.myPlayer != player.whoAmI) return;
				
				if (Main.netMode == NetmodeID.SinglePlayer)
				{
					DeleteDummie();
				}
				else
				{
					var netMessage = Mod.GetPacket();
					netMessage.Write((byte)RemnantOfTheAncientsModMessageType.KillSuperDummy);
					netMessage.Send();
				}
			}
		}

		public static void DeleteDummie()
		{
			foreach (NPC npc in Main.ActiveNPCs)
			{
				if (npc.type == NPCType<SuperDummyNPC>() && npc.active)
				{
					npc.life = 0;
					npc.active = false;
					if (Main.netMode == NetmodeID.Server)
					{
						NetMessage.SendData(MessageID.SyncNPC, -1, -1, null, npc.whoAmI);
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
