using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria.Localization;
using Terraria.GameContent.Creative;
using RemnantOfTheAncientsMod.Content.Tiles;
using RemnantOfTheAncientsMod.Content.Projectiles.Ranger;
using RemnantOfTheAncientsMod.Common.Global.Items;

namespace RemnantOfTheAncientsMod.Content.Items.Items
{
    public class Terracoin : ModItem
    {
        public override void SetStaticDefaults()
        {
           // //DisplayName.SetDefault("Terra Coin");
           // //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Polish), "Moneta Terra");
           // //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Pièce de monnaie Terra");
           // //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Moneda terra");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 100;
        }
        public override void SetDefaults()
        {
            Item.height = 14;
            Item.maxStack = 999;
            Item.value = Item.sellPrice(platinum: 100);
            Item.ammo = AmmoID.Coin;
            Item.notAmmo = true;
            Item.damage = 400;
            Item.shoot = ProjectileType<TerracoinP>();
            Item.shootSpeed = 4f;
            Item.DamageType = DamageClass.Ranged;
            Item.useStyle = 1;
            Item.useTurn = true;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.autoReuse = true;
            Item.consumable = true;
            Item.createTile = TileType<TerracoinB>();
            Item.noMelee = true;
            Item.GetGlobalItem<RemnantGlobalItem>().IsCoin = true;
        } 
        public override bool CanShoot(Player player)
        {
            return false;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ItemID.PlatinumCoin, 100)
            .Register();
        }
    }
}