using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Content.NPCs
{
    public class MiniDuke : ModNPC
    {
        public override string Texture => "Terraria/Images/NPC_" + NPCID.DukeFishron;
        public override void SetStaticDefaults()
        {
           // //DisplayName.SetDefault("Fishron specter");
           // //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Espectro fishron");
           // //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Spectre de poissonron");
            Main.npcFrameCount[Type] = Main.npcFrameCount[NPCID.DukeFishron];
            NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
            {
                Velocity = 1f
            };
            NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, value);
            NPC.spriteDirection = NPC.direction;
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
