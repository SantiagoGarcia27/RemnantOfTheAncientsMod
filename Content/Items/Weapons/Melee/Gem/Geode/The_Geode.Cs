using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.GameContent.Creative;
using RemnantOfTheAncientsMod.Content.Items.Items;
using Terraria;
using Microsoft.Xna.Framework;
using RemnantOfTheAncientsMod.Content.Projectiles.Melee;
using System;
using Terraria.DataStructures;
using RemnantOfTheAncientsMod.World;

namespace RemnantOfTheAncientsMod.Content.Items.Weapons.Melee.Gem.Geode
{
    public class The_Geode : BaseGeode
    {
        public override int damage => 25;
        public override int attakSpeed => 18;
        public override int knockBack => 5;
        public override int GemId => 0;

        //public override void SetStaticDefaults()
        //{
        //    //DisplayName.SetDefault("The Geode");
        //    //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "La géode");
        //    //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "La geoda");

        //   //CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        //}      
    }
    public class The_Geode_Blue : BaseGeode
    {
        public override int damage => 24;
        public override int attakSpeed => 18;
        public override int knockBack => 5;
        public override int GemId => ItemID.LargeSapphire;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("The Geode");
            Tooltip.SetDefault("Inflict Slow on enemies" +
               "\nShoot a sapphire sword that fragments on hit");

            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "La géode");
            Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Inflige Ralentissement aux ennemis" +
              "\nTirez sur une épée de saphir qui se fragmente au coup");

            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "La geoda");
            Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Inflije lentitud a los enemigos" +
                 "\nDispara una espada de safiro que se fragmenta al golpear");

            //CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.shoot = ModContent.ProjectileType<SpiritGemSword>();
            Item.shootSpeed = 5f;
        }
        public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
        {
            base.OnHitNPC(player, target, damage, knockBack, crit);   
            target.AddBuff(BuffID.Slow, 240);
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            int proj = Projectile.NewProjectile(Projectile.GetSource_None(), player.position, velocity, ModContent.ProjectileType<SpiritGemSword>(), damage/4, knockBack, Main.myPlayer, 1, 2);
            Main.projectile[proj].scale *= Reaper.ReaperMode? 4.75f : 2.25f;
            Main.projectile[proj].DamageType = DamageClass.Melee;
            Main.projectile[proj].tileCollide = true;
            return false;
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
            Tooltip.SetDefault("50% chance to heal player 5hp per hit");

            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "La géode");
            Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "50% de chances de soigner le joueur de 5 PV par coup");

            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "La geoda");
            Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Tiene un 50% de provavilidad de curarte 5 de vida");

           //CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }
        public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
        {
            base.OnHitNPC(player, target, damage, knockBack, crit);
            if (Main.rand.NextBool(2)) player.Heal(5);
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
            Tooltip.SetDefault("Inflict fire on enemies" +
                "\n Create explosions on hit");

            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "La géode");
            Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Infliger des tirs aux ennemis" +
                "\n Créer des explosions au coup");

            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "La geoda");
            Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Inflije fuego a los enemigos" +
                  "\n Crea explociones al atacar");
           //CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }
        public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
        {
            base.OnHitNPC(player,target,damage,knockBack,crit);
            target.AddBuff(BuffID.OnFire, 240);
            if (new RemnantOfTheAncientsMod().ParticleMeter(4) != 0)
            {
                Projectile.NewProjectile(Projectile.GetSource_None(), target.position - new Vector2(0, 2*16) + new Vector2(1 *16, 0), new Vector2(0f, 0f), ProjectileID.DD2ExplosiveTrapT2Explosion, damage, 0);
            }
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
           //CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }
        public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
        {
            base.OnHitNPC(player, target, damage, knockBack, crit);
            target.AddBuff(BuffID.Confused, 240);
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
           //CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }
        public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
        {
            base.OnHitNPC(player, target, damage, knockBack, crit);
            target.AddBuff(BuffID.Ichor, 240);
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
            //CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }
    }
    public abstract class BaseGeode : ModItem
    {
        public abstract int damage { get; }
        public abstract int attakSpeed { get; }
        public abstract int knockBack { get; }
        public abstract int GemId { get; }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("The Geode");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "La géode");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "La geoda");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }
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


