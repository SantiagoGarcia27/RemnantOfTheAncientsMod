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
using System.Linq;

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
		public bool BrainDogde;

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
			BrainDogde = false;

			//tuxoniteStealthCounter = 1;
		}

		public override void Load()
		{
            On_Player.TryGettingDevArmor += Player_TryGettingDevArmor;
			base.Load();
		}

		private void Player_TryGettingDevArmor(On_Player.orig_TryGettingDevArmor orig, Player player, IEntitySource source)
		{
			//TryGettingPatreonOrDevArmor(source, this);
			if (Main.rand.NextBool(Main.tenthAnniversaryWorld ? 10 : 20))
			{
				switch (Main.rand.Next(20))
				{
					case 0:
						SpawnSuit(player, source, new List<int>() { 666, 667, 668, 665 });
						break;
					case 1:
						SpawnSuit(player, source, new List<int>() { 1554, 1555, 1556, 1586 });
						break;
					case 2:
						SpawnSuit(player, source, new List<int>() { 1554, 1587, 1588, 1586 });
						break;
					case 3:
						SpawnSuit(player, source, new List<int>() { 1557, 1558, 1559, 1585 });
						break;
					case 4:
						SpawnSuit(player, source, new List<int>() { 1560, 1561, 1562, 1584 });
						break;
					case 5:
						SpawnSuit(player, source, new List<int>() { 1563, 1564, 1565, 3582 });
						break;
					case 6:
                        SpawnSuit(player, source, new List<int>() { 1566, 1567, 1568});
						break;
					case 7:
						SpawnSuit(player, source, new List<int>() { 1580, 1581, 1582, 1583 });
						break;
					case 8:
						SpawnSuit(player, source, new List<int>() { 3226, 3227, 3228, 3288 });
						break;
					case 9:
						SpawnSuit(player, source, new List<int>() { 3583, 3581, 3578, 3579 });
						break;
					case 10:
						SpawnSuit(player, source, new List<int>() { 3585, 3586, 3587, 3588 });
						break;
					case 11:
						SpawnSuit(player, source, new List<int>() { 3589, 3590, 3591, 3592 });
						break;
					case 12:
						SpawnSuit(player, source, new List<int>() { 3368, 3921, 3922, 3923 });
						break;
					case 13:
						SpawnSuit(player, source, new List<int>() { 3925, 3926, 3927, 3928 });
						break;
					case 14:
						SpawnSuit(player, source, new List<int>() { 4732, 4733, 4734, 4730 });
						break;
					case 15:
						SpawnSuit(player, source, new List<int>() { 4747, 4748, 4749, 4746 });
						break;
					case 16:
                        SpawnSuit(player, source, new List<int>() { 4751, 4752, 4753, 4750 });
                        break;
					case 17:	
						SpawnSuit(player, source, new List<int>() { 4755, 4756, 4757, 4754 });
						break;
					case 18:
                        SpawnSuit(player, source, new List<int>() { ItemType<Sangar_Head>(), ItemType<Sangar_Body>(), ItemType<Sangar_Legs>() });
                        break;
					case 19:
						SpawnSuit(player, source, new List<int>() { ItemType<Ttim_Head>(), ItemType<Ttim_Body>(), ItemType<Ttim_Legs>() });
						break;
				}
			}
		}
		public void SpawnSuit(Player player,IEntitySource source, List<int> suitParts) 
		{ 
			for(int a = 0; a < suitParts.Count; a++)
			{
				player.QuickSpawnItem(source, suitParts[a]);
            }
		}

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

			if (Utils1.IsItemOnPlayerInventory(ModContent.ItemType<EndlessWormHole>())) 
			{ 
				WormHoleEffect(Player);
			}
		}
        public override bool FreeDodge(Player.HurtInfo info)
        {
			if(BrainDogde)
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
        public override void ModifyHurt(ref Player.HurtModifiers modifiers)
        {
            base.ModifyHurt(ref modifiers);
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
                        float num2 = DistanceUtils.ToTilePosition(Main.player[k].position.X + Main.player[k].width / 2) * mapFullscreenScale;
                        float num7 = DistanceUtils.ToTilePosition(Main.player[k].position.Y + Main.player[k].gfxOffY + Main.player[k].height / 2) * mapFullscreenScale;
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

		
 	