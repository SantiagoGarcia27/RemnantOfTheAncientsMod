using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.Localization;
using RemnantOfTheAncientsMod.Content.Projectiles.Ranger;
using RemnantOfTheAncientsMod.Common.UtilsTweaks;

namespace RemnantOfTheAncientsMod.Content.Items.Weapons.Ranger.Flamethrower
{
    public class Igniter : ModItem
    {
        public override void SetStaticDefaults()
        {     
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }
        public int NotConsumeAmmoChance = 60;
        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(NotConsumeAmmoChance);
        public override void SetDefaults()
        {
            Item.damage = 10;
            Item.knockBack = 1.5f;
            Item.useTime = 4;
            Item.useAnimation = 4;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.rare = ItemRarityID.Blue;
            Item.shoot = ProjectileID.PurificationPowder;
            Item.ammo = AmmoID.Bullet;
            Item.shootSpeed = 6f;
            Item.autoReuse = true;
            Item.value = Item.sellPrice(0, 0, 2, 0);
            Item.Size = new Vector2(80, 40);      
            Item.DamageType = DamageClass.Ranged;        
            Item.UseSound = SoundID.Item10;
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-20, 0);
        }
        public override bool CanConsumeAmmo(Item ammo, Player player)
        {
            return Main.rand.NextFloat() >= NotConsumeAmmoChance/100f;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if(type == ProjectileID.PurificationPowder)
            {
                var a = Projectile.NewProjectile(source, position, velocity, ModContent.ProjectileType<CustomFlameTrowerProj>(), damage, knockback, player.whoAmI,0,0,Utils1.GetColorID(Color.OrangeRed));
                Main.projectile[a].scale = 12f;
            }
            return false;
        }
        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            velocity = velocity.RotatedByRandom(MathHelper.ToRadians(3));
        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ItemID.MechanicalWheelPiece, 1)
            .AddRecipeGroup(RecipeGroupID.IronBar, 20)
            .AddIngredient(ItemID.SoulofMight, 5)
            .AddIngredient(ItemID.HallowedBar, 10)
            .AddTile(TileID.MythrilAnvil)
            .Register();
        }
    }
}
