using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using opswordsII.Buffs;
using opswordsII.Items.Items;
using static Terraria.ModLoader.ModContent;
using Microsoft.Xna.Framework;
using Terraria.GameContent.ItemDropRules;
using opswordsII.Projectiles;
using System;
using opswordsII.Items.Bloques;
using opswordsII.Items.Mele;
using opswordsII.Items.Magic;
using opswordsII.Items.Summon;
using opswordsII.Items.tresure_bag;
using opswordsII.Items.Ranger.Bows;
using opswordsII.Items.Bloques.Relics;
using opswordsII.Items.Armor.Masks;
using opswordsII.Common.Systems;
using Terraria.Localization;
using opswordsII.World;
using opswordsII.VanillaChanges;
using Terraria.Audio;

namespace opswordsII.NPCs.DAniquilator
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
		}
        public override void SetDefaults()
        {
            NPC.aiStyle = 15;  //15 is the king AI
            NPC.lifeMax = (int)NpcChanges1.ExpertLifeScale(3500,"MyBoss");  
            NPC.damage = (int)NpcChanges1.ExpertDamageScale(30, "MyBoss");  
            NPC.defense = 10;    
            NPC.knockBackResist = 0f;
            NPC.width = 100;
            NPC.height = 100;
            AnimationType = NPCID.BlueSlime;   
            NPC.value = Item.buyPrice(0, 2, 75, 45);
            NPC.npcSlots = 30f;
            NPC.boss = true;  
            NPC.lavaImmune = true;
            NPC.noGravity = false;
            NPC.noTileCollide = false;
            NPC.HitSound = SoundID.NPCHit1;
			NPC.DeathSound = SoundID.NPCDeath1;
            NPC.buffImmune[24] = true;
            Music = MusicLoader.GetMusicSlot(Mod,"Sounds/Music/Desert_Aniquilator");
            NPC.netAlways = true;
        }

        public override void AI()
        {
            Reaper reaper = new Reaper();

            float distance = NPC.Distance(Main.player[NPC.target].Center);

            NPC.ai[0] = 10;
            NPC.ai[1]++;
            Player player = Main.player[NPC.target];
           
            if (NPC.target < 0 || NPC.target == 255 || Main.player[NPC.target].dead || !Main.player[NPC.target].active) NPC.TargetClosest(true);
            NPC.netUpdate = true;


            if (NPC.ai[1] > 800) NPC.ai[1] = 0;
            NPC.ai[2]++;

            if (distance >= 100 * 16) NPC.Center = Main.player[NPC.target].Center + new Vector2(Main.rand.Next(-250 * 2, 150 * 2), Main.rand.Next(-250 * 2, 150 * 2));
            if (distance >= 300 * 16) NPC.Center = player.Center;
            


            bool fase2 = NPC.life == NPC.lifeMax / 2;
            bool fase3 = NPC.life == NPC.lifeMax / 3;

            if (player.ZoneDesert)
            {
                #region Normal
                if (NPC.ai[2] % 356 == 15) NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, NPCType<DSlime>());
                if (NPC.ai[2] % 600 == 100) NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, NPCID.TombCrawlerHead);
                if (NPC.ai[2] % 300 == 100)
                {
                    NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, NPCType<FlyingAntlionD>());
                    NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, NPCType<WalkingAntlionD>());
                }
                if (fase2)
                {
                    if (NPC.ai[3] % 900 == 900) NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, NPCID.DuneSplicerHead);
                    DesertTp(distance, player);
                }

                if (Main.expertMode || Main.masterMode)
                {
                    if (!world1.ReaperMode)
                    {
                        if (NPC.ai[2] % 300 == 300)
                        {
                            NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, NPCType<AntlionD>());
                            NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, NPCType<VultureD>());
                        }
                        if (NPC.ai[2] % 600 == 300)
                        {
                            NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, NPCID.DesertScorpionWalk);
                            TyphonIa(30, 0.5f, 0.5f);
                        }
                    }
                    else
                    {
                        if (NPC.ai[2] % 100 == 100)
                        {
                            NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, NPCType<AntlionD>());
                            NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, NPCType<VultureD>());
                        }
                        if (NPC.ai[2] % 600 == 200)
                        {
                            NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, NPCID.DesertScorpionWalk);
                            TyphonIa(30, 0.5f, 0.5f);
                            TyphonIa(30, 0.5f, -0.5f);
                            TyphonIa(30, -0.5f, -0.5f);
                            TyphonIa(30, -0.5f, 0.5f);
                        }
                    }
                }
                #endregion 
            }
            else
            {
                #region rage
                if (NPC.ai[2] % 356 == 15) NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, NPCType<DSlime>());
                if (NPC.ai[2] % 600 == 200) NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, NPCID.TombCrawlerHead);
                if (NPC.ai[2] % 300 == 200)
                {
                    NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, NPCType<FlyingAntlionD>());
                    NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, NPCType<WalkingAntlionD>());
                }
                if (fase2) if (NPC.ai[3] % 900 == 700) NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, NPCID.DuneSplicerHead);
                if (Main.expertMode || Main.masterMode)
                {
                    if (world1.ReaperMode)
                    {
                        if (NPC.ai[2] % 300 == 200)
                        {
                            NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, NPCType<AntlionD>());
                            NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, NPCType<VultureD>());
                        }
                        if (NPC.ai[2] % 600 == 200)
                        {
                            NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, NPCID.DesertScorpionWalk);
                            NPC.ai[2] = 0;
                        }
                        if (NPC.ai[2] % 300 == 200)
                        {
                            NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, NPCType<AntlionD>());
                            NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, NPCType<VultureD>());
                        }
                        if (NPC.ai[2] % 300 == 200)
                        {
                            NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, NPCID.Tumbleweed);
                        }
                    }
                    else
                    {
                        if (!fase3)
                        {
                            if (NPC.ai[2] % 600 == 200)
                            {
                                NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, NPCID.DesertScorpionWalk);
                                TyphonIa(30, 0.5f, 0.5f);
                                TyphonIa(30, 0.5f, -0.5f);
                                NPC.ai[2] = 0;
                            }
                            if (NPC.ai[2] % 300 == 200)
                            {
                                NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, NPCType<AntlionD>());
                                NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, NPCType<VultureD>());
                                NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, NPCID.Tumbleweed);
                            }
                        }
                        else
                        {
                            if (NPC.ai[2] % 600 == 100)
                            {
                                TyphonIa(30, 0.5f, 0.5f);
                                TyphonIa(30, 0.5f, -0.5f);
                                TyphonIa(30, -0.5f, -0.5f);
                                TyphonIa(30, -0.5f, 0.5f);
                                TyphonIa(30, 1.5f, 1.5f);
                                TyphonIa(30, 1.5f, -1.5f);
                                TyphonIa(30, -1.5f, -1.5f);
                                TyphonIa(30, -1.5f, 1.5f);
                                NPC.ai[2] = 0;
                            }
                        }
                    }
                }
                #endregion
            }
        }
        public void DesertTp(float distance, Player player)
        {
            if (NPC.ai[1] > 380 && distance >= 100 * 16) NPC.Center = player.Center;
            if (NPC.ai[1] > 799 && distance >= 5 * 16 && distance >= 1 * 16)
                NPC.Center = Main.player[NPC.target].Center + new Vector2(Main.rand.Next(-250 * 2, 150 * 2), Main.rand.Next(-250 * 2, 150 * 2));
        }
        public void TyphonIa(int i, float xA, float yA)
        {
            float Speed = 12f;
            int damage = (int)NpcChanges1.ExpertDamageScale(i, "MyBoss"); ;
            Vector2 vector8 = new Vector2(NPC.position.X + (NPC.width / 2), NPC.position.Y + (NPC.height / 2));
            int type = ModContent.ProjectileType<DesertTyphoonE>();
            SoundEngine.PlaySound(SoundID.Item10,NPC.position);
            float rotation = (float)Math.Atan2(vector8.Y - (Main.player[NPC.target].position.Y + (Main.player[NPC.target].height * yA)), vector8.X - (Main.player[NPC.target].position.X + (Main.player[NPC.target].width * xA)));
            int num54 = Projectile.NewProjectile(NPC.GetSource_FromAI(), vector8.X, vector8.Y, (float)(Math.Cos(rotation) * Speed * -1), (float)((Math.Sin(rotation) * Speed) * -1), type, damage, 0f, 0);
        }
        public override void OnHitPlayer(Player player, int damage, bool crit) {
			if (Main.rand.NextBool(3)) {
				player.AddBuff(BuffType<Burning_Sand>(), 100, true);
			}
		}
        public override void BossLoot(ref string name, ref int potionType)
        {
            DownedBossSystem.downedDesert = true;
            potionType = ItemID.HealingPotion;  
            Item.NewItem(NPC.GetSource_Loot(),(int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, ItemID.SandBlock, 60);
            Item.NewItem(NPC.GetSource_Loot(),(int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, ItemType<Sand_escense>(), 10);
        }
        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {

            LeadingConditionRule notExpertRule = new LeadingConditionRule(new Conditions.NotExpert());
            npcLoot.Add(ItemDropRule.NormalvsExpert(ItemType<DesertAMask>(),7,999999999));

            npcLoot.Add(ItemDropRule.NormalvsExpert(ItemType<desertbow>(), 4,999999999));
            npcLoot.Add(ItemDropRule.NormalvsExpert(ItemType<DesertEdge>(), 4, 999999999));
            npcLoot.Add(ItemDropRule.NormalvsExpert(ItemType<DesertStaff>(), 4, 999999999));
            npcLoot.Add(ItemDropRule.NormalvsExpert(ItemType<DesertTome>(), 4, 999999999));

            npcLoot.Add(ItemDropRule.BossBag(ItemType<desertBag>()));
            npcLoot.Add(ItemDropRule.MasterModeCommonDrop(ItemType<Desert_Relic>()));
            npcLoot.Add(ItemDropRule.Common(ItemType<DesertTrophy>(), 10));
        }	
        
   
    }
}