using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using opswordsII.Items.tresure_bag;
using opswordsII.Items.Mele;
using opswordsII.Items.Ranger;
using opswordsII.Items.Magic;
using opswordsII.Items.Summon;
using static Terraria.ModLoader.ModContent;
using Microsoft.Xna.Framework;
using opswordsII.Projectiles;
using Terraria.GameContent.ItemDropRules;
using opswordsII.Common.Systems;
using Terraria.Audio;
using opswordsII.VanillaChanges;
using opswordsII.World;

namespace opswordsII.NPCs.frozen_assaulter
{
    [AutoloadBossHead]
    public class frozen_assaulter : ModNPC
    {
        int invincibilityTimer = 1;
        bool healAnimation = false;
        bool fase3 = false;
        int currentPhase = 1;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Frozen Assaulter");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Asaltante Congelado");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Agresseur gelé");
            Main.npcFrameCount[NPC.type] = 8;
        }
        public override void SetDefaults()
        {
            NPC.aiStyle = 5;  //15 is the king AI
            NPC.lifeMax = (int)NpcChanges1.ExpertLifeScale(18500, "MyBoss");   //boss life
            NPC.damage = (int)NpcChanges1.ExpertDamageScale(90, "MyBoss");  //boss damage
            NPC.defense = 15;    //boss defense
            NPC.knockBackResist = 0f;
            NPC.width = 100;
            NPC.height = 100;  //this boss will behavior like the DemonEye
            Main.npcFrameCount[NPC.type] = 8;   //this boss will behavior like the DemonEye
            NPC.value = Item.buyPrice(0, 5, 75, 45);
            NPC.npcSlots = 10f;
            NPC.boss = true;
            NPC.lavaImmune = true;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.buffImmune[24] = true; 
           // Music = MusicLoader.GetMusicSlot(Mod,"Beta/Mod Sources/opswordsII/Sounds/Music/Frozen_Assaulter");
            NPC.netAlways = true;
            

            /*Mod CalamityMod = ModLoader.GetMod("CalamityMod");
    		if (CalamityMod != null)
            NPC.lifeMax = 19500;*/
        }

        public override void AI() //this is where you program your AI
        {
            Player P = Main.player[NPC.target];
            float distance = NPC.Distance(Main.player[NPC.target].Center);

            if (NPC.target < 0 || NPC.target == 255 || Main.player[NPC.target].dead || !Main.player[NPC.target].active) NPC.TargetClosest(true);
            NPC.netUpdate = true;
            NPC.ai[0]++;

            NPC.ai[1]++;
            phaseChanger();
            if (!Reaper.ReaperMode)
            {
                if (NPC.ai[1] >= 5) shootIa((int)NpcChanges1.ExpertDamageScale(50, "MyBoss"), "Frozenp", P, 20f);
                if (NPC.ai[1] >= 115)
                {
                    shootIa((int)NpcChanges1.ExpertDamageScale(30, "MyBoss"), "Lazer", P, 20f);
                    NPC.ai[1] = 0;
                }
                NPC.ai[2]++;

            }
            else
            {
                if (NPC.ai[1] >= 20) shootIa((int)NpcChanges1.ExpertDamageScale(180, "MyBoss"), "Frozenp", P, 40f);
                if (NPC.ai[1] >= 115) shootIa((int)NpcChanges1.ExpertDamageScale(230, "MyBoss"), "Lazer", P, 30f);
                NPC.ai[2]++;
            }

            if (NPC.ai[2] % 600 == 3)
            {  //Npc spown rate  // 230 is projectile fire rate
                NPC.NewNPC(NPC.GetSource_FromAI(),(int)NPC.position.X, (int)NPC.position.Y, NPCID.IceElemental);
            }
            if (Main.expertMode)
            {
                if (NPC.life < NPC.lifeMax / 2)
                {
                    if (NPC.ai[2] % 900 == 1)
                    {
                        NPC.NewNPC(NPC.GetSource_FromAI(),(int)NPC.position.X, (int)NPC.position.Y, NPCType<IGolem>());
                    }
                }
            }
            if (NPC.ai[2] == 950)
            {
                NPC.ai[2] = 0;
            }

            NPC.ai[3]++;

            if (NPC.life < NPC.lifeMax / 4)
            {
                phaseChanger();
                fase3 = true;
            }
            if (Main.expertMode)
            {

                if (fase3)
                {

                   // music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/Frozen Theme p2 ");
                    NPC.dontTakeDamage = true;

                    if (NPC.ai[3] > 388) NPC.Center = Main.player[NPC.target].Center + new Vector2(Main.rand.Next(-250 * 2, 150 * 2), Main.rand.Next(-250 * 2, 150 * 2));
                    if (invincibilityTimer <= 1304) invincibilityTimer++;
                    if (invincibilityTimer >= 1300) NPC.dontTakeDamage = false;
                    if (invincibilityTimer >= 1303)
                    {
                        if (!healAnimation)
                        {
                            if (!Reaper.ReaperMode)
                            {
                                for (int i = NPC.lifeMax / 4; i <= NPC.lifeMax / 2; i++)
                                {
                                    NPC.life = i;
                                }
                            }
                            else
                            {
                                for (int i = NPC.lifeMax / 4; i <= NPC.lifeMax / 1.5; i++)
                                {
                                    NPC.life = i;
                                }
                            }
                            healAnimation = true;
                        }
                    }

                }
                //npc.Center = new Vector2(Main.rand.Next(-5 * 1, 190 * 2), Main.rand.Next(-100 * 2, 200 * 2));}  
                if (NPC.ai[3] == 390)
                {
                    NPC.ai[3] = 0;
                }
            }
        }
        public void shootIa(int dammage,string tipo,Player P,float speed)
        {
            if (tipo == "Frozenp")
            {
                float Speed = speed;  //projectile speed //20f normal 
                int damage = dammage;  //projectile damage
                Vector2 vector8 = new Vector2(NPC.position.X + (NPC.width / 2), NPC.position.Y + (NPC.height / 2));
                int type = ModContent.ProjectileType<Frozenp>();  //put your projectile
                float rotation = (float)Math.Atan2(vector8.Y - (P.position.Y + (P.height * 0.5f)), vector8.X - (P.position.X + (P.width * 0.5f)));
                int num54 = Projectile.NewProjectile(NPC.GetSource_FromAI(), vector8.X, vector8.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1), type, 10, 0f, 0);
            }
            else if(tipo == "Lazer")
            {
                float Speed = speed;  //projectile speed //20f normal 
                Vector2 vector8 = new Vector2(NPC.position.X + (NPC.width / 2), NPC.position.Y + (NPC.height / 2));
                int damage = dammage;  //projectile damage
                int type = ProjectileID.FrostBeam;  //put your projectile
                float rotation = (float)Math.Atan2(vector8.Y - (P.position.Y + (P.height * 0.5f)), vector8.X - (P.position.X + (P.width * 0.5f)));
                int num54 = Projectile.NewProjectile(NPC.GetSource_FromAI(), vector8.X, vector8.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1), type, 10, 0f, 0);
            }
        }
        public void phaseChanger()
        {
            if (NPC.life > NPC.lifeMax / 2)
            {
                currentPhase = 1;
            }
            else if (NPC.life < NPC.lifeMax / 2 && !fase3)
            {
                currentPhase = 2;
            }
            else if (NPC.life < NPC.lifeMax / 4)
            {
                if (!healAnimation)
                {
                    currentPhase = 3;
                    fase3 = true;
                }
                else
                {
                    currentPhase = 4;
                }
            }

        }
        public override void BossLoot(ref string name, ref int potionType)
        {
            DownedBossSystem.downedFrozen = true;
            potionType = ItemID.LesserHealingPotion;
            Item.NewItem(NPC.GetSource_Loot(),(int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, ItemID.IceBlock, 50);
}
        public override void ModifyNPCLoot(NPCLoot npcLoot) {
            if (!Main.expertMode || Main.masterMode){
                int choice = Main.rand.Next(3);
                int item = 0;
                switch (choice) {    
                    case 0:
                        Item.NewItem(NPC.GetSource_Loot(),(int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, ItemType<FrostShark>(), 1);
                        break;
                    case 1:
                        Item.NewItem(NPC.GetSource_Loot(),(int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, ItemType<Permafrost>(), 1);
                        break;
                    case 2:
                        Item.NewItem(NPC.GetSource_Loot(),(int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, ItemType<FrozenStafff>(), 1);
                        break;
                    case 3:
                        Item.NewItem(NPC.GetSource_Loot(),(int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, ItemType<frozen_staff>(), 1);
                        break;
                }
             }
            else {
                npcLoot.Add(ItemDropRule.BossBag(ModContent.ItemType<frostBag>()));
            }
     }
        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            NPC.lifeMax = (int)(NPC.lifeMax * 0.6f * bossLifeScale);  //boss life scale in expertmode
            NPC.damage = (int)(NPC.damage * 0.6f);  //boss damage increase in expermode


        }
        private const int Frame_static = 0;
		private const int Frame_1 = 1;
		private const int Frame_2 = 2;
		private const int Frame_3 = 3;
		private const int Frame_Breack_0 = 4;
		private const int Frame_Breack_1 = 5;
        private const int Frame_Breack_2 = 6;
        private const int Frame_Breack_3 = 7;

		// Here in FindFrame, we want to set the animation frame our NPC will use depending on what it is doing.
		// We set NPC.frame.Y to x * frameHeight where x is the xth frame in our spritesheet, counting from 0. For convenience, I have defined some consts above.
		public override void FindFrame(int frameHeight) {
			// This makes the sprite flip horizontally in conjunction with the NPC.direction.
			NPC.spriteDirection = NPC.direction;

			// For the most part, our animation matches up with our states.
			    
                
                if (NPC.life > NPC.lifeMax/4){
                     if (NPC.frameCounter < 3){
				         NPC.frame.Y = Frame_static * frameHeight;
                         NPC.frameCounter++;
                     }
			         else if (NPC.frameCounter < 6) {
					    NPC.frame.Y = Frame_1 * frameHeight;
                        NPC.frameCounter++;
                    }
				    else if (NPC.frameCounter < 9) {
					    NPC.frame.Y = Frame_2 * frameHeight;
                        NPC.frameCounter++;
				    }
                     else if (NPC.frameCounter < 12) {
				    	NPC.frame.Y = Frame_3 * frameHeight;
                        NPC.frameCounter++;
				    }
                    else{
                        NPC.frameCounter = 0;
                    }
                }

                if(NPC.life < NPC.lifeMax/4){
			        if (NPC.frameCounter < 3) {
				         NPC.frame.Y = Frame_Breack_0 * frameHeight;
                         NPC.frameCounter++;
                    }
                    else if (NPC.frameCounter < 5) {
				    	NPC.frame.Y = Frame_Breack_1 * frameHeight;
                        NPC.frameCounter++;
				    }
                    else if (NPC.frameCounter < 7) {
					    NPC.frame.Y = Frame_Breack_2 * frameHeight;
                        NPC.frameCounter++;
                    }
                    else if (NPC.frameCounter < 9) {
					    NPC.frame.Y =Frame_Breack_3 * frameHeight;
                        NPC.frameCounter++;
			        }
                    else{
                    NPC.frameCounter=0;
                    }
                }
		    }		
	    }
    }
	

    
