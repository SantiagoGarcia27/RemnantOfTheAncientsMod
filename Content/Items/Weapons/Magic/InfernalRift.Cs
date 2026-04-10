using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using RemnantOfTheAncientsMod.Common.UtilsTweaks;
using RemnantOfTheAncientsMod.Common.Global;

namespace RemnantOfTheAncientsMod.Content.Items.Weapons.Magic
{
	public class InfernalRift : ModItem
	{
		public override void SetStaticDefaults()
		{
			Item.staff[Item.type] = true;
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
			Item.width = 70;
			Item.height = 70;
			Item.useTime = 5;
            Item.useAnimation = 5;
            Item.UseSound = SoundID.Item34;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.shootSpeed = 14f;
			Item.shoot = ProjectileID.Flames;
			Item.value = Item.sellPrice(gold: 8);
			Item.autoReuse = true;
			Item.ResearchUnlockCount = 1;
			Item.GetGlobalItem<CustomTooltip>().SecondHabilitie = true;
        }

		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			if (player.altFunctionUse == 2)
			{
                Vector2 altVelocity = Vector2.Normalize(velocity) * 10f;
                for (int i = -20; i <= 20; i += 10)
				{
					SpawnWeakAttack(i, 0, player, source, position, altVelocity, type, damage, knockback);
                }
                for (int i = -20; i <= 20; i += 10)
                {
                    SpawnWeakAttack(0, i, player, source, position, altVelocity, type, damage, knockback);
                }

				int p = Projectile.NewProjectile(source, position, altVelocity, type, damage / 5, knockback, player.whoAmI, 0);
				Main.projectile[p].scale = 10f;
				Main.projectile[p].timeLeft = 20000;

			}
			else
			{
				Vector2 pos;
                Vector2 altVelocity = Vector2.Normalize(velocity) * 15f;
                Projectile.NewProjectile(source, position, altVelocity, type, damage, knockback, player.whoAmI, 1);
				pos = position + new Vector2(0, -5f * 16f);
				if (!DistanceUtils.ExistTile(pos))
				{
					Projectile.NewProjectile(source, pos, altVelocity, type, damage / 3, knockback, player.whoAmI, 1);
				}


				pos = position + new Vector2(0, +5f * 16f);
                if (!DistanceUtils.ExistTile(pos))
				{
					Projectile.NewProjectile(source, pos, altVelocity, type, damage / 3, knockback, player.whoAmI, 1);
				}
            }
            return false;
        }

        public override void ModifyManaCost(Player player, ref float reduce, ref float mult)
        {
	
			if (player.altFunctionUse == 2)
			{
                int coste1 = 6; int coste2 = 4;
                reduce = coste1 - coste2;
            }
        }
        public void SpawnWeakAttack(float x, float y, Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			if (!DistanceUtils.ExistTile(position + new Vector2(x * 16f, y * 16f)))
			{
				int p =Projectile.NewProjectile(source, position, velocity + new Vector2(x , y), type, damage / 5, knockback, player.whoAmI, 0);
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