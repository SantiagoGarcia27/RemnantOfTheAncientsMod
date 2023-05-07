using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.GameContent.Creative;
using RemnantOfTheAncientsMod.Content.Items.Items;

namespace RemnantOfTheAncientsMod.Content.Items.Armor.Tuxonite
{
	[AutoloadEquip(EquipType.Head)]
	public class TuxoniteHelmet : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Tuxonite Helmet");
			DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Casco de tusonita");
			DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Casque Tuxonite");
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
			Item.width = 18;
			Item.height = 18;
			Item.value = 3600;
			Item.rare = ItemRarityID.White;
			Item.defense = 6;
		}

		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == ModContent.ItemType<Tuxonite_chesplate>() && legs.type == ModContent.ItemType<Tuxonite_Legging>();
		}

		public override void UpdateArmorSet(Player player)
		{
			player.setBonus = "+4 defense";
			player.statDefense += 4;
		}

		public override void AddRecipes()
		{
			CreateRecipe()
			.AddIngredient(ModContent.ItemType<TuxoniteBar>(), 25)
			.AddTile(TileID.Anvils)
			.Register();
		}
	}
}