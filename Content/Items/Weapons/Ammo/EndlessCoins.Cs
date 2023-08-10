using Terraria.Localization;
using Terraria;
using Terraria.ID;
using RemnantOfTheAncientsMod.Content.Items.Items;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Content.Items.Weapons.Ammo
{
    public class EndlesCoperCoin : EndlessBase
    {
        public override Item ItemBase => new(ItemID.CopperCoin);
        public override string TexturePath => null;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Endless Copper Coin");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Pièce de Cuivre Sans Fin");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Moneda de Cobre Eterna");
        }
        public override void AddRecipes()
        {
            base.AddRecipes();
            CreateRecipe()
            .AddIngredient(ItemID.SilverCoin, 40)
            .AddTile(TileID.CrystalBall)
            .Register();
        }
    }
    public class EndlesSilverCoin : EndlessBase
    {
        public override Item ItemBase => new(ItemID.SilverCoin);
        public override string TexturePath => null;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Endless Silver Coin");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Pièce d'argent sans fin");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Moneda de plata infinita");
        }
        public override void AddRecipes()
        {
            base.AddRecipes();
            CreateRecipe()
            .AddIngredient(ItemID.GoldCoin, 40)
            .AddTile(TileID.CrystalBall)
            .Register();
        }
    }
    public class EndlesGoldCoin : EndlessBase
    {
        public override Item ItemBase => new(ItemID.GoldCoin);
        public override string TexturePath => null;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Endless Gold Coin");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Pièce d'or sans fin");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Monedas de oro infinitas");
        }
        public override void AddRecipes()
        {
            base.AddRecipes();
            CreateRecipe()
            .AddIngredient(ItemID.PlatinumCoin, 40)
            .AddTile(TileID.CrystalBall)
            .Register();
        }
    }
    public class EndlesPlatinumCoin : EndlessBase
    {
        public override Item ItemBase => new(ItemID.PlatinumCoin);
        public override string TexturePath => null;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Endless Gold Coin");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Pièce d'or sans fin");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Monedas de oro infinitas");
        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ItemBase.type, 600)
            .AddTile(TileID.CrystalBall)
            .Register();

            CreateRecipe()
           .AddIngredient(ModContent.ItemType<Terracoin>(), 6)
           .AddTile(TileID.CrystalBall)
           .Register();
        }
    }
}
