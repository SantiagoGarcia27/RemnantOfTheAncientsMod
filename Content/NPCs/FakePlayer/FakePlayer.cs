using Microsoft.Xna.Framework;
using PlayerProxyLib.Common;
using RemnantOfTheAncientsMod.Content.Items.Armor.Cosmetic.Strawberry;
using System.IO;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;
using static FakePlayer_Setup;

namespace RemnantOfTheAncientsMod.Content.NPCs.FakePlayer
{
	public abstract class FakePlayer : ModNPC
	{
		private Player proxy;
		private FakePlayer_Attack attackModule { get; set; }

        FakePlayerEquipmentList inventory { get; set; }
		private int meleeWeaponIndex = -1;

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
			NPC.damage = 0;
			AIType = NPCID.Skeleton;
			AnimationType = NPCID.Skeleton;

			Initialize();
        }

		protected virtual void Initialize()
		{
            proxy = NPC.GetPlayerProxy();
            proxy.name = "Rogue";
            proxy.hostile = true;
            inventory = new();
            attackModule = new FakePlayer_Attack(proxy, NPC, inventory: inventory, ref meleeWeaponIndex);

            FakePlayerEquipmentList inv = inventory;

            ModifyInventory(ref inv);

        }

        public override void AI()
		{
			if (proxy == null || !proxy.active) proxy = NPC.GetPlayerProxy();

			NPC.TargetClosest();
			NPC.UpdatePlayerProxy();
			NPC.ConfigureProxyPlayer(shouldBeDrawn: true);

			if (NPC.target > -1)
			{
				Player target = Main.player[NPC.target];
				attackModule.SetTarget(target);
				attackModule.AI();

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



		public virtual void ModifyInventory(ref FakePlayerEquipmentList inventory)
		{
			inventory ??= new();

            proxy.armor[0] = inventory.armor[0]?.item;
            proxy.armor[1] = inventory.armor[1]?.item;
            proxy.armor[2] = inventory.armor[2]?.item;

            this.inventory = inventory;
        }
		
		public override void OnSpawn(IEntitySource source)
		{
			


            

			/*proxy.armor[0] = inventory.armor[0].item;
            proxy.armor[1] = inventory.armor[1].item;
            proxy.armor[2] = inventory.armor[2].item;*/

            base.OnSpawn(source);
		}
		public override void OnKill()
		{
			proxy?.DisposePlayerProxy();
			if (meleeWeaponIndex > -1) Main.projectile[meleeWeaponIndex].Kill();

			/*if (Main.netMode != NetmodeID.Server)
			{
				Gore.NewGore(NPC.GetSource_Death(), NPC.position, new Vector2(Main.rand.Next(-6, 7), Main.rand.Next(-6, 7)), 42, NPC.scale);
				Gore.NewGore(NPC.GetSource_Death(), NPC.position, new Vector2(Main.rand.Next(-6, 7), Main.rand.Next(-6, 7)), 43, NPC.scale);
				Gore.NewGore(NPC.GetSource_Death(), NPC.position, new Vector2(Main.rand.Next(-6, 7), Main.rand.Next(-6, 7)), 44, NPC.scale);
			}*/

			base.OnKill();
		}
		public override void SendExtraAI(BinaryWriter writer)
		{
			writer.Write(meleeWeaponIndex);
			base.SendExtraAI(writer);
		}
		public override void ReceiveExtraAI(BinaryReader reader)
		{
            meleeWeaponIndex = reader.ReadInt32();
			base.ReceiveExtraAI(reader);
		}
	}
}
