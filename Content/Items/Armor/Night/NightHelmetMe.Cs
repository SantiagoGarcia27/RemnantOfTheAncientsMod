using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using RemnantOfTheAncientsMod.Content.Items.Items;
using Terraria.GameContent.Creative;
using RemnantOfTheAncientsMod.Common.UtilsTweaks;

namespace RemnantOfTheAncientsMod.Content.Items.Armor.Night
{
	[AutoloadEquip(EquipType.Head)]
	public class NightHelmetMe : ModItem
	{
        int MeleeDamageBonus = 13;
        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(MeleeDamageBonus);
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
			Item.defense = 10;
		}
		public override void UpdateEquip(Player player)
		{
			player.GetDamage(DamageClass.Melee) += 0.13f;
		}

		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == ModContent.ItemType<Nightchesplate>() && legs.type == ModContent.ItemType<NightLeggings>();
		}
        int DefenseBonus = 5;
        int MeleeDamageSetBonus = 6;
        int SpeedBonus = 5;
        public override void UpdateArmorSet(Player player)
		{
			player.setBonus = this.GetLocalization("SetBonus").WithFormatArgs(DefenseBonus, MeleeDamageSetBonus, SpeedBonus).Value;// "+5 Defense, Increase Melee Damage 6%, Increase Movement Speed";
            player.GetDamage(DamageClass.Melee) += MeleeDamageSetBonus/100f;
			player.statDefense += DefenseBonus;
			player.moveSpeed += SpeedBonus / 100f;
		}

		public override void AddRecipes()
		{
            if (RemnantOfTheAncientsMod.CalamityMod != null)
            {
                CreateRecipe()
                 .AddRecipeGroup("anyCorruptHelmet")
                 .AddIngredient(ItemID.JungleHat)
                 .AddIngredient(ItemID.NecroHelmet)
                 .AddIngredient(ItemID.MoltenHelmet)
                 .AddIngredient(ExternalModCallUtils.GetItemFromMod(RemnantOfTheAncientsMod.CalamityMod, "PurifiedGel"), 5)
                 .AddTile(TileID.DemonAltar)
                 .Register();

                CreateRecipe()
                .AddIngredient(ModContent.ItemType<NightBar>(), 15)
                .AddIngredient(ExternalModCallUtils.GetItemFromMod(RemnantOfTheAncientsMod.CalamityMod, "PurifiedGel"), 5)
                .AddTile(TileID.DemonAltar)
                .Register();
            }
            else
            {
                CreateRecipe()
                .AddRecipeGroup("anyCorruptHelmet")
                 .AddIngredient(ItemID.JungleHat)
                 .AddIngredient(ItemID.NecroHelmet)
                 .AddIngredient(ItemID.MoltenHelmet)
                .AddTile(TileID.DemonAltar)
                .Register();

                CreateRecipe()
                .AddIngredient(ModContent.ItemType<NightBar>(), 15)
                .AddTile(TileID.DemonAltar)
                .Register();
            }
        }
	}
}