using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.ItemDropRules;
using Terraria.GameContent.Creative;
using static Terraria.ModLoader.ModContent;
using opswordsII.Items.Armor.Masks;
using opswordsII.NPCs.ITyrant;
using opswordsII.Items.Mele.saber;
using opswordsII.Items.Ranger.Rep;
using opswordsII.Items.Bloques.MusicBox;
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
		public override void ModifyItemLoot(ItemLoot itemLoot)
		{
			itemLoot.Add(ItemDropRule.NotScalingWithLuck(ItemType<InfernalMask>(), 7));
			itemLoot.Add(ItemDropRule.Common(ItemType<Infernal_core>()));
			itemLoot.Add(ItemDropRule.CoinsBasedOnNPCValue(NPCType<InfernalTyrantBody>()));
			itemLoot.Add(ItemDropRule.NotScalingWithLuck(ItemType<Tyrant_repeater>(), 3));
			itemLoot.Add(ItemDropRule.NotScalingWithLuck(ItemType<Spike_saber>(), 3));
			itemLoot.Add(ItemDropRule.NotScalingWithLuck(ItemType<Tyran_Blast>(), 3));
		}

			public override int BossBagNPC => NPCType<InfernalTyrantHead>();
         

		}	
	}


