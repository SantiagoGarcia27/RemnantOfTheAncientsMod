using RemnantOfTheAncientsMod.Content.NPCs.Bosses.DAniquilator;
using RemnantOfTheAncientsMod.Content.NPCs.Bosses.FrozenAssaulter;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Content.NPCs
{
    public class IGolem : ModNPC
    {
        public override string Texture => "Terraria/Images/NPC_" + NPCID.IceGolem;

        public override void SetStaticDefaults()
        {
            // //DisplayName.SetDefault("Ice Golem");
            NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers()
            {
                // Influences how the NPC looks in the Bestiary
                Velocity = 1f // Draws the NPC in the bestiary as if its walking +1 tiles in the x direction
            };
            NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, value);
            Main.npcFrameCount[NPC.type] = Main.npcFrameCount[NPCID.IceGolem];
        }
        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            // Makes it so whenever you beat the boss associated with it, it will also get unlocked immediately
            int associatedNPCType = ModContent.NPCType<FrozenAssaulter>();
            bestiaryEntry.UIInfoProvider = new CommonEnemyUICollectionInfoProvider(ContentSamples.NpcBestiaryCreditIdsByNpcNetIds[associatedNPCType], quickUnlock: true);

            bestiaryEntry.Info.AddRange(new List<IBestiaryInfoElement> {
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Snow,
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.NightTime,
                // Plain black background
				new FlavorTextBestiaryInfoElement("A minion protecting his boss from taking damage by sacrificing itself. If none are alive, the boss is exposed to damage.")
            });
        }

        public override void SetDefaults()
        {

            NPC.CloneDefaults(NPCID.IceGolem);
            AIType = NPCID.IceGolem;
            AnimationType = NPCID.IceGolem;
            NPC.aiStyle = 3;
            NPC.value = Item.buyPrice(0, 0, 0, 0);
            NPC.lifeMax -= 4000;
        }
    }
}
