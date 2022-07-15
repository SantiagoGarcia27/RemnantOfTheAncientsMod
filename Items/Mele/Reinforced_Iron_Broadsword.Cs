using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using static Terraria.ModLoader.ModContent;
using opswordsII.Items.Items;
using Terraria.GameContent.Creative;

namespace opswordsII.Items.Mele
{
	public class Reinforced_Iron_Broadsword : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Reinforced Iron Broadsword");
			DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Epée large en fer renforcé");
			DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Espada de Hierro Reforzado");
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}
		public override void SetDefaults()
		{
			Item.damage = 21;
			Item.DamageType = DamageClass.Melee;
			Item.width = 40;
			Item.height = 80;
			Item.useTime = 30;
			Item.useAnimation = 30;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 8;
			Item.value = Item.sellPrice(silver: 27);
			Item.rare = ItemRarityID.White;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = false;
		}
	public override void AddRecipes()
		{
			CreateRecipe()
			.AddIngredient(ItemType<Reinforced_ironBar>(), 8)
			.AddTile(TileID.Anvils)
			.Register();
		}
	}
}

