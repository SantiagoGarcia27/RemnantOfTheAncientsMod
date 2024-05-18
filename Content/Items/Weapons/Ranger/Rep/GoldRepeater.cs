using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace RemnantOfTheAncientsMod.Content.Items.Weapons.Ranger.Rep
{
    public class GoldRepeater : ModItem
    {
        public override void SetStaticDefaults()
        {
        }
        public override void SetDefaults()
        {
            Item.damage = 9;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 12;
            Item.height = 38;
            Item.maxStack = 1;
            Item.useTime = 26;
            Item.useAnimation = 26;
            Item.useStyle = 5;
            Item.knockBack = 2;
            Item.value = 12000;
            Item.rare = 2;
            Item.UseSound = SoundID.Item5;
            Item.noMelee = true;
            Item.shoot = 1;
            Item.useAmmo = AmmoID.Arrow;
            Item.shootSpeed = 10f;
            Item.autoReuse = true;
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-10, 0);
        }

        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ItemID.GoldBar, 7)
            .AddTile(TileID.Anvils)
            .Register();
        }
    }
}