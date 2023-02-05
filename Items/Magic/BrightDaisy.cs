using Terraria;
using Terraria.ID;
using Terraria.GameContent.Creative;
using RemnantOfTheAncientsMod.Projectiles;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Items.Magic
{
    internal class BrightDaisy : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Bright Daisy");
            Tooltip.SetDefault("A daisy with the power of luminous skies");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        // no encontré el sonido de estrellas para el uso del arma
        public override void SetDefaults()
        {
            Item.height = 24;
            Item.width = 24;
            Item.DamageType = DamageClass.Magic;
            Item.damage = 15;
            Item.knockBack = 1.2f;
            Item.crit = 6;
            Item.mana = 5;
            Item.useTime = 15;
            Item.useAnimation = 15;
            Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<BrightPetalA>();
            Item.shootSpeed = 20;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.UseSound = SoundID.Item75;
            Item.rare = ItemRarityID.Green;
            Item.value = Item.buyPrice(copper: 50);
            Item.noMelee = true;

        }

        // falta agregar la variacion de las barras de platino 
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ItemID.Daybloom, 5)
            .AddIngredient(ItemID.Star, 5)
            .AddIngredient(ItemID.GoldBar, 2)
            .AddTile(TileID.WorkBenches)
            .Register();
        }
    }
}
