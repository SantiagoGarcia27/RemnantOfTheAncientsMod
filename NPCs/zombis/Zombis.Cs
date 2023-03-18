using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using Microsoft.Xna.Framework;
using Terraria.ModLoader.Utilities;
using RemnantOfTheAncientsMod.Items.Items;

namespace RemnantOfTheAncientsMod.NPCs.zombis
{
	public class Fastzombie : ModNPC
	{
		public override void SetStaticDefaults()
		{

			DisplayName.SetDefault("weak zombie");
			DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "zombie débil");
			DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Zombie faible");
			Main.npcFrameCount[Type] = Main.npcFrameCount[NPCID.Zombie];
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
        public override void ModifyNPCLoot(NPCLoot npcLoot) => Item.NewItem(NPC.GetSource_Loot(), NPC.getRect(), ItemID.Gel);
        public override float SpawnChance(NPCSpawnInfo spawnInfo) => NPC.downedBoss1 ? SpawnCondition.OverworldNightMonster.Chance * 0.001f : 0.03f;

		public override void HitEffect(int hitDirection, double damage)
		{
			for (int i = 0; i < 10; i++)
			{
                Dust dust = Dust.NewDustDirect(NPC.position, NPC.width, NPC.height, Main.rand.Next(139, 143));
                dust.velocity = new Vector2(Main.rand.NextFloat(-50f, 51f) * 1110.01f, Main.rand.NextFloat(-50f, 51f) * 111110.01f);
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
		public override float SpawnChance(NPCSpawnInfo spawnInfo) => NPC.downedBoss1 ? SpawnCondition.OverworldNightMonster.Chance * 0.02f : 0.01f;
		public override void HitEffect(int hitDirection, double damage)
		{
			for (int i = 0; i < 10; i++)
			{
                Dust dust = Dust.NewDustDirect(NPC.position, NPC.width, NPC.height, Main.rand.Next(139, 143));
                dust.velocity = new Vector2(Main.rand.NextFloat(-50f, 51f) * 1110.01f, Main.rand.NextFloat(-50f, 51f) * 111110.01f);
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
        public override float SpawnChance(NPCSpawnInfo spawnInfo) => NPC.downedBoss1 ? SpawnCondition.OverworldNightMonster.Chance * 0.02f : 0.01f;
		public override void HitEffect(int hitDirection, double damage)
		{
			for (int i = 0; i < 10; i++)
			{
                Dust dust = Dust.NewDustDirect(NPC.position, NPC.width, NPC.height, Main.rand.Next(139, 143));
                dust.velocity = new Vector2(Main.rand.NextFloat(-50f, 51f) * 1110.01f, Main.rand.NextFloat(-50f, 51f) * 111110.01f);
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
		public override float SpawnChance(NPCSpawnInfo spawnInfo) => NPC.downedBoss1 ? SpawnCondition.OverworldNightMonster.Chance * 0.02f : 0.01f;
		public override void HitEffect(int hitDirection, double damage)
		{
			for (int i = 0; i < 10; i++)
			{
                Dust dust = Dust.NewDustDirect(NPC.position, NPC.width, NPC.height, Main.rand.Next(139, 143));
                dust.velocity = new Vector2(Main.rand.NextFloat(-50f, 51f) * 1110.01f, Main.rand.NextFloat(-50f, 51f) * 111110.01f);
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
		public override float SpawnChance(NPCSpawnInfo spawnInfo) => NPC.downedBoss1 ? SpawnCondition.OverworldNightMonster.Chance * 0.001f : 0.03f;

		public override void HitEffect(int hitDirection, double damage)
		{
			for (int i = 0; i < 10; i++)
			{
                Dust dust = Dust.NewDustDirect(NPC.position, NPC.width, NPC.height, Main.rand.Next(139, 143));
                dust.velocity = new Vector2(Main.rand.NextFloat(-50f, 51f) * 1110.01f, Main.rand.NextFloat(-50f, 51f) * 111110.01f);
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
		public override float SpawnChance(NPCSpawnInfo spawnInfo) => NPC.downedBoss1 ? SpawnCondition.OverworldNightMonster.Chance * 0.002f : 0.03f;

		public override void HitEffect(int hitDirection, double damage)
		{
			for (int i = 0; i < 10; i++)
			{
                Dust dust = Dust.NewDustDirect(NPC.position, NPC.width, NPC.height, Main.rand.Next(139, 143));
                dust.velocity = new Vector2(Main.rand.NextFloat(-50f, 51f) * 1110.01f, Main.rand.NextFloat(-50f, 51f) * 111110.01f);
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


		public override float SpawnChance(NPCSpawnInfo spawnInfo) => NPC.downedBoss1 ? SpawnCondition.OverworldNightMonster.Chance * 0.002f : 0.03f;

		public override void HitEffect(int hitDirection, double damage)
		{
			for (int i = 0; i < 10; i++)
			{
                Dust dust = Dust.NewDustDirect(NPC.position, NPC.width, NPC.height, Main.rand.Next(139, 143));
                dust.velocity = new Vector2(Main.rand.NextFloat(-50f, 51f) * 1110.01f, Main.rand.NextFloat(-50f, 51f) * 111110.01f);
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

		public override float SpawnChance(NPCSpawnInfo spawnInfo)=> NPC.downedBoss1 ? SpawnCondition.OverworldNightMonster.Chance * 0.001f : 0.02f;

		public override void HitEffect(int hitDirection, double damage)
		{
			for (int i = 0; i < 10; i++)
			{
                Dust dust = Dust.NewDustDirect(NPC.position, NPC.width, NPC.height, Main.rand.Next(139, 143));
                dust.velocity = new Vector2(Main.rand.NextFloat(-50f, 51f) * 1110.01f, Main.rand.NextFloat(-50f, 51f) * 111110.01f);
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
        public override float SpawnChance(NPCSpawnInfo spawnInfo) => NPC.downedBoss1 ? SpawnCondition.OverworldNightMonster.Chance * 0.001f : 0.02f; 

		public override void HitEffect(int hitDirection, double damage)
		{
			for (int i = 0; i < 10; i++)
			{
                Dust dust = Dust.NewDustDirect(NPC.position, NPC.width, NPC.height, Main.rand.Next(139, 143));
                dust.velocity = new Vector2(Main.rand.NextFloat(-50f, 51f) * 1110.01f, Main.rand.NextFloat(-50f, 51f) * 111110.01f);
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
		public override void ModifyNPCLoot(NPCLoot npcLoot)
		{
			Item.NewItem(NPC.GetSource_Loot(), NPC.getRect(), ModContent.ItemType<Reinforced_iron_ore>());
		}
        public override float SpawnChance(NPCSpawnInfo spawnInfo) => NPC.downedBoss1 ? SpawnCondition.OverworldNightMonster.Chance * 0.0008f : 0.01f;

		public override void HitEffect(int hitDirection, double damage)
		{
			for (int i = 0; i < 10; i++)
			{
                Dust dust = Dust.NewDustDirect(NPC.position, NPC.width, NPC.height, Main.rand.Next(139, 143));
                dust.velocity = new Vector2(Main.rand.NextFloat(-50f, 51f) * 1110.01f, Main.rand.NextFloat(-50f, 51f) * 111110.01f);
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
        public override float SpawnChance(NPCSpawnInfo spawnInfo) => NPC.downedSlimeKing ? SpawnCondition.OverworldNightMonster.Chance * 0.02f : 0;

        public override void HitEffect(int hitDirection, double damage)
		{
			for (int i = 0; i < 10; i++)
			{
                Dust dust = Dust.NewDustDirect(NPC.position, NPC.width, NPC.height, Main.rand.Next(139, 143));
                dust.velocity = new Vector2(Main.rand.NextFloat(-50f, 51f) * 1110.01f, Main.rand.NextFloat(-50f, 51f) * 111110.01f);
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
        public override float SpawnChance(NPCSpawnInfo spawnInfo) => NPC.downedBoss1 ? SpawnCondition.OverworldNightMonster.Chance * 0.02f : 0;
        public override void AI()
		{
            //NPC.TargetClosest(true);
            //Vector2 targetPosition = Main.player[NPC.target].position;

            //if (targetPosition.X < NPC.position.X && NPC.velocity.X > -8)
            //{
            //	NPC.velocity.X -= 0.25f;
            //}

            //if (targetPosition.X > NPC.position.X && NPC.velocity.X < 8)
            //{
            //	NPC.velocity.X += 0.25f;
            //}
            NPC.TargetClosest(true);
            Vector2 targetPosition = Main.player[NPC.target].position;
            NPC.velocity.X += targetPosition.X < NPC.position.X ? -0.25f : 0.25f;
        }

		public override void HitEffect(int hitDirection, double damage)
		{
			for (int i = 0; i < 10; i++)
			{
				Dust dust = Dust.NewDustDirect(NPC.position, NPC.width, NPC.height, Main.rand.Next(139, 143));
				dust.velocity = new Vector2(Main.rand.NextFloat(-50f, 51f) * 1110.01f, Main.rand.NextFloat(-50f, 51f) * 111110.01f);
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
			NPC.defense += 19;
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
		public override float SpawnChance(NPCSpawnInfo spawnInfo) => NPC.downedBoss1 ? SpawnCondition.OverworldNightMonster.Chance * 0.02f : 0;
        public override void AI()
        {
            NPC.ai[2]++;

            if (NPC.life <= 80 && NPC.ai[2] % 2 == 0)
            {
                NPC.life++;
            }
            NPC.ai[2] %= 3;
        }

        public override void HitEffect(int hitDirection, double damage)
		{
			for (int i = 0; i < 10; i++)
			{
				Dust dust = Dust.NewDustDirect(NPC.position, NPC.width, NPC.height, Main.rand.Next(139, 143));
				dust.velocity = new Vector2(Main.rand.NextFloat(-50f, 51f) * 1110.01f, Main.rand.NextFloat(-50f, 51f) * 111110.01f);
				dust.scale *= 1f + Main.rand.Next(-30, 31) * 0.01f;
			}
		}
	}
}