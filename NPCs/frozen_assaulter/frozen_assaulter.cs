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

namespace RemnantOfTheAncientsMod.NPCs.frozen_assaulter
{
    [AutoloadBossHead]
    public class frozen_assaulter : ModNPC
    {
        int invincibilityTimer = 1;
        bool healAnimation = false;
        bool fase3 = false;
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

        public override void AI()
        {
            Player P = Main.player[NPC.target];
            float distance = NPC.Distance(Main.player[NPC.target].Center);

            if (NPC.target < 0 || NPC.target == 255 || Main.player[NPC.target].dead || !Main.player[NPC.target].active) NPC.TargetClosest(true);
            NPC.netUpdate = true;
            NPC.ai[0]++;

            NPC.ai[1]++;
            phaseChanger();
            if (!Reaper.ReaperMode)
            {
                if (NPC.ai[1] >= 5) shootIa((int)NpcChanges1.ExpertDamageScale(50, true), "Frozenp", P, 20f);
                if (NPC.ai[1] >= 115)
                {
                    shootIa((int)NpcChanges1.ExpertDamageScale(30, true), "Lazer", P, 20f);
                    NPC.ai[1] = 0;
                }
                NPC.ai[2]++;

            }
            else
            {
                if (NPC.ai[1] >= 20) shootIa((int)NpcChanges1.ExpertDamageScale(180, true), "Frozenp", P, 40f);
                if (NPC.ai[1] >= 115) shootIa((int)NpcChanges1.ExpertDamageScale(230, true), "Lazer", P, 30f);
                NPC.ai[2]++;
            }

            if (NPC.ai[2] % 600 == 3)
            {  //Npc spown rate  // 230 is projectile fire rate
                NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, NPCID.IceElemental);
            }
            if (Main.expertMode)
            {
                if (NPC.life < NPC.lifeMax / 2)
                {
                    if (NPC.ai[2] % 900 == 1)
                    {
                        NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, NPCType<IGolem>());
                    }
                }
            }
            if (NPC.ai[2] == 950)
            {
                NPC.ai[2] = 0;
            }

            NPC.ai[3]++;

            if (NPC.life < NPC.lifeMax / 4)
            {
                phaseChanger();
                fase3 = true;
            }
            if (Main.expertMode)
            {

                if (fase3)
                {

                    Music = MusicLoader.GetMusicSlot(Mod, "Sounds/Music/Frozen_Assaulter_p2");
                    NPC.dontTakeDamage = true;

                    if (NPC.ai[3] > 388) NPC.Center = Main.player[NPC.target].Center + new Vector2(Main.rand.Next(-250 * 2, 150 * 2), Main.rand.Next(-250 * 2, 150 * 2));
                    if (invincibilityTimer <= 1304) invincibilityTimer++;
                    if (invincibilityTimer >= 1300) NPC.dontTakeDamage = false;
                    if (invincibilityTimer >= 1303)
                    {
                        if (!healAnimation)
                        {
                            if (!Reaper.ReaperMode)
                            {
                                for (int i = NPC.lifeMax / 4; i <= NPC.lifeMax / 2; i++)
                                {
                                    NPC.life = i;
                                }
                            }
                            else
                            {
                                for (int i = NPC.lifeMax / 4; i <= NPC.lifeMax / 1.5; i++)
                                {
                                    NPC.life = i;
                                }
                            }
                            healAnimation = true;
                        }
                    }

                }
                //npc.Center = new Vector2(Main.rand.Next(-5 * 1, 190 * 2), Main.rand.Next(-100 * 2, 200 * 2));}  
                if (NPC.ai[3] == 390)
                {
                    NPC.ai[3] = 0;
                }
            }
        }
        public void shootIa(int dammage, string tipo, Player P, float speed)
        {
            if (tipo == "Frozenp")
            {
                float Speed = speed;  
                int damage = dammage; 
                Vector2 vector8 = new Vector2(NPC.position.X + (NPC.width / 2), NPC.position.Y + (NPC.height / 2));
                int type = ModContent.ProjectileType<Frozenp>();  
                float rotation = (float)Math.Atan2(vector8.Y - (P.position.Y + (P.height * 0.5f)), vector8.X - (P.position.X + (P.width * 0.5f)));
                int num54 = Projectile.NewProjectile(NPC.GetSource_FromAI(), vector8.X, vector8.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1), type, 10, 0f, 0);
            }
            else if (tipo == "Lazer")
            {
                float Speed = speed;  
                Vector2 vector8 = new Vector2(NPC.position.X + (NPC.width / 2), NPC.position.Y + (NPC.height / 2));
                int damage = dammage; 
                int type = ProjectileID.FrostBeam; 
                float rotation = (float)Math.Atan2(vector8.Y - (P.position.Y + (P.height * 0.5f)), vector8.X - (P.position.X + (P.width * 0.5f)));
                int num54 = Projectile.NewProjectile(NPC.GetSource_FromAI(), vector8.X, vector8.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1), type, 10, 0f, 0);
            }
        }
        public void phaseChanger()
        {
            if (NPC.life > NPC.lifeMax / 2) currentPhase = 1;
            else if (NPC.life < NPC.lifeMax / 2 && !fase3) currentPhase = 2;
            else if (NPC.life < NPC.lifeMax / 4)
            {
                if (!healAnimation)
                {
                    currentPhase = 3;
                    fase3 = true;
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
	

    
