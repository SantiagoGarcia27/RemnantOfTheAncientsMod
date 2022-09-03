using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.ItemDropRules;
using Terraria.GameContent.Creative;
using static Terraria.ModLoader.ModContent;
using RemnantOfTheAncientsMod.Items.Armor.Masks;
using RemnantOfTheAncientsMod.NPCs.ITyrant;
using RemnantOfTheAncientsMod.Items.Mele.saber;
using RemnantOfTheAncientsMod.Items.Ranger.Rep;
using RemnantOfTheAncientsMod.Items.Bloques.MusicBox;
using RemnantOfTheAncientsMod.Items.Magic;
using RemnantOfTheAncientsMod.Items.Core;


namespace RemnantOfTheAncientsMod.Items.tresure_bag
{
	public class infernalBag : ModItem
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
        public override bool CanRightClick() => true;
        public override void ModifyItemLoot(ItemLoot itemLoot)
		{
			itemLoot.Add(ItemDropRule.Common(ItemType<Infernal_core>()));
			itemLoot.Add(ItemDropRule.CoinsBasedOnNPCValue(NPCType<InfernalTyrantBody>()));
			itemLoot.Add(ItemDropRule.NotScalingWithLuck(ItemType<Tyrant_repeater>(), 3));
			itemLoot.Add(ItemDropRule.NotScalingWithLuck(ItemType<Spike_saber>(), 3));
			itemLoot.Add(ItemDropRule.NotScalingWithLuck(ItemType<Tyran_Blast>(), 3));
		}
		public override int BossBagNPC => NPCType<InfernalTyrantHead>();
	}
}


