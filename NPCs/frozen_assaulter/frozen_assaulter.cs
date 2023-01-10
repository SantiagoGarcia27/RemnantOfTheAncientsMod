using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using RemnantOfTheAncientsMod.Items.tresure_bag;
using RemnantOfTheAncientsMod.Items.Mele;
using RemnantOfTheAncientsMod.Items.Ranger;
using RemnantOfTheAncientsMod.Items.Magic;
using RemnantOfTheAncientsMod.Items.Summon;
using RemnantOfTheAncientsMod.Items.Bloques;
using RemnantOfTheAncientsMod.Items.Bloques.Relics;
using RemnantOfTheAncientsMod.Items.Armor.Masks;
using static Terraria.ModLoader.ModContent;
using Microsoft.Xna.Framework;
using RemnantOfTheAncientsMod.Projectiles;
using Terraria.GameContent.ItemDropRules;
using RemnantOfTheAncientsMod.Common.Systems;
using Terraria.Audio;
using RemnantOfTheAncientsMod.VanillaChanges;
using RemnantOfTheAncientsMod.World;
using RemnantOfTheAncientsMod.Items.Bloques.Trophy;
using System.IO;

namespace RemnantOfTheAncientsMod.NPCs.frozen_assaulter
{
    [AutoloadBossHead]
    public class frozen_assaulter : ModNPC
    {
        int invincibilityTimer = 0;
        bool healAnimation = false;
        int currentPhase = 1;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Frozen Assaulter");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Asaltante Congelado");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Agresseur gelé");
            Main.npcFrameCount[NPC.type] = 8;
        }
        public override void SetDefaults()
        {
            NPC.aiStyle = 5;  
            NPC.lifeMax = (int)NpcChanges1.ExpertLifeScale(18500, true);   
            NPC.damage = (int)NpcChanges1.ExpertDamageScale(90, true); 
            NPC.defense = 15;    
            NPC.knockBackResist = 0f;
            NPC.width = 100;
            NPC.height = 100; 
            Main.npcFrameCount[NPC.type] = 8; 
            NPC.value = Item.buyPrice(0, 5, 75, 45);
            NPC.npcSlots = 10f;
            NPC.boss = true;
            NPC.lavaImmune = true;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.buffImmune[24] = true;
            Music = MusicLoader.GetMusicSlot(Mod, "Sounds/Music/Frozen_Assaulter_p1");
            NPC.netAlways = true;
        }
        private int attackCounter;

        public override void SendExtraAI(BinaryWriter writer)
        {
            writer.Write(attackCounter);
        }
        public override void ReceiveExtraAI(BinaryReader reader)
        {
            attackCounter = reader.ReadInt32();
        }

        public override void AI()
        {



            //NPC.ai[0]++;

            //NPC.ai[1]++;
            //phaseChanger();
            //if (!Reaper.ReaperMode)
            //{
            //    // if (NPC.ai[1] >= 5) shootIa((int)NpcChanges1.ExpertDamageScale(50, true), "Frozenp", player, 20f);
            //    //if (NPC.ai[1] == 115)
            //    //{
            //    //    shootIa((int)NpcChanges1.ExpertDamageScale(30, true), "Lazer", player, 20f);
            //    //    NPC.ai[1] = 0;
            //    //}
            //    NPC.ai[2]++;

            //}
            //else
            //{
            //  //  if (NPC.ai[1] >= 20) shootIa((int)NpcChanges1.ExpertDamageScale(180, true), "Frozenp", player, 40f);
            // //   if (NPC.ai[1] == 115) shootIa((int)NpcChanges1.ExpertDamageScale(230, true), "Lazer", player, 30f);
            //    NPC.ai[2]++;
            //}

            //if (NPC.ai[2] % 600 == 3)
            //{  //Npc spown rate  // 230 is projectile fire rate
            //   // NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, NPCID.IceElemental);
            //}
            //if (Main.expertMode)
            //{
            //   /* if (NPC.life < NPC.lifeMax / 2)
            //    {
            //        if (NPC.ai[2] % 900 == 1)
            //        {
            //            NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, NPCType<IGolem>());
            //        }
            //    }*/
            //}
            //if (NPC.ai[2] == 950)
            //{
            ////    NPC.ai[2] = 0;
            //}

            //NPC.ai[3]++;

            //if (NPC.life < NPC.lifeMax / 4)
            //{
            //    phaseChanger();
            //    fase3 = true;
            //}
            //if (Main.expertMode)
            //{

            //    if (fase3)
            //    {

            //        Music = MusicLoader.GetMusicSlot(Mod, "Sounds/Music/Frozen_Assaulter_p2");
            //        NPC.dontTakeDamage = true;

            //        if (NPC.ai[3] > 388) NPC.Center = Main.player[NPC.target].Center + new Vector2(Main.rand.Next(-250 * 2, 150 * 2), Main.rand.Next(-250 * 2, 150 * 2));
            //        if (invincibilityTimer <= 1304) invincibilityTimer++;
            //        if (invincibilityTimer >= 1300) NPC.dontTakeDamage = false;
            //        if (invincibilityTimer >= 1303)
            //        {
            //            if (!healAnimation)
            //            {
            //                if (!Reaper.ReaperMode)
            //                {
            //                    for (int i = NPC.lifeMax / 4; i <= NPC.lifeMax / 2; i++)
            //                    {
            //                        NPC.life = i;
            //                    }
            //                }
            //                else
            //                {
            //                    for (int i = NPC.lifeMax / 4; i <= NPC.lifeMax / 1.5; i++)
            //                    {
            //                        NPC.life = i;
            //                    }
            //                }
            //                healAnimation = true;
            //            }
            //        }

            //    }
            //    //npc.Center = new Vector2(Main.rand.Next(-5 * 1, 190 * 2), Main.rand.Next(-100 * 2, 200 * 2));}  
            //    if (NPC.ai[3] == 390)
            //    {
            //        NPC.ai[3] = 0;
            //    }
            //}




            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                Player target = Main.player[NPC.target];
                float distance = NPC.Distance(Main.player[NPC.target].Center);

                if (target.dead)
                {
                    NPC.EncourageDespawn(7);
                    return;
                }
                if (NPC.target < 0 || NPC.target == 255 || Main.player[NPC.target].dead || !Main.player[NPC.target].active)
                {
                    NPC.TargetClosest(true);
                }
                if (attackCounter > 0)
                {
                    attackCounter--; // tick down the attack counter.
                    phaseChanger();

                }
                if (!Reaper.ReaperMode)
                {
                    switch (attackCounter)
                    {
                        case 115:
                            shootIa((int)NpcChanges1.ExpertDamageScale(30, true), ProjectileID.FrostBeam, target, 20f,0.5,0.5);
                            break;
                        case 300:
                            NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, NPCID.IceElemental);
                            break;
                        case 850:
                            if (currentPhase >= 2 && (Main.expertMode || Main.masterMode))
                            {
                                NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, NPCType<IGolem>());
                            }
                            break;
                        case > 1:
                            shootIa((int)NpcChanges1.ExpertDamageScale(50, true), ProjectileType<Frozenp>(), target, 20f,0.5,0.5);
                            break;
                    }
                }
                else
                {
                    switch (attackCounter)
                    {
                        case 115:
                            shootIa((int)NpcChanges1.ExpertDamageScale(30, true), ProjectileID.FrostBeam, target, 30f,0.5,0.5);
                            break;
                        case 150:
                            NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, NPCID.IceElemental);
                            break;
                        case 600:
                            if (currentPhase >= 2 && (Main.expertMode || Main.masterMode))
                            {
                                NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, NPCType<IGolem>());
                            }
                            break;
                        case >= 0:
                            shootIa((int)NpcChanges1.ExpertDamageScale(50, true), ProjectileType<Frozenp>(), target, 70f, 2, 2);
                            shootIa((int)NpcChanges1.ExpertDamageScale(50, true), ProjectileType<Frozenp>(), target, 70f, -2, -2);
                            shootIa((int)NpcChanges1.ExpertDamageScale(50, true), ProjectileType<Frozenp>(), target, 70f, 5, 5);
                            shootIa((int)NpcChanges1.ExpertDamageScale(50, true), ProjectileType<Frozenp>(), target, 70f, -5, -5);
                            shootIa((int)NpcChanges1.ExpertDamageScale(50, true), ProjectileType<Frozenp>(), target, 70f, 50, 50);
                            shootIa((int)NpcChanges1.ExpertDamageScale(50, true), ProjectileType<Frozenp>(), target, 70f, -50, -50);
                            shootIa((int)NpcChanges1.ExpertDamageScale(50, true), ProjectileType<Frozenp>(), target, 70f, 100, 100);
                            shootIa((int)NpcChanges1.ExpertDamageScale(50, true), ProjectileType<Frozenp>(), target, 70f, -100, -100);
                            shootIa((int)NpcChanges1.ExpertDamageScale(50, true), ProjectileType<Frozenp>(), target, 70f, 150, 150);
                            shootIa((int)NpcChanges1.ExpertDamageScale(50, true), ProjectileType<Frozenp>(), target, 70f, -150, -150);
                            shootIa((int)NpcChanges1.ExpertDamageScale(50, true), ProjectileType<Frozenp>(), target, 70f, 200, 200);
                            shootIa((int)NpcChanges1.ExpertDamageScale(50, true), ProjectileType<Frozenp>(), target, 70f, -200, -200);
                            break;
                    }

                }                

                if (Main.expertMode)
                {
                    if(currentPhase == 3)
                    {
                        FrozenPhase3();
                    }

                }

                if (attackCounter <= 0 && Vector2.Distance(NPC.Center, target.Center) < 200 && Collision.CanHit(NPC.Center, 1, 1, target.Center, 1, 1))
                {
                    attackCounter = 900;
                    NPC.netUpdate = true;
                }
            }
        }
        public void shootIa(int dammage, int type, Player P, float Speed, double x, double y)
        {
            Vector2 vector8 = new Vector2(NPC.position.X + (NPC.width / 2), NPC.position.Y + (NPC.height / 2));
            float rotation = (float)Math.Atan2(vector8.Y - (P.position.Y + (P.height * x)), vector8.X - (P.position.X + (P.width * y)));
            Projectile.NewProjectile(NPC.GetSource_FromAI(), vector8.X, vector8.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1), type, 10, 0f, 0);
        }
        public void FrozenPhase3()
        {
            Music = MusicLoader.GetMusicSlot(Mod, "Sounds/Music/Frozen_Assaulter_p2");


            switch (invincibilityTimer)
            {
                case 0:
                    invincibilityTimer = 1304;
                    NPC.dontTakeDamage = true;
                    break;
                case 4:
                    NPC.dontTakeDamage = false;
                    invincibilityTimer--;
                    break;
                case 3:
                    if (!healAnimation)
                    {
                        if (Reaper.ReaperMode)
                        {
                            NPC.life = NPC.lifeMax / 2;
                        }
                        else
                        {
                            NPC.life = NPC.lifeMax / 3;
                        }
                        healAnimation = true;
                        invincibilityTimer--;
                    }
                    break;
                case > 1:
                    invincibilityTimer--;
                        break;
            }
        }
        public void phaseChanger()
        {
            if (NPC.life > NPC.lifeMax / 2)
            {
                currentPhase = 1;
            }
            else if (NPC.life < NPC.lifeMax / 2 && NPC.life > NPC.lifeMax / 4)
            {
                currentPhase = 2;
            }
            else if (NPC.life < NPC.lifeMax / 4)
            {
                if (!healAnimation)
                {
                    currentPhase = 3;
                }
                else currentPhase = 4;
            }
        }
        public override void BossLoot(ref string name, ref int potionType)
        {
            DownedBossSystem.downedFrozen = true;
            potionType = ItemID.GreaterHealingPotion;
            Item.NewItem(NPC.GetSource_Loot(), (int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, ItemID.IceBlock, 50);
        }
        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.NormalvsExpert(ItemType<FrostShark>(), 4, 999999999));
            npcLoot.Add(ItemDropRule.NormalvsExpert(ItemType<Permafrost>(), 4, 999999999));
            npcLoot.Add(ItemDropRule.NormalvsExpert(ItemType<FrozenStafff>(), 4, 999999999));
            npcLoot.Add(ItemDropRule.NormalvsExpert(ItemType<frozen_staff>(), 4, 999999999));
            npcLoot.Add(ItemDropRule.NormalvsExpert(ItemType<FrozenMask>(), 7, 999999999));

            npcLoot.Add(ItemDropRule.BossBag(ItemType<frostBag>()));
            npcLoot.Add(ItemDropRule.Common(ItemType<FrostTrophy>(), 10));
            npcLoot.Add(ItemDropRule.MasterModeCommonDrop(ItemType<Frozen_Relic>()));
        }

        private const int Frame_static = 0;
        private const int Frame_1 = 1;
        private const int Frame_2 = 2;
        private const int Frame_3 = 3;
        private const int Frame_Breack_0 = 4;
        private const int Frame_Breack_1 = 5;
        private const int Frame_Breack_2 = 6;
        private const int Frame_Breack_3 = 7;
        public override void FindFrame(int frameHeight)
        {
            NPC.spriteDirection = NPC.direction;

            if (NPC.life > NPC.lifeMax / 4)
            {
                switch (NPC.frameCounter)
                {
                    case < 3:
                        ChoiseFrame(Frame_static, frameHeight);
                        break;
                    case < 6:
                        ChoiseFrame(Frame_1, frameHeight);

                        break;
                    case < 9:
                        ChoiseFrame(Frame_2, frameHeight);
                        break;
                    case < 12:
                        ChoiseFrame(Frame_3, frameHeight);
                        break;
                    default:
                        NPC.frameCounter = 0;
                        break;
                }
            }

            else if (NPC.life < NPC.lifeMax / 4)
            {
                switch (NPC.frameCounter)
                {
                    case < 3:
                        ChoiseFrame(Frame_Breack_0, frameHeight);
                        break;
                    case < 5:
                        ChoiseFrame(Frame_Breack_1, frameHeight);
                        break;
                    case < 7:
                        ChoiseFrame(Frame_Breack_2, frameHeight);
                        break;
                    case < 9:
                        ChoiseFrame(Frame_Breack_3, frameHeight);
                        break;
                    default:
                        NPC.frameCounter = 0;
                        break;
                }
            }
        }
        public void ChoiseFrame(int frame, int frameHeight)
        {
            NPC.frame.Y = frame * frameHeight;
            NPC.frameCounter++;
        } 
    }
}
	

    
