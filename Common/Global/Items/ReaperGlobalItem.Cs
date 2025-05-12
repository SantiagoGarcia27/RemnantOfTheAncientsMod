using RemnantOfTheAncientsMod.Common.ModCompativilitie;
using RemnantOfTheAncientsMod.Common.UtilsTweaks;
using RemnantOfTheAncientsMod.Content.Items.Accesories;
using RemnantOfTheAncientsMod.Content.Items.Consumables.BossSummon;
using RemnantOfTheAncientsMod.Content.Items.Tools.Utilidad;
using RemnantOfTheAncientsMod.World;
using SangarUtilities.Common.UtilsTweaks;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Common.Global.Items
{
	public class ReaperGlobalItem : GlobalItem
	{
		public override bool InstancePerEntity => true; //InstancePerEntity
		public static float originalsize = 1;
		public float newsize = 0;

		public readonly float ReaperWeaponScale = 2.5f;
		public static float currentScale = 1; 

		public override void ModifyItemScale(Item item, Player player, ref float scale)
		{
			ReaperPlayer modPlayer = player.GetModPlayer<ReaperPlayer>();

			if (modPlayer.ChaliceOn && Reaper.ReaperMode && !item.IsAir && item.damage > 0 && item.CountsAsClass(DamageClass.Melee) && item.active)
			{
                currentScale = ReaperWeaponScale;
                scale *= ReaperWeaponScale;
			}
			else
			{
                currentScale = 1;

            }
			base.ModifyItemScale(item, player,ref scale);

        }
		public static void ReaperSetDefaut(Item item)
		{
			if (item.type == ModContent.ItemType<ReaperChalice>())
			{
				item.accessory = Reaper.ReaperMode;
			}
		}
		static List<int> BossSummons = [];
		public static void ReaperConsumable(Item item, Player player)
		{
			AddEventSummon();
            AddBossSummon();
			if (RemnantOfTheAncientsMod.ThoriumMod != null) AddBossSummonThorium();

			if (RemnantOfTheAncientsMod.CalamityMod == null)
			{			
				if (BossSummons.Contains(item.type))
				{

					bool consumable = !Reaper.ReaperMode;
					if(consumable == true)
                        consumable = !Utils1.playerHaveItem(player, ModContent.ItemType<BossSummonSaver>());

                    item.GetGlobalItem<CustomTooltip>().BossSummon = Reaper.ReaperMode;
					item.consumable = consumable;



                }
			}
			else
			{
				if (BossSummons.Contains(item.type))
				{
					item.consumable = false;
                    item.GetGlobalItem<CustomTooltip>().BossSummon = Reaper.ReaperMode;
                }
			}
		}
        public override void OnConsumeItem(Item item, Player player)
        {
            if (Reaper.ReaperMode)
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
            SafeAddContentToList(ModContent.ItemType<DesertGlyph>());
            SafeAddContentToList(ModContent.ItemType<FrozenArtifact>());
            SafeAddContentToList(ModContent.ItemType<InfernalCalis>());
            SafeAddContentToList(ModContent.ItemType<ElderSpeaker>());
        }
		[JITWhenModsEnabled("ThoriumMod")]
		public static void AddBossSummonThorium()
		{
			SafeAddContentToList(CallUtils.GetItemFromMod(RemnantOfTheAncientsMod.ThoriumMod, "StormFlare"));
			SafeAddContentToList(CallUtils.GetItemFromMod(RemnantOfTheAncientsMod.ThoriumMod, "JellyfishResonator"));
			SafeAddContentToList(CallUtils.GetItemFromMod(RemnantOfTheAncientsMod.ThoriumMod, "UnstableCore"));
			SafeAddContentToList(CallUtils.GetItemFromMod(RemnantOfTheAncientsMod.ThoriumMod, "AncientBlade"));
			SafeAddContentToList(CallUtils.GetItemFromMod(RemnantOfTheAncientsMod.ThoriumMod, "StarCaller"));
			SafeAddContentToList(CallUtils.GetItemFromMod(RemnantOfTheAncientsMod.ThoriumMod, "StriderTear"));
			SafeAddContentToList(CallUtils.GetItemFromMod(RemnantOfTheAncientsMod.ThoriumMod, "VoidLens"));
			SafeAddContentToList(CallUtils.GetItemFromMod(RemnantOfTheAncientsMod.ThoriumMod, "DoomSayersCoin"));
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
					if (!ReaperEffectsPlayer.SoulIsEnabled(NPCID.Retinazer) || !ReaperEffectsPlayer.SoulIsEnabled(NPCID.Retinazer)) 
						player.wingTimeMax = OgWingTime / 2;
					else 
						player.wingTimeMax = OgWingTime;
				}
			}
		}
	}
}
