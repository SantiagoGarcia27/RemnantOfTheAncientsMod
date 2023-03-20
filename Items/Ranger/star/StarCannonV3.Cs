using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Localization;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using RemnantOfTheAncientsMod.Projectiles;

namespace RemnantOfTheAncientsMod.Items.Ranger.star
{
	public class StarCannonV3 : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Star Cannon Mk3");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Canon étoile Mk3");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Cañon de estrellas MK3");
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}
		public override void SetDefaults()
		{
            Item.damage = 400;//305
            Item.DamageType = DamageClass.Ranged;
            Item.width = 80;
            Item.height = 40; 
            Item.useTime = 9; 
            Item.useAnimation = 9;
            Item.useStyle = ItemUseStyleID.Shoot; 
            Item.noMelee = true;
            Item.knockBack = 1;
            Item.value = 100000; 
            Item.rare = ItemRarityID.Red;
   		    Item.UseSound = SoundID.Item10;
            Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<CloneFallingStar>(); 
            Item.shootSpeed = 10f;
            Item.useAmmo = AmmoID.FallenStar;
            if (RemnantOfTheAncientsMod.CalamityMod != null)
            {
                Item.damage = 300;  
            }
        }
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback){
			
			if (Main.rand.NextFloat() <= (float)1 / 10) {
				Projectile.NewProjectile(source, position, velocity, ModContent.ProjectileType<CloneStarWrath>(), Item.damage * 4, 1, player.whoAmI);
                return false;
            }


            if (Main.rand.NextFloat() <= (float)1 / 3)
			{
				Projectile.NewProjectile(source, position, velocity, ModContent.ProjectileType<CloneStarFury>(), Item.damage * 2, 1, player.whoAmI);
                return false;
            }
			
			return true;
		}
		public override Vector2? HoldoutOffset()
		{
			return new Vector2(-20, 0);
		}

		public override bool CanConsumeAmmo(Item ammo, Player player)
		{
			return Main.rand.NextFloat() >= .80f;
		}
		public override void AddRecipes()
		{
			CreateRecipe()
			.AddIngredient(ModContent.ItemType<StarCannonV2>())
			.AddIngredient(ItemID.LunarBar, 10)
			.AddIngredient(ItemID.FragmentVortex, 5)
			.AddTile(TileID.LunarCraftingStation)
			.Register();
		}
	}
}
