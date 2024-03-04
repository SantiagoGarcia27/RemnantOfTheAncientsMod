using RemnantOfTheAncientsMod.World;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using RemnantOfTheAncientsMod.Common.UtilsTweaks;
using RemnantOfTheAncientsMod.Content.Items.Armor.Reaper;
using Terraria.DataStructures;
using RemnantOfTheAncientsMod.Content.Projectiles.HeldItem;
using Terraria.UI;
using Terraria.ModLoader.Config;
using RemnantOfTheAncientsMod.Content.Projectiles.Mage;
using RemnantOfTheAncientsMod.Content.Projectiles.Summon.Minioms;
using RemnantOfTheAncientsMod.Common.ModCompativilitie;
using RemnantOfTheAncientsMod.Content.Projectiles.BossProjectile;
using RemnantOfTheAncientsMod.Content.Items.Items;
using System.Linq;
using RemnantOfTheAncientsMod.Content.Items.Placeables.MusicBox;

namespace RemnantOfTheAncientsMod.Common.Global.Items
{
    public class RemnantGlobalItem : GlobalItem
	{
		public bool IsCoin;
		public bool IsAmmo;
        public int StyleStat = 0;
        public Item ItemBase = null;
		public List<int> SpearsList = new List<int>();

        public override bool InstancePerEntity => true;
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
			ItemID.ShadowJoustingLance,
			ItemID.UnholyTrident
		};
		public List<int> CoinsId = new List<int>()
		{
			ItemID.CopperCoin,
			ItemID.SilverCoin,
			ItemID.GoldCoin,
			ItemID.PlatinumCoin
		};
        public List<int> SecondClickWeapons = new List<int>()
        {
            ItemID.BookofSkulls,
            ItemID.FlowerofFire,
            ItemID.FlowerofFrost,
			ItemID.SharpTears,
            ItemID.AmethystStaff,
            ItemID.TopazStaff,
            ItemID.SapphireStaff,
            ItemID.EmeraldStaff,
            ItemID.RubyStaff,
            ItemID.DiamondStaff,
            ItemID.AmberStaff,
            ItemID.BubbleGun
        };
		public List<int> GemStaffs = new List<int>()
		{
			ItemID.AmethystStaff,
			ItemID.TopazStaff,
			ItemID.SapphireStaff,
			ItemID.EmeraldStaff,
			ItemID.RubyStaff,
			ItemID.DiamondStaff,
			ItemID.AmberStaff
        };
        public static List<int> Phones = new List<int>()
        {
            ItemID.CellPhone,
            ItemID.Shellphone,
            ItemID.ShellphoneDummy,
            ItemID.ShellphoneHell,
            ItemID.ShellphoneOcean,
            ItemID.ShellphoneSpawn,
            ItemID.ShellPileBlock
        };
        public List<int> LowFood = new List<int>();
        public List<int> MediumFood = new List<int>();
        public List<int> HightFood = new List<int>();
        public List<int> LowBait = new List<int>();
        public List<int> MediumBait = new List<int>();
        public List<int> HightBait = new List<int>();

		public List<int> MusicBox = new List<int>()
		{

		};
		public override void SetStaticDefaults()
		{
            
        }

		public void fillMusicBox()
		{
			for (int i = 562; i <= 574; i++)
			{
                Utils1.AddSecure(MusicBox, i);
            }
			Utils1.AddSecure(MusicBox, 576);
            for (int i = 1596; i <= 1610; i++)
            {
                Utils1.AddSecure(MusicBox, i);
            }
            for (int i = 1963; i <= 1965; i++)
            {
                Utils1.AddSecure(MusicBox, i);
            }
            Utils1.AddSecure(MusicBox, 2742);
            Utils1.AddSecure(MusicBox, 3044);
            for (int i = 3235; i <= 3237; i++)
            {
                Utils1.AddSecure(MusicBox, i);
            }
            Utils1.AddSecure(MusicBox, 3370);
            Utils1.AddSecure(MusicBox, 3371);
            Utils1.AddSecure(MusicBox, 3796);
            Utils1.AddSecure(MusicBox, 3869);
            for (int i = 4077; i <= 4082; i++)
            {
                Utils1.AddSecure(MusicBox, i);
            }
            Utils1.AddSecure(MusicBox, 4237);
            for (int i = 4356; i <= 4358; i++)
            {
                Utils1.AddSecure(MusicBox, i);
            }
            Utils1.AddSecure(MusicBox, 4421);
            Utils1.AddSecure(MusicBox, 4606);
            Utils1.AddSecure(MusicBox, 4979);
            Utils1.AddSecure(MusicBox, 4985);
            for (int i = 4990; i <= 4992; i++)
            {
                Utils1.AddSecure(MusicBox, i);
            }
            Utils1.AddSecure(MusicBox, 5006);
            for (int i = 5014; i <= 5040; i++)
            {
                Utils1.AddSecure(MusicBox, i);
            }
            Utils1.AddSecure(MusicBox, 5044);
            Utils1.AddSecure(MusicBox, 5112);
            Utils1.AddSecure(MusicBox, 5362);
            Utils1.AddSecure(MusicBox, ModContent.ItemType<DesertMusicBox>());
            Utils1.AddSecure(MusicBox, ModContent.ItemType<FrozenMusicBox>());
            Utils1.AddSecure(MusicBox, ModContent.ItemType<Frozenp2MusicBox>());      
            Utils1.AddSecure(MusicBox, ModContent.ItemType<InfernalMusicBox>());
        }

        public override void SetDefaults(Item item)
		{
			bool WeaponConf = ModContent.GetInstance<ConfigServer>().VanillaWeaponsChangesConf;
			bool Calamity = RemnantOfTheAncientsMod.CalamityMod != null;


            if (WeaponConf)
			{
				fillMusicBox();
                if (SecondClickWeapons.Contains(item.type))
                {
                    ItemID.Sets.ItemsThatAllowRepeatedRightClick[item.type] = true;
                }

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
                if (item.type == ItemID.WeatherPain)
				{
					item.knockBack = 5;
				}
				if(item.type == ItemID.BookofSkulls)
				{
					item.mana = 15;
					item.damage += 5;
				}
				if(item.type == ItemID.InfernoFork)
				{
					item.crit = 76; // da 80 en el juego (valor buscado - 4)
				}
                if (item.type == ItemID.ClingerStaff)
                {
                    item.mana = 10;
                }
                if (item.type == ItemID.ToxicFlask)
                {
					item.useAnimation = 25;
					item.useTime = 7;
                    item.mana += 10;
					item.reuseDelay = 40;
                }
				if(item.type == ItemID.MagicDagger)
				{
					item.damage -= 3;
					item.mana = 4;
				}
                if (item.type == ItemID.ZapinatorGray)
                {
                    item.damage = 25;
                    item.useAnimation = 16;
                    item.useTime = 14;
                }
                if (item.type == ItemID.ZapinatorOrange)
                {
                    item.damage = 60;
                    item.useAnimation = 16;
                    item.useTime = 14;
                }  
                if (item.type == ItemID.PsychoKnife && Calamity)
                {
                    item.damage = 300;
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
				if (SecondClickWeapons.Contains(item.type))
				{
                    item.GetGlobalItem<CustomTooltip>().SecondHabilitie = true;
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
			if(RemnantOfTheAncientsMod.FargosSoulMod != null) {
				if (player.GetModPlayer<RemnantFargosSoulsPlayer>().DesertMedalion)
				{
					if (item.DamageType == DamageClass.Magic)
					{
						int p = Projectile.NewProjectile(Projectile.GetSource_None(), position, velocity * 2, ProjectileID.SandnadoFriendly, damage / 2, knockback, player.whoAmI);
						Main.projectile[p].timeLeft = 50;
					}
					if (item.DamageType == DamageClass.Melee)
					{
						Projectile.NewProjectile(Projectile.GetSource_None(), position, velocity, ModContent.ProjectileType<DesertTyphoonFriendly>(), damage / 2, knockback, player.whoAmI);
					}
					if (item.DamageType == DamageClass.Ranged)
					{
						int p = Projectile.NewProjectile(Projectile.GetSource_None(), position, velocity, ProjectileID.RollingCactusSpike, damage / 2, knockback, player.whoAmI);
						Main.projectile[p].friendly = true;
					}
					if (item.DamageType == DamageClass.Throwing)
					{
						int p = Projectile.NewProjectile(Projectile.GetSource_None(), position, velocity, ModContent.ProjectileType<DesertTyphoonFriendly>(), damage / 2, knockback, player.whoAmI);
						Main.projectile[p].friendly = true;
						Main.projectile[p].hostile = false;
					}
				}
            }



            if (item.type == ItemID.PearlwoodBow)
			{
				return base.Shoot(item, player, source, position, velocity, ProjectileID.JestersArrow, damage, knockback);
			}
			if (item.type == ItemID.BabyBirdStaff)
			{
				var a = Projectile.NewProjectile(Projectile.GetSource_None(), player.position, Vector2.One, ProjectileID.BabyBird, item.damage, item.knockBack, Main.myPlayer, 0, 0, 0);
				Main.projectile[a].minionSlots = 0.5f;
				var p = Projectile.NewProjectile(Projectile.GetSource_None(), player.position, Vector2.One, ProjectileID.BabyBird, item.damage / 3, item.knockBack / 2, Main.myPlayer, 0, 0, 0);
				Main.projectile[p].minionSlots = 0.5f;
				Main.projectile[p].alpha = 200;
				return false;
			}
			if (item.type == ItemID.BookofSkulls)
			{
				if (player.whoAmI == Main.myPlayer)
				{
					float PlayerManaDiscounMultpier = player.statManaMax2 / 20;
					int Shootproj = ModContent.ProjectileType<BookOfSkullHeldProj>();
					if (player.altFunctionUse == 2)
					{
						item.mana = player.statManaMax2;
						if (player.ownedProjectileCounts[Shootproj] <= 0)
						{
							Projectile.NewProjectile(source, player.position, Vector2.Zero, Shootproj, 0, 0, Main.myPlayer, velocity.X, velocity.Y, PlayerManaDiscounMultpier);
						}
						return false;
					}
					else
					{
						item.mana = 15;
						return true;
					}
				}
			}
			if (item.type == ItemID.FrostStaff)
			{
				double ecuacionhonda = Utils1.GenerateWave(3f * 16, player.position.X, counter);
				if (counter % 2 == 0)
				{
					var p = Projectile.NewProjectile(source, player.position - new Vector2(0, (float)ecuacionhonda - 2 * 16f), velocity, 174, damage / 4, knockback, Main.myPlayer);
					Main.projectile[p].stepSpeed /= 2;
				}
				Projectile.NewProjectile(source, player.position + new Vector2(0, 2 * 16f), velocity, type, damage, knockback, Main.myPlayer);
				counter++;
				return false;
			}
			if (item.type == ItemID.FlowerofFire || item.type == ItemID.FlowerofFrost)
			{
				if (player.altFunctionUse != 2)
				{
					item.useTime = item.type == ItemID.FlowerofFire? 16:12;
					item.useAnimation = item.type == ItemID.FlowerofFire ? 16 : 12;
					item.mana = item.type == ItemID.FlowerofFire ? 12 : 11;
                    item.useStyle = ItemUseStyleID.Swing;
					item.shootSpeed = item.type == ItemID.FlowerofFire ? 7.5f : 9f;
					item.autoReuse = true;
				}
				else
				{

					item.useTime = 6;
					item.useAnimation = 6;
					item.useStyle = ItemUseStyleID.Shoot;
                    item.shootSpeed = item.type == ItemID.FlowerofFire ? 1f : 2f;
                    item.channel = true;
					item.mana = 3;
                    item.autoReuse = true;
                    int p = Projectile.NewProjectile(source, position, velocity, ProjectileID.Flames, damage / 2, knockback, player.whoAmI,item.type == ItemID.FlowerofFire ? 2:1);
					Main.projectile[p].DamageType = DamageClass.Magic;
					if (player.ownedProjectileCounts[ModContent.ProjectileType<FlowerHeldProj>()] <= 0)
					{
						Projectile.NewProjectile(source, position, velocity, ModContent.ProjectileType<FlowerHeldProj>(), 0, 0, player.whoAmI, item.type == ItemID.FlowerofFire? 1:2);
					}
					return false;

                }

			}
            if (item.type == ItemID.SharpTears)
            { 
                int Shootproj = ModContent.ProjectileType<BloodThornHeldProj>();
                if (player.altFunctionUse == 2)
                {
                    item.mana = 90;
                    if (player.ownedProjectileCounts[Shootproj] <= 0)
                    {
                        Projectile.NewProjectile(source, player.position, Vector2.Zero, Shootproj, 0, 0, Main.myPlayer, velocity.X, velocity.Y);
                    }
                    return false;
                }
                else
                {
                    item.mana = 20;
                    return true;
                }
            }
			if (item.type == ItemID.CrystalVileShard)
			{
                float numberProjectiles = 2;
                float rotation = MathHelper.ToRadians(10);

                position += Vector2.Normalize(velocity) * 1f;

                for (int i = 0; i < numberProjectiles; i++)
                {
                    Vector2 perturbedSpeed = velocity.RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))) * .2f;
                    int p = Projectile.NewProjectile(source, position, perturbedSpeed * 6, 491, damage, knockback, player.whoAmI,0,0);
                    Main.projectile[p].stepSpeed = 1f;
                    Main.projectile[p].DamageType = DamageClass.Magic;
                }
            }
            if (item.type == ItemID.NettleBurst)
            {
                float numberProjectiles = 2;
                float rotation = MathHelper.ToRadians(10);

                position += Vector2.Normalize(velocity) * 1f;
				if (new Random().Next(4) == 1)
				{
					for (int i = 0; i < numberProjectiles; i++)
					{
						Vector2 perturbedSpeed = velocity.RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))) * .2f;
						int p = Projectile.NewProjectile(source, position, perturbedSpeed * 6, 277, damage, knockback, player.whoAmI, 0, 0);
						Main.projectile[p].stepSpeed = 1f;
						Main.projectile[p].friendly = true;
						Main.projectile[p].hostile = false;
						Main.projectile[p].DamageType = DamageClass.Magic;
					}
				}
            }
			if (item.type == ItemID.ShadowbeamStaff)
			{
				float numberProjectiles = 2;
				float rotation = MathHelper.ToRadians(10);

				position += Vector2.Normalize(velocity) * 1f;

				for (int i = 0; i < numberProjectiles; i++)
				{
					Vector2 perturbedSpeed = velocity.RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))) * .2f;
					int p = Projectile.NewProjectile(source, position, perturbedSpeed * 6, type, damage, knockback, player.whoAmI);
					Main.projectile[p].stepSpeed = 1f;
					Main.projectile[p].friendly = true;
					Main.projectile[p].hostile = false;
					Main.projectile[p].DamageType = DamageClass.Magic;
				}

			}
			else if (GemStaffs.Contains(item.type))
			{
				if (player.altFunctionUse == 2)
				{
					int ColorBase = 0;
					if (item.type == ItemID.TopazStaff)
					{
						ColorBase = 1;

					}
					else if (item.type == ItemID.AmethystStaff)
					{
						ColorBase = 0;

					}
					else if (item.type == ItemID.EmeraldStaff)
					{
						ColorBase = 2;

					}
					else if (item.type == ItemID.SapphireStaff)
					{
						ColorBase = 3;

					}
					else if (item.type == ItemID.RubyStaff)
					{
						ColorBase = 4;

					}
					else if (item.type == ItemID.DiamondStaff)
					{
						ColorBase = 5;

					}
					else if (item.type == ItemID.AmberStaff)
					{
						ColorBase = 6;

					}

					if (player.ownedProjectileCounts[ModContent.ProjectileType<GemStaffHeldProj>()] <= 0)
					{
						if (player.whoAmI == Main.myPlayer)
						{
							Projectile.NewProjectile(source, player.position, velocity, ModContent.ProjectileType<GemStaffHeldProj>(), 0, 0, player.whoAmI, type, ColorBase);
						}
					}
					return false;
				}
			}
			else if (item.type == ItemID.UnholyTrident)
			{
				if (player.altFunctionUse == 2)
				{
					item.mana = 0;
					return false;
				}
				else
				{
					item.mana = 18;
					item.useStyle = ItemUseStyleID.Shoot;
				}
			}
			else if (item.type == ItemID.BubbleGun)
			{
				if (player.altFunctionUse == 2)
				{
					item.reuseDelay = 70;
					int p = Projectile.NewProjectile(source, position, velocity + new Vector2(0, 5), ModContent.ProjectileType<SharknadoBoltClone>(), item.damage * 2, 0, player.whoAmI);
					Main.projectile[p].friendly = true;
					Main.projectile[p].hostile = !Main.projectile[p].friendly;
					return false;
				}
				else
				{
					item.reuseDelay = 0;
				}
			}
			else if (item.type == ItemID.PygmyStaff)
			{
				if(Main.rand.NextBool(2))
				{
					player.AddBuff(BuffID.Pygmies, 10);
					Projectile.NewProjectile(Projectile.GetSource_None(), position, velocity, ModContent.ProjectileType<PygmyMelee>(), damage, knockback, player.whoAmI);
					return false;
				}

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

            AddSecureToList(SpearsList, ItemID.UnholyTrident);
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

			if (WeaponConf)
			{
				if (item.damage > 0 && item.axe < 1 && item.pick < 1 && item.DamageType == DamageClass.Melee)
				{
					return true;
				}
				else if (SecondClickWeapons.Contains(item.type) || item.type == ItemID.UnholyTrident)
				{
					return true;
				}
				else if (item.GetGlobalItem<CustomTooltip>().Lance)
				{
					return true;
				}
                else
                {
                    return base.AltFunctionUse(item, player);
                }
            }
            else
            {
                return base.AltFunctionUse(item, player);
            }
        }
		public override bool? UseItem(Item item, Player player)
		{
			bool WeaponConf = ModContent.GetInstance<ConfigServer>().VanillaWeaponsChangesConf;

			if (player.altFunctionUse == 2 && WeaponConf)
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
						if (item.rare == j) WeaponDash(player, (float)Math.Log(j + 2) * 0.75f /** player.direction*/, 0f);
					}
				}
				return true;
			}
			else
			{
                if (RemnantOfTheAncientsMod.TerrariaOverhaul != null && !ModContent.GetInstance<ConfigServer>().OverhaulMeleeManaCostConfig && item.CountsAsClass(DamageClass.Melee))
                {
                    item.mana = 0;
                }
                return base.UseItem(item, player);
			}
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
			On_ItemSlot.PickItemMovementAction += AllowCoinAndAmmoPlacement;
			base.Load();
        }

        private void On_Item_CheckLavaDeath(On_Item.orig_CheckLavaDeath orig, Item item, int i)
        {
		
			if (DificultyUtils.ReaperMode)
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
		float counter;

		public List<Mod> ModsWith30Endless = new List<Mod>()
		{
			RemnantOfTheAncientsMod.FargowiltasMod
		};
        public override void UpdateInventory(Item item, Player player)
		{
			if (DificultyUtils.ReaperMode)
			{
				ReaperGlobalItem.ReaperConsumable(item);
				ReaperGlobalItem.ReaperSetDefaut(item);
			}
            ReaperGlobalItem.ReaperConsumable(item);


			if(Phones.Contains(item.type))
			{
                player.GetModPlayer<InfoDisplayPlayer>().showMaxMinion = true;
            }

			foreach (Mod mod in ModsWith30Endless)
			{
				if (mod != null) 
				{
					AddEndlessEffectOnInventory(item, player);
				}
			}
        }
        public override void HoldItem(Item item, Player player)
        {
			int Shootproj;

            if (item.type == ItemID.LeafBlower)
			{
				Shootproj = 226;

                if (player.ownedProjectileCounts[Shootproj] <= 0)
                {
					player.AddBuff(BuffID.LeafCrystal, 200, false);
                    Projectile.NewProjectile(Projectile.GetSource_None(), player.position + new Vector2(0,-2 * 16f), Vector2.Zero, Shootproj, 15, 0, Main.myPlayer,1);
                }
            }
            base.HoldItem(item, player);
        }

        private int AllowCoinAndAmmoPlacement(On_ItemSlot.orig_PickItemMovementAction orig, Item[] inv, int context, int slot, Item checkItem)
        {
            if (context == 1 && (IsCoin || FixCoin(checkItem))) 
            {
                return 0;
            }
            if (context == 2 && IsAmmo)
            {      
                return 0;         
            }
            return orig(inv, context, slot, checkItem);
        } 
		public bool FixCoin(Item item)
		{
			List<int> coins = new List<int>() { ModContent.ItemType<Terracoin>()};
			if (coins.Contains(item.type))
			{
				return true;
			}
			return false;
		}

		public void AddEndlessEffectOnInventory(Item item, Player player)
		{
			if (item.IsAir) 
				return;
			

			if (item.buffTime >= (int)Utils1.FormatTimeToTick(14, 0, 0, 0) && item.buffType != 0)
			{
				player.AddBuff(item.buffType, 2);
			}
                
        }
	}
}