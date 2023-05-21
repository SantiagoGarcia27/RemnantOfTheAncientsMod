using RemnantOfTheAncientsMod.Content.Items.Accesories;
using RemnantOfTheAncientsMod.Content.Items.Consumables.BossSummon;
using RemnantOfTheAncientsMod.Content.Items.Consumables.ReaperSouls;
using RemnantOfTheAncientsMod.World;
using System;
using System.Collections;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Common.Global
{
	public class ReaperGlobalItem : GlobalItem
	{
		public override bool InstancePerEntity => true; //InstancePerEntity
		//public static bool Fscale = false;
		public static float originalsize = 1;
		public float newsize = 0;

		public override void ModifyItemScale(Item item, Player player, ref float scale)
		{
			RemnantPlayer modPlayer = player.GetModPlayer<RemnantPlayer>();

			if (modPlayer.ChaliceOn && Reaper.ReaperMode && !item.IsAir && item.damage > 0 && !item.noMelee && item.active)
			{
				scale *= 2.5f;
			}
		}
		public static void ReaperSetDefaut(Item item)
		{
			if (item.type == ModContent.ItemType<ReaperChalice>())
			{
				item.accessory = Reaper.ReaperMode;
			}
		}
		static List<int> BossSummons = new List<int>();
		public static void ReaperConsumable(Item item)
		{
			AddBossSummon();
			if (RemnantOfTheAncientsMod.ThoriumMod != null) AddBossSummonThorium();

			if (RemnantOfTheAncientsMod.CalamityMod == null)
			{
				//if (item.type == ItemID.SlimeCrown ||
				//	item.type == ItemID.SuspiciousLookingEye ||
				//	item.type == ItemID.WormFood ||
				//	item.type == ItemID.BloodySpine ||
				//	item.type == ItemID.Abeemination ||
				//	item.type == ItemID.MechanicalEye ||
				//	item.type == ItemID.MechanicalWorm ||
				//	item.type == ItemID.MechanicalSkull ||
				//	item.type == ItemID.CelestialSigil ||
				//	item.type == ModContent.ItemType<DesertChest>() ||
				//	item.type == ModContent.ItemType<FrozenArtifact>() ||
				//	item.type == ModContent.ItemType<InfernalCalis>())
				//{

				if (BossSummons.Contains(item.type))
				{
					item.consumable = !Reaper.ReaperMode;
					item.GetGlobalItem<CustomTooltip>().BossSummon = Reaper.ReaperMode;
				}
			}
			else
			{
				if (BossSummons.Contains(item.type))
				{
					item.consumable = false;
				}
			}
		}

		public static void AddBossSummon()
		{
            SafeAddContentToList(ItemID.SlimeCrown);
            SafeAddContentToList(ItemID.SuspiciousLookingEye);
            SafeAddContentToList(ItemID.WormFood);
            SafeAddContentToList(ItemID.BloodySpine);
            SafeAddContentToList(ItemID.Abeemination);
            SafeAddContentToList(ItemID.MechanicalEye);
            SafeAddContentToList(ItemID.MechanicalWorm);
            SafeAddContentToList(ItemID.MechanicalSkull);
            SafeAddContentToList(ItemID.CelestialSigil);
            SafeAddContentToList(ModContent.ItemType<DesertChest>());
            SafeAddContentToList(ModContent.ItemType<FrozenArtifact>());
            SafeAddContentToList(ModContent.ItemType<InfernalCalis>());
        }
        [JITWhenModsEnabled("ThoriumMod")]
        public static void AddBossSummonThorium()
		{	
			SafeAddContentToList(Utils1.GetItemFromMod(RemnantOfTheAncientsMod.ThoriumMod, "StormFlare"));
			SafeAddContentToList(Utils1.GetItemFromMod(RemnantOfTheAncientsMod.ThoriumMod, "JellyfishResonator"));	
            SafeAddContentToList(Utils1.GetItemFromMod(RemnantOfTheAncientsMod.ThoriumMod, "UnstableCore"));
            SafeAddContentToList(Utils1.GetItemFromMod(RemnantOfTheAncientsMod.ThoriumMod, "AncientBlade"));
            SafeAddContentToList(Utils1.GetItemFromMod(RemnantOfTheAncientsMod.ThoriumMod, "StarCaller"));
            SafeAddContentToList(Utils1.GetItemFromMod(RemnantOfTheAncientsMod.ThoriumMod, "StriderTear"));
            SafeAddContentToList(Utils1.GetItemFromMod(RemnantOfTheAncientsMod.ThoriumMod, "VoidLens"));
            SafeAddContentToList(Utils1.GetItemFromMod(RemnantOfTheAncientsMod.ThoriumMod, "DoomSayersCoin"));
        }
		public static void SafeAddContentToList(int id)
		{
            if (!BossSummons.Contains(id)) BossSummons.Add(id);
        }
		public static void ReaperWingsNerf(Player player)
		{
			int OgWingTime;
			if (Reaper.ReaperMode)
			{
				if (player.wingTimeMax >= 4)
				{
					OgWingTime = player.wingTimeMax;
					if (!player.GetModPlayer<RetinazorReaperSoulPlayer>().RetinazorReaperUpgrade || !player.GetModPlayer<EmpressReaperSoulPlayer>().EmpressReaperUpgrade) player.wingTimeMax = OgWingTime / 2;
					else player.wingTimeMax = OgWingTime;
				}
			}
		}
	}
	public class FchangesNpc : GlobalNPC
	{
		public override void SetDefaults(NPC npc)
		{
			if (npc.boss) ModifyBossStats(npc, ref npc.damage, npc.lifeMax);
		}
		public static void ModifyBossStats(NPC npc, ref int damage, int lifeMax)
		{
			npc.lifeMax = (int)NpcChanges1.ExpertLifeScale(lifeMax);
			npc.damage = (int)NpcChanges1.ExpertDamageScale(damage);
		}

	}
}
