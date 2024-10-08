using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ModLoader.Utilities;

namespace opswordsII.NPCs
{
	public class ArmoredSlime : ModNPC
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Armored Slime");
			// DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Slime blindado");
			// DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Slime blindé");
			Main.npcFrameCount[Type] = Main.npcFrameCount[NPCID.BlueSlime];

			   NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers(0) { // Influences how the NPC looks in the Bestiary
				Velocity = 1f // Draws the NPC in the bestiary as if its walking +1 tiles in the x direction
			};
			NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, value);
		}
		
		public override void SetDefaults() {
			NPC.width = 32;
			NPC.height = 32;
			NPC.damage = 25;
			NPC.defense = 56;
			NPC.lifeMax = 59;
			NPC.HitSound = SoundID.NPCHit1;
			NPC.DeathSound = SoundID.NPCDeath1;
			NPC.value = 25f;
			NPC.knockBackResist = 0.2f;
			NPC.aiStyle = 1;
			AIType = NPCID.BlueSlime;
			AnimationType = NPCID.BlueSlime;
			Banner = Item.NPCtoBanner(NPCID.BlueSlime); ;
			BannerItem = Item.BannerToItem(Banner);
		}
			public override void ModifyNPCLoot(NPCLoot NPCLoot) { 
	          NPCLoot.Add(ItemDropRule.Common(ItemID.Gel,1));
			  NPCLoot.Add(ItemDropRule.Common(ModContent.ItemType<Items.Items.Reinforced_iron_ore>()));
			}	

		    public override float SpawnChance(NPCSpawnInfo spawnInfo) {

			return SpawnCondition.OverworldNightMonster.Chance * 0.02f;
		}
		    /*public override void HitEffect(int hitDirection, double damage)
		   {
			for (int i = 0; i < 10; i++)
			{
				int dustType = Main.rand.Next(139, 143);
				int dustIndex = Dust.NewDust(NPC.position, NPC.width, NPC.height, dustType);
				Dust dust = Main.dust[dustIndex];
				dust.velocity.X = dust.velocity.X + Main.rand.Next(-50, 51) * 110.01f;
				dust.velocity.Y = dust.velocity.Y + Main.rand.Next(-50, 51) * 110.01f;
				dust.scale *= 1f + Main.rand.Next(-30, 31) * 0.01f;

           }   
		}*/
	}
}
