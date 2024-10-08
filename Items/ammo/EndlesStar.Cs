using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;

namespace opswordsII.Items.ammo
{
	public class EndlesStar : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Endless Star");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Étoile sans fin");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Estrella Eterna");
			Tooltip.SetDefault("");
		}

		public override void SetDefaults()
		{
			Item.damage = 0;
			Item.DamageType = DamageClass.Ranged;
			Item.width = 8;
			Item.height = 8;
			Item.maxStack = 1;
			Item.consumable = false;             //You need to set the item consumable so that the ammo would automatically consumed
			Item.knockBack = 3.5f;
			Item.value = 10;
			Item.rare = 7;
			Item.shoot = ProjectileID.FallingStar;//The projectile shoot when your weapon using this ammo
			Item.shootSpeed = 1f;                  //The speed of the projectile
			Item.ammo = AmmoID.FallenStar;              //The ammo class this ammo belongs to.
		
	
		}

        public override void AddRecipes()
		{
			CreateRecipe()
		    .AddIngredient(ItemID.FallenStar,500)
			.AddTile(TileID.TinkerersWorkbench)			
			.Register();
		}
	}
}