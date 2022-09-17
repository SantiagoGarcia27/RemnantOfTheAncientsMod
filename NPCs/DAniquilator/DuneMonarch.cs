using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using RemnantOfTheAncientsMod.Buffs;
using RemnantOfTheAncientsMod.Items.Items;
using static Terraria.ModLoader.ModContent;
using Microsoft.Xna.Framework;
using Terraria.GameContent.ItemDropRules;
using RemnantOfTheAncientsMod.Projectiles;
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
                if (Main.expertMode)
                {
                    if (!Reaper.ReaperMode)
                    {
                        if (Currentphase == 1)
                        {
                            CounterProjectiles(800);
                            if (ShootCounter == 100) RainCactusIa(90, 0.5f, 0.5f);
                        }
                        else if (Currentphase == 2 || Currentphase == 3)
                        {
                            CounterProjectiles(800);
                            if (ShootCounter == 400) TyphonIa(30, 0.5f, 0.5f);
                            if (ShootCounter == 100) RainCactusIa(90, 0.5f, 0.5f);
                        }
                    }
                    else
                    {
                        CounterProjectiles(700);
                        if (ShootCounter >= 350)
                        {
                            TyphonIa(30, 0.5f, 0.5f);
                            TyphonIa(30, 0.5f, -0.5f);
                            TyphonIa(30, -0.5f, -0.5f);
                            TyphonIa(30, -0.5f, 0.5f);
                        }
                    }
                }
            }
            else
            {
                if (Main.expertMode)
                {
                    if (!Reaper.ReaperMode && Currentphase == 2)
                    {
                        CounterProjectiles(500);
                        if (ShootCounter == 200) TyphonIa(30, 0.5f, 0.5f);
                    }
                    else
                    {
                        CounterProjectiles(400);
                        if (ShootCounter >= 150)
                        {
                            TyphonIa(30, 0.5f, 0.5f);
                            TyphonIa(30, 0.5f, -0.5f);
                            TyphonIa(30, -0.5f, -0.5f);
                            TyphonIa(30, -0.5f, 0.5f);
                        }
                    }
                }
            }
        }
        public void SummoningIa(Player player, float distance)
        {

            if (player.ZoneDesert || player.ZoneUndergroundDesert)
            {
                #region normal
                if (!Main.expertMode)
                {
                    CounterSummon(800);
                    switch (EnemyCounter)
                    {
                        case 150:
                            NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, NPCType<DSlime>());
                            break;
                        case 200:
                            NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, NPCID.TombCrawlerHead);
                            break;
                        case 300:
                            NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, NPCType<FlyingAntlionD>());
                            break;
                        case 350:
                            NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, NPCType<WalkingAntlionD>());
                            break;
                    }
                    if (Currentphase == 2 && EnemyCounter == 799) NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, NPCID.DuneSplicerHead);
                }
                #endregion
                else
                {
                    #region Expert
                    if (!Reaper.ReaperMode)
                    {
                        CounterSummon(600);
                        switch (EnemyCounter)
                        {
                            case 100:
                                NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, NPCType<DSlime>());
                                break;
                            case 150:
                                NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, NPCID.TombCrawlerHead);
                                break;
                            case 250:
                                NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, NPCType<FlyingAntlionD>());
                                break;
                            case 3500:
                                NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, NPCType<WalkingAntlionD>());
                                break;
                            case 500:
                                NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, NPCID.DesertScorpionWalk);
                                break;
                        }
                        if (Currentphase == 2 && EnemyCounter == 599) NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, NPCID.DuneSplicerHead);
                    }
                    else
                    {
                        #region reaper
                        CounterSummon(400);
                        switch (EnemyCounter)
                        {
                            case 50:
                                NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, NPCType<DSlime>());
                                break;
                            case 100:
                                NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, NPCID.TombCrawlerHead);
                                break;
                            case 250:
                                NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, NPCType<FlyingAntlionD>());
                                break;
                            case 300:
                                NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, NPCType<WalkingAntlionD>());
                                break;
                            case 400:
                                NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, NPCID.DesertScorpionWalk);
                                break;
                        }
                        if (Currentphase == 2 && EnemyCounter == 399) NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, NPCID.DuneSplicerHead);
                    }
                    #endregion
                }
            }
            #endregion
            else
            {
                CounterSummon(200);

                switch (EnemyCounter)
                {
                    case 50:
                        NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, NPCType<DSlime>());
                        break;
                    case 30:
                        NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, NPCID.TombCrawlerHead);
                        break;
                    case 90:
                        NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, NPCType<FlyingAntlionD>());
                        break;
                    case 100:
                        NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, NPCType<WalkingAntlionD>());
                        break;
                    case 120:
                        NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, NPCID.DesertScorpionWalk);
                        break;
                }
                if (Currentphase == 2 && EnemyCounter == 99) NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, NPCID.DuneSplicerHead);
            }
        }
        public void DesertTp(float distance, Player player)
        {
            if (distance >= 100 * 16) NPC.Center = Main.player[NPC.target].Center + new Vector2(Main.rand.Next(-250 * 2, 150 * 2), Main.rand.Next(-250 * 2, 150 * 2));
            if (distance >= 300 * 16) NPC.Center = player.Center;
            if (Currentphase == 2)
            {
                if (NPC.ai[1] > 380 && distance >= 100 * 16) NPC.Center = player.Center;
                if (NPC.ai[1] > 799 && distance >= 5 * 16 && distance >= 1 * 16) NPC.Center = Main.player[NPC.target].Center + new Vector2(Main.rand.Next(-250 * 2, 150 * 2), Main.rand.Next(-250 * 2, 150 * 2));
            }
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
            float rotation = (float)Math.Atan2(vector8.Y - (Main.player[NPC.target].position.Y + (Main.player[NPC.target].height * yPosition)), vector8.X - (Main.player[NPC.target].position.X + (Main.player[NPC.target].width * xPostition)));
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
}