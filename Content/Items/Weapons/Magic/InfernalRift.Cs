using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using RemnantOfTheAncientsMod.Content.Projectiles.BossProjectile;
using RemnantOfTheAncientsMod.Common.UtilsTweaks;
using Mono.Cecil;
using RemnantOfTheAncientsMod.Common.Global;

namespace RemnantOfTheAncientsMod.Content.Items.Weapons.Magic
{
	public class InfernalRift : ModItem
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Tyrant blast");
			//DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Polish), "Wybuch tyrana");
			//DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Explosion de tyran");
			//DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Rafaga tirana");
			Item.staff[Item.type] = true;
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
			ItemID.Sets.ItemsThatAllowRepeatedRightClick[Type] = true;
		}

		public override void SetDefaults()
		{
			Item.damage = 60;
			Item.noMelee = true;
			Item.DamageType = DamageClass.Magic;
			Item.channel = true;
			Item.mana = 6;
			Item.rare = ItemRarityID.Yellow;
			Item.width = 72;
			Item.height = 74;
			Item.useTime = 5;
            Item.useAnimation = 5;
            Item.UseSound = SoundID.Item34;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.shootSpeed = 14f;
			Item.shoot = ProjectileID.Flames;
			Item.value = Item.sellPrice(gold: 8);
			Item.autoReuse = true;
			Item.scale /= 1f;
            Item.GetGlobalItem<CustomTooltip>().SecondHabilitie = true;
        }

		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			if (player.altFunctionUse == 2)
			{
                Item.mana = 4;
                Item.shootSpeed = 10f;

				for(int i = -20; i <= 20; i += 10)
				{
					SpawnWeakAttack(i, 0, player, source, position, velocity, type, damage, knockback);
                }
                for (int i = -20; i <= 20; i += 10)
                {
                    SpawnWeakAttack(0, i, player, source, position, velocity, type, damage, knockback);
                }

    //            Projectile.NewProjectile(source, position + new Vector2(10 * 16f, 0f), velocity, type, damage / 5, knockback, player.whoAmI, 0);
				//Projectile.NewProjectile(source, position + new Vector2(-10 * 16f, 0f), velocity, type, damage / 5, knockback, player.whoAmI, 0);
				//Projectile.NewProjectile(source, position + new Vector2(0, 10f * 16f), velocity, type, damage / 5, knockback, player.whoAmI, 0);
				//Projectile.NewProjectile(source, position + new Vector2(0, -10f * 16f), velocity, type, damage / 5, knockback, player.whoAmI, 0);

				//Projectile.NewProjectile(source, position + new Vector2(0, -20f * 16f), velocity, type, damage / 5, knockback, player.whoAmI, 0);
				//Projectile.NewProjectile(source, position + new Vector2(0, +20f * 16f), velocity, type, damage / 5, knockback, player.whoAmI, 0);
				//Projectile.NewProjectile(source, position + new Vector2(-20f * 16f, 0), velocity, type, damage / 5, knockback, player.whoAmI, 0);
				//Projectile.NewProjectile(source, position + new Vector2(-20f * 16f, 0), velocity, type, damage / 5, knockback, player.whoAmI, 0);

				int p = Projectile.NewProjectile(source, position, velocity, type, damage / 5, knockback, player.whoAmI, 0);
				Main.projectile[p].scale = 10f;
				Main.projectile[p].timeLeft = 20000;

			}

			else
			{
				Item.shootSpeed = 15f;
				Vector2 pos;

				Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI, 1);
				pos = position + new Vector2(0, -5f * 16f);
				if (!DistanceUtils.ExistTile(pos))
				{
					Projectile.NewProjectile(source, pos, velocity, type, damage / 3, knockback, player.whoAmI, 1);
				}


				pos = position + new Vector2(0, +5f * 16f);
                if (!DistanceUtils.ExistTile(pos))
				{
					Projectile.NewProjectile(source, pos, velocity, type, damage / 3, knockback, player.whoAmI, 1);
				}
            }
            return false;
        }
		public void SpawnWeakAttack(float x, float y, Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			if (!DistanceUtils.ExistTile(position + new Vector2(x * 16f, y * 16f)))
			{
				int p =Projectile.NewProjectile(source, position /*+ new Vector2(x * 16f, y * 16f)*/, velocity + new Vector2(x , y), type, damage / 5, knockback, player.whoAmI, 0);
				Main.projectile[p].penetrate *= 3; 
			}
        }
        public override bool AltFunctionUse(Player player) => true;
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ModContent.ItemType<FireRift>(), 1)
            .AddIngredient(ItemID.InfernoFork, 1)
            .AddIngredient(ItemID.Ectoplasm,4)
            .AddTile(TileID.MythrilAnvil)
            .Register();
        }
    }
}