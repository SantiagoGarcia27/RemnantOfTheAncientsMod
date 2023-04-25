using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.NPCs
{
    public class IGolem : ModNPC
    {
        public override string Texture => "Terraria/Images/NPC_" + NPCID.IceGolem;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ice Golem");

            Main.npcFrameCount[NPC.type] = Main.npcFrameCount[NPCID.IceGolem];
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
