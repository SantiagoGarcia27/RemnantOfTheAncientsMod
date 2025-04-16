using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.GameContent.Creative;
using Terraria.DataStructures;
using RemnantOfTheAncientsMod.Content.Items.Items;
using RemnantOfTheAncientsMod.Common.Global;
using RemnantOfTheAncientsMod.Content.Projectiles.Ranger;
using System.Drawing;

namespace RemnantOfTheAncientsMod.Content.Items.Weapons.Ranger.Bows
{
    public class TerraBow : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }
        public override void SetDefaults()
        {
            Item.damage = 75;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 12;
            Item.height = 38;
            Item.maxStack = 1;
            Item.useTime = 30;
            Item.useAnimation = 30;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.knockBack = 2;
            Item.value = Item.sellPrice(gold: 20);
            Item.rare = ItemRarityID.Yellow;
            Item.UseSound = SoundID.Item5;
            Item.noMelee = true;
            Item.shoot = ProjectileID.WoodenArrowFriendly;
            Item.useAmmo = AmmoID.Arrow;
            Item.shootSpeed = 20f;
            Item.autoReuse = true;
        }
        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            if(type == ProjectileID.WoodenArrowFriendly) type = ModContent.ProjectileType<TerraArrow>();
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (Main.rand.NextBool(2))
            {

                Vector2 newPos = position;
                newPos.Y += Main.rand.Next(10, 80) * (Main.rand.NextBool(2) ? -1 : 1);
                newPos.X -= Main.rand.Next(50, 180) * player.direction;

                for (int i = 0; i < 360; i += 5)
                {
                    Vector2 dustPos = (newPos + Vector2.One).RotatedBy(MathHelper.ToRadians(i));
                    Vector2 vel = new Vector2(0, 1).RotatedBy(MathHelper.ToRadians(i)) * 10f;
                    Dust.NewDust(dustPos, 10, 10, DustID.TerraBlade, vel.X, vel.Y, Scale: 1.5f);
                }
                Projectile.NewProjectile(source, newPos, velocity.RotatedByRandom(MathHelper.ToRadians(10)), ModContent.ProjectileType<TerraArrow>(), damage, knockback, player.whoAmI);
            }
            Projectile.NewProjectile(source, position - new Vector2(0, 16), velocity, type, damage, knockback, player.whoAmI);
            Projectile.NewProjectile(source, position - new Vector2(32 * player.direction, 48), velocity, type, damage, knockback, player.whoAmI);
            Projectile.NewProjectile(source, position - new Vector2(32 * player.direction, -48), velocity, type, damage, knockback, player.whoAmI);
            return true;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient<TrueFailnaught>()
            .AddIngredient<TrueNigthbow>()
            .AddIngredient(ItemID.BrokenHeroSword)
            .AddTile(TileID.MythrilAnvil)
            .Register();
        }
    }
}
