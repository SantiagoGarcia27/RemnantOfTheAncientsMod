using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.GameContent.Creative;
using Terraria;
using RemnantOfTheAncientsMod.Common.UI.ReaperUI;

namespace RemnantOfTheAncientsMod.Content.Items.Items.Guides
{
	public class AReforge_Guide : ModItem
	{
		public override void SetStaticDefaults()
		{
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}
		public override void SetDefaults()
		{
			Item.width = 26;
			Item.height = 26;
			Item.maxStack = 1;
			Item.useTime = 10;
			Item.useAnimation = 10;
            Item.useStyle = ItemUseStyleID.HoldUp;
			Item.consumable = false;
            Item.value = 0;
			Item.rare = ItemRarityID.Green;
		}
        public override bool CanUseItem(Player player)
        {
            BookUISystem BookUI = ModContent.GetInstance<BookUISystem>();

            if (BookUI.IsVisible()) BookUI.HideMyUI();
            else BookUI.ShowMyUI();
            return base.CanUseItem(player);
        }
        public override bool? UseItem(Player player)
        {  
            return base.UseItem(player);
        }
    }
}