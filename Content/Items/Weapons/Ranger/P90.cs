using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Localization;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;

namespace RemnantOfTheAncientsMod.Content.Items.Weapons.Ranger
{
    public class P90 : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("P90");
            Tooltip.SetDefault("Fast and effective");
            Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Rapide et efficace");
            Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Rapida y efectiva");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }
        public override void SetDefaults()
        {
            Item.damage = 13;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 80;
            Item.height = 40;
            Item.useTime = 4;
            Item.useAnimation = 4;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noMelee = true;
            Item.knockBack = 1;
            Item.value = 100000;
            Item.rare = ItemRarityID.LightPurple;
            Item.UseSound = SoundID.Item10;
            Item.autoReuse = true;
            Item.shoot = ProjectileID.PurificationPowder;
            Item.shootSpeed = 100f;
            Item.useAmmo = AmmoID.Bullet;
            Item.expert = true;
            if (RemnantOfTheAncientsMod.CalamityMod != null)
            {
                Item.damage = 14;
            }
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-20, 0);
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Vector2 perturbedSpeed = new Vector2(velocity.X, velocity.Y).RotatedByRandom(MathHelper.ToRadians(3));
            velocity.X = perturbedSpeed.X;
            velocity.Y = perturbedSpeed.Y;
            return true;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ItemID.MechanicalWheelPiece, 1)
            .AddRecipeGroup("IronBar", 20)
            .AddIngredient(ItemID.SoulofMight, 5)
            .AddIngredient(ItemID.HallowedBar, 10)
            .AddTile(TileID.MythrilAnvil)
            .Register();
        }
    }
}
