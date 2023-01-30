using Terraria;
using Terraria.ID;
using Terraria.GameContent.ItemDropRules;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using RemnantOfTheAncientsMod.NPCs.frozen_assaulter;
using RemnantOfTheAncientsMod.Items.Mele;
using RemnantOfTheAncientsMod.Items.Ranger;
using RemnantOfTheAncientsMod.Items.Magic;
using RemnantOfTheAncientsMod.Items.Summon;
using RemnantOfTheAncientsMod.Items.Core;

using static Terraria.ModLoader.ModContent;
using RemnantOfTheAncientsMod.Items.Fmode;
using RemnantOfTheAncientsMod.World;

namespace RemnantOfTheAncientsMod.Items.tresure_bag
{
	public class frostBag : ModItem
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
			itemLoot.Add(ItemDropRule.CoinsBasedOnNPCValue(NPCType<frozen_assaulter>()));

            itemLoot.Add(ItemDropRule.Common(ItemType<Frost_core>(), 1, 1, 1));
            itemLoot.Add(ItemDropRule.CoinsBasedOnNPCValue(NPCType<frozen_assaulter>())); ;
           // itemLoot.Add(ItemDropRule.Common(ItemType<DScroll>(), 5));
            itemLoot.Add(ItemDropRule.OneFromOptions(1, ItemType<FrostShark>(), ItemType<Permafrost>(), ItemType<frozen_staff>(), ItemType<FrozenStafff>()));
            itemLoot.Add(ItemDropRule.ByCondition(new FrozenReaperSoul(), ItemType<FrozenSoul>()));


        }
		public override int BossBagNPC => NPCType<frozen_assaulter>();
	}
    public class FrozenReaperSoul : IItemDropRuleCondition, IProvideItemConditionDescription
    {
        public bool CanDrop(DropAttemptInfo info) => !Main.LocalPlayer.GetModPlayer<FrozenReaperSoulPlayer>().FrozenReaperUpgrade && Reaper.ReaperMode;
        public bool CanShowItemDropInUI() => true;
        public string GetConditionDescription() => null;
    }
}


