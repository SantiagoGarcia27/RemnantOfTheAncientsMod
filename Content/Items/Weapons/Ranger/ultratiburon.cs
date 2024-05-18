using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Localization;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using RemnantOfTheAncientsMod.Content.Items.Items;
using RemnantOfTheAncientsMod.Content.Projectiles.Ranger;

namespace RemnantOfTheAncientsMod.Content.Items.Weapons.Ranger
{
    public class Ultratiburon : ModItem
    {
        public int NotConsumeAmmoChance = 70;
        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(NotConsumeAmmoChance);
        public override void SetStaticDefaults()
        {   
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }
        public override void SetDefaults()
        {
            Item.DefaultToRangedWeapon(ProjectileID.PurificationPowder, AmmoID.Bullet, 6, 10f, true);
            Item.SetWeaponValues(120, 1f);
            Item.SetShopValues((Terraria.Enums.ItemRarityColor)ItemRarityID.Red, Item.sellPrice(0, 20, 2, 0));
            Item.Size = new Vector2(86, 34);
            Item.DamageType = DamageClass.Ranged;     
            Item.UseSound = SoundID.Item12;
        }
        public override bool CanConsumeAmmo(Item ammo, Player player)
        {
            return Main.rand.NextFloat() >= NotConsumeAmmoChance/100f;
        }
        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            velocity = velocity.RotatedByRandom(MathHelper.ToRadians(3));
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-30, 0);
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (Main.rand.NextFloat() <= (float)1 / 10)
            {
                Projectile.NewProjectileDirect(source, position, new Vector2(velocity.X * 2, velocity.Y), ModContent.ProjectileType<MisilUltra>(), damage * 2, 1, player.whoAmI, 0, 4);
            }
            return true;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ModContent.ItemType<SuperShark>())
            .AddIngredient(ItemID.VortexBeater, 1)
            .AddIngredient(ItemID.Uzi, 1)
            .AddIngredient(ItemID.SDMG, 1)
            .AddIngredient(ModContent.ItemType<CelestialAmalgamate>(), 20)
            .AddTile(TileID.LunarCraftingStation)
            .Register();
        }
    }
}
