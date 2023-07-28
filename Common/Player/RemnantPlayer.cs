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
using Terraria.Chat;
using Terraria.Localization;
using RemnantOfTheAncientsMod.Content.Projectiles.Multiclass;
using CalamityMod;
using RemnantOfTheAncientsMod.Content.Items.Armor.Cosmetic;
using Terraria.Audio;
using Terraria.GameInput;
using RemnantOfTheAncientsMod.Common.UtilsTweaks;
using RemnantOfTheAncientsMod.Content.Items.Consumables.Pociones;

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
		public bool MoneyCollector;	
		public List<int> ScrollsBuff = new List<int>();
		public static int DummyMode = 0;
		public bool CouwldownHolySaber;
		public static bool tuxoniteStealth;
		public static int tuxoniteStealthDuration = 0;
		public static float tuxoniteStealthCounter = 0;
		public static bool DaylightArmorSetBonus;
		public static bool CanWormHole;
		public bool HealingDrone;
		public bool InterceptionDrone;
		public bool DesertHeraldSetBonus;

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
			hasInfernal_core = false;
			SandWeapons = false;		
			MeleeKit = false;
			MoneyCollector = false;
			SunflowerSentry = false;
			tuxoniteStealth = false;
			DaylightArmorSetBonus = false;
			HealingDrone = false;
			InterceptionDrone = false;	
			DesertHeraldSetBonus = false;
			CanWormHole = false;
            //tuxoniteStealthCounter = 1;
        }

        public override void Load()
        {
           // On.Terraria.Player.TryGettingDevArmor += Player_TryGettingDevArmor;		
            base.Load();
        }

	//	private void Player_TryGettingDevArmor(On.Terraria.Player.orig_TryGettingDevArmor orig, Player player, IEntitySource source)
		//{
		//	//TryGettingPatreonOrDevArmor(source, this);
		//	if (Main.rand.NextBool(Main.tenthAnniversaryWorld ? 10 : 20))
		//	{
		//		switch (Main.rand.Next(20))
		//		{
		//			case 0:
		//				player.QuickSpawnItem(source, 666);
		//				player.QuickSpawnItem(source, 667);
		//				player.QuickSpawnItem(source, 668);
		//				player.QuickSpawnItem(source, 665);
		//				player.QuickSpawnItem(source, 3287);
		//				break;
		//			case 1:
		//				player.QuickSpawnItem(source, 1554);
		//				player.QuickSpawnItem(source, 1555);
		//				player.QuickSpawnItem(source, 1556);
		//				player.QuickSpawnItem(source, 1586);
		//				break;
		//			case 2:
		//				player.QuickSpawnItem(source, 1554);
		//				player.QuickSpawnItem(source, 1587);
		//				player.QuickSpawnItem(source, 1588);
		//				player.QuickSpawnItem(source, 1586);
		//				break;
		//			case 3:
		//				player.QuickSpawnItem(source, 1557);
		//				player.QuickSpawnItem(source, 1558);
		//				player.QuickSpawnItem(source, 1559);
		//				player.QuickSpawnItem(source, 1585);
		//				break;
		//			case 4:
		//				player.QuickSpawnItem(source, 1560);
		//				player.QuickSpawnItem(source, 1561);
		//				player.QuickSpawnItem(source, 1562);
		//				player.QuickSpawnItem(source, 1584);
		//				break;
		//			case 5:
		//				player.QuickSpawnItem(source, 1563);
		//				player.QuickSpawnItem(source, 1564);
		//				player.QuickSpawnItem(source, 1565);
		//				player.QuickSpawnItem(source, 3582);
		//				break;
		//			case 6:
		//				player.QuickSpawnItem(source, 1566);
		//				player.QuickSpawnItem(source, 1567);
		//				player.QuickSpawnItem(source, 1568);
		//				break;
		//			case 7:
		//				player.QuickSpawnItem(source, 1580);
		//				player.QuickSpawnItem(source, 1581);
		//				player.QuickSpawnItem(source, 1582);
		//				player.QuickSpawnItem(source, 1583);
		//				break;
		//			case 8:
		//				player.QuickSpawnItem(source, 3226);
		//				player.QuickSpawnItem(source, 3227);
		//				player.QuickSpawnItem(source, 3228);
		//				player.QuickSpawnItem(source, 3288);
		//				break;
		//			case 9:
		//				player.QuickSpawnItem(source, 3583);
		//				player.QuickSpawnItem(source, 3581);
		//				player.QuickSpawnItem(source, 3578);
		//				player.QuickSpawnItem(source, 3579);
		//				player.QuickSpawnItem(source, 3580);
		//				break;
		//			case 10:
		//				player.QuickSpawnItem(source, 3585);
		//				player.QuickSpawnItem(source, 3586);
		//				player.QuickSpawnItem(source, 3587);
		//				player.QuickSpawnItem(source, 3588);
		//				player.QuickSpawnItem(source, 3024, 4);
		//				break;
		//			case 11:
		//				player.QuickSpawnItem(source, 3589);
		//				player.QuickSpawnItem(source, 3590);
		//				player.QuickSpawnItem(source, 3591);
		//				player.QuickSpawnItem(source, 3592);
		//				player.QuickSpawnItem(source, 3599, 4);
		//				break;
		//			case 12:
		//				player.QuickSpawnItem(source, 3368);
		//				player.QuickSpawnItem(source, 3921);
		//				player.QuickSpawnItem(source, 3922);
		//				player.QuickSpawnItem(source, 3923);
		//				player.QuickSpawnItem(source, 3924);
		//				break;
		//			case 13:
		//				player.QuickSpawnItem(source, 3925);
		//				player.QuickSpawnItem(source, 3926);
		//				player.QuickSpawnItem(source, 3927);
		//				player.QuickSpawnItem(source, 3928);
		//				player.QuickSpawnItem(source, 3929);
		//				break;
		//			case 14:
		//				player.QuickSpawnItem(source, 4732);
		//				player.QuickSpawnItem(source, 4733);
		//				player.QuickSpawnItem(source, 4734);
		//				player.QuickSpawnItem(source, 4730);
		//				break;
		//			case 15:
		//				player.QuickSpawnItem(source, 4747);
		//				player.QuickSpawnItem(source, 4748);
		//				player.QuickSpawnItem(source, 4749);
		//				player.QuickSpawnItem(source, 4746);
		//				break;
		//			case 16:
		//				player.QuickSpawnItem(source, 4751);
		//				player.QuickSpawnItem(source, 4752);
		//				player.QuickSpawnItem(source, 4753);
		//				player.QuickSpawnItem(source, 4750);
		//				break;
		//			case 17:
		//				player.QuickSpawnItem(source, 4755);
		//				player.QuickSpawnItem(source, 4756);
		//				player.QuickSpawnItem(source, 4757);
		//				player.QuickSpawnItem(source, 4754);
		//				break;
		//			case 18:
		//				player.QuickSpawnItem(source, ItemType<Sangar_Head>());
		//				player.QuickSpawnItem(source, ItemType<Sangar_Body>());
		//				player.QuickSpawnItem(source, ItemType<Sangar_Legs>());
		//				break;
		//			case 19:
		//				player.QuickSpawnItem(source, ItemType<Ttim_Head>());
		//				player.QuickSpawnItem(source, ItemType<Ttim_Body>());
		//				player.QuickSpawnItem(source, ItemType<Ttim_Legs>());
		//				break;
		//		}
		//	}
		//}
       

        public override void UpdateDead()
		{
			Burn_Sand = false;
			Hell_Fire = false;
			hBurn = false;
			MeleeKit = false;
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
			checkInventory(Player);
			WormHoleEffect(Player);
        }

        public override void OnEnterWorld()
		{
			AddScrollBuff();
		//	if (ModLoader.TryGetMod("CalamityMod", out Mod CalamityMod) && RemnantOfTheAncientsMod.CalamityMod != null) CalamityMessage();
		}
		//[JITWhenModsEnabled("CalamityMod")] 
  //      public void CalamityMessage()
		//{
		//	if(ModLoader.TryGetMod("CalamityMod", out Mod calamityMod) && GetInstance<CalamityConfig>().RemoveReforgeRNG)
		//	{
  //              ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral(Language.GetTextValue("Mods.RemnantOfTheAncientsMod.ChatMessage.CalamityReforgeConfig", Language.GetTextValue("Mods.CalamityMod.Config.EntryTitle.RemoveReforgeRNG"))), Color.Red);
  //          }
		//}
		public static void WormHoleEffect(Player player)
		{
            if (Main.mapFullscreen && Main.netMode == NetmodeID.MultiplayerClient && Main.myPlayer == player.whoAmI && player.team > 0 && Main.mouseLeft && Main.mouseLeftRelease)
            {
                for (int k = 0; k < 255; k++)
                {
                    if (Main.player[k].active && !Main.player[k].dead && k != Main.myPlayer && player.team == Main.player[k].team)
                    {
                        float mapFullscreenScale = Main.mapFullscreenScale;
                        Vector2 mouse = new Vector2(PlayerInput.MouseX, PlayerInput.MouseY);
                        float num2 = DistanceHelper.ToTilePosition(Main.player[k].position.X + Main.player[k].width / 2) * mapFullscreenScale;
                        float num7 = DistanceHelper.ToTilePosition(Main.player[k].position.Y + Main.player[k].gfxOffY + Main.player[k].height / 2) * mapFullscreenScale;
                        num2 +=  -Main.mapFullscreenPos.X * mapFullscreenScale + Main.screenWidth / 2 - 6f;
                        float num8 = num7 + (0f - Main.mapFullscreenPos.Y * mapFullscreenScale + Main.screenHeight / 2 - 4f - mapFullscreenScale / 5f * 2f);
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
			int BuffTime = (Seconds *60) + (Minutes * 60 * 60)+ (Hours * 60 *60 *60);
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
      
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
		{
			//item.GetGlobalItem<FchangesItem>().ReaperSize(item);
			if (hit.DamageType == DamageClass.Melee)// item.noMelee && !item.noUseGraphic)
			{
				if (hasInfernal_core) target.AddBuff(BuffType<Hell_Fire>(), 300);
				if (SandWeapons) target.AddBuff(BuffType<Burning_Sand>(), 300);
				if (MeleeKit) target.AddBuff(BuffID.Ichor, 300);
			}
		}
        public override void OnHitNPCWithProj(Projectile proj, NPC target, NPC.HitInfo hit, int damageDone) //This is the same as the one in OnHitNPC, but for melee projectiles.
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
			if(DesertHeraldSetBonus)
			{
				Projectile.NewProjectile(Projectile.GetSource_None(), npc.position, Vector2.Zero, ProjectileID.SandnadoFriendly,30 , 0, Main.myPlayer);
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
	
	}
}

		
 	