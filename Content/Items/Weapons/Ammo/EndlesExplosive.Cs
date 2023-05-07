using Terraria.Localization;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;

namespace RemnantOfTheAncientsMod.Content.Items.Weapons.Ammo
{
    [CloneByReference]
    public class EndlesExplosive : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Endless Exploding Bullet Pouch");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Pochette de balles explosives sans fin");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Bolsa de balas explosivas Eterna");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }
        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.ExplodingBullet);  
            Item.value += Item.buyPrice(0, 1, 0, 0);
            Item.rare +=1;
            Item.consumable = false;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ItemID.ExplodingBullet, 3996)
            .AddTile(TileID.CrystalBall)
            .Register();
        }
    }
}