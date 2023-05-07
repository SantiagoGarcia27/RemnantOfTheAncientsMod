using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.GameContent.Creative;

namespace RemnantOfTheAncientsMod.Content.Items.Weapons.Ammo
{
    [CloneByReference]
    public class EndlessHoly : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Endless Holy Quiver");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Carquois sacré sans fin");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Carcaj Sagrado eterno");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.HolyArrow);
            Item.maxStack = 1;
            Item.consumable = false;
            Item.value += Item.sellPrice(0, 1, 0, 0);
            Item.rare += 1;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ItemID.HolyArrow, 3996)
            .AddTile(TileID.CrystalBall)
            .Register();
        }
    }
}