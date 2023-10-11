using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.GameContent.Creative;
using Terraria.Localization;
using Microsoft.Xna.Framework;
using RemnantOfTheAncientsMod.Content.Projectiles.Mage;
using Terraria.DataStructures;
using System;
using RemnantOfTheAncientsMod.Content.Projectiles.HeldItem;
using RemnantOfTheAncientsMod.Common.Global;

namespace RemnantOfTheAncientsMod.Content.Items.Weapons.Magic
{
    public class CorrupedRay : ModItem
    {
        public override void SetStaticDefaults()
        {
           // //DisplayName.SetDefault("Shadow Ray");
           // //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Polish), "Promień cienia");
           // //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Rayon d'ombre");
           // //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Rayo de sombra");
            Item.staff[Item.type] = true;
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
            ItemID.Sets.ItemsThatAllowRepeatedRightClick[Type] = true;
        }

        public override void SetDefaults()
        {
            Item.damage = 24;
            Item.noMelee = true;
            Item.DamageType = DamageClass.Magic;
            Item.channel = true;
            Item.mana = 9;
            Item.rare = ItemRarityID.Blue;
            Item.width = 46;
            Item.height = 54;
            Item.useTime = 23;
            Item.UseSound = SoundID.Item34;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.shootSpeed = 14f;
            Item.useAnimation = 49;
            Item.shoot = ProjectileID.AmethystBolt;
            Item.value = Item.sellPrice(silver: 30);
            Item.autoReuse = true;
            Item.GetGlobalItem<CustomTooltip>().SecondHabilitie = true;
            if (RemnantOfTheAncientsMod.CalamityMod != null)
            {
                Item.damage = 26;
                Item.useTime = 15;
            }
        }
        public override bool AltFunctionUse(Player player) => true;
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            int Shootproj = ModContent.ProjectileType<CorruptRayHeldProj>();

            if (player.altFunctionUse == 2)
            {
                Vector2 vector2 = velocity;

                float attackSpeedBonus = player.GetAttackSpeed(DamageClass.Generic);
                Item.useTime = (int)(65 / attackSpeedBonus);
                Item.useAnimation = (int)(65 / attackSpeedBonus);
                Item.mana = 30;

                if (player.ownedProjectileCounts[Shootproj] <= 0)
                {
                    for (int proj = 0; proj <= Main.maxProjectiles; proj++)
                    {
                        if (Main.projectile[proj].type == Shootproj && Main.projectile[proj].owner == player.whoAmI)
                        {
                            Main.projectile[proj].Kill();
                        }
                    }
                }

                if (player.ownedProjectileCounts[Shootproj] <= 0)
                {
                    Projectile.NewProjectile(source, player.position, Vector2.Zero, Shootproj, 0, 0, Main.myPlayer, velocity.X, velocity.Y,attackSpeedBonus);
                }

                return false;
            }
            else
            {
                Item.mana = 9;
                Item.useAnimation = 49;
                Item.useTime = RemnantOfTheAncientsMod.CalamityMod != null ? 15 : 23;

                return true;
            }

        }
        public void SpawnParticles(Player player, Vector2 position)
        {
            const int NUM_DUSTS = 150;
            for (int j = 0; j < ModContent.GetInstance<RemnantOfTheAncientsMod>().ParticleMeter(NUM_DUSTS); j++)
            {
                Vector2 Position = Main.rand.NextVector2CircularEdge(player.width + 100, player.height + 100) + player.position;
                Vector2 Velocity = Position.DirectionTo(position) * 1f;

                var a = Dust.NewDustPerfect(Position, DustID.CorruptTorch, Velocity, 100, Color.Purple, 3f);
                a.noGravity = true;
                var m = Dust.NewDustPerfect(Main.rand.NextVector2Square(player.width + 200, player.height + 200) + player.position, DustID.CorruptTorch, Velocity, 100, Color.Purple, 3f);
                m.noGravity = true;
                var o = Dust.NewDustPerfect(Main.rand.NextVector2Square(player.width - 200, player.height - 200) + player.position, DustID.CorruptTorch, Velocity, 100, Color.Purple, 3f);
                o.noGravity = true;
            }
        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ItemID.DemoniteBar, 10)
            .AddTile(TileID.WorkBenches)
            .Register();
        }
    }
}
