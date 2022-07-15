using Terraria;
using Terraria.ID;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;

namespace opswordsII.Items.Magic
{
    public class LaserMachineGun : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("L.M.G");
            Tooltip.SetDefault("Legendary Drop");
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
			Item.damage = 95;
			Item.noMelee = true;
			Item.DamageType = DamageClass.Magic;
			Item.channel = true; 
			Item.mana = 2;
			Item.rare = -12;
			Item.width = 28;
			Item.height = 30;
			Item.useTime = 5;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.shootSpeed = 14f;
			Item.useAnimation = 5;
			Item.shoot = ProjectileID.LaserMachinegunLaser;
			Item.UseSound = SoundID.Item39;  
			Item.value = 600000;
			Item.autoReuse = true;
		}
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			float numberProjectiles = 5 + Main.rand.Next(4);
			float rotation = MathHelper.ToRadians(4);
			position += Vector2.Normalize(velocity) * 45f;
			for (int i = 0; i < numberProjectiles; i++)
			{
				Vector2 perturbedSpeed = velocity.RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))) * .2f;
				Projectile.NewProjectile(source, position, perturbedSpeed, ProjectileID.LaserMachinegunLaser, 40, knockback, player.whoAmI);
			}
			return false;
		}
	}
}