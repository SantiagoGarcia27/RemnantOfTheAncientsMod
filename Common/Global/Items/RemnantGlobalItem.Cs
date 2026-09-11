using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PlayerProxyLib.Common;
using ReLogic.Content;
using RemnantOfTheAncientsMod.Common.DataSet;
using RemnantOfTheAncientsMod.Common.Global.Items.WeaponsModels;
using RemnantOfTheAncientsMod.Common.RemPlayer;
using RemnantOfTheAncientsMod.Common.UtilsTweaks;
using RemnantOfTheAncientsMod.Content.Items.Items;
using RemnantOfTheAncientsMod.Content.Items.Placeables.MusicBox;
using RemnantOfTheAncientsMod.Content.Projectiles.BossProjectile.Desert;
using SangarUtilities.Common.UtilsTweaks;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.UI;

namespace RemnantOfTheAncientsMod.Common.Global.Items
{
    public class RemnantGlobalItem : GlobalItem
	{
		public bool IsCoin;
		public bool IsAmmo;
		public int StyleStat = 0;


		public Item ItemBase = null;
		public float ChargeMax = 0;
		public static List<int> SpearsList = [];
		public bool CanCharge = false;


		// public float CurrentAmmo = 0;
		public float CurrentAmmoMax = 0;
		public List<int> CurrentAmmoType = [];
		public float ReloadCounter = 0;
		public float ReloadCounterMax = 0;

        public bool WeaponConf => ModContent.GetInstance<ConfigServer>().VanillaWeaponsChangesConf;

        public override bool InstancePerEntity => true;
        
		public override void SetStaticDefaults()
		{
			CurrentAmmoMax = 0;
		}

		public override void SetDefaults(Item item)
		{
			if (WeaponConf)
			{
				
				if (RemnantItemTags.SecondClickWeapons.Contains(item.type))
				{
					ItemID.Sets.ItemsThatAllowRepeatedRightClick[item.type] = true;
				}
		
				if (item.maxStack > 1 && item.maxStack < 9999)
				{
					if (!RemnantItemTags.CoinsId.Contains(item.type))
					{
						item.maxStack = (int)ModContent.GetInstance<ConfigServer>().MaxItemStackConf;
					}
				}
				AddToDashWeaponsLists(item);


				if (SpearsList.Contains(item.type))
				{
					item.GetGlobalItem<CustomTooltip>().Lance = true;
				}

				if (Utils1.NameHasWord(item.Name, "Saber"))
				{
					item.GetGlobalItem<SaberGlobalItem>().isSaber = true;
				}

                if (CanCharge)
				{
					item.channel = true;
					ItemID.Sets.ItemsThatAllowRepeatedRightClick[item.type] = true;
				}
				if (RemnantItemTags.SecondClickWeapons.Contains(item.type))
				{
					item.GetGlobalItem<CustomTooltip>().SecondHabilitie = true;
				}

				if(item.type == ItemID.ReaperHood || item.type == ItemID.ReaperRobe)
				{
					item.value = 0;
				}
			}
		}

		public override void ModifyShootStats(Item item, Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
		{
			if (!WeaponConf) base.ModifyShootStats(item, player, ref position, ref velocity, ref type, ref damage, ref knockback);

			else if (item.type == ItemID.PearlwoodBow) type = ProjectileID.JestersArrow;
			
            base.ModifyShootStats(item, player, ref position, ref velocity, ref type, ref damage, ref knockback);
        }


		public override bool Shoot(Item item, Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			if (RemnantOfTheAncientsMod.FargosSoulMod != null)
			{
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
						Main.projectile[p].hostile = false;
						Main.projectile[p].velocity *= 2;
					}
					if (item.DamageType == DamageClass.Throwing)
					{
						int p = Projectile.NewProjectile(Projectile.GetSource_None(), position, velocity, ModContent.ProjectileType<DesertTyphoonFriendly>(), damage / 2, knockback, player.whoAmI);
						Main.projectile[p].friendly = true;
						Main.projectile[p].hostile = false;
					}
				}
			}
			if (item.DamageType == DamageClass.Ranged)
			{
				var extraProjectile = player.GetModPlayer<StatPlayer>().GetRangerExtraProjectile(item.useAmmo);

				if (extraProjectile != null)
				{
					foreach (ExtraProjectile proj in extraProjectile)
					{
						if (Main.rand.NextBool(100 / proj.Chance))
						{
							Projectile.NewProjectile(Projectile.GetSource_None(), position, velocity, proj.ProjectileType, damage, knockback, player.whoAmI);
						}
					}
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
			 ListUtils.AddSecure(SpearsList, RemnantItemTags.lanceId);
		}
		public static void isSpear(Item item)
        {
            ListUtils.AddSecure(ref SpearsList, item.type);
            if (SpearsList.Contains(item.type))
            {
                item.GetGlobalItem<CustomTooltip>().Lance = true;
            }
        }

		private void AddModedSpearGeneric(Item item)
		{
			string[] LanceNames =
			[
				"Spear",
				"Lance",
				"Trient",
				"Partisan",
				"Halberd",
				"Pike",
				"Halberd",
				"Naginata"
			];

			if (item.DamageType == DamageClass.Melee)
			{
				foreach (string Spearnames in LanceNames)
				{
					if (Utils1.NameHasWord(item.Name, Spearnames))
					{
						 ListUtils.AddSecure(ref SpearsList, item.type);
					}
				}
			}
		}
		public override bool AltFunctionUse(Item item, Player player)
		{
			if (item.GetGlobalItem<CustomTooltip>().SecondHabilitie || item.GetGlobalItem<CustomTooltip>().HaveSecondMode)
				return true;

			if (WeaponConf)
			{
				if (item.damage > 0 && item.axe < 1 && item.pick < 1 && item.DamageType == DamageClass.Melee ||
					(RemnantItemTags.SecondClickWeapons.Contains(item.type) || item.type == ItemID.UnholyTrident) ||
					item.GetGlobalItem<CustomTooltip>().Lance)
				{
					return true;
				}
			}
			return base.AltFunctionUse(item, player);

		}
		public override bool? UseItem(Item item, Player player)
		{
			if (player.altFunctionUse == 2 && WeaponConf)
			{
				if (SpearsList.Contains(item.type))
				{
					for (int j = 0; j < RemnantOfTheAncientsMod.MaxRarity; j++)
					{
						if (item.rare == j)
							WeaponDash(player, (float)Math.Log(j + 2) * 0.75f, 0f);
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

		public static void WeaponDash(Player player, float StrengX, float StrengY)
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
			On_ItemSlot.PickItemMovementAction += AllowCoinAndAmmoPlacement;	
            base.Load();
		}

        public override void UpdateInventory(Item item, Player player)
		{		
			if (RemnantItemTags.Phones.Contains(item.type))
				player.GetModPlayer<InfoDisplayPlayer>().showMaxMinion = true;
			
			foreach (Mod mod in RemnantItemTags.ModsWith30Endless)
			{
				if (mod != null) AddEndlessEffectOnInventory(item, player);
			}
		}


		public float Timmer = 0;
		public float TimmerMax = 0;
		public float UseTimeReduction = 0;
		public override void HoldItem(Item item, Player player)
		{
			int Shootproj;

			if (item.type == ItemID.LeafBlower)
			{
				Shootproj = 226;

				if (player.ownedProjectileCounts[Shootproj] <= 0)
				{
					player.AddBuff(BuffID.LeafCrystal, 200, false);
					Projectile.NewProjectile(Projectile.GetSource_None(), player.position + new Vector2(0, -2 * 16f), Vector2.Zero, Shootproj, 15, 0, Main.myPlayer, 1);
				}
			}

			if (CanCharge)
			{
				RemnantPlayer.GenericChargeCouldownMax = (int)TimmerMax;
				RemnantPlayer.GenericChargeCouldown = (int)Timmer;
			}
			base.HoldItem(item, player);
		}

		private int AllowCoinAndAmmoPlacement(On_ItemSlot.orig_PickItemMovementAction orig, Item[] inv, int context, int slot, Item checkItem)
		{
			if (context == 1 && (IsCoin || FixCoin(checkItem))) return 0;
			if (context == 2 && IsAmmo) return 0;
			
			return orig(inv, context, slot, checkItem);
		}
		public static bool FixCoin(Item item)
		{
			List<int> coins = [ModContent.ItemType<Terracoin>()];
			return coins.Contains(item.type);
		}

		public static void AddEndlessEffectOnInventory(Item item, Player player)
		{
			if (item.IsAir) return;

			if (item.buffTime >= Utils1.FormatTimeToTick(14, 0, 0, 0) && item.buffType != 0)
				player.AddBuff(item.buffType, 2);
		}

		public static List<Asset<Texture2D>> oldTexture = [];
		public override void PostDrawInInventory(Item item, SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
		{
			if (Utils1.IsAprilFoolDay() && ModContent.GetInstance<ConfigClient>().AprilFoolsDay)
			{
				int texture;
				if (item.pick > 0) texture = 3509;
				else if (item.axe > 0) texture = 3506;
				else if (item.hammer > 0) texture = 3505;		
				else if (item.DamageType == DamageClass.Magic) texture = 3069;		
				else if (item.DamageType == DamageClass.Ranged) texture = item.ammo > 0 ? 40 : 39;	
				else if (item.DamageType == DamageClass.Summon) texture = 1309;	
				else if (item.DamageType == DamageClass.Melee || item.DamageType == DamageClass.MeleeNoSpeed) texture = 24;
				else if (item.DamageType == DamageClass.SummonMeleeSpeed) texture = 4672;
				else if (item.accessory) texture = 3068;			
				else if (item.defense > 0) texture = 728;
				else if (item.buffType > 0) texture = 28;
				else texture = 2;
				
				TextureAssets.Item[item.type] = ModContent.Request<Texture2D>("Terraria/Images/Item_" + texture);
				spriteBatch.Draw((Texture2D)ModContent.Request<Texture2D>("Terraria/Images/Item_" + texture), new Vector2(0, 0), default);

			}
			else
			{
				if (oldTexture.Count > 0)
				{
					try
					{
						TextureAssets.Item[item.type] = oldTexture[item.type];
					}
					catch (Exception)
					{
						Main.NewText(item.type);
					}
				}
				base.PostDrawInInventory(item, spriteBatch, position, frame, drawColor, itemColor, origin, scale);
			}
		}
		public override bool CanConsumeAmmo(Item weapon, Item ammo, Player player)
		{
			int NotConsumeAmmoChance = player.GetModPlayer<RemnantPlayer>().NotConsumeAmmoChance;
			float selection = Main.rand.NextFloat(0, 100);

			if (selection <= NotConsumeAmmoChance) return false;
			else return base.CanConsumeAmmo(weapon, ammo, player);
		}
        public override void UpdateEquip(Item item, Player player)
        {
            base.UpdateEquip(item, player);
        }

		public override bool CanUseItem(Item item, Player player)
		{
            return base.CanUseItem(item, player);
		}
        public override void UseStyle(Item item, Player player, Rectangle heldItemFrame)
        {
            base.UseStyle(item, player, heldItemFrame);
        }
		public override float UseSpeedMultiplier(Item item, Player player)
		{
			if (player == Main.LocalPlayer)
			{
				Vector2 mouseWorld = Main.MouseWorld / 16;
				Tile cursorTile = Main.tile[(int)mouseWorld.X, (int)mouseWorld.Y];
				if ((cursorTile.TileType == TileID.Extractinator || cursorTile.TileType == TileID.ChlorophyteExtractinator) && player.cursorItemIconEnabled)
				{
					float multiplier = ModContent.GetInstance<ConfigServer>().FastExtractinatorConf;
					return base.UseSpeedMultiplier(item, player) * multiplier;
				}
			}
			return base.UseSpeedMultiplier(item, player);
		}


	}
}