using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using RemnantOfTheAncientsMod.Common.UtilsTweaks;
using System;
using RemnantOfTheAncientsMod.Common.Global.Items;
using RemnantOfTheAncientsMod.Content.Items.Items;

namespace RemnantOfTheAncientsMod.Content.Items.Weapons.Ranger
{
    public class FrozenNova : ModItem
	{
		public int Timmer = 0;
		public int TimmerMax = (int)Utils1.FormatTimeToTick(0, 0, 0, 6);
		public int TimmerDust = 0;
		public int TimmerDustMax = (int)Utils1.FormatTimeToTick(0, 0, 0, 2);
		public override void SetStaticDefaults()
		{	
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}
		public override void SetDefaults()
		{
			Item.damage = 400;
			Item.DamageType = DamageClass.Ranged;
			Item.width = 2;
			Item.height = 2;
			Item.useTime = 20;
			Item.useAnimation = 20;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.noMelee = true;
			Item.knockBack = 45;
			Item.value = 20000;
			Item.rare = ItemRarityID.Red;
			Item.UseSound = SoundID.Item38;
			Item.autoReuse = true;
			Item.shoot = ProjectileID.PurificationPowder;
			Item.shootSpeed = 20f;
			Item.useAmmo = AmmoID.Snowball;
            Item.GetGlobalItem<RemnantGlobalItem>().CanCharge = true;
        }
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			int numProj = 5;
			for (int i = -1; i <= 0; i++)
			{
                Vector2 unacurency = velocity.RotatedBy(MathHelper.ToRadians(Utils1.GetSign(i) * 45));
				var a = Projectile.NewProjectile(source, position, unacurency, type, damage, 1, player.whoAmI);
                Main.projectile[a].scale = 3.0f;
                Main.projectile[a].penetrate = 4;
			}
			for (int i = 0; i < numProj; i++)
			{
				Vector2 NewVelocity = velocity * (1.0f + i * 0.1f);
                var a = Projectile.NewProjectile(source, position, NewVelocity, type, damage - 1 *(15 * numProj), 1, player.whoAmI);
                Main.projectile[a].scale = 3.0f + i;
                Main.projectile[a].penetrate = 1;
                Main.projectile[a].ai[2] = i;
                if (i < 4 && velocity.Y > 32.5f && velocity.Y < 33)
                {
                    Main.projectile[a].velocity -= new Vector2(0, 30);
                }
            }
			return false;
		}
        public override bool CanShoot(Player player)
        {
			TimmerMax = (int)Utils1.FormatTimeToTick(0, 0, 0, (Item.useTime/10));
			TimmerDustMax = TimmerMax / 3;
            RemnantPlayer.GenericChargeCouldownMax = TimmerMax / 10;
            RemnantPlayer.GenericChargeCouldown = Timmer / 10;
            if (Timmer < TimmerMax)
			{			
				Timmer++;
				SpawnDust(player);
            }
			else
			{
				Timmer = 0;

            }
            return Timmer >= TimmerMax;
        }

		public void SpawnDust(Player player)
		{
			int seconds = 3;
			if (TimmerDust >= TimmerDustMax)
			{
				for (int i = 0; i < 360; i += 360 / seconds) 
				{
					double angle = MathHelper.ToRadians(i);
					Vector2 position = player.Center + new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle)) * 50; 
					Vector2 direction = new((float)Math.Cos(angle), (float)Math.Sin(angle));

					Dust d = Dust.NewDustPerfect(position, DustID.Snow, direction * 2f, 0, Color.White, 1f);
					d.noGravity = true;
				}
				TimmerDust = 0;
            }
			else
			{
				TimmerDust++;
            }
        }    
		public override Vector2? HoldoutOffset()
		{
			return new Vector2(-10, 0);
		}
        public override void UpdateInventory(Player player)
        {
            base.UpdateInventory(player);
        }
        public override void AddRecipes()
		{
			CreateRecipe()
			.AddIngredient<SnowBallBazooka>()
            .AddIngredient(ItemID.LunarBar, 5)
            .AddIngredient<CelestialAmalgamate>(5)
			.AddTile(TileID.LunarCraftingStation)
			.Register();
		}
	}
}
 