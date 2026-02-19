using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using static Terraria.ModLoader.ModContent;
using RemnantOfTheAncientsMod.Content.Items.Items;
using Terraria.GameContent.Creative;

namespace RemnantOfTheAncientsMod.Content.Items.Weapons.Melee
{
	public class ReinforcedIronBroadsword : ModItem
	{
		public override void SetStaticDefaults()
		{
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}
		public override void SetDefaults()
		{
			Item.damage = new Item(ItemID.PlatinumBroadsword).damage + 10;
			Item.DamageType = DamageClass.Melee;
			Item.width = 64;
			Item.height = 64;
			Item.useTime = 50;
			Item.useAnimation = 50;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 8;
			Item.value = Item.sellPrice(silver: 27);
			Item.rare = ItemRarityID.White;
			Item.UseSound = SoundID.Item1;
			Item.ArmorPenetration = 5;
			Item.autoReuse = true;
			Item.scale = 1.5f;
		}
		public override void AddRecipes()
		{
			CreateRecipe()
			.AddIngredient(ItemType<ReinforcedIronBar>(), 8)
			.AddTile(TileID.Anvils)
			.Register();
		}
	}
}

