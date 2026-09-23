using Microsoft.Xna.Framework;
using PlayerProxyLib.Common;
using System.IO;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.ModLoader;
using static FakePlayer_Setup;

namespace RemnantOfTheAncientsMod.Content.NPCs.FakePlayer
{
	public abstract class FakePlayer : ModNPC
	{
		private Player proxy;
		private FakePlayer_Attack attackModule { get; set; }
		private FakePlayer_Consumables consumableModule { get; set; }

        protected FakePlayerEquipmentList inventory { get; set; }
        private int selectedItem;
        private FakePlayer_Attack.VisualState visualState;
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
			NPC.lifeMax = 100;
			AIType = NPCID.Skeleton;
			AnimationType = NPCID.Skeleton;
        }

		protected virtual void Initialize()
		{
            proxy = NPC.GetPlayerProxy();
			if (proxy == null) return;
            proxy.name = "Rogue";
            proxy.hostile = true;
            inventory ??= new();

            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                FakePlayerEquipmentList inv = inventory;
                ModifyInventory(ref inv);
                inventory = inv;
                NPC.netUpdate = true;
            }
            else
            {
                ApplyInventoryToProxy();
            }

            CreateModules();
            proxy.selectedItem = selectedItem;
            SyncProxyStats();
            attackModule?.ApplyVisualState(visualState);
            initialized = true;

        }
        private bool initialized;

        public override bool PreAI()
		{
			// AIType runs before ModNPC.AI. Give the vanilla fighter AI a real target
			// up front so it can chase the player instead of idling around a proxy.
			if (Main.netMode != NetmodeID.MultiplayerClient)
				FindClosestRealPlayer();
			return true;
		}

        public override void AI()
		{
            if (!initialized)
            {
                Initialize();
            }

            proxy ??= NPC.GetPlayerProxy();
            if (proxy != null && NPC.active && !proxy.active)
                proxy.active = true;

            if (proxy == null || !proxy.active || proxy.whoAmI < 0 || proxy.whoAmI >= Main.player.Length || !ReferenceEquals(Main.player[proxy.whoAmI], proxy))
			{

                attackModule = null;
                consumableModule = null;
				proxy = NPC.GetPlayerProxy();
				if (proxy == null) return;
                initialized = false;
                Initialize();
                if (!initialized) return;
            }
            SyncProxyStats();
			NPC.UpdatePlayerProxy();
			NPC.ConfigureProxyPlayer(shouldBeDrawn: true);

			// NPC combat, items, and healing are owned by the server. Clients only
			// advance the visual state received in the NPC snapshot.
			if (Main.netMode == NetmodeID.MultiplayerClient)
			{
                attackModule?.ClientVisualAI();
                return;
            }

            if (attackModule == null || consumableModule == null)
                return;

			if (consumableModule.IsHealing)
            {
                consumableModule.HealingAI();
                return;
            }

            Player target = FindClosestRealPlayer();
			if (target != null)
			{
                int lifeLosed = proxy.statLifeMax2 - proxy.statLife;
				if (!proxy.inventory[9].IsAir && inventory.healPotion?.item is Item healingPotion && lifeLosed >= healingPotion.healLife && proxy.inventory[9].stack > 0)
				{
                    attackModule.StopAnimation();
                    consumableModule.HealingAI();
                    return;
				}

                attackModule.SetTarget(target);
                attackModule.AI();
                
            }
			
            base.AI();
		}

		private Player FindClosestRealPlayer()
		{
			Player closestPlayer = null;
			float closestDistanceSquared = float.MaxValue;

			foreach (Player player in Main.ActivePlayers)
			{
				if (player.dead || player.ghost || player.IsProxyPlayer() || (proxy != null && player.whoAmI == proxy.whoAmI))
					continue;

				float distanceSquared = Vector2.DistanceSquared(NPC.Center, player.Center);
				if (distanceSquared >= closestDistanceSquared)
					continue;

				closestDistanceSquared = distanceSquared;
				closestPlayer = player;
			}

			int newTarget = closestPlayer?.whoAmI ?? Main.maxPlayers;
			if (NPC.target != newTarget)
			{
				NPC.target = newTarget;
				if (Main.netMode != NetmodeID.MultiplayerClient)
					NPC.netUpdate = true;
			}

			return closestPlayer;
		}

		private void SyncProxyStats()
        {
			if (proxy == null) return;
            proxy.statLifeMax = NPC.lifeMax;
            proxy.statLifeMax2 = NPC.lifeMax;
            proxy.statLife = Utils.Clamp(NPC.life, 0, proxy.statLifeMax2);
            NPC.defense = proxy.statDefense;
        }
        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
		{
			// We can use AddRange instead of calling Add multiple times in order to add multiple items at once
			bestiaryEntry.Info.AddRange([
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface,
				new FlavorTextBestiaryInfoElement("A brave warrior with a powerfull mace"),
			]);
		}
        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position) => false;


        public virtual void ModifyInventory(ref FakePlayerEquipmentList inventory)
		{ 
            inventory ??= new();
			this.inventory = inventory;
			ApplyInventoryToProxy();
        }

		private void CreateModules()
		{
			if (proxy == null || inventory == null)
				return;

			if (attackModule == null)
                attackModule = new FakePlayer_Attack(proxy, NPC, inventory);
            else
                attackModule.SetInventory(inventory);

            consumableModule ??= new FakePlayer_Consumables(ref proxy, NPC, inventory);
		}

		private void ApplyInventoryToProxy()
		{
			if (proxy == null || inventory == null)
				return;

			//Armor
            proxy.armor[0] = inventory.armor[0]?.item ?? new Item();
            proxy.armor[1] = inventory.armor[1]?.item ?? new Item();
            proxy.armor[2] = inventory.armor[2]?.item ?? new Item();

            //Accessories
			proxy.armor[3] = inventory.accessories[0]?.item ?? new Item();
			proxy.armor[4] = inventory.accessories[1]?.item ?? new Item();
			proxy.armor[5] = inventory.accessories[2]?.item ?? new Item();
			proxy.armor[6] = inventory.accessories[3]?.item ?? new Item();
			proxy.armor[7] = inventory.accessories[4]?.item ?? new Item();

            //Extra Accessories
            proxy.armor[8] = inventory.accessories[5]?.item ?? new Item();
			proxy.armor[9] = inventory.accessories[6]?.item ?? new Item();
			

            //Weapons
            proxy.inventory[0] = inventory.meleeWeapon?.item ?? new Item();
			proxy.inventory[1] = inventory.rangedWeapon?.item ?? new Item();

            //Ammo
            proxy.inventory[54] = inventory.ammo[0]?.item ?? new Item();
			proxy.inventory[55] = inventory.ammo[1]?.item ?? new Item();
			proxy.inventory[56] = inventory.ammo[2]?.item ?? new Item();
			proxy.inventory[57] = inventory.ammo[3]?.item ?? new Item();

			//Potions
			proxy.inventory[9] = inventory.healPotion?.item ?? new Item();

			for (int i = 0; i < inventory.potions.Length; i++) {
				proxy.inventory[i + 10] = inventory.potions[i]?.item ?? new Item();
            }
        }

        public override void OnKill()
		{
			if (Main.netMode != NetmodeID.MultiplayerClient)
                attackModule?.Dispose();
            NPC.DisposePlayerProxy();
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
            writer.Write((byte)(proxy?.selectedItem ?? selectedItem));
            (inventory ?? new FakePlayerEquipmentList()).Write(writer);
            FakePlayer_Attack.WriteVisualState(writer, attackModule?.CaptureVisualState() ?? visualState);
            base.SendExtraAI(writer);
		}
		public override void ReceiveExtraAI(BinaryReader reader)
		{
            selectedItem = reader.ReadByte();
            inventory = FakePlayerEquipmentList.Read(reader);
            visualState = FakePlayer_Attack.ReadVisualState(reader);

            if (proxy != null)
            {
                proxy.selectedItem = selectedItem;
                ApplyInventoryToProxy();
                CreateModules();
                attackModule?.ApplyVisualState(visualState);
            }

            base.ReceiveExtraAI(reader);
		}
	}
}
