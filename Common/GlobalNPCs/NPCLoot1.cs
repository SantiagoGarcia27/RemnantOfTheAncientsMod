using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using opswordsII;
//using ExampleMod.Content.Items;
//using ExampleMod.Common.ItemDropRules.Conditions;

namespace opswordsII.Common.GlobalNPCs
{
	// This file shows numerous examples of what you can do with the extensive NPC Loot lootable system. 
	// Despite this file being GlobalNPC, everything here can be used with a ModNPC as well! See examples of this in the Content/NPCs folder.
	public class ExampleNPCLoot : GlobalNPC
	{
		// ModifyNPCLoot uses a unique system called the ItemDropDatabase, which has many different rules for many different drop use cases.
		// Here we go through all of them, and how they can be used.
		// There are tons of other examples in vanilla! In a decompiled vanilla build, GameContent/ItemDropRules/ItemDropDatabase adds item drops to every single vanilla NPC, which can be a good resource.

		public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot) {
			if (!NPCID.Sets.CountsAsCritter[npc.type]) {
				npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Items.accesorios.The_Legion>(), 500000));
			}
			if (npc.type == NPCID.BirdRed || npc.type == NPCID.BirdBlue) 
			{
				npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Items.accesorios.Alas.BirdWings>(), 500)); ;
			}
			if (npc.type == NPCID.Shark)
			{
				npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<opswordsII.Items.Items.SharkTooth>(), 3/10)); ;
			}
			if (npc.type == NPCID.MoonLordCore)
			{
				npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<opswordsII.Items.Magic.LaserMachineGun>(), 50)); ;
			}
			if (npc.type == NPCID.Mechanic)
			{
			//	npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<opswordsII.Items.Lanzables.LlaveInglesa>(), 100/100)); ;
			}
			if (npc.type == NPCID.Tim)
			{
				npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<opswordsII.Items.accesorios.magic_stick>(), 2)); ;
			}
			if (npc.type == NPCID.DungeonGuardian)
			{
				npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<opswordsII.Items.pets.TortugaPet>(), 7 / 10)); ;
			}
			if (npc.type == ModContent.NPCType<opswordsII.NPCs.DAniquilator.DesertAniquilator>())
			{
				npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<opswordsII.Items.Armor.Masks.DesertAMask>(), 20 / 100));
				npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<opswordsII.Items.Bloques.DesertTrophy>(), 10 / 100));
			}
			if (npc.type == ModContent.NPCType <opswordsII.NPCs.frozen_assaulter.frozen_assaulter>())
			{
				npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<opswordsII.Items.Armor.Masks.FrozenMask>(), 20 / 100));
				npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<opswordsII.Items.Bloques.FrostTrophy>(), 10 / 100));
			}
			if (npc.type == ModContent.NPCType<opswordsII.NPCs.ITyrant.InfernalTyrantHead>())
			{
				npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<opswordsII.Items.Armor.Masks.InfernalMask>(), 20 / 100));
				npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<opswordsII.Items.Bloques.InfernalTrophy>(), 10 / 100));
			}
			if (npc.type == ModContent.NPCType < opswordsII.NPCs.zombis.Copperzombie>())
			{
				npcLoot.Add(ItemDropRule.Common(ItemID.CopperOre, 100/100));
				npcLoot.Add(ItemDropRule.Common(ItemID.CopperBar, 1 / 100));
			}
			if (npc.type == ModContent.NPCType<opswordsII.NPCs.zombis.Tinzombie>())
			{
				npcLoot.Add(ItemDropRule.Common(ItemID.TinOre, 100 / 100));
				npcLoot.Add(ItemDropRule.Common(ItemID.TinBar, 1 / 100));
			}
			if (npc.type == ModContent.NPCType<opswordsII.NPCs.zombis.Ironzombie>())
			{
				npcLoot.Add(ItemDropRule.Common(ItemID.IronOre, 100 / 100));
				npcLoot.Add(ItemDropRule.Common(ItemID.IronBar, 1 / 100));
			}
			if (npc.type == ModContent.NPCType<opswordsII.NPCs.zombis.Leadzombie>())
			{
				npcLoot.Add(ItemDropRule.Common(ItemID.LeadOre, 100 / 100));
				npcLoot.Add(ItemDropRule.Common(ItemID.LeadBar, 1 / 100));
			}
			if (npc.type == ModContent.NPCType<opswordsII.NPCs.zombis.Silverzombie>())
			{
				npcLoot.Add(ItemDropRule.Common(ItemID.SilverOre, 100 / 100));
				npcLoot.Add(ItemDropRule.Common(ItemID.SilverBar, 1 / 100));
			}
			if (npc.type == ModContent.NPCType<opswordsII.NPCs.zombis.Tungstenzombie>())
			{
				npcLoot.Add(ItemDropRule.Common(ItemID.TungstenOre, 100 / 100));
				npcLoot.Add(ItemDropRule.Common(ItemID.TungstenBar, 1 / 100));
			}
			if (npc.type == ModContent.NPCType<opswordsII.NPCs.zombis.Goldzombie>())
			{
				npcLoot.Add(ItemDropRule.Common(ItemID.GoldOre, 100 / 100));
				npcLoot.Add(ItemDropRule.Common(ItemID.GoldBar, 1 / 100));
			}
			if (npc.type == ModContent.NPCType<opswordsII.NPCs.zombis.Platinumzombie>())
			{
				npcLoot.Add(ItemDropRule.Common(ItemID.PlatinumOre, 100 / 100));
				npcLoot.Add(ItemDropRule.Common(ItemID.PlatinumBar, 1 / 100));
			}
			if (npc.type == ModContent.NPCType<opswordsII.NPCs.zombis.Tuxonitezombie>())
			{
				npcLoot.Add(ItemDropRule.Common(ModContent.ItemType < opswordsII.Items.Items.TuxoniteOre>(), 100 / 100));
				npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<opswordsII.Items.Items.TuxoniteBar>(), 1 / 100));
			}
			if (npc.type == ModContent.NPCType<opswordsII.NPCs.zombis.Armoredzombie>())
			{
				npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<opswordsII.Items.Items.Reinforced_iron_ore>(), 100 / 100));
				npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<opswordsII.Items.Items.Reinforced_ironBar>(), 1 / 100));
			}
			if (npc.type == ModContent.NPCType<opswordsII.NPCs.zombis.Shadowzombie>())
			{
				npcLoot.Add(ItemDropRule.Common(ItemID.DemoniteOre, 1));
				npcLoot.Add(ItemDropRule.Common(ItemID.DemoniteBar, 100));
			}
			if (npc.type == ModContent.NPCType<opswordsII.NPCs.zombis.Crimsonzombi>())
			{
				npcLoot.Add(ItemDropRule.Common(ItemID.CrimtaneOre, 1));
				npcLoot.Add(ItemDropRule.Common(ItemID.CrimtaneBar, 100));
			}
			if (npc.type == ModContent.NPCType<opswordsII.NPCs.ArmoredSlime>())
			{
				npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<opswordsII.Items.Items.Reinforced_iron_ore>(), 100 / 100));
				npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<opswordsII.Items.Items.Reinforced_ironBar>(), 1 / 100));
			}
		}

		// ModifyGlobalLoot allows you to modify loot that every NPC should be able to drop, preferably with a condition.
		// Vanilla uses this for the biome keys, souls of night/light, as well as the holiday drops.
		// Any drop rules in ModifyGlobalLoot should only run once. Everything else should go in ModifyNPCLoot.
		public override void ModifyGlobalLoot(GlobalLoot globalLoot) {
			//globalLoot.Add(ItemDropRule.ByCondition(new Conditions.IsMasterMode(), ModContent.ItemType<ExampleSoul>(), 5, 1, 1)); // If the world is in master mode, drop ExampleSouls 20% of the time from every npc.
			//TODO: Make it so it only drops from enemies in ExampleBiome when that gets made.
		}
	}
}
