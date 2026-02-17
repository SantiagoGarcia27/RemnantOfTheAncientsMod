using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using RemnantOfTheAncientsMod.Content.Items.Items;
using Terraria.GameContent.Creative;
using SangarUtilities.Common.UtilsTweaks;
using RemnantOfTheAncientsMod.Content.Items.Armor.Night;

namespace RemnantOfTheAncientsMod.Content.Items.Armor.TrueNight
{
	[AutoloadEquip(EquipType.Body)]
	public class TrueNightchesplate : ModItem
	{
		int manaBonus = 100;
		int lifeBonus = 100;
        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(manaBonus,lifeBonus);
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
			Item.defense = 18;
		}

		public override void UpdateEquip(Player player)
		{
			player.buffImmune[BuffID.Burning] = true;
			player.buffImmune[BuffID.BrokenArmor] = true;
			player.statManaMax2 += manaBonus;
			player.statLifeMax2 += lifeBonus;
		}

		public override void AddRecipes()
		{
            CreateRecipe()
            .AddIngredient<Nightchesplate>()
            .AddIngredient(ItemID.SoulofFright, 10)
            .AddIngredient(ItemID.SoulofMight, 10)
            .AddIngredient(ItemID.SoulofSight, 10)
            .AddTile(TileID.MythrilAnvil)
            .Register();
			
		}
	}
}

