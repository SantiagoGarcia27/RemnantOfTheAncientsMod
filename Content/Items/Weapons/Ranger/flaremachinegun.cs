using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using RemnantOfTheAncientsMod.Content.Projectiles.Ranger;
using System;

namespace RemnantOfTheAncientsMod.Content.Items.Weapons.Ranger
{
    public class Flaremachinegun : ModItem
    {
        public override void SetStaticDefaults()
        { 
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }
        public int NotConsumeAmmoChance = 50;
        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(NotConsumeAmmoChance);
        public override void SetDefaults()
        {
            Item.damage = 29;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 60;
            Item.height = 40;
            Item.useTime = 6;
            Item.useAnimation = 6;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noMelee = true;
            Item.knockBack = 1;
            Item.value = Item.sellPrice(silver: 400);
            Item.rare = ItemRarityID.Yellow;
            Item.UseSound = SoundID.Item45;
            Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<SuperFlare>();
            Item.shootSpeed = 1f;
            Item.useAmmo = AmmoID.Flare;
            Item.channel = true;
        }
        double wave = 0.0;
        int dir = -1;
        public override bool CanShoot(Player player)
        {
            if (wave >= 1 || wave <= -1) dir *= -1;
            wave += 0.1f * dir;
            return base.CanShoot(player);
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            type = ModContent.ProjectileType<SuperFlare>();
            //velocity = new Vector2(velocity.X, velocity.Y).RotatedByRandom(MathHelper.ToRadians(10));
            //position.Y += (float)Math.Cos(wave) *32;


            Projectile.NewProjectile(source, position, velocity, type, damage, knockback);
            var a = Projectile.NewProjectile(source, position, velocity, type, damage, knockback);
            Main.projectile[a].ai[2] = 1;
            a = Projectile.NewProjectile(source, position, velocity, type, damage, knockback);
            Main.projectile[a].ai[2] = -1;
            return false;
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-10, 0);
        }
        public override bool CanConsumeAmmo(Item ammo, Player player)
        {
            return Main.rand.NextFloat() >= NotConsumeAmmoChance / 100f;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ItemID.FlareGun, 1)
            .AddIngredient(ItemID.ChainGun, 1)
            .AddTile(TileID.MythrilAnvil)
            .Register();
        }
    }
}
