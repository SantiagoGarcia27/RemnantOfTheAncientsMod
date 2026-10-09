using System.Collections.Generic;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Content.NPCs
{
    public class MiniDuke : ModNPC
    {
        public override string Texture => "Terraria/Images/NPC_" + NPCID.DukeFishron;
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[Type] = Main.npcFrameCount[NPCID.DukeFishron];
            NPCID.Sets.NPCBestiaryDrawModifiers value = new()
            {
                Velocity = 1f
            };
            NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, value);
            NPC.spriteDirection = NPC.direction;
        }
        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            // Makes it so whenever you beat the boss associated with it, it will also get unlocked immediately
            int associatedNPCType = NPCID.DukeFishron;
            bestiaryEntry.UIInfoProvider = new CommonEnemyUICollectionInfoProvider(ContentSamples.NpcBestiaryCreditIdsByNpcNetIds[associatedNPCType], quickUnlock: true);

            bestiaryEntry.Info.AddRange(new List<IBestiaryInfoElement> {
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Ocean,         
                // Plain black background
				new FlavorTextBestiaryInfoElement("A minion protecting his boss from taking damage by sacrificing itself. If none are alive, the boss is exposed to damage.")
            });
        }
        public override void SetDefaults()
        {
            NPC.CloneDefaults(NPCID.DukeFishron);
            NPC.lifeMax = 1000;
            NPC.damage /= 4;
            NPC.alpha = 0;
            NPC.boss = false;
            NPC.scale = 0.4f;
            NPC.value = 0;
        }
        public override void AI()
        {
            base.AI();
            NPC.alpha = 100;
        }
    }
}
