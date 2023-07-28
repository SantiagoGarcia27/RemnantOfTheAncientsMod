using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.GameContent.Creative;
using Terraria.DataStructures;
using RemnantOfTheAncientsMod.Content.Projectiles.Melee;

namespace RemnantOfTheAncientsMod.Content.Items.Weapons.Melee
{
    public class ArmaSlimeV2 : ModItem
    {
        public override void SetStaticDefaults()
        {
           // //DisplayName.SetDefault("SlimeGun V2");
           // //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Polish), "SlimeGun V2");
           // //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "SlimeGun V2");
           // //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Arma Slime V2");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
            /*Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(30, 10));//5,4 //30 9 */
        }
        public override void SetDefaults()
        {
            Item.damage = 30;
            Item.DamageType = DamageClass.Melee;
            Item.channel = true;
            Item.width = 80; 
            Item.height = 40; 
            Item.useTime = 20; 
            Item.useAnimation = 20;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noMelee = true;
            Item.knockBack = 1;
            Item.value = 45000; 
            Item.rare = ItemRarityID.Orange;
            Item.UseSound = SoundID.Item10;
            Item.autoReuse = true;
            Item.shoot = ProjectileID.Terragrim; 
            Item.shootSpeed = 100f;
            Item.noUseGraphic = false;
        } 
        public override bool? UseItem(Player player)
        {
           // Projectile.NewProjectile(Projectile.GetSource_None(), player.position, Vector2.Zero, ModContent.ProjectileType<SlimeV2>(), 0, 0, Main.myPlayer);
            return base.UseItem(player);
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            return base.Shoot(player, source, position, velocity, type, damage, knockback);
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-10, 0);
        }
        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.BrokenArmor, 1080);
            target.AddBuff(BuffID.Slow, 1080);
        }
        //public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        //{
        //    for (int i = 0; i < 390; i++)//30
        //    {
        //        float theta = Main.rand.NextFloat(MathHelper.TwoPi);
        //        float x = 3f * Main.rand.NextFloat();
        //        Vector2 vel = new Vector2(x, 0f).RotatedBy(theta);

        //        int dust = Dust.NewDust(position + (new Vector2(32,0)* player.direction), 10,10, DustID.AncientLight);
        //        Main.dust[dust].velocity = vel;
        //        Main.dust[dust].noGravity = true;
        //    }
        //    return true;
        //}
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ItemID.SlimeGun, 1)
            .AddIngredient(ItemID.Terragrim, 1)
            .AddIngredient(ItemID.Gel, 10)
            .AddTile(TileID.Solidifier)
            .Register();
        }
    }
}
