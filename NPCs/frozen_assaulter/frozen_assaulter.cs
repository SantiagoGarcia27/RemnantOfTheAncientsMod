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
using RemnantOfTheAncientsMod.Items.Bloques.Relics;
using RemnantOfTheAncientsMod.Items.Armor.Masks;
using static Terraria.ModLoader.ModContent;
using Microsoft.Xna.Framework;
using RemnantOfTheAncientsMod.Projectiles;
using Terraria.GameContent.ItemDropRules;
using RemnantOfTheAncientsMod.Common.Systems;
using RemnantOfTheAncientsMod.VanillaChanges;
using RemnantOfTheAncientsMod.World;
using RemnantOfTheAncientsMod.Items.Bloques.Trophy;
using System.IO;
using RemnantOfTheAncientsMod.Projectiles.BossProjectile;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent;

namespace RemnantOfTheAncientsMod.NPCs.frozen_assaulter
{
    [AutoloadBossHead]
    public class frozen_assaulter : ModNPC
    {
        int invincibilityTimer = 0;
        bool healAnimation = false;
        int currentPhase = 1;
        float i = 0;
        int idelay = 5;
        int TpDelay = 20;
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
            NPC.lifeMax = (int)NpcChanges1.ExpertLifeScale(18500);   
            NPC.damage = (int)NpcChanges1.ExpertDamageScale(90); 
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
                            shootIa((int)NpcChanges1.ExpertDamageScale(50), ProjectileID.FrostBeam, target, 20f, 0.5, 0.5);
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
                        case >= 0:
                            shootIa((int)NpcChanges1.ExpertDamageScale(10), ProjectileType<Frozenp>(), target, 20f, 0.5, 0.5);
                            break;
                    }
                    if (Main.expertMode)
                    {
                        if (currentPhase == 3)
                        {
                            FrozenPhase3();
                        }
                        if (currentPhase == 4 && (attackCounter == 700 || attackCounter == 200))
                        {
                            frozenTp();
                        }
                    }
                }
                else
                {
                    CheckDistance(distance);

                    switch (attackCounter)
                    {
                        case 400:
                        case 115:
                            for (int i = 0; i < 6; i++)
                            {
                                shootIa((int)NpcChanges1.ExpertDamageScale(10), ProjectileID.FrostBeam, target, 30f, 0.5, 0.5);
                            }
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
                            int l = 0;
                            for (int j = 0; j < 3; j++)
                            {
                                shootIa((int)NpcChanges1.ExpertDamageScale(10), ProjectileType<Frozenp>(), target, 10f, 90f + l);//70
                                shootIa((int)NpcChanges1.ExpertDamageScale(10), ProjectileType<Frozenp>(), target, 10f, 180f + l);
                                shootIa((int)NpcChanges1.ExpertDamageScale(10), ProjectileType<Frozenp>(), target, 10f, 270f + l);
                            }
                            l += 2;
                            l %= 360;
                            break;
                    }


                    if (Main.expertMode)
                    {
                        if (currentPhase == 3)
                        {
                            FrozenPhase3();
                        }
                        if (currentPhase == 4 && (attackCounter == 700 || attackCounter == 200))
                        {
                            frozenTp();
                        }
                        if (attackCounter < 600 && attackCounter > 500 && currentPhase != 3)
                        {
                            shootIa((int)NpcChanges1.ExpertDamageScale(10), ProjectileID.FrostBeam, target, 30f, 0.5, 0.5);
                        }
                    }
                }



                if (attackCounter <= 0 && Vector2.Distance(NPC.Center, target.Center) < 200 && Collision.CanHit(NPC.Center, 1, 1, target.Center, 1, 1))
                {
                    attackCounter = 900;
                    NPC.netUpdate = true;
                }
            }
        }
        public void CheckDistance(float distance)
        {
            if(distance <= 60)
            {
                NPC.directionY = -NPC.oldDirectionY - 310;
            }
        }
        public void shootIa(int damage, int type, Player player, float speed, double x, double y)
        {
            Vector2 vector8 = new Vector2(NPC.position.X + (NPC.width / 2), NPC.position.Y + (NPC.height / 2));
            float rotation = (float)Math.Atan2(vector8.Y - (player.position.Y + (player.height * x)), vector8.X - (player.position.X + (player.width * y)));
            Vector2 direction;
            direction.X = (float)((Math.Cos(rotation) * speed) * -1);
            direction.Y = (float)((Math.Sin(rotation) * speed) * -1);
            int i = Projectile.NewProjectile(NPC.GetSource_FromAI(), vector8, direction, type, damage, 0f, 0);
            Main.projectile[i].timeLeft = 200;
        }

        public void shootIa(int damage, int type, Player player, float speed, float grades)
        {
            Vector2 vector8 = new Vector2(NPC.position.X + (NPC.width / 2), NPC.position.Y + (NPC.height / 2));
            Vector2 direction = Vector2.UnitX * speed;
            direction = direction.RotatedBy(grades);
            int i = Projectile.NewProjectile(NPC.GetSource_FromAI(), vector8, direction, type, damage, 0f, Main.myPlayer);
            Main.projectile[i].timeLeft = 200;
        }
        public void FrozenPhase3()
        {
            Music = MusicLoader.GetMusicSlot(Mod, "Sounds/Music/Frozen_Assaulter_p2");


            switch (invincibilityTimer)
            {
                case 0:
                    NPC.defDefense = 9999;
                    invincibilityTimer = 1304;   
                    break;
                case 4:
                    NPC.defDefense = 15;
                    invincibilityTimer--;
                    break;
                case 3 :
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
        public void frozenTp()
        {

            Vector2 position = Main.player[NPC.target].Center + new Vector2(Main.rand.Next(-250 * 2, 150 * 2), Main.rand.Next(-250 * 2, 150 * 2));
            int projectile = Projectile.NewProjectile(NPC.GetSource_FromAI(), position, new Vector2(0, 0), ProjectileType<BossMark>(), 0, 0f, 0);
            new BossMark().texture = "RemnantOfTheAncientsMod/Items/Core/Frost_core";

            if (TpDelay == 0)
            {
                NPC.position = position;
                TpDelay = 20;
            }
            else
            {
                TpDelay--;
            }
        }
        public void phaseChanger()
        {
            currentPhase = !healAnimation && NPC.life < NPC.lifeMax / 4 ? 3 :
                NPC.life > NPC.lifeMax / 2 ? 1 :
                NPC.life > NPC.lifeMax / 4 ? 2 :
                4;
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
        public override void PostDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            if (NPC.defDefense == 9999)
            {
                SpriteEffects effects = SpriteEffects.None;
                if (NPC.spriteDirection == 1)
                {
                    effects = SpriteEffects.FlipHorizontally;
                }
                Vector2 vectorFrame = new Vector2(TextureAssets.Npc[NPC.type].Value.Width / 2, TextureAssets.Npc[NPC.type].Value.Height / Main.npcFrameCount[NPC.type] / 2);
                Vector2 position = new Vector2(NPC.Center.X, NPC.Center.Y) - (Main.screenPosition - new Vector2(0, -45));
                var a = Request<Texture2D>("RemnantOfTheAncientsMod/NPCs/frozen_assaulter/frozen_assaulter_Shield");
                position -= new Vector2(a.Width(), a.Height() / Main.npcFrameCount[NPC.type]) * 1f / 2f;
                position += vectorFrame + new Vector2(0f, 4f + NPC.gfxOffY);
                Color color = new Color(147, 219, 252,150);
                Main.spriteBatch.Draw((Texture2D)a, position, null, color, NPC.rotation, vectorFrame, 1f, effects, 0f);    
            }
        }
        public void ChoiseFrame(int frame, int frameHeight)
        {
            NPC.frame.Y = frame * frameHeight;
            NPC.frameCounter++;
        } 
    }
}
	

    
