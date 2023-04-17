using CalamityMod;
using CalamityMod.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RemnantOfTheAncientsMod.Common.Systems;
using RemnantOfTheAncientsMod.Items.Armor.Masks;
using RemnantOfTheAncientsMod.Items.Bloques.Relics;
using RemnantOfTheAncientsMod.Items.Bloques.Trophy;
using RemnantOfTheAncientsMod.Items.Magic;
using RemnantOfTheAncientsMod.Items.Mele.saber;
using RemnantOfTheAncientsMod.Items.Ranger.Rep;
using RemnantOfTheAncientsMod.Items.tresure_bag;
using RemnantOfTheAncientsMod.Projectiles;
using RemnantOfTheAncientsMod.VanillaChanges;
using RemnantOfTheAncientsMod.World;
using System;
using System.IO;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.NPCs.ITyrant
{
    public class GenericVariables
    {
        public static int SpawnCounter = 0;
        public static int TimeInmune = 200;
        public static bool IsSpawned = false;
    }
    public static class BaseStats 
    {
       public static int LifeMax = SetMaxLife(30000);

        private static int SetMaxLife(int life)
        {
            return (int)NpcChanges1.ExpertLifeScale(life);
        }
        
    }
    [AutoloadBossHead]
    internal class InfernalTyrantHead : WormHead
    {
        public override int BodyType => ModContent.NPCType<InfernalTyrantBody>();

        public override int TailType => ModContent.NPCType<InfernalTyrantTail>();

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Infernal Tyrant");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Tirano infernal");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Tyran infernal");

            var drawModifier = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
            {
                CustomTexturePath = "RemnantOfTheAncientsMod/NPCs/ITyrant/InfernalTyrantHead_Bestiary", 
                Position = new Vector2(40f, 24f),
                PortraitPositionXOverride = 0f,
                PortraitPositionYOverride = 12f
            };
            NPCID.Sets.NPCBestiaryDrawOffset.Add(NPC.type, drawModifier);
        }
   
        public bool head;
        public bool CanTp = false;
        public override void SetDefaults()
        {
            NPC.CloneDefaults(NPCID.DiggerHead);
            NPC.aiStyle = -1;
            NPC.width = 30;//105
            NPC.height = 30;//103
            NPC.boss = true;
            NPC.lifeMax = BaseStats.LifeMax;// * (int)ModContent.GetInstance<ConfigClient1>().xdlevel;
            if (RemnantOfTheAncientsMod.CalamityMod != null) SetDefautsCalamity();
            NPC.damage = (int)NpcChanges1.ExpertDamageScale(200);
            NPC.defense = TyranStats.TyrantArmor(999, ModContent.GetModNPC(ModContent.NPCType<InfernalTyrantHead>()).NPC);
            NPC.scale = 2.50f;
            NPC.npcSlots = 20f;
            NPC.lavaImmune = true;
            Music = MusicLoader.GetMusicSlot(Mod, "Sounds/Music/Infernal_Tyrant");
        }
        [JITWhenModsEnabled("CalamityMod")]
        public void SetDefautsCalamity()
        {
            NPC.Calamity().canBreakPlayerDefense = true;
            NPC.Calamity().VulnerableToHeat = false;
            NPC.Calamity().VulnerableToCold = false;
            NPC.Calamity().VulnerableToSickness = true;
            SetNpcDamageReductionCalamity(0.02f,0.22f,0.29f,0.3f);
            CalamityLifeScale(BaseStats.LifeMax);
        }
        [JITWhenModsEnabled("CalamityMod")]
        public void CalamityLifeScale(int life)
        {
            life = (int)NpcChanges1.ExpertLifeScale(life);
            NPC.LifeMaxNERB(life, (int)(life * 1.5), (int)(life * 0.8));
        }
        [JITWhenModsEnabled("CalamityMod")]
        public void SetNpcDamageReductionCalamity(float normal, float revenge, float death, float bossrush)
        {
            NPC.DR_NERD(normal, revenge, death, bossrush);
        }
        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
            {
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheUnderworld,
                new FlavorTextBestiaryInfoElement("A great and dreaded worm rules the underworld with an iron fist and his flames, powerful and majestic in equal parts, maintain the order and warmth of the underworld.")
            });
        }

        public override void Init()
        {
            MinSegmentLength = 15;
            MaxSegmentLength = RemnantOfTheAncientsMod.CalamityMod != null? MaxSegmentCount(MinSegmentLength, RemnantOfTheAncientsMod.CalamityMod) : MaxSegmentCount(MinSegmentLength);
            head = true;
            CommonWormInit(this);
        }
        internal static void CommonWormInit(Worm worm)
        {
            worm.MoveSpeed = 30f;
            worm.Acceleration = 0.245f;
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
        [JITWhenModsEnabled("CalamityMod")]
        private int MaxSegmentCount(int MinSegmentLength, Mod calamity) 
        {
            int scale = 0;
            if (CalamityWorld.death) scale = 20;
            else if (CalamityWorld.revenge) scale = 10;
            else if (Main.masterMode) scale = 5;
            else if (Main.expertMode) scale = 2;
            return MinSegmentLength + scale;
        }
        private int MaxSegmentCount(int MinSegmentLength)
        {
            int scale = 0;
            if (Main.masterMode) scale = 5;
            else if (Main.expertMode) scale = 2;

            return MinSegmentLength + scale;
        }
        public override void AI()
        {
            GenericVariables gv = new GenericVariables();
            if (attackCounter > 0)
            {
                attackCounter--;
            }

            Player target = Main.player[NPC.target];
            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                DespawnSafeCheck(target, this);
            }
            DoAttacks(target);

            if (attackCounter <= 0 && Vector2.Distance(NPC.Center, target.Center) < 200 && Collision.CanHit(NPC.Center, 1, 1, target.Center, 1, 1))
            {
                attackCounter = 700;
                NPC.netUpdate = true;
            }
            if (RemnantOfTheAncientsMod.CalamityMod != null)
            {
                if (GenericVariables.SpawnCounter >= GenericVariables.TimeInmune)
                {
                    SetDefautsCalamity();
                    GenericVariables.IsSpawned = true;
                }
                else
                {
                    if (!GenericVariables.IsSpawned)
                    {
                        GenericVariables.SpawnCounter++;
                        SetNpcDamageReductionCalamity(1f, 1f, 1f, 1f);
                    }
                }
            }
            LifeSpeed(this);
        }
        public override void OnSpawn(IEntitySource source)
        {
            GenericVariables.IsSpawned = false;
            GenericVariables.SpawnCounter = 0;
        }
        private void DoAttacks(Player target)
        {
            switch (attackCounter)
            {
                case 200:
                    SpikeIa((int)NpcChanges1.ExpertDamageScale(40), false, -3, -3);
                    break;
                case 250:
                    FireBallIa(12f, (int)NpcChanges1.ExpertDamageScale(70), ModContent.ProjectileType<InfernalBallF>(), "*", 4, 3, target, 0f);
                    FireBallIa(12f, (int)NpcChanges1.ExpertDamageScale(70), ModContent.ProjectileType<InfernalBallF>(), "/", 4, 3, target, 0f);
                    break;
                case 300:
                    SummonIa(NPCID.Demon);
                    break;
                case 400:
                    SpikeIa((int)NpcChanges1.ExpertDamageScale(90), true, 3, -3);
                    break;
                case 430:
                    SpikeIa((int)NpcChanges1.ExpertDamageScale(40), false, 3, -3);
                    break;
                case 600:
                    for (int i = -2; i <= 3; i++)
                        FireBallIa(12f, (int)NpcChanges1.ExpertDamageScale(30), ModContent.ProjectileType<InfernalBall>(), "*", i - 1, i + 2, target, 0);
                    break;
                case 700:
                    SummonIa(NPCID.RedDevil);
                    break;
            }
        }
        public void FindTarget()
        {
            if (NPC.target < 0 || NPC.target == 255 || Main.player[NPC.target].dead)
            {
                if (Main.netMode != NetmodeID.Server)
                {
                    if (AllPlayersDead())
                    {
                        NPC.TargetClosest(true);
                        NPC.velocity *= 0.1f;
                    }
                }
                else
                {
                    if (AllPlayersDead())
                    {
                        NPC.TargetClosest(true);
                        NPC.velocity *= 0.1f;
                        
                    }
                }
            }
        }
        public void DespawnSafeCheck(Player player, Worm worm)
        {
           
            if ((!NPC.WithinRange(player.Center, 170 * 16f) || NPC.position.Y == Main.miniMapY - (10f * 16f))&& !player.dead)
            {
                Vector2 directionToPlayer = Vector2.Normalize(player.Center - NPC.Center);
                NPC.velocity = directionToPlayer * worm.MoveSpeed/4;
                NPC.netUpdate = true;
            }
        }
        public void FireBallIa(float Speed, int damage, int type, string signo1, int cordx, int cordy, Player player, float grades)
        {
            Vector2 vector8 = new Vector2(NPC.position.X + (NPC.width * cordx), NPC.position.Y + (NPC.height * cordy));
            if (signo1 == "/")
                vector8 = new Vector2(NPC.position.X + (NPC.width / cordx), NPC.position.Y + (NPC.height / cordy));

           // int type = ProjectileType<InfernalBallF>();
            SoundEngine.PlaySound(SoundID.DD2_BetsyFireballShot, NPC.Center);

            Vector2 direction = (player.Center - NPC.Center).SafeNormalize(Vector2.UnitX);
            direction = direction.RotatedBy(grades);

            //float rotation = (float)Math.Atan2(vector8.Y - (Main.player[NPC.target].position.Y + (Main.player[NPC.target].height * 0.5f)), vector8.X - (Main.player[NPC.target].position.X + (Main.player[NPC.target].width * 0.5f)));
            int projectile = Projectile.NewProjectile(NPC.GetSource_FromAI(), vector8, direction * 12, type, damage, 0f, 0);
            Main.projectile[projectile].timeLeft = 300;
        }

        public void SpikeIa(int damage, bool isStrong, int cordx, int cordy)
        {
            int type = isStrong ? ModContent.ProjectileType<InfernalSpikeF>() : ModContent.ProjectileType<InfernalSpike>();
            Vector2 spawnPosition = NPC.Center + new Vector2(cordx * NPC.width, cordy * NPC.height);
            SoundEngine.PlaySound(SoundID.DD2_BetsyFireballShot, NPC.Center);
            int projectile = Projectile.NewProjectile(NPC.GetSource_FromAI(),spawnPosition, Vector2.Zero, type, damage, 0f, Main.myPlayer);
            Main.projectile[projectile].timeLeft = 1500;
        }
        public void SummonIa(int Npc) => NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, Npc);
        float moveSpeed = 30f;
        public void LifeSpeed(Worm worm)
        {
            int maxLife = NPC.lifeMax;
            NPC.defense = TyranStats.TyrantArmor(999, ModContent.GetModNPC(ModContent.NPCType<InfernalTyrantHead>()).NPC);

            if (!AllPlayersDead())
            {
                if (GetLifePorcenage() < 5f && Reaper.ReaperMode)
                {
                    if (Main.netMode != 1)
                    {
                        worm.Acceleration = 1.5f;
                    }
                    else
                    {
                        worm.Acceleration = 1.2f;
                    }

                }
                else if (GetLifePorcenage() < 10f)
                {
                    worm.Acceleration = 1.1f;
                }
                else
                {
                    worm.Acceleration = 0.15f;

                    if (GetLifePorcenage() < 25f)
                    {
                        moveSpeed = Main.netMode != 1 ? 70f : 60f;
                    }
                    else if (GetLifePorcenage() < 50f)
                    {
                        moveSpeed = Main.netMode != 1 ? 50f : 40f;
                    }
                    worm.MoveSpeed = moveSpeed;
                }
            }
            else
            {
                worm.MoveSpeed = 10f;
                worm.Acceleration = 1f;
            }
        }
        public float GetLifePorcenage()
        {
            float i = (int)Math.Round((double)(100 * NPC.life) / NPC.lifeMax);//(NPC.lifeMax / NPC.life) * 100;
            return i;
        }
        public bool AllPlayersDead()
        {
            bool allPlayersDead = true;

            foreach (Player player in Main.player)
            {
                if (!player.dead)
                {
                    allPlayersDead = false;
                    break;
                }
            }
            return allPlayersDead;
        }
        public override void HitEffect(int hitDirection, double damage)
        {
            if (NPC.life <= 0)
            {
                if (Main.netMode != NetmodeID.Server)
                {
                    Gore.NewGore(NPC.GetSource_Death(), NPC.position, new Vector2(Main.rand.Next(-6, 7), Main.rand.Next(-6, 7)), Mod.Find<ModGore>("InfernalTyrantHeadGore1").Type, NPC.scale);
                    Gore.NewGore(NPC.GetSource_Death(), NPC.position, new Vector2(Main.rand.Next(-6, 7), Main.rand.Next(-6, 7)), Mod.Find<ModGore>("InfernalTyrantHeadGore2").Type, NPC.scale);
                }
                for (int j = 0; j < 10; j++)
                {
                    Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.Blood, hitDirection, -1f);
                }
            }
        }
        
        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position) => head ? null : false;
        public override void BossLoot(ref string name, ref int potionType)
        {
            RemnantDownedBossSystem.downedTyrant = true;
            potionType = ItemID.GreaterHealingPotion;
        }
        public override void PostDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            SpriteEffects effects = SpriteEffects.None;
            if (NPC.spriteDirection == 1)
            {
                effects = SpriteEffects.FlipHorizontally;
            }
            Vector2 vector = new Vector2(NPC.Center.X, NPC.Center.Y);
            Vector2 vector2 = new Vector2(TextureAssets.Npc[NPC.type].Value.Width / 2, TextureAssets.Npc[NPC.type].Value.Height / Main.npcFrameCount[NPC.type] / 2);
            Vector2 position = vector - Main.screenPosition;
            var a = ModContent.Request<Texture2D>("RemnantOfTheAncientsMod/NPCs/ITyrant/InfernalTyrantHead_Gow");
            position -= new Vector2(a.Width(), a.Height() / Main.npcFrameCount[NPC.type]) * 1f / 2f;
            position += vector2 * 1f + new Vector2(0f, 4f + NPC.gfxOffY);
            Color color = Utils.MultiplyRGBA(new Color(127 - NPC.alpha, 127 - NPC.alpha, 127 - NPC.alpha, 0), Color.LightYellow);
            Main.spriteBatch.Draw((Texture2D)a, position, NPC.frame, color, NPC.rotation, vector2, NPC.scale, effects, 0f);
        }
        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.NormalvsExpert(ModContent.ItemType<Spike_saber>(), 3, 999999999));
            npcLoot.Add(ItemDropRule.NormalvsExpert(ModContent.ItemType<Tyrant_repeater>(), 3, 999999999));
            npcLoot.Add(ItemDropRule.NormalvsExpert(ModContent.ItemType<Tyran_Blast>(), 3, 999999999));
            npcLoot.Add(ItemDropRule.NormalvsExpert(ModContent.ItemType<InfernalMask>(), 7, 999999999));
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<InfernalTrophy>(), 10));
            npcLoot.Add(ItemDropRule.BossBag(ModContent.ItemType<infernalBag>()));
            npcLoot.Add(ItemDropRule.MasterModeCommonDrop(ModContent.ItemType<Tyrant_Relic>()));
        }

    }

    internal class InfernalTyrantBody : WormBody
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Infernal Tyrant");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Tirano infernal");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Tyran infernal");

            NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
            {
                Hide = true // Hides this NPC from the Bestiary, useful for multi-part NPCs whom you only want one entry.
            };
            NPCID.Sets.NPCBestiaryDrawOffset.Add(NPC.type, value);
        }

        public override void SetDefaults()
        {

            NPC.CloneDefaults(NPCID.DiggerBody);
            NPC.lifeMax = BaseStats.LifeMax;
            if (RemnantOfTheAncientsMod.CalamityMod != null) SetDefautsCalamity();
            NPC.defense = TyranStats.TyrantArmor(999,ModContent.GetModNPC(ModContent.NPCType<InfernalTyrantBody>()).NPC);//999
            NPC.aiStyle = -1;
            NPC.width = 30;//105
            NPC.height = 30;//103
            NPC.scale = 2.50f;
            NPC.boss = true;
        }
        [JITWhenModsEnabled("CalamityMod")]
        public void SetDefautsCalamity()
        {
            NPC.Calamity().VulnerableToHeat = false;
            NPC.Calamity().VulnerableToCold = false;
            NPC.Calamity().VulnerableToSickness = true;
            NPC.Calamity().canBreakPlayerDefense = true;
            SetNpcDamageReductionCalamity(0.2f, 0.42f, 0.59f, 0.6f);
            CalamityLifeScale(BaseStats.LifeMax);
        }
        [JITWhenModsEnabled("CalamityMod")]
        public void CalamityLifeScale(int life)
        {
            life = (int)NpcChanges1.ExpertLifeScale(life);
            NPC.LifeMaxNERB(life, (int)(life * 1.5), (int)(life * 0.8));
        }
        [JITWhenModsEnabled("CalamityMod")]
        public void SetNpcDamageReductionCalamity(float normal, float revenge, float death, float bossrush)
        {
            NPC.DR_NERD(normal, revenge, death, bossrush);
        }
        public override void PostDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            SpriteEffects effects = SpriteEffects.None;
            if (NPC.spriteDirection == 1)
            {
                effects = SpriteEffects.FlipHorizontally;
            }
            Vector2 vector = new Vector2(NPC.Center.X, NPC.Center.Y);
            Vector2 vector2 = new Vector2(TextureAssets.Npc[NPC.type].Value.Width / 2, TextureAssets.Npc[NPC.type].Value.Height / Main.npcFrameCount[NPC.type] / 2);
            Vector2 position = vector - Main.screenPosition;
            var a = ModContent.Request<Texture2D>("RemnantOfTheAncientsMod/NPCs/ITyrant/InfernalTyrantBody_Glow");
            position -= new Vector2(a.Width(), a.Height() / Main.npcFrameCount[NPC.type]) * 1f / 2f;
            position += vector2 * 1f + new Vector2(0f, 4f + NPC.gfxOffY);
            Color color = Utils.MultiplyRGBA(new Color(127 - NPC.alpha, 127 - NPC.alpha, 127 - NPC.alpha, 0), Color.LightYellow);
            Main.spriteBatch.Draw((Texture2D)a, position, NPC.frame, color, NPC.rotation, vector2, NPC.scale, effects, 0f);
        }
        public override void HitEffect(int hitDirection, double damage)
        {
            if (NPC.life <= 0)
            {
                if (Main.netMode != NetmodeID.Server)
                {
                    Gore.NewGore(NPC.GetSource_Death(), NPC.position, new Vector2(Main.rand.Next(-6, 7), Main.rand.Next(-6, 7)), Mod.Find<ModGore>("InfernalTyrantBodyGore1").Type, NPC.scale);
                    Gore.NewGore(NPC.GetSource_Death(), NPC.position, new Vector2(Main.rand.Next(-6, 7), Main.rand.Next(-6, 7)), Mod.Find<ModGore>("InfernalTyrantBodyGore2").Type, NPC.scale);
                    Gore.NewGore(NPC.GetSource_Death(), NPC.position, new Vector2(Main.rand.Next(-6, 7), Main.rand.Next(-6, 7)), Mod.Find<ModGore>("InfernalTyrantBodyGore3").Type, NPC.scale);
                }
                for (int j = 0; j < 10; j++)
                {
                    Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.Blood, hitDirection, -1f);
                }
            }
        }
        public override void AI()
        {
            GenericVariables gv = new GenericVariables();
            if (RemnantOfTheAncientsMod.CalamityMod != null)
            {
                if (GenericVariables.SpawnCounter >= GenericVariables.TimeInmune)
                {
                    SetDefautsCalamity();
                }
                else
                {
                    //  GenericVariables.SpawnCounter++;
                    if (RemnantOfTheAncientsMod.CalamityMod != null) SetNpcDamageReductionCalamity(1f, 1f, 1f, 1f);
                }
            }
        }
        public override void Init()
        {
            InfernalTyrantHead.CommonWormInit(this);
        }
    }

    internal class InfernalTyrantTail : WormTail
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Infernal Tyrant");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Tirano infernal");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Tyran infernal");

            NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
            {
                Hide = true // Hides this NPC from the Bestiary, useful for multi-part NPCs whom you only want one entry.
            };
            NPCID.Sets.NPCBestiaryDrawOffset.Add(NPC.type, value);
        }

        public override void SetDefaults()
        {
            NPC.CloneDefaults(NPCID.DiggerTail);
            NPC.lifeMax = BaseStats.LifeMax;
            if (RemnantOfTheAncientsMod.CalamityMod != null) SetDefautsCalamity();
            NPC.width = 30;//105
            NPC.height = 40;//103
            NPC.defense = 25;
            NPC.aiStyle = -1;
            NPC.scale = 2.50f;
            NPC.boss = true;
        }
        [JITWhenModsEnabled("CalamityMod")]
        public void SetDefautsCalamity()
        {
            NPC.Calamity().VulnerableToHeat = false;
            NPC.Calamity().VulnerableToCold = false;
            NPC.Calamity().VulnerableToSickness = true;
            NPC.Calamity().canBreakPlayerDefense = true;
            SetNpcDamageReductionCalamity(0.01f, 0.12f, 0.19f, 0.2f);
            CalamityLifeScale(BaseStats.LifeMax);
        }
        [JITWhenModsEnabled("CalamityMod")]
        public void CalamityLifeScale(int life)
        {
            life = (int)NpcChanges1.ExpertLifeScale(life);
            NPC.LifeMaxNERB(life, (int)(life * 1.5), (int)(life * 0.8));
        }
        [JITWhenModsEnabled("CalamityMod")]
        public void SetNpcDamageReductionCalamity(float normal, float revenge, float death, float bossrush)
        {
            NPC.DR_NERD(normal, revenge, death, bossrush);
        }
        public override void AI()
        {
            GenericVariables gv = new GenericVariables();
            if (RemnantOfTheAncientsMod.CalamityMod != null)
            {
                if (GenericVariables.SpawnCounter >= GenericVariables.TimeInmune)
                {
                    SetDefautsCalamity();
                }
                else
                {
                    // GenericVariables.SpawnCounter++;
                    SetNpcDamageReductionCalamity(1f, 1f, 1f, 1f);
                }
            }
        }
        public override void HitEffect(int hitDirection, double damage)
        {
            if (NPC.life <= 0)
            {
                if (Main.netMode != NetmodeID.Server)
                {
                    Gore.NewGore(NPC.GetSource_Death(), NPC.position, new Vector2(Main.rand.Next(-6, 7), Main.rand.Next(-6, 7)), Mod.Find<ModGore>("InfernalTyrantTailGore1").Type, NPC.scale);
                    Gore.NewGore(NPC.GetSource_Death(), NPC.position, new Vector2(Main.rand.Next(-6, 7), Main.rand.Next(-6, 7)), Mod.Find<ModGore>("InfernalTyrantTailGore2").Type, NPC.scale);
                }
                for (int j = 0; j < 10; j++)
                {
                    Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.Blood, hitDirection, -1f);
                }
            }
        }
        public override void PostDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            SpriteEffects effects = SpriteEffects.None;
            if (NPC.spriteDirection == 1)
            {
                effects = SpriteEffects.FlipHorizontally;
            }
            Vector2 vectorFrame = new Vector2(TextureAssets.Npc[NPC.type].Value.Width / 2, TextureAssets.Npc[NPC.type].Value.Height / Main.npcFrameCount[NPC.type] / 2);
            Vector2 position = new Vector2(NPC.Center.X, NPC.Center.Y) - Main.screenPosition;
            var a = ModContent.Request<Texture2D>("RemnantOfTheAncientsMod/NPCs/ITyrant/InfernalTyrantTail_glow");
            position -= new Vector2(a.Width(), a.Height() / Main.npcFrameCount[NPC.type]) * 1f / 2f;
            position += vectorFrame * 1f + new Vector2(0f, 4f + NPC.gfxOffY);
            Color color = Utils.MultiplyRGBA(new Color(127 - NPC.alpha, 127 - NPC.alpha, 127 - NPC.alpha, 0), Color.LightYellow);
            Main.spriteBatch.Draw((Texture2D)a, position, NPC.frame, color, NPC.rotation, vectorFrame, NPC.scale, effects, 0f);
        }
        public override void Init()
        {
            InfernalTyrantHead.CommonWormInit(this);
        }
    }

    public static class TyranStats 
    {
        public static int TyrantArmor(int i, NPC npc)
        {
            if (Main.expertMode || Main.masterMode)
            {
                if (npc.life > npc.life / 10)
                {
                    i /= 3;
                }
                else if (npc.life > npc.life / 15 && Reaper.ReaperMode)
                {
                    i = 0;
                }
            }
            else if (npc.life > npc.life / 4)
            {
                i /= 2;
            }

            int a = RemnantOfTheAncientsMod.CalamityMod != null ? i * 2 : i;

            return a;
        }
    }
}