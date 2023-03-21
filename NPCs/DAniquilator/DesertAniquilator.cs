using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using RemnantOfTheAncientsMod.Items.Items;
using static Terraria.ModLoader.ModContent;
using Microsoft.Xna.Framework;
using Terraria.GameContent.ItemDropRules;
using System;
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
using RemnantOfTheAncientsMod.Projectiles.BossProjectile;
using RemnantOfTheAncientsMod.Buffs.Debuff;
using RemnantOfTheAncientsMod.Items.Bloques.Trophy;
using System.IO;

namespace RemnantOfTheAncientsMod.NPCs.DAniquilator
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
            NPC.lifeMax = (int)NpcChanges1.ExpertLifeScale(3500);  
            NPC.damage = (int)NpcChanges1.ExpertDamageScale(30);  
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
            Music = MusicLoader.GetMusicSlot(Mod,"Sounds/Music/Desert_Aniquilator");
            NPC.netAlways = true;
            AnimationType = NPCID.BlueSlime;
        }

        private int attackCounter;
        private int summonCounter;
        private int tpDirection;
        private int currentPhase;

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
            //Player player = Main.player[NPC.target];

            //float distance = NPC.Distance(Main.player[NPC.target].Center);

            //NPC.ai[0] = 10;
            //NPC.ai[1]++;
            //if (NPC.ai[1] > 800) NPC.ai[1] = 0;
            //NPC.ai[2]++;


            //if (distance >= 100 * 16 && !player.dead) DesertTp();

            //Player target = Main.player[NPC.target];

            //if (target.dead)
            //{
            //    NPC.EncourageDespawn(7);
            //    DespawnBoss();
            //    return;
            //}
            //if (NPC.target < 0 || NPC.target == 255 || Main.player[NPC.target].dead || !Main.player[NPC.target].active)
            //{
            //    NPC.TargetClosest(true);
            //}
            //setCurrentPhase(NPC);
            //if (player.ZoneDesert)
            //{
            //     NormalIA(target);
            //}
            //else
            //{
            //    RageIA(target);
            //}


            Player player = Main.player[NPC.target];

            NPC.ai[0] = 10;
            NPC.ai[1] = (NPC.ai[1] + 1) % 800;
            NPC.ai[2]++;

            if (NPC.Distance(player.Center) >= 100 * 16 && !player.dead)
            {
                DesertTp();
            }

            if (player.dead || NPC.target < 0 || NPC.target == 255 || !player.active)
            {
                NPC.TargetClosest(true);
            }

            setCurrentPhase(NPC);

            if (player.ZoneDesert)
            {
                NormalIA(player);
            }
            else
            {
                RageIA(player);
            }

            if (player.dead)
            {
                NPC.EncourageDespawn(7);
                DespawnBoss();
            }
        }

        private void NormalIA(Player target)
        {

            if (attackCounter >= 0)
            {
                setAttackCounter(1000);
            }

            if (!Reaper.ReaperMode)
            {
                //switch (summonCounter)
                //{
                //    case 600:
                //        NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, NPCType<DSlime>());
                //        break;
                //    case 400:
                //        NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, NPCType<DSlime>());
                //        break;
                //    case 200:
                //        if(Main.expertMode) NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, NPCID.TombCrawlerHead);
                //        break;
                //    case 100:
                //        NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, NPCType<FlyingAntlionD>());
                //        NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, NPCType<WalkingAntlionD>());
                //        break;
                //    case 10:
                //        if (currentPhase >= 2) 
                //            NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, NPCID.DuneSplicerHead);
                //            break;
                //}
                //if (Main.expertMode)
                //{                  
                //    switch (attackCounter)
                //    {
                //        case 600:
                //            ShootIa((int)NpcChanges1.ExpertDamageScale(30), ProjectileType<DesertTyphoon>(),target,12f, 0.5f, 0.5f);
                //            break;
                //        case 590:
                //            ShootIa((int)NpcChanges1.ExpertDamageScale(30), ProjectileType<DesertTyphoon>(), target, 12f, 0.5f, -0.5f);
                //            break;
                //        case 580:
                //            ShootIa((int)NpcChanges1.ExpertDamageScale(30), ProjectileType<DesertTyphoon>(), target, 12f, -0.5f, -0.5f);
                //            break;
                //        case 530:
                //            ShootIa((int)NpcChanges1.ExpertDamageScale(30), ProjectileType<DesertTyphoon>(), target, 12f, -0.5f, 0.5f);
                //            break;
                //        case 500:
                //            DesertTp();
                //            break;
                //    }
                //}        
                if (summonCounter == 600 || summonCounter == 400)
                {
                    NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, NPCType<DSlime>());
                }
                else if (summonCounter == 200 && Main.expertMode)
                {
                    NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, NPCID.TombCrawlerHead);
                }
                else if (summonCounter == 100)
                {
                    NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, NPCType<FlyingAntlionD>());
                    NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, NPCType<WalkingAntlionD>());
                }
                else if (summonCounter == 10 && currentPhase >= 2)
                {
                    NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, NPCID.DuneSplicerHead);
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
                            ShootIa((int)NpcChanges1.ExpertDamageScale(30), ProjectileType<DesertTyphoon>(), target, 12f, -0.5f, 0.5f);
                            break;
                        case 500:
                            DesertTp();
                            break;
                    }
                }
            }
            else
            {
                //switch (summonCounter)
                //{
                //case 500:
                //    NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, NPCType<DSlime>());
                //    break;
                //case 400:
                //    NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, NPCType<DSlime>());
                //    break;
                //case 200:
                //    NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, NPCID.TombCrawlerHead);
                //    break;
                //case 100:
                //    NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, NPCType<FlyingAntlionD>());
                //    NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, NPCType<WalkingAntlionD>());
                //    break;

                if (summonCounter == 500 || summonCounter == 400)
                {
                    NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, NPCType<DSlime>());
                }
                else if (summonCounter == 200)
                {
                    NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, NPCID.TombCrawlerHead);
                }
                else if (summonCounter == 100)
                {
                    NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, NPCType<FlyingAntlionD>());
                    NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, NPCType<WalkingAntlionD>());
                }
                else if (summonCounter == 10 && currentPhase >= 2)
                {
                    NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, NPCID.DuneSplicerHead);
                }
                // }
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
                            DesertTp();
                            break;
                    }
                }
            }
        }
        private void RageIA(Player target)
        {
            if (Vector2.Distance(NPC.Center, target.Center) < 200 && Collision.CanHit(NPC.Center, 1, 1, target.Center, 1, 1))
            {
                setAttackCounter(1000);
            }
            if (!Reaper.ReaperMode)
            {
                switch (summonCounter)
                {
                    case 600:
                        NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, NPCType<DSlime>());
                        break;
                    case 400:
                        NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, NPCType<DSlime>());
                        break;
                    case 200:
                        if (Main.expertMode) NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, NPCID.TombCrawlerHead);
                        break;
                    case 100:
                        NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, NPCType<FlyingAntlionD>());
                        NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, NPCType<WalkingAntlionD>());
                        break;
                    case 10:
                        if (currentPhase >= 2)
                            NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, NPCID.DuneSplicerHead);
                        break;
                }
                if (Main.expertMode)
                {
                    switch (attackCounter)
                    {
                        case 600:
                            ShootIa((int)NpcChanges1.ExpertDamageScale(40), ProjectileType<DesertTyphoon>(), target, 12f, 0.5f, 0.5f);
                            break;
                        case 590:
                            ShootIa((int)NpcChanges1.ExpertDamageScale(40), ProjectileType<DesertTyphoon>(), target, 12f, 0.5f, -0.5f);
                            break;
                        case 580:
                            ShootIa((int)NpcChanges1.ExpertDamageScale(40), ProjectileType<DesertTyphoon>(), target, 12f, -0.5f, -0.5f);
                            break;
                        case 530:
                            ShootIa((int)NpcChanges1.ExpertDamageScale(40), ProjectileType<DesertTyphoon>(), target, 12f, -0.5f, 0.5f);
                            break;
                        case 500:
                            DesertTp();
                            attackCounter++;
                            break;
                        default:
                            break;

                    }
                }
            }
            else
            {
                switch (summonCounter)
                {
                    case 500:
                        NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, NPCType<DSlime>());
                        break;
                    case 400:
                        NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, NPCType<DSlime>());
                        break;
                    case 200:
                        NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, NPCID.TombCrawlerHead);
                        break;
                    case 100:
                        NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, NPCType<FlyingAntlionD>());
                        NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, NPCType<WalkingAntlionD>());
                        break;
                }
                if (Main.expertMode)
                {
                    switch (attackCounter)
                    {
                        case 500:
                            ShootIa((int)NpcChanges1.ExpertDamageScale(40), ProjectileType<DesertTyphoon>(), target, 12f, 0.5f, 0.5f);
                            break;
                        case 490:
                            ShootIa((int)NpcChanges1.ExpertDamageScale(40), ProjectileType<DesertTyphoon>(), target, 12f, 0.5f, -0.5f);
                            break;
                        case 480:
                            ShootIa((int)NpcChanges1.ExpertDamageScale(40), ProjectileType<DesertTyphoon>(), target, 12f, -0.5f, -0.5f);
                            break;  
                        case 430:
                            ShootIa((int)NpcChanges1.ExpertDamageScale(40), ProjectileType<DesertTyphoon>(), target, 12f, -0.5f, 0.5f);
                            ShootIa((int)NpcChanges1.ExpertDamageScale(40), ProjectileType<DesertTyphoon>(), target, 12f, 3.5f, 3.5f);
                            ShootIa((int)NpcChanges1.ExpertDamageScale(40), ProjectileType<DesertTyphoon>(), target, 12f, 3.5f, -3.5f);
                            ShootIa((int)NpcChanges1.ExpertDamageScale(40), ProjectileType<DesertTyphoon>(), target, 12f, -3.5f, -3.5f);
                            ShootIa((int)NpcChanges1.ExpertDamageScale(40), ProjectileType<DesertTyphoon>(), target, 12f, -3.5f, 3.5f);
                            break;
                        case 481:
                            DesertTp();
                            break;
                    }
                }
            }
        }
        public void setAttackCounter(int i)
        {
            int masterAttackCounterScale = AttackCounterScale(i - 400);
            int expertAttackCounterScale = AttackCounterScale(i - 300);
            int normalAttackCounterScale = AttackCounterScale(i - 200);

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

        public void setCurrentPhase(NPC npc)
        {
            if (npc.life <= npc.lifeMax / 4) currentPhase = 3;
            else if (npc.life <= npc.lifeMax / 2) currentPhase = 2;
            else currentPhase = 1;
        }

        public int AttackCounterScale(int Num) => Reaper.ReaperMode ? Num - 100 : Num;
        public void DesertTp()
        {
            tpDirection = new Random().Next(1, 3);

            switch (tpDirection)
            {
                case 1:
                    NPC.Center = Main.player[NPC.target].Center + new Vector2(30* 16,0);
                    break;
                case 2:
                    NPC.Center = Main.player[NPC.target].Center + new Vector2(0, -20* 16);
                    break;
                case 3:
                    NPC.Center = Main.player[NPC.target].Center + new Vector2(-30 * 16, 0);
                    break;

            }

          /*  if (NPC.ai[1] > 380 && distance >= 100 * 16) NPC.Center = player.Center;
            if (NPC.ai[1] > 799 && distance >= 5 * 16 && distance >= 1 * 16)
                NPC.Center = Main.player[NPC.target].Center + new Vector2(Main.rand.Next(-250 * 2, 150 * 2), Main.rand.Next(-250 * 2, 150 * 2));*/
        }
        /* public void TyphonIa(int i, float xA, float yA)
         {
             float Speed = 12f;
             int damage = (int)NpcChanges1.ExpertDamageScale(i, true); ;
             Vector2 vector8 = new Vector2(NPC.position.X + (NPC.width / 2), NPC.position.Y + (NPC.height / 2));
             int type = ProjectileType<DesertTyphoon>();
             SoundEngine.PlaySound(SoundID.Item10,NPC.position);
             float rotation = (float)Math.Atan2(vector8.Y - (Main.player[NPC.target].position.Y + (Main.player[NPC.target].height * yA)), vector8.X - (Main.player[NPC.target].position.X + (Main.player[NPC.target].width * xA)));
             Projectile.NewProjectile(NPC.GetSource_FromAI(), vector8.X, vector8.Y, (float)(Math.Cos(rotation) * Speed * -1), (float)((Math.Sin(rotation) * Speed) * -1), type, damage, 0f, 0);
         }*/

        public void ShootIa(int dammage, int type, Player player, float Speed, double x, double y)
        {
            Vector2 vector8 = new Vector2(NPC.position.X + (NPC.width / 2), NPC.position.Y + (NPC.height / 2));
            float rotation = (float)Math.Atan2(vector8.Y - (player.position.Y + (player.height * x)), vector8.X - (player.position.X + (player.width * y)));
            Vector2 direction;
            direction.X = (float)(Math.Cos(rotation) * Speed * -1);
            direction.Y = (float)(Math.Sin(rotation) * Speed * -1);
            Projectile.NewProjectile(NPC.GetSource_FromAI(), vector8, direction, type, dammage, 0f, Main.myPlayer);
        }
        public void DespawnBoss()
        {
            NPC.velocity.X -= 3.09f;
            NPC.velocity.Y -= 3f;

            NPC.EncourageDespawn(7);
            return;
        }
        public override void OnHitPlayer(Player player, int damage, bool crit) 
        {
			if (Main.rand.NextBool(3)) player.AddBuff(BuffType<Burning_Sand>(), 100, true);	
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