using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Localization;
using Terraria.GameContent.Creative;

namespace RemnantOfTheAncientsMod.Content.Items.Weapons.Ranger
{
    public class SuperShark : ModItem
    {
        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(40);
        public override void SetStaticDefaults()
        {
           // //DisplayName.SetDefault("Supershark");
           // //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "superrequin");
           // //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Supertiburón");
            //Tooltip.SetDefault("40% chance of not spending ammo");
            //Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "40% de chances de ne pas dépenser de munitions ");
            //Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Probabilidad del 40% de no gastar munición ");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.damage = 70;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 80;
            Item.height = 40;
            Item.useTime = 9;
            Item.useAnimation = 10;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noMelee = true;
            Item.knockBack = 1;
            Item.value = 100000;
            Item.rare = ItemRarityID.Red;
            Item.UseSound = SoundID.Item31;
            Item.autoReuse = true;
            Item.shoot = ProjectileID.JestersArrow;
            Item.shootSpeed = 100f;
            Item.useAmmo = AmmoID.Bullet;

            if (RemnantOfTheAncientsMod.CalamityMod != null)
            {
                Item.useTime = 5;
                Item.useAnimation = 5;
            }

        }
        public override bool CanConsumeAmmo(Item ammo, Player player)
        {
            return Main.rand.NextFloat() >= .40f;
        }
        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            velocity = new Vector2(velocity.X, velocity.Y).RotatedByRandom(MathHelper.ToRadians(5));
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-20, 0);
        }

        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ItemID.Megashark, 1)
            .AddIngredient(ItemID.FragmentVortex, 10)
            .AddTile(TileID.LunarCraftingStation)
            .Register();
        }
    }
}
