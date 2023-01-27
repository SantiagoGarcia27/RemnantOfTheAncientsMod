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
using RemnantOfTheAncientsMod.Items.Fmode;
using RemnantOfTheAncientsMod.Buffs.Debuff;
using RemnantOfTheAncientsMod.Buffs.Scrolls;

namespace RemnantOfTheAncientsMod
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
		public bool ChaliceOn;
		public static bool FWeapons;
		public static bool ReaperFirstTime;
		private static List<NPC> _hallucinationCandidates = new List<NPC>();

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

				if (Player.lifeRegen > 0) Player.lifeRegen = 0;
				Player.lifeRegenTime = 0;
				Player.lifeRegen -= 16;
			}
		}
		public override void OnEnterWorld(Player player)
		{
			FWeapons = true;
			FchangesItem.ReaperWingsNerf(player);
		}
		public override IEnumerable<Item> AddStartingItems(bool mediumCoreDeath)
		{
			if (mediumCoreDeath) return new[] { new Item(ItemType<Ftoggler>()) };
			return new[] { new Item(ItemType<Ftoggler>()), };
		}
		public override void UpdateEquips()
		{
			if (Player.wingTimeMax > 4) FchangesItem.ReaperWingsNerf(Player);
			if(RemnantOfTheAncientsMod.DebuggMode) Debugg();
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
				if (SandWeapons && !item.noMelee && !item.noUseGraphic) target.AddBuff(BuffType<Burning_Sand>(), 300);
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
		public void ScrollInmunity(int buff)
		{
			int b = BuffType<Slim>();
			Player.buffImmune[BuffType<Slim>()] = true;
			Player.buffImmune[BuffType<Eye>()] = true;
			Player.buffImmune[BuffType<AoD>()] = true;
			Player.buffImmune[BuffType<Putrid>()] = true;
			Player.buffImmune[BuffType<Bee>()] = true;
			Player.buffImmune[BuffType<Skeleton>()] = true;
			Player.buffImmune[BuffType<MasterD>()] = true;
			Player.buffImmune[BuffType<Infernal>()] = true;

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
				if (Player.GetModPlayer<EyeReaperSoulPlayer>().EyeeReaperUpgrade) Player.statLifeMax2 += 10;
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
			if(Player.getDPS() >= 1) damage = Player.getDPS()/4; //18;
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
		
        public void Debugg()
		{
			bool rft = GetInstance<ConfigClient1>().ReaperFirsTimeConf;
			if (rft) ReaperFirstTime = true;
			else ReaperFirstTime = false;
		}
	}
}

		
 	