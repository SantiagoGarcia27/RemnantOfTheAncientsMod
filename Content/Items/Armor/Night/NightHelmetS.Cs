using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using RemnantOfTheAncientsMod.Content.Items.Items;
using Terraria.GameContent.Creative;
using SangarUtilities.Common.UtilsTweaks;

namespace RemnantOfTheAncientsMod.Content.Items.Armor.Night
{
	[AutoloadEquip(EquipType.Head)]
	public class NightHelmetS : ModItem
	{
        private readonly int SummonDamageBonus = 13;
        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(SummonDamageBonus);
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
			Item.defense = 2;
		}
		public override void UpdateEquip(Player player)
		{
			player.GetDamage(DamageClass.Summon) += 0.13f;
		}

		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == ModContent.ItemType<Nightchesplate>() && legs.type == ModContent.ItemType<NightLeggings>();
		}
        private readonly int DefenseBonus = 5;
        private readonly int SummonDamageSetBonus = 6;
        private readonly int SpeedBonus = 5;
        private readonly int MaxMinionBonus = 2;
        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = this.GetLocalization("SetBonus").WithFormatArgs(DefenseBonus, SummonDamageSetBonus, SpeedBonus, MaxMinionBonus).Value;
            player.GetDamage(DamageClass.Summon) += SummonDamageSetBonus / 100f;
            player.statDefense += DefenseBonus;
            player.moveSpeed += SpeedBonus / 100f;
            player.maxMinions += MaxMinionBonus;
        }
		public override void AddRecipes()
		{
            int PurifiedGelId = CallUtils.TryGetItemFromMod(RemnantOfTheAncientsMod.CalamityMod, "PurifiedGel");

            Recipe recipe = CreateRecipe()
                .AddRecipeGroup("anyCorruptHelmet")
                .AddIngredient(ItemID.JungleHat)
                .AddIngredient(ItemID.NecroHelmet)
                .AddIngredient(ItemID.MoltenHelmet);
            if (PurifiedGelId > 0) recipe.AddIngredient(PurifiedGelId, 5);
            recipe.AddTile(TileID.DemonAltar).Register();

            Recipe recipe2 = CreateRecipe()
                .AddIngredient(ModContent.ItemType<NightBar>(), 15)
                .AddIngredient(CallUtils.TryGetItemFromMod(RemnantOfTheAncientsMod.CalamityMod, "PurifiedGel"), 5);
            if (PurifiedGelId > 0) recipe2.AddIngredient(PurifiedGelId, 5);
            recipe2.AddTile(TileID.DemonAltar).Register();
        }
	}
}