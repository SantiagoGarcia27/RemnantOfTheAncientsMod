/*using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using RemnantOfTheAncientsMod.Items.Items;
using static Terraria.ModLoader.ModContent;
using Microsoft.Xna.Framework;
using Terraria.GameContent.ItemDropRules;
using System;
using RemnantOfTheAncientsMod.Items.Bloques;
using RemnantOfTheAncientsMod.Items.Mele;
using RemnantOfTheAncientsMod.Items.Magic;
using RemnantOfTheAncientsMod.Items.Summon;
using RemnantOfTheAncientsMod.Items.tresure_bag;
using RemnantOfTheAncientsMod.Items.Ranger.Bows;
using RemnantOfTheAncientsMod.Items.Bloques.Relics;
using RemnantOfTheAncientsMod.Items.Armor.Masks;
using RemnantOfTheAncientsMod.Common.Systems;
using Terraria.Localization;
using RemnantOfTheAncientsMod.World;
using RemnantOfTheAncientsMod.VanillaChanges;
using Terraria.Audio;
using RemnantOfTheAncientsMod.Projectiles.BossProjectile;
using RemnantOfTheAncientsMod.Buffs.Debuff;

namespace RemnantOfTheAncientsMod.NPCs.DAniquilator
{
    [AutoloadBossHead]
    public class DuneMonarch : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Dune Monarch");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Aniquilador del desierto");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Monarca de las dunas");
            Main.npcFrameCount[NPC.type] = Main.npcFrameCount[NPCID.BlueSlime];
        }
        public override void SetDefaults()
        {
            NPC.aiStyle = 15;
            NPC.lifeMax = (int)NpcChanges1.ExpertLifeScale(28000, "MyBoss");
            NPC.damage = (int)NpcChanges1.ExpertDamageScale(60, "MyBoss");
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
            Music = MusicLoader.GetMusicSlot(Mod, "Sounds/Music/Desert_Aniquilator");
            NPC.netAlways = true;
        }
        public static int EnemyCounter;
        public static int ShootCounter;
        public static int Currentphase;
        public override void AI()
        {
            float distance = NPC.Distance(Main.player[NPC.target].Center);
            Player player = Main.player[NPC.target];
            NPC.ai[0] = 10;
            NPC.ai[1]++;
            if (NPC.target < 0 || NPC.target == 255 || Main.player[NPC.target].dead || !Main.player[NPC.target].active) NPC.TargetClosest(true);
            NPC.netUpdate = true;
            if (NPC.ai[1] > 800) NPC.ai[1] = 0;
            PhaseUpdater();
            ShootIa(player);
            SummoningIa(player, distance);
            DesertTp(distance, player);
        }
        public void PhaseUpdater()
        {
            if (NPC.life <= NPC.lifeMax / 2) Currentphase = 2;
            else if (NPC.life <= NPC.lifeMax / 3) Currentphase = 3;
            else Currentphase = 1;
        }
        public void ShootIa(Player player)
        {
            if (player.ZoneDesert || player.ZoneUndergroundDesert) 
            {
                if (!Main.expertMode)
                {
                    if (!Reaper.ReaperMode)
                    {
                        CounterProjectiles(800);
                        if (Currentphase == 2 || Currentphase == 3)
                        { 
                            if (ShootCounter % 400 == 0)  TyphonIa(30, 0.5f, 0.5f);
                            if (ShootCounter % 100 == 0)  RainCactusIa(90, 0.5f, 0.5f);
                        }
                        else if (ShootCounter % 100 == 0)  RainCactusIa(90, 0.5f, 0.5f); 
                    }
                    else
                    {
                        CounterProjectiles(700);
                        if (ShootCounter % 600 == 0)
                        {
                            for (float n = -0.5f; n <= 0.5f; n += 0.5f)
                            {
                                TyphonIa(30, n, n);
                            }
                        }
                    }
                }
                else
                {
                    if (!Reaper.ReaperMode && Currentphase == 2) CounterProjectiles(500);
                    else CounterProjectiles(400);

                    if (!Reaper.ReaperMode)
                    {
                        if (Currentphase == 2 && ShootCounter % 200 == 0) { TyphonIa(30, 0.5f, 0.5f); Main.NewText(Reaper.ReaperMode); };
                        if (Currentphase == 3 && ShootCounter % 200 == 0) { TyphonIa(30, 0.5f, 0.5f); Main.NewText("z"); }
                    }
                    else
                    {
                        if (Currentphase == 2 && ShootCounter % 150 == 0)for (float n = -1.5f; n <= 20.5f;n += 5.5f) TyphonIa(30, 5, n);
                        else if (Currentphase == 3 && ShootCounter % 150 == 0) for (float n = -1.5f; n <= 40.5f; n += 2.5f) TyphonIa(30, 5, n);
                    }
                }  
            }
            else
            {
                CounterProjectiles(200);
                if (ShootCounter % 200 == 0) for (float n = -1.5f; n <= 30.5f; n += 1.5f) TyphonIa(30, 5, n); 
            }
        }
        public void SummoningIa(Player player, float distance)
        {
            int n; //DSlime
            int n2; //TombCrawler
            int n3; //FlyingAntlion
            int n4; //WalkingAntlion
            int n5; //DuneSplicerHead
            int n6; //DesertScorpionWalk
           
            if (player.ZoneDesert || player.ZoneUndergroundDesert)
            {
                if (!Main.expertMode)
                {
                    if (!Reaper.ReaperMode) CounterSummon(800);
                    else CounterSummon(600);
                    n = !Reaper.ReaperMode ? 150 : 100;
                    n2 = !Reaper.ReaperMode ? 200 : 150;
                    n3 = !Reaper.ReaperMode ? 300 : 250;
                    n4 = !Reaper.ReaperMode ? 350 : 300;
                    n5 = !Reaper.ReaperMode ? 799 : 599;
                    n6 = !Reaper.ReaperMode ? 500 : 99999;
                }
                else
                {
                    if (!Reaper.ReaperMode) CounterSummon(600);
                    else CounterSummon(500);
                    n = !Reaper.ReaperMode ? 100 : 60;
                    n2 = !Reaper.ReaperMode ? 150 : 120;
                    n3 = !Reaper.ReaperMode ? 250 : 180;
                    n4 = !Reaper.ReaperMode ? 350 : 350;
                    n5 = !Reaper.ReaperMode ? 599 : 400;
                    n6 = !Reaper.ReaperMode ? 600 : 499;
                }
            }
            else
            {
                CounterSummon(200);
                n = 10;
                n2 = 30;
                n3 = 50;
                n4 = 100;
                n5 = 200;
                n6 = 180;
            }
          
            if (EnemyCounter % n == 0) NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, NPCType<DSlime>());
            if (EnemyCounter % n2 == 0) NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, NPCID.TombCrawlerHead);
            if (EnemyCounter % n3 == 0) NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, NPCType<FlyingAntlionD>());
            if (EnemyCounter % n4 == 0) NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, NPCType<WalkingAntlionD>());
            if (Currentphase == 2 && EnemyCounter % n5 == 0) NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, NPCID.DuneSplicerHead);
            if (EnemyCounter % n6 == 0) NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, NPCID.DesertScorpionWalk);
        }
        public void DesertTp(float distance, Player player)
        {
            if (distance >= 100 * 16) NPC.Center = Main.player[NPC.target].Center + new Vector2(Main.rand.Next(-250 * 2, 150 * 2), Main.rand.Next(-250 * 2, 150 * 2));
            if (Currentphase == 2 && NPC.ai[1] > 799 && distance >= 5 * 16 && distance >= 1 * 16) 
                NPC.Center = Main.player[NPC.target].Center + new Vector2(Main.rand.Next(-250 * 2, 150 * 2), Main.rand.Next(-250 * 2, 150 * 2));
        }
        public void CounterProjectiles(int maxcount)
        {
            if (ShootCounter >= maxcount) ShootCounter = 0;
            else ShootCounter++;
        }
        public void CounterSummon(int maxcount)
        {
            if (EnemyCounter >= maxcount) EnemyCounter = 0;
            else EnemyCounter++;
        }

        public void RainCactusIa(int Damage, float xPostition, float yPosition)
        {
            Player player = Main.player[NPC.target];
            float Speed = 12f;
            int damage = (int)NpcChanges1.ExpertDamageScale(Damage, "MyBoss"); ;
            Vector2 vector8 = new Vector2(player.position.X + (NPC.width), ((NPC.position.Y + (NPC.width)) * 2f) + (NPC.height * 2));
            SoundEngine.PlaySound(SoundID.Item10, NPC.position);
            float rotation = (float)Math.Atan2(vector8.Y - (Main.player[NPC.target].position.Y + (Main.player[NPC.target].height * (yPosition*16))), vector8.X - (Main.player[NPC.target].position.X + (Main.player[NPC.target].width * (xPostition *16))));
            int num54 = Projectile.NewProjectile(NPC.GetSource_FromAI(), vector8.X, vector8.Y, (float)(Math.Cos(rotation) * Speed * -1), (float)((Math.Sin(rotation) * Speed) * -1), ProjectileID.RollingCactus, damage, 0f, 0);
        }
        public void TyphonIa(int i, float xA, float yA)
        {
            float Speed = 12f;
            int damage = (int)NpcChanges1.ExpertDamageScale(i, "MyBoss"); ;
            Vector2 vector8 = new Vector2(NPC.position.X + (NPC.width / 2), NPC.position.Y + (NPC.height / 2));
            int type = ProjectileType<DesertTyphoon>();
            SoundEngine.PlaySound(SoundID.Item10, NPC.position);
            float rotation = (float)Math.Atan2(vector8.Y - (Main.player[NPC.target].position.Y + (Main.player[NPC.target].height * yA)), vector8.X - (Main.player[NPC.target].position.X + (Main.player[NPC.target].width * xA)));
            int num54 = Projectile.NewProjectile(NPC.GetSource_FromAI(), vector8.X, vector8.Y, (float)(Math.Cos(rotation) * Speed * -1), (float)((Math.Sin(rotation) * Speed) * -1), type, damage, 0f, 0);
        }
        public override void OnHitPlayer(Player player, int damage, bool crit) => player.AddBuff(BuffType<Burning_Sand>(), 100, true);
        public override void BossLoot(ref string name, ref int potionType)
        {
            DownedBossSystem.downedDesert = true;
            potionType = ItemID.HealingPotion;
            Item.NewItem(NPC.GetSource_Loot(), (int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, ItemID.SandBlock, 60);
            Item.NewItem(NPC.GetSource_Loot(), (int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, ItemType<Sand_escense>(), 10);
        }
        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {

            LeadingConditionRule notExpertRule = new LeadingConditionRule(new Conditions.NotExpert());
            npcLoot.Add(ItemDropRule.NormalvsExpert(ItemType<DesertAMask>(), 7, 999999999));

            npcLoot.Add(ItemDropRule.NormalvsExpert(ItemType<desertbow>(), 4, 999999999));
            npcLoot.Add(ItemDropRule.NormalvsExpert(ItemType<DesertEdge>(), 4, 999999999));
            npcLoot.Add(ItemDropRule.NormalvsExpert(ItemType<DesertStaff>(), 4, 999999999));
            npcLoot.Add(ItemDropRule.NormalvsExpert(ItemType<DesertTome>(), 4, 999999999));

            npcLoot.Add(ItemDropRule.BossBag(ItemType<desertBag>()));
            npcLoot.Add(ItemDropRule.MasterModeCommonDrop(ItemType<Desert_Relic>()));
            npcLoot.Add(ItemDropRule.Common(ItemType<DesertTrophy>(), 10));
        }
    }
}*/