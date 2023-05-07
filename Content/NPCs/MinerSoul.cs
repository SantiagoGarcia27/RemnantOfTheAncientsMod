using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using RemnantOfTheAncientsMod.Content.Items.Tools;
using Terraria.Localization;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria.ModLoader.Utilities;
using System.Linq;
using Terraria.Utilities;

namespace RemnantOfTheAncientsMod.Content.NPCs
{
	// This NPC inherits from the Hover abstract class included in ExampleMod, which is a more customizable copy of the vanilla Hovering AI.
	// It implements the `CustomBehavior` and `ShouldMove` virtual methods being overridden here, as well as the `acceleration` and `accelerationY` field being set in the class constructor.
	public class MinerSoul : Hover
	{
		public MinerSoul()
		{
			acceleration = 0.06f;
			accelerationY = 0.025f;
		}

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Miner Soul");
			DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Alma minera");
			DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Âme de mineur");
			Main.npcFrameCount[NPC.type] = Main.npcFrameCount[NPCID.Wraith];
		}

		public override void SetDefaults()
		{
			NPC.CloneDefaults(NPCID.Wraith);
			NPC.width = 28;
			NPC.height = 36;
			NPC.aiStyle = -1;
			NPC.damage = 0;
			NPC.friendly = true;
			NPC.dontTakeDamageFromHostiles = true;
			AnimationType = NPCID.Wraith;
		}
		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			return SpawnCondition.Cavern.Chance * 0.09f;//0.0009
		}

		// Allows hitting the NPC with melee type weapons, even if it's friendly.
		public override bool? CanBeHitByItem(Player player, Item item)
		{
			return false;
		}

		// Same as the above but with projectiles.
		public override bool? CanBeHitByProjectile(Projectile projectile)
		{
			return projectile.friendly && projectile.owner < 255;
		}

		public override void HitEffect(int hitDirection, double damage)
		{
			if (NPC.life > 0)
			{
				for (int i = 0; i < damage / NPC.lifeMax * 100; i++)
				{
					Dust dust = Dust.NewDustDirect(NPC.position, NPC.width, NPC.height, DustID.Ghost, hitDirection, -1f, 100, new Color(100, 100, 100, 100), 1f);
					dust.noGravity = true;
				}
				return;
			}
			for (int i = 0; i < 50; i++)
			{
				Dust dust = Dust.NewDustDirect(NPC.position, NPC.width, NPC.height, DustID.Ghost, 2 * hitDirection, -2f, 100, new Color(100, 100, 100, 100), 1f);
				dust.noGravity = true;
			}
		}

		// Allows the NPC to talk with the player, even if it isn't a town NPC.
		public override bool CanChat()
		{
			return true;
		}

		public override string GetChat()
		{
			// NPC.SpawnedFromStatue value is kept when the NPC is transformed.
			WeightedRandom<string> chat = new WeightedRandom<string>();
			chat.Add(Language.GetTextValue("Mods.RemnantOfTheAncientsMod.Dialogue.MinerSoul.CommonDialogue1"));
			chat.Add(Language.GetTextValue("Mods.RemnantOfTheAncientsMod.Dialogue.MinerSoul.CommonDialogue2"));

            Main.LocalPlayer.currentShoppingSettings.HappinessReport = "";
            return chat;
		}

		public override void OnChatButtonClicked(bool firstButton, ref bool shop)
		{
			Player player = Main.LocalPlayer;
			if (firstButton)
			{
				shop = true;
			}
			else
			{
				// Hit the NPC for about 500 damage
				if (player.inventory.Any(item => !item.IsAir && item.pick < 70 && item.pick > 0))
				{
					Item item = player.inventory.FirstOrDefault(item => !item.IsAir && item.pick < 70 && item.pick > 0);
					Main.npcChatText = Language.GetTextValue("Mods.RemnantOfTheAncientsMod.Dialogue.MinerSoul.ThanksDialogue1");
					int IronPickaxeItemIndex = player.FindItem(item.type);
					player.inventory[IronPickaxeItemIndex].TurnToAir();
					player.QuickSpawnItem(NPC.GetSource_Loot(), ItemType<Ghost_Pickaxe>());
					player.QuickSpawnItem(NPC.GetSource_Loot(), ItemID.GoldCoin, 10);
					player.ApplyDamageToNPC(NPC, Main.DamageVar(500), 5f, player.direction, true);
					return;
				}

				
                if (player.HasItem(ItemID.EnchantedSword)) UpgradeItem(player, ItemID.EnchantedSword, 1, ItemID.Terragrim, 1, ItemID.GoldCoin, 50);

				if (Main.hardMode)
				{
					if (player.HasItem(ItemID.Terragrim)) UpgradeItem(player, ItemID.Terragrim, 1, ItemID.Arkhalis, 1, ItemID.GoldCoin, 100);
				}
            }
		}
		public void UpgradeItem(Player player,int input, int inputCount, int output, int outputCount, int money, int moneyCount)
		{
            Main.npcChatText = Main.npcChatText = Language.GetTextValue("Mods.RemnantOfTheAncientsMod.Dialogue.MinerSoul.ThanksDialogue2");
            int ItemIndex = player.FindItem(input);
            player.inventory[ItemIndex].TurnToAir();
            player.QuickSpawnItem(NPC.GetSource_Loot(), output, outputCount);
            player.QuickSpawnItem(NPC.GetSource_Loot(), money, moneyCount);
            player.ApplyDamageToNPC(NPC, Main.DamageVar(500), 5f, player.direction, true);
            return;
        }
		public override void SetChatButtons(ref string button, ref string button2)
		{
			button = Language.GetTextValue("LegacyInterface.28");

			if (Main.LocalPlayer.inventory.Any(item => !item.IsAir && item.pick < 70 && item.pick > 0))
			{
				button2 = Language.GetTextValue("Mods.RemnantOfTheAncientsMod.UI.Buttoms.DeliverPickaxe");
			}
			if (Main.LocalPlayer.HasItem(ItemID.EnchantedSword)) 
			{
				Item item = new(ItemID.EnchantedSword);
				button2 = Language.GetTextValue("Mods.RemnantOfTheAncientsMod.UI.Buttoms.UpgradeItem", item.Name);
			}

        }
		public override void SetupShop(Chest shop, ref int nextSlot)
		{
            AddShopItem(shop, nextSlot, ItemType<ironstonepickaxe>(), Item.buyPrice(0, 3, 0, 0));
            nextSlot++;
            AddShopItem(shop, nextSlot, ItemID.Torch, Item.buyPrice(0, 0, 0, 30));
            nextSlot++;
            AddShopItem(shop, nextSlot, ItemID.Wood, Item.buyPrice(0, 0, 0, 20));
            nextSlot++;
            AddShopItem(shop, nextSlot, ItemID.DirtBlock, Item.buyPrice(0, 0, 0, 2));
            nextSlot++;
            AddShopItem(shop, nextSlot, ItemID.StoneBlock, Item.buyPrice(0, 0, 0, 5));
            nextSlot++;

            if (WorldGen.SavedOreTiers.Copper == TileID.Tin) AddShopItem(shop, nextSlot, ItemID.CopperBar, Item.buyPrice(0, 0, 2, 0));
            if (WorldGen.SavedOreTiers.Copper == TileID.Copper) AddShopItem(shop, nextSlot, ItemID.TinBar, Item.buyPrice(0, 0, 2, 0));
            nextSlot++;

            if (WorldGen.SavedOreTiers.Iron == TileID.Lead)	AddShopItem(shop, nextSlot, ItemID.IronBar, Item.buyPrice(0, 0, 5, 0));
			if (WorldGen.SavedOreTiers.Iron == TileID.Iron) AddShopItem(shop, nextSlot, ItemID.LeadBar, Item.buyPrice(0, 0, 5, 0));
            nextSlot++;

            if (WorldGen.SavedOreTiers.Silver == TileID.Silver) AddShopItem(shop, nextSlot, ItemID.TungstenBar, Item.buyPrice(0, 0, 25, 0));
            if (WorldGen.SavedOreTiers.Silver == TileID.Tungsten) AddShopItem(shop, nextSlot, ItemID.SilverBar, Item.buyPrice(0, 0, 25, 0));
            nextSlot++;

            if (WorldGen.SavedOreTiers.Gold == TileID.Platinum) AddShopItem(shop, nextSlot, ItemID.GoldBar, Item.buyPrice(0, 0, 60, 0));	
			if (WorldGen.SavedOreTiers.Gold == TileID.Gold) AddShopItem(shop, nextSlot, ItemID.PlatinumBar, Item.buyPrice(0, 0, 70, 0));	
            nextSlot++;

			if (NPC.downedSlimeKing)
			{
                AddShopItem(shop, nextSlot, ItemID.Amethyst, Item.buyPrice(0, 0, 5, 0));
                nextSlot++;
                AddShopItem(shop, nextSlot, ItemID.Topaz, Item.buyPrice(0, 0, 5, 0));
                nextSlot++;
                AddShopItem(shop, nextSlot, ItemID.Sapphire, Item.buyPrice(0, 0, 10, 0));
                nextSlot++;
                AddShopItem(shop, nextSlot, ItemID.Emerald, Item.buyPrice(0, 0, 10, 0));
                nextSlot++;
                AddShopItem(shop, nextSlot, ItemID.Ruby, Item.buyPrice(0, 0, 15, 0));
                nextSlot++;
                AddShopItem(shop, nextSlot, ItemID.Diamond, Item.buyPrice(0, 0, 20, 0));
                nextSlot++;
            }

			if (Main.hardMode)
			{
				AddShopItem(shop, nextSlot, ItemID.PinkGel, Item.buyPrice(0, 0, 50, 0));
				nextSlot++;

                if (WorldGen.SavedOreTiers.Cobalt == TileID.Palladium) AddShopItem(shop, nextSlot, ItemID.CobaltOre, Item.buyPrice(0, 1, 0, 0));
                if (WorldGen.SavedOreTiers.Cobalt == TileID.Cobalt) AddShopItem(shop, nextSlot, ItemID.PalladiumOre, Item.buyPrice(0, 1, 0, 0));
                nextSlot++;

                if (WorldGen.SavedOreTiers.Mythril == TileID.Orichalcum) AddShopItem(shop, nextSlot, ItemID.MythrilOre, Item.buyPrice(0, 6, 0, 0));
                if (WorldGen.SavedOreTiers.Mythril == TileID.Mythril) AddShopItem(shop, nextSlot, ItemID.OrichalcumOre, Item.buyPrice(0, 6, 0, 0));
                nextSlot++;
            }
		}
		public void AddShopItem(Chest shop,int nextSlot, int item, int price)
		{
            shop.item[nextSlot].SetDefaults(item);
            shop.item[nextSlot].shopCustomPrice = price;
        }


		// Only show health bar of the NPC when close to the player
		public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
		{
			float distance = NPC.Distance(Main.player[NPC.target].Center);
			if (distance <= 200)
			{
				if (distance > 100)
				{
					// Make the health bar become smaller the farther away the NPC is.
					scale *= (100 - (distance - 100)) / 100;
				}
				return null;
			}
			return false;
		}

		// Make the NPC invisible when far away from the player.
		public override void CustomBehavior(ref float ai)
		{
			float distance = NPC.Distance(Main.player[NPC.target].Center);
			if (distance <= 250)
			{
				NPC.alpha = 100;
				if (distance > 100)
				{
					// Make the NPC fade out the farther away the NPC is.
					NPC.alpha += (int)(155 * ((distance - 100) / 150));
				}
				return;
			}
			NPC.alpha = 255;
		}

		// Make the NPC stop moving if it is close to the player.
		public override bool ShouldMove(float ai)
		{
			NPC.ai[2] = 0; // Prevents the NPC from stopping following their target.
			if (NPC.Distance(Main.player[NPC.target].Center) < 150f)
			{
				NPC.velocity *= 0.95f;
				if (Math.Abs(NPC.velocity.X) < 0.1f)
				{
					NPC.spriteDirection = Main.player[NPC.target].Center.X > NPC.Center.X ? 1 : -1;
					NPC.velocity.X = 0;
				}
				return false;
			}
			return true;
		}
	}

	public class PurificationPowder : GlobalProjectile
	{
		// Make purification powder transform wraiths into purified ghosts.
		public override void PostAI(Projectile projectile)
		{
			if (projectile.type != ProjectileID.PurificationPowder || Main.netMode == NetmodeID.MultiplayerClient)
			{
				return;
			}

			for (int i = 0; i < Main.maxNPCs; i++)
			{
				if (Main.npc[i].active && (Main.npc[i].type == NPCID.Wraith || Main.npc[i].type == NPCID.Ghost) && projectile.Hitbox.Intersects(Main.npc[i].Hitbox))
				{
					Main.npc[i].Transform(NPCType<MinerSoul>());
				}
			}
		}
	}
}
