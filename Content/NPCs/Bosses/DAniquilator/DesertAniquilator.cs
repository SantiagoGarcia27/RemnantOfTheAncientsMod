using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using RemnantOfTheAncientsMod.Content.Items.Items;
using static Terraria.ModLoader.ModContent;
using Microsoft.Xna.Framework;
using Terraria.GameContent.ItemDropRules;
using System;
using RemnantOfTheAncientsMod.Content.Items.Weapons.Melee;
using RemnantOfTheAncientsMod.Content.Items.Weapons.Magic;
using RemnantOfTheAncientsMod.Content.Items.Weapons.Summon;
using RemnantOfTheAncientsMod.Content.Items.Consumables.tresure_bag;
using RemnantOfTheAncientsMod.Content.Items.Weapons.Ranger.Bows;
using RemnantOfTheAncientsMod.Content.Items.Placeables.Relics;
using RemnantOfTheAncientsMod.Common.Systems;
using Terraria.Localization;
using RemnantOfTheAncientsMod.World;
using RemnantOfTheAncientsMod.Content.Projectiles.BossProjectile;
using RemnantOfTheAncientsMod.Content.Buffs.Debuff;
using RemnantOfTheAncientsMod.Content.Items.Placeables.Trophy;
using System.IO;
using CalamityMod.World;
using RemnantOfTheAncientsMod.Common.Global;
using RemnantOfTheAncientsMod.Content.Items.Armor.Masks;
using RemnantOfTheAncientsMod.Content.Items.Weapons.Melee.saber;
using RemnantOfTheAncientsMod.Content.Items.Weapons.Ranger.Rep;
using RemnantOfTheAncientsMod.Common.Drops.DropRules;
using RemnantOfTheAncientsMod.Content.Items.Consumables.ReaperSouls;

namespace RemnantOfTheAncientsMod.Content.NPCs.Bosses.DAniquilator
{
    [AutoloadBossHead]
    public class DesertAniquilator : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Desert Annihilator");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Aniquilador del desierto");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Aniquilateur du désert");
            Main.npcFrameCount[NPC.type] = Main.npcFrameCount[NPCID.BlueSlime];
            NPCID.Sets.MPAllowedEnemies[Type] = true;
            NPCID.Sets.BossBestiaryPriority.Add(Type);
        }
        public override void SetDefaults()
        {
            NPC.aiStyle = NPCID.BlueSlime;
            NPC.lifeMax = 3500;// (int)NpcChanges1.ExpertLifeScale(3500);
            NPC.damage = 30;// (int)NpcChanges1.ExpertDamageScale(30);
            NPC.defense = 10;
            NPC.knockBackResist = 0f;
            NPC.width = 100;
            NPC.height = 100;
            NPC.value = Item.buyPrice(0, 2, 75, 45);
            NPC.npcSlots = 30f;
            NPC.boss = true;
            NPC.lavaImmune = true;
            NPC.noGravity = false;
            NPC.noTileCollide = false;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.buffImmune[24] = true;
            Music = MusicLoader.GetMusicSlot(Mod, "Sounds/Music/Desert_Aniquilator");
            NPC.netAlways = true;
            AnimationType = NPCID.BlueSlime;
            NPC.stepSpeed = 8f;
        }

        private int attackCounter;
        private int summonCounter;
        private int tpDirection;
        private int currentPhase;
        int tpParticleTimer = Utils1.FormatTime(0,0,0,5);
        public override void SendExtraAI(BinaryWriter writer)
        {
            writer.Write(attackCounter);
            writer.Write(summonCounter);
        }
        public override void ReceiveExtraAI(BinaryReader reader)
        {
            attackCounter = reader.ReadInt32();
            summonCounter = reader.ReadInt32();
        } 
        public override void AI()
        {
            Player player = Main.player[NPC.target];

            NPC.ai[0] = 10;
            NPC.ai[1] = (NPC.ai[1] + 1) % 800;
            NPC.ai[2]++;

            if (NPC.Distance(player.Center) >= 100 * 16 && !player.dead)
            {
                GenerateTpParticles();
                DesertTp();
            }

            Point NpcFloor = Utils.ToTileCoordinates(NPC.Center);
            Point PlayerFloor = Utils.ToTileCoordinates(Main.player[NPC.target].Center);
            // Main.player[NPC.target].position = Utils.ToTileCoordinates(NPC.Center).ToVector2();//test teleport
            if (Main.tile[NpcFloor.X, NpcFloor.Y + 1].LiquidAmount > 0)
            {
                if (Main.tile[PlayerFloor.X, PlayerFloor.Y + 1].LiquidAmount == 0)
                {
                    GenerateTpParticles();
                    DesertTp();
                }
            }
            NPC.scale = RemnantOfTheAncientsMod.CalamityMod != null ? LifeSize(NPC, RemnantOfTheAncientsMod.CalamityMod) : LifeSize(NPC);
            if (player.dead || NPC.target < 0 || NPC.target == 255 || !player.active)
            {
                NPC.TargetClosest(true);
            }
            setCurrentPhase(NPC);
            AttackIA(player);
            if (player.dead)
            {
                NPC.EncourageDespawn(7);
                DespawnBoss();
            }
        }
        private void AttackIA(Player target)
        {
            bool IsRage = !target.ZoneDesert; 
            if (attackCounter >= 0)
            {
                setAttackCounter(1000,target);
            }

            if (!Reaper.ReaperMode)
            {
                switch (summonCounter)
                {
                    case 600:
                    case 400:
                        if (!IsRage)
                        {
                            NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, NPCType<DSlime>());
                        }
                        else
                        {
                            for (int i = 0; i >= 4; i++) NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, NPCType<DSlime>());
                        }
                        break;
                    case 200:
                        if (Main.expertMode)
                        {
                            if (!IsRage)
                            {
                                NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)Main.worldSurface + (3 * 16), NPCID.TombCrawlerHead);
                            }
                            else
                            {
                                for (int i = 0; i >= 4; i++)
                                {
                                    NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)Main.worldSurface + (3 * 16), NPCID.TombCrawlerHead);
                                }
                            }
                        }
                
                        break;
                    case 100:
                        NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, NPCType<FlyingAntlionD>());
                        NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, NPCType<WalkingAntlionD>());
                        break;
                    case 10:
                        if(currentPhase >= 2)
                        {
                            NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)Main.worldSurface + (3 * 16), NPCID.DuneSplicerHead);
                            if (Main.expertMode)
                            {
                                NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, NPCID.DesertScorpionWalk);
                            }
                        }
                        break;
                }
                if (Main.expertMode)
                {
                    switch (attackCounter)
                    {
                        case 600:
                            ShootIa((int)NpcChanges1.ExpertDamageScale(30), ProjectileType<DesertTyphoon>(), target, 12f, 0.5f, 0.5f);
                            break;
                        case 590:
                            ShootIa((int)NpcChanges1.ExpertDamageScale(30), ProjectileType<DesertTyphoon>(), target, 12f, 0.5f, -0.5f);
                            break;
                        case 580:
                            ShootIa((int)NpcChanges1.ExpertDamageScale(30), ProjectileType<DesertTyphoon>(), target, 12f, -0.5f, -0.5f);
                            break;
                        case 530:
                            if (IsRage)
                            {
                                if (!Reaper.ReaperMode)
                                {
                                    for (int i = 0; i <= 7; i++)
                                    {
                                        ShootIa((int)NpcChanges1.ExpertDamageScale(30), ProjectileType<DesertTyphoon>(), target, 12f + i, -0.5f + (i), 0.5f);
                                    }
                                }
                                else
                                {
                                    for (float i = 0f; i <= 6; i += 0.5f)
                                    {
                                        ShootIa((int)NpcChanges1.ExpertDamageScale(30), ProjectileType<DesertTyphoon>(), target, 12f, -3.5f + i, 3.5f - i);
                                    }
                                }
                            }
                            else
                            {
                                if (!Reaper.ReaperMode)
                                {
                                    ShootIa((int)NpcChanges1.ExpertDamageScale(30), ProjectileType<DesertTyphoon>(), target, 12f, -0.5f, 0.5f);
                                }
                                else
                                {
                                    for(float i = 0f; i <= 4; i += 0.5f) 
                                    {
                                        ShootIa((int)NpcChanges1.ExpertDamageScale(30), ProjectileType<DesertTyphoon>(), target, 12f, -3.5f + i, 3.5f - i);
                                    }
                                }
                            }
                            break;
                        case 500:

                            GenerateTpParticles();
                            DesertTp();
                            break;
                    }
                    if(attackCounter < 560 && attackCounter > 500)
                    {
                        GenerateTpParticles();
                    }
                }
            }
            else
            {
                switch (summonCounter)
                {
                    case 500:
                    case 400:
                        NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, NPCType<DSlime>());
                        break;
                    case 200:
                        if (Main.expertMode) NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)Main.worldSurface - (3 * 16), NPCID.TombCrawlerHead);
                        break;
                    case 100:
                        NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, NPCType<FlyingAntlionD>());
                        NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, NPCType<WalkingAntlionD>());
                        break;
                    case 10:
                        if (currentPhase >= 2)
                        {
                            NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, NPCID.DuneSplicerHead);
                            if (Main.expertMode)
                            {
                                NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, NPCID.DesertScorpionWalk);
                            }
                        }
                        break;
                }        
                if (Main.expertMode)
                {
                    switch (attackCounter)
                    {
                        case 500:
                            ShootIa((int)NpcChanges1.ExpertDamageScale(30), ProjectileType<DesertTyphoon>(), target, 12f, 0.5f, 0.5f);
                            break;
                        case 490:
                            ShootIa((int)NpcChanges1.ExpertDamageScale(30), ProjectileType<DesertTyphoon>(), target, 12f, 0.5f, -0.5f);
                            break;
                        case 480:
                            ShootIa((int)NpcChanges1.ExpertDamageScale(30), ProjectileType<DesertTyphoon>(), target, 12f, -0.5f, -0.5f);
                            break;
                        case 430:
                            ShootIa((int)NpcChanges1.ExpertDamageScale(30), ProjectileType<DesertTyphoon>(), target, 12f, -0.5f, 0.5f);
                            ShootIa((int)NpcChanges1.ExpertDamageScale(30), ProjectileType<DesertTyphoon>(), target, 12f, 3.5f, 3.5f);
                            ShootIa((int)NpcChanges1.ExpertDamageScale(30), ProjectileType<DesertTyphoon>(), target, 12f, 3.5f, -3.5f);
                            ShootIa((int)NpcChanges1.ExpertDamageScale(30), ProjectileType<DesertTyphoon>(), target, 12f, -3.5f, -3.5f);
                            ShootIa((int)NpcChanges1.ExpertDamageScale(30), ProjectileType<DesertTyphoon>(), target, 12f, -3.5f, 3.5f);
                            break;
                        case 400:
                            GenerateTpParticles();
                            DesertTp();
                            break;
                    }
                }
            }
        }
        public override void HitEffect(int hitDirection, double damage)
        {
            if (NPC.life <= 0)
            {
                if (Main.netMode != NetmodeID.Server)
                {
                    Gore.NewGore(NPC.GetSource_Death(), NPC.position, new Vector2(Main.rand.Next(-6, 7), Main.rand.Next(-6, 7)), Mod.Find<ModGore>("DesertAniquilatorGore").Type, NPC.scale);         
                }
                for (int j = 0; j <new RemnantOfTheAncientsMod().ParticleMeter(1000); j++)
                {
                    Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.Sandstorm, hitDirection, -1f);
                }
            }
        }
        public void setAttackCounter(int i, Player player)
        {
            int masterAttackCounterScale = AttackCounterScale(i - 400, player);
            int expertAttackCounterScale = AttackCounterScale(i - 300, player);
            int normalAttackCounterScale = AttackCounterScale(i - 200, player);

            if (summonCounter > 0) summonCounter--;
            if (attackCounter > 0) attackCounter--;

            if (summonCounter <= 0)
            {
                summonCounter = Main.masterMode ? masterAttackCounterScale : Main.expertMode ? expertAttackCounterScale : normalAttackCounterScale;
                NPC.netUpdate = true;
            }

            if (attackCounter <= 0)
            {
                attackCounter = Main.masterMode ? masterAttackCounterScale : Main.expertMode ? expertAttackCounterScale : normalAttackCounterScale;
                NPC.netUpdate = true;
            }
        }
        int blockIncrement = 0;
        public Vector2 GetSecurePosition(Vector2 pos)
        {
            Vector2 newPos;

            if (!CoordHasTile(pos))
            {
                return pos;
            }
            else
            {   
                do
                {
                    blockIncrement++;
                    newPos = new(pos.X, pos.Y - blockIncrement * 16);
                } while (CoordHasTile(newPos));
                blockIncrement = 0;
                return newPos;
            }
        }
        public bool CoordHasTile(Vector2 pos)
        {
            if (Collision.SolidCollision(pos, 6 * 16, 6 * 16))
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        public void setCurrentPhase(NPC NPC)
        {
            if (NPC.life <= NPC.lifeMax / 4) currentPhase = 3;
            else if (NPC.life <= NPC.lifeMax / 2) currentPhase = 2;
            else currentPhase = 1;
        }
        public int AttackCounterScale(int Num, Player player) 
        {
            return (!Reaper.ReaperMode) ? Num : Num - 100;
        }
        public void DesertTp()
        {
            
            tpDirection = new Random().Next(1, 3);

            switch (tpDirection)
            {
                case 1:
                    NPC.Center = GetSecurePosition(Main.player[NPC.target].Center + new Vector2(30 * 16,0));
                    break;
                case 2:
                    NPC.Center = GetSecurePosition(Main.player[NPC.target].Center + new Vector2(0, -20 * 16));
                    break;
                case 3:
                    NPC.Center = GetSecurePosition(Main.player[NPC.target].Center + new Vector2(-30 * 16, 0));
                    break;

            }
            tpParticleTimer = Utils1.FormatTime(0, 0, 0, 5);
        }
        public void GenerateTpParticles()
        {
            do
            {
                tpParticleTimer--;
                for (int i = 0; i < 25; i++)
                {

                    Vector2 dustPosition = NPC.position + new Vector2(Main.rand.Next(NPC.width), Main.rand.Next(NPC.height));
                    Dust dust = Dust.NewDustDirect(dustPosition, NPC.width, NPC.height, DustID.Sand,0,0,100,default,3f);
                    dust.velocity = NPC.velocity * 0.2f; 
                    dust.noGravity = true; 
                }
            } while (tpParticleTimer > 0);

        }
        private float LifeSize(NPC npc)
        {
            float porcentage = Utils1.GetPorcentage(npc.life, npc.lifeMax);


            if (Reaper.ReaperMode) return applyLifeSize(porcentage, 1.5f);
            else if (Main.masterMode) return applyLifeSize(porcentage, 1.35f);
            else if (Main.expertMode) return applyLifeSize(porcentage, 1.3f);
            else return applyLifeSize(porcentage, 1.25f);

            //else if (porcentage < 100 && porcentage >= 85) return 1.2f;
            //else if (porcentage < 85 && porcentage >= 55) return 1.15f;
            //else if (porcentage < 55 && porcentage >= 40) return 1.1f;
            //else if (porcentage < 40 && porcentage >= 30) return 1.0f;
            //else if (porcentage < 30 && porcentage >= 20) return 0.8f;
            //else if (porcentage < 20 && porcentage >= 10) return 0.7f;
            //else if (porcentage < 10 && porcentage >= 5) return 0.6f;
            //else if (porcentage < 5) return 0.5f;
            //return 1f;
        }
        [JITWhenModsEnabled("CalamityMod")]
        private float LifeSize(NPC npc, Mod mod)
        {
            float porcentage = Utils1.GetPorcentage(npc.life, npc.lifeMax);
            if (CalamityWorld.death) return applyLifeSize(porcentage, 2f);
            else if (CalamityWorld.revenge || Reaper.ReaperMode) return applyLifeSize(porcentage, 1.5f);
            else if (Main.masterMode) return applyLifeSize(porcentage, 1.35f);
            else if (Main.expertMode) return applyLifeSize(porcentage, 1.3f);
            else return applyLifeSize(porcentage, 1.25f);
        }

        private float applyLifeSize(float porcentage, float MaxValue)
        {
           
            for (int i = 100; i >= 0; i--)
            {
                if ((int)porcentage == i)
                {
                    float j = Utils1.GetValueFromPorcentage(MaxValue, i);
                    if(j > Utils1.GetValueFromPorcentage(MaxValue, 30))
                    {
                        return j;
                    }
                }

            }
            return Utils1.GetValueFromPorcentage(MaxValue, 30);
        }
            //if(porcentage == 100) return Utils1.GetValueFromPorcentage(MaxValue,100);
            //else if (porcentage == 99) return Utils1.GetValueFromPorcentage(MaxValue, 99);
            //else if (porcentage < 85 && porcentage >= 55) return 1.15f;
            //else if (porcentage < 55 && porcentage >= 40) return 1.1f;
            //else if (porcentage < 40 && porcentage >= 30) return 1.0f;
            //else if (porcentage < 30 && porcentage >= 20) return 0.8f;
            //else if (porcentage < 20 && porcentage >= 10) return 0.7f;
            //else if (porcentage < 10 && porcentage >= 5) return 0.6f;
            //else if (porcentage < 5) return 0.5f;
           // return 1f;
        
        
        public void ShootIa(int dammage, int type, Player player, float Speed, double x, double y)
        {
            Vector2 NpcPosition = new Vector2(NPC.position.X + (NPC.width / 2), NPC.position.Y + (NPC.height / 2));
            float rotation = (float)Math.Atan2(NpcPosition.Y - (player.position.Y + (player.height * x)), NpcPosition.X - (player.position.X + (player.width * y)));
            Vector2 direction;
            direction.X = (float)(Math.Cos(rotation) * Speed * -1);
            direction.Y = (float)(Math.Sin(rotation) * Speed * -1);
            Projectile.NewProjectile(NPC.GetSource_FromAI(), NpcPosition, direction, type, dammage, 0f, Main.myPlayer);
        }
        public void DespawnBoss()
        {
            NPC.velocity.X -= 3.09f;
            NPC.velocity.Y -= 3f;

            NPC.EncourageDespawn(7);
            return;
        }
        public override bool? CanFallThroughPlatforms() => false;
            
        public override void OnHitPlayer(Player player, int damage, bool crit) 
        {
        if (Main.rand.NextBool(3)) player.AddBuff(BuffType<Burning_Sand>(), 100, true);	
		}
        public override void BossLoot(ref string name, ref int potionType)
        {
            RemnantDownedBossSystem.downedDesert = true;
            potionType = ItemID.HealingPotion;  
            Item.NewItem(NPC.GetSource_Loot(),(int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, ItemID.SandBlock, 60);
            Item.NewItem(NPC.GetSource_Loot(),(int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, ItemType<Sand_escense>(), 10);
        }
        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.NormalvsExpertOneFromOptions(1, 999999999, new[]
            {
                ItemType<desertbow>(),
                ItemType<DesertEdge>(),
                ItemType<DesertStaff>(),
                ItemType<DesertTome>()
            }));
            npcLoot.Add(ItemDropRule.Common(ItemType<Sand_escense>(), 1,5,20));
            npcLoot.Add(ItemDropRule.Common(ItemID.SandBlock, 1,1,50));
            npcLoot.Add(ItemDropRule.NormalvsExpert(ItemID.Amber, 6, 1));
            npcLoot.Add(ItemDropRule.ByCondition(new IsHardModeRule(), ItemID.AncientBattleArmorMaterial, 5, 1,1, Utils1.ReaperDropScaler(1)));

            npcLoot.Add(ItemDropRule.Common(ItemType<DesertAMask>(), 7));
            npcLoot.Add(ItemDropRule.Common(ItemType<DesertTrophy>(), 10));

            npcLoot.Add(ItemDropRule.BossBag(ItemType<desertBag>()));
            npcLoot.Add(ItemDropRule.MasterModeCommonDrop(ItemType<Desert_Relic>()));
           
        }	
    }
}