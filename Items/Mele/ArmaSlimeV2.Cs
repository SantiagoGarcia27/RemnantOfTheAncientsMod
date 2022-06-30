using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;

namespace opswordsII.Items.Mele
{
    public class ArmaSlimeV2 : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("SlimeGun V2");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Polish), "SlimeGun V2");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "SlimeGun V2");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Arma Slime V2");

            /*Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(30, 10));//5,4 //30 9 */
        }
        public override void SetDefaults()
        {
            Item.damage = 30;
            Item.DamageType = DamageClass.Ranged;
            Item.DamageType = DamageClass.Melee;
            Item.channel = true;
            Item.width = 80; //Tamaño de Ancho
            Item.height = 40; //Tamaño de Alto
            Item.useTime = 20; //mientras mas alto sea el useTime mas lenta será el arma. Usa un bajo UseTime para que el arma sea Rapida
            Item.useAnimation = 20;
            Item.useStyle = ItemUseStyleID.HoldUp; //Dejar en 5 para que el personaje use el arma de forma normal
            Item.noMelee = true;
            Item.knockBack = 1;
            Item.value = 45000; //Precio
            Item.rare = 3;
            Item.UseSound = SoundID.Item10;
            Item.autoReuse = true;
            Item.shoot = 595; //dejar en 10 para que dispare balas normales
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
            .AddIngredient(ItemID.Arkhalis, 1)
            .AddIngredient(ItemID.Gel, 10)
            .AddTile(TileID.Solidifier)
            .Register();
        }
    }
}
