using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.Localization;
using RemnantOfTheAncientsMod.Content.Projectiles.Ranger;
using RemnantOfTheAncientsMod.Common.UtilsTweaks;
using System.Collections.Generic;

namespace RemnantOfTheAncientsMod.Content.Items.Weapons.Ranger.Flamethrower
{
    public class TinIgniter : ModItem
    {
        public override void SetStaticDefaults()
        {     
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }
        public int ArmorPenetration = 5;
        public int NotConsumeAmmoChance = 10;
        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(ArmorPenetration);
        public override void SetDefaults()
        { 
            Item baseItem = new(ItemID.Flamethrower);
            Item.damage = 15;
            Item.knockBack = 0.5f;
            Item.useTime = 4;
            Item.useAnimation = 40;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.rare = ItemRarityID.White;
            Item.shoot = ProjectileID.Flames;
            Item.shootSpeed = 4f;
            Item.useAmmo = AmmoID.Gel;
            Item.autoReuse = true;
            Item.value = Item.sellPrice(0, 0, 2, 0);
            Item.Size = new Vector2(80, 40);      
            Item.DamageType = DamageClass.Ranged;        
            Item.UseSound = baseItem.UseSound;
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(0, 0);
        }
        public override bool CanConsumeAmmo(Item ammo, Player player)
        {
            return !(player.itemAnimation < Item.useAnimation - 2) && Main.rand.NextFloat() >= NotConsumeAmmoChance / 100f;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if(type == ProjectileID.Flames)
            {
                int colorId = Utils1.GetColorID(Color.DarkOrange);
                var a = Projectile.NewProjectile(source, position, velocity, ModContent.ProjectileType<CustomFlameTrowerProj>(), damage, knockback, player.whoAmI,0,0, colorId);
                Main.projectile[a].scale = Main.rand.NextFloat(0.5f, 1f);
                Main.projectile[a].alpha = Main.rand.Next(0, 200);
            }
            return false;
        }
        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            velocity = velocity.RotatedByRandom(MathHelper.ToRadians(10));
        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ItemID.TinBar, 10)
            .AddRecipeGroup(RecipeGroupID.Wood, 10)
            .AddIngredient(ItemID.Torch, 10)
            .AddTile(TileID.Anvils)
            .Register();
        }
    }
}
