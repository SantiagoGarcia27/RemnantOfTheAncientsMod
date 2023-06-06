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
using RemnantOfTheAncientsMod.Common.Global;
using RemnantOfTheAncientsMod.Content.Items.Armor.Masks;
using RemnantOfTheAncientsMod.Common.Drops.DropRules;
using System.Collections.Generic;

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
        private int tornadoCounter;
        private int summonCounter;
        private int tpDirection;
        private int currentPhase;
        private bool BossIsInRage = false;
        Point NpcFloor; 
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
           
            BossIsInRage = CheckRage(player);
            NPC.scale = LifeSize(NPC);
            float distance = NPC.Distance(player.Center);
            if (distance >= 110 * 16 && !player.dead && !Reaper.ReaperMode)
            {
                GenerateTpParticles();
                DesertTp();
            }

            NpcFloor = Utils.ToTileCoordinates(NPC.Center);
            Point PlayerFloor = Utils.ToTileCoordinates(Main.player[NPC.target].Center);
       
            if (Main.tile[NpcFloor.X, NpcFloor.Y + 1].LiquidAmount > 0)
            {
                if (Main.tile[PlayerFloor.X, PlayerFloor.Y + 1].LiquidAmount == 0)
                {
                    GenerateTpParticles();
                    DesertTp();
                }
            }
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
        public static bool CheckRage(Player player) => !player.dead && player.active && !player.ZoneDesert && !player.ZoneUndergroundDesert;
        private void AttackIA(Player target)
        {
            int mark = NPC.target;
            List<int[]> AttackValue = setAttackCounter();
            UpdateCounters(AttackValue);

            if (Reaper.ReaperMode)
            {
                for (int i = 1; i <= 2; i++)
                {
                    for (int j = 0; j < AttackValue[i].Length; j++)
                    {
                        AttackValue[i][j] -= Utils1.FormatTime(0, 0, 0, 2);
                    }
                }
            }
            if (BossIsInRage)
            {
                for (int i = 1; i <= 2; i++)
                {
                    for (int j = 0; j < AttackValue[i].Length; j++)
                    {
                        AttackValue[i][j] -= Utils1.FormatTime(0, 0, 0, 4);
                    }
                }
            }
            SummonAI(AttackValue);
            if (Main.expertMode)
            {
                ShootAI(AttackValue,target);
                TornadoAI(mark);
            }
            if (attackCounter == 500)
            {
                DesertTp();
            }
            if (attackCounter < 560)
            {
                if (attackCounter > 500)
                {
                    GenerateTpParticles();
                }
            }
        }
        public void ShootAI(List<int[]> AttackValue, Player target)
        {
            if (attackCounter == AttackValue[1][0])
            {
                ShootHelper((int)NpcChanges1.ExpertDamageScale(20), ProjectileType<DesertTyphoon>(), target, 12f, 0.5f, 0.5f);
            }
            if (attackCounter == AttackValue[1][1])
            {
                ShootHelper((int)NpcChanges1.ExpertDamageScale(20), ProjectileType<DesertTyphoon>(), target, 12f, 0.5f, -0.5f);
            }
            if (attackCounter == AttackValue[1][2])
            {
                ShootHelper((int)NpcChanges1.ExpertDamageScale(20), ProjectileType<DesertTyphoon>(), target, 12f, -0.5f, -0.5f);
            }
            if (attackCounter == AttackValue[1][3])
            {
                ShootHelper((int)NpcChanges1.ExpertDamageScale(20), ProjectileType<DesertTyphoon>(), target, 12f, -0.5f, -0.5f);
            }
            if (attackCounter == AttackValue[1][4])
            {
                ShootHelper((int)NpcChanges1.ExpertDamageScale(20), ProjectileType<DesertTyphoon>(), target, 12f, -0.5f, -0.5f);
            }
            if (attackCounter == AttackValue[1][5])
            {
                if (BossIsInRage)
                {
                    for (int i = 0; i <= 7; i++)
                    {
                        ShootHelper((int)NpcChanges1.ExpertDamageScale(20), ProjectileType<DesertTyphoon>(), target, 12f + i, -0.5f + i, 0.5f);
                    }  
                }
                else
                {
                    int n = Reaper.ReaperMode ? 2 : 0;

                    for (float i = 0f; i < n; i += 0.5f)
                    {
                        ShootHelper((int)NpcChanges1.ExpertDamageScale(20), ProjectileType<DesertTyphoon>(), target, 12f, -3.5f + i, 3.5f - i);
                    }
                }
            }
        }
        public void SummonAI(List<int[]> AttackValue)
        {
            if (summonCounter == AttackValue[2][0] || summonCounter == AttackValue[2][1])
            {
                int NumberOfNPCs = BossIsInRage ? 4 : 0;

                for (int i = 0; i <= NumberOfNPCs; i++)
                {
                    NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, NPCType<DSlime>());
                }
            }

            if (summonCounter == AttackValue[2][2] && Main.expertMode && Main.tile[NpcFloor.X, NpcFloor.Y + 1].TileType == TileID.Sand)
            {
                int NumberOfNPCs = BossIsInRage ? 4 : 0;

                for (int i = 0; i <= NumberOfNPCs; i++)
                {
                    NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)Main.worldSurface + (3 * 16), NPCID.TombCrawlerHead);
                }
            }
            if (summonCounter == AttackValue[2][3])
            {
                NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, NPCType<FlyingAntlionD>());
                NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, NPCType<WalkingAntlionD>());
            }
            if (summonCounter == AttackValue[2][4])
            {
                if (currentPhase >= 2)
                {
                    NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)Main.worldSurface + (3 * 16), NPCID.DuneSplicerHead);

                    if (Main.expertMode)
                    {
                        NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, NPCID.DesertScorpionWalk);
                    }
                }
            }

        }
        public void TornadoAI(int mark)
        {

            if (tornadoCounter == 0 && NPC.life > Utils1.GetValueFromPorcentage(NPC.lifeMax, 3))
            {
                tornadoCounter = Utils1.FormatTime(0, 0, 0, 5);
            }
            else if (tornadoCounter > 0)
            {
                tornadoCounter--;
            }
            else
            {
                tornadoCounter = 0;
            }

            if (!Reaper.ReaperMode)
            {
                if (tornadoCounter == 0)
                {
                    mark = Projectile.NewProjectile(Projectile.GetSource_None(), Main.player[NPC.target].position, Vector2.Zero, ProjectileID.SandnadoHostileMark, 0, 0, Main.myPlayer);
                }
                else if (tornadoCounter == Utils1.FormatTime(0, 0, 0, 4))
                {
                    Projectile.NewProjectile(Projectile.GetSource_None(), Main.projectile[mark].position, Vector2.Zero, ProjectileID.SandnadoHostile, 30, 1, Main.myPlayer);
                }           
            }
            else
            {
                float distanceBetweenTornados = 5f;
                if (tornadoCounter == 1)
                {
                    for (int a = 0; a < 7; a++)
                    {
                        mark = Projectile.NewProjectile(Projectile.GetSource_None(), Main.player[NPC.target].position + new Vector2(a * 16 * distanceBetweenTornados * Main.player[NPC.target].direction, 0), Vector2.Zero, ProjectileID.SandnadoHostileMark, 0, 0, Main.myPlayer);
                    }
                }
                else if (tornadoCounter == Utils1.FormatTime(0, 0, 0, 4))
                {
                    Projectile.NewProjectile(Projectile.GetSource_None(), Main.projectile[mark].position, Vector2.Zero, ProjectileID.SandnadoHostile, 40, 1, Main.myPlayer);
                }
            }

        }
        public override void HitEffect(int hitDirection, double damage)
        {
            int choice = Main.rand.Next(2, 8);
            if (Reaper.ReaperMode || BossIsInRage) choice *= 2;
            for (int i = 0; i < choice; i++)
            {
                NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, NPCID.SandSlime);
            }
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
        public List<int[]> setAttackCounter()
        {
            int[] MaxCounter = new int[2] { 0, 0 };
            int[] AttackCounter = new int[6] { 0, 0, 0, 0, 0, 0 };
            int[] SummonCounter = new int[5] { 0, 0, 0, 0, 0};

            if (!Main.expertMode && !Main.masterMode)
            {
                MaxCounter[0] = Utils1.FormatTime(0, 0, 0, 15);
                MaxCounter[1] = Utils1.FormatTime(0, 0, 0, 14);
            }
            else if (Main.expertMode && !Main.masterMode)
            {
                MaxCounter[0] = Utils1.FormatTime(0, 0, 0, 13);
                MaxCounter[1] = Utils1.FormatTime(0, 0, 0, 13);
            }
            else if (Main.expertMode && Main.masterMode)
            {
                MaxCounter[0] = Utils1.FormatTime(0, 0, 0, 10);
                MaxCounter[1] = Utils1.FormatTime(0, 0, 0, 8);
            }
            for (int i = 0; i < AttackCounter.Length; i++)
            {
                AttackCounter[i] = (MaxCounter[0] / AttackCounter.Length) * i;
            }
            for (int l = 0; l < SummonCounter.Length; l++)
            {
                SummonCounter[l] = (MaxCounter[1] / SummonCounter.Length) * l;
            }

            List<int[]> ListCounter = new List<int[]>()
            {
                MaxCounter,
                AttackCounter,
                SummonCounter
            };
            return ListCounter;
        }
        public void UpdateCounters(List<int[]> ListCounter)
        {
            if (summonCounter > 0) summonCounter--;
            if (attackCounter > 0) attackCounter--;

            if (summonCounter <= 0)
            {
                var a = ListCounter[0][1];
                summonCounter = ListCounter[0][1]; // Main.masterMode ? masterAttackCounterScale : Main.expertMode ? expertAttackCounterScale : normalAttackCounterScale;
                NPC.netUpdate = true;
            }

            if (attackCounter <= 0)
            {
                attackCounter = ListCounter[0][0];// Main.masterMode ? masterAttackCounterScale : Main.expertMode ? expertAttackCounterScale : normalAttackCounterScale;
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
            tpDirection = new Random().Next(1,2);

            switch (tpDirection)
            {
                case 1:
                    NPC.Center = GetSecurePosition(Main.player[NPC.target].Center + new Vector2(30 * 16,0));
                    break;
               // case 2:
                    //NPC.Center = GetSecurePosition(Main.player[NPC.target].Center + new Vector2(0, -20 * 16));
                    break;
                case 2:
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

            if (CalamityUtils.IsDificultyActive("death")) return applyLifeSize(porcentage, 2f);
            else if (CalamityUtils.IsDificultyActive("revengeance") || Reaper.ReaperMode) return applyLifeSize(porcentage, 1.5f);
            else if (Reaper.ReaperMode) return applyLifeSize(porcentage, 1.5f);
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
        
        
        public void ShootHelper(int dammage, int type, Player player, float Speed, double x, double y)
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