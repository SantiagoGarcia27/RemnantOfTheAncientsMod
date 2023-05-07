using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.GameContent.Creative;

namespace RemnantOfTheAncientsMod.Content.Items.Weapons.Ammo
{
    [CloneByReference]
    public class EndlesChlorophyte : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Endless Chlorophyte Pouch");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Pochette de Chlorophyte Sans Fin");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Bolsa de Clorofita Eterna");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.ChlorophyteBullet);
            Item.maxStack = 1;
            Item.consumable = false;
            Item.value += Item.sellPrice(0, 1, 0, 0);
            Item.rare += 1;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ItemID.ChlorophyteBullet, 3996)
            .AddTile(TileID.CrystalBall)
            .Register();
        }
    }
}