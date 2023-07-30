using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using RemnantOfTheAncientsMod.Content.Items.Tools.Utilidad;
using RemnantOfTheAncientsMod.Content.Items.Weapons.Magic;
using static Terraria.ModLoader.ModContent;
using Terraria.GameContent.Events;
using Terraria.GameContent;
using ReLogic.Content;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.Personalities;
using Terraria.Utilities;
using RemnantOfTheAncientsMod.World;
using Terraria.Audio;
using RemnantOfTheAncientsMod.Content.Dusts;
using RemnantOfTheAncientsMod.Content.Projectiles.BossProjectile;

namespace RemnantOfTheAncientsMod.Content.NPCs.Town
{

	[AutoloadHead]
	public class Time_Wizard : ModNPC
	{
		public override void SetStaticDefaults()
		{

			Main.npcFrameCount[Type] = Main.npcFrameCount[NPCID.Wizard];
			NPCID.Sets.ExtraFramesCount[NPC.type] = NPCID.Sets.ExtraFramesCount[NPCID.Wizard];
			NPCID.Sets.AttackFrameCount[NPC.type] = NPCID.Sets.AttackFrameCount[NPCID.Wizard];
			NPCID.Sets.DangerDetectRange[NPC.type] = 700;
			NPCID.Sets.AttackType[NPC.type] = NPCID.Sets.AttackType[NPCID.Wizard];
			NPCID.Sets.AttackTime[NPC.type] = 90;
			NPCID.Sets.AttackAverageChance[NPC.type] = 30;
			NPCID.Sets.HatOffsetY[NPC.type] = 4;
			//23 frames
			NPC.Happiness
				.SetBiomeAffection<HallowBiome>(AffectionLevel.Love)
				.SetBiomeAffection<DungeonBiome>(AffectionLevel.Love)
				.SetBiomeAffection<ForestBiome>(AffectionLevel.Like)
				.SetBiomeAffection<JungleBiome>(AffectionLevel.Like)
				.SetBiomeAffection<SnowBiome>(AffectionLevel.Dislike)
				.SetBiomeAffection<UndergroundBiome>(AffectionLevel.Dislike)
				.SetNPCAffection(NPCID.Wizard, AffectionLevel.Love)
				.SetNPCAffection(NPCID.Princess, AffectionLevel.Love)
				.SetNPCAffection(NPCID.Dryad, AffectionLevel.Like)
				.SetNPCAffection(NPCID.Clothier, AffectionLevel.Like)
				.SetNPCAffection(NPCID.Guide, AffectionLevel.Like)
				.SetNPCAffection(NPCID.Angler, AffectionLevel.Dislike)
				.SetNPCAffection(NPCID.BestiaryGirl, AffectionLevel.Hate)
				.SetNPCAffection(NPCID.Demolitionist, AffectionLevel.Hate);
			//DisplayName.SetDefault("Time Wizard");
			//DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Magicien du temps");
			//DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Mago del tiempo");

		}

		public override void SetDefaults()
		{
			NPC.townNPC = true;
			NPC.friendly = true;
			NPC.width = 18;
			NPC.height = 40;
			NPC.aiStyle = 7;
			NPC.damage = 10;
			NPC.defense = 15;
			NPC.lifeMax = 250;
			NPC.HitSound = SoundID.NPCHit1;
			NPC.DeathSound = SoundID.NPCDeath1;
			NPC.knockBackResist = 0.5f;
			AnimationType = NPCID.Wizard;
		}

		public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
		{
			bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {
			BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface,
			new FlavorTextBestiaryInfoElement("A ancient wizard that study the secrets of the time manipulation"),
			});
		}
		public override void HitEffect(NPC.HitInfo hit)
		{
			int num = NPC.life > 0 ? 1 : 5;
			for (int k = 0; k < num; k++)
			{
				Dust.NewDust(NPC.position, NPC.width, NPC.height, DustType<Sparkle>());
			}
		}
		public List<int> SpawnItems = new List<int>()
		{
			ItemID.GoldWatch,
			ItemID.PlatinumWatch,
			ItemID.GPS,
			ItemID.PDA,
			ItemID.CellPhone,
			ItemID.Shellphone
		};
		public override bool CanTownNPCSpawn(int numTownNPCs)
		{
			for (int k = 0; k < 255; k++)
			{
				Player player = Main.player[k];
				if (!player.active) continue;

				foreach (Item item in player.inventory)
				{
					if (SpawnItems.Contains(item.type))
					{
						if (NPC.downedBoss2)
						{
							return true;
						}
					}
				}
			}
			return false;
		}
		public override List<string> SetNPCNameList()
		{
			return new List<string>()
			{
				"Gand Alf",
				"Dio",
				"Gabriel",
				"Johy",
				"Harry"
			};
		}
		public override void OnChatButtonClicked(bool firstButton, ref string shop)
		{
			Player player = Main.LocalPlayer;
			if (firstButton)
			{
				shop = ShopName;
			}
			else
			{
				Main.sundialCooldown = 0;
				SoundEngine.PlaySound(SoundID.Item4, player.position);

				if (Main.netMode == NetmodeID.MultiplayerClient)
				{
					NetMessage.SendData(MessageID.MiscDataSync, number: Main.myPlayer, number2: 3f);
				}
				if (Main.dayTime) Main.fastForwardTimeToDusk = true;
				else Main.fastForwardTimeToDawn = true;
				NetMessage.SendData(MessageID.WorldData);
				RemanntWorld.TimeWizardTimeAcelerationCouldown = 108000;
			}
		}
		public override void AI()
		{
			if (RemanntWorld.TimeWizardTimeAcelerationCouldown <= 108000 && RemanntWorld.TimeWizardTimeAcelerationCouldown > 0)
			{
				RemanntWorld.TimeWizardTimeAcelerationCouldown--;
			}
		}

		public override string GetChat()
		{
			WeightedRandom<string> chat = new WeightedRandom<string>();
			int Wizard = NPC.FindFirstNPC(NPCID.Wizard);
			int Clothier = NPC.FindFirstNPC(NPCID.Clothier);

			if (Wizard >= 0 && Main.rand.NextBool(4)) chat.Add(formater(Language.GetTextValue("Mods.RemnantOfTheAncientsMod.Dialogue.Time_Wizard.WizardDialogue", Main.npc[Wizard].GivenName)));

			if (Main.dayTime)
			{
				chat.Add(formater(Language.GetTextValue("Mods.RemnantOfTheAncientsMod.Dialogue.Time_Wizard.DayDialogue1")), 4);
				chat.Add(formater(Language.GetTextValue("Mods.RemnantOfTheAncientsMod.Dialogue.Time_Wizard.DayDialogue2", Main.LocalPlayer.name)));
			}
			else
			{
				chat.Add(formater(Language.GetTextValue("Mods.RemnantOfTheAncientsMod.Dialogue.Time_Wizard.NightDialogue1")), 4);
				chat.Add(formater(Language.GetTextValue("Mods.RemnantOfTheAncientsMod.Dialogue.Time_Wizard.NightDialogue2", Main.LocalPlayer.name)));
			}

			if (NPC.downedBoss3 && Clothier >= 0) chat.Add(formater(Language.GetTextValue("Mods.RemnantOfTheAncientsMod.Dialogue.Time_Wizard.ClotierDialogue", Main.npc[Clothier].GivenName)));
			if (!NPC.downedQueenBee) chat.Add(formater(Language.GetTextValue("Mods.RemnantOfTheAncientsMod.Dialogue.Time_Wizard.NoBeeDefeatedDialogue1")), 4);
			if (Main.LocalPlayer.HasItem(ItemType<Judgment>())) chat.Add(formater(Language.GetTextValue("Mods.RemnantOfTheAncientsMod.Dialogue.Time_Wizard.JudmnetDialogue1")));
			chat.Add(formater(Language.GetTextValue("Mods.RemnantOfTheAncientsMod.Dialogue.Time_Wizard.GenericDialog1")));

			return chat;
		}
		public override void SetChatButtons(ref string button, ref string button2)
		{
			button = Language.GetTextValue("LegacyInterface.28");
			if (RemanntWorld.TimeWizardTimeAcelerationCouldown == 0)
			{
				button2 = Language.GetTextValue("Mods.RemnantOfTheAncientsMod.UI.Buttoms.SkipDay");
			}
		}
		private string formater(String imput)
		{
			string a = imput.Replace("\u00BF", "�").Replace("\u00A1", "�").Replace("\u00E1", "�").Replace("\u00ED", "�");
			return a;
		}
		//public bool FindItemInventoriPlayer(int ItemChoise)
		//{
		//	for (int k = 0; k < 255; k++)
		//	{
		//		Player player = Main.player[k];
		//		if (!player.active)
		//		{
		//			continue;
		//		}

		//		foreach (Item item in player.inventory)
		//		{
		//			if (item.type == ItemChoise) return true;
		//		}

		//	}
		//	return false;
		//}



		//public override void SetupShop(Chest shop, ref int nextSlot)
		//{
		//          AddShopItem(shop, nextSlot, ItemType<DayToken>(), Item.buyPrice(0, 1, 0, 0));
		//          nextSlot++;
		//          AddShopItem(shop, nextSlot, ItemType<NightToken>(), Item.buyPrice(0, 1, 0, 0));
		//          nextSlot++;

		//          if (Main.raining)
		//	{
		//              AddShopItem(shop, nextSlot, ItemType<RainToken>(), Item.buyPrice(0, 1, 0, 0));
		//              nextSlot++;
		//          }
		//	if (Sandstorm.Happening)
		//	{
		//              AddShopItem(shop, nextSlot, ItemType<SandstormToken>(), Item.buyPrice(0, 1, 0, 0));
		//              nextSlot++;
		//          }
		//	if (NPC.downedQueenBee)
		//	{
		//              AddShopItem(shop, nextSlot, ItemType<Judgment>(), Item.buyPrice(0, 2, 0, 0));
		//              nextSlot++;
		//          }
		//          if (NPC.FindFirstNPC(NPCID.GoblinTinkerer) >= 0)
		//          {
		//              AddShopItem(shop, nextSlot, ItemType<PlayerStatViewer>(), Item.buyPrice(0, 40, 0, 0));
		//              nextSlot++;
		//          }
		//	if (Main.hardMode)
		//	{
		//              AddShopItem(shop, nextSlot, ItemID.Sundial, Item.buyPrice(0, 20, 0, 0));
		//              nextSlot++;
		//          }
		public const string ShopName = "Shop";
		public override void AddShops()
		{
			var npcShop = new NPCShop(Type, ShopName);
			npcShop.Add(new Item(ItemType<DayToken>()) { shopCustomPrice = Item.buyPrice(0, 1, 0, 0) })
			.Add(new Item(ItemType<NightToken>()) { shopCustomPrice = Item.buyPrice(0, 1, 0, 0) })
			.Add(new Item(ItemType<RainToken>()) { shopCustomPrice = Item.buyPrice(0, 1, 0, 0) }, new Condition("Mods.RemnantOfTheAncientsMod.Conditions.IsRaining", () => Main.raining))
			.Add(new Item(ItemType<SandstormToken>()) { shopCustomPrice = Item.buyPrice(0, 1, 0, 0) }, new Condition("Mods.RemnantOfTheAncientsMod.Conditions.IsSandstorm", () => Sandstorm.Happening))
			.Add(new Item(ItemType<Judgment>()) { shopCustomPrice = Item.buyPrice(0, 2, 0, 0) }, new Condition("Mods.RemnantOfTheAncientsMod.Conditions.QueenBeeDefeated", () => NPC.downedQueenBee))
			.Add(new Item(ItemType<PlayerStatViewer>()) { shopCustomPrice = Item.buyPrice(0, 40, 0, 0) }, new Condition("Mods.RemnantOfTheAncientsMod.Conditions.TinkererAlive", () => NPC.FindFirstNPC(NPCID.GoblinTinkerer) >= 0))
			.Add(new Item(ItemID.Sundial) { shopCustomPrice = Item.buyPrice(0, 20, 0, 0) }, new Condition("Mods.RemnantOfTheAncientsMod.Conditions.IsHardmode", () => Main.hardMode))
			.Register();
		}

		//public void AddShopItem(Chest shop, int nextSlot, int item, int price)
		//{
		//    shop.item[nextSlot].SetDefaults(item);
		//    shop.item[nextSlot].shopCustomPrice = price;
		//}

		public override bool CanGoToStatue(bool toKingStatue) => true;

		public override void OnGoToStatue(bool toKingStatue)
		{
			if (Main.netMode == NetmodeID.Server)
			{
				ModPacket packet = Mod.GetPacket();
				packet.Write((byte)NPC.whoAmI);
				packet.Send();
			}
			else
			{
				StatueTeleport();
			}
		}

		public void StatueTeleport()
		{
			for (int i = 0; i < 30; i++)
			{
				Vector2 position = Main.rand.NextVector2Square(-20, 21);
				if (Math.Abs(position.X) > Math.Abs(position.Y))
				{
					position.X = Math.Sign(position.X) * 20;
				}
				else
				{
					position.Y = Math.Sign(position.Y) * 20;
				}
				Dust.NewDustPerfect(NPC.Center + position, DustType<Hell_Fire_P>(), Vector2.Zero).noGravity = true;
			}
		}
		public override ITownNPCProfile TownNPCProfile()
		{
			return new TimeWizardProfile();
		}

		public override void TownNPCAttackStrength(ref int damage, ref float knockback)
		{
			if (!Main.hardMode)
			{
				damage = 20;
				knockback = 4f;
			}
			else
			{
				damage = 80;
				knockback = 4f;
			}
		}

		public override void TownNPCAttackCooldown(ref int cooldown, ref int randExtraCooldown)
		{
			cooldown = 30;
			randExtraCooldown = 30;
		}

		public override void TownNPCAttackProj(ref int projType, ref int attackDelay)
		{
			if (!Main.hardMode)
			{
				projType = ProjectileID.BallofFrost;
				attackDelay = 1;
			}
			else
			{
				projType = ProjectileType<InfernalBallF_f>();
				attackDelay = 1;
			}
		}

		public override void TownNPCAttackProjSpeed(ref float multiplier, ref float gravityCorrection, ref float randomOffset)
		{
			multiplier = 12f;
			randomOffset = 2f;
		}
	}
	public class TimeWizardProfile : ITownNPCProfile
	{
		public int RollVariation() => 0;
		public string GetNameForVariant(NPC npc) => npc.getNewNPCName();

		public Asset<Texture2D> GetTextureNPCShouldUse(NPC npc)
		{
			if (npc.IsABestiaryIconDummy && !npc.ForcePartyHatOn)
				return Request<Texture2D>("RemnantOfTheAncientsMod/Content/NPCs/Town/Time_Wizard");

			if (npc.altTexture == 1)
				return Request<Texture2D>("RemnantOfTheAncientsMod/Content/NPCs/Town/Time_Wizard_Party");

			return Request<Texture2D>("RemnantOfTheAncientsMod/Content/NPCs/Town/Time_Wizard");
		}

		public int GetHeadTextureIndex(NPC npc) => GetModHeadSlot("RemnantOfTheAncientsMod/Content/NPCs/Town/Time_Wizard_Head");
	}
}

