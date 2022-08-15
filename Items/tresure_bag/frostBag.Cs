using Terraria;
using Terraria.ID;
using Terraria.GameContent.ItemDropRules;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using opswordsII.NPCs.frozen_assaulter;
using opswordsII.Items.Armor.Masks;
using opswordsII.Items.Mele;
using opswordsII.Items.Ranger;
using opswordsII.Items.Magic;
using opswordsII.Items.Summon;
using opswordsII.Items.Bloques.MusicBox;
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
			itemLoot.Add(ItemDropRule.NotScalingWithLuck(ItemType<FrozenMask>(), 7));
			itemLoot.Add(ItemDropRule.Common(ItemType<Frost_core>()));
			itemLoot.Add(ItemDropRule.CoinsBasedOnNPCValue(NPCType<frozen_assaulter>()));
			itemLoot.Add(ItemDropRule.NotScalingWithLuck(ItemType<FrostShark>(), 4));
			itemLoot.Add(ItemDropRule.NotScalingWithLuck(ItemType<Permafrost>(), 4));
			itemLoot.Add(ItemDropRule.NotScalingWithLuck(ItemType<frozen_staff>(), 4));
			itemLoot.Add(ItemDropRule.NotScalingWithLuck(ItemType<FrozenStafff>(), 4));
			itemLoot.Add(ItemDropRule.NotScalingWithLuck(ItemType<FrozenMusicBox>(), 10));
			itemLoot.Add(ItemDropRule.NotScalingWithLuck(ItemType<Frozenp2MusicBox>(), 10));


		}
			public override int BossBagNPC => NPCType<frozen_assaulter>();
         

		}	
	}


