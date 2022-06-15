using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using opswordsII.NPCs.frozen_assaulter;
using opswordsII.Items.Armor;
using opswordsII.Items.Mele;
using opswordsII.Items.Ranger;
using opswordsII.Items.Magic;
using opswordsII.Items.Summon;
using opswordsII.Items.Bloques;
using opswordsII.Items.Core;

using static Terraria.ModLoader.ModContent;

namespace opswordsII.Items.tresure_bag
{
	public class frostBag : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Treasure Bag");
			Tooltip.SetDefault("{$CommonItemTooltip.RightClickToOpen}");
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
				
			int choice = Main.rand.Next(4);
			if (choice == 0) {
				player.QuickSpawnItem(player.GetSource_OpenItem(ItemType<frostBag>()),ItemType<FrostShark>());
			}
			else if (choice == 1) {
				player.QuickSpawnItem(player.GetSource_OpenItem(ItemType<frostBag>()),ItemType<Permafrost>());
			}
            else if (choice == 2) {
				player.QuickSpawnItem(player.GetSource_OpenItem(ItemType<frostBag>()),ItemType<frozen_staff>());
			}
			else if (choice == 3) {
				player.QuickSpawnItem(player.GetSource_OpenItem(ItemType<frostBag>()),ItemType<FrozenStafff>());
			}
			else if (choice != 5)
			{
				player.QuickSpawnItem(player.GetSource_OpenItem(ItemType<frostBag>()), ItemType<Frost_core>());
				if (Main.rand.NextFloat() <= (float)1 / 10) player.QuickSpawnItem(player.GetSource_OpenItem(ItemType<frostBag>()), ItemType<FrostTrophy>());
			}
		}
			public override int BossBagNPC => NPCType<frozen_assaulter>();
         

		}	
	}


