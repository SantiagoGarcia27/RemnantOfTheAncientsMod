using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using Microsoft.Xna.Framework;
using Terraria.ModLoader.Utilities;
using RemnantOfTheAncientsMod.Content.Items.Items;
using RemnantOfTheAncientsMod.Content.Items.Armor.Tuxonite;
using RemnantOfTheAncientsMod.Content.Items.Armor.Reinforced_Iron;
using Terraria.DataStructures;
using RemnantOfTheAncientsMod.Content.Items.Armor.Night;
using RemnantOfTheAncientsMod.Common.Systems;
using RemnantOfTheAncientsMod.Common.UtilsTweaks;

namespace RemnantOfTheAncientsMod.Content.NPCs.zombis
{
    public class Fastzombie : ModNPC
    {
        public override void SetStaticDefaults()
        {

            DisplayName.SetDefault("weak zombie");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "zombie débil");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Zombie faible");
            Main.npcFrameCount[Type] = Main.npcFrameCount[NPCID.Zombie];
        }

        public override void SetDefaults()
        {
            NPC.CloneDefaults(NPCID.Zombie);
            NPC.damage = (int)(ContentSamples.NpcsByNetId[NPCID.Zombie].damage * 0.5f);
            NPC.defense = 5;
            NPC.lifeMax = ContentSamples.NpcsByNetId[NPCID.Zombie].lifeMax / 2;
            NPC.value = 60f;
            NPC.knockBackResist = 1.2f;
            AIType = NPCID.Zombie;
            AnimationType = NPCID.Zombie;
        }
        public override void ModifyNPCLoot(NPCLoot npcLoot) => Item.NewItem(NPC.GetSource_Loot(), NPC.getRect(), ItemID.Gel);
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (NPC.downedBoss3) return SpawnCondition.OverworldNightMonster.Chance * 0f;
            else if (NPC.downedBoss1) return SpawnCondition.OverworldNightMonster.Chance * 0.001f;
            else return SpawnCondition.OverworldNightMonster.Chance * 0.03f;
        }

        public override void HitEffect(int hitDirection, double damage)
        {
            for (int i = 0; i < 10; i++)
            {
                Dust dust = Dust.NewDustDirect(NPC.position, NPC.width, NPC.height, Main.rand.Next(139, 143));
                dust.velocity = new Vector2(Main.rand.NextFloat(-50f, 51f) * 1110.01f, Main.rand.NextFloat(-50f, 51f) * 111110.01f);
                dust.scale *= 1f + Main.rand.Next(-30, 31) * 0.01f;
            }
        }
    }

    public class Copperzombie : BaseZombie
    {
        static int DefenseSetBonus = 2;
        static int Helmet = new Item(ItemID.CopperHelmet).defense;
        static int Chestplate = new Item(ItemID.CopperChainmail).defense;
        static int Greaves = new Item(ItemID.CopperGreaves).defense;
        static int AttackBonus = 1;
        static int CritBonus = 0;
        int Defense = DefenseSetBonus + Helmet + Chestplate + Greaves;
        public override void SetDefaults()
        {
            NPC.CloneDefaults(NPCID.Zombie);
            NPC.lifeMax = ContentSamples.NpcsByNetId[NPCID.Zombie].lifeMax;
            NPC.damage = (int)(ContentSamples.NpcsByNetId[NPCID.Zombie].damage * AttackBonus);
            NPC.defense += Defense;
            NPC.value += 60f;
            NPC.knockBackResist = 0;
            NPC.aiStyle = 3;
            AIType = NPCID.Zombie;
            AnimationType = NPCID.Zombie;
            Banner = Item.NPCtoBanner(NPCID.Zombie);
            BannerItem = Item.BannerToItem(Banner);
            NPC.GetGlobalNPC<RemnantGlobalNPC>().CanMakeCrit = CritBonus > 0 ? true : false;
            NPC.GetGlobalNPC<RemnantGlobalNPC>().CritChance = CritBonus;
        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (NPC.downedBoss3) return SpawnCondition.OverworldNightMonster.Chance * 0f;
            else if (NPC.downedBoss1) return SpawnCondition.OverworldNightMonster.Chance * 0.02f;
            else return SpawnCondition.OverworldNightMonster.Chance * 0.01f;
        }
        public override void HitEffect(int hitDirection, double damage)
        {
            for (int i = 0; i < 10; i++)
            {
                Dust dust = Dust.NewDustDirect(NPC.position, NPC.width, NPC.height, Main.rand.Next(139, 143));
                dust.velocity = new Vector2(Main.rand.NextFloat(-50f, 51f) * 1110.01f, Main.rand.NextFloat(-50f, 51f) * 111110.01f);
                dust.scale *= 1f + Main.rand.Next(-30, 31) * 0.01f;
            }
        }
    }

    public class Tinzombie : BaseZombie
    {
        static int DefenseSetBonus = 2;
        static int Helmet = new Item(ItemID.TinHelmet).defense;
        static int Chestplate = new Item(ItemID.TinChainmail).defense;
        static int Greaves = new Item(ItemID.TinGreaves).defense;
        static int AttackBonus = 1;
        static int CritBonus = 0;
        int Defense = DefenseSetBonus + Helmet + Chestplate + Greaves;

        public override void SetDefaults()
        {
            NPC.CloneDefaults(NPCID.Zombie);
            NPC.lifeMax = ContentSamples.NpcsByNetId[NPCID.Zombie].lifeMax;
            NPC.damage = (int)(ContentSamples.NpcsByNetId[NPCID.Zombie].damage * AttackBonus);
            NPC.defense += Defense;
            NPC.value += 60f;
            NPC.knockBackResist = 0;
            NPC.aiStyle = 3;
            AIType = NPCID.Zombie;
            AnimationType = NPCID.Zombie;
            Banner = Item.NPCtoBanner(NPCID.Zombie);
            BannerItem = Item.BannerToItem(Banner);
            NPC.GetGlobalNPC<RemnantGlobalNPC>().CanMakeCrit = CritBonus > 0 ? true : false;
            NPC.GetGlobalNPC<RemnantGlobalNPC>().CritChance = CritBonus;
        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (NPC.downedBoss3) return SpawnCondition.OverworldNightMonster.Chance * 0f;
            else if (NPC.downedBoss1) return SpawnCondition.OverworldNightMonster.Chance * 0.02f;
            else return SpawnCondition.OverworldNightMonster.Chance * 0.01f;
        }
        public override void HitEffect(int hitDirection, double damage)
        {
            for (int i = 0; i < 10; i++)
            {
                Dust dust = Dust.NewDustDirect(NPC.position, NPC.width, NPC.height, Main.rand.Next(139, 143));
                dust.velocity = new Vector2(Main.rand.NextFloat(-50f, 51f) * 1110.01f, Main.rand.NextFloat(-50f, 51f) * 111110.01f);
                dust.scale *= 1f + Main.rand.Next(-30, 31) * 0.01f;
            }
        }
    }

    public class Leadzombie : BaseZombie
    {
        static int DefenseSetBonus = 2;
        static int Helmet = new Item(ItemID.LeadHelmet).defense;
        static int Chestplate = new Item(ItemID.LeadChainmail).defense;
        static int Greaves = new Item(ItemID.LeadGreaves).defense;
        static int AttackBonus = 1;
        static int CritBonus = 0;
        int Defense = DefenseSetBonus + Helmet + Chestplate + Greaves;

        public override void SetDefaults()
        {
            NPC.CloneDefaults(NPCID.Zombie);
            NPC.lifeMax = ContentSamples.NpcsByNetId[NPCID.Zombie].lifeMax;
            NPC.damage = (int)(ContentSamples.NpcsByNetId[NPCID.Zombie].damage * AttackBonus);
            NPC.defense += Defense;
            NPC.value += 60f;
            NPC.knockBackResist = 0;
            NPC.aiStyle = 3;
            AIType = NPCID.Zombie;
            AnimationType = NPCID.Zombie;
            Banner = Item.NPCtoBanner(NPCID.Zombie);
            BannerItem = Item.BannerToItem(Banner);
            NPC.GetGlobalNPC<RemnantGlobalNPC>().CanMakeCrit = CritBonus > 0 ? true : false;
            NPC.GetGlobalNPC<RemnantGlobalNPC>().CritChance = CritBonus;
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (NPC.downedBoss3) return SpawnCondition.OverworldNightMonster.Chance * 0f;
            else if (NPC.downedBoss1) return SpawnCondition.OverworldNightMonster.Chance * 0.02f;
            else return SpawnCondition.OverworldNightMonster.Chance * 0.01f;
        }
        public override void HitEffect(int hitDirection, double damage)
        {
            for (int i = 0; i < 10; i++)
            {
                Dust dust = Dust.NewDustDirect(NPC.position, NPC.width, NPC.height, Main.rand.Next(139, 143));
                dust.velocity = new Vector2(Main.rand.NextFloat(-50f, 51f) * 1110.01f, Main.rand.NextFloat(-50f, 51f) * 111110.01f);
                dust.scale *= 1f + Main.rand.Next(-30, 31) * 0.01f;
            }
        }
    }

    public class Ironzombie : BaseZombie
    {
        static int DefenseSetBonus = 2;
        static int Helmet = new Item(ItemID.IronHelmet).defense;
        static int Chestplate = new Item(ItemID.IronChainmail).defense;
        static int Greaves = new Item(ItemID.IronGreaves).defense;
        static int AttackBonus = 1;
        static int CritBonus = 0;
        int Defense = DefenseSetBonus + Helmet + Chestplate + Greaves;

        public override void SetDefaults()
        {
            NPC.CloneDefaults(NPCID.Zombie);
            NPC.lifeMax = ContentSamples.NpcsByNetId[NPCID.Zombie].lifeMax;
            NPC.damage = (int)(ContentSamples.NpcsByNetId[NPCID.Zombie].damage * AttackBonus);
            NPC.defense += Defense;
            NPC.value += 60f;
            NPC.knockBackResist = 0;
            NPC.aiStyle = 3;
            AIType = NPCID.Zombie;
            AnimationType = NPCID.Zombie;
            Banner = Item.NPCtoBanner(NPCID.Zombie);
            BannerItem = Item.BannerToItem(Banner);
            NPC.GetGlobalNPC<RemnantGlobalNPC>().CanMakeCrit = CritBonus > 0 ? true : false;
            NPC.GetGlobalNPC<RemnantGlobalNPC>().CritChance = CritBonus;

        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (NPC.downedBoss3) return SpawnCondition.OverworldNightMonster.Chance * 0f;
            else if (NPC.downedBoss1) return SpawnCondition.OverworldNightMonster.Chance * 0.02f;
            else return SpawnCondition.OverworldNightMonster.Chance * 0.01f;
        }
        public override void HitEffect(int hitDirection, double damage)
        {
            for (int i = 0; i < 10; i++)
            {
                Dust dust = Dust.NewDustDirect(NPC.position, NPC.width, NPC.height, Main.rand.Next(139, 143));
                dust.velocity = new Vector2(Main.rand.NextFloat(-50f, 51f) * 1110.01f, Main.rand.NextFloat(-50f, 51f) * 111110.01f);
                dust.scale *= 1f + Main.rand.Next(-30, 31) * 0.01f;
            }
        }
    }

    public class Silverzombie : BaseZombie
    {
        static int DefenseSetBonus = 3;
        static int Helmet = new Item(ItemID.SilverHelmet).defense;
        static int Chestplate = new Item(ItemID.SilverChainmail).defense;
        static int Greaves = new Item(ItemID.SilverGreaves).defense;
        static int AttackBonus = 1;
        static int CritBonus = 0;
        int Defense = DefenseSetBonus + Helmet + Chestplate + Greaves;
        public override void SetDefaults()
        {
            NPC.CloneDefaults(NPCID.Zombie);
            NPC.lifeMax = ContentSamples.NpcsByNetId[NPCID.Zombie].lifeMax;
            NPC.damage = (int)(ContentSamples.NpcsByNetId[NPCID.Zombie].damage * AttackBonus);
            NPC.defense += Defense;
            NPC.value += 60f;
            NPC.knockBackResist = 0;
            NPC.aiStyle = 3;
            AIType = NPCID.Zombie;
            AnimationType = NPCID.Zombie;
            Banner = Item.NPCtoBanner(NPCID.Zombie);
            BannerItem = Item.BannerToItem(Banner);
            NPC.GetGlobalNPC<RemnantGlobalNPC>().CanMakeCrit = CritBonus > 0 ? true : false;
            NPC.GetGlobalNPC<RemnantGlobalNPC>().CritChance = CritBonus;
        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (NPC.downedBoss3) return SpawnCondition.OverworldNightMonster.Chance * 0f;
            else if (NPC.downedBoss1) return SpawnCondition.OverworldNightMonster.Chance * 0.019f;
            else return SpawnCondition.OverworldNightMonster.Chance * 0.009f;
        }

        public override void HitEffect(int hitDirection, double damage)
        {
            for (int i = 0; i < 10; i++)
            {
                Dust dust = Dust.NewDustDirect(NPC.position, NPC.width, NPC.height, Main.rand.Next(139, 143));
                dust.velocity = new Vector2(Main.rand.NextFloat(-50f, 51f) * 1110.01f, Main.rand.NextFloat(-50f, 51f) * 111110.01f);
                dust.scale *= 1f + Main.rand.Next(-30, 31) * 0.01f;
            }
        }
    }

    public class Tungstenzombie : BaseZombie
    {
        static int DefenseSetBonus = 3;
        static int Helmet = new Item(ItemID.TungstenHelmet).defense;
        static int Chestplate = new Item(ItemID.TungstenChainmail).defense;
        static int Greaves = new Item(ItemID.TungstenGreaves).defense;
        static int AttackBonus = 1;
        static int CritBonus = 0;
        int Defense = DefenseSetBonus + Helmet + Chestplate + Greaves;
        public override void SetDefaults()
        {
            NPC.CloneDefaults(NPCID.Zombie);
            NPC.lifeMax = ContentSamples.NpcsByNetId[NPCID.Zombie].lifeMax;
            NPC.damage = (int)(ContentSamples.NpcsByNetId[NPCID.Zombie].damage * AttackBonus);
            NPC.defense += Defense;
            NPC.value += 60f;
            NPC.knockBackResist = 0;
            NPC.aiStyle = 3;
            AIType = NPCID.Zombie;
            AnimationType = NPCID.Zombie;
            Banner = Item.NPCtoBanner(NPCID.Zombie);
            BannerItem = Item.BannerToItem(Banner);
            NPC.GetGlobalNPC<RemnantGlobalNPC>().CanMakeCrit = CritBonus > 0 ? true : false;
            NPC.GetGlobalNPC<RemnantGlobalNPC>().CritChance = CritBonus;
        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (NPC.downedBoss3) return SpawnCondition.OverworldNightMonster.Chance * 0f;
            else if (NPC.downedBoss1) return SpawnCondition.OverworldNightMonster.Chance * 0.019f;
            else return SpawnCondition.OverworldNightMonster.Chance * 0.009f;
        }

        public override void HitEffect(int hitDirection, double damage)
        {
            for (int i = 0; i < 10; i++)
            {
                Dust dust = Dust.NewDustDirect(NPC.position, NPC.width, NPC.height, Main.rand.Next(139, 143));
                dust.velocity = new Vector2(Main.rand.NextFloat(-50f, 51f) * 1110.01f, Main.rand.NextFloat(-50f, 51f) * 111110.01f);
                dust.scale *= 1f + Main.rand.Next(-30, 31) * 0.01f;
            }
        }
    }

    public class Goldzombie : BaseZombie
    {
        static int DefenseSetBonus = 3;
        static int Helmet = new Item(ItemID.GoldHelmet).defense;
        static int Chestplate = new Item(ItemID.GoldChainmail).defense;
        static int Greaves = new Item(ItemID.GoldGreaves).defense;
        static int AttackBonus = 1;
        static int CritBonus = 0;
        int Defense = DefenseSetBonus + Helmet + Chestplate + Greaves;
        public override void SetDefaults()
        {
            NPC.CloneDefaults(NPCID.Zombie);
            NPC.lifeMax = ContentSamples.NpcsByNetId[NPCID.Zombie].lifeMax;
            NPC.damage = (int)(ContentSamples.NpcsByNetId[NPCID.Zombie].damage * AttackBonus);
            NPC.defense += Defense;
            NPC.value += 60f;
            NPC.knockBackResist = 0;
            NPC.aiStyle = 3;
            AIType = NPCID.Zombie;
            AnimationType = NPCID.Zombie;
            Banner = Item.NPCtoBanner(NPCID.Zombie);
            BannerItem = Item.BannerToItem(Banner);
            NPC.GetGlobalNPC<RemnantGlobalNPC>().CanMakeCrit = CritBonus > 0 ? true : false;
            NPC.GetGlobalNPC<RemnantGlobalNPC>().CritChance = CritBonus;

        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (Main.hardMode) return SpawnCondition.OverworldNightMonster.Chance * 0f;
            else if (NPC.downedBoss3) return SpawnCondition.OverworldNightMonster.Chance * 0.02f;
            else if (NPC.downedBoss1) return SpawnCondition.OverworldNightMonster.Chance * 0.016f;
            else return SpawnCondition.OverworldNightMonster.Chance * 0.006f;
        }

        public override void HitEffect(int hitDirection, double damage)
        {
            for (int i = 0; i < 10; i++)
            {
                Dust dust = Dust.NewDustDirect(NPC.position, NPC.width, NPC.height, Main.rand.Next(139, 143));
                dust.velocity = new Vector2(Main.rand.NextFloat(-50f, 51f) * 1110.01f, Main.rand.NextFloat(-50f, 51f) * 111110.01f);
                dust.scale *= 1f + Main.rand.Next(-30, 31) * 0.01f;
            }
        }
    }

    public class Platinumzombie : BaseZombie
    {
        static int DefenseSetBonus = 4;
        static int Helmet => ContentSamples.ItemsByType[ItemID.PlatinumHelmet].defense;
        static int Chestplate => ContentSamples.ItemsByType[ItemID.PlatinumChainmail].defense;
        static int Greaves => ContentSamples.ItemsByType[ItemID.PlatinumGreaves].defense;
        int Defense => DefenseSetBonus + Helmet + Chestplate + Greaves;
        static float AttackBonus = 1.00f;
        static int CritBonus = 0;

        public override void SetDefaults()
        {
            NPC.CloneDefaults(NPCID.Zombie);
            NPC.lifeMax = ContentSamples.NpcsByNetId[NPCID.Zombie].lifeMax;
            NPC.damage = (int)(ContentSamples.NpcsByNetId[NPCID.Zombie].damage * AttackBonus);
            NPC.defense += Defense;
            NPC.value += 60f;
            NPC.knockBackResist = 0;
            NPC.aiStyle = 3;
            AIType = NPCID.Zombie;
            AnimationType = NPCID.Zombie;
            Banner = Item.NPCtoBanner(NPCID.Zombie);
            BannerItem = Item.BannerToItem(Banner);
            NPC.GetGlobalNPC<RemnantGlobalNPC>().CanMakeCrit = CritBonus > 0 ? true : false;
            NPC.GetGlobalNPC<RemnantGlobalNPC>().CritChance = CritBonus;

        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (Main.hardMode) return SpawnCondition.OverworldNightMonster.Chance * 0f;
            else if (NPC.downedBoss3) return SpawnCondition.OverworldNightMonster.Chance * 0.02f;
            else if (NPC.downedBoss1) return SpawnCondition.OverworldNightMonster.Chance * 0.016f;
            else return SpawnCondition.OverworldNightMonster.Chance * 0.006f;
        }

        public override void HitEffect(int hitDirection, double damage)
        {
            for (int i = 0; i < 10; i++)
            {
                Dust dust = Dust.NewDustDirect(NPC.position, NPC.width, NPC.height, Main.rand.Next(139, 143));
                dust.velocity = new Vector2(Main.rand.NextFloat(-50f, 51f) * 1110.01f, Main.rand.NextFloat(-50f, 51f) * 111110.01f);
                dust.scale *= 1f + Main.rand.Next(-30, 31) * 0.01f;
            }
        }
    }

    public class ReinforcedIronZombie : BaseZombie
    {
        static int DefenseSetBonus => 2;
        static int Helmet => ContentSamples.ItemsByType[ModContent.ItemType<Reforced_iron_Helmet>()].defense;
        static int Chestplate => ContentSamples.ItemsByType[ModContent.ItemType<Reforced_iron_chesplate>()].defense;
        static int Greaves => ContentSamples.ItemsByType[ModContent.ItemType<Reforced_Iron_Greaves>()].defense;
        int Defense => DefenseSetBonus + Helmet + Chestplate + Greaves;
        static float AttackBonus => 1.02f;
        static int CritBonus = 0;
        public override void SetDefaults()
        {
            NPC.CloneDefaults(NPCID.Zombie);
            NPC.lifeMax = ContentSamples.NpcsByNetId[NPCID.Zombie].lifeMax;
            NPC.damage = (int)(ContentSamples.NpcsByNetId[NPCID.Zombie].damage * AttackBonus);
            NPC.defense += 0;
            NPC.value += 60f;
            NPC.knockBackResist = 0;
            NPC.aiStyle = 3;
            AIType = NPCID.Zombie;
            AnimationType = NPCID.Zombie;
            Banner = Item.NPCtoBanner(NPCID.Zombie);
            BannerItem = Item.BannerToItem(Banner);
            NPC.GetGlobalNPC<RemnantGlobalNPC>().CanMakeCrit = CritBonus > 0 ? true : false;
            NPC.GetGlobalNPC<RemnantGlobalNPC>().CritChance = CritBonus;
        }
        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            Item.NewItem(NPC.GetSource_Loot(), NPC.getRect(), ModContent.ItemType<Reinforced_iron_ore>());
        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (Main.hardMode) return SpawnCondition.OverworldNightMonster.Chance * 0f;
            else if (NPC.downedBoss3) return SpawnCondition.OverworldNightMonster.Chance * 0.02f;
            else if (NPC.downedBoss1) return SpawnCondition.OverworldNightMonster.Chance * 0.016f;
            else return SpawnCondition.OverworldNightMonster.Chance * 0.006f;
        }
        public override void AI()
        {
            NPC.TargetClosest(true);
            Vector2 targetPosition = Main.player[NPC.target].position;
            if (NPC.velocity.Y == 0)
            {
                NPC.velocity.X *= targetPosition.X < NPC.position.X ? 0.5f : 0.5f;//0.25
            }
        }
        public override void OnSpawn(IEntitySource source)
        {
            NPC.defense += Defense;
        }
    }


    public class Tuxonitezombie : BaseZombie
    {
        static int DefenseSetBonus = 0;
        static int Helmet => ContentSamples.ItemsByType[ModContent.ItemType<TuxoniteHelmet>()].defense;
        static int Chestplate => ContentSamples.ItemsByType[ModContent.ItemType<Tuxonite_chesplate>()].defense;
        static int Greaves => ContentSamples.ItemsByType[ModContent.ItemType<Tuxonite_Legging>()].defense;
        int Defense => DefenseSetBonus + Helmet + Chestplate + Greaves;
        static float AttackBonus = 1.00f;
        static int CritBonus = 5;
        public override void SetDefaults()
        {
            NPC.CloneDefaults(NPCID.Zombie);
            NPC.lifeMax = ContentSamples.NpcsByNetId[NPCID.Zombie].lifeMax;
            NPC.damage = (int)(ContentSamples.NpcsByNetId[NPCID.Zombie].damage * AttackBonus);
            NPC.defense += 0;
            NPC.value += 60f;
            NPC.knockBackResist = 0;
            NPC.aiStyle = 3;
            AIType = NPCID.Zombie;
            AnimationType = NPCID.Zombie;
            Banner = Item.NPCtoBanner(NPCID.Zombie);
            BannerItem = Item.BannerToItem(Banner);
            NPC.GetGlobalNPC<RemnantGlobalNPC>().CanMakeCrit = CritBonus > 0 ? true : false;
            NPC.GetGlobalNPC<RemnantGlobalNPC>().CritChance = CritBonus;
        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (Main.hardMode) return SpawnCondition.OverworldNightMonster.Chance * 0f;
            else if (NPC.downedBoss3) return SpawnCondition.OverworldNightMonster.Chance * 0.02f;
            else if (NPC.downedSlimeKing) return SpawnCondition.OverworldNightMonster.Chance * 0.016f;
            else return SpawnCondition.OverworldNightMonster.Chance * 0f;
        }
        public override void OnSpawn(IEntitySource source)
        {
            NPC.defense += Defense;
        }

    }

    public class Shadowzombie : BaseZombie
    {
        static int DefenseSetBonus = 0;
        static int Helmet = new Item(ItemID.ShadowHelmet).defense;
        static int Chestplate = new Item(ItemID.ShadowScalemail).defense;
        static int Greaves = new Item(ItemID.ShadowGreaves).defense;
        static int AttackBonus = 1;
        static int CritBonus = 15;//15
        int Defense = DefenseSetBonus + Helmet + Chestplate + Greaves;

        public override void SetDefaults()
        {
            NPC.CloneDefaults(NPCID.Zombie);
            NPC.lifeMax = ContentSamples.NpcsByNetId[NPCID.Zombie].lifeMax;
            NPC.damage = (int)(ContentSamples.NpcsByNetId[NPCID.Zombie].damage * AttackBonus);
            NPC.defense += Defense;
            NPC.value += 60f;
            NPC.knockBackResist = 0;
            NPC.aiStyle = 3;
            AIType = NPCID.Zombie;
            AnimationType = NPCID.Zombie;
            Banner = Item.NPCtoBanner(NPCID.Zombie);
            BannerItem = Item.BannerToItem(Banner);
            NPC.GetGlobalNPC<RemnantGlobalNPC>().CanMakeCrit = CritBonus > 0 ? true : false;
            NPC.GetGlobalNPC<RemnantGlobalNPC>().CritChance = CritBonus;
        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (Main.hardMode) return SpawnCondition.OverworldNightMonster.Chance * 0f;
            else if (NPC.downedBoss3) return SpawnCondition.OverworldNightMonster.Chance * 0.02f;
            else if (NPC.downedBoss1) return SpawnCondition.OverworldNightMonster.Chance * 0.016f;
            else return SpawnCondition.OverworldNightMonster.Chance * 0.0f;
        }
        public override void AI()
        {
            NPC.TargetClosest(true);
            Vector2 targetPosition = Main.player[NPC.target].position;
            if (NPC.velocity.Y == 0)
            {
                NPC.velocity.X += targetPosition.X < NPC.position.X ? -0.15f : 0.15f;
            }
        }

        //public override void HitEffect(int hitDirection, double damage)
        //{
        //	for (int i = 0; i < 10; i++)
        //	{
        //		Dust dust = Dust.NewDustDirect(NPC.position, NPC.width, NPC.height, Main.rand.Next(139, 143));
        //		dust.velocity = new Vector2(Main.rand.NextFloat(-50f, 51f) * 1110.01f, Main.rand.NextFloat(-50f, 51f) * 111110.01f);
        //		dust.scale *= 1f + Main.rand.Next(-30, 31) * 0.01f;
        //	}
        //}
    }

    public class Crimsonzombi : BaseZombie
    {

        static int DefenseSetBonus = 0;
        static float AttackBonus = 1.09f;
        static int Helmet = new Item(ItemID.CrimsonHelmet).defense;
        static int Chestplate = new Item(ItemID.CrimsonScalemail).defense;
        static int Greaves = new Item(ItemID.CrimsonGreaves).defense;
        int Defense = DefenseSetBonus + Helmet + Chestplate + Greaves;
        int CritBonus = 0;
        public override void SetDefaults()
        {
            NPC.CloneDefaults(NPCID.Zombie);
            NPC.lifeMax = ContentSamples.NpcsByNetId[NPCID.Zombie].lifeMax;
            NPC.damage = (int)(ContentSamples.NpcsByNetId[NPCID.Zombie].damage * AttackBonus);
            NPC.defense += Defense;
            NPC.value += 60f;
            NPC.knockBackResist = 0;
            NPC.aiStyle = 3;
            AIType = NPCID.Zombie;
            AnimationType = NPCID.Zombie;
            Banner = Item.NPCtoBanner(NPCID.Zombie);
            BannerItem = Item.BannerToItem(Banner);
            NPC.GetGlobalNPC<RemnantGlobalNPC>().CanMakeCrit = CritBonus > 0 ? true : false;
            NPC.GetGlobalNPC<RemnantGlobalNPC>().CritChance = CritBonus;
            NPC.lifeRegen = 100;
            NPC.lifeRegenCount = 2000;
        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (Main.hardMode) return SpawnCondition.OverworldNightMonster.Chance * 0f;
            else if (NPC.downedBoss3) return SpawnCondition.OverworldNightMonster.Chance * 0.02f;
            else if (NPC.downedBoss1) return SpawnCondition.OverworldNightMonster.Chance * 0.016f;
            else return SpawnCondition.OverworldNightMonster.Chance * 0f;
        }
        int RegenCounter = 0;
        public override void AI()
        {

            if (NPC.life < NPC.lifeMax)
            {
                if (RegenCounter == 190)
                {
                    NPC.life++;
                }
                if (RegenCounter >= 200)
                {
                    RegenCounter = 0;
                }
                else
                {
                    RegenCounter++;
                }
            }
        }
    }
    public class MoltenZombie : BaseZombie
    {

        static int DefenseSetBonus = 0;
        static float AttackBonus = 1.17f;
        static int Helmet = new Item(ItemID.MoltenHelmet).defense;
        static int Chestplate = new Item(ItemID.MoltenBreastplate).defense;
        static int Greaves = new Item(ItemID.MoltenGreaves).defense;
        int Defense = DefenseSetBonus + Helmet + Chestplate + Greaves;
        int CritBonus = 7;
        public override void SetDefaults()
        {
            NPC.CloneDefaults(NPCID.Zombie);
            NPC.lifeMax = ContentSamples.NpcsByNetId[NPCID.Zombie].lifeMax;
            NPC.damage = (int)(ContentSamples.NpcsByNetId[NPCID.Zombie].damage * AttackBonus);
            NPC.defense += Defense;
            NPC.value += 60f;
            NPC.knockBackResist = 0;
            NPC.aiStyle = 3;
            AIType = NPCID.Zombie;
            AnimationType = NPCID.Zombie;
            Banner = Item.NPCtoBanner(NPCID.Zombie);
            BannerItem = Item.BannerToItem(Banner);
            NPC.GetGlobalNPC<RemnantGlobalNPC>().CanMakeCrit = CritBonus > 0 ? true : false;
            NPC.GetGlobalNPC<RemnantGlobalNPC>().CritChance = CritBonus;
            NPC.buffImmune[BuffID.OnFire] = true;
        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (Main.hardMode)
            {
                if (RemnantDownedBossSystem.downedFrozen) return SpawnCondition.OverworldNightMonster.Chance * 0f;
                else return SpawnCondition.OverworldNightMonster.Chance * 0.2f;
            }
            else if (NPC.downedBoss3) return SpawnCondition.OverworldNightMonster.Chance * 0.016f;
            else return SpawnCondition.OverworldNightMonster.Chance * 0f;
        }
        public override void AI()
        {
        }
    }
    public class NightZombie : BaseZombie
    {
        static int DefenseSetBonus = 5;
        static int Helmet => ContentSamples.ItemsByType[ModContent.ItemType<NightHelmetMe>()].defense;
        static int Chestplate => ContentSamples.ItemsByType[ModContent.ItemType<Nightchesplate>()].defense;
        static int Greaves => ContentSamples.ItemsByType[ModContent.ItemType<NightLeggings>()].defense;
        int Defense => DefenseSetBonus + Helmet + Chestplate + Greaves;
        static float AttackBonus = 1.19f;
        static int CritBonus = 0;
        public override void SetDefaults()
        {
            NPC.CloneDefaults(NPCID.Zombie);
            NPC.lifeMax = ContentSamples.NpcsByNetId[NPCID.Zombie].lifeMax + 50;
            NPC.damage = (int)(ContentSamples.NpcsByNetId[NPCID.Zombie].damage * AttackBonus);
            NPC.defense += 0;
            NPC.value += 60f;
            NPC.knockBackResist = 0;
            NPC.aiStyle = 3;
            AIType = NPCID.Zombie;
            AnimationType = NPCID.Zombie;
            Banner = Item.NPCtoBanner(NPCID.Zombie);
            BannerItem = Item.BannerToItem(Banner);
            NPC.GetGlobalNPC<RemnantGlobalNPC>().CanMakeCrit = CritBonus > 0 ? true : false;
            NPC.GetGlobalNPC<RemnantGlobalNPC>().CritChance = CritBonus;
            NPC.buffImmune[BuffID.BrokenArmor] = true;
            NPC.buffImmune[BuffID.Shine] = true;
            NPC.buffImmune[BuffID.OnFire] = true;
        }
        public override void AI()
        {
            NPC.TargetClosest(true);
            Vector2 targetPosition = Main.player[NPC.target].position;
            if (NPC.velocity.Y == 0)
            {
                NPC.velocity.X += targetPosition.X < NPC.position.X ? -0.2f : 0.2f;
            }
        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (Main.hardMode)
            {
                if (NPC.downedMechBossAny) return SpawnCondition.OverworldNightMonster.Chance * 0f;
                else return SpawnCondition.OverworldNightMonster.Chance * 0.1f;
            }
            else if (NPC.downedBoss3) return SpawnCondition.OverworldNightMonster.Chance * 0.006f;
            else return SpawnCondition.OverworldNightMonster.Chance * 0f;
        }
        public override void OnSpawn(IEntitySource source)
        {
            NPC.defense += Defense;
        }
    }
    public class CobaltZombie : BaseZombie
    {

        static int DefenseSetBonus = 0;
        static float AttackBonus = 1.18f;
        static int Helmet = new Item(ItemID.CobaltHelmet).defense;
        static int Chestplate = new Item(ItemID.CobaltBreastplate).defense;
        static int Greaves = new Item(ItemID.CobaltLeggings).defense;
        int Defense = DefenseSetBonus + Helmet + Chestplate + Greaves;
        int CritBonus = 5;
        public override void SetDefaults()
        {
            NPC.CloneDefaults(NPCID.Zombie);
            NPC.lifeMax = ContentSamples.NpcsByNetId[NPCID.Zombie].lifeMax;
            NPC.damage = (int)(ContentSamples.NpcsByNetId[NPCID.Zombie].damage * AttackBonus);
            NPC.defense += Defense;
            NPC.value += 60f;
            NPC.knockBackResist = 0;
            NPC.aiStyle = 3;
            AIType = NPCID.Zombie;
            AnimationType = NPCID.Zombie;
            Banner = Item.NPCtoBanner(NPCID.Zombie);
            BannerItem = Item.BannerToItem(Banner);
            NPC.GetGlobalNPC<RemnantGlobalNPC>().CanMakeCrit = CritBonus > 0 ? true : false;
            NPC.GetGlobalNPC<RemnantGlobalNPC>().CritChance = CritBonus;
            NPC.buffImmune[BuffID.OnFire] = true;
        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (Main.hardMode)
            {
                if (RemnantDownedBossSystem.downedFrozen) return SpawnCondition.OverworldNightMonster.Chance * 0.16f;
                else return SpawnCondition.OverworldNightMonster.Chance * 0.2f;
            }
            else return SpawnCondition.OverworldNightMonster.Chance * 0f;
        }
        public override void AI()
        {
            NPC.TargetClosest(true);
            Vector2 targetPosition = Main.player[NPC.target].position;
            if (NPC.velocity.Y == 0)
            {
                NPC.velocity.X += targetPosition.X < NPC.position.X ? -0.2f : 0.2f;
            }
        }
    }
    public class PaladiumZombie : BaseZombie
    {

        static int DefenseSetBonus = 0;
        static float AttackBonus = 1.17f;
        static int Helmet = new Item(ItemID.PalladiumMask).defense;
        static int Chestplate = new Item(ItemID.PalladiumBreastplate).defense;
        static int Greaves = new Item(ItemID.PalladiumLeggings).defense;
        int Defense = DefenseSetBonus + Helmet + Chestplate + Greaves;
        int CritBonus = 3;

        public override void SetDefaults()
        {
            NPC.CloneDefaults(NPCID.Zombie);
            NPC.lifeMax = ContentSamples.NpcsByNetId[NPCID.Zombie].lifeMax;
            NPC.damage = (int)(ContentSamples.NpcsByNetId[NPCID.Zombie].damage * AttackBonus);
            NPC.defense += Defense;
            NPC.value += 60f;
            NPC.knockBackResist = 0;
            NPC.aiStyle = 3;
            AIType = NPCID.Zombie;
            AnimationType = NPCID.Zombie;
            Banner = Item.NPCtoBanner(NPCID.Zombie);
            BannerItem = Item.BannerToItem(Banner);
            NPC.GetGlobalNPC<RemnantGlobalNPC>().CanMakeCrit = CritBonus > 0 ? true : false;
            NPC.GetGlobalNPC<RemnantGlobalNPC>().CritChance = CritBonus;
            NPC.buffImmune[BuffID.OnFire] = true;
        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (Main.hardMode)
            {
                if (RemnantDownedBossSystem.downedFrozen) return SpawnCondition.OverworldNightMonster.Chance * 0.16f;
                else return SpawnCondition.OverworldNightMonster.Chance * 0.2f;
            }
            else return SpawnCondition.OverworldNightMonster.Chance * 0f;
        }
        int RegenCounter = 0;
        public override void AI()
        {

            if (NPC.localAI[1] == 1)
            {
                if (NPC.life < NPC.lifeMax)
                {
                    if (RegenCounter < Utils1.FormatTime(0, 0, 0, 4))
                    {
                        if (Main.rand.NextBool(2)) NPC.life++;
                        RegenCounter++;
                    }
                    else
                    {
                        RegenCounter = 0;
                        NPC.localAI[1] = 0;
                    }
                }
            }
            else
            {

            }
        }
        public override void ModifyHitPlayer(Player target, ref int damage, ref bool crit)
        {
            NPC.localAI[1] = 1;
        }
    }
    public abstract class BaseZombie : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Armored Zombie");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Zombi blindado");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Zombie blindé");
            Main.npcFrameCount[NPC.type] = Main.npcFrameCount[NPCID.Zombie];
        }
        public override void HitEffect(int hitDirection, double damage)
        {
            for (int i = 0; i < 10; i++)
            {
                Dust dust = Dust.NewDustDirect(NPC.position, NPC.width, NPC.height, Main.rand.Next(139, 143));
                dust.velocity = new Vector2(Main.rand.NextFloat(-50f, 51f) * 1110.01f, Main.rand.NextFloat(-50f, 51f) * 111110.01f);
                dust.scale *= 1f + Main.rand.Next(-30, 31) * 0.01f;
            }
        }
    }
}