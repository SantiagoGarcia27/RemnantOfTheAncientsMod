using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using Terraria.Localization;
using static Terraria.ModLoader.ModContent;

namespace opswordsII.Items.Mele.saber
{
	public class Enchanted_saber : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Enchanted Saber");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Sabre enchanté");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Sable Encantado");
			Tooltip.SetDefault("Shoot an enchanted sword beam");
           	Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Tirez sur un rayon d'épée enchanté");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Dispara un rayo de espada encantada");
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}
		public override void SetDefaults()
		{
			Item.damage = 10;
			Item.DamageType = DamageClass.Melee;
			Item.width = 40;
			Item.height = 40;
			Item.useTime = 15;
			Item.useAnimation = 25;
			Item.useStyle = 1;
			Item.knockBack = 10;
			Item.value = Item.sellPrice(silver: 50);
			Item.rare = 2;
			Item.shoot = 173;
			 Item.shootSpeed = 10f;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
		}

		public override void AddRecipes()
		{
			CreateRecipe()
			.AddIngredient(ItemID.EnchantedSword,1)
			.AddTile(TileID.Anvils)
			.Register();
		}
	}
}
