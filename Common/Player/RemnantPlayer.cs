using RemnantOfTheAncientsMod.Content.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using System.Collections.Generic;
using RemnantOfTheAncientsMod.Content.Items.Consumables.DificultChanger;
using RemnantOfTheAncientsMod.World;
using RemnantOfTheAncientsMod.Content.Items.Consumables.ReaperSouls;
using RemnantOfTheAncientsMod.Common.Global;
using RemnantOfTheAncientsMod.Content.Dusts;
using RemnantOfTheAncientsMod.Content.Items.Accesories;
using RemnantOfTheAncientsMod.Content.Buffs.Debuff;
using RemnantOfTheAncientsMod.Content.Buffs.Buffs.Scrolls;
using RemnantOfTheAncientsMod.Content.Buffs.Buffs;
using CalamityMod;
using Terraria.Chat;
using Terraria.Localization;
using RemnantOfTheAncientsMod.Content.Projectiles.Multiclass;

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
		public bool SunflowerSentry;
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
		public static int DummyMode = 0;
		public bool CouwldownHolySaber;
		public static bool tuxoniteStealth;
        public static int tuxoniteStealthDuration = 0;
        public static float tuxoniteStealthCounter = 0;
		public static bool DaylightArmorSetBonus;
		public bool HealingDrone;
        public bool InterceptionDrone;


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
			SunflowerSentry= false;
			tuxoniteStealth = false;
			DaylightArmorSetBonus = false;
			HealingDrone = false;
			InterceptionDrone = false;
			//tuxoniteStealthCounter = 1;
        }


		public override void UpdateDead()
		{
			Burn_Sand = false;
			Hell_Fire = false;
			hBurn = false;
			MeleeKit = false;
			MoneyCollector = false;

			int selection = chanceTomb(GetInstance<ConfigServer>().DropTombstomOnDeadtConf);
			if (selection != 0) 
			{
				if (Main.rand.NextBool(selection))
				{
					RemanntWorld.KillTombstom();
				}
			}
		}
		private int chanceTomb(float config)
		{
			switch (config)
			{
				case 0:
					return 1;
				case 1:
					return 2;	
				case 2:
					return 0;		
				default: 
					return 0;
			}
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
			ReaperGlobalItem.ReaperWingsNerf(player);
			AddScrollBuff();
			if (RemnantOfTheAncientsMod.CalamityMod != null) CalamityMessage();


        }
        [JITWhenModsEnabled("CalamityMod")]
        public void CalamityMessage()
		{
			if(ModContent.GetInstance<CalamityConfig>().RemoveReforgeRNG)
			{
                ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral(Language.GetTextValue("Mods.RemnantOfTheAncientsMod.ChatMessage.CalamityReforgeConfig", Language.GetTextValue("Mods.CalamityMod.Config.EntryTitle.RemoveReforgeRNG"))), Color.Red);
            }

		}
		public override IEnumerable<Item> AddStartingItems(bool mediumCoreDeath)
		{
			if (mediumCoreDeath) return new[] { new Item(ItemType<Ftoggler>()) };
			return new[] { new Item(ItemType<Ftoggler>()), };
		}
		public override void UpdateEquips()
		{
			if (Player.wingTimeMax > 4) ReaperGlobalItem.ReaperWingsNerf(Player);
			//if(RemnantOfTheAncientsMod.DebuggMode) Debugg();
		}
		public static void ApplyBuffToAllPlayers(int BuffId, int Hours, int Minutes, int Seconds)
		{
			int BuffTime = (Seconds *60) + (Minutes * 60 * 60)+ (Hours * 60 *60 *60);
			int MaxPlayer = RemnantOfTheAncientsMod.MaxPlayerOnline();
            for (int CurrentPlayer = 0; CurrentPlayer <= MaxPlayer; CurrentPlayer++) 
			{
				Main.player[CurrentPlayer].AddBuff(BuffId, BuffTime);
			}
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
			//  ReaperGlobalItem.ReaperSize(p);
			if (proj.CountsAsClass(DamageClass.Melee) || proj.CountsAsClass(DamageClass.SummonMeleeSpeed)|| proj.CountsAsClass(DamageClass.Throwing))
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
		public void SpawnMinionItem(Player player)
		{
           if(InterceptionDrone) SpawnHelper(player.Center,Vector2.Zero,ProjectileType<InterceptionDrone>(), 1, 0,Main.myPlayer);
           if (HealingDrone) SpawnHelper(player.Center,new Vector2(7f,2f), ProjectileType<InterceptionDrone>(), 1, 0,Main.myPlayer);
		   
        }
		public void SpawnHelper(Vector2 center, Vector2 velocity, int type, int damage, int knockback, int owner, float Ai0, float Ai1)
		{
			if (Player.whoAmI == Main.myPlayer && Player.ownedProjectileCounts[type] < 1 && Player.whoAmI == Main.myPlayer)
			{
				var projectile = Projectile.NewProjectileDirect(Player.GetSource_FromAI(), center, velocity, type, damage, knockback, owner, (float)Ai0, (float)Ai1);
				projectile.originalDamage = damage;
			}
		}
		public void SpawnHelper(Vector2 center, Vector2 velocity, int type, int damage, int knockback, int owner)
		{
			if (Player.whoAmI == Main.myPlayer && Player.ownedProjectileCounts[type] < 1 && Player.whoAmI == Main.myPlayer)
			{
				var projectile = Projectile.NewProjectileDirect(Player.GetSource_FromAI(), center, velocity, type, damage, knockback, owner);
				projectile.originalDamage = damage;
			}
        }

        public override void Kill(double damage, int hitDirection, bool pvp, PlayerDeathReason damageSource)
		{
			Player.respawnTimer *= (int)RespawnCalculator();
		}
		private float RespawnCalculator()
		{
			bool DesertAnhilatorActivated = (GetInstance<ConfigClient>().ToggleDesertAnhilatorSoul && Player.GetModPlayer<DesertReaperSoulPlayer>().DesertReaperUpgrade) ? true : false;

            bool MoonlordRespawnActivated = (GetInstance<ConfigClient>().ToggleMoonlordSoul && Player.GetModPlayer<MoonReaperSoulPlayer>().MoonReaperUpgrade) ? true : false;

			if (Reaper.ReaperMode) 
			{
				if (DesertAnhilatorActivated && !MoonlordRespawnActivated) return 0.7f;
				else if (!DesertAnhilatorActivated && MoonlordRespawnActivated) return 0.5f;
				else if (DesertAnhilatorActivated && MoonlordRespawnActivated) return 0.3f;
			}
			return 1;
		}
		public void ReaperSoulsBoost()
		{
			float OgPlayerSpeed = Player.moveSpeed;
			if (Reaper.ReaperMode && ChaliceOn)
			{
				if (Player.GetModPlayer<SlimeReaperSoulPlayer>().SlimeReaperUpgrade)
				{
					float speedForce = (GetInstance<ConfigClient>().ToggleSkeletronPrimeSoul/100) + 1;
                    Player.moveSpeed = OgPlayerSpeed + speedForce;
				}
				if (Player.GetModPlayer<EyeReaperSoulPlayer>().EyeReaperUpgrade && GetInstance<ConfigClient>().ToggleEyeOfChutuluSoul)
				{
					Player.statLifeMax2 += 10;
				}
				if (Player.GetModPlayer<CorruptReaperSoulPlayer>().CorruptReaperUpgrade && GetInstance<ConfigClient>().ToggleCorruptSoul)
				{
					Player.lifeRegen += 5;
				}
				if (Player.GetModPlayer<BeeReaperSoulPlayer>().BeeReaperUpgrade && GetInstance<ConfigClient>().ToggleQueenBeeSoul)
				{
					Player.honey = true;
					Player.beeDamage(20);
				}
				if (Player.GetModPlayer<SkeletonReaperSoulPlayer>().SkeletonReaperUpgrade && GetInstance<ConfigClient>().ToggleSkeletronSoul) 
				{ 
					Player.statDefense += 5; 
				}
				if (Player.GetModPlayer<FleshReaperSoulPlayer>().FleshReaperUpgrade && GetInstance<ConfigClient>().ToggleWallOfFleshSoul)
				{
					Player.GetDamage(DamageClass.Generic) *= 1.10f;
				}
				if (Player.GetModPlayer<FrozenReaperSoulPlayer>().FrozenReaperUpgrade && GetInstance<ConfigClient>().ToggleFrozenAssaulterSoul) FrostInmune();
				if (Player.GetModPlayer<QueenReaperSoulPlayer>().QueenReaperUpgrade && GetInstance<ConfigClient>().ToggleQueenSlimeSoul) Player.statLifeMax2 += 15;
				if (Player.GetModPlayer<DestroyerReaperSoulPlayer>().DestroyerReaperUpgrade && GetInstance<ConfigClient>().ToggleDestroyerSoul) Player.pickSpeed -= 0.35f;
				if (Player.GetModPlayer<SpazmatismReaperSoulPlayer>().SpazmatismReaperUpgrade) FirenInmune();
				if (Player.GetModPlayer<SkeletronPrimeReaperSoulPlayer>().SkeletronPrimeReaperUpgrade)
				{
                    float JumpForce = ModContent.GetInstance<ConfigClient>().ToggleSkeletronPrimeSoul;
                    Player.findTreasure = true;
					Player.jumpSpeedBoost += JumpForce;
				}
				if (Player.GetModPlayer<EmpressReaperSoulPlayer>().EmpressReaperUpgrade && GetInstance<ConfigClient>().ToggleEmpressOfLightSoul) Player.empressBrooch = true;

				if (Player.GetModPlayer<InfernalReaperSoulPlayer>().InfernalReaperUpgrade && GetInstance<ConfigClient>().ToggleInfernalTyrantSoul)
				{
					Player.fireWalk = true;
					Player.lavaImmune = true;
					Player.GetDamage(DamageClass.Generic) *= 1.10f;
				}
				if (Player.GetModPlayer<GolemReaperSoulPlayer>().GolemReaperUpgrade && GetInstance<ConfigClient>().ToggleGolemSoul) Player.statDefense += 10;

				if (Reaper.ReaperMode && Player.GetModPlayer<DukeReaperSoulPlayer>().DukeReaperUpgrade && GetInstance<ConfigClient>().ToggleDukeFishronSoul)
				{
					AddMinion(ProjectileType<TempestClone>(), 140, 10f);
					Player.aggro -= 400;
				}
				if (Player.GetModPlayer<CultistReaperSoulPlayer>().CultistReaperUpgrade && GetInstance<ConfigClient>().ToggleLunaticCultistSoul) AddMinion(ProjectileType<IceMistF>(), 680, 10f);

			}
		}
		public void ReaperSoulsBoost(Item item)
		{
			if (Reaper.ReaperMode && ChaliceOn)
			{
				if (Player.GetModPlayer<PlantReaperSoulPlayer>().PlantReaperUpgrade && GetInstance<ConfigClient>().TogglePlanteraSoul)
                {
					Player.sporeSac = true;
					Player.SporeSac(item);
					Player.statLifeMax2 += 10;
				}
				if (Player.GetModPlayer<DeerclopsReaperSoulPlayer>().DeerclopsReaperUpgrade && GetInstance<ConfigClient>().ToggleDearclopsSoul) SpawnHallucination(item);
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
           if(GetInstance<ConfigClient>().KitsGrav) player.gravControl = true;
           if(GetInstance<ConfigClient>().KitsFeatherFall) player.slowFall = true;

			player.buffImmune[8] = true;
			player.buffImmune[18] = true;
		}

		public static void FishingInfusion(Player player)
		{
			player.calmed = true;
			player.fishingSkill += 15;
			player.sonarPotion = true;
			player.cratePotion = true;
            if (ModContent.GetInstance<ConfigClient>().KitsInvis) player.invis = true;
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
            if (ModContent.GetInstance<ConfigClient>().KitsInferno) player.inferno = true;

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

		
 	