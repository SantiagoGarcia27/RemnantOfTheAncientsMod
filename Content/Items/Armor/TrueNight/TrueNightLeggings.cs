using RemnantOfTheAncientsMod.Common.UtilsTweaks;
using RemnantOfTheAncientsMod.Content.Items.Armor.Night;
using RemnantOfTheAncientsMod.Content.Items.Items;
using SangarUtilities.Common.UtilsTweaks;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Content.Items.Armor.TrueNight
{
	[AutoloadEquip(EquipType.Legs)]
	public class TrueNightLeggings : ModItem
	{
        int MovmentSpeedBonus = 15;
        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(MovmentSpeedBonus);
        public override void SetStaticDefaults()
		{
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
			Item.width = 18;
			Item.height = 18;
			Item.value = Item.sellPrice(gold:1);
			Item.rare = ItemRarityID.Yellow;
			Item.defense = 13;
		}

		public override void UpdateEquip(Player player)
		{
			player.moveSpeed *= 1f + (MovmentSpeedBonus/100);
		}

		public override void AddRecipes()
		{
            CreateRecipe()
            .AddIngredient<NightLeggings>()
            .AddIngredient(ItemID.SoulofFright,10)
            .AddIngredient(ItemID.SoulofMight,10)
            .AddIngredient(ItemID.SoulofSight,10)
            .AddTile(TileID.MythrilAnvil)
            .Register();
        }
	}
}

