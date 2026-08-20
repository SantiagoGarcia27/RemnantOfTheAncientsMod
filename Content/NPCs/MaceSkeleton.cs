using PlayerProxyLib.Common;
using RemnantOfTheAncientsMod.Content.Items.Items;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;

namespace RemnantOfTheAncientsMod.Content.NPCs
{
	public class MaceSkeleton : ModNPC
	{
		public override void SetStaticDefaults()
		{
			
			Main.npcFrameCount[Type] = Main.npcFrameCount[NPCID.Skeleton];
            NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers()
            {
                Velocity = 1f
            };
            NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, value);
            NPC.spriteDirection = NPC.direction;
		}

		public override void SetDefaults()
		{
			NPC.width = 36;
			NPC.height = 32;
			NPC.damage = 25;
			NPC.defense = 56;
			NPC.lifeMax = 59;
			NPC.HitSound = SoundID.NPCHit1;
			NPC.DeathSound = SoundID.NPCDeath1;
			NPC.value = 25f;
			NPC.knockBackResist = 0.2f;
			NPC.aiStyle =  NPCAIStyleID.Fighter;
			AIType = NPCID.Skeleton;
			AnimationType = NPCID.Skeleton;
			Banner = Item.NPCtoBanner(NPCID.Skeleton); 
			BannerItem = Item.BannerToItem(Banner);
		}
        public override void AI()
        {
			NPC.TargetClosest();

			if (NPC.target != null)
			{
				//Player target = Main.player[NPC.target];
				Player proxy = NPC.GetPlayerProxy();

                if (proxy.inventory[0] == null || proxy.inventory[0].IsAir)
                {
                    // Usa SetDefaults en lugar de new Item() para inicializar correctamente los stats del arma
                    proxy.inventory[0].SetDefaults(ItemID.Mace);
                }

                // 2. Seleccionar el slot
                proxy.selectedItem = 0;

				proxy.Click();
                // 3. Forzar los controles de entrada
               /* proxy.PressUseItem();

                // 4. Iniciar la animación SOLO si el personaje está libre para atacar
                if (proxy.itemAnimation == 0)
                {
                    proxy.ApplyItemAnimation(proxy.HeldItem);
                }
			   */
                
            }


            
            base.AI();
        }
        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            // We can use AddRange instead of calling Add multiple times in order to add multiple items at once
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {
				// Sets the spawning conditions of this NPC that is listed in the bestiary.
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface,

				// Sets the description of this NPC that is listed in the bestiary.
				new FlavorTextBestiaryInfoElement("A fearless slime with a stolen helmet"),
            });
        }
        public override void ModifyNPCLoot(NPCLoot NPCLoot)
		{
			NPCLoot.Add(ItemDropRule.Common(ItemID.Gel, 1));
			NPCLoot.Add(ItemDropRule.Common(ModContent.ItemType<ReinforcedIronOre>()));
		}

		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			return 0;//SpawnCondition.OverworldNightMonster.Chance * 0.02f; //* ModContent.GetInstance<ConfigClient1>().xdlevel; 
        }
	}
}
