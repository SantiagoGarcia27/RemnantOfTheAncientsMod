using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.Chat;
using Microsoft.Xna.Framework;

namespace RemnantOfTheAncientsMod.Content.NPCs
{
	public class SuperDummyNPC : ModNPC
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Super Dummy");
			//DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Super factice");
			//DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Super muñeco de pruebas");
			Main.npcFrameCount[Type] = Main.npcFrameCount[NPCID.BlueSlime];
            NPCID.Sets.CantTakeLunchMoney[Type] = true;
            NPC.spriteDirection = NPC.direction;
		}

		public override void SetDefaults()
		{
			NPC.width = 32;
			NPC.height = 32;
			NPC.damage = 0;
			NPC.defense = 0;
			NPC.lifeMax = 9999999;
			NPC.HitSound = SoundID.NPCHit1;
			NPC.DeathSound = SoundID.NPCDeath1;
			NPC.friendly = false;
			AIType = -1;
			AnimationType = NPCID.BlueSlime;
            NPC.netAlways = true;
            NPC.aiStyle = 0;
            NPC.knockBackResist = 0;
        }
        public override void UpdateLifeRegen(ref int damage)
        {
            NPC.lifeRegen += 2000000;
        }
        public override void AI()
        {
			if (NPC.ai[0] == 0) 
			{
                NPC.defense = 0;
            }
			else if (NPC.ai[0] == 1)
            {
                NPC.defense = Main.player[Main.myPlayer].statDefense;	
            }
            else if (NPC.ai[0] == 2)
            {
				NPC.defense = 0;
                NPC.width = 230;
                NPC.height = 230;
                NPC.scale = 10;
            }
            else if (NPC.ai[0] == 3)
            {
				NPC.defense = Main.player[Main.myPlayer].statDefense;
                NPC.width = 230;
                NPC.height = 230;
                NPC.scale = 10;
            }
        }
        public override void OnKill()
        {
            ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral("Small with player defense"), Color.Red);
            base.OnKill();
        }
      
        public override bool CheckDead()
        {
            if (NPC.lifeRegen < 0)
            {
                NPC.life = NPC.lifeMax;
                return false;
            }
            return true;
        }
    }
}
