using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.GameContent.Creative;
using RemnantOfTheAncientsMod.Content.Items.Accesories;
using Terraria.Localization;

namespace RemnantOfTheAncientsMod.Content.Items.Armor.Daylight
{
    [AutoloadEquip(EquipType.Legs)]
	public class Daylight_Legging : ModItem
	{
		public override void SetStaticDefaults()
		{
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}
        public int IncreasedMinionDamage = 4;
        public int IncreasesMaxMana = 5;
        public int MovmentSpeedBonus = 5;
        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(IncreasedMinionDamage, IncreasesMaxMana, MovmentSpeedBonus);
        public override void SetDefaults()
		{
			Item.width = 18;
			Item.height = 18;
			Item.value = Item.sellPrice(0, 0, 5, 0);
            Item.rare = ItemRarityID.White;
			Item.defense = 4;
		}

        public override void UpdateEquip(Player player)
        {
            player.GetDamage(DamageClass.Summon) += IncreasedMinionDamage / 100f;
            player.statManaMax2 += IncreasesMaxMana;
            player.moveSpeed += MovmentSpeedBonus / 100f;
        }
        public override void AddRecipes()
		{
			CreateRecipe()
            .AddIngredient(ItemID.Daybloom, 4)
            .AddIngredient(ItemID.Vine, 1)
            .AddIngredient<IronBand>()
            .AddTile(TileID.Anvils)
            .Register();
		}
	}
}

