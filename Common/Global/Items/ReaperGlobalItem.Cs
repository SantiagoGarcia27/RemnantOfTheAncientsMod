using RemnantOfTheAncientsMod.Common.ModCompativilitie;
using RemnantOfTheAncientsMod.Common.UtilsTweaks;
using RemnantOfTheAncientsMod.Content.Items.Accesories;
using RemnantOfTheAncientsMod.Content.Items.Consumables.BossSummon;
using RemnantOfTheAncientsMod.World;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Common.Global.Items
{
	public class ReaperGlobalItem : GlobalItem
	{
		public override bool InstancePerEntity => true; //InstancePerEntity
		//public static bool Fscale = false;
		public static float originalsize = 1;
		public float newsize = 0;

		public override void ModifyItemScale(Item item, Player player, ref float scale)
		{
			ReaperPlayer modPlayer = player.GetModPlayer<ReaperPlayer>();

			if (modPlayer.ChaliceOn && DificultyUtils.ReaperMode && !item.IsAir && item.damage > 0 && item.CountsAsClass(DamageClass.Melee) && item.active)
			{
				scale *= 2.5f;
			}
			base.ModifyItemScale(item, player,ref scale);

        }
		public static void ReaperSetDefaut(Item item)
		{
			if (item.type == ModContent.ItemType<ReaperChalice>())
			{
				item.accessory = DificultyUtils.ReaperMode;
			}
		}
		static List<int> BossSummons = new List<int>();
		public static void ReaperConsumable(Item item)
		{
			AddEventSummon();
            AddBossSummon();
			if (RemnantOfTheAncientsMod.ThoriumMod != null) AddBossSummonThorium();

			if (RemnantOfTheAncientsMod.CalamityMod == null)
			{			
				if (BossSummons.Contains(item.type))
				{
					item.consumable = !DificultyUtils.ReaperMode;
					item.GetGlobalItem<CustomTooltip>().BossSummon = DificultyUtils.ReaperMode;
				}
			}
			else
			{
				if (BossSummons.Contains(item.type))
				{
					item.consumable = false;
                    item.GetGlobalItem<CustomTooltip>().BossSummon = DificultyUtils.ReaperMode;
                }
			}
		}
        public override void OnConsumeItem(Item item, Player player)
        {
            if (DificultyUtils.ReaperMode)
            {
                if (item.type == ItemID.LihzahrdPowerCell)
                {
                    Main.player[Main.myPlayer].QuickSpawnItem(item.GetSource_ReleaseEntity(), item.type, 1);
                }
                if (item.type == ItemID.DD2ElderCrystal)
                {
                    Main.player[Main.myPlayer].QuickSpawnItem(item.GetSource_ReleaseEntity(), item.type, 1);
                }
            }
            base.OnConsumeItem(item, player);
        }
        public static void AddEventSummon()
		{
            SafeAddContentToList(ItemID.BloodMoonStarter);
            SafeAddContentToList(ItemID.GoblinBattleStandard);
            SafeAddContentToList(ItemID.DD2ElderCrystal);
            SafeAddContentToList(ItemID.SnowGlobe);
            SafeAddContentToList(ItemID.SolarTablet);
            SafeAddContentToList(ItemID.PirateMap);
            SafeAddContentToList(ItemID.PumpkinMoonMedallion);
            SafeAddContentToList(ItemID.NaughtyPresent);
        }
		public static void AddBossSummon()
		{
            SafeAddContentToList(ItemID.SlimeCrown);
            SafeAddContentToList(ItemID.SuspiciousLookingEye);
            SafeAddContentToList(ItemID.WormFood);
            SafeAddContentToList(ItemID.BloodySpine);
            SafeAddContentToList(ItemID.Abeemination);
            SafeAddContentToList(ItemID.DeerThing);
            SafeAddContentToList(ItemID.GuideVoodooDoll);
            SafeAddContentToList(ItemID.QueenSlimeCrystal);
            SafeAddContentToList(ItemID.MechanicalEye);
            SafeAddContentToList(ItemID.MechanicalWorm);
            SafeAddContentToList(ItemID.EmpressButterfly);
            SafeAddContentToList(ItemID.TruffleWorm);
            SafeAddContentToList(ItemID.MechanicalSkull);
            SafeAddContentToList(ItemID.CelestialSigil);
            SafeAddContentToList(ModContent.ItemType<DesertChest>());
            SafeAddContentToList(ModContent.ItemType<FrozenArtifact>());
            SafeAddContentToList(ModContent.ItemType<InfernalCalis>());
            SafeAddContentToList(ModContent.ItemType<ElderSpeaker>());
        }
		[JITWhenModsEnabled("ThoriumMod")]
		public static void AddBossSummonThorium()
		{
			SafeAddContentToList(ExternalModCallUtils.GetItemFromMod(RemnantOfTheAncientsMod.ThoriumMod, "StormFlare"));
			SafeAddContentToList(ExternalModCallUtils.GetItemFromMod(RemnantOfTheAncientsMod.ThoriumMod, "JellyfishResonator"));
			SafeAddContentToList(ExternalModCallUtils.GetItemFromMod(RemnantOfTheAncientsMod.ThoriumMod, "UnstableCore"));
			SafeAddContentToList(ExternalModCallUtils.GetItemFromMod(RemnantOfTheAncientsMod.ThoriumMod, "AncientBlade"));
			SafeAddContentToList(ExternalModCallUtils.GetItemFromMod(RemnantOfTheAncientsMod.ThoriumMod, "StarCaller"));
			SafeAddContentToList(ExternalModCallUtils.GetItemFromMod(RemnantOfTheAncientsMod.ThoriumMod, "StriderTear"));
			SafeAddContentToList(ExternalModCallUtils.GetItemFromMod(RemnantOfTheAncientsMod.ThoriumMod, "VoidLens"));
			SafeAddContentToList(ExternalModCallUtils.GetItemFromMod(RemnantOfTheAncientsMod.ThoriumMod, "DoomSayersCoin"));
		}
		public static void SafeAddContentToList(int id)
		{
            if (!BossSummons.Contains(id)) BossSummons.Add(id);
        }
		public static void ReaperWingsNerf(Player player)
		{
			int OgWingTime;
			if (DificultyUtils.ReaperMode)
			{
				if (player.wingTimeMax >= 4)
				{
					OgWingTime = player.wingTimeMax;
					if (!player.GetModPlayer<ReaperPlayer>().SoulsUpgrades[11] || !player.GetModPlayer<ReaperPlayer>().SoulsUpgrades[17]) player.wingTimeMax = OgWingTime / 2;
					else player.wingTimeMax = OgWingTime;
				}
			}
		}
	}
}
