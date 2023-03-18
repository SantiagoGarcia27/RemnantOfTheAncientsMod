


using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
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
using Terraria.GameContent;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.NPCs.ITyrant
{
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
            { // Influences how the NPC looks in the Bestiary
                CustomTexturePath = "RemnantOfTheAncientsMod/NPCs/ITyrant/InfernalTyrantHead_Bestiary", // If the NPC is multiple parts like a worm, a custom texture for the Bestiary is encouraged.
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
            // Head is 10 defence, body 20, tail 30.
            NPC.CloneDefaults(NPCID.DiggerHead);
            NPC.aiStyle = -1;
            NPC.width = 30;//105
            NPC.height = 30;//103
            NPC.boss = true;
            NPC.lifeMax = (int)NpcChanges1.ExpertLifeScale(30000);
            NPC.damage = (int)NpcChanges1.ExpertDamageScale(300);
            NPC.defense = TyranStats.TyrantArmor(999, ModContent.GetModNPC(ModContent.NPCType<InfernalTyrantHead>()).NPC);
            NPC.scale = 2.50f;
            NPC.npcSlots = 20f;
            NPC.lavaImmune = true;
            Music = MusicLoader.GetMusicSlot(Mod, "Sounds/Music/Infernal_Tyrant");
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            // We can use AddRange instead of calling Add multiple times in order to add multiple items at once
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {
				// Sets the spawning conditions of this NPC that is listed in the bestiary.
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheUnderworld,

				// Sets the description of this NPC that is listed in the bestiary.
				new FlavorTextBestiaryInfoElement("A great and dreaded worm rules the underworld with an iron fist and his flames, powerful and majestic in equal parts, maintain the order and warmth of the underworld.")
            });
        }

        public override void Init()
        {
            // Set the segment variance
            // If you want the segment length to be constant, set these two properties to the same value
            MinSegmentLength = 15;
            MaxSegmentLength = 15;
            head = true;
            CommonWormInit(this);
        }

        // This method is invoked from ExampleWormHead, ExampleWormBody and ExampleWormTail
        internal static void CommonWormInit(Worm worm)
        {
            // These two properties handle the movement of the worm
            
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

        public override void AI()
        {

            if (Main.netMode != NetmodeID.MultiplayerClient)
            {

                if (attackCounter > 0)
                {
                    attackCounter--; // tick down the attack counter.
                }

                Player target = Main.player[NPC.target];
                DespawnSafeCheck(target);
                switch (attackCounter)
                {
                    case 200:
                        SpikeIa(0f, (int)NpcChanges1.ExpertDamageScale(40), false, -3, -3);
                        break;
                    case 250:
                        FireBallIa(12f, (int)NpcChanges1.ExpertDamageScale(70), ModContent.ProjectileType<InfernalBallF>(), "*", 4, 3, target, 0f);
                        FireBallIa(12f, (int)NpcChanges1.ExpertDamageScale(70), ModContent.ProjectileType<InfernalBallF>(), "/", 4, 3, target, 0f);
                        break;
                    case 300:
                        SummonIa(NPCID.Demon);
                        break;
                    case 400:
                        SpikeIa(0f, (int)NpcChanges1.ExpertDamageScale(90), true, 3, -3);
                        break;
                    case 430:
                        SpikeIa(0f, (int)NpcChanges1.ExpertDamageScale(40), false, 3, -3);
                        break;
                    case 600:
                        for (int i = -2; i <= 3; i++)
                            FireBallIa(12f, (int)NpcChanges1.ExpertDamageScale(30), ModContent.ProjectileType<InfernalBall>(), "*", i - 1, i + 2, target, 0);
                        break;
                    case 700:
                        SummonIa(NPCID.RedDevil);
                        break;
                }

                // If the attack counter is 0, this NPC is less than 12.5 tiles away from its target, and has a path to the target unobstructed by blocks, summon a projectile.
                if (attackCounter <= 0 && Vector2.Distance(NPC.Center, target.Center) < 200 && Collision.CanHit(NPC.Center, 1, 1, target.Center, 1, 1))
                {
                    attackCounter = 700;
                    NPC.netUpdate = true;
                }
                if (Vector2.Distance(target.Center, NPC.Center) > 200 * 16 && !Main.player[NPC.target].dead)
                {
                    if (CanTp)
                    {
                        TyrantTp(target);
                    }
                    else if (Vector2.Distance(target.Center, NPC.Center) <= 10 * 16)
                    {
                        CanTp = true;

                    }
                }
                LifeSpeed(this);
            }
        }
        public void FindTarget()
        {
            if (NPC.target < 0 || NPC.target == 255 || Main.player[NPC.target].dead) NPC.TargetClosest(true);
        }
        public void DespawnSafeCheck(Player player)
        {
            Vector2 distance = player.Center - NPC.Center;
            if (distance.Y >= 300 && distance.Y <= 30) 
           // if (Vector2.Distance(player.Center, NPC.Center) > 5 * 16)//245  //45
            {
                NPC.SimpleFlyMovement(player.position * 0.25f, MoveSpeed * 0.5f);
               // NPC.velocity = player.position;
            }
        }
        public void TyrantTp(Player player)
        {
            if (!Main.player[NPC.target].dead)
            {
                if (Vector2.Distance(player.Center, NPC.Center) > 500 * 16 && !Main.player[NPC.target].dead)
                {
                    NPC.Center = player.Center + new Vector2(350, 350);
                }
            }
            else CanTp = false;
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

        public void SpikeIa(float Speed, int damage, bool isStrong, int cordx, int cordy)
        {
            int type = isStrong? ModContent.ProjectileType<InfernalSpikeF>(): ModContent.ProjectileType<InfernalSpike>(); 
            Vector2 vector8 = new Vector2(NPC.position.X + (NPC.width * cordx), NPC.position.Y + (NPC.height * cordy));
            SoundEngine.PlaySound(SoundID.DD2_BetsyFireballShot, NPC.Center);
            float rotation = (float)Math.Atan2(vector8.Y - (Main.player[NPC.target].position.Y + (Main.player[NPC.target].height * 0.5f)), vector8.X - (Main.player[NPC.target].position.X + (Main.player[NPC.target].width * 0.5f)));
            int projectile = Projectile.NewProjectile(NPC.GetSource_FromAI(), vector8.X, vector8.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1), type, damage, 0f, 0);

            Main.projectile[projectile].timeLeft = 1500;
        }
        public void SummonIa(int Npc) => NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, Npc);
        public void LifeSpeed(Worm worm)
        { 
            NPC.defense = TyranStats.TyrantArmor(999, ModContent.GetModNPC(ModContent.NPCType<InfernalTyrantHead>()).NPC);
            if (NPC.life < NPC.lifeMax / 2)
            {
                worm.MoveSpeed = 50f;//9.5f
                worm.Acceleration = 0.15f;
            }
            else if (NPC.life < NPC.lifeMax / 4)
            {
                worm.MoveSpeed = 70f;//9.5f
                worm.Acceleration = 0.55f;
            }
            else if (NPC.life < NPC.lifeMax / 10) worm.Acceleration = 1.1f;
            else if (NPC.life < NPC.lifeMax / 15 && Reaper.ReaperMode) worm.Acceleration = 1.5f;
            else worm.MoveSpeed = 30f;
        }

        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position) => head ? null : false;
        public override void BossLoot(ref string name, ref int potionType)
        {
            DownedBossSystem.downedTyrant = true;
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
            NPC.lifeMax = 3000;
            NPC.defense = TyranStats.TyrantArmor(999,ModContent.GetModNPC(ModContent.NPCType<InfernalTyrantBody>()).NPC);//999
            NPC.aiStyle = -1;
            NPC.width = 30;//105
            NPC.height = 30;//103
            NPC.scale = 2.50f;
            NPC.boss = true;
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
            NPC.lifeMax = 3000;
            NPC.width = 30;//105
            NPC.height = 40;//103
            NPC.defense = 25;
            NPC.aiStyle = -1;
            NPC.scale = 2.50f;
            NPC.boss = true;
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
                    i = i / 3;
                }
                else if (npc.life > npc.life / 15 && Reaper.ReaperMode)
                {
                    i = 0;
                }
            }
            else if (npc.life > npc.life / 4)
            {
                i = i / 2;
            }
            int a = 0;
            if (RemnantOfTheAncientsMod.CalamityMod != null)
            {
                a = i * 2;
            }
            else a = i;
            return a;
        }
    }
}