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
		private FakePlayer_Consumables consumableModule { get; set; }

        FakePlayerEquipmentList inventory { get; set; }
		private int meleeWeaponIndex = -1;
        private int rangerWeaponIndex = -1;
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
            attackModule = new FakePlayer_Attack(proxy, NPC, inventory: inventory, ref meleeWeaponIndex, ref rangerWeaponIndex);
			consumableModule = new FakePlayer_Consumables(proxy, NPC, inventory: inventory);

            FakePlayerEquipmentList inv = inventory;

            ModifyInventory(ref inv);
            SyncProxyStats(spawn: true);

          
        }

        public override void AI()
		{
			if (proxy == null || !proxy.active) proxy = NPC.GetPlayerProxy();
            SyncProxyStats();
            NPC.TargetClosest();
			NPC.UpdatePlayerProxy();
			NPC.ConfigureProxyPlayer(shouldBeDrawn: true);

			if (NPC.target > -1)
			{
				Player target = Main.player[NPC.target];
				attackModule.SetTarget(target);
				attackModule.AI();
				consumableModule.AI();
            }
			

            base.AI();
		}

		private void  SyncProxyStats(bool spawn = false)
        {
            proxy.statLife = NPC.life;
			//NPC.life = proxy.statLife;
            NPC.defense = proxy.statDefense;
			if (spawn)
			{
				NPC.lifeMax = proxy.statLifeMax2;
				NPC.life = proxy.statLifeMax2;
            }
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
            proxy.inventory[0] = inventory.meleeWeapon.item ?? new Item();
			proxy.inventory[1] = inventory.rangedWeapon.item ?? new Item();

            //Ammo
            proxy.inventory[54] = inventory.ammo[0]?.item ?? new Item();
			proxy.inventory[55] = inventory.ammo[1]?.item ?? new Item();
			proxy.inventory[56] = inventory.ammo[2]?.item ?? new Item();
			proxy.inventory[57] = inventory.ammo[3]?.item ?? new Item();

			//Potions
			proxy.inventory[9] = inventory.healPotion?.item ?? new Item();

			for (int i = 0; i <= (inventory?.potions?.Length - 1 ?? 0); i++) {
				proxy.inventory[i + 10] = inventory.potions[i]?.item ?? new Item();
            }

            this.inventory = inventory;
        }
		
		public override void OnSpawn(IEntitySource source)
		{
            base.OnSpawn(source);
		}

      
        public override void OnKill()
		{
			
			if (meleeWeaponIndex > -1) Main.projectile[meleeWeaponIndex].Kill();
			if(rangerWeaponIndex > -1) Main.projectile[rangerWeaponIndex].Kill();
            if (Main.netMode != NetmodeID.Server)
			{
				Gore.NewGore(NPC.GetSource_Death(), NPC.position, new Vector2(Main.rand.Next(-6, 7), Main.rand.Next(-6, 7)), 42, NPC.scale);
				Gore.NewGore(NPC.GetSource_Death(), NPC.position, new Vector2(Main.rand.Next(-6, 7), Main.rand.Next(-6, 7)), 43, NPC.scale);
				Gore.NewGore(NPC.GetSource_Death(), NPC.position, new Vector2(Main.rand.Next(-6, 7), Main.rand.Next(-6, 7)), 44, NPC.scale);
			}
            base.OnKill();
		}

        public override void OnHitByProjectile(Projectile projectile, NPC.HitInfo hit, int damageDone)
        {
            PlayerDeathReason deathReason = PlayerDeathReason.ByProjectile(projectile.owner, projectile.whoAmI);
            if (NPC.life <= 0)
            {
                
                proxy.KillMe(deathReason,damageDone,hit.HitDirection,proxy.hostile);
                //proxy?.DisposePlayerProxy();
            }

			proxy.Hurt(deathReason, damageDone, hit.HitDirection, proxy.hostile, armorPenetration: projectile.ArmorPenetration, knockback: hit.Knockback);
			hit.Damage = 0;
        }
        public override void OnHitByItem(Player player, Item item, NPC.HitInfo hit, int damageDone)
        {
            PlayerDeathReason deathReason = PlayerDeathReason.ByPlayerItem(player.whoAmI, item);
            if (NPC.life <= 0)
            {
                
                proxy.KillMe(deathReason, damageDone, hit.HitDirection, proxy.hostile);
				//proxy?.DisposePlayerProxy();
            }
            proxy.Hurt(deathReason, damageDone, hit.HitDirection, proxy.hostile, armorPenetration: player.GetArmorPenetration(item.DamageType), knockback: hit.Knockback);
            hit.Damage = 0;
        }
        public override void SendExtraAI(BinaryWriter writer)
		{
			writer.Write(meleeWeaponIndex);
            writer.Write(rangerWeaponIndex);
            base.SendExtraAI(writer);
		}
		public override void ReceiveExtraAI(BinaryReader reader)
		{
            meleeWeaponIndex = reader.ReadInt32();
            rangerWeaponIndex = reader.ReadInt32();
            base.ReceiveExtraAI(reader);
		}
	}
}
