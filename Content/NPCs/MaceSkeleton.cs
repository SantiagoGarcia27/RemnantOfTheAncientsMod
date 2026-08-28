using Microsoft.Xna.Framework;
using PlayerProxyLib.Common;
using RemnantOfTheAncientsMod.Common.UtilsTweaks;
using System.IO;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;

namespace RemnantOfTheAncientsMod.Content.NPCs
{
	public class MaceSkeleton : ModNPC
	{
		private Player proxy;
		private int timmer;
		readonly int timmerMax = Utils1.FormatTimeToTick(Second: 2);

		private int _maceId = -1;
		int MaceId
		{
			get => _maceId;
			set
			{
				_maceId = value;
				if (Main.netMode != NetmodeID.MultiplayerClient) NPC.netUpdate = true;
			}
		}

		public override void SetStaticDefaults()
		{

			Main.npcFrameCount[Type] = Main.npcFrameCount[NPCID.Skeleton];
			NPCID.Sets.NPCBestiaryDrawModifiers value = new()
			{
				Velocity = 1f
			};
			NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, value);
			NPC.spriteDirection = NPC.direction;
		}

		public override void SetDefaults()
		{
			NPC.CloneDefaults(NPCID.Skeleton);
			AIType = NPCID.Skeleton;
			AnimationType = NPCID.Skeleton;
			Banner = Item.NPCtoBanner(NPCID.Skeleton);
			BannerItem = Item.BannerToItem(Banner);
		}
		public override void AI()
		{
			if (proxy == null || !proxy.active) proxy = NPC.GetPlayerProxy();

			NPC.TargetClosest();
			NPC.UpdatePlayerProxy();
			NPC.ConfigureProxyPlayer(shouldBeDrawn: false);

			if (NPC.target > -1)
			{
				Player target = Main.player[NPC.target];

				float distance = NPC.Center.DistanceSQ(target.Center);
				float distanceMin = 10.ToCoordinatePosition() * 10.ToCoordinatePosition();
				if (proxy.ownedProjectileCounts[ProjectileID.Mace] < 1 && distance < distanceMin)
				{
					timmer = timmerMax;

					MaceId = Projectile.NewProjectile(NPC.GetSource_FromAI(), proxy.Center, Vector2.Zero, ProjectileID.Mace, 25, 0f, proxy.whoAmI);
					Main.projectile[MaceId].hostile = true;
					Main.projectile[MaceId].friendly = false;
				}
				else
				{
					if (MaceId == -1) return;

					if (timmer > 0) timmer--;
					else
					{
						if (proxy.ownedProjectileCounts[ProjectileID.Mace] >= 1)
						{
							proxy.channel = false;
							Main.projectile[MaceId].Kill();
							MaceId = -1;
						}

					}
				}
			}
			base.AI();
		}

		public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
		{
			// We can use AddRange instead of calling Add multiple times in order to add multiple items at once
			bestiaryEntry.Info.AddRange([
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface,
				new FlavorTextBestiaryInfoElement("A brave warrior with a powerfull mace"),
			]);
		}
		public override void ModifyNPCLoot(NPCLoot NPCLoot)
		{
			NPCLoot.Add(ItemDropRule.Common(ItemID.Mace, 90));
		}

		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			return SpawnCondition.Cavern.Chance * 0.01f; //* ModContent.GetInstance<ConfigClient1>().xdlevel; 
		}
		public override void OnSpawn(IEntitySource source)
		{
			proxy = NPC.GetPlayerProxy();
			base.OnSpawn(source);
		}
		public override void OnKill()
		{
			proxy?.DisposePlayerProxy();
			if (MaceId > -1) Main.projectile[MaceId].Kill();

			if (Main.netMode != NetmodeID.Server)
			{
				Gore.NewGore(NPC.GetSource_Death(), NPC.position, new Vector2(Main.rand.Next(-6, 7), Main.rand.Next(-6, 7)), 42, NPC.scale);
				Gore.NewGore(NPC.GetSource_Death(), NPC.position, new Vector2(Main.rand.Next(-6, 7), Main.rand.Next(-6, 7)), 43, NPC.scale);
				Gore.NewGore(NPC.GetSource_Death(), NPC.position, new Vector2(Main.rand.Next(-6, 7), Main.rand.Next(-6, 7)), 44, NPC.scale);
			}

			base.OnKill();
		}
		public override void SendExtraAI(BinaryWriter writer)
		{
			writer.Write(MaceId);
			base.SendExtraAI(writer);
		}
		public override void ReceiveExtraAI(BinaryReader reader)
		{
			MaceId = reader.ReadInt32();
			base.ReceiveExtraAI(reader);
		}
	}
}
