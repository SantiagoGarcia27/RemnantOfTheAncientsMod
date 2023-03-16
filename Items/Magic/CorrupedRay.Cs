using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.GameContent.Creative;
using Terraria.Localization;
using Microsoft.Xna.Framework;
using RemnantOfTheAncientsMod.Projectiles.Mage;
using Terraria.DataStructures;

namespace RemnantOfTheAncientsMod.Items.Magic
{
	public class CorrupedRay : ModItem
	{
		public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("Shadow Ray");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Polish), "Promień cienia");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Rayon d'ombre");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Rayo de sombra");
			Item.staff[Item.type] = true;
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
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

            if (RemnantOfTheAncientsMod.CalamityMod != null)
            {
                Item.damage = 26;
                Item.useTime = 15;
            }
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            float numberProjectiles = 2;
            float rotation = MathHelper.ToRadians(5);

            position += Vector2.Normalize(velocity) * 1f;
            if (Main.rand.NextFloat() <= (float)2 / 10)
            {
                for (int i = 0; i < numberProjectiles; i++)
                {
                    Vector2 perturbedSpeed = velocity.RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))) * .2f;
                    int p = Projectile.NewProjectile(source, position, perturbedSpeed * 10, ModContent.ProjectileType<CustomBolt>(), damage, knockback, player.whoAmI);
                    Main.projectile[p].stepSpeed = 7f;
                    Main.projectile[p].GetAlpha(Color.Violet);
                }
            }
            //Projectile.NewProjectile(source, position, velocity, type, damage, 1, player.whoAmI);
            return true;
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
