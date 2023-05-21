using RemnantOfTheAncientsMod.World;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using RemnantOfTheAncientsMod.Prefixe;
using CalamityMod;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace RemnantOfTheAncientsMod.Common.Global
{
	public class RemnantGlobalItem : GlobalItem
	{
		public List<int> SpearsList = new List<int>();
		public int[] lanceId = new int[]
		{
			ItemID.Spear,
			ItemID.Trident,
			ItemID.DarkLance,
			ItemID.TheRottedFork,
			ItemID.ThunderSpear,
			ItemID.Swordfish,
			ItemID.CobaltNaginata,
			ItemID.PalladiumPike,
			ItemID.MythrilHalberd,
			ItemID.OrichalcumHalberd,
			ItemID.AdamantiteGlaive,
			ItemID.TitaniumTrident,
			ItemID.ChlorophytePartisan,
			ItemID.MushroomSpear,
			ItemID.ObsidianSwordfish,
			ItemID.NorthPole,
			ItemID.JoustingLance,
			ItemID.HallowJoustingLance,
			ItemID.ShadowJoustingLance
		};

		public override void SetDefaults(Item item)
		{
			bool WeaponConf = ModContent.GetInstance<ConfigServer>().VanillaWeaponsChangesConf;

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


				AddToDashWeaponsLists(item);

				if (SpearsList.Contains(item.type))
				{
					item.GetGlobalItem<CustomTooltip>().Lance = true;
				}

				if (Utils1.NameHasWord(item.Name, "Saber"))
				{
					item.GetGlobalItem<CustomTooltip>().Saber = true;
				}
			}
		}
		public void AddToDashWeaponsLists(Item item)
		{

			AddVanilaSpear();
			AddModedSpearGeneric(item);
		}
		public void AddVanilaSpear()
		{
			AddSecureToList(SpearsList, ItemID.Spear);
			AddSecureToList(SpearsList, ItemID.Trident);
			AddSecureToList(SpearsList, ItemID.DarkLance);
			AddSecureToList(SpearsList, ItemID.TheRottedFork);
			AddSecureToList(SpearsList, ItemID.ThunderSpear);
			AddSecureToList(SpearsList, ItemID.Swordfish);
			AddSecureToList(SpearsList, ItemID.CobaltNaginata);
			AddSecureToList(SpearsList, ItemID.PalladiumPike);
			AddSecureToList(SpearsList, ItemID.MythrilHalberd);
			AddSecureToList(SpearsList, ItemID.OrichalcumHalberd);
			AddSecureToList(SpearsList, ItemID.AdamantiteGlaive);
			AddSecureToList(SpearsList, ItemID.TitaniumTrident);
			AddSecureToList(SpearsList, ItemID.ChlorophytePartisan);
			AddSecureToList(SpearsList, ItemID.MushroomSpear);
			AddSecureToList(SpearsList, ItemID.ObsidianSwordfish);
			AddSecureToList(SpearsList, ItemID.NorthPole);
			AddSecureToList(SpearsList, ItemID.JoustingLance);
			AddSecureToList(SpearsList, ItemID.HallowJoustingLance);
			AddSecureToList(SpearsList, ItemID.ShadowJoustingLance);
		}

		public void AddModedSpearGeneric(Item item)
		{
			string[] LanceNames = new string[]
			{
				"Spear",
				"Lance",
				"Trient",
				"Partisan",
				"Halberd",
				"Pike",
				"Halberd",
				"Naginata"
			};

			if (item.DamageType == DamageClass.Melee)
			{
				for (int i = 0; i < LanceNames.Length; i++)
				{
					if (Utils1.NameHasWord(item.Name, LanceNames[i]))
					{
						AddSecureToList(SpearsList, item.type);
					}
				}
			}
		}

		public void AddSecureToList(List<int> list, int Content)
		{
			if (!list.Contains(Content)) list.Add(Content);
		}
		public override bool AltFunctionUse(Item item, Player player)
		{
			bool WeaponConf = ModContent.GetInstance<ConfigServer>().VanillaWeaponsChangesConf;
			if (WeaponConf && item.damage > 0 && item.axe < 1 && item.pick < 1)
			{
				return true;
			}
			else
			{
				return base.AltFunctionUse(item, player);
			}
		}
		public override bool? UseItem(Item item, Player player)
		{
			bool WeaponConf = ModContent.GetInstance<ConfigServer>().VanillaWeaponsChangesConf;
			if (WeaponConf)
			{
				if (player.altFunctionUse == 2)
				{
					if (item.type == ItemID.ChlorophyteSaber)
					{
						bool PlayerTouchFlour = Main.tile[(int)(player.Center.X / 16), (int)((player.Center.Y + (2 * 16)) / 16)].HasTile;
						int projectileCount = 32;
						float projectileDistance = 10f;


						if (PlayerTouchFlour)
						{
							WeaponDash(player, 1f, 0.75f);
							for (int i = 0; i < projectileCount; i++)
							{
								float angle = MathHelper.ToRadians(360f / projectileCount * i);
								Vector2 velocity = angle.ToRotationVector2() * projectileDistance;
								Vector2 position = player.Center + velocity;

								var p = Projectile.NewProjectile(Projectile.GetSource_None(), position, velocity, item.shoot, item.damage, 2f, Main.myPlayer);
								Main.projectile[p].timeLeft = 100;
							}
						}
						return false;
					}
					else
					{
						if (Utils1.NameHasWord(item.Name, "Saber") && item.ModItem.Mod.Name != "RemnantOfTheAncientsMod")
						{
							float n = 0.5f;

							for (int j = 0; j <= RemnantOfTheAncientsMod.MaxRarity; j++)
							{
								if (item.rare == j)
									if (j > 10)
									{
										WeaponDash(player, (float)Math.Log(j - Math.Log(j)), 1.7f);
									}
									else
									{
										WeaponDash(player, (float)Math.Log(j), (float)Math.Log(j));
									}
							}
						}
					}
					if (SpearsList.Contains(item.type))
					{
						float n = 0.1f;
						for (int j = 0; j < RemnantOfTheAncientsMod.MaxRarity; j++)
						{
							if (item.rare == j) WeaponDash(player, n * j + 1, 0f);
						}
					}
					return true;
				}
				else return base.UseItem(item, player);
			}
			else return base.UseItem(item, player);
		}

		public void WeaponDash(Player player, float StrengX, float StrengY)
		{
			bool PlayerTouchFlour = Main.tile[(int)(player.Center.X / 16), (int)((player.Center.Y + (2 * 16)) / 16)].HasTile;
			if (player.altFunctionUse == 2)
			{
				if (PlayerTouchFlour)
				{
					DashPlayer.JumpDash(player, StrengX, StrengY);
				}
			}
		}
		public override void UpdateInventory(Item item, Player player)
		{
			if (Reaper.ReaperMode)
			{
				ReaperGlobalItem.ReaperConsumable(item);
				ReaperGlobalItem.ReaperSetDefaut(item);
			}
		}

		public override void UpdateAccessory(Item item, Player player, bool hideVisual)
		{
			if (item.prefix == ModContent.PrefixType<Healthy>())
			{
				PrefixUpdate(player, 5, 0, 1);
				CustomTooltip.HealthyReforgeValue = Reaper.ReaperMode ? 1 : 5;
			}
			if (item.prefix == ModContent.PrefixType<Healer>())
			{
				PrefixUpdate(player, 10, 0, 2);
				CustomTooltip.HealerReforgeValue = Reaper.ReaperMode ? 2 : 10;
			}
			if (item.prefix == ModContent.PrefixType<Atletic>())
			{
				PrefixUpdate(player, 15, 0, 3);
				CustomTooltip.AtleticReforgeValue = Reaper.ReaperMode ? 3 : 15;
			}
			if (item.prefix == ModContent.PrefixType<Gigant>())
			{
				PrefixUpdate(player, 20, 0, 5);
				CustomTooltip.GigantReforgeValue = Reaper.ReaperMode ? 5 : 20;
			}
			if (item.prefix == ModContent.PrefixType<Titanic>())
			{
				PrefixUpdate(player, 40, 0, 10);
				CustomTooltip.TitanicReforgeValue = Reaper.ReaperMode ? 10 : 40;
			}
			if (item.prefix == ModContent.PrefixType<Impenetrable>())
			{
				int Basedefense = 8;
				if (RemnantOfTheAncientsMod.CalamityMod != null)
				{
					Basedefense = CalamityPrefixChanger(player);
					player.endurance += 0.02f;
				}
				CustomTooltip.UnpenetrableReforgeValue = Basedefense;
				PrefixUpdate(player, Basedefense, 1);
			}
			if (item.prefix == ModContent.PrefixType<Supersonic>())
			{
				PrefixUpdate(player, 0.08f, 2);
			}
			if (item.prefix == ModContent.PrefixType<Acurate>())
			{
				PrefixUpdate(player, 8f, 3);
			}
			if (item.prefix == ModContent.PrefixType<Sharp>())
			{
				PrefixUpdate(player, 0.08f, 4);
			}
			if (item.prefix == ModContent.PrefixType<Uncontrolled>())
			{
				PrefixUpdate(player, 0.08f, 5);
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
					player.lifeRegen += (int)value;
					break;
			}
		}
		private void PrefixUpdate(Player player, float value, int stat, float reaperValue)
		{
			if (stat == 0)
			{
				player.statLifeMax2 += Reaper.ReaperMode ? (int)reaperValue : (int)value;
			}
		}
		public override bool InstancePerEntity => true;
	}
}