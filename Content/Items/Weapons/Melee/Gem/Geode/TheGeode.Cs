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
using RemnantOfTheAncientsMod.Common.ModCompativilitie;
using RemnantOfTheAncientsMod.Common.UtilsTweaks;

namespace RemnantOfTheAncientsMod.Content.Items.Weapons.Melee.Gem.Geode
{
    public class TheGeode : BaseGeode
    {
        public override int damage => 25;
        public override int attakSpeed => 18;
        public override int knockBack => 5;
        public override int GemId => 0;
    }
    public class TheGeodeBlue : BaseGeode
    {
        public override int damage => 24;
        public override int attakSpeed => 18;
        public override int knockBack => 5;
        public override int GemId => ItemID.LargeSapphire;
 
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.shoot = ModContent.ProjectileType<SpiritGemSword>();
            Item.shootSpeed = 5f;
        }
        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            base.OnHitNPC(player, target, hit,damageDone);   
            target.AddBuff(BuffID.Slow, 240);
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            int colorID = Utils1.GetColorID(Color.Blue);
            int proj = Projectile.NewProjectile(Projectile.GetSource_None(), player.position, velocity, ModContent.ProjectileType<SpiritGemSword>(), damage/4, knockBack, Main.myPlayer, 1, colorID);
            Main.projectile[proj].scale *= Reaper.ReaperMode? 4.75f : 2.25f;
            Main.projectile[proj].DamageType = DamageClass.Melee;
            Main.projectile[proj].tileCollide = true;
            return false;
        }
    }
    public class TheGeodeGreen : BaseGeode
    {
        public override int damage => 25;
        public override int attakSpeed => 18;
        public override int knockBack => 5;
        public override int GemId => ItemID.LargeEmerald;  
        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            base.OnHitNPC(player, target, hit, damageDone);
            if (Main.rand.NextBool(2)) player.Heal(5);
        }
    }
    public class TheGeodeRed : BaseGeode
    {
        public override int damage => 25;
        public override int attakSpeed => 18;
        public override int knockBack => 5;
        public override int GemId => ItemID.LargeRuby; 
        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            base.OnHitNPC(player, target, hit, damageDone);
            target.AddBuff(BuffID.OnFire, 240);
            if (RemnantOfTheAncientsMod.ParticleMeter(4) != 0)
            {
                Projectile.NewProjectile(Projectile.GetSource_None(), target.position - new Vector2(0, 2*16) + new Vector2(1 *16, 0), new Vector2(0f, 0f), ProjectileID.DD2ExplosiveTrapT2Explosion, damage, 0);
            }
        }
    }
    public class TheGeodeViolet : BaseGeode
    {
        public override int damage => 25;
        public override int attakSpeed => 18;
        public override int knockBack => 5;
        public override int GemId => ItemID.LargeAmethyst;
        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            base.OnHitNPC(player, target, hit, damageDone);
            target.AddBuff(BuffID.Confused, 240);
        }
    }
    public class TheGeodeYellow : BaseGeode
    {
        public override int damage => 25;
        public override int attakSpeed => 18;
        public override int knockBack => 5;
        public override int GemId => ItemID.LargeTopaz;   
        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            base.OnHitNPC(player, target, hit, damageDone);
            target.AddBuff(BuffID.Ichor, 240);
        }
    }
    public class TheGeodeWhite : BaseGeode
    {
        public override int damage => 35;
        public override int attakSpeed => 18;
        public override int knockBack => 5;
        public override int GemId => ItemID.LargeDiamond; 
    }
    public class TheGeodeBrown : BaseGeode
    {
        public override int damage => 30;
        public override int attakSpeed => 18;
        public override int knockBack => 5;
        public override int GemId => ItemID.LargeAmber;
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.shoot = ProjectileID.Excalibur;
            Item.shootSpeed = 5f;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            float adjustedItemScale = player.GetAdjustedItemScale(Item); // Get the melee scale of the player and item.
           int p = Projectile.NewProjectile(source, player.MountedCenter, new Vector2(player.direction, 0f), type, damage, knockback, player.whoAmI, player.direction * player.gravDir, player.itemAnimationMax, adjustedItemScale * 1.2f);
            Main.projectile[p].GetAlpha(Color.SandyBrown);
            int a = Projectile.NewProjectile(source, player.MountedCenter, new Vector2(-player.direction, 0f), type, damage, knockback, player.whoAmI, player.direction * player.gravDir, player.itemAnimationMax, adjustedItemScale * 1.3f);
            Main.projectile[a].GetAlpha(Color.SandyBrown);


             p = Projectile.NewProjectile(source, player.MountedCenter, new Vector2(player.direction *2f, 0f), type, damage, knockback, player.whoAmI, player.direction * player.gravDir, player.itemAnimationMax, adjustedItemScale * 1.4f);
            Main.projectile[p].GetAlpha(Color.SandyBrown);
             a = Projectile.NewProjectile(source, player.MountedCenter, new Vector2(-player.direction *2f, 0f), type, damage, knockback, player.whoAmI, player.direction * player.gravDir, player.itemAnimationMax, adjustedItemScale * 1f);
            Main.projectile[a].GetAlpha(Color.SandyBrown);
            NetMessage.SendData(MessageID.PlayerControls, -1, -1, null, player.whoAmI); // Sync the changes in multiplayer.

            return base.Shoot(player, source, position, velocity, type, damage, knockback);
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
        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {

            int numProjectiles = 6;
            float spacing = MathHelper.TwoPi / numProjectiles;
            float radius = 10 * 16f;

            for (int i = 0; i < numProjectiles; i++)
            {
                Vector2 projectilePos = target.Center + radius * new Vector2((float)Math.Cos(spacing * i), (float)Math.Sin(spacing * i));
                Vector2 projectileVel = 10f * (target.Center - projectilePos).SafeNormalize(Vector2.Zero);

                int proj = Projectile.NewProjectile(Projectile.GetSource_None(), projectilePos, projectileVel, ModContent.ProjectileType<SpiritGemSword>(), damage / 3, knockBack, Main.myPlayer, 0, -1);
                float angle = (float)Math.Atan2(projectileVel.Y, projectileVel.X);
                Main.projectile[proj].rotation = angle;
            }
        }  
        public override void AddRecipes()
        {
            if (GemId != 0)
            {
                CreateRecipe()
                .AddIngredient(ModContent.ItemType<TheGeode>())
                .AddIngredient(GemId, 1)
                .AddTile(TileID.Anvils)
                .Register();
            }
            else
            {
                CreateRecipe()
                .AddIngredient(ModContent.ItemType<AmethystSword>())
                .AddIngredient(ModContent.ItemType<TopazSword>())
                .AddIngredient(ModContent.ItemType<SapphireSword>())
                .AddIngredient(ModContent.ItemType<RubySword>())
                .AddIngredient(ModContent.ItemType<EmeraldSword>())
                .AddIngredient(ModContent.ItemType<AmberSword>())
                .AddIngredient(ModContent.ItemType<DiamondSword>())
                .AddIngredient(ModContent.ItemType<BoneBar>(), 2)
                .AddIngredient(ItemID.StoneBlock, 10)
                .AddTile(TileID.Anvils)
                .Register();
            }
        }
    }
}


