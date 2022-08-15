using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.ItemDropRules;
using Terraria.GameContent.Creative;
using opswordsII.NPCs.DAniquilator;
using opswordsII.Items.Armor.Masks;
using opswordsII.Items.Mele;
using opswordsII.Items.Magic;
using opswordsII.Items.Bloques.MusicBox;
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
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 3;
		}

		public override void SetDefaults()
		{
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
			itemLoot.Add(ItemDropRule.NotScalingWithLuck(ItemType<DesertAMask>(),7));
			itemLoot.Add(ItemDropRule.Common(ItemType<Desert_Core>()));
			itemLoot.Add(ItemDropRule.CoinsBasedOnNPCValue(NPCType<DesertAniquilator>()));
			itemLoot.Add(ItemDropRule.NotScalingWithLuck(ItemType<desertbow>(),4));
			itemLoot.Add(ItemDropRule.NotScalingWithLuck(ItemType<DesertEdge>(),4));
			itemLoot.Add(ItemDropRule.NotScalingWithLuck(ItemType<DesertTome>(),4));
			itemLoot.Add(ItemDropRule.NotScalingWithLuck(ItemType<DesertStaff>(),4));
			itemLoot.Add(ItemDropRule.NotScalingWithLuck(ItemType<DScroll>(),5));
			itemLoot.Add(ItemDropRule.NotScalingWithLuck(ItemType<DesertMusicBox>(),10));
		}

		public override int BossBagNPC => NPCType<DesertAniquilator>();

	}
}

