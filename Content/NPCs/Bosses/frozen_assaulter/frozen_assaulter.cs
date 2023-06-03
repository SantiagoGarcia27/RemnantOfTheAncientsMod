using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using RemnantOfTheAncientsMod.Content.Items.Consumables.tresure_bag;
using RemnantOfTheAncientsMod.Content.Items.Weapons.Melee;
using RemnantOfTheAncientsMod.Content.Items.Weapons.Ranger;
using RemnantOfTheAncientsMod.Content.Items.Weapons.Magic;
using RemnantOfTheAncientsMod.Content.Items.Weapons.Summon;
using RemnantOfTheAncientsMod.Content.Items.Placeables.Relics;
using static Terraria.ModLoader.ModContent;
using Microsoft.Xna.Framework;
using RemnantOfTheAncientsMod.Content.Projectiles;
using Terraria.GameContent.ItemDropRules;
using RemnantOfTheAncientsMod.Common.Systems;
using RemnantOfTheAncientsMod.World;
using RemnantOfTheAncientsMod.Content.Items.Placeables.Trophy;
using System.IO;
using RemnantOfTheAncientsMod.Content.Projectiles.BossProjectile;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent;
using Terraria.DataStructures;
using RemnantOfTheAncientsMod.Common.Global;
using RemnantOfTheAncientsMod.Content.Items.Armor.Masks;

namespace RemnantOfTheAncientsMod.Content.NPCs.Bosses.frozen_assaulter
{
    [AutoloadBossHead]
    public class frozen_assaulter : ModNPC
    {
        int invincibilityTimer = 0;
        bool healAnimation = false;
        int currentPhase = 1;
        int MaxPlayers = RemnantOfTheAncientsMod.MaxPlayerOnline() / 2;
        int TpDelay = 20;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Frozen Assaulter");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Asaltante Congelado");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Agresseur gelé");
            Main.npcFrameCount[NPC.type] = 8;
            NPCID.Sets.MPAllowedEnemies[Type] = true;
            NPCID.Sets.BossBestiaryPriority.Add(Type);
        }
        public override void SetDefaults()
        {
            NPC.aiStyle = 5;
            NPC.lifeMax = 18500;//(int)NpcChanges1.ExpertLifeScale(18500); //* (int)ModContent.GetInstance<ConfigClient1>().xdlevel; 
            NPC.damage = 90;// (int)NpcChanges1.ExpertDamageScale(90); 
            NPC.defense = 15;
            NPC.knockBackResist = 0f;
            NPC.width = 100;
            NPC.height = 100;
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
        float grades = 0;
        float delay = 2;
        public override void AI()
        {
            Player target = Main.player[NPC.target];
            float distance = NPC.Distance(Main.player[NPC.target].Center);
            PhaseChanger();
            setAttackCounter(target);

            if (target.dead)
            {
                NPC.EncourageDespawn(7);
                return;
            }
            if (NPC.target < 0 || NPC.target == 255 || Main.player[NPC.target].dead || !Main.player[NPC.target].active)
            {
                NPC.TargetClosest(true);
            }
            shootAi(target, Reaper.ReaperMode);
            summonAi(Reaper.ReaperMode);
            CheckDistance(distance);

            if (Main.expertMode)
            {
                if (attackCounter < 600 && attackCounter > 500 && currentPhase != 3)
                {
                    shootIa((int)NpcChanges1.ExpertDamageScale(10), ProjectileID.FrostBeam, target, 30f, 0.5, 0.5);
                }
            }
            if (Main.expertMode)
            {
                checkPhase();
            }
        }
        private void summonAi(bool isReaper)
        {  
            int conter1 = isReaper? 150 : 300;
            if(attackCounter == conter1) NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, NPCID.IceElemental);
            if (attackCounter == 600 && currentPhase >= 2 && (Main.expertMode || Main.masterMode))
            {
                NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, NPCType<IGolem>());
            }
        }
        private void shootAi(Player target, bool isReaper)
        {
            switch (attackCounter)
            {
                case 400:
                    if (isReaper)
                    {
                        for (int i = 0; i < 6; i++)
                        {
                            shootIa(10, ProjectileID.FrostBeam, target, 30f, 0.5, 0.5);
                        }
                    }
                    break;
                case 115:
                    if (isReaper)
                    {
                        for (int i = 0; i < 6; i++)
                        {
                            shootIa(10, ProjectileID.FrostBeam, target, 30f, 0.5, 0.5);
                        }
                    }
                    else
                    {
                        shootIa(50, ProjectileID.FrostBeam, target, 20f, 0.5, 0.5);
                    }
                    break;
                case >= 0:
                    if (currentPhase != 3)
                    {
                        if (!isReaper)
                        {

                            if (currentPhase > 2) shootIa(10, ProjectileType<Frozenp>(), target, -20f, 0.5, 0.5);
                            for (int i = -1; i < MaxPlayers; i++)
                            {
                                int a = i == -1? Main.myPlayer: i>1 ? i-1 : i;
                                shootIa(10, ProjectileType<Frozenp>(), Main.player[a], 20f, 0.5, 0.5);
                            }
                        }
                        else
                        {
                            if (++delay >= 3)
                            {
                                for (int j = 0; j < 3; j++)
                                {
                                    shootIa(10, ProjectileType<Frozenp>(), target, 7f, 360f + grades);//70
                                    shootIa(10, ProjectileType<Frozenp>(), target, 7f, -120f + grades);
                                    shootIa(10, ProjectileType<Frozenp>(), target, 7f, 120f + grades);
                                    shootIa(10, ProjectileType<Frozenp>(), target, 7f, -360f + grades);
                                }
                                grades = (grades <= 360) ? (grades + 0.01f) : 0;
                                delay = 0;
                            }
                        }
                    }
                    break;

            }
        }
        private void setAttackCounter(Player target)
        {
            if (attackCounter > 0)
            {
                attackCounter--;
            }
            if (attackCounter <= 0 && Vector2.Distance(NPC.Center, target.Center) < 200 && Collision.CanHit(NPC.Center, 1, 1, target.Center, 1, 1))
            {
                attackCounter = 900;
                NPC.netUpdate = true;
            }
        }
        private void checkPhase()
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
        public override void HitEffect(int hitDirection, double damage)
        {
            if (NPC.life <= 0)
            {
                if (Main.netMode != NetmodeID.Server)
                {
                    Gore.NewGore(NPC.GetSource_Death(), NPC.position, new Vector2(Main.rand.Next(-6, 7), Main.rand.Next(-6, 7)), Mod.Find<ModGore>("frozen_assaulterFragment1").Type, NPC.scale);
                    Gore.NewGore(NPC.GetSource_Death(), NPC.position, new Vector2(Main.rand.Next(-6, 7), Main.rand.Next(-6, 7)), Mod.Find<ModGore>("frozen_assaulterFragment2").Type, NPC.scale);
                    Gore.NewGore(NPC.GetSource_Death(), NPC.position, new Vector2(Main.rand.Next(-6, 7), Main.rand.Next(-6, 7)), Mod.Find<ModGore>("frozen_assaulterFragment3").Type, NPC.scale);
                    Gore.NewGore(NPC.GetSource_Death(), NPC.position, new Vector2(Main.rand.Next(-6, 7), Main.rand.Next(-6, 7)), Mod.Find<ModGore>("frozen_assaulterFragment4").Type, NPC.scale);
                    Gore.NewGore(NPC.GetSource_Death(), NPC.position, new Vector2(Main.rand.Next(-6, 7), Main.rand.Next(-6, 7)), Mod.Find<ModGore>("frozen_assaulterFragment5").Type, NPC.scale);
                }
                for (int j = 0; j < 10; j++)
                {
                    Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.Blood, hitDirection, -1f);
                }
            }
            PhaseChanger();
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
        public void PhaseChanger()
        {
            if (NPC.life >= (NPC.lifeMax / 2)) currentPhase = 1;
            else if (NPC.life <= (NPC.lifeMax / 4) && healAnimation) currentPhase = 4;
            else if (NPC.life <= (NPC.lifeMax / 4) && !healAnimation) currentPhase = 3;
            else if (NPC.life <= (NPC.lifeMax / 2)) currentPhase = 2;
        }
        public override void BossLoot(ref string name, ref int potionType)
        {
            RemnantOfTheAncientsMod.MaxPlayers = 0;
            RemnantDownedBossSystem.downedFrozen = true;
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
            npcLoot.Add(ItemDropRule.NormalvsExpert(ItemID.FrostCore, 5, 3));
            npcLoot.Add(ItemDropRule.BossBag(ItemType<frostBag>()));
            npcLoot.Add(ItemDropRule.Common(ItemType<FrostTrophy>(), 10));
            npcLoot.Add(ItemDropRule.MasterModeCommonDrop(ItemType<Frozen_Relic>()));

            if (RemnantOfTheAncientsMod.CalamityMod != null) CalamityDrop(npcLoot);
        }

        [JITWhenModsEnabled("CalamityMod")]
        private void CalamityDrop(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(Utils1.GetItemFromMod(RemnantOfTheAncientsMod.CalamityMod, "EssenceofEleum"), 1,2,Utils1.ReaperDropScaler(5)));
        } 
        public override void OnSpawn(IEntitySource source)
        {
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
                var a = Request<Texture2D>("RemnantOfTheAncientsMod/Content/NPCs/Bosses/frozen_assaulter/frozen_assaulter_Shield");
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
	

    
