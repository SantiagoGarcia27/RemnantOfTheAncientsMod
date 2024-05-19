using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.DataStructures;

namespace RemnantOfTheAncientsMod.Content.Items.Weapons.Ranger.Rep
{
    public class CopperRepeater : ModItem
    {
        float Timmer = 0;
        float UseTimeReduction = 0;
        public override void SetStaticDefaults()
        {
        }
        public override void SetDefaults()
        {
            Item Base = new(ItemID.CopperBow);
            Item.DefaultToRangedWeapon(ProjectileID.WoodenArrowFriendly, AmmoID.Arrow, 30, 10f, true);
            Item.SetWeaponValues(Base.damage -2, Base.knockBack);
            Item.SetShopValues((Terraria.Enums.ItemRarityColor)Base.rare, Base.value);
            Item.Size = new Vector2(12, 38);
            Item.DamageType = DamageClass.Ranged;
            Item.UseSound = SoundID.Item12;
            Item.channel = true;
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-10, 0);
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {   
            return base.Shoot(player, source, position, velocity, type, damage, knockback);
        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ItemID.CopperBar, 7)
            .AddTile(TileID.Anvils)
            .Register();
        }
    }
}