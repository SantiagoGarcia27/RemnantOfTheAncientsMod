using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using opswordsII.Buffs;
using opswordsII.Items.tresure_bag;
using opswordsII.Items.Items;
using static Terraria.ModLoader.ModContent;
using Microsoft.Xna.Framework;
using opswordsII.Common.Systems;
using Terraria.GameContent.ItemDropRules;
using opswordsII.Projectiles;
using System;
using Terraria.Audio;

namespace opswordsII.NPCs.DAniquilator
{
    [AutoloadBossHead]
    public class DesertAniquilator : ModNPC
    {
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Desert Annihilator");
           // DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Aniquilador del desierto");
			// DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Aniquilateur du désert");
				Main.npcFrameCount[NPC.type] = Main.npcFrameCount[NPCID.BlueSlime];

                /*
                 DisplayName.AddTranslation(GameCulture.Russian, "Пустынный Аниквилятор");
			   DisplayName.AddTranslation(GameCulture.Chinese, "沙漠杀手"); */
		}
        public override void SetDefaults()
        {
            NPC.aiStyle = 15;  //15 is the king AI
            NPC.lifeMax = 3500;   //boss life
            NPC.damage = 30;  //boss damage
            NPC.defense = 10;    //boss defense
            NPC.knockBackResist = 0f;
            NPC.width = 100;
            NPC.height = 100;
            AnimationType = NPCID.BlueSlime;   //this boss will behavior like the DemonEye
            NPC.value = Item.buyPrice(0, 2, 75, 45);
            NPC.npcSlots = 30f;
            NPC.boss = true;  
            NPC.lavaImmune = true;
            NPC.noGravity = false;
            NPC.noTileCollide = false;
            NPC.HitSound = SoundID.NPCHit1;
			NPC.DeathSound = SoundID.NPCDeath1;
            NPC.buffImmune[24] = true;
           // Music = MusicLoader.GetMusicSlot(Mod, "Sounds/Music/Desert_Aniquilator");
            NPC.netAlways = true;


           /* Mod CalamityMod = ModLoader.GetMod("CalamityMod");
    		if (CalamityMod != null)

            NPC.damage = 40;
            NPC.lifeMax = 4500;*/
        }
       
        public override void AI() {
            
            float distance = NPC.Distance(Main.player[NPC.target].Center);
           
           NPC.ai[0] = 10;//1 = bunny hop //10 = fr0zen 2 

            NPC.ai[1]++;
           Player player = Main.player[NPC.target];

            
            if (NPC.target < 0 || NPC.target == 255 || Main.player[NPC.target].dead || !Main.player[NPC.target].active)
            {
                NPC.TargetClosest(true);
            }
            NPC.netUpdate = true; 

        if (NPC.ai[1] > 380 && distance  >= 100*16)  //120 =  2s 
        {
                NPC.Center = player.Center;
        }
        if (NPC.ai[1] > 799 && distance  >= 5*16 && distance  >= 1*16)  //120 =  2s 
        {
            NPC.Center = Main.player[NPC.target].Center + new Vector2(Main.rand.Next(-250 * 2, 150 * 2), Main.rand.Next(-250 * 2, 150 * 2));
         }
        
    
        if (NPC.ai[1] > 800){//400

            NPC.ai[1] = 0;
        }


        NPC.ai[2]++;




       if(player.ZoneDesert){
           if (NPC.ai[2] % 356 == 15)  //NPC spown rate 56
 
            {
                NPC.NewNPC(NPC.GetSource_FromAI(),(int)NPC.position.X, (int)NPC.position.Y, ModContent.NPCType<DSlime>()); 
            }
             if (NPC.ai[2] % 600 == 100)
             {
                NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, NPCID.TombCrawlerHead);
             }
              if (NPC.ai[2] % 300 == 100)
             {
                NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, ModContent.NPCType<FlyingAntlionD>());
                 NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, ModContent.NPCType<WalkingAntlionD>());
             
             }
             if (NPC.life < NPC.lifeMax/2){
                if (NPC.ai[3] % 900 == 900){
                        NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, NPCID.DuneSplicerHead);
                }
             }
                 if (Main.expertMode) {
             
                if (NPC.ai[2] % 300 == 300){ 
               NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, ModContent.NPCType<AntlionD>());
               NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, ModContent.NPCType<VultureD>());
                }
               if (NPC.ai[2] % 600 == 300){ 
                NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, NPCID.DesertScorpionWalk);
                }
       }
       }
       else {
        if (NPC.ai[2] % 356 == 15)  //NPC spown rate 56
 
            {
                NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, ModContent.NPCType<DSlime>()); 
            }
             if (NPC.ai[2] % 600 == 200)
             {
                NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, NPCID.TombCrawlerHead);
             }
              if (NPC.ai[2] % 300 == 200)
             {
                NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, ModContent.NPCType<FlyingAntlionD>());
                 NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, ModContent.NPCType<WalkingAntlionD>());
             
             }
             if (NPC.life < NPC.lifeMax/2){
                if (NPC.ai[3] % 900 == 700){
                        NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, NPCID.DuneSplicerHead);
                }
             }
                 if (Main.expertMode) {
             
                if (NPC.ai[2] % 300 == 200){ 
               NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, ModContent.NPCType<AntlionD>());
               NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, ModContent.NPCType<VultureD>());
                }
               if (NPC.ai[2] % 600 == 200){ 
                NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, NPCID.DesertScorpionWalk);
                }
                 }
                }
                     if (Main.expertMode) {
                     if (NPC.ai[2] % 600 == 200){//90
                         float Speed = 12f;
                         int damage = 30;
                         Vector2 vector8 = new Vector2(NPC.position.X + (NPC.width / 2), NPC.position.Y + (NPC.height / 2));
                         int type = ModContent.ProjectileType<DesertTyphoonE>();
                        SoundEngine.PlaySound(2, (int) NPC.position.X, (int) NPC.position.Y, 17);
                        float rotation = (float) Math.Atan2(vector8.Y-(Main.player[NPC.target].position.Y+(Main.player[NPC.target].height * 0.5f)), vector8.X-(Main.player[NPC.target].position.X+(Main.player[NPC.target].width * 0.5f)));
                        int num54 = Projectile.NewProjectile(NPC.GetSource_FromAI(),vector8.X, vector8.Y,(float)(Math.Cos(rotation) * Speed*-1),(float)((Math.Sin(rotation) * Speed)*-1), type, damage, 0f, 0);
                         NPC.ai[2] = 0;
                   }
                     }
        }   

        public override void OnHitPlayer(Player player, int damage, bool crit) {
			if (Main.rand.NextBool(3)) {
				player.AddBuff(BuffType<QuemaduraArenosa>(), 100, true);
			}
		}
        public override void BossLoot(ref string name, ref int potionType)
        {
            DownedBossSystem.downedDesert = true;
            potionType = ItemID.LesserHealingPotion;  
            Item.NewItem(NPC.GetSource_Loot(),(int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, ItemID.SandBlock, 60);
            Item.NewItem(NPC.GetSource_Loot(),(int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, ItemType<Sand_escense>(), 10);
            
             //boss drops
        }
        public override void ModifyNPCLoot(NPCLoot npcLoot) 
        {
            if (!Main.expertMode){
                int choice = Main.rand.Next(4);
                int item = 0;
                switch (choice) {
                    
                    case 0:
                        npcLoot.Add(ItemDropRule.Common(ItemType<Items.Ranger.desertbow>(), 1));
                        break;
                    case 1:
                        npcLoot.Add(ItemDropRule.Common(ItemType<Items.Mele.DesertEdge>(), 1));
                        break;
                    case 2:
                        npcLoot.Add(ItemDropRule.Common(ItemType<Items.Summon.DesertStaff>(), 1));
                        break;
                    case 3:
                        npcLoot.Add(ItemDropRule.Common(ItemType<Items.Magic.DesertTome>(), 1));
                        break;
                }
            }
            else {
                npcLoot.Add(ItemDropRule.BossBag(ModContent.ItemType<desertBag>()));
            }

		}	
        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            NPC.lifeMax = (int)(NPC.lifeMax * 0.6f * bossLifeScale);  //boss life scale in expertmode
            NPC.damage = (int)(NPC.damage * 0.6f);  //boss damage increase in expermode
        
        
        }
   
    }
}