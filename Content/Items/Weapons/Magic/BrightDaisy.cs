using Microsoft.Xna.Framework;
using RemnantOfTheAncientsMod.Common.RemPlayer;
using RemnantOfTheAncientsMod.Content.Projectiles.Mage;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Content.Items.Weapons.Magic
{
    internal class BrightDaisy : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
            Item.staff[Item.type] = true;
        }

        // no encontré el sonido de estrellas para el uso del arma
        public override void SetDefaults()
        {
            Item.height = 24;
            Item.width = 24;
            Item.DamageType = DamageClass.Magic;
            Item.damage = 10;
            Item.knockBack = 1.2f;
            Item.crit = 0;
            Item.mana = 20;
            Item.useTime = 7;
            Item.useAnimation = 40;
            Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<BrightPetal>();
            Item.shootSpeed = 20;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.UseSound = SoundID.Item34;
            Item.rare = ItemRarityID.Green;
            Item.value = Item.buyPrice(silver: 1);
            Item.noMelee = true;
            Item.reuseDelay = 60;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (Main.rand.NextFloat() <= (float)1 / 10) Projectile.NewProjectile(source, position, velocity, ModContent.ProjectileType<BrightPetal_Fire>(), damage, 1, player.whoAmI);
            return true;
        }
        public override void ModifyManaCost(Player player, ref float reduce, ref float mult)
        {
            mult = !player.GetModPlayer<RemnantPlayer>().DaylightArmorSetBonus ? mult : 0;
            base.ModifyManaCost(player, ref reduce, ref mult);
        }
        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            velocity = velocity.RotatedByRandom(MathHelper.ToRadians(20f));
            velocity *= Main.rand.NextFloat(0.8f, 1.2f);
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
