using opswordsII.Buffs;
using opswordsII.Dusts;
using opswordsII.Projectiles;
using opswordsII.VanillaChanges;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria.ModLoader.IO;
using System.Collections.Generic;
using System.IO;
using opswordsII.Items.DificultChanger;
using opswordsII.World;
using opswordsII.Items.Fmode;
using opswordsII.Items.accesorios;

namespace opswordsII
{

	public class Player1 : ModPlayer
	{
		
		#region Minions
		public bool FrozenMinion;
		public bool DesertMinion;
		public bool SaplingMinion;
		public bool SaplingGoldMinion;
		public bool SaplingPlatinumMinion;
		public bool SaplingSilverMinion;
		public bool SaplingTungstenMinion;
		public bool StardustMinion;
		public bool StardustDragonV2Minion;
		public bool TyrantMinion;
		#endregion
		public bool Burn_Sand;
		public bool hBurn;
		public bool Hell_Fire;
		public bool hasInfernal_core;
		public bool SandWeapons;
		public int healHurt;
		public bool TortugaPet;
		public bool TwitchPet;
		public bool YtPet;
		public bool ModPlayer = true;
		public bool anyBossIsAlive;
		public static bool FWeapons;
		public static bool ReaperFirstTime;


		public override void ResetEffects()
		{
			Burn_Sand = false;
			hBurn = false;
			Hell_Fire = false;
			healHurt = 0;
			TortugaPet = false;
			TwitchPet = false;
			YtPet = false;
			FrozenMinion = false;
			SaplingMinion = false;
			SaplingGoldMinion = false;
			SaplingPlatinumMinion = false;
			SaplingSilverMinion = false;
			SaplingTungstenMinion = false;
			StardustMinion = false;
			StardustDragonV2Minion = false;
			TyrantMinion = false;
			hasInfernal_core = false;
			SandWeapons = false;
		}


		public override void UpdateDead()
		{
			Burn_Sand = false;
			Hell_Fire = false;
			hBurn = false;
		}
		public override void UpdateBadLifeRegen()
		{
			if (Burn_Sand || Hell_Fire || hBurn)
			{
				
				if (Player.lifeRegen > 0)
				{
					Player.lifeRegen = 0;
				}
				Player.lifeRegenTime = 0;
				// lifeRegen is measured in 1/2 life per second. Therefore, this effect causes 8 life lost per second.
				Player.lifeRegen -= 16;
			}
		}
		public override void OnEnterWorld(Player Player)
		{
			FWeapons = true;
			FchangesItem.ReaperWingsNerf(Player);
			//Main.NewText("");
		}
		public override IEnumerable<Item> AddStartingItems(bool mediumCoreDeath)
		{
			if (mediumCoreDeath)
			{
				return new[] {
					new Item(ItemType<Ftoggler>())
				};
			}
			return new[] {
				new Item(ItemType<Ftoggler>()),
			};
		}
		public override void UpdateEquips()
		{
			if (Player.wingTimeMax > 4) FchangesItem.ReaperWingsNerf(Player);
			ReaperSoulsBoost();
		}
		public void ReaperStarter(IList<Item> items)
		{
			if (ReaperFirstTime)
			{
				Player.QuickSpawnItem(Player.GetSource_DropAsItem(), ItemID.ReaperHood);
				Player.QuickSpawnItem(Player.GetSource_DropAsItem(), ItemID.ReaperRobe);
			}
		}

		public override void DrawEffects(PlayerDrawSet drawInfo, ref float r, ref float g, ref float b, ref float a, ref bool fullBright)
		{
			if (Burn_Sand)
			{
				if (Main.rand.NextBool(4) && drawInfo.shadow == 0f)
				{
					int dust = Dust.NewDust(drawInfo.Position - new Vector2(2f, 2f), Player.width + 4, Player.height + 4, DustType<QuemaduraA>(), Player.velocity.X * 0.4f, Player.velocity.Y * 0.4f, 100, default, 3f);
					Main.dust[dust].noGravity = true;
					Main.dust[dust].velocity *= 1.8f;
					Main.dust[dust].velocity.Y -= 0.5f;
					drawInfo.DustCache.Add(dust);
				}
			}
			if (Hell_Fire)
			{
				if (Main.rand.NextBool(4) && drawInfo.shadow == 0f)
				{
					int dust = Dust.NewDust(drawInfo.Position - new Vector2(2f, 2f), Player.width + 4, Player.height + 4, DustType<Hell_Fire_P>(), Player.velocity.X * 0.4f, Player.velocity.Y * 0.4f, 100, default, 3f);
					Main.dust[dust].noGravity = true;
					Main.dust[dust].velocity *= 1.8f;
					Main.dust[dust].velocity.Y -= 0.5f;
					drawInfo.DustCache.Add(dust);
				}
			}
			if (hBurn)
			{
				if (Main.rand.NextBool(4) && drawInfo.shadow == 0f)
				{
					int dust = Dust.NewDust(drawInfo.Position - new Vector2(2f, 2f), Player.width + 4, Player.height + 4, DustType<HollyBurn_P>(), Player.velocity.X * 0.4f, Player.velocity.Y * 0.4f, 100, default, 3f);
					Main.dust[dust].noGravity = true;
					Main.dust[dust].velocity *= 1.8f;
					Main.dust[dust].velocity.Y -= 0.5f;
					drawInfo.DustCache.Add(dust);
				}
			}
		}
		public override void OnHitNPC(Item item, NPC target, int damage, float knockback, bool crit)
		{
			FchangesItem.ReaperSize(item);
			if (!item.noMelee && !item.noUseGraphic)
			{
				if (hasInfernal_core)
				{
					target.AddBuff(BuffType<Hell_Fire>(), 300);
					//Main.NewText("true", Color.Orange);; //if the condition is true, then apply the ethereal flames debuff to the targeted NPC for 5 seconds.
				}
				if (SandWeapons && !item.noMelee && !item.noUseGraphic)
				{
					target.AddBuff(BuffType<Burning_Sand>(), 300);
				}
			}
		}
		public override void OnHitNPCWithProj(Projectile proj, NPC target, int damage, float knockback, bool crit) //This is the same as the one in OnHitNPC, but for melee projectiles.
		{
			if (proj.CountsAsClass(DamageClass.Melee))
			{
				if (hasInfernal_core)
				{
					target.AddBuff(BuffType<Hell_Fire>(), 300);
				}
				if (SandWeapons)
				{
					target.AddBuff(BuffType<Burning_Sand>(), 300);
				}
			}
		}
		public void AnkInmunity()
		{
			Player.buffImmune[BuffID.Bleeding] = true;
			Player.buffImmune[BuffID.BrokenArmor] = true;
			Player.buffImmune[BuffID.Burning] = true;
			Player.buffImmune[BuffID.Confused] = true;
			Player.buffImmune[BuffID.Cursed] = true;
			Player.buffImmune[BuffID.Darkness] = true;
			Player.buffImmune[BuffID.Poisoned] = true;
			Player.buffImmune[BuffID.Silenced] = true;
			Player.buffImmune[BuffID.Slow] = true;
			Player.buffImmune[BuffID.Weak] = true;
			Player.buffImmune[BuffID.Chilled] = true;
		}
		public void ScrollInmunity()
		{
			Player.buffImmune[BuffType<Bee>()] = true;
			Player.buffImmune[BuffType<AoD>()] = true;
			Player.buffImmune[BuffType<Infernal>()] = true;
			Player.buffImmune[BuffType<MasterD>()] = true;
			Player.buffImmune[BuffType<Putrid>()] = true;
			Player.buffImmune[BuffType<Eye>()] = true;
			Player.buffImmune[BuffType<Slim>()] = true;
		}
		public void ExoticA(int l, int m, int m2, int p, Item item)
		{
			Player.lifeRegen += l;
			Player.manaRegenBonus = m;
			Player.statManaMax2 += m2;
			Player.GetArmorPenetration(DamageClass.Generic) += p;
			Player.pStone = true;

			if (Player.whoAmI == Main.myPlayer) Player.starCloakItem = item;
			
			if (Player.whoAmI == Main.myPlayer) Player.honeyCombItem = item;
			
			Player.longInvince = true;


			Player.longInvince = true;
			Player.arcticDivingGear = true;
			Player.panic = true;
			Player.fireWalk = true;
			Player.lavaImmune = true;
		}
		public void VanillaFlaskInmune()
		{
			Player.buffImmune[BuffID.WeaponImbueConfetti] = true;
			Player.buffImmune[BuffID.WeaponImbueCursedFlames] = true;
			Player.buffImmune[BuffID.WeaponImbueFire] = true;
			Player.buffImmune[BuffID.WeaponImbueGold] = true;
			Player.buffImmune[BuffID.WeaponImbueIchor] = true;
			Player.buffImmune[BuffID.WeaponImbueNanites] = true;
			Player.buffImmune[BuffID.WeaponImbuePoison] = true;
			Player.buffImmune[BuffID.WeaponImbueVenom] = true;
		}

		public void FrostInmune()
		{
			Player.buffImmune[BuffID.Frozen] = true;
			Player.buffImmune[BuffID.Frostburn] = true;
			Player.buffImmune[BuffID.Chilled] = true;
		}
		public void FirenInmune()
		{
			Player.buffImmune[BuffID.OnFire] = true;
			Player.buffImmune[BuffID.Frostburn] = true;
			Player.buffImmune[BuffID.CursedInferno] = true;
			Player.buffImmune[BuffID.Burning] = true;
			Player.buffImmune[BuffID.ShadowFlame] = true;
		}

		public override void Kill(double damage, int hitDirection, bool pvp, PlayerDeathReason damageSource)
		{
			if (world1.ReaperMode)
			{
				if (Player.GetModPlayer<DesertReaperSoulPlayer>().DesertReaperUpgrade && !Player.GetModPlayer<MoonReaperSoulPlayer>().MoonReaperUpgrade)
				{
					Player.respawnTimer = (int)((double)Player.respawnTimer * 0.7);
				}
				else if (Player.GetModPlayer<MoonReaperSoulPlayer>().MoonReaperUpgrade && Player.GetModPlayer<DesertReaperSoulPlayer>().DesertReaperUpgrade)
				{
					Player.respawnTimer = (int)((double)Player.respawnTimer * 0.3);
				}
			}
			else
			{
				Player.respawnTimer = (int)((double)Player.respawnTimer * 1);
			}
		}

		public void ReaperSoulsBoost()
		{
			float OgPlayerSpeed = Player.moveSpeed;
			if (world1.ReaperMode)
			{
				if (Player.GetModPlayer<SlimeReaperSoulPlayer>().SlimeReaperUpgrade) Player.moveSpeed = OgPlayerSpeed + 1.30f;
				if (Player.GetModPlayer<EyeReaperSoulPlayer>().EyeeReaperUpgrade) Player.statLifeMax2 += 10;
				if (Player.GetModPlayer<CorruptReaperSoulPlayer>().CorruptReaperUpgrade) Player.lifeRegen += 5;
				
				if (Player.GetModPlayer<BeeReaperSoulPlayer>().BeeReaperUpgrade)
				{
					//Player.honeyCombItem = true;
				}
				if (Player.GetModPlayer<SkeletonReaperSoulPlayer>().SkeletonReaperUpgrade) Player.statDefense += 5;
				if (Player.GetModPlayer<FleshReaperSoulPlayer>().FleshReaperUpgrade) Player.GetDamage(DamageClass.Generic) *= 1.10f;
				if (Player.GetModPlayer<FrozenReaperSoulPlayer>().FrozenReaperUpgrade) FrostInmune();	
				if (Player.GetModPlayer<DestroyerReaperSoulPlayer>().DestroyerReaperUpgrade) Player.pickSpeed -= 0.35f;
				if (Player.GetModPlayer<SpazmatismReaperSoulPlayer>().SpazmatismReaperUpgrade) FirenInmune();
				if (Player.GetModPlayer<SkeletronPrimeReaperSoulPlayer>().SkeletronPrimeReaperUpgrade)
				{
					Player.findTreasure = true;
					Player.jumpSpeedBoost += 10f;
				}
				if (Player.GetModPlayer<PlantReaperSoulPlayer>().PlantReaperUpgrade)
				{
					AddMinion(ProjectileType<CloroCrystalClone>(), 140, 0f);
					Player.statLifeMax2 += 10;
				}
				if (Player.GetModPlayer<InfernalReaperSoulPlayer>().InfernalReaperUpgrade)
				{
					Player.fireWalk = true;
					Player.lavaImmune = true;
					Player.GetDamage(DamageClass.Generic) *= 1.10f;
				}
				if (Player.GetModPlayer<GolemReaperSoulPlayer>().GolemReaperUpgrade) Player.statDefense += 10;
				
				if (world1.ReaperMode && Player.GetModPlayer<DukeReaperSoulPlayer>().DukeReaperUpgrade)
				{
					AddMinion(ProjectileType<TempestClone>(), 140, 10f);
					Player.aggro -= 400;
				}
				if (Player.GetModPlayer<CultistReaperSoulPlayer>().CultistReaperUpgrade) AddMinion(ProjectileType<IceMistF>(), 680, 10f);
				
			}
		}
		public void AddMinion(int proj, int damage, float knockback)
		{
			if (Player.whoAmI == Main.myPlayer && Player.ownedProjectileCounts[proj] < 1 && Player.whoAmI == Main.myPlayer)
			{
				Vector2 velocity = Player.velocity * 1.5f;
				var projectile = Projectile.NewProjectileDirect(Player.GetSource_FromAI(), Player.Center, velocity, proj, damage, knockback, Main.myPlayer);
				projectile.originalDamage = damage;
			}
		}
		public override void PreUpdateBuffs()
		{
			if (world1.ReaperMode)
			{
				Player.AddBuff(BuffType<ReaperBuff>(), 1);
			}
		}

		public void KillMinion(int proj)
		{
			Main.projectile[proj].Kill();
		}




		/*public override void LoadLegacy(BinaryReader reader)
		{
			int loadVersion = reader.ReadInt32();
		}*/


		/*public static void CalamityConvert(){
			Mod CalamityMod = ModLoader.GetMod("CalamityMod");
			if (CalamityMod != null){
			ModPlayer CalamityPlayer = Player.GetModPlayer(CalamityMod, "CalamityPlayer");
			Type CalamityPlayerType = CalamityPlayer.GetType();
			FieldInfo rogueStealthMax = CalamityPlayerType.GetField("rogueStealthMax", BindingFlags.Instance | BindingFlags.Public);
			FieldInfo wearingRogueArmor = CalamityPlayerType.GetField("wearingRogueArmor", BindingFlags.Instance | BindingFlags.Public);
			int oldResource = (int)rogueStealthMax.GetValue(CalamityPlayer);
			int oldResource2 = (int)wearingRogueArmor.GetValue(CalamityPlayer);
			}
		}*/

	}
	public class DashPlayer : ModPlayer
	{
		public const int DashDown = 0;
		public const int DashUp = 1;
		public const int DashRight = 2;
		public const int DashLeft = 3;
		public const int DashCooldown = 50;
		public const int DashDuration = 35;
		public const float DashVelocity = 10f;
		public int DashDir = -1;
		public bool DashEquipped;
		public int DashDelay = 0;
		public int DashTimer = 0;

		public override void ResetEffects()
		{

			DashEquipped = false;
			if (Player.controlDown && Player.releaseDown && Player.doubleTapCardinalTimer[DashDown] < 15)
			{
				DashDir = DashDown;
			}
			else if (Player.controlUp && Player.releaseUp && Player.doubleTapCardinalTimer[DashUp] < 15)
			{
				DashDir = DashUp;
			}
			else if (Player.controlRight && Player.releaseRight && Player.doubleTapCardinalTimer[DashRight] < 15)
			{
				DashDir = DashRight;
			}
			else if (Player.controlLeft && Player.releaseLeft && Player.doubleTapCardinalTimer[DashLeft] < 15)
			{
				DashDir = DashLeft;
			}
			else
			{
				DashDir = -1;
			}
		}
		public override void PreUpdateMovement()
		{
			if (CanUseDash() && DashDir != -1 && DashDelay == 0)
			{
				Vector2 newVelocity = Player.velocity;

				switch (DashDir)
				{
					case DashUp when Player.velocity.Y > -DashVelocity:
					case DashDown when Player.velocity.Y < DashVelocity:
						{
							float dashDirection = DashDir == DashDown ? 1 : -1.3f;
							newVelocity.Y = dashDirection * DashVelocity;
							break;
						}
					case DashLeft when Player.velocity.X > -DashVelocity:
					case DashRight when Player.velocity.X < DashVelocity:
						{
							float dashDirection = DashDir == DashRight ? 1 : -1;
							newVelocity.X = dashDirection * DashVelocity;
							break;
						}
					default:
						return;
				}
				DashDelay = DashCooldown;
				DashTimer = DashDuration;
				Player.velocity = newVelocity;
			}

			if (DashDelay > 0)
				DashDelay--;

			if (DashTimer > 0)
			{
				Player.eocDash = DashTimer;
				Player.armorEffectDrawShadowEOCShield = true;

				DashTimer--;
			}
		}

		private bool CanUseDash()
		{
			return DashEquipped
				&& Player.dashType == 1
				&& !Player.setSolar
				&& !Player.mount.Active;
		}
	}
}

		
 	