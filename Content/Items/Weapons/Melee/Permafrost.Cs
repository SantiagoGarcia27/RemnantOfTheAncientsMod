using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using RemnantOfTheAncientsMod.Content.Projectiles.Multiclass;
using Terraria.Localization;
using static Terraria.ModLoader.ModContent;
using Terraria.GameContent.Creative;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using RemnantOfTheAncientsMod.Content.Projectiles.Melee;
using RemnantOfTheAncientsMod.Common.Global;
using RemnantOfTheAncientsMod.Content.Items.Items;

namespace RemnantOfTheAncientsMod.Content.Items.Weapons.Melee
{
    public class Permafrost : ModItem
    {
        public override void SetStaticDefaults()
        {
           // //DisplayName.SetDefault("Permafrost");
           // //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Le pergélisol");
           // //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Permafrost");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.damage = RemnantOfTheAncientsMod.CalamityMod != null? 125: 45;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.width = 72;
            Item.height = 72;
            Item.useTime = 40;
            Item.useAnimation = 25;
            Item.shootSpeed = 3.7f;
            Item.knockBack = 6.5f;
            Item.scale = 1f;
            Item.rare = ItemRarityID.Pink;
            Item.value = Item.sellPrice(silver: 10);
            Item.DamageType = DamageClass.Melee;
            Item.autoReuse = true;
            Item.UseSound = SoundID.Item1;
            if (RemnantOfTheAncientsMod.TerrariaOverhaul != null)
            {
                if (GetInstance<ConfigServer>().OverhaulMeleeManaCostConfig) Item.shoot = ProjectileType<FrostBeamF>();
            }
            else Item.shoot = ProjectileType<FrostBeamF>();
            Item.shootSpeed = 7f;
            Item.GetGlobalItem<CustomTooltip>().SecondHabilitie = true;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
             .AddIngredient(ItemID.TitaniumSword, 1)
             .AddIngredient(ModContent.ItemType<Ice_escense>(), 5)
             .AddTile(TileID.Anvils)
             .Register();
        }
        public override bool AltFunctionUse(Player player) => true;
        public override bool CanUseItem(Player player)
        {
            var source = Projectile.GetSource_None();
            Vector2 position = player.position;
            Vector2 velocity = Vector2.Normalize(Main.MouseWorld - player.position) * Item.shootSpeed;
            int damage = Item.damage;
            float knockback = Item.knockBack;

            if (player.altFunctionUse != 2)
            {

                if (RemnantOfTheAncientsMod.TerrariaOverhaul != null && !GetInstance<ConfigServer>().OverhaulMeleeManaCostConfig)
                {
                    Projectile.NewProjectile(source, position, velocity, ProjectileType<FrostBeamF>(), damage, knockback, player.whoAmI);
                }
                else
                {
                    Item.shoot = ProjectileType<FrostBeamF>();
                }
                Item.useTime = 40;
                Item.useAnimation = 25;
                Item.useStyle = ItemUseStyleID.Swing;
                Item.staff[Item.type] = false;
            }
            else
            {
                if (RemnantOfTheAncientsMod.TerrariaOverhaul != null && !GetInstance<ConfigServer>().OverhaulMeleeManaCostConfig)
                {
                    shootSecondAttack(player, position, velocity, ProjectileType<PermafrostRain>(), damage, knockback);
                }
                else
                {
                    Item.shoot = ProjectileType<PermafrostRain>();
                }
                Item.useTime = 85;
                Item.useAnimation = 85;
                Item.useStyle = ItemUseStyleID.Shoot;
                Item.channel = true;
                Item.staff[Item.type] = true;
            }
            Item.channel = RemnantOfTheAncientsMod.MeleeWeaponEffects != null && player.altFunctionUse == 2;
            return base.CanUseItem(player);
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (player.altFunctionUse != 2)
            {
                int projectileID = Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI);
            }
            else
            {
                shootSecondAttack(player,  position, velocity, type, damage, knockback);
            }
            return false;
        }
        private void shootSecondAttack(Player player, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Vector2 target = Main.screenPosition + new Vector2(Main.mouseX, Main.mouseY);
            float ceilingLimit = target.Y;
            if (ceilingLimit > player.Center.Y - 200f)
            {
                ceilingLimit = player.Center.Y - 200f;
            }     
            for (int i = 0; i < 3; i++)
            {
                position = player.Center - new Vector2(Main.rand.NextFloat(401) * player.direction, 600f);
                position.Y -= 100 * i;
                Vector2 heading = target - position;

                if (heading.Y < 0f)
                {
                    heading.Y *= -1f;
                }

                if (heading.Y < 20f)
                {
                    heading.Y = 20f;
                }

                heading.Normalize();
                heading *= velocity.Length();
                heading.Y += Main.rand.Next(-40, 41) * 0.02f;
                Projectile.NewProjectile(Projectile.GetSource_None(), position, heading, type, damage, knockback, player.whoAmI, 0f, ceilingLimit);
            }
        }
    }
}
