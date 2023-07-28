using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Localization;
using Microsoft.Xna.Framework;
using static Terraria.ModLoader.ModContent;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using RemnantOfTheAncientsMod.Content.Items.Items;
using RemnantOfTheAncientsMod.Content.Projectiles.Summon.Minioms;
using RemnantOfTheAncientsMod.Content.Buffs.Buffs.Minions;

namespace RemnantOfTheAncientsMod.Content.Items.Weapons.Summon
{
    public class DesertStaff : ModItem
    {
        public override void SetStaticDefaults()
        {
           // //DisplayName.SetDefault("Desert Staff");
           // //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Bastón del desierto");
           // //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Polish), "Desert Staff");

            //Tooltip.SetDefault("Summons a Baby Desert Aniquilator to fight for you");
            //Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Invoca a un bebé aniquilador del desierto para que luche por ti");
            //Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Polish), "Przywołuje Baby Desert Aniquilator, aby walczył dla ciebie");

            ItemID.Sets.GamepadWholeScreenUseRange[Item.type] = true;
            ItemID.Sets.LockOnIgnoresCollision[Item.type] = true;
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.damage = 15;
            Item.knockBack = 3f;
            Item.mana = 10;
            Item.width = 32;
            Item.height = 32;
            Item.useTime = 36;
            Item.useAnimation = 36;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.value = Item.buyPrice(0, 1, 0, 0);
            Item.rare = ItemRarityID.Orange;
            Item.UseSound = SoundID.Item44;
            Item.noMelee = true;
            Item.DamageType = DamageClass.Summon;
            Item.buffType = BuffType<DesertMinionBuff>();
            Item.shoot = ProjectileType<DesertMinion>();
        }
        /*public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
//player.AddBuff(Item.buffType, 2);
/*var projectile = Projectile.NewProjectileDirect(source, position, velocity, type, damage, knockback, Main.myPlayer);
projectile.originalDamage = Item.damage;
return false;*//*
RemnantPlayer Modplayer = player.RemnantOfTheAncientsMod();
Modplayer.DesertMinion = true;
if (player.altFunctionUse != 2)
{
                Modplayer.DesertMinion = true;
                position = Main.MouseWorld;
	velocity = new Vector2(0,0);
                var p  = Projectile.NewProjectile(source ,position, velocity, type, damage, knockback, player.whoAmI, 0f, 0f);
                Main.projectile[p].originalDamage = Item.damage;
            }
            return false;
        }*/
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            player.AddBuff(Item.buffType, 2);
            var projectile = Projectile.NewProjectileDirect(source, position, velocity, type, damage, knockback, Main.myPlayer);
            projectile.originalDamage = Item.damage;
            return false;
        }
        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            position = Main.MouseWorld;
        }
        /*public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback) 
		{
player.AddBuff(Item.buffType, 2);
position = Main.MouseWorld;
		}*/

        public override void AddRecipes()
        {
            CreateRecipe()
             .AddIngredient(ItemID.SlimeStaff, 1)
             .AddIngredient(ModContent.ItemType<Sand_escense>(), 5)
             .AddTile(TileID.Anvils)
             .Register();
        }
    }
}