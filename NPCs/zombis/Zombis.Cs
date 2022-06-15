using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using Microsoft.Xna.Framework;
using Terraria.ModLoader.Utilities;
using opswordsII.Items.Items;

namespace opswordsII.NPCs.zombis
{
    public class Fastzombie : ModNPC
	{
		public override void SetStaticDefaults()
		{
			 
			 DisplayName.SetDefault("weak zombie");
			 DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "zombie débil");
			 DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Zombie faible");
			 Main.npcFrameCount[Type] = Main.npcFrameCount[NPCID.Zombie];

			/*
			DisplayName.AddTranslation(GameCulture.Russian, "слабый зомби");
			   DisplayName.AddTranslation(GameCulture.Chinese, "弱僵尸");*/
		}

		public override void SetDefaults()
		{
			NPC.width = 18;
			NPC.height = 40;
			NPC.damage = 15;
			NPC.defense = 5;
			NPC.lifeMax = 10;
			NPC.HitSound = SoundID.NPCHit1;
			NPC.DeathSound = SoundID.NPCDeath2;
			NPC.value = 60f;
			NPC.knockBackResist = 1.2f;
			NPC.aiStyle = 3;
			AIType = NPCID.Zombie;
			AnimationType = NPCID.Zombie;
			Banner = Item.NPCtoBanner(NPCID.Zombie);
			BannerItem = Item.BannerToItem(Banner);
		}
        public override void ModifyNPCLoot(NPCLoot NPCLoot)
            {
	          Item.NewItem(NPC.GetSource_Loot(),NPC.getRect(),ItemID.Gel);
			}	
		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			if(NPC.downedBoss1 == false){
			return SpawnCondition.OverworldNightMonster.Chance * 0.03f;
            }
            else{
            return SpawnCondition.OverworldNightMonster.Chance * 0.001f;
            }
		}

		public override void HitEffect(int hitDirection, double damage)
		{
			for (int i = 0; i < 10; i++)
			{
				int dustType = Main.rand.Next(139, 143);
				int dustIndex = Dust.NewDust(NPC.position, NPC.width, NPC.height, dustType);
				Dust dust = Main.dust[dustIndex];
				dust.velocity.X = dust.velocity.X + Main.rand.Next(-50, 51) * 1110.01f;
				dust.velocity.Y = dust.velocity.Y + Main.rand.Next(-50, 51) * 111110.01f;
				dust.scale *= 1f + Main.rand.Next(-30, 31) * 0.01f;
			}
		}
	}

    public class Copperzombie : ModNPC
    {
		public override void SetStaticDefaults()
		{
				DisplayName.SetDefault("Armored Zombie");
			 DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Zombi blindado");
			 DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Zombie blindé");
			Main.npcFrameCount[NPC.type] = Main.npcFrameCount[NPCID.Zombie];


			/*
			DisplayName.AddTranslation(GameCulture.Russian, "Бронированный зомби");
			   DisplayName.AddTranslation(GameCulture.Chinese, "装甲僵尸");*/
		}

		public override void SetDefaults()
		{
			NPC.CloneDefaults(NPCID.Zombie);
			NPC.width = 18;
			NPC.height = 40;
			NPC.damage = 28;
			NPC.defense = 12;
			NPC.lifeMax = 59;
			NPC.HitSound = SoundID.NPCHit1;
			NPC.DeathSound = SoundID.NPCDeath2;
			NPC.value = 60f;
			NPC.knockBackResist = 0.2f;
			NPC.aiStyle = 3;
			AIType = NPCID.Zombie;
			AnimationType = NPCID.Zombie;
			Banner = Item.NPCtoBanner(NPCID.Zombie);
			BannerItem = Item.BannerToItem(Banner);

		}
        public override void ModifyNPCLoot(NPCLoot NPCLoot)
            {
			}	
		public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
		if(NPC.downedBoss1 == false){
			return SpawnCondition.OverworldNightMonster.Chance * 0.02f;
            }
            else{
            return SpawnCondition.OverworldNightMonster.Chance * 0.01f;
            }
        }
		public override void HitEffect(int hitDirection, double damage)
		{
			for (int i = 0; i < 10; i++)
			{
				int dustType = Main.rand.Next(139, 143);
				int dustIndex = Dust.NewDust(NPC.position, NPC.width, NPC.height, dustType);
				Dust dust = Main.dust[dustIndex];
				dust.velocity.X = dust.velocity.X + Main.rand.Next(-50, 51) * 1110.01f;
				dust.velocity.Y = dust.velocity.Y + Main.rand.Next(-50, 51) * 111110.01f;
				dust.scale *= 1f + Main.rand.Next(-30, 31) * 0.01f;
			}
		}
	}

    public class Tinzombie : ModNPC
	{
		public override void SetStaticDefaults()
		{
				DisplayName.SetDefault("Armored Zombie");
			 DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Zombi blindado");
			 DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Zombie blindé");
			 
			Main.npcFrameCount[NPC.type] = Main.npcFrameCount[NPCID.Zombie];

			/* DisplayName.AddTranslation(GameCulture.Russian, "Бронированный зомби");
			   DisplayName.AddTranslation(GameCulture.Chinese, "装甲僵尸");*/
		}

		public override void SetDefaults()
		{
			NPC.CloneDefaults(NPCID.Zombie);
			NPC.width = 18;
			NPC.height = 40;
			NPC.damage = 28;
			NPC.defense = 13;
			NPC.lifeMax = 59;
			NPC.HitSound = SoundID.NPCHit1;
			NPC.DeathSound = SoundID.NPCDeath2;
			NPC.value = 60f;
			NPC.knockBackResist = 0.2f;
			NPC.aiStyle = 3;
			AIType = NPCID.Zombie;
			AnimationType = NPCID.Zombie;
			Banner = Item.NPCtoBanner(NPCID.Zombie);
			BannerItem = Item.BannerToItem(Banner);

		}
        public override void ModifyNPCLoot(NPCLoot NPCLoot)
            {
			}	
		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
		if(NPC.downedBoss1 == false){
			return SpawnCondition.OverworldNightMonster.Chance * 0.02f;
            }
            else{
            return SpawnCondition.OverworldNightMonster.Chance * 0.01f;
            }
		}

		public override void HitEffect(int hitDirection, double damage)
		{
			for (int i = 0; i < 10; i++)
			{
				int dustType = Main.rand.Next(139, 143);
				int dustIndex = Dust.NewDust(NPC.position, NPC.width, NPC.height, dustType);
				Dust dust = Main.dust[dustIndex];
				dust.velocity.X = dust.velocity.X + Main.rand.Next(-50, 51) * 1110.01f;
				dust.velocity.Y = dust.velocity.Y + Main.rand.Next(-50, 51) * 111110.01f;
				dust.scale *= 1f + Main.rand.Next(-30, 31) * 0.01f;
			}
		}
	}

    public class Leadzombie : ModNPC
	{
		public override void SetStaticDefaults()
		{
				DisplayName.SetDefault("Armored Zombie");
			 DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Zombi blindado");
			 DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Zombie blindé");
			Main.npcFrameCount[NPC.type] = Main.npcFrameCount[NPCID.Zombie];


			/*
			DisplayName.AddTranslation(GameCulture.Russian, "Бронированный зомби");
			   DisplayName.AddTranslation(GameCulture.Chinese, "装甲僵尸");*/
		}

		public override void SetDefaults()
		{
			NPC.CloneDefaults(NPCID.Zombie);
			NPC.width = 18;
			NPC.height = 40;
			NPC.damage = 28;
			NPC.defense = 17;
			NPC.lifeMax = 59;
			NPC.HitSound = SoundID.NPCHit1;
			NPC.DeathSound = SoundID.NPCDeath2;
			NPC.value = 60f;
			NPC.knockBackResist = 0.2f;
			NPC.aiStyle = 3;
			AIType = NPCID.Zombie;
			AnimationType = NPCID.Zombie;
			Banner = Item.NPCtoBanner(NPCID.Zombie);
			BannerItem = Item.BannerToItem(Banner);

		}
        public override void ModifyNPCLoot(NPCLoot NPCLoot)
            {
			}	
		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			if(NPC.downedBoss1 == false){
			return SpawnCondition.OverworldNightMonster.Chance * 0.02f;
            }
            else{
            return SpawnCondition.OverworldNightMonster.Chance * 0.01f;
            }
		}

		public override void HitEffect(int hitDirection, double damage)
		{
			for (int i = 0; i < 10; i++)
			{
				int dustType = Main.rand.Next(139, 143);
				int dustIndex = Dust.NewDust(NPC.position, NPC.width, NPC.height, dustType);
				Dust dust = Main.dust[dustIndex];
				dust.velocity.X = dust.velocity.X + Main.rand.Next(-50, 51) * 1110.01f;
				dust.velocity.Y = dust.velocity.Y + Main.rand.Next(-50, 51) * 111110.01f;
				dust.scale *= 1f + Main.rand.Next(-30, 31) * 0.01f;
			}
		}
	}

    public class Ironzombie : ModNPC
	{
		public override void SetStaticDefaults()
		{
				DisplayName.SetDefault("Armored Zombie");
			 DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Zombi blindado");
			 DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Zombie blindé");
			 
			Main.npcFrameCount[NPC.type] = Main.npcFrameCount[NPCID.Zombie];

			/* DisplayName.AddTranslation(GameCulture.Russian, "Бронированный зомби");
			   DisplayName.AddTranslation(GameCulture.Chinese, "装甲僵尸"); */
		}

		public override void SetDefaults()
		{
			NPC.CloneDefaults(NPCID.Zombie);
			NPC.width = 18;
			NPC.height = 40;
			NPC.damage = 28;
			NPC.defense = 15;
			NPC.lifeMax = 59;
			NPC.HitSound = SoundID.NPCHit1;
			NPC.DeathSound = SoundID.NPCDeath2;
			NPC.value = 60f;
			NPC.knockBackResist = 0.2f;
			NPC.aiStyle = 3;
			AIType = NPCID.Zombie;
			AnimationType = NPCID.Zombie;
			Banner = Item.NPCtoBanner(NPCID.Zombie);
			BannerItem = Item.BannerToItem(Banner);

		}
        public override void ModifyNPCLoot(NPCLoot NPCLoot)
            {
			}	
		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			if(NPC.downedBoss1 == false){
			return SpawnCondition.OverworldNightMonster.Chance * 0.02f;
            }
            else{
            return SpawnCondition.OverworldNightMonster.Chance * 0.01f;
            }
		}

		public override void HitEffect(int hitDirection, double damage)
		{
			for (int i = 0; i < 10; i++)
			{
				int dustType = Main.rand.Next(139, 143);
				int dustIndex = Dust.NewDust(NPC.position, NPC.width, NPC.height, dustType);
				Dust dust = Main.dust[dustIndex];
				dust.velocity.X = dust.velocity.X + Main.rand.Next(-50, 51) * 1110.01f;
				dust.velocity.Y = dust.velocity.Y + Main.rand.Next(-50, 51) * 111110.01f;
				dust.scale *= 1f + Main.rand.Next(-30, 31) * 0.01f;
			}
		}
	}

    public class Silverzombie : ModNPC
	{
		public override void SetStaticDefaults()
		{
				DisplayName.SetDefault("Armored Zombie");
			 DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Zombi blindado");
			 DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Zombie blindé");
			Main.npcFrameCount[NPC.type] = Main.npcFrameCount[NPCID.Zombie];

			/*
			 DisplayName.AddTranslation(GameCulture.Russian, "Бронированный зомби");
			   DisplayName.AddTranslation(GameCulture.Chinese, "装甲僵尸");*/
		}

		public override void SetDefaults()
		{
			NPC.CloneDefaults(NPCID.Zombie);
			NPC.width = 18;
			NPC.height = 40;
			NPC.damage = 19;
			NPC.defense = 22;
			NPC.lifeMax = 59;
			NPC.HitSound = SoundID.NPCHit1;
			NPC.DeathSound = SoundID.NPCDeath2;
			NPC.value = 60f;
			NPC.knockBackResist = 0.2f;
			NPC.aiStyle = 3;
			AIType = NPCID.Zombie;
			AnimationType = NPCID.Zombie;
			Banner = Item.NPCtoBanner(NPCID.Zombie);
			BannerItem = Item.BannerToItem(Banner);

		}
        public override void ModifyNPCLoot(NPCLoot NPCLoot)
            {
			}	
		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			return SpawnCondition.OverworldNightMonster.Chance * 0.02f;
		}

		public override void HitEffect(int hitDirection, double damage)
		{
			for (int i = 0; i < 10; i++)
			{
				int dustType = Main.rand.Next(139, 143);
				int dustIndex = Dust.NewDust(NPC.position, NPC.width, NPC.height, dustType);
				Dust dust = Main.dust[dustIndex];
				dust.velocity.X = dust.velocity.X + Main.rand.Next(-50, 51) * 1110.01f;
				dust.velocity.Y = dust.velocity.Y + Main.rand.Next(-50, 51) * 111110.01f;
				dust.scale *= 1f + Main.rand.Next(-30, 31) * 0.01f;
			}
		}
	}

    public class Tungstenzombie : ModNPC
	{
		public override void SetStaticDefaults()
		{
				DisplayName.SetDefault("Armored Zombie");
			 DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Zombi blindado");
			 DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Zombie blindé");
			 
			Main.npcFrameCount[NPC.type] = Main.npcFrameCount[NPCID.Zombie];

			/* DisplayName.AddTranslation(GameCulture.Russian, "Бронированный зомби");
			   DisplayName.AddTranslation(GameCulture.Chinese, "装甲僵尸");*/
		}

		public override void SetDefaults()
		{
			NPC.CloneDefaults(NPCID.Zombie);
			NPC.width = 18;
			NPC.height = 40;
			NPC.damage = 28;
			NPC.defense = 21;
			NPC.lifeMax = 59;
			NPC.HitSound = SoundID.NPCHit1;
			NPC.DeathSound = SoundID.NPCDeath2;
			NPC.value = 60f;
			NPC.knockBackResist = 0.2f;
			NPC.aiStyle = 3;
			AIType = NPCID.Zombie;
			AnimationType = NPCID.Zombie;
			Banner = Item.NPCtoBanner(NPCID.Zombie);
			BannerItem = Item.BannerToItem(Banner);

		}
        public override void ModifyNPCLoot(NPCLoot NPCLoot)
            {
			}	
		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			return SpawnCondition.OverworldNightMonster.Chance * 0.02f;
		}

		public override void HitEffect(int hitDirection, double damage)
		{
			for (int i = 0; i < 10; i++)
			{
				int dustType = Main.rand.Next(139, 143);
				int dustIndex = Dust.NewDust(NPC.position, NPC.width, NPC.height, dustType);
				Dust dust = Main.dust[dustIndex];
				dust.velocity.X = dust.velocity.X + Main.rand.Next(-50, 51) * 1110.01f;
				dust.velocity.Y = dust.velocity.Y + Main.rand.Next(-50, 51) * 111110.01f;
				dust.scale *= 1f + Main.rand.Next(-30, 31) * 0.01f;
			}
		}
	}

    public class Goldzombie : ModNPC
	{
		public override void SetStaticDefaults()
		{
				DisplayName.SetDefault("Armored Zombie");
			 DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Zombi blindado");
			 DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Zombie blindé");
			Main.npcFrameCount[NPC.type] = Main.npcFrameCount[NPCID.Zombie];


			/*
			  DisplayName.AddTranslation(GameCulture.Russian, "Бронированный зомби");
			   DisplayName.AddTranslation(GameCulture.Chinese, "装甲僵尸");*/
		}

		public override void SetDefaults()
		{
			NPC.CloneDefaults(NPCID.Zombie);
			NPC.width = 18;
			NPC.height = 40;
			NPC.damage = 28;
			NPC.defense = 22;
			NPC.lifeMax = 59;
			NPC.HitSound = SoundID.NPCHit1;
			NPC.DeathSound = SoundID.NPCDeath2;
			NPC.value = 60f;
			NPC.knockBackResist = 0.2f;
			NPC.aiStyle = 3;
			AIType = NPCID.Zombie;
			AnimationType = NPCID.Zombie;
			Banner = Item.NPCtoBanner(NPCID.Zombie);
			BannerItem = Item.BannerToItem(Banner);

		}
        public override void ModifyNPCLoot(NPCLoot NPCLoot)
            {
			}	
		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			return SpawnCondition.OverworldNightMonster.Chance * 0.02f;
		}

		public override void HitEffect(int hitDirection, double damage)
		{
			for (int i = 0; i < 10; i++)
			{
				int dustType = Main.rand.Next(139, 143);
				int dustIndex = Dust.NewDust(NPC.position, NPC.width, NPC.height, dustType);
				Dust dust = Main.dust[dustIndex];
				dust.velocity.X = dust.velocity.X + Main.rand.Next(-50, 51) * 1110.01f;
				dust.velocity.Y = dust.velocity.Y + Main.rand.Next(-50, 51) * 111110.01f;
				dust.scale *= 1f + Main.rand.Next(-30, 31) * 0.01f;
			}
		}
	}

    public class Platinumzombie : ModNPC
	{
		public override void SetStaticDefaults()
		{
				DisplayName.SetDefault("Armored Zombie");
			 DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Zombi blindado");
			 DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Zombie blindé");
			 Main.npcFrameCount[NPC.type] = Main.npcFrameCount[NPCID.Zombie];

			/*
			 DisplayName.AddTranslation(GameCulture.Russian, "Бронированный зомби");
			   DisplayName.AddTranslation(GameCulture.Chinese, "装甲僵尸");*/
		}

		public override void SetDefaults()
		{
			NPC.CloneDefaults(NPCID.Zombie);
			NPC.width = 18;
			NPC.height = 40;
			NPC.damage = 28;
			NPC.defense = 26;
			NPC.lifeMax = 59;
			NPC.HitSound = SoundID.NPCHit1;
			NPC.DeathSound = SoundID.NPCDeath2;
			NPC.value = 60f;
			NPC.knockBackResist = 0.2f;
			NPC.aiStyle = 3;
			AIType = NPCID.Zombie;
			AnimationType = NPCID.Zombie;
			Banner = Item.NPCtoBanner(NPCID.Zombie);
			BannerItem = Item.BannerToItem(Banner);

		}
        public override void ModifyNPCLoot(NPCLoot NPCLoot)
            {
			}	
		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			return SpawnCondition.OverworldNightMonster.Chance * 0.02f;
		}

		public override void HitEffect(int hitDirection, double damage)
		{
			for (int i = 0; i < 10; i++)
			{
				int dustType = Main.rand.Next(139, 143);
				int dustIndex = Dust.NewDust(NPC.position, NPC.width, NPC.height, dustType);
				Dust dust = Main.dust[dustIndex];
				dust.velocity.X = dust.velocity.X + Main.rand.Next(-50, 51) * 1110.01f;
				dust.velocity.Y = dust.velocity.Y + Main.rand.Next(-50, 51) * 111110.01f;
				dust.scale *= 1f + Main.rand.Next(-30, 31) * 0.01f;
			}
		}
	}

    public class Armoredzombie : ModNPC
	{
		public override void SetStaticDefaults()
		{
				DisplayName.SetDefault("Armored Zombie");
			 DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Zombi blindado");
			 DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Zombie blindé");
			  
			Main.npcFrameCount[NPC.type] = Main.npcFrameCount[NPCID.Zombie];

			/*
			DisplayName.AddTranslation(GameCulture.Russian, "Бронированный зомби");
			   DisplayName.AddTranslation(GameCulture.Chinese, "装甲僵尸");*/
		}

		public override void SetDefaults()
		{
			NPC.CloneDefaults(NPCID.Zombie);
			NPC.width = 18;
			NPC.height = 40;
			NPC.damage = 15;
			NPC.defense = 56;
			NPC.lifeMax = 59;
			NPC.HitSound = SoundID.NPCHit1;
			NPC.DeathSound = SoundID.NPCDeath2;
			NPC.value = 60f;
			NPC.knockBackResist = 0.2f;
			NPC.aiStyle = 3;
			AIType = NPCID.Zombie;
			AnimationType = NPCID.Zombie;
			Banner = Item.NPCtoBanner(NPCID.Zombie);
			BannerItem = Item.BannerToItem(Banner);

		}
        public override void ModifyNPCLoot(NPCLoot NPCLoot)
            {
	          Item.NewItem(NPC.GetSource_Loot(),NPC.getRect(),ModContent.ItemType<Reinforced_iron_ore>());
			}	
		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			if(NPC.downedBoss1 == true){
			return SpawnCondition.OverworldNightMonster.Chance * 0.02f;
            }
            if(NPC.downedBoss2 == true){
			return SpawnCondition.OverworldNightMonster.Chance * 0.03f;
            }
            else{
            return SpawnCondition.OverworldNightMonster.Chance * 0.01f;
            }
		}

		public override void HitEffect(int hitDirection, double damage)
		{
			for (int i = 0; i < 10; i++)
			{
				int dustType = Main.rand.Next(139, 143);
				int dustIndex = Dust.NewDust(NPC.position, NPC.width, NPC.height, dustType);
				Dust dust = Main.dust[dustIndex];
				dust.velocity.X = dust.velocity.X + Main.rand.Next(-50, 51) * 1110.01f;
				dust.velocity.Y = dust.velocity.Y + Main.rand.Next(-50, 51) * 111110.01f;
				dust.scale *= 1f + Main.rand.Next(-30, 31) * 0.01f;
			}
		}
	}

    public class Tuxonitezombie : ModNPC
	{
		public override void SetStaticDefaults()
		{
				DisplayName.SetDefault("Armored Zombie");
			 DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Zombi blindado");
			 DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Zombie blindé");
			 
			Main.npcFrameCount[NPC.type] = Main.npcFrameCount[NPCID.Zombie];

			/* DisplayName.AddTranslation(GameCulture.Russian, "Бронированный зомби");
			   DisplayName.AddTranslation(GameCulture.Chinese, "装甲僵尸");*/
		}

		public override void SetDefaults()
		{
			NPC.CloneDefaults(NPCID.Zombie);
			NPC.width = 18;
			NPC.height = 40;
			NPC.damage = 28;
			NPC.defense = 28;
			NPC.lifeMax = 59;
			NPC.HitSound = SoundID.NPCHit1;
			NPC.DeathSound = SoundID.NPCDeath2;
			NPC.value = 60f;
			NPC.knockBackResist = 0.2f;
			NPC.aiStyle = 3;
			AIType = NPCID.Zombie;
			AnimationType = NPCID.Zombie;
			Banner = Item.NPCtoBanner(NPCID.Zombie);
			BannerItem = Item.BannerToItem(Banner);
		}
        public override void ModifyNPCLoot(NPCLoot NPCLoot)
            {
			}	
		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			if(NPC.downedSlimeKing == true){
			return SpawnCondition.OverworldNightMonster.Chance * 0.02f;
			}
			return 0;
		}

		public override void HitEffect(int hitDirection, double damage)
		{
			for (int i = 0; i < 10; i++)
			{
				int dustType = Main.rand.Next(139, 143);
				int dustIndex = Dust.NewDust(NPC.position, NPC.width, NPC.height, dustType);
				Dust dust = Main.dust[dustIndex];
				dust.velocity.X = dust.velocity.X + Main.rand.Next(-50, 51) * 1110.01f;
				dust.velocity.Y = dust.velocity.Y + Main.rand.Next(-50, 51) * 111110.01f;
				dust.scale *= 1f + Main.rand.Next(-30, 31) * 0.01f;
			}
		}
	}

    public class Shadowzombie : ModNPC
	{
		public override void SetStaticDefaults()
		{
				DisplayName.SetDefault("Armored Zombie");
			 DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Zombi blindado");
			 DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Zombie blindé");
			 
			Main.npcFrameCount[NPC.type] = Main.npcFrameCount[NPCID.Zombie];

			/* DisplayName.AddTranslation(GameCulture.Russian, "Бронированный зомби");
			   DisplayName.AddTranslation(GameCulture.Chinese, "装甲僵尸");*/
		}

		public override void SetDefaults()
		{
			NPC.CloneDefaults(NPCID.Zombie);
			NPC.width = 18;
			NPC.height = 40;
			NPC.damage = 28;
			NPC.defense = 23;
			NPC.lifeMax = 59;
			NPC.HitSound = SoundID.NPCHit1;
			NPC.DeathSound = SoundID.NPCDeath2;
			NPC.value = 60f;
			NPC.knockBackResist = 0.2f;
			NPC.aiStyle = 3;
			AIType = NPCID.Zombie;
			AnimationType = NPCID.Zombie;
			Banner = Item.NPCtoBanner(NPCID.Zombie);
			BannerItem = Item.BannerToItem(Banner);
		}
        public override void ModifyNPCLoot(NPCLoot NPCLoot)
            {
			}	
		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			if(NPC.downedBoss1 == true){
			return SpawnCondition.OverworldNightMonster.Chance * 0.02f;
			}
			return 0;
		}
		 public override void AI() //this is where you program your AI
        {	
			NPC.TargetClosest(true);	
			Vector2 targetPosition = Main.player[NPC.target].position;

 			if (targetPosition.X < NPC.position.X && NPC.velocity.X > -8) // AND I'm not at max "left" velocity
			{
  				NPC.velocity.X -= 0.25f; // accelerate to the left
			}

			if (targetPosition.X > NPC.position.X && NPC.velocity.X < 8) // AND I'm not at max "right" velocity
			{
             	NPC.velocity.X += 0.25f; // accelerate to the right
			}
		}

		public override void HitEffect(int hitDirection, double damage)
		{
			for (int i = 0; i < 10; i++)
			{
				int dustType = Main.rand.Next(139, 143);
				int dustIndex = Dust.NewDust(NPC.position, NPC.width, NPC.height, dustType);
				Dust dust = Main.dust[dustIndex];
				dust.velocity.X = dust.velocity.X + Main.rand.Next(-50, 51) * 1110.01f;
				dust.velocity.Y = dust.velocity.Y + Main.rand.Next(-50, 51) * 111110.01f;
				dust.scale *= 1f + Main.rand.Next(-30, 31) * 0.01f;
			}
		}
	}

    public class Crimsonzombi : ModNPC
	{
		public override void SetStaticDefaults()
		{
				DisplayName.SetDefault("Armored Zombie");
			 DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Zombi blindado");
			 DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Zombie blindé");
			 
			Main.npcFrameCount[NPC.type] = Main.npcFrameCount[NPCID.Zombie];
		}

		public override void SetDefaults()
		{
			NPC.CloneDefaults(NPCID.Zombie);
			NPC.width = 18;
			NPC.height = 40;
			NPC.damage = 29;
			NPC.defense = 25;
			NPC.lifeMax = 59;
			NPC.HitSound = SoundID.NPCHit1;
			NPC.DeathSound = SoundID.NPCDeath2;
			NPC.value = 60f;
			NPC.knockBackResist = 0.2f;
			NPC.aiStyle = 3;
			AIType = NPCID.Zombie;
			AnimationType = NPCID.Zombie;
			Banner = Item.NPCtoBanner(NPCID.Zombie);
			BannerItem = Item.BannerToItem(Banner);
		}
        public override void ModifyNPCLoot(NPCLoot NPCLoot)
            {
			}	
		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			if(NPC.downedBoss1 == true){
			return SpawnCondition.OverworldNightMonster.Chance * 0.02f;
			}
			return 0;
		}
		 public override void AI(){
			  NPC.ai[2]++;
			 
			 if(NPC.life <= 80){
			 if (NPC.ai[2] %2 == 0){
				  NPC.life = NPC.life + 1;
			  }
			 }
			  if (NPC.ai[2] %3 == 0){
				   NPC.ai[2] = 0;
			  }
		 }

		public override void HitEffect(int hitDirection, double damage)
		{
			for (int i = 0; i < 10; i++)
			{
				int dustType = Main.rand.Next(139, 143);
				int dustIndex = Dust.NewDust(NPC.position, NPC.width, NPC.height, dustType);
				Dust dust = Main.dust[dustIndex];
				dust.velocity.X = dust.velocity.X + Main.rand.Next(-50, 51) * 1110.01f;
				dust.velocity.Y = dust.velocity.Y + Main.rand.Next(-50, 51) * 111110.01f;
				dust.scale *= 1f + Main.rand.Next(-30, 31) * 0.01f;
			}
		}
	}
}