using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.GameContent.Creative;
using CalamityMod;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using RemnantOfTheAncientsMod.Content.Projectiles.Trower;

namespace RemnantOfTheAncientsMod.Content.Items.Weapons.Throwing
{
    public class Golden_Shuriken : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Golden Shuriken");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Shuriken doré");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Shuriken dorado");

            if(RemnantOfTheAncientsMod.CalamityMod != null)
            {
                Tooltip.SetDefault("Stealth Strike double the enemy penetration");
                Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "El ataque con sigilo tiene el doble de penetración de enemigos");
               Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Stealth Strike double la pénétration de l'ennemi");
            }
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 99;
        }
        public override void SetDefaults()
        {
            if (RemnantOfTheAncientsMod.CalamityMod != null) SetDefaultsCalamity();
            else 
            { 
                Item.DamageType = DamageClass.Throwing;
                Item.damage = 30;
            }
            Item.noMelee = true;
            Item.width = 20;
            Item.height = 20;
            Item.useTime = 15;
            Item.useAnimation = 15;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 6;
            Item.value = 4;
            Item.rare = 1;
            Item.reuseDelay = 10;
            Item.UseSound = SoundID.Item18;
            Item.autoReuse = false;
            Item.shoot = ModContent.ProjectileType<Golden_ShurikenP>();
            Item.shootSpeed = 10f;
            Item.useTurn = true;
            Item.maxStack = 999;
            Item.consumable = true;
            Item.noUseGraphic = true;
        }
        [JITWhenModsEnabled("CalamityMod")]
        public void SetDefaultsCalamity()
        {
            Item.damage = 20;
            Item.DamageType = ModContent.GetInstance<RogueDamageClass>();
        }
        [JITWhenModsEnabled("CalamityMod")]
        private void StealStrike(Player player, Vector2 position,Vector2 velocity,int type,int damage,float knockback)
        {
            if (player.Calamity().StealthStrikeAvailable())
            {
                var p = Projectile.NewProjectile(Projectile.GetSource_None(), position, velocity, type, damage, knockback);
                Main.projectile[p].penetrate *= 2; 
            }
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (RemnantOfTheAncientsMod.CalamityMod != null) StealStrike(player, position, velocity, type, damage, knockback);
            return true;      
        }
        
        public override void AddRecipes()
        {
            CreateRecipe(100)
            .AddRecipeGroup("GoldBar", 1)
            .AddIngredient(ItemID.Shuriken, 100)
            .AddTile(TileID.Anvils)
            .Register();
        }
    }
}
