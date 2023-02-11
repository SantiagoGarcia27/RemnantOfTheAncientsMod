using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.ItemDropRules;
using Terraria.GameContent.Creative;
using static Terraria.ModLoader.ModContent;
using RemnantOfTheAncientsMod.NPCs.ITyrant;
using RemnantOfTheAncientsMod.Items.Mele.saber;
using RemnantOfTheAncientsMod.Items.Ranger.Rep;
using RemnantOfTheAncientsMod.Items.Magic;
using RemnantOfTheAncientsMod.Items.Core;
using RemnantOfTheAncientsMod.Items.ReaperSouls;
using RemnantOfTheAncientsMod.World;

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
            // itemLoot.Add(ItemDropRule.Common(ItemType<DScroll>(), 5));
            itemLoot.Add(ItemDropRule.OneFromOptions(1, ItemType<Tyrant_repeater>(), ItemType<Spike_saber>(), ItemType<Tyran_Blast>()));
            itemLoot.Add(ItemDropRule.ByCondition(new InfernalReaperSoul(), ItemType<InfernalSoul>()));
        }
		public override int BossBagNPC => NPCType<InfernalTyrantHead>();
	}
    public class InfernalReaperSoul : IItemDropRuleCondition, IProvideItemConditionDescription
    {
        public bool CanDrop(DropAttemptInfo info) => !Main.LocalPlayer.GetModPlayer<InfernalReaperSoulPlayer>().InfernalReaperUpgrade && Reaper.ReaperMode;
        public bool CanShowItemDropInUI() => true;
        public string GetConditionDescription() => null;
    }
}


