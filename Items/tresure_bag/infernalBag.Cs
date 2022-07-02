using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using static Terraria.ModLoader.ModContent;
using opswordsII.NPCs.ITyrant;
using opswordsII.Items.Mele.saber;
using opswordsII.Items.Ranger.Rep;
using opswordsII.Items.Magic;
using opswordsII.Items.Core;


namespace opswordsII.Items.tresure_bag
{
    public class infernalBag : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Treasure Bag");
			Tooltip.SetDefault("{$CommonItemTooltip.RightClickToOpen}");
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 3;
		}

	public override void SetDefaults() {
			Item.maxStack = 999;
			Item.consumable = true;
			Item.width = 24;
			Item.height = 24;
			Item.rare = ItemRarityID.Purple;
			Item.expert = true;
		}

		public override bool CanRightClick()
		{
			return true;
		}

		public override void OpenBossBag(Player player){
				
			int choice = Main.rand.Next(3);
			if (choice == 0) {
				player.QuickSpawnItem(player.GetSource_OpenItem(ItemType<infernalBag>()), ItemType<Tyrant_repeater>()); 
			}
			else if (choice == 1) {
				player.QuickSpawnItem(player.GetSource_OpenItem(ItemType<infernalBag>()), ItemType<Spike_saber>());
			}
            else if (choice == 2) {
				player.QuickSpawnItem(player.GetSource_OpenItem(ItemType<infernalBag>()), ItemType<Tyran_Blast>());	
			}
			else if (choice != 4)
            {
				player.QuickSpawnItem(player.GetSource_OpenItem(ItemType<infernalBag>()), ItemType<Infernal_core>());
			}
		/*	else if (choice == 3) {
				player.QuickSpawnItem(ItemType<Frost_core>());
				player.QuickSpawnItem(ItemType<Infernal_core>());
		
		}*/
		}
			public override int BossBagNPC => NPCType<InfernalTyrantHead>();
         

		}	
	}


