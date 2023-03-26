using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.GameContent.Creative;
using RemnantOfTheAncientsMod.Items.Items;
using Terraria;
using Microsoft.Xna.Framework;
using RemnantOfTheAncientsMod.Projectiles;
using RemnantOfTheAncientsMod.Projectiles.Melee;
using static Terraria.ModLoader.PlayerDrawLayer;
using System;

namespace RemnantOfTheAncientsMod.Items.Mele.Gem.Geode
{
    public class The_Geode : BaseGeode
    {
        public override int damage => 25;
        public override int attakSpeed => 18;
        public override int knockBack => 5;
        public override int GemId => 0;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("The Geode");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Polish), "The Geode");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "La géode");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "La geoda");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }   
    }
    public class The_Geode_Blue : BaseGeode
    {
        public override int damage => 26;
        public override int attakSpeed => 18;
        public override int knockBack => 5;
        public override int GemId => ItemID.LargeSapphire;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("The Geode");
            Tooltip.SetDefault("Inflict Slow on enemies");

            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "La géode");
            Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Infliger Slow sur les ennemis");

            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "La geoda");
            Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Inflije lentitud a los enemigos");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }
    }
    public class The_Geode_Green : BaseGeode
    {
        public override int damage => 25;
        public override int attakSpeed => 18;
        public override int knockBack => 5;
        public override int GemId => ItemID.LargeEmerald;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("The Geode");
            Tooltip.SetDefault("Inflict poisoned on enemies");

            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "La géode");
            Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Infliger du poison aux ennemis");

            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "La geoda");
            Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Inflije veneno a los enemigos");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }
    }
    public class The_Geode_Red : BaseGeode
    {
        public override int damage => 25;
        public override int attakSpeed => 18;
        public override int knockBack => 5;
        public override int GemId => ItemID.LargeRuby;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("The Geode");
            Tooltip.SetDefault("Inflict fire on enemies");

            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "La géode");
            Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Infliger des tirs aux ennemis");

            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "La geoda");
            Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Inflije fuego a los enemigos");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }
    }
    public class The_Geode_Vi : BaseGeode
    {
        public override int damage => 25;
        public override int attakSpeed => 18;
        public override int knockBack => 5;
        public override int GemId => ItemID.LargeAmethyst;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("The Geode");
            Tooltip.SetDefault("Inflict Confused on enemies");

            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "La géode");
            Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Inflict Confused sur les ennemis");

            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "La geoda");
            Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Inflije confución a los enemigos");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }
    }
    public class The_Geode_Yellow : BaseGeode
    {
        public override int damage => 25;
        public override int attakSpeed => 18;
        public override int knockBack => 5;
        public override int GemId => ItemID.LargeTopaz;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("The Geode");
            Tooltip.SetDefault("Inflict Ichor on enemies");

            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "La géode");
            Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Infligez Ichor aux ennemis");

            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "La geoda");
            Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Inflije Icor a los enemigos");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }
    }
    public class The_Geode_White : BaseGeode
    {
        public override int damage => 30;
        public override int attakSpeed => 18;
        public override int knockBack => 5;
        public override int GemId => ItemID.LargeDiamond;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("The Geode");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "La géode");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "La geoda");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }
    }
    public abstract class BaseGeode : ModItem
    {
        public abstract int damage { get; }
        public abstract int attakSpeed { get; }
        public abstract int knockBack { get; }
        public abstract int GemId { get; }
        public override void SetDefaults()
        {
            Item.damage = damage;
            Item.DamageType = DamageClass.Melee;
            Item.width = 88;
            Item.height = 88;
            Item.useTime = attakSpeed;
            Item.useAnimation = attakSpeed;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = knockBack;
            Item.value = 1200;
            Item.rare = ItemRarityID.Orange;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
        }
        public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
        {

            int numProjectiles = 6;
            float spacing = MathHelper.TwoPi / numProjectiles;
            float radius = 10 * 16f;

            for (int i = 0; i < numProjectiles; i++)
            {
                Vector2 projectilePos = target.Center + radius * new Vector2((float)Math.Cos(spacing * i), (float)Math.Sin(spacing * i));
                Vector2 projectileVel = 10f * (target.Center - projectilePos).SafeNormalize(Vector2.Zero);

              int proj = Projectile.NewProjectile(Projectile.GetSource_None(), projectilePos, projectileVel, ModContent.ProjectileType<SpiritGemSword>(), damage/3, knockBack, Main.myPlayer, 0, 6);
                float angle = (float)Math.Atan2(projectileVel.Y, projectileVel.X);
                Main.projectile[proj].rotation = angle;
            }
        }
        //public void SpawnSpiritSword(Player player,NPC target)
        //{
        //    Vector2 Position = Main.rand.NextVector2CircularEdge(target.width + 100, target.height + 100) + target.position;
        //    Vector2 Velocity = Position.DirectionTo(target.position) * 0.1f;
        //    var j = Projectile.NewProjectileDirect(Projectile.GetSource_None(), Position, Velocity, ModContent.ProjectileType<SpiritGemSword>(), damage / 2, Main.myPlayer, player.whoAmI,0,6);
        //}
        public override void AddRecipes()
        {
            if (GemId != 0)
            {
                CreateRecipe()
                .AddIngredient(ModContent.ItemType<The_Geode>())
                .AddIngredient(GemId, 1)
                .AddTile(TileID.Anvils)
                .Register();
            }
            else
            {
                CreateRecipe()
                .AddIngredient(ModContent.ItemType<Amethyst_Sword>())
                .AddIngredient(ModContent.ItemType<Topaz_Sword>())
                .AddIngredient(ModContent.ItemType<Sapphire_Sword>())
                .AddIngredient(ModContent.ItemType<Ruby_Sword>())
                .AddIngredient(ModContent.ItemType<Emerald_Sword>())
                .AddIngredient(ModContent.ItemType<Diamond_Sword>())
                .AddIngredient(ModContent.ItemType<BoneBar>(), 2)
                .AddIngredient(ItemID.StoneBlock, 10)
                .AddTile(TileID.Anvils)
                .Register();
            }
        }
    }
}


