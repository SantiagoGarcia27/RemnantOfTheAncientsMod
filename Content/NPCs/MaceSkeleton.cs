using Microsoft.Xna.Framework;
using PlayerProxyLib.Common.ProxyPlayer;
using RemnantOfTheAncientsMod.Common.UtilsTweaks;
using SangarUtilities.Common.UtilsTweaks;
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
		readonly int timmerMax = Utils1.FormatTimeToTick(Second: 4);


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
            NPC.UpdatePlayerProxy();
            if (Main.netMode == NetmodeID.MultiplayerClient) return;
			if (proxy == null || !proxy.active) Inicializar();

            if (proxy == null) return;

			NPC.TargetClosest();
			NPC.UpdatePlayerProxy();
			NPC.ConfigureProxyPlayer(shouldBeDrawn: false);

			if (NPC.target > -1)
			{
				Player target = Main.player[NPC.target];

				float distance = NPC.Center.DistanceSQ(target.Center);
				float distanceMin = 10.ToCoordinatePosition() * 10.ToCoordinatePosition();

                if (distance < distanceMin)
                {
                    proxy.SetMouseWorld(target.Center);


                    if (timmer <= 0 && proxy.ownedProjectileCounts[ProjectileID.Mace] == 0)
                    {
                        timmer = timmerMax;
                        proxy.controlUseItem = true;
                    }

                    timmer--;

                    if (timmer > MathUtils.GetValueFromPorcentage(timmerMax, 90))
                    {
                        proxy.controlUseItem = true;
                    }
                    else if(timmer > MathUtils.GetValueFromPorcentage(timmerMax, 70))
                    {
                        proxy.controlUseItem = false;
                    }
                    else if(timmer > 3)
                    {
                        proxy.controlUseItem = true;
                    }
                    else
                    {
                        proxy.controlUseItem = false;
                    }
                }
                else
                {
                    proxy.controlUseItem = false;
                }
            }
			base.AI();
		}

		public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
		{
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
			return SpawnCondition.Cavern.Chance * 0.01f;
		}

		private void Inicializar()
		{
            proxy = NPC.GetPlayerProxy();

            if (proxy == null) return;

            proxy.hostile = true;

            proxy.inventory[0].SetDefaults(ItemID.Mace);
			proxy.inventory[0].damage = NPC.GetAttackDamage_ScaledByStrength(proxy.inventory[0].damage);

            proxy.selectedItem = 0;
        }
		public override void OnSpawn(IEntitySource source)
		{
			Inicializar();
            base.OnSpawn(source);
		}
		public override void OnKill()
		{
			proxy?.DisposePlayerProxy();

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
			base.SendExtraAI(writer);
		}
		public override void ReceiveExtraAI(BinaryReader reader)
		{
			base.ReceiveExtraAI(reader);
		}
	}
}
