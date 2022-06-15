using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using opswordsII.NPCs.DAniquilator;
using opswordsII.Items.Armor;
using opswordsII.Items.Mele;
using opswordsII.Items.Ranger;
using opswordsII.Items.Magic;
using opswordsII.Items.Bloques;
using opswordsII.Items.Summon;
using opswordsII.Items.Summon.Buf;
using opswordsII.Items.Core;
using static Terraria.ModLoader.ModContent;
using opswordsII.Items.Ranger.Bows;

namespace opswordsII.Items.tresure_bag
{
	public class desertBag : ModItem
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
				
			int choice = Main.rand.Next(5);
			if (choice == 0) {
				player.QuickSpawnItem(player.GetSource_OpenItem(ItemType<desertBag>()),ItemType<desertbow>());
			}
			else if (choice == 1) {
				player.QuickSpawnItem(player.GetSource_OpenItem(ItemType<desertBag>()),ItemType<DesertEdge>());
			}
			else if (choice == 2) {
				player.QuickSpawnItem(player.GetSource_OpenItem(ItemType<desertBag>()),ItemType<DesertTome>());
			}
			else if (choice == 3) {
				player.QuickSpawnItem(player.GetSource_OpenItem(ItemType<desertBag>()),ItemType<DesertStaff>());
			}
			else if (choice == 4) {
				player.QuickSpawnItem(player.GetSource_OpenItem(ItemType<desertBag>()),ItemType<DesertStaff>());
			}
			else if (choice != 5)
            {
				player.QuickSpawnItem(player.GetSource_OpenItem(ItemType<desertBag>()), ItemType<Desert_Core>());
				if (Main.rand.NextFloat() <= (float)1 / 10) 
					player.QuickSpawnItem(player.GetSource_OpenItem(ItemType<desertBag>()), ItemType<DesertTrophy>());
				if (Main.rand.NextFloat() <= (float)1 / 5) 
					player.QuickSpawnItem(player.GetSource_OpenItem(ItemType<desertBag>()), ItemType<DScroll>());
			}
		}

			public override int BossBagNPC => NPCType<DesertAniquilator>();
         
	}
}

