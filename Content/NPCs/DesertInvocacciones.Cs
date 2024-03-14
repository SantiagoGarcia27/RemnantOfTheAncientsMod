using Microsoft.Xna.Framework;
using RemnantOfTheAncientsMod.Common.UtilsTweaks;
using RemnantOfTheAncientsMod.Content.NPCs.Bosses.DAniquilator;
using RemnantOfTheAncientsMod.Content.Projectiles.Melee;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Content.NPCs
{
    public class AntlionD : ModNPC
    {
        public override string Texture => "Terraria/Images/NPC_" + NPCID.Antlion;
        public override void SetStaticDefaults()
        {
            NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers()
            {
                Velocity = 1f
            };
            NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, value);
            Main.npcFrameCount[NPC.type] = Main.npcFrameCount[NPCID.Antlion];
        }


        public override void SetDefaults()
        {
            NPC.CloneDefaults(NPCID.Antlion);
            NPC.lifeMax = ContentSamples.NpcsByNetId[NPCID.Antlion].lifeMax /2;
            NPC.damage = ContentSamples.NpcsByNetId[NPCID.Antlion].damage / 2;
            //NPC.value += 60f;
            //NPC.knockBackResist = 0;
            NPC.aiStyle = 19;
            AIType = NPCID.Antlion;
            AnimationType = NPCID.Antlion;
            Banner = Item.NPCtoBanner(NPCID.Antlion);
            BannerItem = Item.BannerToItem(Banner);

            //NPC.aiStyle = 19;
            //AIType = NPCID.Antlion;
            //AnimationType = NPCID.Antlion;
            //NPC.CloneDefaults(NPCID.Antlion);
            //NPC.value = Item.buyPrice(0, 0, 0, 0);
            //NPC.lifeMax = 50;
        }
        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            // Makes it so whenever you beat the boss associated with it, it will also get unlocked immediately
            int associatedNPCType = ModContent.NPCType<DesertAniquilator>();
            bestiaryEntry.UIInfoProvider = new CommonEnemyUICollectionInfoProvider(ContentSamples.NpcBestiaryCreditIdsByNpcNetIds[associatedNPCType], quickUnlock: true);

            bestiaryEntry.Info.AddRange(new List<IBestiaryInfoElement> {
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Desert,
                // Plain black background
				new FlavorTextBestiaryInfoElement("A minion protecting his boss from taking damage by sacrificing itself. If none are alive, the boss is exposed to damage.")
            });
        }
    }

    public class VultureD : ModNPC
    {
        public override string Texture => "Terraria/Images/NPC_" + NPCID.Vulture;
        public override void SetStaticDefaults()
        {
            NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers()
            {
                Velocity = 1f
            };
            NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, value);
            Main.npcFrameCount[NPC.type] = Main.npcFrameCount[NPCID.Vulture];
        }


        public override void SetDefaults()
        {

            NPC.aiStyle = 3;
            AIType = NPCID.Vulture;
            AnimationType = NPCID.Vulture;
            NPC.CloneDefaults(NPCID.Vulture);
            NPC.value = Item.buyPrice(0, 0, 0, 0);
            NPC.lifeMax = 40;
        }
        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            // Makes it so whenever you beat the boss associated with it, it will also get unlocked immediately
            int associatedNPCType = ModContent.NPCType<DesertAniquilator>();
            bestiaryEntry.UIInfoProvider = new CommonEnemyUICollectionInfoProvider(ContentSamples.NpcBestiaryCreditIdsByNpcNetIds[associatedNPCType], quickUnlock: true);

            bestiaryEntry.Info.AddRange(new List<IBestiaryInfoElement> {
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Desert,
                // Plain black background
				new FlavorTextBestiaryInfoElement("A minion protecting his boss from taking damage by sacrificing itself. If none are alive, the boss is exposed to damage.")
            });
        }
    }
    public class WalkingAntlionD : ModNPC
    {
        public override string Texture => "Terraria/Images/NPC_" + NPCID.WalkingAntlion;
        public override void SetStaticDefaults()
        {
            NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers()
            {
                Velocity = 1f
            };
            NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, value);
            Main.npcFrameCount[NPC.type] = Main.npcFrameCount[NPCID.WalkingAntlion];
        }


        public override void SetDefaults()
        {

            NPC.aiStyle = 3;
            AIType = NPCID.WalkingAntlion;
            AnimationType = NPCID.WalkingAntlion;
            NPC.CloneDefaults(NPCID.WalkingAntlion);
            NPC.value = Item.buyPrice(0, 0, 0, 0);
            base.NPC.lifeMax -= 90;
        }
        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            // Makes it so whenever you beat the boss associated with it, it will also get unlocked immediately
            int associatedNPCType = ModContent.NPCType<DesertAniquilator>();
            bestiaryEntry.UIInfoProvider = new CommonEnemyUICollectionInfoProvider(ContentSamples.NpcBestiaryCreditIdsByNpcNetIds[associatedNPCType], quickUnlock: true);

            bestiaryEntry.Info.AddRange(new List<IBestiaryInfoElement> {
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Desert,
                // Plain black background
				new FlavorTextBestiaryInfoElement("A minion protecting his boss from taking damage by sacrificing itself. If none are alive, the boss is exposed to damage.")
            });
        }
    }
    public class DesertScorpionD : ModNPC
    {
        public override string Texture => "Terraria/Images/NPC_" + NPCID.DesertScorpionWalk;
        public override void SetStaticDefaults()
        {
            NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers()
            {
                Velocity = 1f
            };
            NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, value);
            Main.npcFrameCount[NPC.type] = Main.npcFrameCount[NPCID.DesertScorpionWalk];
        }


        public override void SetDefaults()
        {

            NPC.CloneDefaults(NPCID.DesertScorpionWalk);
            AIType = NPCID.DesertScorpionWalk;
            AnimationType = NPCID.DesertScorpionWalk;
            NPC.value = Item.buyPrice(0, 0, 0, 0);
            NPC.lifeMax -= 320;
            NPC.damage = 66;
            NPC.defense = 12;
        }
        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            // Makes it so whenever you beat the boss associated with it, it will also get unlocked immediately
            int associatedNPCType = ModContent.NPCType<DesertAniquilator>();
            bestiaryEntry.UIInfoProvider = new CommonEnemyUICollectionInfoProvider(ContentSamples.NpcBestiaryCreditIdsByNpcNetIds[associatedNPCType], quickUnlock: true);

            bestiaryEntry.Info.AddRange(new List<IBestiaryInfoElement> {
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Desert,
                // Plain black background
				new FlavorTextBestiaryInfoElement("A minion protecting his boss from taking damage by sacrificing itself. If none are alive, the boss is exposed to damage.")
            });
        }
    }
    public class DesertAnnihilatorServant : ModNPC
    {
        public override void SetStaticDefaults()
        {
            NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers()
            {
                Velocity = 1f
            };
            NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, value);
            Main.npcFrameCount[NPC.type] = Main.npcFrameCount[NPCID.SandSlime];
        }
        public override void SetDefaults()
        {
            AIType = NPCID.SandSlime;
            AnimationType = NPCID.SandSlime;
            NPC.CloneDefaults(NPCID.SandSlime);
            NPC.value = Item.buyPrice(0, 0, 0, 0);
            NPC.lifeMax = 50;
            NPC.damage = 22;
        }
        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            // Makes it so whenever you beat the boss associated with it, it will also get unlocked immediately
            int associatedNPCType = ModContent.NPCType<DesertAniquilator>();
            bestiaryEntry.UIInfoProvider = new CommonEnemyUICollectionInfoProvider(ContentSamples.NpcBestiaryCreditIdsByNpcNetIds[associatedNPCType], quickUnlock: true);

            bestiaryEntry.Info.AddRange(new List<IBestiaryInfoElement> {
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Desert,
                // Plain black background
				new FlavorTextBestiaryInfoElement("A minion protecting his boss from taking damage by sacrificing itself. If none are alive, the boss is exposed to damage.")
            });
        }
    }
    public class FlyingAntlionD : ModNPC
    {
        public override string Texture => "Terraria/Images/NPC_" + NPCID.FlyingAntlion;
        public override void SetStaticDefaults()
        {
            NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers()
            {
                Velocity = 1f
            };
            NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, value);
            Main.npcFrameCount[NPC.type] = Main.npcFrameCount[NPCID.FlyingAntlion];
        }


        public override void SetDefaults()
        {

            AIType = NPCID.FlyingAntlion;
            AnimationType = NPCID.FlyingAntlion;
            NPC.CloneDefaults(NPCID.FlyingAntlion);
            NPC.value = Item.buyPrice(0, 0, 0, 0);
            NPC.lifeMax = 100;
        }
        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            // Makes it so whenever you beat the boss associated with it, it will also get unlocked immediately
            int associatedNPCType = ModContent.NPCType<DesertAniquilator>();
            bestiaryEntry.UIInfoProvider = new CommonEnemyUICollectionInfoProvider(ContentSamples.NpcBestiaryCreditIdsByNpcNetIds[associatedNPCType], quickUnlock: true);

            bestiaryEntry.Info.AddRange(new List<IBestiaryInfoElement> {
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Desert,
                // Plain black background
				new FlavorTextBestiaryInfoElement("A minion protecting his boss from taking damage by sacrificing itself. If none are alive, the boss is exposed to damage.")
            });
        }
    }
    [JITWhenModsEnabled("CalamityMod")]
    public class DesertAnnihilatorGuard : ModNPC
    {

        public override bool IsLoadingEnabled(Mod mod)
        {

            return ModLoader.TryGetMod("InfernumMode", out Mod InfernumMode);
        }
        public override void SetStaticDefaults()
        {
            NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers()
            {
                Velocity = 1f
            };
            NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, value);
            Main.npcFrameCount[NPC.type] = Main.npcFrameCount[NPCID.SandSlime];
        }
        public override void SetDefaults()
        {
            AIType = NPCID.SandSlime;
            AnimationType = NPCID.SandSlime;
            NPC.CloneDefaults(NPCID.SandSlime);
            NPC.value = Item.buyPrice(0, 0, 0, 0);
            NPC.lifeMax = 2000;
            NPC.damage = 22;
            NPC.scale = 1;
        }
        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            // Makes it so whenever you beat the boss associated with it, it will also get unlocked immediately
            int associatedNPCType = ModContent.NPCType<DesertAniquilator>();
            bestiaryEntry.UIInfoProvider = new CommonEnemyUICollectionInfoProvider(ContentSamples.NpcBestiaryCreditIdsByNpcNetIds[associatedNPCType], quickUnlock: true);

            bestiaryEntry.Info.AddRange(new List<IBestiaryInfoElement> {
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Desert,
                // Plain black background
				new FlavorTextBestiaryInfoElement("A minion protecting his boss from taking damage by sacrificing itself. If none are alive, the boss is exposed to damage.")
            });
        }
        public override void AI()
        {
            Player player = Main.player[NPC.target];
            NPC.scale = LifeSize(NPC);
            int[] projectiles = new int[]
            {
                ExternalModCallUtils.GetProjectileFromMod(RemnantOfTheAncientsMod.CalamityMod,"LuxorsGiftMagic"),
                ExternalModCallUtils.GetProjectileFromMod(RemnantOfTheAncientsMod.CalamityMod,"LuxorsGiftMelee"),
                ExternalModCallUtils.GetProjectileFromMod(RemnantOfTheAncientsMod.CalamityMod,"LuxorsGiftRanged"),
                ExternalModCallUtils.GetProjectileFromMod(RemnantOfTheAncientsMod.CalamityMod,"LuxorsGiftRogue")
            };
            int proj = 0;


            if (NPC.ai[1] == 200)
            {

                int numProjectiles = 10;
                float spacing = MathHelper.TwoPi / numProjectiles;
                float radius = 10 * 16f;

                for (int i = 0; i < numProjectiles; i++)
                {
                    proj = proj == projectiles.Length - 1 ? 0 : proj++;
                    Vector2 projectilePos = NPC.Center + radius * new Vector2((float)Math.Cos(spacing * i), (float)Math.Sin(spacing * i));
                    Vector2 projectileVel = 10f * (player.Center - projectilePos).SafeNormalize(Vector2.Zero);     
                    int p = Projectile.NewProjectile(NPC.GetSource_FromAI(), projectilePos, projectileVel, projectiles[proj], 50, 1, Main.player[NPC.target].whoAmI);
                    proj++;
                    Main.projectile[p].hostile = true;
                    Main.projectile[p].friendly = false;
                    float angle = (float)Math.Atan2(projectileVel.Y, projectileVel.X);
                    Main.projectile[p].rotation = angle;
                }



                //for (int i = 0; i < 10; i++)
                //{
                //    int p = Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, velocity, projectiles[proj], 50, 1, Main.player[NPC.target].whoAmI);
                //    Main.projectile[p].hostile = true;
                //    Main.projectile[p].friendly = false;
                //}
                NPC.ai[1] = 0;
            }
            else
            {
                NPC.ai[1]++;
            }
            base.AI();
        }
        private float LifeSize(NPC npc)
        {
            float porcentage = Utils1.GetPorcentage(npc.life, npc.lifeMax);
            return applyLifeSize(porcentage, 1.5f);          
        }
        private float applyLifeSize(float porcentage, float MaxValue)
        {
            for (int i = 100; i >= 0; i--)
            {
                if ((int)porcentage == i)
                {
                    float j = Utils1.GetValueFromPorcentage(MaxValue, i);
                    if (j > Utils1.GetValueFromPorcentage(MaxValue, 30))
                    {
                        return j;
                    }
                }

            }
            return Utils1.GetValueFromPorcentage(MaxValue, 30);
        }
    }
}