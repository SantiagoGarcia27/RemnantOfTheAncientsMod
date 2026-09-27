using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Content.Items.Armor.Reaper
{
	[AutoloadEquip(EquipType.Legs)]
	public class ReaperPants : ModItem
	{ 
        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs();
        public override void SetStaticDefaults()
		{
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}   
        
		public override void SetDefaults()
		{
			Item.width = 18;
			Item.height = 18;
			Item.value = 0;
			Item.vanity = true;
            Item.rare = ItemRarityID.White;
		}
	}
}

