using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.ItemDropRules;
using Terraria.GameContent.Creative;
using RemnantOfTheAncientsMod.NPCs.DAniquilator;
using RemnantOfTheAncientsMod.Items.Mele;
using RemnantOfTheAncientsMod.Items.Magic;
using RemnantOfTheAncientsMod.Items.Summon;
using RemnantOfTheAncientsMod.Items.Summon.Buf;
using RemnantOfTheAncientsMod.Items.Core;
using static Terraria.ModLoader.ModContent;
using RemnantOfTheAncientsMod.Items.Ranger.Bows;
using RemnantOfTheAncientsMod.Items.Fmode;
using RemnantOfTheAncientsMod.World;

namespace RemnantOfTheAncientsMod.Items.tresure_bag
{
	public class desertBag : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Treasure Bag");
			Tooltip.SetDefault("{$CommonItemTooltip.RightClickToOpen}");
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 3;
            ItemID.Sets.PreHardmodeLikeBossBag[Type] = true;
            ItemID.Sets.BossBag[Type] = true;
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
			itemLoot.Add(ItemDropRule.Common(ItemType<Desert_Core>(), 1, 1, 1));
			itemLoot.Add(ItemDropRule.CoinsBasedOnNPCValue(NPCType<DesertAniquilator>())); ;
			itemLoot.Add(ItemDropRule.Common(ItemType<DesertAniquilatorScroll>(), 5));
            itemLoot.Add(ItemDropRule.OneFromOptions(1, ItemType<desertbow>(), ItemType<DesertEdge>(), ItemType<DesertTome>(), ItemType<DesertStaff>()));
			itemLoot.Add(ItemDropRule.ByCondition(new DesertReaperSoul(), ItemType<DesertSoul>()));
        }
		//public override int BossBagNPC => NPCType<DesertAniquilator>();
	}
    public class DesertReaperSoul : IItemDropRuleCondition, IProvideItemConditionDescription
    {
		public bool CanDrop(DropAttemptInfo info) => !Main.LocalPlayer.GetModPlayer<DesertReaperSoulPlayer>().DesertReaperUpgrade && Reaper.ReaperMode;
        public bool CanShowItemDropInUI() => true;
        public string GetConditionDescription() => null;
    }
}


