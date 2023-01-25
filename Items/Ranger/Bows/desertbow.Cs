using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Localization;
using RemnantOfTheAncientsMod.Projectiles;
using Terraria.GameContent.Creative;
using RemnantOfTheAncientsMod.Projectiles.BossProjectile;
using Terraria.DataStructures;

namespace RemnantOfTheAncientsMod.Items.Ranger.Bows
{
    public class desertbow : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Desert Bow");
            Tooltip.SetDefault(""
            + "\nTransform normal arrows in desert arrows");

            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Arc du désert");
            Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), ""
            + "\nTransformez les flèches normales en flèches du désert");

            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Arco Desertico");
            Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), ""
            + "\nTransforma flechas normales en flechas del deserticas");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }
        public override void SetDefaults()
        {
            Item.damage = 25;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 12;
            Item.height = 38;
            Item.maxStack = 1;
            Item.useTime = 28;
            Item.useAnimation = 28;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.knockBack = 2;
            Item.value = 12000;
            Item.rare = ItemRarityID.Green;
            Item.UseSound = SoundID.Item5;
            Item.noMelee = true;
            Item.shoot = ProjectileID.WoodenArrowFriendly;
            Item.useAmmo = AmmoID.Arrow;
            Item.shootSpeed = 10f;
            Item.autoReuse = true;
        }
        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            if (type == ProjectileID.WoodenArrowFriendly)
            {
                type = ModContent.ProjectileType<DesertArrowP>();
            }
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            float numberProjectiles = 2;
            float rotation = MathHelper.ToRadians(5);

            position += Vector2.Normalize(velocity) * 1f;

            for (int i = 0; i < numberProjectiles; i++)
            {
                Vector2 perturbedSpeed = velocity.RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))) * .2f;
                int p = Projectile.NewProjectile(source, position, perturbedSpeed * 10, ModContent.ProjectileType<DesertArrowP>(), damage, knockback, player.whoAmI);
                Main.projectile[p].stepSpeed = 10f;
            }
            //Projectile.NewProjectile(source, position, velocity, type, damage, 1, player.whoAmI);
            return true;
        }
    }
}
