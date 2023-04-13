using RemnantOfTheAncientsMod.World;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using RemnantOfTheAncientsMod.Prefixe;
using CalamityMod;
using RemnantOfTheAncientsMod.Common.Systems;
using CalamityMod.World;

namespace RemnantOfTheAncientsMod.VanillaChanges
{
	public class VanillaWeapons : GlobalItem
	{

		public override void SetDefaults(Item item)
		{
			bool WeaponConf = ModContent.GetInstance<ConfigClient1>().VanillaWeaponsChangesConf;

			if (WeaponConf)
			{
				if (item.type == ItemID.DaedalusStormbow)
				{
					item.damage = 35;
					item.useTime = 24;
					item.useAnimation = 24;
				}
				if (item.type == ItemID.FetidBaghnakhs)
				{
					item.damage = 50;
					item.useTime = 11;
					item.useAnimation = 11;
					item.scale = 1.80f;
				}
				if (item.type == ItemID.HeatRay)
				{
					item.damage = 200;
					item.useTime = 58;
					item.useAnimation = 58;
				}
				if (item.type == ItemID.NightsEdge)
				{
					if (!ModLoader.TryGetMod("CalamityMod", out Mod mod)) item.damage = 45;
				}
				if (item.type == ItemID.Minishark)
				{
					item.knockBack = 0.1f;
				}
				if (item.type == ItemID.DD2BallistraTowerT1Popper) item.damage = 45;
				if (item.type == ItemID.DD2BallistraTowerT2Popper) item.damage = 85;
				if (item.type == ItemID.DD2BallistraTowerT3Popper) item.damage = 200;
				if (item.type == ItemID.DD2FlameburstTowerT1Popper) item.damage = 30;
				if (item.type == ItemID.DD2FlameburstTowerT2Popper) item.damage = 60;
				if (item.type == ItemID.DD2FlameburstTowerT3Popper) item.damage = 100;
				if (item.type == ItemID.DD2ExplosiveTrapT1Popper) item.damage = 40;
				if (item.type == ItemID.DD2ExplosiveTrapT2Popper) item.damage = 80;
				if (item.type == ItemID.DD2ExplosiveTrapT3Popper) item.damage = 250;
				if (item.type == ItemID.DD2LightningAuraT1Popper) item.damage = 10;//30
				if (item.type == ItemID.DD2LightningAuraT2Popper) item.damage = 16;//50
				if (item.type == ItemID.DD2LightningAuraT3Popper) item.damage = 50;//150
			}
			else
			{

			}
		}
		public override void UpdateInventory(Item item, Player player)
		{
			if (Reaper.ReaperMode)
			{
				FchangesItem.ReaperConsumable(item);
				FchangesItem.ReaperSetDefaut(item);
			}

			// item.GetGlobalItem<FchangesItem>().ReaperSize(item);           
		}
		// [JITWhenModsEnabled("CalamityMod")]
		// public static bool IsThingyDowned() => RemnantOfTheAncientsMod.CalamityMod.CalamityWorld.downedYharon;
		public override void UpdateAccessory(Item item, Player player, bool hideVisual)
		{
			if (item.prefix == ModContent.PrefixType<Healthy>())
			{
				PrefixUpdate(player, 5, 0, 1);
				GlobalItem1.HealthyReforgeValue = Reaper.ReaperMode ? 1 : 5;
			}
			if (item.prefix == ModContent.PrefixType<Healer>())
			{
				PrefixUpdate(player, 10, 0, 2);
				GlobalItem1.HealerReforgeValue = Reaper.ReaperMode ? 2 : 10;
			}
			if (item.prefix == ModContent.PrefixType<Atletic>())
			{
				PrefixUpdate(player, 15, 0, 3);
				GlobalItem1.AtleticReforgeValue = Reaper.ReaperMode ? 3 : 15;
			}
			if (item.prefix == ModContent.PrefixType<Gigant>())
			{
				PrefixUpdate(player, 20, 0, 5);
				GlobalItem1.GigantReforgeValue = Reaper.ReaperMode ? 5 : 20;
			}
			if (item.prefix == ModContent.PrefixType<Titanic>())
			{
				PrefixUpdate(player, 40, 0, 10);
				GlobalItem1.TitanicReforgeValue = Reaper.ReaperMode ? 10 : 40;
			}
			if (item.prefix == ModContent.PrefixType<Impenetrable>())
			{
				int Basedefense = 8;
				if (RemnantOfTheAncientsMod.CalamityMod != null)
				{
                    Basedefense = CalamityPrefixChanger(player);
                    player.endurance += 0.02f;
                }
                GlobalItem1.UnpenetrableReforgeValue = Basedefense;
                PrefixUpdate(player, Basedefense, 1);
			}
			if (item.prefix == ModContent.PrefixType<Supersonic>())
			{
				PrefixUpdate(player, 0.08f, 2);
			}
			if (item.prefix == ModContent.PrefixType<Acurate>())
			{
				PrefixUpdate(player, 6f, 3);
			}
			if (item.prefix == ModContent.PrefixType<Uncontrolled>())
			{
				PrefixUpdate(player, 8f, 4);
			}
			if (item.prefix == ModContent.PrefixType<Uncontrolled>())
			{
				PrefixUpdate(player, 6f, 5);
			}
			if (item.prefix == ModContent.PrefixType<Regenerative>())
			{
				PrefixUpdate(player, 2f, 6);
			}
		}
		[JITWhenModsEnabled("CalamityMod")]
		private int CalamityPrefixChanger(Player player)
		{
			if (DownedBossSystem.downedYharon)
			{
				return 20;
			}
			else if (DownedBossSystem.downedDoG)
			{
				return 19;
			}
			else if (DownedBossSystem.downedPolterghast)
			{
				return 18;
			}
			else if (DownedBossSystem.downedProvidence)
			{
				return 16;
			}
			else if (NPC.downedMoonlord)
			{
				return 14;
			}
			else if (NPC.downedGolemBoss)
			{
				return 12;
			}
			else if (Main.hardMode)
			{
				return 10;
			}
			return 8;
		}
        private static void PrefixUpdate(Player player, float value, int stat)
        {
			switch (stat)
			{
				case 0:
                    player.statLifeMax2 += (int)value;
                    break;
				case 1:
                    player.statDefense += (int)value;
                    break;
				case 2:
                    player.moveSpeed += value;
                    break; 
				case 3:
                    player.GetCritChance(DamageClass.Generic) += value;
                    break;
				case 4:
                    ref StatModifier GenericDamage = ref (player.GetDamage<GenericDamageClass>());
                    GenericDamage += value;
                    break;
				case 5:
                    player.GetAttackSpeed(DamageClass.Melee) += value;
                    break;
				case 6:
					player.lifeRegen+= (int)value;
					break;
			} 
        }
		private void PrefixUpdate(Player player, float value, int stat, float reaperValue)
		{
            if (stat == 0)
            {
                player.statLifeMax2 +=  Reaper.ReaperMode? (int)reaperValue : (int)value;
            }
        }
        public override bool InstancePerEntity => true;

	

	}
}