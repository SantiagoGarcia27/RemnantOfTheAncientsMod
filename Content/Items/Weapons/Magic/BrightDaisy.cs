using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using RemnantOfTheAncientsMod.Content.Projectiles.Mage;

namespace RemnantOfTheAncientsMod.Content.Items.Weapons.Magic
{
    internal class BrightDaisy : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Bright Daisy");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Marguerite brillante");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Margarita Brillante");
            Tooltip.SetDefault("A daisy with the power of luminous skies");
            Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Une marguerite avec le pouvoir des cieux lumineux");
            Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Una margarita con el poder del cielo luminoso");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
            Item.staff[Item.type] = true;
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
            Item.shoot = ModContent.ProjectileType<BrightPetal>();
            Item.shootSpeed = 20;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.UseSound = SoundID.Item75;
            Item.rare = ItemRarityID.Green;
            Item.value = Item.buyPrice(copper: 50);
            Item.noMelee = true;

        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (Main.rand.NextFloat() <= (float)1 / 10) Projectile.NewProjectile(source, position, velocity, ModContent.ProjectileType<BrightPetal_Fire>(), damage, 1, player.whoAmI);
            return true;
        }
        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            velocity = velocity.RotatedByRandom(MathHelper.ToRadians(5));
        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ItemID.Daybloom, 5)
            .AddIngredient(ItemID.FallenStar, 5)
            .AddRecipeGroup("GoldBar", 2)
            .AddTile(TileID.WorkBenches)
            .Register();
        }
    }
}
