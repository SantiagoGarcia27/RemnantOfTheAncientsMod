using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.GameContent.Creative;

namespace RemnantOfTheAncientsMod.Content.Items.Weapons.Ammo
{
    [CloneByReference]
    public class EndlessWrathSlimeBullet : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Endless Angry Slime Pouch");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Pochette de Slime en Colère Sans Fin");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Saco Infinito de Bala de Slime Furioso");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }
        public override void SetDefaults()
        {
            Item.CloneDefaults(ModContent.ItemType<WrathSlimeBullet>());
            Item.maxStack = 1;
            Item.consumable = false;
            Item.value += Item.buyPrice(0, 1, 0, 0);
            Item.rare += 1; 
        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ModContent.ItemType<WrathSlimeBullet>(), 3996)
            .AddTile(TileID.CrystalBall)
            .Register();
        }
    }
}