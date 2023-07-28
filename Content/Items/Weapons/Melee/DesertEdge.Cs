using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using static Terraria.ModLoader.ModContent;
using Terraria.GameContent.Creative;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using RemnantOfTheAncientsMod.Content.Projectiles.BossProjectile;
using RemnantOfTheAncientsMod.Content.Projectiles.Melee;
using RemnantOfTheAncientsMod.Content.Items.Items;
using RemnantOfTheAncientsMod.Content.Projectiles;
using Mono.Cecil;
using static Terraria.ModLoader.PlayerDrawLayer;
using RemnantOfTheAncientsMod.Content.Buffs.Debuff;

namespace RemnantOfTheAncientsMod.Content.Items.Weapons.Melee
{
    public class DesertEdge : ModItem
    {
        public override void SetStaticDefaults()
        {
           // //DisplayName.SetDefault("Desert Edge");
           // //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Épée des désert");
           // //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Filo del desierto");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }
        public override void SetDefaults()
        {
            Item.damage = 25;
            Item.DamageType = DamageClass.Melee;
            Item.width = 60;
            Item.height = 60;
            Item.useTime = 30;
            Item.useAnimation = 30;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 1;
            Item.autoReuse = true;
            Item.UseSound = SoundID.Item39;
            Item.value = 100900;
            Item.rare = ItemRarityID.Green;
            if (RemnantOfTheAncientsMod.TerrariaOverhaul != null)
            {
                if (ModContent.GetInstance<ConfigServer>().OverhaulMeleeManaCostConfig) Item.shoot = ProjectileType<DesertS>();
            }
            else Item.shoot = ProjectileType<DesertS>();
            Item.shootSpeed = 10f;
            Item.noUseGraphic = false;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            float numberProjectiles = 2;
            float rotation = MathHelper.ToRadians(10);
          
            position += Vector2.Normalize(velocity) * 1f;

            for (int i = 0; i < numberProjectiles; i++)
            {
                Vector2 perturbedSpeed = velocity.RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))) * .2f;
                Projectile.NewProjectile(source, position, perturbedSpeed * 2, ProjectileType<DesertTyphoonFriendly>(), damage / 2, knockback, player.whoAmI);
            }
            Projectile.NewProjectile(source, position, velocity, type, damage, 1, player.whoAmI);
          
            return false;
        }
        public override bool CanUseItem(Player player)
        {
            if (RemnantOfTheAncientsMod.TerrariaOverhaul != null && !ModContent.GetInstance<ConfigServer>().OverhaulMeleeManaCostConfig)
            {
                float numberProjectiles = 2;
                float rotation = MathHelper.ToRadians(10);
                Vector2 velocity = Vector2.Normalize(Main.MouseWorld - player.position) * Item.shootSpeed;
                Vector2 position = player.position;

                player.position += Vector2.Normalize(velocity) * 1f;

                for (int i = 0; i < numberProjectiles; i++)
                {
                    Vector2 perturbedSpeed = velocity.RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))) * .2f;
                    Projectile.NewProjectile(Projectile.GetSource_None(), position, perturbedSpeed * 2, ProjectileType<DesertTyphoonFriendly>(), Item.damage / 2, Item.knockBack, player.whoAmI);
                }
                Projectile.NewProjectile(Projectile.GetSource_None(), position, velocity, ProjectileType<DesertS>(), Item.damage, 1, player.whoAmI);
            }
            return base.CanUseItem(player);
        }
        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffType<Burning_Sand>(), 300);
        }
        public override void AddRecipes()
        {
            CreateRecipe()
             .AddIngredient(ItemID.PlatinumBroadsword, 1)
             .AddIngredient(ModContent.ItemType<Sand_escense>(), 5)
             .AddTile(TileID.Anvils)
             .Register();
        }
    }
}
