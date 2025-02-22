using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using Terraria.Localization;
using static Terraria.ModLoader.ModContent;
using Microsoft.Xna.Framework;
using RemnantOfTheAncientsMod.Content.NPCs;
using Terraria.Chat;
using CalamityMod;
using static RemnantOfTheAncientsMod.Netcode;

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
                RemnantPlayer.DummyMode = RemnantPlayer.DummyMode == 3 ? 0 : RemnantPlayer.DummyMode + 1;
 
				string Size = "Small";
				string Defense = "no";

				if(RemnantPlayer.DummyMode == 1 || RemnantPlayer.DummyMode == 3)
					Defense= "Player";
				else if(RemnantPlayer.DummyMode == 2 || RemnantPlayer.DummyMode == 3)
					Size = "Gigant";

				Main.NewText(Size + " with " + Defense+" defense");
			}
			else
			{
				CheckDummyAlive();
			}
			return true;
		}

		public void CheckDummyAlive()
		{
			Player player = Main.player[Main.myPlayer];

			if (!NPC.AnyNPCs(NPCType<SuperDummyNPC>()))
			{
				if (player.whoAmI == Main.myPlayer)
				{
					int x = (int)Main.MouseWorld.X - 9;
					int y = (int)Main.MouseWorld.Y - 20;
					if (Main.netMode == NetmodeID.SinglePlayer)
					{
						NPC.NewNPC(Terraria.Entity.GetSource_None(), x, y, NPCType<SuperDummyNPC>(),0, RemnantPlayer.DummyMode);
					}
					else
					{
						var netMessage = Mod.GetPacket();
						netMessage.Write((byte)RemnantOfTheAncientsModMessageType.SpawnSuperDummy);
						netMessage.Write(x);
						netMessage.Write(y);
						netMessage.Send();
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
						var netMessage = Mod.GetPacket();
						netMessage.Write((byte)RemnantOfTheAncientsModMessageType.KillSuperDummy);
						netMessage.Send();
					}
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
