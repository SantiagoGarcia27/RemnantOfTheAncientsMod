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
using Terraria.DataStructures;
using System.Security.Cryptography.X509Certificates;

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
            Music = MusicLoader.GetMusicSlot(Mod, "Sounds/Music/Desert_Aniquilator");
            NPC.netAlways = true;
            AnimationType = NPCID.BlueSlime;
            NPC.stepSpeed = 8f;
        }

        private int attackCounter;
        private int summonCounter;
        private int tpDirection;
        private int currentPhase;

        private int jumpTimer;
        private int jumpCounter;
        private int AfkCounter = 0;


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
        public override void OnSpawn(IEntitySource source)
        {
           // NPC.velocity.X = 8f;
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

            //MovmentIA(player);
            if (NPC.Distance(player.Center) >= 100 * 16 && !player.dead)
            {
                DesertTp();
            }

            if (player.dead || NPC.target < 0 || NPC.target == 255 || !player.active)
            {
                NPC.TargetClosest(true);
            }

            setCurrentPhase(NPC);

            //if (player.ZoneDesert)
            //{
                AttackIA(player);
            //}
            //else
            //{
            //    RageIA(player);
            //}

            if (player.dead)
            {
                NPC.EncourageDespawn(7);
                DespawnBoss();
            }
        }
        private const float MoveSpeed = 8f;
        private const float JumpHeight = 12f;
        private const int MaxJumps = 2;
       /* private void MovmentIA(Player player)
        {

            
            if (NPC.ai[0] == 0f) // initialize AI
            {
                NPC.ai[0] = 1f;
                jumpTimer = 0;
                jumpCounter = MaxJumps;
            }

            if (NPC.justHit) // if hit by a player
            {
                if (NPC.velocity.X > 0f) // move to the left
                {
                    NPC.direction = -1;
                }
                else if (NPC.velocity.X < 0f) // move to the right
                {
                    NPC.direction = 1;
                }

                jumpCounter = MaxJumps;
            }

            // jump
            if (jumpTimer > 0)
            {
                jumpTimer--;
            }
            else if (NPC.collideY)
            {
                NPC.velocity.Y = -JumpHeight;
                jumpCounter--;

                if (jumpCounter <= 0)
                {
                    jumpCounter = MaxJumps;
                    jumpTimer = 60; // wait before jumping again
                }
            }



            float MoveSpeed = 8f;
            float Inertia = 0.995f;
            bool isTouchingPlayer = false;

            // Verificar si el jugador está cerca y ajustar la dirección y velocidad del NPC
            if (player.active && !player.dead)
            {
                float distance = Vector2.Distance(player.Center, NPC.Center);
                if (distance < 300f)
                {
                    // Calcular dirección hacia el jugador
                    int direction = NPC.Center.X < player.Center.X ? 1 : -1;

                    // Si el NPC está en contacto con el jugador, ajustar su dirección y velocidad basado en la inercia
                    if (distance < NPC.width / 2f + player.width / 2f)
                    {
                        isTouchingPlayer = true;
                        if (NPC.velocity.X != 0f)
                        {
                            NPC.velocity.X *= Inertia;
                            if (Math.Abs(NPC.velocity.X) < 0.1f)
                            {
                                NPC.velocity.X = 0f;
                            }
                        }
                    }
                    else
                    {
                        isTouchingPlayer = false;
                        NPC.velocity.X = MoveSpeed * direction;
                    }

                    // Actualizar dirección del NPC
                    NPC.direction = direction;
                    NPC.spriteDirection = direction;
                }

                if(NPC.position == NPC.oldPosition)
                {
                    if (AfkCounter >= 20)
                    {
                        AfkCounter = 0;
                        NPC.velocity.Y -= 20f;
                    }
                    else
                    {
                        AfkCounter++;
                    }

                }
               
            }

            //NPC.velocity.Y = 0f; // No queremos que el NPC se mueva verticalmente

            // Movemos el NPC
            // NPC.position += NPC.velocity;



            //// animate
            //NPC.frameCounter++;
            //if (NPC.frameCounter >= 8)
            //{
            //    NPC.frameCounter = 0;
            //    NPC.frame.Y += 96;

            //    if (NPC.frame.Y > 480)
            //    {
            //        NPC.frame.Y = 0;
            //    }
            //}




            //if (NPC.life > 0)
            //{
            //    for (int a = 0; a < 10 / (double)NPC.lifeMax * 300.0; a++)
            //    {
            //        Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.TintableDust, 0, -1f, 175, new Color(0, 80, 255, 100));
            //    }
            //    return;
            //}
            //for (int i = 0; i < 200; i++)
            //{
            //    Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.TintableDust, 2 * 0, -2f, 175, new Color(0, 80, 255, 100));
            //}
            //Gore.NewGore(NPC.GetSource_FromAI(),NPC.Center + new Vector2(-40f, -NPC.height / 2), NPC.velocity, 734);
            //if (Main.netMode == NetmodeID.MultiplayerClient)
            //{
            //    return;
            //}        
            //for (int j = 0; j < Main.rand.Next(4) + 4; j++)
            //{
            //    int x = (int)(NPC.position.X + (float)Main.rand.Next(NPC.width - 32));
            //    int y = (int)(NPC.position.Y + (float)Main.rand.Next(NPC.height - 32));
            //    int NPC = NPC.NewNPC(NPC.GetSource_FromAI(), x, y, NPCType<DSlime>(), NPC.whoAmI + 1);
            //    Main.NPC[NPC].SetDefaults(1);
            //    Main.NPC[NPC].velocity.X = (float)Main.rand.Next(-15, 16) * 0.1f;
            //    Main.NPC[NPC].velocity.Y = (float)Main.rand.Next(-30, 1) * 0.1f;
            //    Main.NPC[NPC].ai[0] = -1000 * Main.rand.Next(3);
            //    Main.NPC[NPC].ai[1] = 0f;
            //    if (Main.netMode == 2 && NPC < 200)
            //    {
            //        NetMessage.SendData(23, -1, -1, null, NPC);
            //    }
            //}
        }*/
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
                            DesertTp();
                            break;
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
                            DesertTp();
                            break;
                    }
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
                    NPC.Center = Main.player[NPC.target].Center + new Vector2(30* 16,0);
                    break;
                case 2:
                    NPC.Center = Main.player[NPC.target].Center + new Vector2(0, -20* 16);
                    break;
                case 3:
                    NPC.Center = Main.player[NPC.target].Center + new Vector2(-30 * 16, 0);
                    break;

            }
        }
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
        public override bool? CanFallThroughPlatforms() => true;
        
           
        
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