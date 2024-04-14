using Terraria.Localization;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using RemnantOfTheAncientsMod.Content.Items.Items;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;

namespace RemnantOfTheAncientsMod.Content.Items.Weapons.Ammo
{
    public class EndlesCoperCoin : EndlessBase
    {
        public override Item ItemBase => new(ItemID.CopperCoin);
        public override string TexturePath => null;
        public override void SetStaticDefaults()
        {
           // //DisplayName.SetDefault("ModelEndless Copper Coin");
           // //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Pièce de Cuivre Sans Fin");
           // //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Moneda de Cobre Eterna");
        }
        public override void AddRecipes()
        {
            base.AddRecipes();
            CreateRecipe()
            .AddIngredient(ItemID.SilverCoin, 40)
            .AddTile(TileID.CrystalBall)
            .Register();
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            return false;
        }
    }
    public class EndlesSilverCoin : EndlessBase
    {
        public override Item ItemBase => new(ItemID.SilverCoin);
        public override string TexturePath => null;
        public override void SetStaticDefaults()
        {

           // //DisplayName.SetDefault("ModelEndless Silver Coin");
           // //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Pièce d'argent sans fin");
           // //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Moneda de plata infinita");
        }
        public override void AddRecipes()
        {
            base.AddRecipes();
            CreateRecipe()
            .AddIngredient(ItemID.GoldCoin, 40)
            .AddTile(TileID.CrystalBall)
            .Register();
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            return false;
        }
    }
    public class EndlesGoldCoin : EndlessBase
    {
        public override Item ItemBase => new(ItemID.GoldCoin);
        public override string TexturePath => null;
        public override void SetStaticDefaults()
        {
           // //DisplayName.SetDefault("ModelEndless Gold Coin");
           // //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Pièce d'or sans fin");
           // //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Monedas de oro infinitas");
        }
        public override void AddRecipes()
        {
            base.AddRecipes();
            CreateRecipe()
            .AddIngredient(ItemID.PlatinumCoin, 40)
            .AddTile(TileID.CrystalBall)
            .Register();

        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            return false;
        }
    }
    public class EndlesPlatinumCoin : EndlessBase
    {
        public override Item ItemBase => new(ItemID.PlatinumCoin);
        public override string TexturePath => null;
        public override void SetStaticDefaults()
        {
           // //DisplayName.SetDefault("ModelEndless Gold Coin");
           // //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Pièce d'or sans fin");
           // //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Monedas de oro infinitas");
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            return false;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ItemBase.type, 600)
            .AddTile(TileID.CrystalBall)
            .Register();
        }
    }
}
