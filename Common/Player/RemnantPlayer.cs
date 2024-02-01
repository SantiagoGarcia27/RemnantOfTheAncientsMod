using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using System.Collections.Generic;
using RemnantOfTheAncientsMod.World;
using RemnantOfTheAncientsMod.Content.Dusts;
using RemnantOfTheAncientsMod.Content.Buffs.Debuff;
using RemnantOfTheAncientsMod.Content.Buffs.Buffs.Scrolls;
using RemnantOfTheAncientsMod.Content.Buffs.Buffs;
using RemnantOfTheAncientsMod.Content.Projectiles.Multiclass;
using Terraria.Audio;
using Terraria.GameInput;
using RemnantOfTheAncientsMod.Common.UtilsTweaks;
using RemnantOfTheAncientsMod.Content.Items.Consumables.Pociones;
using RemnantOfTheAncientsMod.Content.Items.Armor.Cosmetic.Sangar;
using RemnantOfTheAncientsMod.Content.Items.Armor.Cosmetic.TTIM;
using Terraria.Chat;
using Terraria.Localization;
using Terraria.UI;
using RemnantOfTheAncientsMod.Common.UI.ReaperUI;
using RemnantOfTheAncientsMod.Common.Global.NPCs;
using RemnantOfTheAncientsMod.Common;
using RemnantOfTheAncientsMod.Content.Projectiles.Fargos.Eternity;
using CalamityMod;
using RemnantOfTheAncientsMod.Common.ModCompativilitie;

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
		#region Debuffs
		public bool Burn_Sand;
		public bool hBurn;
		public bool Marble_Erosion;
		#endregion

		#region pets
		public bool TortugaPet;
		public bool TwitchPet;
		public bool YtPet;
		#endregion
		public bool Hell_Fire;
		public int healHurt;

		public bool anyBossIsAlive;
		public bool MoneyCollector;
		public List<int> ScrollsBuff = new List<int>();
		public static int DummyMode = 0;
		public bool Inmortal;


		public static bool DaylightArmorSetBonus;
		public static bool CanWormHole;
		public bool HealingDrone;
		public bool InterceptionDrone;
		public bool DesertHeraldSetBonus;
		public bool BrainDogde;
		public float EnemyProjectilesScaleBouns = 1;
		public float EnemyProjectilesSpeedScaleBouns = 1;

		public int StyleStat = 0;
		#region tuxonite
		public static bool tuxoniteStealth;
		public static int tuxoniteStealthDuration = 0;
		public static float tuxoniteStealthCounter = 0;

	
		#endregion



		public List<int> MinionsBuffInflict = new List<int> { };
		public List<int> MeleeBuffInflict = new List<int> { };
		public List<int> MageBuffInflict = new List<int> { };
		public List<int> RangerBuffInflict = new List<int> { };
		public List<int> TrowerBuffInflict = new List<int> { };
		public List<int> AllClassBuffInflict = new List<int> { };
		public int MinionCritChance = 0;

		#region Couldowns
		public bool CouwldownHolySaber;
		#endregion

		public static List<int> DevSuits = new List<int>()
		{
            666, 667, 668, 665,
            1554, 1555, 1556, 1586,
			1554, 1587, 1588, 1586,
			1557, 1558, 1559, 1585,
			1560, 1561, 1562, 1584,
			1563, 1564, 1565, 3582,
			1566, 1567, 1568,
			1580, 1581, 1582, 1583,
			3226, 3227, 3228, 3288,
			3583, 3581, 3578, 3579,
			3585, 3586, 3587, 3588,
			3589, 3590, 3591, 3592,
			3368, 3921, 3922, 3923,
			3925, 3926, 3927, 3928,
			4732, 4733, 4734, 4730,
			4747, 4748, 4749, 4746,
			4751, 4752, 4753, 4750,
			4755, 4756, 4757, 4754,
			
        };

		


		public override void ResetEffects()
		{
			Burn_Sand = false;
			hBurn = false;
			Hell_Fire = false;
			Marble_Erosion = false;
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
			MoneyCollector = false;
			SunflowerSentry = false;
			tuxoniteStealth = false;
			
			DaylightArmorSetBonus = false;
			HealingDrone = false;
			InterceptionDrone = false;
			DesertHeraldSetBonus = false;
			CanWormHole = false;
			BrainDogde = false;
			Inmortal = false;
            EnemyProjectilesScaleBouns = 1;
			EnemyProjectilesSpeedScaleBouns = 1;
		
			MinionCritChance = 0;
            StyleStat = 0;

            if (MinionsBuffInflict.Count > 0) MinionsBuffInflict.Clear();
			if (MeleeBuffInflict.Count > 0) MeleeBuffInflict.Clear();
			if (MageBuffInflict.Count > 0) MageBuffInflict.Clear();
			if (RangerBuffInflict.Count > 0) RangerBuffInflict.Clear();
			if (TrowerBuffInflict.Count > 0) TrowerBuffInflict.Clear();
			if (AllClassBuffInflict.Count > 0) AllClassBuffInflict.Clear();
		}

		public override void Load()
		{
			On_Player.TryGettingDevArmor += Player_TryGettingDevArmor;
			base.Load();
		}

       
        public override bool PreKill(double damage, int hitDirection, bool pvp, ref bool playSound, ref bool genGore, ref PlayerDeathReason damageSource)
        {
            if (Inmortal)
            {
                Player.statLife = Player.statLifeMax2;
                Player.lifeRegen = 999;
                return false;
            }
            return true;
        }
        private void Player_TryGettingDevArmor(On_Player.orig_TryGettingDevArmor orig, Player player, IEntitySource source)
		{
			//TryGettingPatreonOrDevArmor(source, this);
			Dictionary<int, List<int>> Suits = new Dictionary<int, List<int>>()
			{
				{ 0, new List<int>() { 666, 667, 668, 665 }},
				{ 1, new List<int>() { 1554, 1555, 1556, 1586}},
				{ 2, new List<int>()  { 1554, 1587, 1588, 1586 }},
				{ 3, new List<int>() { 1557, 1558, 1559, 1585 }},
				{ 4, new List<int>()  { 1560, 1561, 1562, 1584 }},
				{ 5, new List<int>() { 1563, 1564, 1565, 3582 }},
				{ 6, new List<int>() { 1566, 1567, 1568}},
				{ 7, new List<int>() { 1580, 1581, 1582, 1583 }},
				{ 8, new List<int>()  { 3226, 3227, 3228, 3288 }},
				{ 9, new List<int>() { 3583, 3581, 3578, 3579 }},
				{ 10, new List<int>() { 3585, 3586, 3587, 3588 }},
				{ 11, new List<int>() { 3589, 3590, 3591, 3592 }},
				{ 12, new List<int>() { 3368, 3921, 3922, 3923 }},
				{ 13, new List<int>() { 3925, 3926, 3927, 3928 }},
				{ 14, new List<int>() { 4732, 4733, 4734, 4730 }},
				{ 15, new List<int>() { 4747, 4748, 4749, 4746 }},
				{ 16, new List<int>() { 4751, 4752, 4753, 4750 }},
				{ 17, new List<int>() { 4755, 4756, 4757, 4754 }},
				{ 18, new List<int>() { ItemType<Sangar_Head>(), ItemType<Sangar_Body>(), ItemType<Sangar_Legs>() }},
				{ 19, new List<int>() { ItemType<Ttim_Head>(), ItemType<Ttim_Body>(), ItemType<Ttim_Legs>() }}

			};

			if (Main.rand.NextBool(Main.tenthAnniversaryWorld ? 10 : DificultyUtils.ReaperMode ? 2 : 20))
			{
				int selection = Main.rand.Next(Suits.Count);

				if (Suits.ContainsKey(selection))
				{
					SpawnSuit(player, source, Suits[selection]);

				}
			}
		}
		public void SpawnSuit(Player player, IEntitySource source, List<int> suitParts)
		{
			foreach (var suitPart in suitParts)
			{
				player.QuickSpawnItem(source, suitPart);
			}
		}

		public override void UpdateDead()
		{
			Burn_Sand = false;
			Hell_Fire = false;
			hBurn = false;
			MoneyCollector = false;
			Marble_Erosion = false;


			int selection = chanceTomb(GetInstance<ConfigServer>().DropTombstomOnDeadtConf);
			if (selection != 0)
			{
				if (Main.rand.NextBool(selection))
				{
					RemanntWorld.KillTombstom();
				}
			}
		}
		public void checkInventory(Player player)
		{
			CanWormHole = Utils1.IsItemOnPlayerInventory(ItemType<EndlessWormHole>(), player);
		}

		private int chanceTomb(float config)
		{
			if (config == 0 || config == 1) return (int)config++;
			return 0;
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
			Player.opacityForAnimation = 1;

			
			if (MoneyCollector)
			{
				MoneyColectorBuff.UpdateCoins(Player);
			}
			checkInventory(Player);

			if (Utils1.IsItemOnPlayerInventory(ItemType<EndlessWormHole>()))
			{
				WormHoleEffect(Player);
			}
			SpawnMimics(Player);

		}
		public override bool FreeDodge(Player.HurtInfo info)
		{
			if (BrainDogde)
			{
				if (Main.rand.NextBool(5))
				{
					Player.BrainOfConfusionDodge();
					Player.SetImmuneTimeForAllTypes(120);
					Dust.QuickDust(Player.position, Color.Gray);
					return true;
				}
			}
			return base.FreeDodge(info);
		}
		public override void OnEnterWorld()
		{
			AddScrollBuff();
			if (ModLoader.TryGetMod("CalamityMod", out Mod CalamityMod)) CalamityMessage();
		}
		[JITWhenModsEnabled("CalamityMod")]
		public void CalamityMessage()
		{
			if (GetInstance<CalamityConfig>().RemoveReforgeRNG)
			{
				ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral(Language.GetTextValue("Mods.RemnantOfTheAncientsMod.ChatMessage.CalamityReforgeConfig", Language.GetTextValue("Mods.CalamityMod.Configs.CalamityConfig.RemoveReforgeRNG.Label"))), Color.Red);
			}
		}
		public static void WormHoleEffect(Player player)
		{
			if (Main.mapFullscreen && Main.netMode == NetmodeID.MultiplayerClient && Main.myPlayer == player.whoAmI && player.team > 0 && Main.mouseLeft && Main.mouseLeftRelease)
			{
				for (int k = 0; k < 255; k++)
				{
					if (Main.player[k].active && !Main.player[k].dead && k != Main.myPlayer && player.team == Main.player[k].team)
					{
						Vector2 mouse = new Vector2(PlayerInput.MouseX, PlayerInput.MouseY);
						float num2 = DistanceUtils.ToTilePosition(Main.player[k].position.X + Main.player[k].width / 2) * Main.mapFullscreenScale;
						float num7 = DistanceUtils.ToTilePosition(Main.player[k].position.Y + Main.player[k].gfxOffY + Main.player[k].height / 2) * Main.mapFullscreenScale;
						num2 += -Main.mapFullscreenPos.X * Main.mapFullscreenScale + Main.screenWidth / 2 - 6f;
						float num8 = num7 + (0f - Main.mapFullscreenPos.Y * Main.mapFullscreenScale + Main.screenHeight / 2 - 4f - Main.mapFullscreenScale / 5f * 2f);
						float num3 = num2 + 4f - 14f * Main.UIScale;
						float num4 = num8 + 2f - 14f * Main.UIScale;
						float num5 = num3 + 28f * Main.UIScale;
						float num6 = num4 + 28f * Main.UIScale;

						if (mouse.X >= num3 && mouse.X <= num5 && mouse.Y >= num4 && mouse.Y <= num6)
						{
							SoundEngine.PlaySound(SoundID.Item12, new Vector2(-1f, 120f));
							Main.mouseLeftRelease = false;
							Main.mapFullscreen = false;
							player.UnityTeleport(Main.player[k].position);
							break;
						}
					}
				}
			}
		}
		public static void ApplyBuffToAllPlayers(int BuffId, int Hours, int Minutes, int Seconds)
		{
			int BuffTime = (Seconds * 60) + (Minutes * 60 * 60) + (Hours * 60 * 60 * 60);
			int MaxPlayer = RemnantOfTheAncientsMod.MaxPlayerOnline();
			for (int CurrentPlayer = 0; CurrentPlayer <= MaxPlayer; CurrentPlayer++)
			{
				Main.player[CurrentPlayer].AddBuff(BuffId, BuffTime);
			}
		}
		public static void ClearBuffToAllPlayers(int BuffId)
		{
			int MaxPlayer = RemnantOfTheAncientsMod.MaxPlayerOnline();
			for (int CurrentPlayer = 0; CurrentPlayer <= MaxPlayer; CurrentPlayer++)
			{
				Main.player[CurrentPlayer].ClearBuff(BuffId);
			}
		}


		public override void DrawEffects(PlayerDrawSet drawInfo, ref float r, ref float g, ref float b, ref float a, ref bool fullBright)
		{
			if (drawInfo.shadow == 0f && Main.rand.NextBool(4))
			{
				if (Burn_Sand)
				{
					SpawnDust(DustType<QuemaduraA>());
				}
				if (Hell_Fire)
				{
					SpawnDust(DustType<Hell_Fire_P>());
				}
				if (hBurn)
				{
					SpawnDust(DustType<HollyBurn_P>());
				}
			}

			void SpawnDust(int id)
			{
				int dust = Dust.NewDust(drawInfo.Position - new Vector2(2f, 2f), Player.width + 4, Player.height + 4, id, Player.velocity.X * 0.4f, Player.velocity.Y * 0.4f, 100, default, 3f);
				Main.dust[dust].noGravity = true;
				Main.dust[dust].velocity *= 1.8f;
				Main.dust[dust].velocity.Y -= 0.5f;
				drawInfo.DustCache.Add(dust);
			}
		}


		public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
		{
			//item.GetGlobalItem<FchangesItem>().ReaperSize(item);
			if (hit.DamageType == DamageClass.Melee)// item.noMelee && !item.noUseGraphic)
			{
				foreach (int b in MeleeBuffInflict)
				{
					target.AddBuff(b, 300);
				}
			}
		}
		public override void OnHitNPCWithProj(Projectile proj, NPC target, NPC.HitInfo hit, int damageDone) //This is the same as the one in OnHitNPC, but for melee projectiles.
		{
			if (proj.CountsAsClass(DamageClass.Melee) || proj.CountsAsClass(DamageClass.SummonMeleeSpeed))
			{
				foreach (int b in MeleeBuffInflict)
				{
					target.AddBuff(b, 300);
				}
			}
			if (proj.CountsAsClass(DamageClass.Magic))
			{
				foreach (int b in MageBuffInflict)
				{
					target.AddBuff(b, 300);
				}
			}
			if (proj.CountsAsClass(DamageClass.Ranged))
			{
				foreach (int b in RangerBuffInflict)
				{
					target.AddBuff(b, 300);
				}
			}
			if (proj.CountsAsClass(DamageClass.Summon))
			{
				foreach (int b in MinionsBuffInflict)
				{
					target.AddBuff(b, 300);
				}
			}
			if (proj.CountsAsClass(DamageClass.Throwing))
			{
				foreach (int b in TrowerBuffInflict)
				{
					target.AddBuff(b, 300);
				}
			}
			foreach (int b in AllClassBuffInflict)
			{
				target.AddBuff(b, 300);
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
			ScrollsBuff.Clear();

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
			AddScrollBuff();
			foreach (int Scrollbuff in ScrollsBuff)
			{
				Player.buffImmune[Scrollbuff] = true;
			}
			Player.buffImmune[buff] = false;
		}
		public bool PlayerHaveScroll()
		{
			AddScrollBuff();
			try
			{
				foreach (int Scrollbuff in ScrollsBuff)
				{
					if (Player.HasBuff(Scrollbuff))
					{
						return true;
					}
				}
                return false;
            }
			catch
			{
                return false;
            }
		
		}
		public int SearchCurrenScrollEffect()
		{
			foreach (int Scrollbuff in ScrollsBuff)
			{
				if (Player.HasBuff(Scrollbuff))
					return Scrollbuff;
			}
			return -1;
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
		public void FireInmune()
		{
			Player.buffImmune[BuffID.OnFire] = true;
			Player.buffImmune[BuffID.Frostburn] = true;
			Player.buffImmune[BuffID.CursedInferno] = true;
			Player.buffImmune[BuffID.Burning] = true;
			Player.buffImmune[BuffID.ShadowFlame] = true;
		}

		public void SpawnMinionItem(Player player)
		{
			if (InterceptionDrone) SpawnHelper(player.Center, Vector2.Zero, ProjectileType<InterceptionDrone>(), 1, 0, Main.myPlayer);
			if (HealingDrone) SpawnHelper(player.Center, new Vector2(7f, 2f), ProjectileType<InterceptionDrone>(), 1, 0, Main.myPlayer);

		}
		public void SpawnHelper(Vector2 center, Vector2 velocity, int type, int damage, int knockback, int owner, float Ai0 = 0, float Ai1 = 0)
		{
			if (Player.whoAmI == Main.myPlayer && Player.ownedProjectileCounts[type] < 1 && Player.whoAmI == Main.myPlayer)
			{
				var projectile = Projectile.NewProjectileDirect(Player.GetSource_FromAI(), center, velocity, type, damage, knockback, owner, (float)Ai0, (float)Ai1);
				projectile.originalDamage = damage;
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

		public void KillMinion(int proj) => Main.projectile[proj].Kill();

		public override void OnHitByNPC(NPC npc, Player.HurtInfo hurtInfo)
		{
			if (DesertHeraldSetBonus)
			{
				Projectile.NewProjectile(Projectile.GetSource_None(), npc.position, Vector2.Zero, ProjectileID.SandnadoFriendly, 30, 0, Main.myPlayer);
			}
			base.OnHitByNPC(npc, hurtInfo);
		}

		public override void OnHitByProjectile(Projectile proj, Player.HurtInfo hurtInfo)
		{
			if (DesertHeraldSetBonus)
			{
				Projectile.NewProjectile(Projectile.GetSource_None(), Main.npc[proj.owner].position, Vector2.Zero, ProjectileID.SandnadoFriendly, 30, 0, Main.myPlayer);
			}
			base.OnHitByProjectile(proj, hurtInfo);
		}

		public static int lastChest = -1;
		public void SpawnMimics(Player player)
		{
			if (Main.netMode != NetmodeID.MultiplayerClient)
			{
				if (player.chest == -1 && lastChest >= 0 && Main.chest[lastChest] != null)
				{
					int x = Main.chest[lastChest].x;
					int y2 = Main.chest[lastChest].y;
					RemnantGlobalNPC.BigMimicSummonCheck(x, y2, player);
				}
				if (lastChest != player.chest && player.chest >= 0 && Main.chest[player.chest] != null)
				{
					int x2 = Main.chest[player.chest].x;
					int y3 = Main.chest[player.chest].y;
					Projectile.GasTrapCheck(x2, y3, player);
					ItemSlot.forceClearGlowsOnChest = true;
				}
				lastChest = player.chest;
			}
		}
		static Projectile proj;
		public void SpawnProjectileOnMouse(int id)
		{
			Vector2 MousePosition = new Vector2(Player.tileTargetX * 16, (Player.tileTargetY - 1) * 16);
			int CountOfProj = Player.ownedProjectileCounts[id];

			if (CountOfProj <= 0)
			{
				proj = Projectile.NewProjectileDirect(Projectile.GetSource_None(), MousePosition, Vector2.Zero, ProjectileID.RollingCactus, 10, 0, Player.whoAmI);
				proj.friendly = true;
				proj.hostile = false;
				proj.tileCollide = false;
			}
			proj.position = MousePosition;
		}
        public Projectile SpawnProjectileOnMouse(int id, Projectile p)
        {
            Vector2 MousePosition = new Vector2(Player.tileTargetX * 16, (Player.tileTargetY - 1) * 16);
            int CountOfProj = Player.ownedProjectileCounts[id];

            if (CountOfProj <= 0)
            {
                p = Projectile.NewProjectileDirect(Projectile.GetSource_None(), MousePosition, Vector2.Zero, ProjectileID.RollingCactus, 10, 0, Player.whoAmI);
                p.friendly = true;
                p.hostile = false;
                p.tileCollide = false;
				p.timeLeft = 50;
            }
			if (p != null)
			{
				p.position = MousePosition;
			}
			return p;
        }
    }
    public class RemnantKeybindPlayer : ModPlayer
    {
        public override void ProcessTriggers(TriggersSet triggersSet)
        {
            if (KeybindSystem.TogleReaperInterface.JustPressed)
            {
				ReaperSoulsUISystem ReaperUI = GetInstance<ReaperSoulsUISystem>();

				if (ReaperUI.IsVisible()) ReaperUI.HideMyUI();             
				else ReaperUI.ShowMyUI();	
            }
			if (RemnantOfTheAncientsMod.FargosSoulMod != null)
			{
				if (FargosKeybindSystem.TogleFrostBarrier.JustPressed && !Player.HasBuff<FrostBarrierCouldown>() && Player.ownedProjectileCounts[ModContent.ProjectileType<FrostBarrier>()] == 0)
				{
					Projectile.NewProjectile(Projectile.GetSource_None(), Player.position, Vector2.Zero, ModContent.ProjectileType<FrostBarrier>(), 0, 0, Player.whoAmI);
				}
				if (FargosKeybindSystem.ToggNightTp.JustPressed)
				{
					if (Player.GetModPlayer<RemnantFargosSoulsPlayer>().NightTp)
					{
						if (RemnantFargosSoulsPlayer.NightTpCouldown == 0)
						{
							int x = Player.tileTargetX * 16;
							int y = Player.tileTargetY * 16;
							Vector2 pos = new Vector2(x, y);
							for (int i = 0; i < 55; i++)
							{
								Dust.NewDustDirect(Player.position * 16, 10, 10, DustID.Corruption);
							}

							Player.position = pos;

							for (int i = 0; i < 55; i++)
							{
								Dust.NewDustDirect(pos, 10, 10, DustID.Corruption);
							}
                            RemnantFargosSoulsPlayer.NightTpCouldown = RemnantFargosSoulsPlayer.NightTpCouldownMax;
						}
					}
				}
            }
        }
    }
}

		
 	