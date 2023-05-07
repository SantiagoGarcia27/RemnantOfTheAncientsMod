using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Content.NPCs
{
    public class AntlionD : ModNPC
    {
        public override string Texture => "Terraria/Images/NPC_" + NPCID.Antlion;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Antlion");

            Main.npcFrameCount[NPC.type] = Main.npcFrameCount[NPCID.Antlion];
        }


        public override void SetDefaults()
        {

            NPC.aiStyle = 19;
            AIType = NPCID.Antlion;
            AnimationType = NPCID.Antlion;
            NPC.CloneDefaults(NPCID.Antlion);
            NPC.value = Item.buyPrice(0, 0, 0, 0);
            NPC.lifeMax = 50;
        }
    }

    public class VultureD : ModNPC
    {
        public override string Texture => "Terraria/Images/NPC_" + NPCID.Vulture;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Vulture");

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
    }
    public class WalkingAntlionD : ModNPC
    {
        public override string Texture => "Terraria/Images/NPC_" + NPCID.WalkingAntlion;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Antlion Charger");

            Main.npcFrameCount[NPC.type] = Main.npcFrameCount[NPCID.WalkingAntlion];
        }


        public override void SetDefaults()
        {

            NPC.aiStyle = 3;
            AIType = NPCID.WalkingAntlion;
            AnimationType = NPCID.WalkingAntlion;
            NPC.CloneDefaults(NPCID.WalkingAntlion);
            NPC.value = Item.buyPrice(0, 0, 0, 0);
            NPC.lifeMax -= 90;
        }
    }
    public class DesertScorpionD : ModNPC
    {
        public override string Texture => "Terraria/Images/NPC_" + NPCID.DesertScorpionWalk;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Sand Poacher");

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
    }
    public class DSlime : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Servant of Desert Annihilator");
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
    }
    public class FlyingAntlionD : ModNPC
    {
        public override string Texture => "Terraria/Images/NPC_" + NPCID.FlyingAntlion;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Antlion Swarmer");

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
    }
}