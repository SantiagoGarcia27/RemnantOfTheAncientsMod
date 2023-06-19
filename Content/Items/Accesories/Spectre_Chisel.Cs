using Terraria;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.GameContent.Creative;
using Terraria.ID;

namespace RemnantOfTheAncientsMod.Content.Items.Accesories
{
    public class Spectre_Chisel : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Spectre Chisel");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Spectre Chisel");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Cincel espectral");
            Tooltip.SetDefault("Increases mining speed by 45%");
            Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Augmente la vitesse de minage de 45 %");
            Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Aumenta la velocidad de minado en un 45%");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 10;
            Item.height = 14;
            Item.value = new Item(ItemID.SpectrePaintbrush).value;
            Item.rare = new Item(ItemID.SpectrePaintbrush).rare;
            Item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.pickSpeed -= 0.45f;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<Restaurated_Chisel>())
                .AddIngredient(ItemID.Ectoplasm,8)
                .AddIngredient(ItemID.ChlorophyteBar, 8)
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }
    }
}