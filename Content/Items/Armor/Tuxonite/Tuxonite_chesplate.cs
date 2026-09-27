using RemnantOfTheAncientsMod.Common.UtilsTweaks;
using RemnantOfTheAncientsMod.Content.Items.Items;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Content.Items.Armor.Tuxonite
{
	[AutoloadEquip(EquipType.Body)]
	public class Tuxonite_chesplate : ModItem
	{
		public override void SetStaticDefaults()
		{
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}
        public int RangerDamageBonus = 3;
        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(RangerDamageBonus);
        public override void SetDefaults()
		{
			Item.width = 18;
			Item.height = 18;
			Item.value = 3000;
			Item.rare = ItemRarityID.White;
			Item.defense = 5;
		}
        public override void UpdateEquip(Player player)
        {
			player.GetDamage(DamageClass.Ranged) += .03f;
        }
        public override void AddRecipes()
		{
			CreateRecipe()
			.AddIngredient(ModContent.ItemType<TuxoniteBar>(), RecipeUtils.SearchAmmountRecipe(ItemID.GoldChainmail, ItemID.GoldBar))
			.AddTile(TileID.Anvils)
			.Register();
		}
	}
}

