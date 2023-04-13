using RemnantOfTheAncientsMod.Buffs;
using RemnantOfTheAncientsMod.Dusts;
using RemnantOfTheAncientsMod.Projectiles;
using RemnantOfTheAncientsMod.VanillaChanges;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using System.Collections.Generic;
using RemnantOfTheAncientsMod.Items.DificultChanger;
using RemnantOfTheAncientsMod.World;
using RemnantOfTheAncientsMod.Items.ReaperSouls;
using RemnantOfTheAncientsMod.Buffs.Debuff;
using RemnantOfTheAncientsMod.Buffs.Scrolls;
using RemnantOfTheAncientsMod.Items.accesorios;
using RemnantOfTheAncientsMod.Buffs.Buffs;

namespace RemnantOfTheAncientsMod
{

	public class RemnantPlayer : ModPlayer
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
		public bool MeleeKit;
		public int healHurt;
		public bool TortugaPet;
		public bool TwitchPet;
		public bool YtPet;
		public bool ModPlayer = true;
		public bool anyBossIsAlive;
		public bool ChaliceOn;
		public bool MoneyCollector;
		public static bool FWeapons;
		public static bool ReaperFirstTime;
		private static List<NPC> _hallucinationCandidates = new List<NPC>();
		public List<int> ScrollsBuff = new List<int>();


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
			ChaliceOn = false;
			MeleeKit = false;
			MoneyCollector = false;
		}


		public override void UpdateDead()
		{
			Burn_Sand = false;
			Hell_Fire = false;
			hBurn = false;
			MeleeKit = false;
			MoneyCollector = false;
		}
		public override void UpdateBadLifeRegen()
		{
			if (Burn_Sand || Hell_Fire || hBurn)
			{

				if (Player.lifeRegen > 0) Player.lifeRegen = 0;
				Player.lifeRegenTime = 0;
				Player.lifeRegen -= 16;
			}
		}
		public override void PostUpdate()
		{
			if (MoneyCollector)
			{
				MoneyColectorBuff.UpdateCoins(Player);
			}
		}

		public override void OnEnterWorld(Player player)
		{
			FWeapons = true;
			FchangesItem.ReaperWingsNerf(player);
			AddScrollBuff();
		}
		public override IEnumerable<Item> AddStartingItems(bool mediumCoreDeath)
		{
			if (mediumCoreDeath) return new[] { new Item(ItemType<Ftoggler>()) };
			return new[] { new Item(ItemType<Ftoggler>()), };
		}
		public override void UpdateEquips()
		{
			if (Player.wingTimeMax > 4) FchangesItem.ReaperWingsNerf(Player);
			//if(RemnantOfTheAncientsMod.DebuggMode) Debugg();
		}
		public void ReaperStarter()
		{
			if (!ReaperFirstTime)
			{
				Player.QuickSpawnItem(Player.GetSource_DropAsItem(), ItemID.ReaperHood);
				Player.QuickSpawnItem(Player.GetSource_DropAsItem(), ItemID.ReaperRobe);
				Player.QuickSpawnItem(Player.GetSource_DropAsItem(), ItemType<ReaperChalice>());
			}
		}

		public override void DrawEffects(PlayerDrawSet drawInfo, ref float r, ref float g, ref float b, ref float a, ref bool fullBright)
		{
			if (Burn_Sand && Main.rand.NextBool(4) && drawInfo.shadow == 0f)
			{
				int dust = Dust.NewDust(drawInfo.Position - new Vector2(2f, 2f), Player.width + 4, Player.height + 4, DustType<QuemaduraA>(), Player.velocity.X * 0.4f, Player.velocity.Y * 0.4f, 100, default, 3f);
				Main.dust[dust].noGravity = true;
				Main.dust[dust].velocity *= 1.8f;
				Main.dust[dust].velocity.Y -= 0.5f;
				drawInfo.DustCache.Add(dust);
			}
			if (Hell_Fire && Main.rand.NextBool(4) && drawInfo.shadow == 0f)
			{

				int dust = Dust.NewDust(drawInfo.Position - new Vector2(2f, 2f), Player.width + 4, Player.height + 4, DustType<Hell_Fire_P>(), Player.velocity.X * 0.4f, Player.velocity.Y * 0.4f, 100, default, 3f);
				Main.dust[dust].noGravity = true;
				Main.dust[dust].velocity *= 1.8f;
				Main.dust[dust].velocity.Y -= 0.5f;
				drawInfo.DustCache.Add(dust);

			}
			if (hBurn && Main.rand.NextBool(4) && drawInfo.shadow == 0f)
			{
				int dust = Dust.NewDust(drawInfo.Position - new Vector2(2f, 2f), Player.width + 4, Player.height + 4, DustType<HollyBurn_P>(), Player.velocity.X * 0.4f, Player.velocity.Y * 0.4f, 100, default, 3f);
				Main.dust[dust].noGravity = true;
				Main.dust[dust].velocity *= 1.8f;
				Main.dust[dust].velocity.Y -= 0.5f;
				drawInfo.DustCache.Add(dust);
			}
		}
		public override void OnHitNPC(Item item, NPC target, int damage, float knockback, bool crit)
		{
			//item.GetGlobalItem<FchangesItem>().ReaperSize(item);
			if (!item.noMelee && !item.noUseGraphic)
			{
				if (hasInfernal_core) target.AddBuff(BuffType<Hell_Fire>(), 300);
				if (SandWeapons) target.AddBuff(BuffType<Burning_Sand>(), 300);
				if (MeleeKit) target.AddBuff(BuffID.Ichor, 300);
			}
		}
		public override void OnHitNPCWithProj(Projectile proj, NPC target, int damage, float knockback, bool crit) //This is the same as the one in OnHitNPC, but for melee projectiles.
		{
			//  FchangesItem.ReaperSize(p);
			if (proj.CountsAsClass(DamageClass.Melee))
			{
				if (hasInfernal_core) target.AddBuff(BuffType<Hell_Fire>(), 300);
				if (SandWeapons) target.AddBuff(BuffType<Burning_Sand>(), 300);
			}
		}
		public void UndeadInmunity()
		{
			Player.buffImmune[BuffID.Blackout] = true;
			Player.buffImmune[BuffID.Horrified] = true;
			Player.buffImmune[BuffID.Suffocation] = true;
			Player.buffImmune[BuffID.ChaosState] = true;
			Player.buffImmune[BuffID.TheTongue] = true;
			Player.buffImmune[BuffID.CursedInferno] = true;
			Player.buffImmune[BuffID.Ichor] = true;
			AnkInmunity();
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
		public void AddScrollBuff()
		{
			ScrollsBuff.Add(BuffType<Slim>());
			ScrollsBuff.Add(BuffType<Eye>());
			ScrollsBuff.Add(BuffType<BrainOfChutuluScrollBuff>());
			ScrollsBuff.Add(BuffType<Putrid>());
			ScrollsBuff.Add(BuffType<Skeleton>());
			ScrollsBuff.Add(BuffType<Bee>());
			ScrollsBuff.Add(BuffType<MasterD>());
			ScrollsBuff.Add(BuffType<Infernal>());
			ScrollsBuff.Add(BuffType<QueenSlimeScrollBuff>());
		}
		public void ScrollInmunity(int buff)
		{
			for (int i = 0; i < ScrollsBuff.Count; i++)
			{
				Player.buffImmune[ScrollsBuff[i]] = true;
			}
			Player.buffImmune[buff] = false;
		}
		public void ExoticA(int l, int m, int m2, int p, Item item)
		{
			Player.lifeRegen += l;
			Player.manaRegenBonus = m;
			Player.statManaMax2 += m2;
			Player.GetArmorPenetration(DamageClass.Generic) += p;
			Player.pStone = true;
			if (Player.whoAmI == Main.myPlayer)
			{
				Player.starCloakItem = item;
				Player.honeyCombItem = item;
			}
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
			if (Reaper.ReaperMode)
			{
				if (Player.GetModPlayer<DesertReaperSoulPlayer>().DesertReaperUpgrade && !Player.GetModPlayer<MoonReaperSoulPlayer>().MoonReaperUpgrade) Player.respawnTimer = (int)(Player.respawnTimer * 0.7);
				else if (Player.GetModPlayer<MoonReaperSoulPlayer>().MoonReaperUpgrade && Player.GetModPlayer<DesertReaperSoulPlayer>().DesertReaperUpgrade) Player.respawnTimer = (int)(Player.respawnTimer * 0.3);
			}
			else Player.respawnTimer = (int)((double)Player.respawnTimer * 1);
		}

		public void ReaperSoulsBoost()
		{
			float OgPlayerSpeed = Player.moveSpeed;
			if (Reaper.ReaperMode && ChaliceOn)
			{
				if (Player.GetModPlayer<SlimeReaperSoulPlayer>().SlimeReaperUpgrade) Player.moveSpeed = OgPlayerSpeed + 1.30f;
				if (Player.GetModPlayer<EyeReaperSoulPlayer>().EyeReaperUpgrade) Player.statLifeMax2 += 10;
				if (Player.GetModPlayer<CorruptReaperSoulPlayer>().CorruptReaperUpgrade) Player.lifeRegen += 5;
				if (Player.GetModPlayer<BeeReaperSoulPlayer>().BeeReaperUpgrade)
				{
					Player.honey = true;
					Player.beeDamage(20);
				}
				if (Player.GetModPlayer<SkeletonReaperSoulPlayer>().SkeletonReaperUpgrade) Player.statDefense += 5;
				if (Player.GetModPlayer<FleshReaperSoulPlayer>().FleshReaperUpgrade) Player.GetDamage(DamageClass.Generic) *= 1.10f;
				if (Player.GetModPlayer<FrozenReaperSoulPlayer>().FrozenReaperUpgrade) FrostInmune();
				if (Player.GetModPlayer<QueenReaperSoulPlayer>().QueenReaperUpgrade) Player.statLifeMax2 += 15;
				if (Player.GetModPlayer<DestroyerReaperSoulPlayer>().DestroyerReaperUpgrade) Player.pickSpeed -= 0.35f;
				if (Player.GetModPlayer<SpazmatismReaperSoulPlayer>().SpazmatismReaperUpgrade) FirenInmune();
				if (Player.GetModPlayer<SkeletronPrimeReaperSoulPlayer>().SkeletronPrimeReaperUpgrade)
				{
					Player.findTreasure = true;
					Player.jumpSpeedBoost += 10f;
				}
				if (Player.GetModPlayer<EmpressReaperSoulPlayer>().EmpressReaperUpgrade) Player.empressBrooch = true;

				if (Player.GetModPlayer<InfernalReaperSoulPlayer>().InfernalReaperUpgrade)
				{
					Player.fireWalk = true;
					Player.lavaImmune = true;
					Player.GetDamage(DamageClass.Generic) *= 1.10f;
				}
				if (Player.GetModPlayer<GolemReaperSoulPlayer>().GolemReaperUpgrade) Player.statDefense += 10;

				if (Reaper.ReaperMode && Player.GetModPlayer<DukeReaperSoulPlayer>().DukeReaperUpgrade)
				{
					AddMinion(ProjectileType<TempestClone>(), 140, 10f);
					Player.aggro -= 400;
				}
				if (Player.GetModPlayer<CultistReaperSoulPlayer>().CultistReaperUpgrade) AddMinion(ProjectileType<IceMistF>(), 680, 10f);

			}
		}
		public void ReaperSoulsBoost(Item item)
		{
			if (Reaper.ReaperMode && ChaliceOn)
			{
				if (Player.GetModPlayer<PlantReaperSoulPlayer>().PlantReaperUpgrade)
				{
					Player.sporeSac = true;
					Player.SporeSac(item);
					Player.statLifeMax2 += 10;
				}
				if (Player.GetModPlayer<DeerclopsReaperSoulPlayer>().DeerclopsReaperUpgrade) SpawnHallucination(item);
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
			if (Reaper.ReaperMode) Player.AddBuff(BuffType<ReaperBuff>(), 1);
		}
		public void KillMinion(int proj) => Main.projectile[proj].Kill();

		private void SpawnHallucination(Item item)
		{
			if (Player.whoAmI != Main.myPlayer) return;

			Player.insanityShadowCooldown = Utils.Clamp(Player.insanityShadowCooldown - 1, 0, 100);

			if (Player.insanityShadowCooldown > 0) return;

			Player.insanityShadowCooldown = Main.rand.Next(20, 101);
			float num = 500f;
			int damage = 10;
			if (Player.getDPS() >= 1) damage = Player.getDPS() / 4; //18;
			_hallucinationCandidates.Clear();
			for (int i = 0; i < 200; i++)
			{
				NPC nPC = Main.npc[i];
				if (nPC.CanBeChasedBy(this) && !(Player.Distance(nPC.Center) > num) && Collision.CanHitLine(Player.position, Player.width, Player.height, nPC.position, nPC.width, nPC.height)) _hallucinationCandidates.Add(nPC);
			}
			if (_hallucinationCandidates.Count != 0)
			{
				Projectile.RandomizeInsanityShadowFor(Main.rand.NextFromCollection(_hallucinationCandidates), isHostile: false, out var spawnposition, out var spawnvelocity, out var ai, out var ai2);
				Projectile.NewProjectile(new EntitySource_ItemUse(Player, item), spawnposition, spawnvelocity, ProjectileID.InsanityShadowFriendly, damage, 0f, Player.whoAmI, ai, ai2);
			}
		}

		public static void BasicInfusion(Player player)
		{
			player.moveSpeed += 0.25f;
			player.lifeRegen += 4;
			player.statDefense += 8;
			player.buffImmune[2] = true;
			player.buffImmune[3] = true;
			player.buffImmune[5] = true;
		}
		public static void AdvancedInfusion(Player player)
		{
			BasicInfusion(player);
			ref StatModifier GenericDamage = ref (player.GetDamage<GenericDamageClass>());
			GenericDamage += 0.10f;
			player.GetCritChance(DamageClass.Generic) += 10f;
			player.buffImmune[115] = true;
			player.buffImmune[117] = true;
		}
		public static void MeleeInfusion(Player player)
		{
			new RemnantPlayer().MeleeKit = true;
			if (player.inventory[player.selectedItem].CountsAsClass(DamageClass.Melee)) player.statDefense -= 4;
			player.GetCritChance(DamageClass.Melee) += 2f;
			player.GetAttackSpeed(DamageClass.Melee) += 0.1f;
			player.kbBuff = true;

			player.buffImmune[25] = true;
			player.buffImmune[108] = true;
		}
		public static void MiningInfusion(Player player)
		{
			player.findTreasure = true;
			player.nightVision = true;
			player.pickSpeed -= 0.25f;
			Lighting.AddLight((int)((double)player.position.X + player.width / 2) / 16, (int)(player.position.Y + (double)(player.height / 2)) / 16, 1f, 1f, 2f);//16,3f,3f,3f
			player.buffImmune[11] = true;
			player.buffImmune[12] = true;
			player.buffImmune[104] = true;
			player.buffImmune[9] = true;
		}
		public static void FishPotion(Player player)
		{
			player.waterWalk = true;
			player.ignoreWater = true;
			player.accFlipper = true;
			player.gills = true;
			player.buffImmune[4] = true;
			player.buffImmune[15] = true;
			player.buffImmune[109] = true;

		}
		public static void RangerInfusion(Player player)
		{
			player.arrowDamage.Flat *= 0.2f;
			player.archery = true;
			player.ammoPotion = true;

			player.buffImmune[16] = true;
			player.buffImmune[112] = true;
		}

		public static void GravityControlPotion(Player player)
		{
           if(GetInstance<ConfigClient1>().KitsGrav) player.gravControl = true;
           if(GetInstance<ConfigClient1>().KitsFeatherFall) player.slowFall = true;

			player.buffImmune[8] = true;
			player.buffImmune[18] = true;
		}

		public static void FishingInfusion(Player player)
		{
			player.calmed = true;
			player.fishingSkill += 15;
			player.sonarPotion = true;
			player.cratePotion = true;
            if (ModContent.GetInstance<ConfigClient1>().KitsInvis) player.invis = true;
			player.luck += 0.4f;

			player.buffImmune[10] = true;
			player.buffImmune[106] = true;
			player.buffImmune[121] = true;
			player.buffImmune[122] = true;
			player.buffImmune[123] = true;
			player.buffImmune[257] = true;
		}

		public static void MageInfusion(Player player)
		{
			ref StatModifier MagicDamage = ref (player.GetDamage<MagicDamageClass>());
			MagicDamage += 0.20f;
			player.manaRegen += 4;

			player.buffImmune[6] = true;
			player.buffImmune[7] = true;
		}

		public static void TankInfusion(Player player)
		{
			player.statLifeMax2 += (player.statLifeMax * 20) / 100;
			player.endurance += 0.10f;
			player.thorns += 0.33f;
			player.lifeMagnet = true;
			player.resistCold = true;

			player.buffImmune[113] = true;
			player.buffImmune[114] = true;
			player.buffImmune[124] = true;
			player.buffImmune[14] = true;
			player.buffImmune[105] = true;
		}

		public static void SummonInfusion(Player player)
		{
			player.maxMinions++;
			player.maxTurrets++;
            if (ModContent.GetInstance<ConfigClient1>().KitsInferno) player.inferno = true;

			player.buffImmune[110] = true;
			player.buffImmune[116] = true;
			player.buffImmune[BuffType<CentryBuff>()] = true;
		}
		public static void SuperSenseBuff(Player player)
		{
			player.detectCreature = true;
			player.findTreasure = true;

			player.buffImmune[111] = true;
			player.buffImmune[17] = true;
		}

		public static void ExplorationInfusion(Player player)
		{
			player.lavaImmune = true;
			GravityControlPotion(player);
			SuperSenseBuff(player);
			MiningInfusion(player);
			player.buffImmune[1] = true;
		}

		public static void PoseidonInfusion(Player player)
		{
			FishingInfusion(player);
			FishPotion(player);
		}
		public static void CombatInfusion(Player player)
		{
			MeleeInfusion(player);
			RangerInfusion(player);
			MageInfusion(player);
			SummonInfusion(player);
			TankInfusion(player);
			AdvancedInfusion(player);
		}
		public static void DefinitiveInfusion(Player player)
		{
			CombatInfusion(player);
			ExplorationInfusion(player);
			PoseidonInfusion(player);
			player.tileSpeed += 0.25f;
			player.wallSpeed += 0.25f;
			player.blockRange++;
			player.buffImmune[107] = true;
			player.GetModPlayer<RemnantPlayer>().MoneyCollector = true;
		}
		/* public void Debugg()
		 {
 bool rft = GetInstance<ConfigClient1>().ReaperFirsTimeConf;
 if (rft) ReaperFirstTime = true;
 else ReaperFirstTime = false;
		 }*/
	}
}

		
 	