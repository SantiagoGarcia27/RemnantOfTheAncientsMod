using RemnantOfTheAncientsMod.World;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using RemnantOfTheAncientsMod.Prefixe;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using RemnantOfTheAncientsMod.Common.UtilsTweaks;
using RemnantOfTheAncientsMod.Content.Items.Armor.Reaper;
using Terraria.DataStructures;

namespace RemnantOfTheAncientsMod.Common.Global.Items
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
		public List<int> CoinsId = new List<int>()
		{
			ItemID.CopperCoin,
			ItemID.SilverCoin,
			ItemID.GoldCoin,
			ItemID.PlatinumCoin
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
					if (RemnantOfTheAncientsMod.CalamityMod == null) item.damage = 45;
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
				
				if(item.type == ItemID.PearlwoodBow)
				{
					item.damage = 30;
					item.shootSpeed = 10;
					item.useTime= 8;
					item.useAnimation = 70;	
				}				
				if(item.type == ItemID.FlinxFurCoat)
				{
					item.defense = 3;
				}
				if (item.maxStack > 1 && item.maxStack < 9999)
				{
					if (!CoinsId.Contains(item.type))
					{
						item.maxStack = (int)ModContent.GetInstance<ConfigServer>().MaxItemStackConf;
					}
				}
				//if (ItemID.Sets.BossBag[item.type]) 
				//{
				//	AddValueToTreasureBags(item);
				//}
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
		//public void AddValueToTreasureBags(Item item)
		//{
		//	Main.ItemDropsDB.GetRulesForItemID(item.type);// .GetRulesForItemID(item.type);
  //      }
		
        public override void ModifyShootStats(Item item, Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            bool WeaponConf = ModContent.GetInstance<ConfigServer>().VanillaWeaponsChangesConf;
			if (WeaponConf)
			{
				if (item.type == ItemID.PearlwoodBow)
				{
					type = ProjectileID.JestersArrow;
					base.ModifyShootStats(item, player, ref position, ref velocity, ref type, ref damage, ref knockback);
				}
			}
			else base.ModifyShootStats(item, player, ref position, ref velocity, ref type, ref damage, ref knockback);
        }
        public override bool Shoot(Item item, Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
			if(item.type == ItemID.PearlwoodBow)
			{
                return base.Shoot(item, player, source, position, velocity, ProjectileID.JestersArrow, damage, knockback);
            }
			if (item.type == ItemID.BabyBirdStaff)
			{
                var a = Projectile.NewProjectile(Projectile.GetSource_None(), player.position, Vector2.One, ProjectileID.BabyBird, item.damage, item.knockBack, Main.myPlayer, 0, 0, 0);
                Main.projectile[a].minionSlots = 0.5f;
                var p = Projectile.NewProjectile(Projectile.GetSource_None(), player.position, Vector2.One, ProjectileID.BabyBird, item.damage / 3, item.knockBack / 2,Main.myPlayer, 0, 0, 0);
				Main.projectile[p].minionSlots = 0.5f;
				Main.projectile[p].alpha = 200;
				return false;
            }
            return base.Shoot(item, player, source, position, velocity, type, damage, knockback);

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
        public override void UpdateEquip(Item item, Player player)
        {
            base.UpdateEquip(item, player);
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
							for (int j = 0; j <= RemnantOfTheAncientsMod.MaxRarity; j++)
							{
								if (item.rare == j)
								{
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
					}
					if (SpearsList.Contains(item.type))
					{
						for (int j = 0; j < RemnantOfTheAncientsMod.MaxRarity; j++)
						{
							if (item.rare == j) WeaponDash(player, (float)Math.Log(j + 2), 0f);
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
        public override void Load()
        {
           On_Item.CheckLavaDeath += On_Item_CheckLavaDeath;
           base.Load();
        }

        private void On_Item_CheckLavaDeath(On_Item.orig_CheckLavaDeath orig, Item item, int i)
        {
		
			if (Reaper.ReaperMode)
			{
				if (Main.player[Main.myPlayer].GetModPlayer<ReaperPlayer>().AllSoulsAreActive())
				{
					if (item.type == ItemID.ReaperHood)
					{
						Main.player[Main.myPlayer].QuickSpawnItem(item.GetSource_ReleaseEntity(), ModContent.ItemType<Reaper_Hood>(), 1);
					}
					if (item.type == ItemID.ReaperRobe)
					{
						Main.player[Main.myPlayer].QuickSpawnItem(item.GetSource_ReleaseEntity(), ModContent.ItemType<Reaper_Robe>(), 1);
					}
					if (item.type == ItemID.NecroGreaves)
					{
						Main.player[Main.myPlayer].QuickSpawnItem(item.GetSource_ReleaseEntity(), ModContent.ItemType<Reaper_Pants>(), 1);
						item.rare = 0;
					}
				}
				if(item.type == ItemID.GuideVoodooDoll && item.rare == 0)
				{
                  int a = Main.player[Main.myPlayer].QuickSpawnItem(item.GetSource_ReleaseEntity(), ItemID.GuideVoodooDoll, 1);
                  Main.item[a].rare = 1;
				}
            }
            orig(item, i);

            //  return base.CanBurnInLava(item);	
        }
        public override void UpdateInventory(Item item, Player player)
		{
			if (Reaper.ReaperMode)
			{
				ReaperGlobalItem.ReaperConsumable(item);
				ReaperGlobalItem.ReaperSetDefaut(item);
			}
		}

		
		public override bool InstancePerEntity => true;
	}
}