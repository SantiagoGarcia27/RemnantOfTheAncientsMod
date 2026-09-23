using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using Terraria.Localization;

namespace RemnantOfTheAncientsMod.Content.Items.Armor.Daylight
{
	[AutoloadEquip(EquipType.Body)]
	public class Daylight_chesplate : ModItem
	{
		public override void SetStaticDefaults()
		{
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}
        private readonly int IncreasedMagicDamage = 3;
        private readonly int IncreasedMinionBonus = 1;
        private readonly int IncreasesMaxMana = 5;

        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(IncreasedMagicDamage, IncreasesMaxMana, IncreasedMinionBonus);
        public override void SetDefaults()
		{
			Item.width = 18;
			Item.height = 18;
			Item.value = Item.sellPrice(0, 0, 10, 0);
            Item.rare = ItemRarityID.White;
			Item.defense = 3;
		}
		
        public override void UpdateEquip(Player player)
        {
			player.GetCritChance(DamageClass.Magic) *= 1f + IncreasedMagicDamage/100f;
			player.maxMinions += IncreasedMinionBonus;
			player.statManaMax2 += IncreasesMaxMana;
        }
        public override void AddRecipes()
		{
			CreateRecipe()
			.AddIngredient(ItemID.Daybloom,8)
			.AddIngredient(ItemID.Vine, 1)
            .AddRecipeGroup(RecipeGroupID.IronBar,2)
            .AddTile(TileID.Anvils)
			.Register();
		}
	}
}

