using Terraria;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.GameContent.Creative;
using Terraria.ID;

namespace RemnantOfTheAncientsMod.Content.Items.Accesories
{
    public class Restaurated_Chisel : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Restaurated Chisel");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Ciseau restauré");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Cincel restaurado");
            Tooltip.SetDefault("Increases mining speed by 35%");
            Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Augmente la vitesse de minage de 35 %");
            Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Aumenta la velocidad de minado en un 35%");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 10;
            Item.height = 14;
            Item.value = new Item(ItemID.AncientChisel).value;
            Item.rare = 0;
            Item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.pickSpeed -= 0.35f;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.AncientChisel)
                .AddRecipeGroup("GoldBar", 5)
                .AddTile(TileID.SharpeningStation)
                .Register();
        }
    }
}