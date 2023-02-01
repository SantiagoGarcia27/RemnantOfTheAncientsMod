using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.GameContent.Creative;
using RemnantOfTheAncientsMod.Items.Items;
using Terraria;
using Microsoft.Xna.Framework;
using RemnantOfTheAncientsMod.Projectiles;

namespace RemnantOfTheAncientsMod.Items.Mele.Gem.Geode
{
    public class The_Geode : BaseGeode
    {
        public override int damage => 25;
        public override int attakSpeed => 18;
        public override int knockBack => 5;
        public override int Debuff => 0;
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
        public override int Debuff => BuffID.Slow;
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
        public override int Debuff => BuffID.Poisoned;
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
        public override int Debuff => BuffID.OnFire;
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
        public override int Debuff => BuffID.Confused;
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
        public override int Debuff => BuffID.Ichor;
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
        public override void SetDefaults()
        {
            Item.damage = 25;
            Item.DamageType = DamageClass.Melee;
            Item.width = 88;
            Item.height = 88;
            Item.useTime = 18;
            Item.useAnimation = 18;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 5;
            Item.value = 1200;
            Item.rare = ItemRarityID.Orange;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = false;

        }
        public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
        {
            Vector2 perturbedSpeed = getPerturbatedSpeed(player, target, 0, "-");
            Projectile.NewProjectile(Projectile.GetSource_None(), new Vector2(target.position.X - ((target.direction * 6) * 16), target.position.Y), (perturbedSpeed) * 0.25f, ModContent.ProjectileType<DiamondSpiritGemSword>(), damage, Main.myPlayer, player.whoAmI);
            perturbedSpeed = getPerturbatedSpeed(player, target, MathHelper.ToRadians(50 * -target.direction), "-");
            Projectile.NewProjectile(Projectile.GetSource_None(), new Vector2(target.position.X - ((target.direction * 7) * 16), target.position.Y + 10 * 16), (perturbedSpeed) * 0.25f, ModContent.ProjectileType<SaphireSpiritGemSword>(), damage, Main.myPlayer, player.whoAmI);
        }
    }
    public class The_Geode_White : BaseGeode
    {
        public override int damage => 30;
        public override int attakSpeed => 18;
        public override int knockBack => 5;
        public override int Debuff => 0;
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
        public abstract int Debuff { get; }
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
            target.AddBuff(Debuff, 400);
            Vector2 perturbedSpeed = getPerturbatedSpeed(player, target, 0, "-");
            Projectile.NewProjectile(Projectile.GetSource_None(), new Vector2(target.position.X - ((target.direction * 6) * 16), target.position.Y), (perturbedSpeed) * 0.25f, ModContent.ProjectileType<DiamondSpiritGemSword>(), damage / 5, Main.myPlayer, player.whoAmI);
            perturbedSpeed = getPerturbatedSpeed(player, target, MathHelper.ToRadians(50 * -target.direction), "-");
            Projectile.NewProjectile(Projectile.GetSource_None(), new Vector2(target.position.X - ((target.direction * 7) * 16), target.position.Y + 10 * 16), (perturbedSpeed) * 0.25f, ModContent.ProjectileType<SaphireSpiritGemSword>(), damage / 5, Main.myPlayer, player.whoAmI);
            perturbedSpeed = getPerturbatedSpeed(player, target, MathHelper.ToRadians(50 * target.direction), "-");
            Projectile.NewProjectile(Projectile.GetSource_None(), new Vector2(target.position.X - ((target.direction * 7) * 16), target.position.Y - 10 * 16), (perturbedSpeed) * 0.25f, ModContent.ProjectileType<EmeraldSpiritGemSword>(), damage / 5, Main.myPlayer, player.whoAmI);
            perturbedSpeed = getPerturbatedSpeed(player, target, 0, "+");
            Projectile.NewProjectile(Projectile.GetSource_None(), new Vector2(target.position.X + ((target.direction * 6) * 16), target.position.Y + 1 * 16), (perturbedSpeed) * 0.25f, ModContent.ProjectileType<RubySpiritGemSword>(), damage / 5, Main.myPlayer, player.whoAmI);
            perturbedSpeed = getPerturbatedSpeed(player, target, MathHelper.ToRadians(50 * target.direction), "+");
            Projectile.NewProjectile(Projectile.GetSource_None(), new Vector2(target.position.X - ((target.direction * -4) * 16), target.position.Y + 6 * 16), (perturbedSpeed) * 0.25f, ModContent.ProjectileType<TopazSpiritGemSword>(), damage / 5, Main.myPlayer, player.whoAmI);
            perturbedSpeed = getPerturbatedSpeed(player, target, MathHelper.ToRadians(50 * -target.direction), "+");
            Projectile.NewProjectile(Projectile.GetSource_None(), new Vector2(target.position.X - ((target.direction * -7) * 16), target.position.Y - 6 * 16), (perturbedSpeed) * 0.25f, ModContent.ProjectileType<AmathystSpiritGemSword>(), damage / 5, Main.myPlayer, player.whoAmI);
        }

        public Vector2 getPerturbatedSpeed(Player player, NPC target, float rotation, string signo)
        {
            if (signo == "-")
            {
                return (player.position - target.position).RotatedBy(MathHelper.Lerp(-rotation, rotation, 1)) * .2f;
            }
            else return (target.position - player.position).RotatedBy(MathHelper.Lerp(-rotation, rotation, 1)) * .2f;
        }
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


