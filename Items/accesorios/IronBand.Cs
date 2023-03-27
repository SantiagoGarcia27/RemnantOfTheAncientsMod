using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Items.accesorios
{
	public class IronBand : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Iron Band");
			DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Bande de Fer");
			DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Banda de Hierro");
			Tooltip.SetDefault("2% Increased damage reduction");
			Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Réduction des dégâts augmentée de 2%");
			Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Aumenta la reducción de daño en un 2%");
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
			Item.width = 26; //Tamaño inGame 
			Item.height = 26;
			Item.maxStack = 1; //Cantidad maxima en un espacio de inventario
			Item.value = Item.buyPrice(0, 0, 2, 72);  //Precio
			Item.rare = 1;
			Item.accessory = true;
			Item.defense = 1;
		}
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.endurance += 0.02f;
        }

        public override void AddRecipes()
		{
			CreateRecipe()
			.AddRecipeGroup("IronBar", 2)
			.AddTile(TileID.Anvils)
			.Register();
        }
	}
}
