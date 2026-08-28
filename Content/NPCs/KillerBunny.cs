using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.ItemDropRules;
using Terraria.ModLoader.Utilities;
using Terraria.Localization;
using RemnantOfTheAncientsMod.Content.Items.Items;
using Terraria.GameContent.Bestiary;
using Microsoft.Xna.Framework;

namespace RemnantOfTheAncientsMod.Content.NPCs
{
	public class KillerBunny : ModNPC
	{
		public override void SetStaticDefaults()
		{
			
			Main.npcFrameCount[Type] = Main.npcFrameCount[NPCID.CorruptBunny];
            NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers()
            {
               
                Velocity = 1f
            };
            NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, value);
            NPC.spriteDirection = NPC.direction;
		}

		public override void SetDefaults()
		{
            NPC.CloneDefaults(NPCID.CorruptBunny);

            NPC.height = 32;
            NPC.width = 36;
			NPC.height = 32;
			NPC.damage = 25;
			NPC.defense = 2;
			NPC.lifeMax = 50;
			NPC.HitSound = SoundID.NPCHit1;
			NPC.DeathSound = SoundID.NPCDeath1;
			NPC.value = 25f;
			NPC.knockBackResist = 0.2f;

            AnimationType = NPCID.CorruptBunny;
            Banner = Item.NPCtoBanner(NPCID.Bunny); 
			BannerItem = Item.BannerToItem(Banner);
		}
        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {   
            bestiaryEntry.Info.AddRange([
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface,
				new FlavorTextBestiaryInfoElement("A strange bunny with a strange diet"),
            ]);
        }
        public override void ModifyNPCLoot(NPCLoot NPCLoot)
		{
			NPCLoot.Add(ItemDropRule.Common(ItemID.BunnyHood,5));
		}

		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			return SpawnCondition.OverworldDayBirdCritter.Chance * 1.1f;
        }
        public override void AI()
        {
            NPC.TargetClosest();
        }
        public override void OnKill()
        {       
            if (Main.netMode != NetmodeID.Server)
            {
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, new Vector2(Main.rand.Next(-6, 7), Main.rand.Next(-6, 7)), 76, NPC.scale);
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, new Vector2(Main.rand.Next(-6, 7), Main.rand.Next(-6, 7)), 77, NPC.scale);;
            }          
            base.OnKill();
        }
    }
}
