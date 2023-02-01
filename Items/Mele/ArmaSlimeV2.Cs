using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.GameContent.Creative;

namespace RemnantOfTheAncientsMod.Items.Mele
{
    public class ArmaSlimeV2 : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("SlimeGun V2");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Polish), "SlimeGun V2");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "SlimeGun V2");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Arma Slime V2");
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
            Item.shoot = ProjectileID.Arkhalis; 
            Item.shootSpeed = 100f;
            Item.noUseGraphic = false;
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-10, 0);
        }
        public override void OnHitNPC(Player player, NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.BrokenArmor, 1080);
            target.AddBuff(BuffID.Slow, 1080);
        }
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
