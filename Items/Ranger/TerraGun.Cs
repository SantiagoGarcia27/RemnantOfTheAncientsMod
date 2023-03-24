using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Localization;
using Terraria.GameContent.Creative;
using Terraria.DataStructures;
using RemnantOfTheAncientsMod.Projectiles;

namespace RemnantOfTheAncientsMod.Items.Ranger
{
	public class TerraGun : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("TerraGun1");
			DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "La Terra Arma");
			Tooltip.SetDefault("A weapon fit for a legend"
				+ "\n50% chance of not spending ammo"
				+"\nMusquet bullets turn into terra bullet");
			Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "TTim le preguntó por colores y le dijo el arcoiris"
					+ "\nProbabilidad del 50% de no gastar munición " +
					"+\nTransforma las balas de mosquete en ballas terra");
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}
		public override void SetDefaults()
		{
			Item.damage = 128;
			Item.crit = 12;
			Item.DamageType = DamageClass.Ranged;
			Item.width = 86;
			Item.height = 30;
			Item.useTime = 33;
			Item.useAnimation = 33;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.noMelee = true;
			Item.knockBack = 1;
			Item.value = Item.sellPrice(0,15,0,0);
			Item.rare = 11;
			Item.UseSound = SoundID.Item40;
			Item.autoReuse = true;
			Item.shoot = ProjectileID.Bullet;
			Item.shootSpeed = 10f;
			Item.useAmmo = AmmoID.Bullet;
		}
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (type == ProjectileID.Bullet) type = ModContent.ProjectileType<TerraBulletP>();

            const int NumProjectiles = 4;
            for (int i = 0; i < NumProjectiles; i++)
            {
                Vector2 newVelocity = velocity.RotatedByRandom(MathHelper.ToRadians(5));
                newVelocity *= 1f - Main.rand.NextFloat(0.3f);
                Projectile.NewProjectileDirect(source, position, newVelocity, type, damage, knockback, player.whoAmI);
            }

            return false;
        }
        public override Vector2? HoldoutOffset()
		{
			return new Vector2(-10, 0);
		}
        public override bool CanConsumeAmmo(Item ammo,Player player)
		{
			return Main.rand.NextFloat() >= .50f;	
		}
	}
}
