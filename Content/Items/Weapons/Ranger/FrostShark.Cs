using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using RemnantOfTheAncientsMod.Content.Projectiles.Multiclass;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using RemnantOfTheAncientsMod.Content.Projectiles.Ranger;
using RemnantOfTheAncientsMod.Content.Items.Items;
using RemnantOfTheAncientsMod.Content.Projectiles.BossProjectiles.Frozen;

namespace RemnantOfTheAncientsMod.Content.Items.Weapons.Ranger
{
    public class FrostShark : ModItem
	{
		public override void SetStaticDefaults()
		{
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
			Item.damage = RemnantOfTheAncientsMod.CalamityMod != null ? 26 : 24;
			Item.DamageType = DamageClass.Ranged;
			Item.width = 64;
			Item.height = 28;
			Item.useTime = 8;
			Item.useAnimation = 8;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.noMelee = true;
			Item.knockBack = 1;
			Item.value = Item.sellPrice(0,3,0,0);
			Item.rare = ItemRarityID.Pink;
			Item.UseSound = SoundID.Item31;
			Item.autoReuse = true;
			Item.shoot = ProjectileID.PurificationPowder;
			Item.shootSpeed = 10f;
			Item.useAmmo = AmmoID.Bullet;
		}
        public override void AddRecipes()
        {

            CreateRecipe()
             .AddIngredient(ItemID.Minishark, 1)
             .AddIngredient(ModContent.ItemType<Ice_escense>(), 5)
             .AddTile(RemnantOfTheAncientsMod.CalamityMod != null? TileID.MythrilAnvil : TileID.Anvils)
             .Register();
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			if (Main.rand.NextFloat() <= (float)1 / 5)
				Projectile.NewProjectile(source, position, velocity, ModContent.ProjectileType<ice_bulletP>(), damage, 1, player.whoAmI);
			if (Main.rand.NextFloat() <= (float)1 / 10)
				Projectile.NewProjectile(source, position, velocity, ModContent.ProjectileType<FrostBeamF>(), 30, 1, player.whoAmI);

			if (RemnantOfTheAncientsMod.CalamityMod != null)
			{
				if (Main.rand.NextFloat() <= (float)1 / 1)
					Projectile.NewProjectile(source, position, velocity, ModContent.ProjectileType<frozen_p_f>(), 8, 1, player.whoAmI);
			}
			return true;
		}
        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
			if(type == ProjectileID.Bullet) type = ModContent.ProjectileType<EnchantedIceBullet>();

            base.ModifyShootStats(player, ref position, ref velocity, ref type, ref damage, ref knockback);
        }
        public override Vector2? HoldoutOffset()
		{
			return new Vector2(-10, 0);
		}
		public override bool CanConsumeAmmo(Item ammo, Player player)
		{
			return Main.rand.NextFloat() >= .10f;
		}
	}
}
