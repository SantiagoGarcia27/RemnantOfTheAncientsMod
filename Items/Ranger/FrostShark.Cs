using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Localization;
using RemnantOfTheAncientsMod.Projectiles;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using RemnantOfTheAncientsMod.Projectiles.BossProjectile;

namespace RemnantOfTheAncientsMod.Items.Ranger
{
    public class FrostShark : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Frozen Shark");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Requin congelé");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Tiburón Congelado");
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}
		public override void SetDefaults()
		{
			Item.damage = 24;
			Item.DamageType = DamageClass.Ranged;
			Item.width = 80;
			Item.height = 40;
			Item.useTime = 8;
			Item.useAnimation = 8;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.noMelee = true;
			Item.knockBack = 1;
			Item.value = 1000;
			Item.rare = ItemRarityID.Pink;
			Item.UseSound = SoundID.Item31;
			Item.autoReuse = true;
			Item.shoot = ProjectileID.JestersArrow;
			Item.shootSpeed = 10f;
			Item.useAmmo = AmmoID.Bullet;

			if (ModLoader.TryGetMod("CalamityMod", out Mod mod)) Item.damage = 26;

		}

		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			if (Main.rand.NextFloat() <= (float)1 / 5)
				Projectile.NewProjectile(source, position, velocity, ModContent.ProjectileType<ice_bulletP>(), damage, 1, player.whoAmI);
			if (Main.rand.NextFloat() <= (float)1 / 10)
				Projectile.NewProjectile(source, position, velocity, ModContent.ProjectileType<FrostBeamF>(), 30, 1, player.whoAmI);

			if (ModLoader.TryGetMod("CalamityMod", out Mod mod))
			{
				if (Main.rand.NextFloat() <= (float)1 / 1)
					Projectile.NewProjectile(source, position, velocity, ModContent.ProjectileType<frozen_p_f>(), 8, 1, player.whoAmI);
				if (Main.rand.NextFloat() <= (float)1 / 10)
					Projectile.NewProjectile(source, position, velocity, ModContent.ProjectileType<FrostBeamF>(), 30, 1, player.whoAmI);
			}
			return true;
		}
        public override Vector2? HoldoutOffset()
		{
			return new Vector2(-10, 0);
		}
        public override bool CanConsumeAmmo(Item ammo,Player player)
		{
			return Main.rand.NextFloat() >= .10f;	
		}
	}
}
