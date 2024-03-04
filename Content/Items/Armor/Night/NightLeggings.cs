using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Localization;
using Terraria.GameContent.Creative;
using RemnantOfTheAncientsMod.Content.Items.Items;
using RemnantOfTheAncientsMod.Common.UtilsTweaks;

namespace RemnantOfTheAncientsMod.Content.Items.Armor.Night
{
	[AutoloadEquip(EquipType.Legs)]
	public class NightLeggings : ModItem
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
			Item.value = 40000;
			Item.rare = ItemRarityID.Orange;
			Item.defense = 9;
		}

		public override void UpdateEquip(Player player)
		{
			player.moveSpeed *= 1.15f;
		}

		public override void AddRecipes()
		{
			if (RemnantOfTheAncientsMod.CalamityMod != null)
			{
				CreateRecipe()
				 .AddRecipeGroup("anyCorruptGreaves")
				 .AddIngredient(ItemID.JunglePants)
				 .AddIngredient(ItemID.NecroGreaves)
				 .AddIngredient(ItemID.MoltenGreaves)
				 .AddIngredient(ExternalModCallUtils.GetItemFromMod(RemnantOfTheAncientsMod.CalamityMod, "PurifiedGel"), 5)
				 .AddTile(TileID.DemonAltar)
				 .Register();

				CreateRecipe()
				.AddIngredient(ModContent.ItemType<NightBar>(), 13)
				.AddIngredient(ExternalModCallUtils.GetItemFromMod(RemnantOfTheAncientsMod.CalamityMod, "PurifiedGel"), 5)
				.AddTile(TileID.DemonAltar)
				.Register();
			}
			else
			{
                CreateRecipe()
                .AddRecipeGroup("anyCorruptGreaves")
				.AddIngredient(ItemID.JunglePants)
                .AddIngredient(ItemID.NecroGreaves)
                .AddIngredient(ItemID.MoltenGreaves)
                .AddTile(TileID.DemonAltar)
                .Register();

                CreateRecipe()
				.AddIngredient(ModContent.ItemType<NightBar>(), 13)
				.AddTile(TileID.DemonAltar)
				.Register();
			}
		}
	}
}

