using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.DataStructures;

namespace opswordsII.Items.ArmasMiselaneas
{
	public class   ArmaSlimeV2 : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("SlimeGun V2"); 
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Polish), "SlimeGun V2");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "SlimeGun V2");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Arma Slime V2");

			/*Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(30, 10));//5,4 //30 9 */
		}
		public override void SetDefaults()
		{
            Item.damage = 30;
            Item.DamageType = DamageClass.Ranged;
			Item.DamageType = DamageClass.Magic;
			Item.DamageType = DamageClass.Melee;
			Item.DamageType = DamageClass.Summon;
			
            Item.channel = true;
            Item.width = 80; //Tamaño de Ancho
            Item.height = 40; //Tamaño de Alto
            Item.useTime = 20; //mientras mas alto sea el useTime mas lenta será el arma. Usa un bajo UseTime para que el arma sea Rapida
            Item.useAnimation = 20;
            Item.useStyle = ItemUseStyleID.HoldUp; //Dejar en 5 para que el personaje use el arma de forma normal
            Item.noMelee = true;
            Item.knockBack = 1;
            Item.value = 45000; //Precio
            Item.rare = 3;
   		    Item.UseSound = SoundID.Item10;
            
            Item.autoReuse = true;
            Item.shoot = 595; //dejar en 10 para que dispare balas normales
            Item.shootSpeed = 100f;
			Item.noUseGraphic = false;
        }
   		/*public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
		{
			Vector2 muzzleOffset = Vector2.Normalize(new Vector2(speedX, speedY)) * 25f;
			if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
			{
				position += muzzleOffset;
			}
			if (Main.rand.NextFloat () <= (float) 100 / 100) Projectile.NewProjectile(position.X, position.Y, speedX, speedY,ModContent.ProjectileType<Projectiles.SlimeV2>(), 0, 1, player.whoAmI);

			
			return true;
		
		}*/
         public override Vector2? HoldoutOffset()
		{
			return new Vector2(-10, 0);
		}
    public override void OnHitNPC(Player player, NPC target, int damage, float knockback, bool crit)
		{
			// Add Onfire buff to the NPC for 1 second
			// 60 frames = 1 second
			target.AddBuff(BuffID.BrokenArmor, 1080);
			target.AddBuff(BuffID.Slow, 1080);
        }
     public override void AddRecipes()	   
     	{
            CreateRecipe()		
	        .AddIngredient(ItemID.SlimeGun, 1)
	        .AddIngredient(ItemID.Arkhalis, 1)
	        .AddIngredient(ItemID.Gel, 10)
	        .AddTile(TileID.Solidifier)
			.Register();
	    }
	}
} 