/*using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria.Localization;

namespace opswordsII.Items.Ranger
{
	public class ultratiburonDOG : ModItem
	{
		public override bool Autoload(ref string name)
		{
		return ModLoader.GetMod("CalamityMod") != null;
		}
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Punisher of Gods");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Polish), "Punisher of Gods");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Punisher of Gods");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Castigadora de Dioses");
			
			
			Tooltip.SetDefault(""
			+ "\n90% chance of not spending ammo ");
           Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Polish), ""
			+ "\n90% szans na nie wydawanie amunicji ");
            Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), ""
			+ "\n90% de chances de ne pas dépenser de munitions ");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), ""
			+ "\nProbabilidad del 90% de no gastar munición ");

		}
		public override void SetDefaults()
		{
            Item.damage = 530;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 80; //Tamaño de Ancho
            Item.height = 40; //Tamaño de Alto
            Item.useTime = 2; //mientras mas alto sea el useTime mas lenta será el arma. Usa un bajo UseTime para que el arma sea Rapida
            Item.useAnimation = 10;
            Item.useStyle = 5; //Dejar en 5 para que el personaje use el arma de forma normal
            Item.noMelee = true;
            Item.knockBack = 1;
            Item.value = Item.sellPrice(1, 50, 2, 0);
            Item.rare = 10;
   		    Item.UseSound = SoundID.Item12;
            Item.autoReuse = true;
            Item.shoot = 10; //dejar en 10 para que dispare balas normales
            Item.shootSpeed = 100f;
            Item.useAmmo = AmmoID.Bullet;
			
		}
         /*  public override bool CanConsumeAmmo(Player player)
		{
			return Main.rand.NextFloat() >= .90f;
		}
          public override Vector2? HoldoutOffset()
		{
			return new Vector2(-30, 0);
		}
		public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
		{
			Vector2 muzzleOffset = Vector2.Normalize(new Vector2(speedX, speedY)) * 25f;
			if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
			{
				position += muzzleOffset;
			}
			if (Main.rand.NextFloat () <= (float) 1 / 100) Projectile.NewProjectile(position.X, position.Y, speedX, speedY,ModContent.ProjectileType<("MisilUltra"), 10955, 1, player.whoAmI);

			
			return true;
		
			int numberProjectiles = 4 + Main.rand.Next(2); // 4 or 5 shots
			for (int i = 0; i < numberProjectiles; i++)
			
				
			return true;
			
		 // return false because we don't want tmodloader to shoot projectile
		}
		public override void AddRecipes()
		{
			CreateRecipe()
			.AddIngredient(null, "ultratiburon", 1)
			.AddIngredient(ModLoader.GetMod("CalamityMod").ItemType("CosmiliteBar"), 15)
			.AddIngredient(ModLoader.GetMod("CalamityMod").ItemType("Phantoplasm"), 5)
			.AddIngredient(ModLoader.GetMod("CalamityMod").ItemType("EndothermicEnergy"), 10)
			.AddIngredient(ModLoader.GetMod("CalamityMod").ItemType("NightmareFuel"), 10)
			.AddIngredient(ModLoader.GetMod("CalamityMod").ItemType("GalacticaSingularity"), 15)
			.AddIngredient(ModLoader.GetMod("opswordsII").ItemType("CelestialAmalgamate"), 3)
			.AddTile(ModLoader.GetMod("CalamityMod").TileType<DraedonsForge>())
			.Register();

		}
			
	}
}*/
