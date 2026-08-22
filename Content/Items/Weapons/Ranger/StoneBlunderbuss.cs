using Microsoft.Xna.Framework;
using RemnantOfTheAncientsMod.Content.Projectiles.HeldItem;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Content.Items.Weapons.Ranger
{
	public class StoneBlunderbuss : ModItem
	{
		public override void SetStaticDefaults()
		{
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}
		public override void SetDefaults()
		{
			Item.damage = 10;
			Item.DamageType = DamageClass.Ranged;
			Item.width = 2;
			Item.height = 2;
			Item.useTime = 26;
			Item.useAnimation = 26;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.noMelee = true;
			Item.noUseGraphic = true;
			Item.scale = 1f;
			Item.knockBack = 1;
			Item.value = Item.sellPrice(0, 0, 20, 0);
			Item.rare = ItemRarityID.Green;
			//Item.UseSound = SoundID.Item38;
			Item.autoReuse = true;
			Item.shoot = ProjectileID.Bullet;
			Item.shootSpeed = 10f;
		}
        public override void HoldItem(Player player)
		{ 
            if (player.ownedProjectileCounts[ModContent.ProjectileType<StoneBlunderbusHeldProjectile>()] < 1)
                Projectile.NewProjectile(Projectile.GetSource_None(), player.Center + new Vector2(16 * player.direction, 0), Vector2.Zero, ModContent.ProjectileType<StoneBlunderbusHeldProjectile>(), 0, Item.knockBack, player.whoAmI);
            base.HoldItem(player);
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            return false;
        }
		public override void AddRecipes(){}
	}
}
