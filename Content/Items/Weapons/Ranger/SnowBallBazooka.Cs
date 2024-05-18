using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using RemnantOfTheAncientsMod.Common.UtilsTweaks;
using System;

namespace RemnantOfTheAncientsMod.Content.Items.Weapons.Ranger
{
    public class SnowBallBazooka : ModItem
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
			Item.damage = 70;
			Item.DamageType = DamageClass.Ranged;
			Item.width = 2;
			Item.height = 2;
			Item.useTime = 40;
			Item.useAnimation = 40;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.noMelee = true;
			Item.knockBack = 15;
			Item.value = 20000;
			Item.rare = ItemRarityID.Orange;
			Item.UseSound = SoundID.Item38;
			Item.autoReuse = true;
			Item.shoot = ProjectileID.PurificationPowder;
			Item.shootSpeed = 20f;
			Item.useAmmo = AmmoID.Snowball;	
		}
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			var a = Projectile.NewProjectileDirect(source, position, velocity, type, damage, 1, player.whoAmI);
			a.scale = 2.0f;
			a.penetrate = 2;
			return false;
		}
        public override bool CanShoot(Player player)
        {
			TimmerMax = (int)Utils1.FormatTimeToTick(0, 0, 0, (Item.useTime/10));
			TimmerDustMax = TimmerMax / 3;
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
			if (TimmerDust >= TimmerDustMax)
			{
				for (int i = 0; i < 360; i += 36) // Ajusta el ángulo para la densidad del círculo
				{
					double angle = MathHelper.ToRadians(i);
					Vector2 position = player.Center + new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle)) * 50; // Ajusta el radio del círculo
					Vector2 direction = new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle)); // Vector de dirección

					Dust d = Dust.NewDustPerfect(position, DustID.Snow, direction * 2f, 0, Color.White, 1f); // Reemplaza "TuModDust" con el nombre de tu clase de polvo}
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
			.AddIngredient(ItemID.SnowballCannon, 1)
            .AddIngredient(ItemID.HellstoneBar, 6)
            .AddIngredient(ItemID.Bone, 5)
            .AddIngredient(ItemID.IllegalGunParts, 1)
			.AddTile(TileID.Anvils)
			.Register();
		}
	}
}
 