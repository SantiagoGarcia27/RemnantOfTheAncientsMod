using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using RemnantOfTheAncientsMod.Content.Items.Items;
using Terraria.GameContent.Creative;
using RemnantOfTheAncientsMod.Common.UtilsTweaks;

namespace RemnantOfTheAncientsMod.Content.Items.Armor.Night
{
	[AutoloadEquip(EquipType.Body)]
	public class Nightchesplate : ModItem
	{
		int manaBonus = 20;
		int lifeBonus = 50;
        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(manaBonus,lifeBonus);
        public override void SetStaticDefaults()
		{
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}
		public override void SetDefaults()
		{
			Item.width = 18;
			Item.height = 18;
			Item.value = 50000;
			Item.rare = ItemRarityID.Orange;
			Item.defense = 12;
		}

		public override void UpdateEquip(Player player)
		{
			player.buffImmune[BuffID.Shine] = true;
			player.buffImmune[BuffID.Burning] = true;
			player.buffImmune[BuffID.BrokenArmor] = true;
			player.statManaMax2 += 20;
			player.statLifeMax2 += 50;
		}

		public override void AddRecipes()
		{
			if (RemnantOfTheAncientsMod.CalamityMod != null)
			{
                CreateRecipe()
                 .AddRecipeGroup("anyCorruptChestplate")
                 .AddIngredient(ItemID.JungleShirt)
                 .AddIngredient(ItemID.NecroGreaves)
                 .AddIngredient(ItemID.MoltenBreastplate)
                 .AddIngredient(ExternalModCallUtils.GetItemFromMod(RemnantOfTheAncientsMod.CalamityMod, "PurifiedGel"), 5)
                 .AddTile(TileID.DemonAltar)
                 .Register();

                CreateRecipe()
				.AddIngredient(ModContent.ItemType<NightBar>(), 20)
                .AddIngredient(ExternalModCallUtils.GetItemFromMod(RemnantOfTheAncientsMod.CalamityMod, "PurifiedGel"), 5)
				.AddTile(TileID.DemonAltar)
                .Register();
            }
			else
			{
                CreateRecipe()
                .AddRecipeGroup("anyCorruptChestplate")
				.AddIngredient(ItemID.JungleShirt)
				.AddIngredient(ItemID.NecroGreaves)
				.AddIngredient(ItemID.MoltenBreastplate)	
				.AddTile(TileID.DemonAltar)
				.Register();

				CreateRecipe()
				.AddIngredient(ModContent.ItemType<NightBar>(), 20)
				.AddTile(TileID.DemonAltar)
				.Register();
			}
		}
	}
}

